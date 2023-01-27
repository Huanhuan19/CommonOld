using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCYDataSource
{
    public class Protocal
    {
        public static Protocal protocalInstance = new Protocal();
        public List<NewCommandChannelPara> SensorChannels = new List<NewCommandChannelPara>();
    }

    public class ParseBuffer
    {
        #region New 协议

        public static NewCommandChannelPara Parse(int sensorChannalID, byte[] buffer)
        {
            NewCommandChannelPara ncp = new NewCommandChannelPara();
            try
            {
                var data = buffer.Take(2).ToArray();
                if (Enumerable.SequenceEqual(data, NewCommandType.SYN))
                {
                    ncp.IsNewCommandType = true;
                    ncp.IsRightPackage = true;//后面会判断 校验位和结束符
                    byte cmd = buffer[2];//命令字位置
                    int datadLength = buffer[3] * 256 + buffer[4];
                    var dataBuffer = buffer.Skip(5).Take(datadLength).ToArray();
                    switch (cmd)//解析命令字
                    {
                        case (byte)NewCommandType.CommandType.GetDeviceInfo://临时根据力 电流 添加的临时程序
                            {
                                var devicePara = System.Text.Encoding.ASCII.GetString(dataBuffer.Skip(7).Take(3).ToArray());

                                int senserId = Convert.ToInt32(devicePara);
                                if (!Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == sensorChannalID))
                                {
                                    int dataIndex = 0;
                                    if (sensorChannalID > 1)//不是第一通道
                                    {
                                        if (Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == sensorChannalID - 1))//上一个通道加载完毕
                                        {
                                            var lastChannel = Protocal.protocalInstance.SensorChannels.FirstOrDefault(o => o.SensorChannelID == sensorChannalID - 1);//找到该通道
                                            var maxIndex = (from o in lastChannel.dataTypeClasses select o.DataIndex).Max();
                                            dataIndex = maxIndex + 1;
                                        }
                                        else//不进行解析 等待一个通道加载完毕
                                        {
                                            ncp.CommandType = NewCommandType.CommandType.None;
                                            return ncp;
                                        }
                                    }
                                        ncp.SensorChannelID = sensorChannalID;
                                        ncp.CommandType = NewCommandType.CommandType.GetDeviceIDAndDataType;//临时根据力 电流 添加的临时程序

                                        DataTypeClass dtc = new DataTypeClass() { DataIndex = dataIndex, SensorID = senserId};
                                        ncp.dataTypeClasses.Add(dtc);
                                }
                                Protocal.protocalInstance.SensorChannels.Add(ncp);
                            }
                            break;
                        case (byte)NewCommandType.CommandType.GetDeviceIDAndDataType:
                            if (!Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == sensorChannalID))
                            {
                                int dataIndex = 0;
                                if (sensorChannalID > 1)//不是第一通道
                                {
                                    if (Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == sensorChannalID - 1))//上一个通道加载完毕
                                    {
                                        var lastChannel = Protocal.protocalInstance.SensorChannels.FirstOrDefault(o => o.SensorChannelID == sensorChannalID - 1);//找到该通道
                                        var maxIndex = (from o in lastChannel.dataTypeClasses select o.DataIndex).Max();
                                        dataIndex = maxIndex + 1;
                                    }
                                    else//不进行解析 等待一个通道加载完毕
                                    {
                                        ncp.CommandType = NewCommandType.CommandType.None;
                                        return ncp;
                                    }
                                }
                                ncp.SensorChannelID = sensorChannalID;
                                ncp.CommandType = NewCommandType.CommandType.GetDeviceIDAndDataType;

                                while (dataBuffer.Count() >= 4)
                                {
                                    int senserId = BitConverter.ToUInt16(dataBuffer.Take(2).Reverse().ToArray(), 0);
                                    bool isSigned = Convert.ToBoolean(dataBuffer[2] & 0x80);
                                    int dataLength = dataBuffer[2] & 0x7f;
                                    double dtatK = 0;
                                    if (dataBuffer[3] == 0)
                                    {
                                        dtatK = 1;
                                    }
                                    else if (dataBuffer[3] == 1)
                                    {
                                        dtatK = 0.1;
                                    }
                                    else if (dataBuffer[3] == 2)
                                    {
                                        dtatK = 0.01;
                                    }
                                    else if (dataBuffer[3] == 3)
                                    {
                                        dtatK = 0.001;
                                    }
                                    else if (dataBuffer[3] == 4)
                                    {
                                        dtatK = 0.0001;
                                    }
                                    DataTypeClass dtc = new DataTypeClass() { DataIndex = dataIndex, SensorID = senserId, IsSigned = isSigned, K = dtatK, DataLength = dataLength };
                                    ncp.dataTypeClasses.Add(dtc);
                                    dataBuffer = dataBuffer.Skip(4).Take(dataBuffer.Length - 4).ToArray();
                                    dataIndex++;
                                }
                                Protocal.protocalInstance.SensorChannels.Add(ncp);
                            }
                            break;
                        case (byte)NewCommandType.CommandType.StartSendData:
                            if (Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == sensorChannalID))
                            {
                                ncp.CommandType = NewCommandType.CommandType.StartSendData;
                                var sensorChannel = Protocal.protocalInstance.SensorChannels.FirstOrDefault(o => o.SensorChannelID == sensorChannalID);
                                int dataIndex = (from o in sensorChannel.dataTypeClasses select o.DataIndex).Min();//找到当前通道的最小的值
                                //while (dataBuffer.Count() > 0)
                                {
                                    var dt = sensorChannel.dataTypeClasses.FirstOrDefault(o => o.DataIndex == dataIndex);
                                    //var bufferD = dataBuffer.Take(dt.DataLength).ToArray();
                                    long valueZX = 0;
                                    switch (dataBuffer.Length)
                                    {
                                        case 2:
                                            {
                                                Array.Reverse(dataBuffer);
                                                valueZX = BitConverter.ToInt16(dataBuffer, 0);//有无符号 直接转化;
                                            }
                                            break;
                                        case 3:
                                            {
                                                if (dt.IsSigned)//有符号
                                                {

                                                }
                                                else
                                                {
                                                    valueZX = dataBuffer[0] * 256 * 256 + dataBuffer[1] * 256 + dataBuffer[2];
                                                }
                                            }
                                            break;
                                        case 4:
                                            {
                                                Array.Reverse(dataBuffer);
                                                valueZX = BitConverter.ToInt32(dataBuffer, 0);
                                            }
                                            break;
                                    }
                                    //dataBuffer = dataBuffer.Skip(dt.DataLength).Take(dataBuffer.Length - dt.DataLength).ToArray();
                                    dt.Value = (int)valueZX;
                                    dataIndex++;
                                }
                            }
                            break;
                        default:
                            ncp.CommandType = NewCommandType.CommandType.None;
                            break;
                    }
                    var crc = NewCommandType.CRCCalc(buffer.Skip(2).Take(datadLength + 3).ToArray());
                    if (!Enumerable.SequenceEqual(crc, buffer.Skip(datadLength + 5).Take(2).ToArray()))
                    {
                        ncp.IsRightPackage = false;
                    }
                    if (!Enumerable.SequenceEqual(buffer.Skip(datadLength + 7).Take(2).ToArray(), NewCommandType.ETX))
                    {
                        ncp.IsRightPackage = false;
                    }
                }
            }
            catch (Exception exc)
            {

                throw;
            }
            return ncp;
        }
        #endregion
    }

    public class NewCommandChannelPara
    {
        public int SensorChannelID;
        public bool IsNewCommandType = false;
        public bool IsRightPackage = false;
        public int ValueInt;

        public NewCommandType.CommandType CommandType;
        public List<DataTypeClass> dataTypeClasses = new List<DataTypeClass>();
    }

    public class DataTypeClass
    {
        public int DataIndex;
        public int SensorID;
        public bool IsSigned = false;
        public int DataLength = 0;
        public int Value;
        public double K;
    }
}
