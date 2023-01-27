using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCYDataSource
{

    public class NewCommandType
    {
        #region para     

        public static byte[] SYN = new byte[] { 0x5a, 0xa5 };
        public static byte[] ETX = new byte[] { 0x5b, 0xb5 };
        public enum CommandType : byte
        {
            None=0x00,
            GetDeviceInfo = 0x21,
            GetDeviceIDAndDataType=0x36,
            StartSendData = 0x60,
            StopSenddData = 0x61,
            SetFrequency=0x3D
        }

        #endregion

        public static byte[] GetDeviceInfo()//0x21      5A A5 21 00 00 B1 F6 5B B5
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(SYN);
            buffer.Add((byte)CommandType.GetDeviceInfo);
            buffer.Add(0x00);//数据长度2个字节
            buffer.Add(0x00);//数据长度2个字节
            var crc = CRCCalc(buffer.Skip(2).Take (3).ToArray ());
            buffer.AddRange(crc);
            buffer.AddRange(ETX);
            return buffer.ToArray();
        }

        public static byte[] GetDeviceIDAndDataType()//0x36     5A A5 36 00 00 77 05 5B B5
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(SYN);
            buffer.Add((byte)CommandType.GetDeviceIDAndDataType);
            buffer.Add(0x00);//数据长度2个字节
            buffer.Add(0x00);//数据长度2个字节
            var crc = CRCCalc(buffer.Skip(2).Take(3).ToArray());
            buffer.AddRange(crc);
            buffer.AddRange(ETX);
            return buffer.ToArray();
        }

        //5A A5 3D 00 06 01 00 00 00 0A 00 A1 7F 5B B5
        //5A A5 3D 00 06 01 00 00 00 64 00 89 5A 5B B5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectType"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static byte[] SetFrequency(ConnectType connectType,long frequency)
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(SYN);
            buffer.Add((byte)CommandType.SetFrequency);
            buffer.Add(0x00);//数据长度2个字节
            buffer.Add(0x06);//数据长度2个字节
            buffer.Add((byte)connectType);//传输方式
            buffer.Add(Convert.ToByte((frequency >> 24) & 0xff));
            buffer.Add(Convert.ToByte((frequency >> 16) & 0xff));
            buffer.Add(Convert.ToByte((frequency >> 8) & 0xff));
            buffer.Add(Convert.ToByte(frequency& 0xff));
            buffer.Add(0x00);//采集方式
            var crc = CRCCalc(buffer.Skip(2).Take(9).ToArray());
            buffer.AddRange(crc);
            buffer.AddRange(ETX);
            return buffer.ToArray();
        }

        public static byte[] GetDeviceDataStart(ConnectType connectType)//5A A5 60 00 02 01 00 44 8D 5B B5 // 01
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(SYN);
            buffer.Add((byte)CommandType.StartSendData);
            buffer.Add(0x00);//数据长度2个字节
            buffer.Add(0x02);//数据长度2个字节
            buffer.Add((byte)connectType);//传输方式
            buffer.Add(0x00);//采集方式
            var crc = CRCCalc(buffer.Skip(2).Take(5).ToArray());
            buffer.AddRange(crc);
            buffer.AddRange(ETX);
            return buffer.ToArray();
        }

        public static byte[] GetDeviceDataStop(ConnectType connectType)//5A A5 61 00 02 01 00 EE DC 5B B5
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(SYN);
            buffer.Add((byte)CommandType.StopSenddData);
            buffer.Add(0x00);//数据长度2个字节
            buffer.Add(0x02);//数据长度2个字节
            buffer.Add((byte)connectType);//传输方式
            buffer.Add(0x00);//采集方式
            var crc = CRCCalc(buffer.Skip(2).Take(5).ToArray());
            buffer.AddRange(crc);
            buffer.AddRange(ETX);
            return buffer.ToArray();
        }

        public static byte[] CRCCalc(byte[] data)
        {
            int len = data.Length;
            int u16Crc = 0;
            while (len != 0)
            {
                for (byte i = 0x80; i != 0; i /= 2)
                {
                    if ((u16Crc & 0x8000) != 0)
                    {
                        u16Crc *= 2;
                        u16Crc ^= 0x1021;
                    }
                    else
                    {
                        u16Crc *= 2;
                    }
                    if ((data[data.Length - len] & i) != 0)
                    {
                        u16Crc ^= 0x1021;
                    }
                }
                len--;
            }
            byte[] crc = new byte[2];
            crc[1] = (byte)(u16Crc & 0xff);
            crc[0] = (byte)(u16Crc >> 8 & 0xff);
            return crc;
        }

    }

    public enum ConnectType
    {
        USB=0x01,
        BLE=0x00
    }
}
