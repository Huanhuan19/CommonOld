using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WCYDataSource
{
    public class DataSource
    {
        public DataSource(Method_GetSensorType getSensorType)
        {
            _serialPortEngine = new SerialPortEngine(QueryDefine.QueryType());

            _portCollection = new PortCollection();
            _getSensorType = getSensorType;
            _readThread = new Thread(Looping);
            _readThread.Start();
            _serialPortEngine.DataCatch.SourceDataRecieved += new SourceDataHandler(DataCatch_SourceDataRecieved);//serialPortEngine
            //_hid_1.OpenDevice_1(0x1FC9, 0x000B, "DEM000000000");
            //_hid_2.OpenDevice_2(0x1FC9, 0x000B, "DEMO00000000");
            //_hid_3.OpenDevice_3(0x1FC9, 0x000B, "DEMO00000000");


            //_hid_1.DataReceived += new EventHandler<report>(_hid_1_DataReceived);//USB
            //_hid_2.DataReceived += new EventHandler<report>(_hid_2_DataReceived);
            //_hid_3.DataReceived += new EventHandler<report>(_hid_3_DataReceived);


            _serialPortEngine.ConnectChanged += new ConnectedHandler(_serialPortEngine_ConnectChanged);

            runningTime = 0;
        }

        public DataSource(object getSensorType)
        {
            this.getSensorType = getSensorType;
        }

        Hid _hid_1;
        Hid _hid_2;
        Hid _hid_3;
        Hid _hid_4;
        Hid _hid_5;
        Hid _hid_6;
        Hid _hid_7;
        Hid _hid_8;

        bool IsStartBLE = false;//定义蓝牙功能 搜索传感器
        public void BLEStart()//2021蓝牙和USB 分开初始化  再from中调用 不是win10的电脑也可以用
        {
            // BLE Init
            BLEDispose();
            InitBLE();
            IsStartBLE = true;
        }
        public void CMDHidStart()//2021蓝牙和USB 分开初始化  再from中调用 不是win10的电脑也可以用
        {
            // HID Init
            HidStart();
        }
        public void HidStart()
        {
            // BLE Init
            //BLEDispose();
            //////InitBLE();
            sensorBuffer = new List<SenserMsg>();
            bluetoothList = new List<BluetoothInfo>();


            _hid_1 = new Hid();
            _hid_2 = new Hid();
            _hid_3 = new Hid();
            _hid_4 = new Hid();
            _hid_5 = new Hid();
            _hid_6 = new Hid();
            _hid_7 = new Hid();
            _hid_8 = new Hid();


            _hid_1.OpenDevice_1(0x1FC9, 0x000B, "DEM000000000");
            _hid_2.OpenDevice_2(0x1FC9, 0x000B, "DEMO00000000");
            _hid_3.OpenDevice_3(0x1FC9, 0x000B, "DEMO00000000");
            _hid_4.OpenDevice_4(0x1FC9, 0x000B, "DEMO00000000");
            _hid_5.OpenDevice_5(0x1FC9, 0x000B, "DEM000000000");
            _hid_6.OpenDevice_6(0x1FC9, 0x000B, "DEMO00000000");
            _hid_7.OpenDevice_7(0x1FC9, 0x000B, "DEMO00000000");
            _hid_8.OpenDevice_8(0x1FC9, 0x000B, "DEMO00000000");



            _hid_1.DataReceived += new EventHandler<report>(_hid_1_DataReceived);//USB
            _hid_2.DataReceived += new EventHandler<report>(_hid_2_DataReceived);
            _hid_3.DataReceived += new EventHandler<report>(_hid_3_DataReceived);
            _hid_4.DataReceived += new EventHandler<report>(_hid_4_DataReceived);
            _hid_5.DataReceived += new EventHandler<report>(_hid_5_DataReceived);//USB
            _hid_6.DataReceived += new EventHandler<report>(_hid_6_DataReceived);
            _hid_7.DataReceived += new EventHandler<report>(_hid_7_DataReceived);
            _hid_8.DataReceived += new EventHandler<report>(_hid_8_DataReceived);

            if (Frequence == 0)
            {
                Frequence = 1;
                _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
            }
            else
            {
                _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                _skipPoint_wuxian = (int)_ReceiveFrequence_1 / Frequence;
            }
            //_skipPoint_2 = (int)_ReceiveFrequence_2 / Frequence;
            //_skipPoint_3 = (int)_ReceiveFrequence_3 / Frequence;
            //_skipPoint_4 = (int)_ReceiveFrequence_4 / Frequence;

            Frequence = 10;//默认是10

        }
        public void QueryType()
        {
            if (_hid_1.deviceOpened && Started == false)//新协议下发的  读设备
            {
                if (!Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == 1))
                {
                    _hid_1.SendCommand(NewCommandType.GetDeviceDataStop(ConnectType.USB));//先发个停止
                    _hid_1.SendCommand(NewCommandType.GetDeviceIDAndDataType());
                }
            }
            if (_hid_2.deviceOpened && Started == false)//新协议下发的  读设备
            {
                if (!Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == 2))
                {
                    _hid_2.SendCommand(NewCommandType.GetDeviceDataStop(ConnectType.USB));//先发个停止
                    _hid_2.SendCommand(NewCommandType.GetDeviceIDAndDataType());
                }
            }
            if (ble1 != null && ble1.BLEIsConnected == true && Started == false)//新协议下发的  读设备
            {
                ble1.SendCommand(NewCommandType.GetDeviceInfo());
            }
            if (Hid.IsCloseObject)
            {
                Hid.IsCloseObject = false;
                HidStart();

            }
            if ((DateTime.Now - _lastRefresh1 >= LOST_CONNECT_SPAN) && IsUSBCOM)
            {
                byte[] QueryBuffer = new byte[10];
                //UnpackQueryTypeUSB(QueryBuffer);  //新协议屏蔽
                //GuangDianMen1FirstValue = true;
            }
            if (DateTime.Now - _lastRefresh2 >= LOST_CONNECT_SPAN && IsUSBCOM)
            {
                _noExist2 = true;
            }
            if (DateTime.Now - _lastRefresh3 >= LOST_CONNECT_SPAN && IsUSBCOM)
            {
                _noExist3 = true;
            }
            if (DateTime.Now - _lastRefresh4 >= LOST_CONNECT_SPAN && IsUSBCOM)
            {
                _noExist4 = true;
            }
            if (DateTime.Now - _lastRefresh5 >= LOST_CONNECT_SPAN && IsUSBCOM)
            {
                _noExist5 = true;
            }
            if (DateTime.Now - _lastRefresh6 >= LOST_CONNECT_SPAN && IsUSBCOM)
            {
                _noExist6 = true;
            }
            if (DateTime.Now - _lastRefresh7 >= LOST_CONNECT_SPAN && IsUSBCOM)
            {
                _noExist7 = true;
            }
            if (DateTime.Now - _lastRefresh8 >= LOST_CONNECT_SPAN && IsUSBCOM)
            {
                _noExist8 = true;
            }
            if (Hid.IsPortChange())
            {
                //Thread.Sleep(1000);
                //HidStart();
            }

        }

        void _serialPortEngine_ConnectChanged(object sender, ConnectEventArgs e)
        {


            OnConnectedChanged(e);
        }
        byte[,] chuanganqi = new byte[5, 3];
        byte[] buffer1;
        Int64 USB_1_index = 0;
        int Count_1 = 0;
        int Count_Pre_1 = 0;
        float UpSideValue45, UpSideValue2_45, LastValue = 0, LastValue2 = 0;
        bool Photo = false;

        int IsUsb_SetOnce = 0;
        //List<byte> WLQueryBuffer2 = new List<byte>();以后可以加上到hid2 目前无线传感器和别的传感器只支持2个
        int skip = 0;
        double valueShengbo = 0;
        List<float> valueList = new List<float>();
        List<float> valueList2 = new List<float>();
        bool GuangDianMen1FirstValue = true;
        DateTime GDMFirstTime;
        bool GuangDianMen2FirstValue = true;
        DateTime GDMSecondTime;

        double SkipCount1 = 6.2;//大概60多hz 数据包
        double ReceiveCount1 = 0;
        double SkipCount2 = 6.2;//大概60多hz 数据包
        double ReceiveCount2 = 0;

        bool IsStartCreatClass = true;
        CalculateSkipCount hid1_SkipCount;
        void _hid_1_DataReceived(object sender, report e)
        {


            #region 新协议
            var NewPackage = ParseBuffer.Parse(1, e.reportBuff);
            if (NewPackage.IsNewCommandType)
            {
                IsUSBCOM = true;
                _lastRefresh1 = DateTime.Now;

                if (NewPackage.IsRightPackage)
                {
                    switch (NewPackage.CommandType)
                    {
                        case NewCommandType.CommandType.GetDeviceInfo:
                            byte[] QueryBuffer = new byte[11];//查询传感器
                            //Sensor1 = NewPackage.sensorID;
                            //QueryBuffer[1] = NewPackage.sensorID;

                            if ((_noExist2 && !QueryUsbIsOk) || Sensor2 == 100)
                            {
                                UnpackQueryTypeUSB(QueryBuffer);
                            }
                            break;
                        case NewCommandType.CommandType.GetDeviceIDAndDataType:
                            if (_noExist2)
                            {
                                if (Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == 1))
                                {
                                    var sensorChannel = Protocal.protocalInstance.SensorChannels.FirstOrDefault(o => o.SensorChannelID == 1);
                                    var sensorIDs = (from o in sensorChannel.dataTypeClasses select o.SensorID).ToArray();
                                    UnpackQueryTypeUSBForNewInt(sensorIDs);
                                }
                            }
                            break;
                        case NewCommandType.CommandType.StartSendData:
                            if (Started)
                            {
                                if (Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == 1))
                                {
                                    var sensorChannel = Protocal.protocalInstance.SensorChannels.FirstOrDefault(o => o.SensorChannelID == 1);
                                    foreach (var item in sensorChannel.dataTypeClasses)
                                    {
                                        ValueEventArgs ee = new ValueEventArgs(item.Value, item.DataIndex);
                                        OnValueChanged(ee);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                return;
            }
            #endregion

            else
            {
                Hid1ParseBuffer(e.reportBuff);
            }

        }
        float GetGMUseGDM()
        {
            float value = 0;
            for (int i = 0; i < 100; i++)
            {
                if (value == 0)
                {
                    TimeSpan ts = DateTime.Now - GDMFirstTime;
                    value = (float)Math.Round(ts.TotalSeconds, 3);
                    System.Threading.Thread.Sleep(1);
                }
                else
                {
                    return value;
                }
            }
            return value;
        }
        void Hid1ParseBuffer(byte[] reportBuff)
        {
            IsUSBCOM = true;
            byte[] QueryBuffer = new byte[11];
            byte[] QueryBuffer_JiaSuDu = new byte[13];
            byte[] JaSuDu = new byte[11];
            byte[] QueryBuffer_CiGanYing_2 = new byte[12];
            byte[] ReceiveTwoDatas = new byte[8];
            byte[] ReceiveFourDatas = new byte[14];
            byte[] QueryBuffer_Guang_4 = new byte[14];
            byte[] voice = new byte[64];
            byte[] WireLess = new byte[50];

            List<byte> WLQueryBuffer = new List<byte>();



            #region 长度50字节传感器
            if (reportBuff.Length == 50)
            {
                buffer1 = reportBuff;
                QueryBuffer[0] = buffer1[0];
                QueryBuffer[1] = buffer1[1];
                for (int i = 2; i < 8; i++)
                {
                    QueryBuffer[i] = 0;
                }
                byte x, y;
                bool youwu = false;
                Sensor1 = buffer1[1];
                _lastRefresh1 = DateTime.Now;
                #region 光电门 7
                if (Sensor1 == 7)//光电门；
                {
                    int index = 0;
                    if ((buffer1.Length == 50) && (buffer1[0] == 0xa5))
                    {
                        if (_portCollection.PlugInPortIndexes.Contains(index))
                        {
                            if (Started)
                            {
                                #region old Rule
                                //for (int i = 44; i <= buffer1.Length - 5; i += 5)
                                //{
                                //    byte side = buffer1[i];//保留光电门 5位小数点

                                //    float value = UsbByteToGate(new byte[] { buffer1[i + 2], buffer1[i + 3], buffer1[i + 4], buffer1[i + 5] },_portCollection.Ports[index].K, _portCollection.Ports[index].B);



                                //if (side == 0x01 && UpSideValue45 != buffer1[45])
                                //{
                                //    //if (Photo == false)
                                //    //{
                                //    //    Photo = true;
                                //    //    continue;
                                //    //}                                        
                                //    _portCollection.Ports[index].DownSideValue = value;
                                //    value = value - LastValue;
                                //    UpSideValue45 = buffer1[45];
                                //    if (value >= 0)
                                //    {
                                //        ValueEventArgs eee = new ValueEventArgs(value, index);
                                //        OnValueChanged(eee);
                                //    }
                                //}
                                //else
                                //{
                                //    LastValue = value;
                                //    UpSideValue45 = buffer1[45];
                                //}


                                //}
                                #endregion

                                #region New 20190414

                                List<float> valueListTemp = new List<float>();
                                for (int i = 6; i <= buffer1.Length - 4; i += 8)
                                {
                                    byte side = buffer1[i];//保留光电门 5位小数点
                                    float value = UsbByteToGate(new byte[] { buffer1[i], buffer1[i + 1], buffer1[i + 2], buffer1[i + 3] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);

                                    if (value != 0)
                                    {
                                        valueListTemp.Add(value);
                                    }
                                }
                                for (int indx = 0; indx < valueListTemp.Count; indx++)
                                {
                                    if (valueList.Contains(valueListTemp[indx]))//contains the value
                                    {
                                        int a = valueList.IndexOf(valueListTemp[indx]);
                                        if (valueList.IndexOf(valueListTemp[indx]) >= indx)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            if (GuangDianMen1FirstValue)
                                            {
                                                //GDMFirstTime = DateTime.Now;
                                                GuangDianMen1FirstValue = false;
                                            }
                                            else
                                            {
                                                _portCollection.Ports[index].DownSideValue = GetGMUseGDM();
                                            }
                                            ValueEventArgs eee = new ValueEventArgs(valueListTemp[indx], index);
                                            OnValueChanged(eee);

                                        }
                                    }
                                    else // do not contains 
                                    {
                                        if (GuangDianMen1FirstValue)
                                        {
                                            //GDMFirstTime = DateTime.Now;
                                            GuangDianMen1FirstValue = false;
                                        }
                                        else
                                        {
                                            _portCollection.Ports[index].DownSideValue = GetGMUseGDM();
                                        }
                                        ValueEventArgs eee = new ValueEventArgs(valueListTemp[indx], index);
                                        OnValueChanged(eee);
                                    }
                                }
                                valueList = valueListTemp;
                                #endregion
                            }
                        }
                    }
                }
                #endregion
                #region 无线接收
                else if (Sensor1 == 100)
                {
                    WireLess = reportBuff;
                    _lastRefresh1 = DateTime.Now;
                    if (WireLess[2] != 0x00 && WireLess[2] != 0xff)//无线接收1
                    {
                        WLQueryBuffer.Clear();
                        WLQueryBuffer.Add(0xa5);
                        //WLQueryBuffer.Add(8);
                        for (int i = 2; i <= 11; i++)
                        {
                            WLQueryBuffer.Add(WireLess[i]);
                            Sensor1 = WireLess[2];
                        }//查询传感器 1 ID

                        if (WireLess[8] != 0x00 && WireLess[8] != 0xff)//无线接收2
                        {
                            WLQueryBuffer.Clear();
                            WLQueryBuffer.Add(0xa5);
                            WLQueryBuffer.Add(WireLess[2]);
                            for (int i = 8; i <= 17; i++)
                            {
                                WLQueryBuffer.Add(WireLess[i]);
                            }//查询传感器 2 ID
                            if (WireLess[14] != 0x00 && WireLess[14] != 0xff)//无线接收3
                            {
                                WLQueryBuffer.Clear();
                                WLQueryBuffer.Add(0xa5);
                                WLQueryBuffer.Add(WireLess[2]);
                                WLQueryBuffer.Add(WireLess[8]);
                                for (int i = 14; i <= 23; i++)
                                {
                                    WLQueryBuffer.Add(WireLess[i]);
                                }//查询传感器 3 ID
                                if (WireLess[20] != 0x00 && WireLess[20] != 0xff)//无线接收4
                                {
                                    WLQueryBuffer.Clear();
                                    WLQueryBuffer.Add(0xa5);
                                    WLQueryBuffer.Add(WireLess[2]);
                                    WLQueryBuffer.Add(WireLess[8]);
                                    WLQueryBuffer.Add(WireLess[14]);
                                    for (int i = 20; i <= 29; i++)
                                    {
                                        WLQueryBuffer.Add(WireLess[i]);
                                    }//查询传感器 4 ID
                                    if (WireLess[26] != 0x00 && WireLess[26] != 0xff)//无线接收5
                                    {
                                        WLQueryBuffer.Clear();
                                        WLQueryBuffer.Add(0xa5);
                                        WLQueryBuffer.Add(WireLess[2]);
                                        WLQueryBuffer.Add(WireLess[8]);
                                        WLQueryBuffer.Add(WireLess[14]);
                                        WLQueryBuffer.Add(WireLess[20]);
                                        for (int i = 26; i <= 35; i++)
                                        {
                                            WLQueryBuffer.Add(WireLess[i]);
                                        }//查询传感器 5 ID
                                        if (WireLess[32] != 0x00 && WireLess[32] != 0xff)//无线接收6
                                        {
                                            WLQueryBuffer.Clear();
                                            WLQueryBuffer.Add(0xa5);
                                            WLQueryBuffer.Add(WireLess[2]);
                                            WLQueryBuffer.Add(WireLess[8]);
                                            WLQueryBuffer.Add(WireLess[14]);
                                            WLQueryBuffer.Add(WireLess[20]);
                                            WLQueryBuffer.Add(WireLess[26]);
                                            for (int i = 32; i <= 41; i++)
                                            {
                                                WLQueryBuffer.Add(WireLess[i]);
                                            }//查询传感器 6 ID
                                            if (WireLess[38] != 0x00 && WireLess[38] != 0xff)//无线接收7
                                            {
                                                WLQueryBuffer.Clear();
                                                WLQueryBuffer.Add(0xa5);
                                                WLQueryBuffer.Add(WireLess[2]);
                                                WLQueryBuffer.Add(WireLess[8]);
                                                WLQueryBuffer.Add(WireLess[14]);
                                                WLQueryBuffer.Add(WireLess[20]);
                                                WLQueryBuffer.Add(WireLess[26]);
                                                WLQueryBuffer.Add(WireLess[32]);
                                                for (int i = 38; i <= 47; i++)
                                                {
                                                    WLQueryBuffer.Add(WireLess[i]);
                                                }//查询传感器 7 ID
                                                if (WireLess[44] != 0x00 && WireLess[44] != 0xff)//无线接收7
                                                {
                                                    WLQueryBuffer.Clear();
                                                    WLQueryBuffer.Add(0xa5);
                                                    WLQueryBuffer.Add(WireLess[2]);
                                                    WLQueryBuffer.Add(WireLess[8]);
                                                    WLQueryBuffer.Add(WireLess[14]);
                                                    WLQueryBuffer.Add(WireLess[20]);
                                                    WLQueryBuffer.Add(WireLess[26]);
                                                    WLQueryBuffer.Add(WireLess[32]);
                                                    WLQueryBuffer.Add(WireLess[38]);
                                                    for (int i = 44; i <= 49; i++)
                                                    {
                                                        WLQueryBuffer.Add(WireLess[i]);
                                                    }//查询传感器 8 ID
                                                    for (int j = 50; j <= 53; j++)
                                                    {
                                                        WLQueryBuffer.Add(0x00);
                                                    }//查询传感器 7 ID
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (Started)
                        {


                            if (--_skipPoint_1 == 0)
                            {
                                _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                                if (WLQueryBuffer.Count >= 11)
                                {
                                    int index = 0;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[5], WireLess[6], WireLess[7] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                if (WLQueryBuffer.Count >= 12)
                                {
                                    int index = 1;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[11], WireLess[12], WireLess[13] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                if (WLQueryBuffer.Count >= 13)
                                {
                                    int index = 2;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[17], WireLess[18], WireLess[19] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                if (WLQueryBuffer.Count >= 14)
                                {
                                    int index = 3;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[23], WireLess[24], WireLess[25] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                //添加一共8个接收
                                if (WLQueryBuffer.Count >= 15)
                                {
                                    int index = 4;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[29], WireLess[30], WireLess[31] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                if (WLQueryBuffer.Count >= 16)
                                {
                                    int index = 5;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[35], WireLess[36], WireLess[37] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                if (WLQueryBuffer.Count >= 17)
                                {
                                    int index = 6;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[41], WireLess[42], WireLess[43] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                if (WLQueryBuffer.Count >= 18)
                                {
                                    int index = 7;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[47], WireLess[48], WireLess[49] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                            }


                        }
                        if (Adjusted)
                        {
                            if (--_skipPoint_1 == 0)
                            {
                                _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                                if (WLQueryBuffer.Count >= 8)
                                {
                                    int index = 0;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[5], WireLess[6], WireLess[7] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                if (WLQueryBuffer.Count >= 9)
                                {
                                    int index = 1;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[11], WireLess[12], WireLess[13] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                if (WLQueryBuffer.Count >= 10)
                                {
                                    int index = 2;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[17], WireLess[18], WireLess[19] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                                if (WLQueryBuffer.Count >= 11)
                                {
                                    int index = 3;
                                    float value = ByteToFloatUSB(new byte[] { WireLess[23], WireLess[24], WireLess[25] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                            }
                        }
                    }
                    if ((_noExist2 && !QueryUsbIsOk) || Sensor2 == 100)
                    {
                        UnpackQueryTypeUSB(WLQueryBuffer.ToArray());
                    }
                    return;
                }
                #endregion
                #region 温湿度一体
                else if (Sensor1 == 62)//温湿度一体
                {
                    _lastRefresh1 = DateTime.Now;
                    byte[] QueryBuffer_WenDu = new byte[12];
                    QueryBuffer_WenDu[0] = 0xa5;
                    QueryBuffer_WenDu[1] = 62;
                    QueryBuffer_WenDu[2] = 63;

                    if (Started)
                    {
                        //if (Frequence <= 10)
                        //{
                        //    _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                        //}
                        //else
                        _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                        Count_1 = (int)(USB_1_index * 16 / _skipPoint_1 - Count_Pre_1);
                        if (Count_1 != 0)
                        {
                            int index = 0, index_2 = 1;
                            for (int i = 0; i < Count_1; i++)
                            {
                                float value = buffer1[3];
                                float value_2 = buffer1[4];
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                                ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_2);
                                OnValueChanged(ee_2);
                            }
                        }
                        USB_1_index++;
                        Count_Pre_1 = Count_1 + Count_Pre_1;
                    }

                    if (Adjusted)
                    {
                        int index = 0;
                        if (--_skipPoint_1 == 0)
                        {
                            _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                            float value = ByteToFloatUSB(new byte[] { buffer1[2], buffer1[3], buffer1[4] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                    }
                    UnpackQueryTypeUSB(QueryBuffer_WenDu);
                    return;
                }
                #endregion
                #region  普通传感器
                else
                {
                    if ((buffer1.Length == 50) && (buffer1[0] == 0xa5))//普通传感器
                    {
                        if (Started)
                        {
                            if (Frequence <= 10)
                            {
                                _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                            }
                            else
                                _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                            Count_1 = (int)(USB_1_index * 16 / _skipPoint_1 - Count_Pre_1);
                            if (Count_1 != 0)
                            {
                                int index = 0;
                                for (int i = 0; i < Count_1; i++)
                                {
                                    float value = ByteToFloatUSB(new byte[] { buffer1[3 * (i + 1) - 1], buffer1[3 * (i + 1)], buffer1[3 * (i + 1) + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                            }
                            USB_1_index++;
                            Count_Pre_1 = Count_1 + Count_Pre_1;
                        }

                        if (Adjusted)
                        {
                            int index = 0;
                            if (--_skipPoint_1 == 0)
                            {
                                _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                                float value = ByteToFloatUSB(new byte[] { buffer1[2], buffer1[3], buffer1[4] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                            }
                        }

                    }
                }
                #endregion
                if ((_noExist2 && !QueryUsbIsOk) || Sensor2 == 100)
                {
                    UnpackQueryTypeUSB(QueryBuffer);
                }


            }
            #endregion
            //
            #region 声波 64字节
            else if (reportBuff.Length == 64)//声波
            {
                voice = reportBuff;
                _lastRefresh1 = DateTime.Now;
                if (voice[0] == 0xa0)
                {
                    if (Started)
                    {
                        int index = 0;
                        for (int i = 1; i <= 31; i++)
                        {
                            float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                        //for (int i = 2; i <= 63; i++)
                        //{
                        //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                        //    OnValueChanged(ee);
                        //}
                    }
                    QueryBuffer[0] = 0xa5;
                    QueryBuffer[1] = 8;
                    for (int i = 2; i < 8; i++)
                    {
                        QueryBuffer[i] = 0;
                    }
                    UnpackQueryTypeUSB(QueryBuffer);
                }

            }
            #endregion
            #region 声波 22字节
            else if (reportBuff.Length == 22)//声波
            {
                voice = reportBuff;
                _lastRefresh1 = DateTime.Now;
                if (voice[0] == 0xa0)
                {
                    if (Started)
                    {
                        int index = 0;
                        for (int i = 1; i <= 10; i++)
                        {
                            float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                        //for (int i = 2; i <= 63; i++)
                        //{
                        //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                        //    OnValueChanged(ee);
                        //}
                    }
                    QueryBuffer[0] = 0xa5;
                    QueryBuffer[1] = 8;
                    Sensor1 = 8;//声波编号
                    for (int i = 2; i < 8; i++)
                    {
                        QueryBuffer[i] = 0;
                    }
                    //UnpackQueryTypeUSB(QueryBuffer);
                    if ((_noExist2 && !QueryUsbIsOk) || Sensor2 == 100)
                    {
                        UnpackQueryTypeUSB(QueryBuffer);
                    }
                }

            }
            #endregion
            #region 声波声强一体 23字节
            else if (reportBuff.Length == 23)//声波声强一体  20160716
            {
                voice = reportBuff;
                _lastRefresh1 = DateTime.Now;
                if (voice[0] == 0xa0)
                {
                    if (Started)
                    {
                        int index = 0;
                        for (int i = 1; i <= 10; i++)
                        {
                            float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                        //for (int i = 2; i <= 63; i++)
                        //{
                        //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                        //    OnValueChanged(ee);
                        //}
                        //if (skip == (int)(1000/Frequence))
                        if (skip >= 1000)
                        {
                            if (voice[22] > 30 && voice[22] < 130)
                            {
                                ValueEventArgs ee1 = new ValueEventArgs(voice[22], 1);
                                OnValueChanged(ee1);
                                skip = 0;
                            }

                        }
                        else
                        {
                            skip++;
                        }
                    }
                    QueryBuffer_CiGanYing_2[0] = 0xa5;
                    QueryBuffer_CiGanYing_2[1] = voice[1];
                    QueryBuffer_CiGanYing_2[2] = 20;
                    for (int i = 3; i < 9; i++)
                    {
                        QueryBuffer_CiGanYing_2[i] = 0;
                    }
                    UnpackQueryTypeUSB(QueryBuffer_CiGanYing_2);
                }

            }
            #endregion
            #region 分成3个传感器  11字节
            else if (reportBuff.Length == 11)
            {
                JaSuDu = reportBuff;
                _lastRefresh1 = DateTime.Now;
                if (JaSuDu[0] == 0xa5)
                {
                    QueryBuffer_JiaSuDu[0] = JaSuDu[0];
                    switch (JaSuDu[1])
                    {
                        case 50://加速度
                            {
                                QueryBuffer_JiaSuDu[1] = 50;
                                QueryBuffer_JiaSuDu[2] = 51;
                                QueryBuffer_JiaSuDu[3] = 52;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1, index_2 = 2;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { JaSuDu[2], JaSuDu[3], JaSuDu[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { JaSuDu[5], JaSuDu[6], JaSuDu[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);
                                        float value_3 = ByteToFloatUSB(new byte[] { JaSuDu[8], JaSuDu[9], JaSuDu[10] }, _portCollection.Ports[index_2].K, _portCollection.Ports[index_2].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);
                                        ValueEventArgs ee_3 = new ValueEventArgs(value_3, index_2);
                                        OnValueChanged(ee_3);
                                    }
                                }
                            }
                            break;


                        default:
                            break;
                    }

                }
                UnpackQueryTypeUSB(QueryBuffer_JiaSuDu);
            }
            #endregion
            #region 分成2个传感器  8字节
            else if (reportBuff.Length == 8)
            {
                ReceiveTwoDatas = reportBuff;
                _lastRefresh1 = DateTime.Now;
                if (ReceiveTwoDatas[0] == 0xa5)
                {
                    QueryBuffer_CiGanYing_2[0] = ReceiveTwoDatas[0];
                    switch (ReceiveTwoDatas[1])
                    {
                        case 97://
                            {
                                QueryBuffer_CiGanYing_2[1] = 97;
                                QueryBuffer_CiGanYing_2[2] = 98;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);

                                    }
                                }
                            }
                            break;
                        case 95://
                            {
                                QueryBuffer_CiGanYing_2[1] = 95;
                                QueryBuffer_CiGanYing_2[2] = 96;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);

                                    }
                                }
                            }
                            break;

                        case 93://
                            {
                                QueryBuffer_CiGanYing_2[1] = 93;
                                QueryBuffer_CiGanYing_2[2] = 94;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);

                                    }
                                }
                            }
                            break;
                        case 101://大气压强温度
                            {
                                QueryBuffer_CiGanYing_2[1] = 101;
                                QueryBuffer_CiGanYing_2[2] = 102;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);

                                    }
                                }
                            }
                            break;
                        case 103://距离 时间
                            {
                                QueryBuffer_CiGanYing_2[1] = 103;
                                QueryBuffer_CiGanYing_2[2] = 104;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);

                                    }
                                }
                            }
                            break;
                        case 105://力 倾角
                            {
                                QueryBuffer_CiGanYing_2[1] = 105;
                                QueryBuffer_CiGanYing_2[2] = 106;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);

                                    }
                                }
                            }
                            break;
                        case 111://挡光时间，飞行时间
                            {
                                QueryBuffer_CiGanYing_2[1] = 111;
                                QueryBuffer_CiGanYing_2[2] = 112;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);

                                    }
                                }
                            }
                            break;
                        case 160://声音
                            {
                                QueryBuffer_CiGanYing_2[1] = 160;
                                QueryBuffer_CiGanYing_2[2] = 161;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);

                                    }
                                }
                            }
                            break;

                        default:
                            break;
                    }

                }
                UnpackQueryTypeUSB(QueryBuffer_CiGanYing_2);
            }
            #endregion
            #region 分成 4个传感器 14字节
            else if (reportBuff.Length == 14)
            {
                ReceiveFourDatas = reportBuff;
                _lastRefresh1 = DateTime.Now;
                if (ReceiveFourDatas[0] == 0xa5)
                {
                    QueryBuffer_Guang_4[0] = ReceiveFourDatas[0];
                    switch (ReceiveFourDatas[1])
                    {
                        case 180://
                            {
                                QueryBuffer_Guang_4[1] = 180;
                                QueryBuffer_Guang_4[2] = 181;
                                QueryBuffer_Guang_4[3] = 182;
                                QueryBuffer_Guang_4[4] = 183;
                                if (Started)
                                {
                                    int index_0 = 0, index_1 = 1;
                                    int index_2 = 2, index_3 = 3;
                                    if (--_skipPoint_JiaSuDu == 0)
                                    {
                                        _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                        float value_1 = ByteToFloatUSB(new byte[] { ReceiveFourDatas[2], ReceiveFourDatas[3], ReceiveFourDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                        float value_2 = ByteToFloatUSB(new byte[] { ReceiveFourDatas[5], ReceiveFourDatas[6], ReceiveFourDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);
                                        float value_3 = ByteToFloatUSB(new byte[] { ReceiveFourDatas[8], ReceiveFourDatas[9], ReceiveFourDatas[10] }, _portCollection.Ports[index_2].K, _portCollection.Ports[index_2].B);
                                        float value_4 = ByteToFloatUSB(new byte[] { ReceiveFourDatas[11], ReceiveFourDatas[12], ReceiveFourDatas[13] }, _portCollection.Ports[index_3].K, _portCollection.Ports[index_3].B);

                                        ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                        OnValueChanged(ee);
                                        ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                        OnValueChanged(ee_2);
                                        ValueEventArgs ee_3 = new ValueEventArgs(value_3, index_2);
                                        OnValueChanged(ee_3);
                                        ValueEventArgs ee_4 = new ValueEventArgs(value_4, index_3);
                                        OnValueChanged(ee_4);

                                    }
                                }
                            }
                            break;
                    }
                }
                UnpackQueryTypeUSB(QueryBuffer_Guang_4);
            }
            #endregion
            if (IsUsb_SetOnce < 100)
            {
                IsUsb_SetOnce++;
            }
        }

        Int64 USB_2_index = 0;
        int Count_2 = 0;
        int Count_Pre_2 = 0;
        byte[] QueryBuffer2 = new byte[13];//无线在2时
        void _hid_2_DataReceived(object sender, report e)
        {

            #region 新协议
            var NewPackage = ParseBuffer.Parse(2, e.reportBuff);
            if (NewPackage.IsNewCommandType)
            {
                IsUSBCOM = true;
                _lastRefresh2 = DateTime.Now;

                _noExist2 = false;

                if (NewPackage.IsRightPackage)
                {
                    switch (NewPackage.CommandType)
                    {
                        case NewCommandType.CommandType.GetDeviceInfo:
                            byte[] QueryBuffer = new byte[12];//查询传感器

                            QueryBuffer[1] = Sensor1;
                            //QueryBuffer[2] = NewPackage.sensorID;

                            if ((_noExist3 && !QueryUsbIsOk) || Sensor2 == 100)
                            {
                                UnpackQueryTypeUSB(QueryBuffer);
                            }
                            break;
                        case NewCommandType.CommandType.GetDeviceIDAndDataType:
                            if (Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == 2))
                            {
                                var sensorChannel = Protocal.protocalInstance.SensorChannels.FirstOrDefault(o => o.SensorChannelID == 1);
                                var sensorChannel2 = Protocal.protocalInstance.SensorChannels.FirstOrDefault(o => o.SensorChannelID == 2);
                                List<int> sensorIDs = (from o in sensorChannel.dataTypeClasses select o.SensorID).ToList();
                                int[] sensorIDs2 = (from o in sensorChannel2.dataTypeClasses select o.SensorID).ToArray();
                                sensorIDs.AddRange(sensorIDs2);
                                UnpackQueryTypeUSBForNewInt(sensorIDs.ToArray ());
                            }
                            break;
                        case NewCommandType.CommandType.StartSendData:
                            if (Started)
                            {
                                if (Protocal.protocalInstance.SensorChannels.Exists(o => o.SensorChannelID == 2))
                                {
                                    var sensorChannel = Protocal.protocalInstance.SensorChannels.FirstOrDefault(o => o.SensorChannelID == 2);
                                    foreach (var item in sensorChannel.dataTypeClasses)
                                    {
                                        ValueEventArgs ee = new ValueEventArgs(item.Value, item.DataIndex);
                                        OnValueChanged(ee);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                return;
            }
            #endregion

            else
            {
                IsUSBCOM = true;
                _noExist2 = false;
                byte[] QueryBuffer = new byte[12];
                byte[] buffer = e.reportBuff;
                byte[] voice = new byte[64];

                Sensor2 = buffer[1];

                if (Sensor1 == 0x00)
                    return;
                if (Sensor1 == 100)
                {
                    QueryBuffer = new byte[11];
                    QueryBuffer[1] = buffer[1];
                }
                else
                {
                    QueryBuffer[1] = Sensor1;
                    QueryBuffer[2] = buffer[1];
                }
                QueryBuffer[0] = 0xa5;
                _lastRefresh2 = DateTime.Now;
                for (int i = 3; i < 8; i++)
                {
                    QueryBuffer[i] = 0;
                }
                #region 50字节长的通用 的传感器


                if (buffer.Length == 50 && buffer[0] == 0xa5)
                {
                    #region 光电门 7
                    if (Sensor2 == 7)//光电门；
                    {
                        int index = 1;
                        if (Sensor1 == 100)
                            index = 0;
                        if ((buffer.Length == 50) && (buffer[0] == 0xa5))
                        {
                            if (_portCollection.PlugInPortIndexes.Contains(index))
                            {
                                if (Started)
                                {
                                    //for (int i = 44; i <= buffer.Length - 5; i += 5)
                                    //{
                                    //    byte side = buffer[i];//保留光电门 5位小数点

                                    //    float value = UsbByteToGate(new byte[] { buffer[i + 2], buffer[i + 3], buffer[i + 4], buffer[i + 5] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);

                                    //    if (side == 0x01 && UpSideValue2_45 != buffer[45])
                                    //    {
                                    //        //if (Photo == false)
                                    //        //{
                                    //        //    Photo = true;
                                    //        //    continue;
                                    //        //}                                        
                                    //        _portCollection.Ports[index].DownSideValue = value;
                                    //        value = value - LastValue2;
                                    //        UpSideValue2_45 = buffer[45];
                                    //        if (value >= 0)
                                    //        {
                                    //            ValueEventArgs eee = new ValueEventArgs(value, index);
                                    //            OnValueChanged(eee);
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        LastValue2 = value;
                                    //        UpSideValue2_45 = buffer[45];
                                    //    }
                                    //}
                                    #region New 20190414

                                    List<float> valueListTemp = new List<float>();
                                    for (int i = 6; i <= buffer.Length - 4; i += 8)
                                    {
                                        byte side = buffer1[i];//保留光电门 5位小数点
                                        float value = UsbByteToGate(new byte[] { buffer[i], buffer[i + 1], buffer[i + 2], buffer[i + 3] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);

                                        if (value != 0)
                                        {
                                            valueListTemp.Add(value);
                                        }
                                    }
                                    for (int indx = 0; indx < valueListTemp.Count; indx++)
                                    {
                                        if (valueList2.Contains(valueListTemp[indx]))//contains the value
                                        {
                                            int a = valueList2.IndexOf(valueListTemp[indx]);
                                            if (valueList2.IndexOf(valueListTemp[indx]) >= indx)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                if (GuangDianMen2FirstValue)
                                                {
                                                    // GDMSecondTime = DateTime.Now;
                                                    GuangDianMen2FirstValue = false;
                                                }
                                                else
                                                {
                                                    _portCollection.Ports[index].DownSideValue = GetGMUseGDM();
                                                }
                                                ValueEventArgs eee = new ValueEventArgs(valueListTemp[indx], index);
                                                OnValueChanged(eee);
                                            }
                                        }
                                        else // do not contains 
                                        {
                                            if (GuangDianMen2FirstValue)
                                            {
                                                //GDMSecondTime = DateTime.Now;
                                                GuangDianMen2FirstValue = false;
                                            }
                                            else
                                            {
                                                _portCollection.Ports[index].DownSideValue = GetGMUseGDM();
                                            }
                                            ValueEventArgs eee = new ValueEventArgs(valueListTemp[indx], index);
                                            OnValueChanged(eee);
                                        }
                                    }
                                    valueList2 = valueListTemp;
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                    #region 无线接收
                    else if (Sensor2 == 100)
                    {
                        IsUSBCOM = true;

                        byte[] QueryBuffer_JiaSuDu = new byte[13];
                        byte[] JaSuDu = new byte[11];
                        byte[] QueryBuffer_CiGanYing_2 = new byte[12];
                        byte[] ReceiveTwoDatas = new byte[8];
                        byte[] ReceiveFourDatas = new byte[14];
                        byte[] QueryBuffer_Guang_4 = new byte[14];
                        //byte[] voice = new byte[64];
                        byte[] WireLess = new byte[50];

                        WireLess = e.reportBuff;
                        _lastRefresh1 = DateTime.Now;

                        List<byte> WLQueryBuffer = new List<byte>();

                        if (WireLess[2] != 0x00 && WireLess[2] != 0xff)//无线接收1
                        {
                            WLQueryBuffer.Clear();
                            WLQueryBuffer.Add(0xa5);
                            WLQueryBuffer.Add(Sensor1);
                            //WLQueryBuffer.Add(8);
                            for (int i = 2; i <= 11; i++)
                            {
                                WLQueryBuffer.Add(WireLess[i]);
                                Sensor2 = WireLess[2];
                            }//查询传感器 1 ID

                            if (WireLess[8] != 0x00 && WireLess[8] != 0xff)//无线接收2
                            {
                                WLQueryBuffer.Clear();
                                WLQueryBuffer.Add(0xa5);
                                WLQueryBuffer.Add(Sensor1);
                                WLQueryBuffer.Add(WireLess[2]);
                                for (int i = 8; i <= 17; i++)
                                {
                                    WLQueryBuffer.Add(WireLess[i]);
                                }//查询传感器 2 ID
                                if (WireLess[14] != 0x00 && WireLess[14] != 0xff)//无线接收3
                                {
                                    WLQueryBuffer.Clear();
                                    WLQueryBuffer.Add(0xa5);
                                    WLQueryBuffer.Add(Sensor1);
                                    WLQueryBuffer.Add(WireLess[2]);
                                    WLQueryBuffer.Add(WireLess[8]);
                                    for (int i = 14; i <= 23; i++)
                                    {
                                        WLQueryBuffer.Add(WireLess[i]);
                                    }//查询传感器 3 ID
                                    if (WireLess[20] != 0x00 && WireLess[20] != 0xff)//无线接收1
                                    {
                                        WLQueryBuffer.Clear();
                                        WLQueryBuffer.Add(0xa5);
                                        WLQueryBuffer.Add(Sensor1);
                                        WLQueryBuffer.Add(WireLess[2]);
                                        WLQueryBuffer.Add(WireLess[8]);
                                        WLQueryBuffer.Add(WireLess[14]);
                                        for (int i = 20; i <= 28; i++)
                                        {
                                            WLQueryBuffer.Add(WireLess[i]);
                                        }//查询传感器 4 ID
                                        WLQueryBuffer.Add(0x00);
                                    }
                                }
                            }
                            if (Started)
                            {

                                if (--_skipPoint_wuxian == 0)
                                {
                                    _skipPoint_wuxian = (int)_ReceiveFrequence_1 / Frequence;

                                    if (WLQueryBuffer.Count >= 12)
                                    {
                                        int index = 1;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[5], WireLess[6], WireLess[7] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 13)
                                    {
                                        int index = 2;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[11], WireLess[12], WireLess[13] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 14)
                                    {
                                        int index = 3;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[17], WireLess[18], WireLess[19] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 15)
                                    {
                                        int index = 4;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[23], WireLess[24], WireLess[25] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                }

                            }
                            if (Adjusted)
                            {
                                if (--_skipPoint_1 == 0)
                                {
                                    _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                                    if (WLQueryBuffer.Count >= 8)
                                    {
                                        int index = 0;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[5], WireLess[6], WireLess[7] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 9)
                                    {
                                        int index = 1;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[11], WireLess[12], WireLess[13] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 10)
                                    {
                                        int index = 2;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[17], WireLess[18], WireLess[19] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 11)
                                    {
                                        int index = 3;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[23], WireLess[24], WireLess[25] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                }
                            }
                        }
                        if (_noExist3 && Sensor2 != 100)
                            UnpackQueryTypeUSB(WLQueryBuffer.ToArray());
                        return;
                    }
                    #endregion
                    else
                    {
                        if (Started)
                        {
                            if (Frequence <= 10)
                            {
                                _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                            }
                            else

                                _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                            Count_2 = (int)(USB_2_index * 16 / _skipPoint_1 - Count_Pre_2);
                            if (Count_2 != 0)
                            {
                                int index = 1;
                                for (int i = 0; i < Count_2; i++)
                                {
                                    float value = ByteToFloatUSB(new byte[] { buffer[3 * (i + 1) - 1], buffer[3 * (i + 1)], buffer[3 * (i + 1) + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                            }
                            USB_2_index++;
                            Count_Pre_2 = Count_2 + Count_Pre_2;
                        }
                        if (Adjusted)
                        {
                            int index = 1;
                            if (--_skipPoint_2 == 0)
                            {
                                _skipPoint_2 = (int)_ReceiveFrequence_1 / Frequence;
                                float value = ByteToFloatUSB(new byte[] { buffer[9], buffer[10], buffer[11] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                            }
                        }
                    }


                }
                #endregion
                #region 声波 22字节
                else if (buffer.Length == 22)//声波
                {
                    voice = e.reportBuff;
                    _lastRefresh1 = DateTime.Now;
                    Sensor2 = 8;//声波编号
                    if (voice[0] == 0xa0)
                    {
                        if (Started)
                        {
                            int index = 1;
                            for (int i = 1; i <= 10; i++)
                            {
                                float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                            }
                            //for (int i = 2; i <= 63; i++)
                            //{
                            //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                            //    OnValueChanged(ee);
                            //}
                        }
                        //QueryBuffer[0] = 0xa5;
                        QueryBuffer[2] = 8;
                        //for (int i = 2; i < 8; i++)
                        //{
                        //    QueryBuffer[i] = 0;
                        //}
                        //UnpackQueryTypeUSB(QueryBuffer);
                    }

                }
                #endregion
                //ConnectEventArgs a = new ConnectEventArgs(true);//连接问题
                //OnConnectedChanged(a);
                if (_noExist3)
                {
                    UnpackQueryTypeUSB(QueryBuffer);
                }
            }

        }
        Int64 USB_3_index = 0;
        int Count_3 = 0;
        int Count_Pre_3 = 0;
        void _hid_3_DataReceived(object sender, report e)
        {
            IsUSBCOM = true;
            _noExist3 = false;
            byte[] buffer = e.reportBuff;
            byte[] QueryBuffer = new byte[13];
            byte[] voice = new byte[64];
            Sensor3 = buffer[1];

            QueryBuffer[0] = 0xa5;
            if (Sensor1 == 0x00)
                return;
            if (Sensor2 == 0x00)
                return;
            QueryBuffer[1] = Sensor1;
            QueryBuffer[2] = Sensor2;
            QueryBuffer[3] = Sensor3;
            _lastRefresh3 = DateTime.Now;
            #region 50字节传感器

            if (buffer.Length == 50 && buffer[0] == 0xa5)
            {
                if (Started)
                {
                    if (Frequence <= 10)
                    {
                        _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                    }
                    else
                        _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;

                    Count_3 = (int)(USB_3_index * 16 / _skipPoint_1 - Count_Pre_3);
                    if (Count_3 != 0)
                    {
                        int index = 2;
                        for (int i = 0; i < Count_3; i++)
                        {
                            float value = ByteToFloatUSB(new byte[] { buffer[3 * (i + 1) - 1], buffer[3 * (i + 1)], buffer[3 * (i + 1) + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                    }
                    USB_3_index++;
                    Count_Pre_3 = Count_3 + Count_Pre_3;

                }
                if (Adjusted)
                {
                    int index = 2;
                    if (--_skipPoint_3 == 0)
                    {
                        _skipPoint_3 = (int)_ReceiveFrequence_1 / Frequence;
                        float value = ByteToFloatUSB(new byte[] { buffer[10], buffer[11], buffer[12] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                        ValueEventArgs ee = new ValueEventArgs(value, index);
                        OnValueChanged(ee);
                    }
                }

            }
            #endregion
            #region 声波 22字节
            else if (buffer.Length == 22)//声波
            {
                voice = e.reportBuff;
                _lastRefresh1 = DateTime.Now;
                Sensor3 = 8;//声波编号
                if (voice[0] == 0xa0)
                {
                    if (Started)
                    {
                        int index = 2;
                        for (int i = 1; i <= 10; i++)
                        {
                            float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                        //for (int i = 2; i <= 63; i++)
                        //{
                        //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                        //    OnValueChanged(ee);
                        //}
                    }
                    //QueryBuffer[0] = 0xa5;
                    QueryBuffer[3] = 8;
                    //for (int i = 2; i < 8; i++)
                    //{
                    //    QueryBuffer[i] = 0;
                    //}
                    //UnpackQueryTypeUSB(QueryBuffer);
                }

            }
            #endregion
            //ConnectEventArgs a = new ConnectEventArgs(true);//连接问题
            //OnConnectedChanged(a);
            if (_noExist4)
            {
                UnpackQueryTypeUSB(QueryBuffer);
            }
        }
        Int64 USB_4_index = 0;
        int Count_4 = 0;
        int Count_Pre_4 = 0;
        void _hid_4_DataReceived(object sender, report e)
        {
            IsUSBCOM = true;
            _noExist4 = false;
            byte[] buffer = e.reportBuff;
            byte[] QueryBuffer = new byte[14];
            byte[] voice = new byte[64];
            Sensor4 = buffer[1];
            QueryBuffer[0] = 0xa5;
            if (Sensor1 == 0x00)
                return;
            if (Sensor2 == 0x00)
                return;
            if (Sensor3 == 0x00)
                return;
            QueryBuffer[1] = Sensor1;
            QueryBuffer[2] = Sensor2;
            QueryBuffer[3] = Sensor3;
            QueryBuffer[4] = Sensor4;
            _lastRefresh4 = DateTime.Now;
            #region 50字节传感器
            if (buffer.Length == 50 && buffer[0] == 0xa5)
            {
                if (Started)
                {
                    if (Frequence <= 10)
                    {
                        _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                    }
                    else
                        _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;

                    Count_4 = (int)(USB_4_index * 16 / _skipPoint_1 - Count_Pre_4);
                    if (Count_4 != 0)
                    {
                        int index = 3;
                        for (int i = 0; i < Count_4; i++)
                        {
                            float value = ByteToFloatUSB(new byte[] { buffer[3 * (i + 1) - 1], buffer[3 * (i + 1)], buffer[3 * (i + 1) + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                    }
                    USB_4_index++;
                    Count_Pre_4 = Count_4 + Count_Pre_4;
                }
                if (Adjusted)
                {
                    int index = 4;
                    if (--_skipPoint_4 == 0)
                    {
                        //_skipPoint_4 = (int)_ReceiveFrequence_4 / Frequence;
                        float value = ByteToFloatUSB(new byte[] { buffer[11], buffer[12], buffer[13] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                        ValueEventArgs ee = new ValueEventArgs(value, index);
                        OnValueChanged(ee);
                    }
                }

            }
            #endregion
            #region 声波 22字节
            else if (buffer.Length == 22)//声波
            {
                voice = e.reportBuff;
                _lastRefresh1 = DateTime.Now;
                Sensor4 = 8;//声波编号
                if (voice[0] == 0xa0)
                {
                    if (Started)
                    {
                        int index = 3;
                        for (int i = 1; i <= 10; i++)
                        {
                            float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                        //for (int i = 2; i <= 63; i++)
                        //{
                        //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                        //    OnValueChanged(ee);
                        //}
                    }
                    //QueryBuffer[0] = 0xa5;
                    QueryBuffer[4] = 8;
                    //for (int i = 2; i < 8; i++)
                    //{
                    //    QueryBuffer[i] = 0;
                    //}
                    //UnpackQueryTypeUSB(QueryBuffer);
                }

            }
            #endregion
            //ConnectEventArgs a = new ConnectEventArgs(true);//连接问题
            //OnConnectedChanged(a);
            if (_noExist5)
            {
                UnpackQueryTypeUSB(QueryBuffer);
            }

        }
        Int64 USB_5_index = 0;
        int Count_5 = 0;
        int Count_Pre_5 = 0;
        void _hid_5_DataReceived(object sender, report e)
        {
            IsUSBCOM = true;
            _noExist5 = false;
            byte[] QueryBuffer = new byte[15];
            byte[] buffer = e.reportBuff;

            Sensor5 = buffer[1];
            QueryBuffer[0] = 0xa5;
            if (Sensor1 == 0x00)
                return;
            if (Sensor2 == 0x00)
                return;
            if (Sensor3 == 0x00)
                return;
            if (Sensor4 == 0x00)
                return;
            QueryBuffer[1] = Sensor1;
            QueryBuffer[2] = Sensor2;
            QueryBuffer[3] = Sensor3;
            QueryBuffer[4] = Sensor4;
            QueryBuffer[5] = Sensor5;

            _lastRefresh5 = DateTime.Now;

            if (buffer.Length == 50 && buffer[0] == 0xa5)
            {
                if (Started)
                {
                    if (Frequence <= 10)
                    {
                        _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                    }
                    else

                        _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                    Count_5 = (int)(USB_5_index * 16 / _skipPoint_1 - Count_Pre_5);
                    if (Count_5 != 0)
                    {
                        int index = 4;
                        for (int i = 0; i < Count_5; i++)
                        {
                            float value = ByteToFloatUSB(new byte[] { buffer[3 * (i + 1) - 1], buffer[3 * (i + 1)], buffer[3 * (i + 1) + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                    }
                    USB_5_index++;
                    Count_Pre_5 = Count_5 + Count_Pre_5;
                }
                if (Adjusted)
                {
                    int index = 4;
                    if (--_skipPoint_5 == 0)
                    {
                        _skipPoint_2 = (int)_ReceiveFrequence_1 / Frequence;
                        float value = ByteToFloatUSB(new byte[] { buffer[9], buffer[10], buffer[11] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                        ValueEventArgs ee = new ValueEventArgs(value, index);
                        OnValueChanged(ee);
                    }
                }

            }
            //ConnectEventArgs a = new ConnectEventArgs(true);//连接问题
            //OnConnectedChanged(a);
            if (_noExist6)
            {
                UnpackQueryTypeUSB(QueryBuffer);
            }


        }
        Int64 USB_6_index = 0;
        int Count_6 = 0;
        int Count_Pre_6 = 0;
        void _hid_6_DataReceived(object sender, report e)
        {
            IsUSBCOM = true;
            _noExist6 = false;
            byte[] QueryBuffer = new byte[16];
            byte[] buffer = e.reportBuff;

            Sensor6 = buffer[1];
            QueryBuffer[0] = 0xa5;
            if (Sensor1 == 0x00)
                return;
            if (Sensor2 == 0x00)
                return;
            if (Sensor3 == 0x00)
                return;
            if (Sensor4 == 0x00)
                return;
            if (Sensor5 == 0x00)
                return;

            QueryBuffer[1] = Sensor1;
            QueryBuffer[2] = Sensor2;
            QueryBuffer[3] = Sensor3;
            QueryBuffer[4] = Sensor4;
            QueryBuffer[5] = Sensor5;
            QueryBuffer[6] = Sensor6;

            _lastRefresh6 = DateTime.Now;

            if (buffer.Length == 50 && buffer[0] == 0xa5)
            {
                if (Started)
                {
                    if (Frequence <= 10)
                    {
                        _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                    }
                    else

                        _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                    Count_6 = (int)(USB_6_index * 16 / _skipPoint_1 - Count_Pre_6);
                    if (Count_6 != 0)
                    {
                        int index = 5;
                        for (int i = 0; i < Count_6; i++)
                        {
                            float value = ByteToFloatUSB(new byte[] { buffer[3 * (i + 1) - 1], buffer[3 * (i + 1)], buffer[3 * (i + 1) + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                    }
                    USB_6_index++;
                    Count_Pre_6 = Count_6 + Count_Pre_6;
                }
                if (Adjusted)
                {
                    int index = 5;
                    if (--_skipPoint_6 == 0)
                    {
                        _skipPoint_2 = (int)_ReceiveFrequence_1 / Frequence;
                        float value = ByteToFloatUSB(new byte[] { buffer[9], buffer[10], buffer[11] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                        ValueEventArgs ee = new ValueEventArgs(value, index);
                        OnValueChanged(ee);
                    }
                }

            }
            //ConnectEventArgs a = new ConnectEventArgs(true);//连接问题
            //OnConnectedChanged(a);
            if (_noExist7)
            {
                UnpackQueryTypeUSB(QueryBuffer);
            }


        }
        Int64 USB_7_index = 0;
        int Count_7 = 0;
        int Count_Pre_7 = 0;
        void _hid_7_DataReceived(object sender, report e)
        {
            IsUSBCOM = true;
            _noExist7 = false;
            byte[] QueryBuffer = new byte[17];
            byte[] buffer = e.reportBuff;

            Sensor7 = buffer[1];
            QueryBuffer[0] = 0xa5;
            if (Sensor1 == 0x00)
                return;
            if (Sensor2 == 0x00)
                return;
            if (Sensor3 == 0x00)
                return;
            if (Sensor4 == 0x00)
                return;
            if (Sensor5 == 0x00)
                return;
            if (Sensor6 == 0x00)
                return;

            QueryBuffer[1] = Sensor1;
            QueryBuffer[2] = Sensor2;
            QueryBuffer[3] = Sensor3;
            QueryBuffer[4] = Sensor4;
            QueryBuffer[5] = Sensor5;
            QueryBuffer[6] = Sensor6;
            QueryBuffer[7] = Sensor7;

            _lastRefresh7 = DateTime.Now;

            if (buffer.Length == 50 && buffer[0] == 0xa5)
            {
                if (Started)
                {
                    if (Frequence <= 10)
                    {
                        _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                    }
                    else

                        _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                    Count_7 = (int)(USB_7_index * 16 / _skipPoint_1 - Count_Pre_7);
                    if (Count_2 != 0)
                    {
                        int index = 6;
                        for (int i = 0; i < Count_7; i++)
                        {
                            float value = ByteToFloatUSB(new byte[] { buffer[3 * (i + 1) - 1], buffer[3 * (i + 1)], buffer[3 * (i + 1) + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                    }
                    USB_7_index++;
                    Count_Pre_7 = Count_7 + Count_Pre_7;
                }
                if (Adjusted)
                {
                    int index = 6;
                    if (--_skipPoint_7 == 0)
                    {
                        _skipPoint_7 = (int)_ReceiveFrequence_1 / Frequence;
                        float value = ByteToFloatUSB(new byte[] { buffer[9], buffer[10], buffer[11] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                        ValueEventArgs ee = new ValueEventArgs(value, index);
                        OnValueChanged(ee);
                    }
                }

            }
            //ConnectEventArgs a = new ConnectEventArgs(true);//连接问题
            //OnConnectedChanged(a);
            if (_noExist8)
            {
                UnpackQueryTypeUSB(QueryBuffer);
            }


        }
        Int64 USB_8_index = 0;
        int Count_8 = 0;
        int Count_Pre_8 = 0;
        void _hid_8_DataReceived(object sender, report e)
        {
            IsUSBCOM = true;
            _noExist8 = false;
            byte[] QueryBuffer = new byte[18];
            byte[] buffer = e.reportBuff;

            Sensor8 = buffer[1];
            QueryBuffer[0] = 0xa5;
            if (Sensor1 == 0x00)
                return;
            if (Sensor2 == 0x00)
                return;
            if (Sensor3 == 0x00)
                return;
            if (Sensor4 == 0x00)
                return;
            if (Sensor5 == 0x00)
                return;
            if (Sensor6 == 0x00)
                return;
            if (Sensor7 == 0x00)
                return;

            QueryBuffer[1] = Sensor1;
            QueryBuffer[2] = Sensor2;
            QueryBuffer[3] = Sensor3;
            QueryBuffer[4] = Sensor4;
            QueryBuffer[5] = Sensor5;
            QueryBuffer[6] = Sensor6;
            QueryBuffer[7] = Sensor7;
            QueryBuffer[8] = Sensor8;

            _lastRefresh8 = DateTime.Now;

            if (buffer.Length == 50 && buffer[0] == 0xa5)
            {
                if (Started)
                {
                    if (Frequence <= 10)
                    {
                        _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                    }
                    else

                        _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                    Count_8 = (int)(USB_8_index * 16 / _skipPoint_1 - Count_Pre_8);
                    if (Count_8 != 0)
                    {
                        int index = 7;
                        for (int i = 0; i < Count_8; i++)
                        {
                            float value = ByteToFloatUSB(new byte[] { buffer[3 * (i + 1) - 1], buffer[3 * (i + 1)], buffer[3 * (i + 1) + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            ValueEventArgs ee = new ValueEventArgs(value, index);
                            OnValueChanged(ee);
                        }
                    }
                    USB_8_index++;
                    Count_Pre_8 = Count_8 + Count_Pre_8;
                }
                if (Adjusted)
                {
                    int index = 7;
                    if (--_skipPoint_8 == 0)
                    {
                        _skipPoint_8 = (int)_ReceiveFrequence_1 / Frequence;
                        float value = ByteToFloatUSB(new byte[] { buffer[9], buffer[10], buffer[11] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                        ValueEventArgs ee = new ValueEventArgs(value, index);
                        OnValueChanged(ee);
                    }
                }

            }
            //ConnectEventArgs a = new ConnectEventArgs(true);//连接问题
            //OnConnectedChanged(a);
            UnpackQueryTypeUSB(QueryBuffer);


        }
        void DataCatch_SourceDataRecieved(object sender, SourceDataEventArgs e)
        {
            byte[] buffer = e.Buffer;
            if (buffer.Length >= 3)
            {
                byte length = buffer[0];
                byte command = buffer[1];
                switch (command)
                {
                    case (byte)CommandDefine.QueryType:

                        UnpackQueryType(buffer);
                        break;
                    case (byte)CommandDefine.QueryShift:
                        UnpackQueryShift(buffer);
                        break;
                    case (byte)CommandDefine.ShiftSet:
                        UnpackSetShift(buffer);
                        break;
                    case (byte)CommandDefine.Start:
                        UnparkStart(buffer);
                        break;
                    case (byte)CommandDefine.Stop:
                        StartStopEventArgs startStopEventArgs = new StartStopEventArgs(false);
                        OnWorkStatusChanged(startStopEventArgs);
                        break;
                }
            }
        }
        #region Props
        SerialPortEngine _serialPortEngine;
        PortCollection _portCollection;
        Method_GetSensorType _getSensorType = null;
        Thread _readThread = null;
        bool _looping = true;
        bool _started = false;

        bool _adjusted = false;

        int _frequence_1 = 10;
        int _ReceiveFrequence_1 = 1000;
        int _skipPoint_1 = 0, _skipPoint_2 = 0, _skipPoint_3 = 0, _skipPoint_4 = 0;
        int _skipPoint_5 = 0, _skipPoint_6 = 0, _skipPoint_7 = 0, _skipPoint_8 = 0;
        int _skipPoint_wuxian = 0;

        int _ReceiveFrequence_JiaSuDu = 250;
        int _skipPoint_JiaSuDu = 0;

        byte Sensor1 = 0x00;
        byte Sensor2 = 0x00;
        byte Sensor3 = 0x00;
        byte Sensor4 = 0x00;
        byte Sensor5 = 0x00;
        byte Sensor6 = 0x00;
        byte Sensor7 = 0x00;
        byte Sensor8 = 0x00;


        bool _noExist2 = true;
        bool _noExist3 = true;
        bool _noExist4 = true;
        bool _noExist5 = true;
        bool _noExist6 = true;
        bool _noExist7 = true;
        bool _noExist8 = true;



        int W_Index = 0;
        bool QueryUsbIsOk = false;
        bool WireLessQueryStop = false;
        bool _isUsbCom = false;

        //bool WireLessQueryStop = false;

        //public int SplitPoint
        //{
        //    get {return _skipPoint ;}
        //    set { _skipPoint = value; }
        //}

        int voiceCount = 0;
        long voiceValue = 0;
        float V_Value;//声振动传感器 波形幅值

        bool IsVoice = false;

        int count_clear = 0;
        static TimeSpan LOST_CONNECT_SPAN = new TimeSpan(0, 0, 0, 1);
        DateTime _lastRefresh1 = DateTime.Now;//判断USB是否连接
        DateTime _lastRefresh2 = DateTime.Now;//判断USB是否连接
        DateTime _lastRefresh3 = DateTime.Now;//判断USB是否连接
        DateTime _lastRefresh4 = DateTime.Now;//判断USB是否连接
        DateTime _lastRefresh5 = DateTime.Now;//判断USB是否连接
        DateTime _lastRefresh6 = DateTime.Now;//判断USB是否连接
        DateTime _lastRefresh7 = DateTime.Now;//判断USB是否连接
        DateTime _lastRefresh8 = DateTime.Now;//判断USB是否连接



        public bool IsUSBCOM
        {
            get { return _isUsbCom; }
            set { _isUsbCom = value; }
        }
        public int Frequence
        {
            get { return _frequence_1; }
            set {
                _frequence_1 = value;
                if (_hid_1.deviceOpened)//新协议下发的  设频率
                {
                    _hid_1.SendCommand(NewCommandType.SetFrequency(ConnectType.USB,value));
                }
                if (_hid_2.deviceOpened)//新协议下发的
                {
                    _hid_1.SendCommand(NewCommandType.SetFrequency(ConnectType.USB, value));
                }
            }
        }
        //public int Frequence2
        //{
        //    get { return _frequence_2; }
        //    set { _frequence_2 = value; }
        //}
        //public int Frequence3
        //{
        //    get { return _frequence_3; }
        //    set { _frequence_3 = value; }
        //}
        //public int Frequence4
        //{
        //    get { return _frequence_4; }
        //    set { _frequence_4 = value; }
        //}
        //byte type_1=0x00;
        public PortCollection PortCollection
        {
            get { return _portCollection; }
        }
        public bool Connected
        {
            //get { return _serialPortEngine.Connected; }
            get { return true; }
        }
        public bool Working
        {
            get { return _serialPortEngine.Working; }
        }
        public bool Started
        {
            get { return _started; }
            set
            {
                _started = value;
                if (value)
                {
                    GDMFirstTime = DateTime.Now;
                }
            }
        }
        public bool Adjusted
        {
            get { return _adjusted; }
            set { _adjusted = value; }
        }
        #endregion

        #region Methods
        public void Dispose()
        {
            _looping = false;
            _serialPortEngine.Dispose();
        }
        public void QueryShift(int index)
        {
            if (index >= byte.MinValue && index <= byte.MaxValue && _portCollection.Contains(index))
            {
                if (_serialPortEngine.Connected)
                {
                    _serialPortEngine.Send(QueryDefine.QueryShift((byte)index));
                }
            }
        }
        public void SetShift(int index, byte shift)
        {
            if (index >= byte.MinValue && index <= byte.MaxValue && _portCollection.Contains(index))
            {
                if (_serialPortEngine.Connected)
                {
                    _serialPortEngine.Send(QueryDefine.SetShift((byte)index, shift));
                }
            }
        }
        static byte[] aaa = new byte[11];
        public void Start(byte a, byte b, byte c)
        {
            int FrequenceUsbSend;
            FrequenceUsbSend = Frequence * 10;
            aaa[0] = 0xa6;
            aaa[1] = 0x04;
            aaa[7] = Convert.ToByte(FrequenceUsbSend / 16777216);
            aaa[8] = Convert.ToByte((FrequenceUsbSend % 16777216) / 65535);
            aaa[9] = Convert.ToByte(((FrequenceUsbSend % 16777216) % 65535) / 256);
            aaa[10] = Convert.ToByte(((FrequenceUsbSend % 16777216) % 65535) % 256);
            report r = new report(0x00, aaa);
            if (_serialPortEngine.Connected && !_serialPortEngine.Working)
            {
                StartStopEventArgs startStopEventArgs = new StartStopEventArgs(true);
                OnWorkStatusChanged(startStopEventArgs);
                _serialPortEngine.Working = true;
                _serialPortEngine.Send(QueryDefine.Start(a, b, c));
            }
            if (Started)//USB
            {
                StartStopEventArgs startStopEventArgs = new StartStopEventArgs(true);
                OnWorkStatusChanged(startStopEventArgs);
                if (!IsStartBLE)
                {
                    _hid_1.Write(r);
                    _hid_2.Write(r);
                    _hid_3.Write(r);
                    _hid_4.Write(r);
                }
            }
            if (Adjusted)//USB
            {
                StartStopEventArgs startStopEventArgs = new StartStopEventArgs(true);
                OnWorkStatusChanged(startStopEventArgs);
            }

        }
        public void Wave(byte Wave, byte Wavetype, byte value, byte time)
        {
            if (_serialPortEngine.Connected)
                _serialPortEngine.Send(QueryDefine.Wave(Wave, Wavetype, value, time));
        }
        public void USBWave(byte Type, byte YY, byte XX, byte DD, byte PP)
        {
            report r = new report(0, QueryDefine.USBWave(Type, YY, XX, DD, PP));
            _hid_1.Write(r);
            _hid_2.Write(r);
            _hid_3.Write(r);
            _hid_4.Write(r);
        }
        public void Stop()
        {
            if (_serialPortEngine.Connected && _serialPortEngine.Working)
            {
                _serialPortEngine.Working = false;
                _serialPortEngine.Send(QueryDefine.Stop());
            }
            StartStopEventArgs startStopEventArgs = new StartStopEventArgs(false);//USB
            OnWorkStatusChanged(startStopEventArgs);
            USB_1_index = 0;
            Count_1 = 0;
            Count_Pre_1 = 0;

            USB_2_index = 0;
            Count_2 = 0;
            Count_Pre_2 = 0;

            USB_3_index = 0;
            Count_3 = 0;
            Count_Pre_3 = 0;

            USB_4_index = 0;
            Count_4 = 0;
            Count_Pre_4 = 0;
            Photo = false;
        }
        void Looping()
        {
            while (_looping)
            {

                if (_serialPortEngine.DataCatch.HaveNewData)
                {
                    _serialPortEngine.DataCatch.Read();
                    Thread.Sleep(1);
                }
                else
                {
                    Thread.Sleep(10);
                }
                Thread.Sleep(1000);
                if (runningTime < 30)
                {
                    runningTime++;
                }
                else
                {
                    if (IsStartBLE)
                    {
                        UnpackQueryTypeUSB(sensorBuffer);  //蓝牙的搜索  新协议暂时关闭
                    }
                }
            }
        }
        /// <summary>
        /// 转换数据类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        float ByteToFloat(byte[] data, float k, float b)
        {
            //float value = 0;
            //if (data.Length == 2)
            //{
            //    //value = (data[0] * 256 + data[1]) * k + b;
            //    value = data[0] * 256 + data[1];
            //    if (value >= 32768)
            //    {
            //        value = (65536 - value) * -1f;
            //    }
            //    value = value * k - b;
            //}
            //return value;
            float value = 0;
            if (data.Length == 2)
            {
                value = (data[0] * 256 + data[1]) * k + b;
            }
            return value;

        }
        float ByteToFloatUSB(byte[] data, float k, float b)
        {
            float value = 0;
            if (data.Length == 3)
            {
                if (data[0] < 128)
                {
                    value = (data[0] * 65536 + data[1] * 256 + data[2]) * k + b;
                }
                else
                    value = -1 * (((data[0] - 128) * 65536 + data[1] * 256 + data[2]) * k + b);
            }
            return value;
        }
        float ByteToFloatUSBVoice(byte[] data, float k, float b)
        {
            float value = 0;
            value = (data[0] * 256 + data[1]) * k + b;
            return value;
        }
        float ByteToGate(byte[] data)
        {
            return (((data[0] * 256 + data[1]) * 256 + data[2]) * 256 + data[3]) * 0.0001f;
        }
        float UsbByteToGate(byte[] data, float k, float b)
        {
            return (((data[0] * 256 + data[1]) * 256 + data[2]) * 256 + data[3]) * k + b;
        }
        void UnpackQueryTypeUSBForNewInt(int[] sensorIDs)
        {
            bool changed = false;
            int count = sensorIDs.Length;
            if (count != _portCollection.AvailablePortCount)
            {
                changed = true;
            }
            if (count != count_clear)//传感器显示 热插拔
            {
                _portCollection.PlugOut(0);
                _portCollection.PlugOut(1);
                _portCollection.PlugOut(2);
                _portCollection.PlugOut(3);
                _portCollection.PlugOut(4);
                _portCollection.PlugOut(5);
                _portCollection.PlugOut(6);
                _portCollection.PlugOut(7);
            }
            count_clear = count;
            if (count > 0 && count < _portCollection.PortCount)
            {
                for (int i = 0; i < count; i++)
                {
                    int index = i;
                    if (_portCollection.PortChanged(i, sensorIDs[index]))
                    {
                        changed = true;
                    }
                    SensorTypeDefine sensorType = SensorTypeDefine.Normal;
                    float k = 1, b = 0;
                    if (_getSensorType != null)
                    {
                        sensorType = _getSensorType(sensorIDs[index], ref k, ref b);
                    }
                    _portCollection.PlugIn(i, sensorIDs[index], sensorType, k, b);
                }
            }

            if (changed)
            {
                PortCollectionEventArgs e = new PortCollectionEventArgs(_portCollection.Ports);
                OnPortChanged(e);
            }
        }
        void UnpackQueryTypeUSB(byte[] buffer)
        {
            bool changed = false;
            int count = buffer.Length - 10;
            if (count != _portCollection.AvailablePortCount)
            {
                changed = true;
            }
            if (count != count_clear)//传感器显示 热插拔
            {
                _portCollection.PlugOut(0);
                _portCollection.PlugOut(1);
                _portCollection.PlugOut(2);
                _portCollection.PlugOut(3);
                _portCollection.PlugOut(4);
                _portCollection.PlugOut(5);
                _portCollection.PlugOut(6);
                _portCollection.PlugOut(7);

            }
            count_clear = count;
            if (count > 0 && count < _portCollection.PortCount)
            {
                for (int i = 0; i < count; i++)
                {
                    int index = i + 1;
                    if (_portCollection.PortChanged(i, buffer[index]))
                    {
                        changed = true;
                    }
                    SensorTypeDefine sensorType = SensorTypeDefine.Normal;
                    float k = 1, b = 0;
                    if (_getSensorType != null)
                    {
                        sensorType = _getSensorType(buffer[index], ref k, ref b);
                    }
                    _portCollection.PlugIn(i, buffer[index], sensorType, k, b);
                }
            }
            if (changed)
            {
                PortCollectionEventArgs e = new PortCollectionEventArgs(_portCollection.Ports);
                OnPortChanged(e);
            }

        }

        int countLastTime = 0;
        public void UnpackQueryTypeUSB(List<SenserMsg> sensorBuffer)
        {
            try
            {
                bool changed = false;
                int count = sensorBuffer.Count();
                if (count != _portCollection.AvailablePortCount)
                {
                    changed = true;
                }
                if (count != countLastTime)//传感器显示 热插拔
                {
                    _portCollection.PlugOut(0);
                    _portCollection.PlugOut(1);
                    _portCollection.PlugOut(2);
                    _portCollection.PlugOut(3);
                    _portCollection.PlugOut(4);
                    _portCollection.PlugOut(5);
                    _portCollection.PlugOut(6);
                    _portCollection.PlugOut(7);

                }
                countLastTime = count;
                if (count > 0 && count < _portCollection.PortCount)
                {

                    for (int i = 0; i < count; i++)
                    {
                        if (_portCollection.PortChanged(i, sensorBuffer[i].sensorID))
                        {
                            changed = true;
                            Thread.Sleep(200);
                            //bve = new BetteryValueEventArgs(sensorBuffer[i].Power, sensorBuffer[i].index);
                            //OnBetteryValueChanged(bve);
                        }
                        SensorTypeDefine sensorType = SensorTypeDefine.Normal;
                        float k = 1, b = 0;
                        if (_getSensorType != null)
                        {
                            sensorType = _getSensorType(sensorBuffer[i].sensorID, ref k, ref b);
                        }
                        _portCollection.PlugIn(i, sensorBuffer[i].sensorID, sensorType, k, b);
                        UpdataHidBLEKB(sensorBuffer[i], k, b);
                    }
                }
                if (changed)
                {
                    PortCollectionEventArgs e = new PortCollectionEventArgs(_portCollection.Ports);
                    OnPortChanged(e);
                }
                //StartBluetoothSearch();
                Thread.Sleep(3000);
            }
            catch (Exception exc)
            {
                string s = exc.Message;
            }
        }
        public void UpdataHidBLEKB(SenserMsg sm, float k, float b)
        {
            if (sm.connectName.Contains("hd"))
            {
                //switch (sm.index)
                //{
                //    case 0:
                //        hd1.K = k;
                //        hd1.B = b;
                //        break;
                //    case 1:
                //        hd2.K = k;
                //        hd2.B = b;
                //        break;
                //    case 2:
                //        hd3.K = k;
                //        hd3.B = b;
                //        break;
                //    case 3:
                //        hd4.K = k;
                //        hd4.B = b;
                //        break;
                //    default:
                //        break;
                //}
            }
            else//BLE
            {
                switch (sm.connectName)
                {
                    case "ble1":
                        ble1.K = k;
                        ble1.B = b;
                        break;
                    case "ble2":
                        ble2.K = k;
                        ble2.B = b;
                        break;
                    case "ble3":
                        ble3.K = k;
                        ble3.B = b;
                        break;
                    case "ble4":
                        ble4.K = k;
                        ble4.B = b;
                        break;
                    default:
                        break;
                }
            }
        }

        void UnpackQueryType(byte[] buffer)
        {
            //float a1 = _portCollection.Ports[1].K;
            //float value1 = ByteToFloat(new byte[] { 2, 2 }, _portCollection.Ports[1].K, _portCollection.Ports[1].B);
            try
            {
                bool changed = false;
                int count = buffer.Length - 3;
                if (count != _portCollection.AvailablePortCount)
                {
                    changed = true;
                }
                if (count != count_clear)//传感器显示 热插拔
                {
                    _portCollection.PlugOut(0);
                    _portCollection.PlugOut(1);
                    _portCollection.PlugOut(2);
                    _portCollection.PlugOut(3);

                    //if (count_clear == 1)
                    //{
                    //    _portCollection.PlugIn(0, 0, 0x00, 0, 0);
                    //    //count_clear = 1;
                    //}
                    //if (count_clear == 2)
                    //{
                    //    _portCollection.PlugIn(0, 0, 0x00, 0, 0);
                    //    _portCollection.PlugIn(1, 0, 0x00, 0, 0);

                    //    //_portCollection.PlugIn(2, 0, 0x00, 0, 0);
                    //    //_portCollection.PlugIn(3, 0, 0x00, 0, 0);
                    //}
                    ////count_clear = count;
                    //if (count_clear == 3)
                    //{
                    //    _portCollection.PlugIn(0, 0, 0x00, 0, 0);
                    //    _portCollection.PlugIn(1, 0, 0x00, 0, 0);

                    //    _portCollection.PlugIn(2, 0, 0x00, 0, 0);
                    //    //_portCollection.PlugIn(3, 0, 0x00, 0, 0);
                    //}
                    //if (count_clear == 4)
                    //{
                    //    _portCollection.PlugIn(0, 0, 0x00, 0, 0);
                    //    _portCollection.PlugIn(1, 0, 0x00, 0, 0);

                    //    _portCollection.PlugIn(2, 0, 0x00, 0, 0);
                    //    _portCollection.PlugIn(3, 0, 0x00, 0, 0);
                    //}
                }
                count_clear = count;

                if (count > 0 && count < _portCollection.PortCount)
                {
                    for (int i = 0; i < count; i++)
                    {
                        int index = i + 2;

                        if (_portCollection.PortChanged(i, buffer[index]))
                        {
                            changed = true;
                        }
                        if (buffer[index] == 28)//是否是声振动传感器；
                        {
                            IsVoice = true;
                        }
                        SensorTypeDefine sensorType = SensorTypeDefine.Normal;
                        float k = 1, b = 0;
                        if (_getSensorType != null)
                        {
                            sensorType = _getSensorType(buffer[index], ref k, ref b);
                        }
                        _portCollection.PlugIn(i, buffer[index], sensorType, k, b);
                    }
                }
                if (changed)
                {
                    PortCollectionEventArgs e = new PortCollectionEventArgs(_portCollection.Ports);
                    OnPortChanged(e);
                }
            }
            catch (Exception exc)
            {
                string a = exc.Message;
            }
        }
        void UnpackQueryShift(byte[] buffer)
        {
            if (buffer.Length >= 5)
            {
                int index = (int)buffer[2];
                byte shift = buffer[3];
                ShiftEventArgs e = new ShiftEventArgs(index, shift);
                OnShiftChanged(e);
            }
        }
        void UnpackSetShift(byte[] buffer)
        {
            if (buffer.Length >= 5)
            {
                int index = (int)buffer[2];
                byte shift = buffer[3];
                ShiftEventArgs e = new ShiftEventArgs(index, shift);
                OnShiftChanged(e);
            }
        }
        void UnparkStart(byte[] buffer)
        {

            int length = (int)buffer[0] - 3;
            if (length > 0)
            {
                int index = (int)buffer[2];

                if (_portCollection.PlugInPortIndexes.Contains(index))
                {
                    SensorTypeDefine sensorType = _portCollection.Ports[index].SensorType;
                    switch (sensorType)
                    {
                        default:
                        case SensorTypeDefine.Normal:
                            if (buffer.Length >= 6)
                            {
                                for (int i = 3; i <= buffer.Length - 3; i += 2)
                                {
                                    float value = ByteToFloat(new byte[] { buffer[i], buffer[i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    //false Value2=ByteToFloat();


                                    _portCollection.Ports[index].LastValue = value;
                                    ValueEventArgs e = new ValueEventArgs(value, index);
                                    OnValueChanged(e);
                                    if (IsVoice)//声震动传感器 振幅的添加；
                                    {
                                        //float value2 = ByteToFloat(new byte[] { buffer[i], buffer[i + 1] }, _portCollection.Ports[29].K, _portCollection.Ports[29].B);
                                        float value2 = buffer[i] * 256 + buffer[i + 1];
                                        if (voiceCount < 100)
                                        {
                                            if (value2 > 0x700)
                                            {
                                                voiceValue += Convert.ToInt64(value2);
                                                voiceCount++;
                                            }
                                            if (V_Value != 0)
                                            {

                                                ValueEventArgs e1 = new ValueEventArgs(V_Value, 1);
                                                OnValueChanged(e1);
                                            }
                                        }
                                        else
                                        {
                                            V_Value = voiceValue / 100;
                                            V_Value = V_Value * 0.052287f - 64.117f;
                                            _portCollection.Ports[1].LastValue = value2;
                                            //ValueEventArgs e1 = new ValueEventArgs(V_Value, 1);
                                            //OnValueChanged(e1);
                                            voiceValue = 0;
                                            voiceCount = 0;
                                        }
                                    }
                                }
                            }
                            break;
                        case SensorTypeDefine.PhotoGate:

                            for (int i = 3; i <= buffer.Length - 5; i += 5)
                            {
                                byte side = buffer[i];
                                float value = ByteToGate(new byte[] { buffer[i + 1], buffer[i + 2], buffer[i + 3], buffer[i + 4] });

                                if (side == 0x01)
                                {
                                    float upsideValue = _portCollection.Ports[index].UpSideValue;
                                    _portCollection.Ports[index].DownSideValue = value;
                                    _portCollection.Ports[index].LastValue = value - upsideValue;
                                    ValueEventArgs e = new ValueEventArgs(value - upsideValue, index);
                                    OnValueChanged(e);
                                }
                                else
                                {
                                    _portCollection.Ports[index].LastValue = value;

                                    _portCollection.Ports[index].UpSideValue = value;
                                }
                            }
                            break;
                        case SensorTypeDefine.Heart:
                            break;
                    }
                }
            }
        }
        #endregion

        #region Events
        public event StartStopHandler WorkStatusChanged = null;
        protected void OnWorkStatusChanged(StartStopEventArgs e)
        {
            if (WorkStatusChanged != null)
            {
                WorkStatusChanged(this, e);
            }
        }
        public event PortCollectionHandler PortChanged = null;
        protected void OnPortChanged(PortCollectionEventArgs e)
        {
            if (PortChanged != null)
            {
                PortChanged(this, e);
            }
        }
        public event ShiftHandler ShiftChanged = null;
        protected void OnShiftChanged(ShiftEventArgs e)
        {
            if (ShiftChanged != null)
            {
                ShiftChanged(this, e);
            }
        }
        public event ConnectedHandler ConnectChanged = null;
        protected void OnConnectedChanged(ConnectEventArgs e)
        {
            if (ConnectChanged != null)
            {
                ConnectChanged(this, e);
            }
        }
        public event ValueHandler ValueChanged = null;
        protected void OnValueChanged(ValueEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
        #endregion

        #region Bluetooth

        public List<BluetoothInfo> bluetoothList = new List<BluetoothInfo>();//All the history bluetooth device
        List<BluetoothInfo> btListHistory = new List<BluetoothInfo>();//select bluetooth
        BLEDeviceConnect ble;
        BLEDeviceConnect ble1;
        BLEDeviceConnect ble2;
        BLEDeviceConnect ble3;
        BLEDeviceConnect ble4;

        List<SenserMsg> sensorBuffer = new List<SenserMsg>();
        int runningTime = 0;

        public void InitBLE()
        {
            Thread.Sleep(500);
            ble = new BLEDeviceConnect(0, "", "", "");
            ble.BleWarchingChanged += Ble1_BleWarchingChanged;

            StartBluetoothSearch();

            //BluetoothConnect(btListHistory);
        }

        public void StartBluetoothSearch()
        {
            if (ble != null)
            {
                //bluetoothList.Clear();
                ble.StartBluetoothSearch();
            }
        }

        public void BluetoothConnect(List<BluetoothInfo> btList)
        {
            btListHistory = btList;
            Task t = Task.Run(() =>
            {
                BluetoothInitConnect(btListHistory);
            });
            InitBlePara();
        }

        public void InitBlePara()
        {
            IsRecordBle1 = true;
            Ble1DataCount = 0;
            readDataIndex = 0;
            BleSkipPoint = 0;

            IsRecordBle2 = true;
            Ble2DataCount = 0;
            readDataIndex2 = 0;
            Ble2SkipPoint = 0;

            int sleepCount = 40;
            while (sleepCount > 0)
            {
                if (ble1 != null && ble1.bluetooth.IsConnected && ble1.IsNewSensor)
                {
                    ble1.SendCommand(NewCommandType.GetDeviceInfo());//新协议下发命令  旧的传感器收到命令就卡住了
                    break;
                }
                else
                {
                    sleepCount--;
                    Thread.Sleep(500);
                }
            }
        }

        public void StartSendCMDToDeviceBLE()
        {
            if (ble1 != null && ble1.bluetooth.IsConnected && ble1.IsNewSensor)//新协议下发的开始命令
            {
                //System.Threading.Thread.Sleep(10000);
                ble1.SendCommand(NewCommandType.GetDeviceDataStart(ConnectType.BLE));
            }
        }

        public void StartSendCMDToDevice(bool IsStart)
        {
            if (_hid_1.deviceOpened)//新协议下发的  设频率  读设备
            {
                //_hid_1.SendCommand(NewCommandType.SetFrequency(ConnectType.USB, Frequence));
                if (IsStart)
                {
                    _hid_1.SendCommand(NewCommandType.GetDeviceDataStart(ConnectType.USB));
                   
                }
                else
                {
                    _hid_1.SendCommand(NewCommandType.GetDeviceDataStop(ConnectType.USB));
                }
            }
            if (_hid_2.deviceOpened)//新协议下发的  读设备
            {

                if (IsStart)
                {
                    _hid_2.SendCommand(NewCommandType.GetDeviceDataStart(ConnectType.USB));

                }
                else
                {
                    _hid_2.SendCommand(NewCommandType.GetDeviceDataStop(ConnectType.USB));
                }
            }
        }

        public void BluetoothInitConnect(List<BluetoothInfo> btList)
        {

            BLEDispose();
            Thread.Sleep(1000);
            sensorBuffer.RemoveAll(o => o.connectName.Contains("ble"));
            byte usbCount = 0;
            for (int i = 0; i < btList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        ble1 = new BLEDeviceConnect(usbCount, btList[i].MAC, $"ble1", btList[i].Adresse);
                        ble1.DeceiveValueChanged += Ble1_DeceiveValueChanged;
                        ble1.BleDeleteChanged += Ble1_BleDeleteChanged;
                        ble1.AddSensorDataChanged += AddSensorDataChanged;
                        //Thread.Sleep(100000);
                        break;
                    case 1:
                        ble2 = new BLEDeviceConnect((byte)(usbCount + 1), btList[i].MAC, $"ble2", btList[i].Adresse);
                        ble2.DeceiveValueChanged += Ble2_DeceiveValueChanged;
                        ble2.BleDeleteChanged += Ble1_BleDeleteChanged;
                        ble2.AddSensorDataChanged += AddSensorDataChanged2;
                        break;
                    case 2:
                        ble3 = new BLEDeviceConnect((byte)(usbCount + 2), btList[i].MAC, $"ble3", btList[i].Adresse);
                        ble3.DeceiveValueChanged += Ble1_DeceiveValueChanged;
                        ble3.BleDeleteChanged += Ble1_BleDeleteChanged;
                        ble3.AddSensorDataChanged += AddSensorDataChanged;
                        break;
                    case 3:
                        ble4 = new BLEDeviceConnect((byte)(usbCount + 3), btList[i].MAC, $"ble4", btList[i].Adresse);
                        ble4.DeceiveValueChanged += Ble1_DeceiveValueChanged;
                        ble4.BleDeleteChanged += Ble1_BleDeleteChanged;
                        ble4.AddSensorDataChanged += AddSensorDataChanged;
                        break;
                    default:
                        break;
                }

            }
        }

        bool IsRecordBle1 = true;
        DateTime Ble1Datatime;
        int Ble1DataCount = 0;
        double readDataIndex = 0;
        double BleSkipPoint = 0;
        static double LastSkipPoint1 = 0;

        bool IsRecordBle2 = true;
        DateTime Ble2Datatime;
        int Ble2DataCount = 0;
        double readDataIndex2 = 0;
        double Ble2SkipPoint = 0;
        static double LastSkipPoint2 = 0;
        private object getSensorType;

        List<float> Receivedata(byte[] data, int dataStartIndex, int eachDataCount, float k, float b)
        {
            List<float> buffer = new List<float>();
            int dataIndex = dataStartIndex;
            while (dataIndex + eachDataCount <= data.Length)
            {
                var databuffer = data.Skip(dataIndex).Take(eachDataCount).ToArray();
                var value = ByteToFloatUSB(databuffer, k, b);
                dataIndex += eachDataCount;
                buffer.Add(value);
            }
            return buffer;
        }




        private void Ble1_DeceiveValueChanged(object sender, DeceiveDataArgs e)
        {
            #region 新协议
            var NewPackage = ParseBuffer.Parse(1, e.reportBuff);
            if (NewPackage.IsNewCommandType)
            {
                IsUSBCOM = true;
                _lastRefresh1 = DateTime.Now;

                if (NewPackage.IsRightPackage)
                {
                    switch (NewPackage.CommandType)
                    {
                        case NewCommandType.CommandType.GetDeviceInfo:
                            byte[] QueryBuffer = new byte[11];//查询传感器
                            //Sensor1 = NewPackage.sensorID;
                            //QueryBuffer[1] = NewPackage.sensorID;

                            if ((_noExist2 && !QueryUsbIsOk) || Sensor2 == 100)
                            {
                                UnpackQueryTypeUSB(QueryBuffer);
                            }
                            break;
                        case NewCommandType.CommandType.StartSendData:
                            if (Started)
                            {
                                int index = 0;
                                float value = NewPackage.ValueInt * _portCollection.Ports[index].K + _portCollection.Ports[index].B;
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                                ReceiveCount1 -= BleSkipPoint;
                            }
                            break;
                        default:
                            break;
                    }
                }
                return;
            }
            #endregion
            //_ReceiveFrequence_1 = 100;
            else
            {
                IsUSBCOM = true;
                byte[] QueryBuffer = new byte[11];
                byte[] QueryBuffer_JiaSuDu = new byte[13];
                byte[] JaSuDu = new byte[11];
                byte[] QueryBuffer_CiGanYing_2 = new byte[12];
                byte[] ReceiveTwoDatas = new byte[8];
                byte[] ReceiveFourDatas = new byte[14];
                byte[] QueryBuffer_Guang_4 = new byte[14];
                byte[] voice = new byte[64];
                byte[] WireLess = new byte[50];

                List<byte> WLQueryBuffer = new List<byte>();


                #region 长度50字节传感器
                if (e.reportBuff.Length == 50)
                {
                    buffer1 = e.reportBuff;
                    QueryBuffer[0] = buffer1[0];
                    QueryBuffer[1] = buffer1[1];
                    for (int i = 2; i < 8; i++)
                    {
                        QueryBuffer[i] = 0;
                    }
                    byte x, y;
                    bool youwu = false;
                    Sensor1 = buffer1[1];
                    _lastRefresh1 = DateTime.Now;
                    #region 光电门 7
                    if (Sensor1 == 7)//光电门；
                    {
                        int index = 0;
                        if ((buffer1.Length == 50) && (buffer1[0] == 0xa5))
                        {
                            if (_portCollection.PlugInPortIndexes.Contains(index))
                            {
                                if (Started)
                                {
                                    #region old Rule
                                    //for (int i = 44; i <= buffer1.Length - 5; i += 5)
                                    //{
                                    //    byte side = buffer1[i];//保留光电门 5位小数点

                                    //    float value = UsbByteToGate(new byte[] { buffer1[i + 2], buffer1[i + 3], buffer1[i + 4], buffer1[i + 5] },_portCollection.Ports[index].K, _portCollection.Ports[index].B);



                                    //if (side == 0x01 && UpSideValue45 != buffer1[45])
                                    //{
                                    //    //if (Photo == false)
                                    //    //{
                                    //    //    Photo = true;
                                    //    //    continue;
                                    //    //}                                        
                                    //    _portCollection.Ports[index].DownSideValue = value;
                                    //    value = value - LastValue;
                                    //    UpSideValue45 = buffer1[45];
                                    //    if (value >= 0)
                                    //    {
                                    //        ValueEventArgs eee = new ValueEventArgs(value, index);
                                    //        OnValueChanged(eee);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    LastValue = value;
                                    //    UpSideValue45 = buffer1[45];
                                    //}


                                    //}
                                    #endregion

                                    #region New 20190414

                                    List<float> valueListTemp = new List<float>();
                                    for (int i = 6; i <= buffer1.Length - 4; i += 8)
                                    {
                                        byte side = buffer1[i];//保留光电门 5位小数点
                                        float value = UsbByteToGate(new byte[] { buffer1[i], buffer1[i + 1], buffer1[i + 2], buffer1[i + 3] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);

                                        if (value != 0)
                                        {
                                            valueListTemp.Add(value);
                                        }
                                    }
                                    for (int indx = 0; indx < valueListTemp.Count; indx++)
                                    {
                                        if (valueList.Contains(valueListTemp[indx]))//contains the value
                                        {
                                            int a = valueList.IndexOf(valueListTemp[indx]);
                                            if (valueList.IndexOf(valueListTemp[indx]) >= indx)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                if (GuangDianMen1FirstValue)
                                                {
                                                    //GDMFirstTime = DateTime.Now;
                                                    GuangDianMen1FirstValue = false;
                                                }
                                                else
                                                {
                                                    _portCollection.Ports[index].DownSideValue = GetGMUseGDM();
                                                }
                                                ValueEventArgs eee = new ValueEventArgs(valueListTemp[indx], index);
                                                OnValueChanged(eee);

                                            }
                                        }
                                        else // do not contains 
                                        {
                                            if (GuangDianMen1FirstValue)
                                            {
                                                //GDMFirstTime = DateTime.Now;
                                                GuangDianMen1FirstValue = false;
                                            }
                                            else
                                            {
                                                _portCollection.Ports[index].DownSideValue = GetGMUseGDM();
                                            }
                                            ValueEventArgs eee = new ValueEventArgs(valueListTemp[indx], index);
                                            OnValueChanged(eee);
                                        }
                                    }
                                    valueList = valueListTemp;
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                    #region 无线接收
                    else if (Sensor1 == 100)
                    {
                        WireLess = e.reportBuff;
                        _lastRefresh1 = DateTime.Now;
                        if (WireLess[2] != 0x00 && WireLess[2] != 0xff)//无线接收1
                        {
                            WLQueryBuffer.Clear();
                            WLQueryBuffer.Add(0xa5);
                            //WLQueryBuffer.Add(8);
                            for (int i = 2; i <= 11; i++)
                            {
                                WLQueryBuffer.Add(WireLess[i]);
                                Sensor1 = WireLess[2];
                            }//查询传感器 1 ID

                            if (WireLess[8] != 0x00 && WireLess[8] != 0xff)//无线接收2
                            {
                                WLQueryBuffer.Clear();
                                WLQueryBuffer.Add(0xa5);
                                WLQueryBuffer.Add(WireLess[2]);
                                for (int i = 8; i <= 17; i++)
                                {
                                    WLQueryBuffer.Add(WireLess[i]);
                                }//查询传感器 2 ID
                                if (WireLess[14] != 0x00 && WireLess[14] != 0xff)//无线接收3
                                {
                                    WLQueryBuffer.Clear();
                                    WLQueryBuffer.Add(0xa5);
                                    WLQueryBuffer.Add(WireLess[2]);
                                    WLQueryBuffer.Add(WireLess[8]);
                                    for (int i = 14; i <= 23; i++)
                                    {
                                        WLQueryBuffer.Add(WireLess[i]);
                                    }//查询传感器 3 ID
                                    if (WireLess[20] != 0x00 && WireLess[20] != 0xff)//无线接收4
                                    {
                                        WLQueryBuffer.Clear();
                                        WLQueryBuffer.Add(0xa5);
                                        WLQueryBuffer.Add(WireLess[2]);
                                        WLQueryBuffer.Add(WireLess[8]);
                                        WLQueryBuffer.Add(WireLess[14]);
                                        for (int i = 20; i <= 29; i++)
                                        {
                                            WLQueryBuffer.Add(WireLess[i]);
                                        }//查询传感器 4 ID
                                        if (WireLess[26] != 0x00 && WireLess[26] != 0xff)//无线接收5
                                        {
                                            WLQueryBuffer.Clear();
                                            WLQueryBuffer.Add(0xa5);
                                            WLQueryBuffer.Add(WireLess[2]);
                                            WLQueryBuffer.Add(WireLess[8]);
                                            WLQueryBuffer.Add(WireLess[14]);
                                            WLQueryBuffer.Add(WireLess[20]);
                                            for (int i = 26; i <= 35; i++)
                                            {
                                                WLQueryBuffer.Add(WireLess[i]);
                                            }//查询传感器 5 ID
                                            if (WireLess[32] != 0x00 && WireLess[32] != 0xff)//无线接收6
                                            {
                                                WLQueryBuffer.Clear();
                                                WLQueryBuffer.Add(0xa5);
                                                WLQueryBuffer.Add(WireLess[2]);
                                                WLQueryBuffer.Add(WireLess[8]);
                                                WLQueryBuffer.Add(WireLess[14]);
                                                WLQueryBuffer.Add(WireLess[20]);
                                                WLQueryBuffer.Add(WireLess[26]);
                                                for (int i = 32; i <= 41; i++)
                                                {
                                                    WLQueryBuffer.Add(WireLess[i]);
                                                }//查询传感器 6 ID
                                                if (WireLess[38] != 0x00 && WireLess[38] != 0xff)//无线接收7
                                                {
                                                    WLQueryBuffer.Clear();
                                                    WLQueryBuffer.Add(0xa5);
                                                    WLQueryBuffer.Add(WireLess[2]);
                                                    WLQueryBuffer.Add(WireLess[8]);
                                                    WLQueryBuffer.Add(WireLess[14]);
                                                    WLQueryBuffer.Add(WireLess[20]);
                                                    WLQueryBuffer.Add(WireLess[26]);
                                                    WLQueryBuffer.Add(WireLess[32]);
                                                    for (int i = 38; i <= 47; i++)
                                                    {
                                                        WLQueryBuffer.Add(WireLess[i]);
                                                    }//查询传感器 7 ID
                                                    if (WireLess[44] != 0x00 && WireLess[44] != 0xff)//无线接收7
                                                    {
                                                        WLQueryBuffer.Clear();
                                                        WLQueryBuffer.Add(0xa5);
                                                        WLQueryBuffer.Add(WireLess[2]);
                                                        WLQueryBuffer.Add(WireLess[8]);
                                                        WLQueryBuffer.Add(WireLess[14]);
                                                        WLQueryBuffer.Add(WireLess[20]);
                                                        WLQueryBuffer.Add(WireLess[26]);
                                                        WLQueryBuffer.Add(WireLess[32]);
                                                        WLQueryBuffer.Add(WireLess[38]);
                                                        for (int i = 44; i <= 49; i++)
                                                        {
                                                            WLQueryBuffer.Add(WireLess[i]);
                                                        }//查询传感器 8 ID
                                                        for (int j = 50; j <= 53; j++)
                                                        {
                                                            WLQueryBuffer.Add(0x00);
                                                        }//查询传感器 7 ID
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (Started)
                            {


                                if (--_skipPoint_1 == 0)
                                {
                                    _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                                    if (WLQueryBuffer.Count >= 11)
                                    {
                                        int index = 0;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[5], WireLess[6], WireLess[7] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 12)
                                    {
                                        int index = 1;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[11], WireLess[12], WireLess[13] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 13)
                                    {
                                        int index = 2;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[17], WireLess[18], WireLess[19] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 14)
                                    {
                                        int index = 3;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[23], WireLess[24], WireLess[25] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    //添加一共8个接收
                                    if (WLQueryBuffer.Count >= 15)
                                    {
                                        int index = 4;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[29], WireLess[30], WireLess[31] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 16)
                                    {
                                        int index = 5;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[35], WireLess[36], WireLess[37] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 17)
                                    {
                                        int index = 6;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[41], WireLess[42], WireLess[43] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 18)
                                    {
                                        int index = 7;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[47], WireLess[48], WireLess[49] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                }


                            }
                            if (Adjusted)
                            {
                                if (--_skipPoint_1 == 0)
                                {
                                    _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                                    if (WLQueryBuffer.Count >= 8)
                                    {
                                        int index = 0;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[5], WireLess[6], WireLess[7] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 9)
                                    {
                                        int index = 1;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[11], WireLess[12], WireLess[13] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 10)
                                    {
                                        int index = 2;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[17], WireLess[18], WireLess[19] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 11)
                                    {
                                        int index = 3;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[23], WireLess[24], WireLess[25] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                }
                            }
                        }
                        if ((_noExist2 && !QueryUsbIsOk) || Sensor2 == 100)
                        {
                            UnpackQueryTypeUSB(WLQueryBuffer.ToArray());
                        }
                        return;
                    }
                    #endregion
                    #region 温湿度一体
                    else if (Sensor1 == 62)//温湿度一体
                    {
                        _lastRefresh1 = DateTime.Now;
                        byte[] QueryBuffer_WenDu = new byte[12];
                        QueryBuffer_WenDu[0] = 0xa5;
                        QueryBuffer_WenDu[1] = 62;
                        QueryBuffer_WenDu[2] = 63;

                        if (Started)
                        {
                            //if (Frequence <= 10)
                            //{
                            //    _skipPoint_1 = (int)(_ReceiveFrequence_1 / (Frequence + 0.14));
                            //}
                            //else
                            _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                            Count_1 = (int)(USB_1_index * 16 / _skipPoint_1 - Count_Pre_1);
                            if (Count_1 != 0)
                            {
                                int index = 0, index_2 = 1;
                                for (int i = 0; i < Count_1; i++)
                                {
                                    float value = buffer1[3];
                                    float value_2 = buffer1[4];
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                    ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_2);
                                    OnValueChanged(ee_2);
                                }
                            }
                            USB_1_index++;
                            Count_Pre_1 = Count_1 + Count_Pre_1;
                        }

                        if (Adjusted)
                        {
                            int index = 0;
                            if (--_skipPoint_1 == 0)
                            {
                                _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                                float value = ByteToFloatUSB(new byte[] { buffer1[2], buffer1[3], buffer1[4] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                            }
                        }
                        UnpackQueryTypeUSB(QueryBuffer_WenDu);
                        return;
                    }
                    #endregion
                    #region  普通传感器
                    else
                    {
                        if ((buffer1.Length == 50) && (buffer1[0] == 0xa5))//普通传感器
                        {
                            if (Started)
                            {
                                int index = 0;
                                var datas = Receivedata(buffer1, 2, 3, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                for (double i = readDataIndex; i < datas.Count; i += BleSkipPoint)
                                {
                                    int readIndex = (int)i;
                                    ValueEventArgs ee = new ValueEventArgs(datas[readIndex], index);
                                    OnValueChanged(ee);

                                    readDataIndex += BleSkipPoint;
                                }
                                readDataIndex -= datas.Count;
                            }

                            if (Adjusted)
                            {
                                int index = 0;
                                if (--_skipPoint_1 == 0)
                                {
                                    _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                                    float value = ByteToFloatUSB(new byte[] { buffer1[2], buffer1[3], buffer1[4] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                    ValueEventArgs ee = new ValueEventArgs(value, index);
                                    OnValueChanged(ee);
                                }
                            }

                        }
                    }
                    #endregion
                    //if ((_noExist2 && !QueryUsbIsOk) || Sensor2 == 100)
                    //{
                    //    UnpackQueryTypeUSB(QueryBuffer);
                    //}


                }
                #endregion
                //
                #region 声波 64字节
                else if (e.reportBuff.Length == 64)//声波
                {
                    voice = e.reportBuff;
                    _lastRefresh1 = DateTime.Now;
                    if (voice[0] == 0xa0)
                    {
                        if (Started)
                        {
                            int index = 0;
                            for (int i = 1; i <= 31; i++)
                            {
                                float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                            }
                            //for (int i = 2; i <= 63; i++)
                            //{
                            //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                            //    OnValueChanged(ee);
                            //}
                        }
                        //QueryBuffer[0] = 0xa5;
                        //QueryBuffer[1] = 8;
                        //for (int i = 2; i < 8; i++)
                        //{
                        //    QueryBuffer[i] = 0;
                        //}
                        //UnpackQueryTypeUSB(QueryBuffer);
                    }

                }
                #endregion
                #region 声波 22字节
                else if (e.reportBuff.Length == 22)//声波
                {
                    voice = e.reportBuff;
                    _lastRefresh1 = DateTime.Now;
                    if (voice[0] == 0xa0)
                    {
                        if (Started)
                        {
                            int index = 0;
                            for (int i = 1; i <= 10; i++)
                            {
                                float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                            }
                            //for (int i = 2; i <= 63; i++)
                            //{
                            //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                            //    OnValueChanged(ee);
                            //}
                        }
                        QueryBuffer[0] = 0xa5;
                        QueryBuffer[1] = 8;
                        Sensor1 = 8;//声波编号
                        for (int i = 2; i < 8; i++)
                        {
                            QueryBuffer[i] = 0;
                        }
                        //UnpackQueryTypeUSB(QueryBuffer);
                        //if ((_noExist2 && !QueryUsbIsOk) || Sensor2 == 100)
                        //{
                        //    UnpackQueryTypeUSB(QueryBuffer);
                        //}
                    }

                }
                #endregion
                #region 声波声强一体 23字节
                else if (e.reportBuff.Length == 23)//声波声强一体  20160716
                {
                    voice = e.reportBuff;
                    _lastRefresh1 = DateTime.Now;
                    if (voice[0] == 0xa0)
                    {
                        if (Started)
                        {
                            int index = 0;
                            for (int i = 1; i <= 10; i++)
                            {
                                float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                            }
                            //for (int i = 2; i <= 63; i++)
                            //{
                            //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                            //    OnValueChanged(ee);
                            //}
                            //if (skip == (int)(1000/Frequence))
                            if (skip >= 1000)
                            {
                                if (voice[22] > 30 && voice[22] < 130)
                                {
                                    ValueEventArgs ee1 = new ValueEventArgs(voice[22], 1);
                                    OnValueChanged(ee1);
                                    skip = 0;
                                }

                            }
                            else
                            {
                                skip++;
                            }
                        }
                        QueryBuffer_CiGanYing_2[0] = 0xa5;
                        QueryBuffer_CiGanYing_2[1] = voice[1];
                        QueryBuffer_CiGanYing_2[2] = 20;
                        for (int i = 3; i < 9; i++)
                        {
                            QueryBuffer_CiGanYing_2[i] = 0;
                        }
                        //UnpackQueryTypeUSB(QueryBuffer_CiGanYing_2);
                    }

                }
                #endregion
                #region 分成3个传感器  11字节
                else if (e.reportBuff.Length == 11)
                {
                    JaSuDu = e.reportBuff;
                    _lastRefresh1 = DateTime.Now;
                    if (JaSuDu[0] == 0xa5)
                    {
                        QueryBuffer_JiaSuDu[0] = JaSuDu[0];
                        switch (JaSuDu[1])
                        {
                            case 50://加速度
                                {
                                    QueryBuffer_JiaSuDu[1] = 50;
                                    QueryBuffer_JiaSuDu[2] = 51;
                                    QueryBuffer_JiaSuDu[3] = 52;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1, index_2 = 2;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { JaSuDu[2], JaSuDu[3], JaSuDu[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { JaSuDu[5], JaSuDu[6], JaSuDu[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);
                                            float value_3 = ByteToFloatUSB(new byte[] { JaSuDu[8], JaSuDu[9], JaSuDu[10] }, _portCollection.Ports[index_2].K, _portCollection.Ports[index_2].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);
                                            ValueEventArgs ee_3 = new ValueEventArgs(value_3, index_2);
                                            OnValueChanged(ee_3);
                                        }
                                    }
                                }
                                break;


                            default:
                                break;
                        }

                    }
                    //UnpackQueryTypeUSB(QueryBuffer_JiaSuDu);
                }
                #endregion
                #region 分成2个传感器  8字节
                else if (e.reportBuff.Length == 8)
                {
                    ReceiveTwoDatas = e.reportBuff;
                    _lastRefresh1 = DateTime.Now;
                    if (ReceiveTwoDatas[0] == 0xa5)
                    {
                        QueryBuffer_CiGanYing_2[0] = ReceiveTwoDatas[0];
                        switch (ReceiveTwoDatas[1])
                        {
                            case 97://
                                {
                                    QueryBuffer_CiGanYing_2[1] = 97;
                                    QueryBuffer_CiGanYing_2[2] = 98;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);

                                        }
                                    }
                                }
                                break;
                            case 95://
                                {
                                    QueryBuffer_CiGanYing_2[1] = 95;
                                    QueryBuffer_CiGanYing_2[2] = 96;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);

                                        }
                                    }
                                }
                                break;

                            case 93://
                                {
                                    QueryBuffer_CiGanYing_2[1] = 93;
                                    QueryBuffer_CiGanYing_2[2] = 94;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);

                                        }
                                    }
                                }
                                break;
                            case 101://大气压强温度
                                {
                                    QueryBuffer_CiGanYing_2[1] = 101;
                                    QueryBuffer_CiGanYing_2[2] = 102;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);

                                        }
                                    }
                                }
                                break;
                            case 103://距离 时间
                                {
                                    QueryBuffer_CiGanYing_2[1] = 103;
                                    QueryBuffer_CiGanYing_2[2] = 104;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);

                                        }
                                    }
                                }
                                break;
                            case 105://力 倾角
                                {
                                    QueryBuffer_CiGanYing_2[1] = 105;
                                    QueryBuffer_CiGanYing_2[2] = 106;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);

                                        }
                                    }
                                }
                                break;
                            case 111://挡光时间，飞行时间
                                {
                                    QueryBuffer_CiGanYing_2[1] = 111;
                                    QueryBuffer_CiGanYing_2[2] = 112;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);

                                        }
                                    }
                                }
                                break;
                            case 160://声音
                                {
                                    QueryBuffer_CiGanYing_2[1] = 160;
                                    QueryBuffer_CiGanYing_2[2] = 161;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[2], ReceiveTwoDatas[3], ReceiveTwoDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { ReceiveTwoDatas[5], ReceiveTwoDatas[6], ReceiveTwoDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);

                                        }
                                    }
                                }
                                break;

                            default:
                                break;
                        }

                    }
                    //UnpackQueryTypeUSB(QueryBuffer_CiGanYing_2);
                }
                #endregion
                #region 分成 4个传感器 14字节
                else if (e.reportBuff.Length == 14)
                {
                    ReceiveFourDatas = e.reportBuff;
                    _lastRefresh1 = DateTime.Now;
                    if (ReceiveFourDatas[0] == 0xa5)
                    {
                        QueryBuffer_Guang_4[0] = ReceiveFourDatas[0];
                        switch (ReceiveFourDatas[1])
                        {
                            case 180://
                                {
                                    QueryBuffer_Guang_4[1] = 180;
                                    QueryBuffer_Guang_4[2] = 181;
                                    QueryBuffer_Guang_4[3] = 182;
                                    QueryBuffer_Guang_4[4] = 183;
                                    if (Started)
                                    {
                                        int index_0 = 0, index_1 = 1;
                                        int index_2 = 2, index_3 = 3;
                                        if (--_skipPoint_JiaSuDu == 0)
                                        {
                                            _skipPoint_JiaSuDu = (int)_ReceiveFrequence_JiaSuDu / Frequence;
                                            float value_1 = ByteToFloatUSB(new byte[] { ReceiveFourDatas[2], ReceiveFourDatas[3], ReceiveFourDatas[4] }, _portCollection.Ports[index_0].K, _portCollection.Ports[index_0].B);
                                            float value_2 = ByteToFloatUSB(new byte[] { ReceiveFourDatas[5], ReceiveFourDatas[6], ReceiveFourDatas[7] }, _portCollection.Ports[index_1].K, _portCollection.Ports[index_1].B);
                                            float value_3 = ByteToFloatUSB(new byte[] { ReceiveFourDatas[8], ReceiveFourDatas[9], ReceiveFourDatas[10] }, _portCollection.Ports[index_2].K, _portCollection.Ports[index_2].B);
                                            float value_4 = ByteToFloatUSB(new byte[] { ReceiveFourDatas[11], ReceiveFourDatas[12], ReceiveFourDatas[13] }, _portCollection.Ports[index_3].K, _portCollection.Ports[index_3].B);

                                            ValueEventArgs ee = new ValueEventArgs(value_1, index_0);
                                            OnValueChanged(ee);
                                            ValueEventArgs ee_2 = new ValueEventArgs(value_2, index_1);
                                            OnValueChanged(ee_2);
                                            ValueEventArgs ee_3 = new ValueEventArgs(value_3, index_2);
                                            OnValueChanged(ee_3);
                                            ValueEventArgs ee_4 = new ValueEventArgs(value_4, index_3);
                                            OnValueChanged(ee_4);

                                        }
                                    }
                                }
                                break;
                        }
                    }
                    //UnpackQueryTypeUSB(QueryBuffer_Guang_4);
                }
                #endregion
                if (IsUsb_SetOnce < 100)
                {
                    IsUsb_SetOnce++;
                }
            }
        }
        void GetSkipPoint(int bufferDataCount, ref bool record, ref DateTime dt, ref int receiveDataCount, ref double skipPoint, ref double LaseSkip)
        {
            if (record)
            {
                record = false;
                dt = DateTime.Now;
            }
            TimeSpan tf = DateTime.Now - dt;
            receiveDataCount += bufferDataCount;
            if (tf.TotalSeconds < 100)
            {
                skipPoint = Convert.ToDouble(receiveDataCount) / (tf.TotalSeconds * Frequence);
            }
            if (receiveDataCount < 100 && LaseSkip != 0)
            {
                skipPoint = LaseSkip;
            }
            else
            {
                LaseSkip = skipPoint;
            }
        }
        void Ble2_DeceiveValueChanged(object sender, DeceiveDataArgs e)
        {
            GetSkipPoint(1, ref IsRecordBle2, ref Ble2Datatime, ref Ble2DataCount, ref Ble2SkipPoint, ref LastSkipPoint2);

            #region 新协议
            var NewPackage = ParseBuffer.Parse(2, e.reportBuff);
            if (NewPackage.IsNewCommandType)
            {
                IsUSBCOM = true;
                _lastRefresh1 = DateTime.Now;

                if (NewPackage.IsRightPackage)
                {
                    switch (NewPackage.CommandType)
                    {
                        case NewCommandType.CommandType.GetDeviceInfo:
                            byte[] QueryBuffer = new byte[11];//查询传感器
                            //Sensor1 = NewPackage.sensorID;
                            //QueryBuffer[1] = NewPackage.sensorID;

                            if ((_noExist2 && !QueryUsbIsOk) || Sensor2 == 100)
                            {
                                UnpackQueryTypeUSB(QueryBuffer);
                            }
                            break;
                        case NewCommandType.CommandType.StartSendData:
                            if (Started)
                            {
                                int index = 1;
                                ReceiveCount2 += 1;
                                while (true)
                                {
                                    if (ReceiveCount2 >= Ble2SkipPoint)
                                    {
                                        float value = NewPackage.ValueInt * _portCollection.Ports[index].K + _portCollection.Ports[index].B;
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                        ReceiveCount2 -= Ble2SkipPoint;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                            }
                            break;
                        default:
                            break;
                    }
                }
                return;
            }
            #endregion
            //_ReceiveFrequence_1 = 100;
            else
            {

                _ReceiveFrequence_1 = 100;
                IsUSBCOM = true;
                _noExist2 = false;
                byte[] QueryBuffer = new byte[12];
                byte[] buffer = e.reportBuff;
                byte[] voice = new byte[64];

                Sensor2 = buffer[1];

                if (Sensor1 == 0x00)
                    return;
                if (Sensor1 == 100)
                {
                    QueryBuffer = new byte[11];
                    QueryBuffer[1] = buffer[1];
                }
                else
                {
                    QueryBuffer[1] = Sensor1;
                    QueryBuffer[2] = buffer[1];
                }
                QueryBuffer[0] = 0xa5;
                _lastRefresh2 = DateTime.Now;
                for (int i = 3; i < 8; i++)
                {
                    QueryBuffer[i] = 0;
                }
                #region 50字节长的通用 的传感器


                if (buffer.Length == 50 && buffer[0] == 0xa5)
                {
                    #region 光电门 7
                    if (Sensor2 == 7)//光电门；
                    {
                        int index = 1;
                        if (Sensor1 == 100)
                            index = 0;
                        if ((buffer.Length == 50) && (buffer[0] == 0xa5))
                        {
                            if (_portCollection.PlugInPortIndexes.Contains(index))
                            {
                                if (Started)
                                {
                                    //for (int i = 44; i <= buffer.Length - 5; i += 5)
                                    //{
                                    //    byte side = buffer[i];//保留光电门 5位小数点

                                    //    float value = UsbByteToGate(new byte[] { buffer[i + 2], buffer[i + 3], buffer[i + 4], buffer[i + 5] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);

                                    //    if (side == 0x01 && UpSideValue2_45 != buffer[45])
                                    //    {
                                    //        //if (Photo == false)
                                    //        //{
                                    //        //    Photo = true;
                                    //        //    continue;
                                    //        //}                                        
                                    //        _portCollection.Ports[index].DownSideValue = value;
                                    //        value = value - LastValue2;
                                    //        UpSideValue2_45 = buffer[45];
                                    //        if (value >= 0)
                                    //        {
                                    //            ValueEventArgs eee = new ValueEventArgs(value, index);
                                    //            OnValueChanged(eee);
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        LastValue2 = value;
                                    //        UpSideValue2_45 = buffer[45];
                                    //    }
                                    //}
                                    #region New 20190414

                                    List<float> valueListTemp = new List<float>();
                                    for (int i = 6; i <= buffer.Length - 4; i += 8)
                                    {
                                        byte side = buffer1[i];//保留光电门 5位小数点
                                        float value = UsbByteToGate(new byte[] { buffer[i], buffer[i + 1], buffer[i + 2], buffer[i + 3] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);

                                        if (value != 0)
                                        {
                                            valueListTemp.Add(value);
                                        }
                                    }
                                    for (int indx = 0; indx < valueListTemp.Count; indx++)
                                    {
                                        if (valueList2.Contains(valueListTemp[indx]))//contains the value
                                        {
                                            int a = valueList2.IndexOf(valueListTemp[indx]);
                                            if (valueList2.IndexOf(valueListTemp[indx]) >= indx)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                if (GuangDianMen2FirstValue)
                                                {
                                                    // GDMSecondTime = DateTime.Now;
                                                    GuangDianMen2FirstValue = false;
                                                }
                                                else
                                                {
                                                    _portCollection.Ports[index].DownSideValue = GetGMUseGDM();
                                                }
                                                ValueEventArgs eee = new ValueEventArgs(valueListTemp[indx], index);
                                                OnValueChanged(eee);
                                            }
                                        }
                                        else // do not contains 
                                        {
                                            if (GuangDianMen2FirstValue)
                                            {
                                                //GDMSecondTime = DateTime.Now;
                                                GuangDianMen2FirstValue = false;
                                            }
                                            else
                                            {
                                                _portCollection.Ports[index].DownSideValue = GetGMUseGDM();
                                            }
                                            ValueEventArgs eee = new ValueEventArgs(valueListTemp[indx], index);
                                            OnValueChanged(eee);
                                        }
                                    }
                                    valueList2 = valueListTemp;
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                    #region 无线接收
                    else if (Sensor2 == 100)
                    {
                        IsUSBCOM = true;

                        byte[] QueryBuffer_JiaSuDu = new byte[13];
                        byte[] JaSuDu = new byte[11];
                        byte[] QueryBuffer_CiGanYing_2 = new byte[12];
                        byte[] ReceiveTwoDatas = new byte[8];
                        byte[] ReceiveFourDatas = new byte[14];
                        byte[] QueryBuffer_Guang_4 = new byte[14];
                        //byte[] voice = new byte[64];
                        byte[] WireLess = new byte[50];

                        WireLess = e.reportBuff;
                        _lastRefresh1 = DateTime.Now;

                        List<byte> WLQueryBuffer = new List<byte>();

                        if (WireLess[2] != 0x00 && WireLess[2] != 0xff)//无线接收1
                        {
                            WLQueryBuffer.Clear();
                            WLQueryBuffer.Add(0xa5);
                            WLQueryBuffer.Add(Sensor1);
                            //WLQueryBuffer.Add(8);
                            for (int i = 2; i <= 11; i++)
                            {
                                WLQueryBuffer.Add(WireLess[i]);
                                Sensor2 = WireLess[2];
                            }//查询传感器 1 ID

                            if (WireLess[8] != 0x00 && WireLess[8] != 0xff)//无线接收2
                            {
                                WLQueryBuffer.Clear();
                                WLQueryBuffer.Add(0xa5);
                                WLQueryBuffer.Add(Sensor1);
                                WLQueryBuffer.Add(WireLess[2]);
                                for (int i = 8; i <= 17; i++)
                                {
                                    WLQueryBuffer.Add(WireLess[i]);
                                }//查询传感器 2 ID
                                if (WireLess[14] != 0x00 && WireLess[14] != 0xff)//无线接收3
                                {
                                    WLQueryBuffer.Clear();
                                    WLQueryBuffer.Add(0xa5);
                                    WLQueryBuffer.Add(Sensor1);
                                    WLQueryBuffer.Add(WireLess[2]);
                                    WLQueryBuffer.Add(WireLess[8]);
                                    for (int i = 14; i <= 23; i++)
                                    {
                                        WLQueryBuffer.Add(WireLess[i]);
                                    }//查询传感器 3 ID
                                    if (WireLess[20] != 0x00 && WireLess[20] != 0xff)//无线接收1
                                    {
                                        WLQueryBuffer.Clear();
                                        WLQueryBuffer.Add(0xa5);
                                        WLQueryBuffer.Add(Sensor1);
                                        WLQueryBuffer.Add(WireLess[2]);
                                        WLQueryBuffer.Add(WireLess[8]);
                                        WLQueryBuffer.Add(WireLess[14]);
                                        for (int i = 20; i <= 28; i++)
                                        {
                                            WLQueryBuffer.Add(WireLess[i]);
                                        }//查询传感器 4 ID
                                        WLQueryBuffer.Add(0x00);
                                    }
                                }
                            }
                            if (Started)
                            {

                                if (--_skipPoint_wuxian == 0)
                                {
                                    _skipPoint_wuxian = (int)_ReceiveFrequence_1 / Frequence;

                                    if (WLQueryBuffer.Count >= 12)
                                    {
                                        int index = 1;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[5], WireLess[6], WireLess[7] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 13)
                                    {
                                        int index = 2;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[11], WireLess[12], WireLess[13] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 14)
                                    {
                                        int index = 3;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[17], WireLess[18], WireLess[19] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 15)
                                    {
                                        int index = 4;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[23], WireLess[24], WireLess[25] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                }

                            }
                            if (Adjusted)
                            {
                                if (--_skipPoint_1 == 0)
                                {
                                    _skipPoint_1 = (int)_ReceiveFrequence_1 / Frequence;
                                    if (WLQueryBuffer.Count >= 8)
                                    {
                                        int index = 0;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[5], WireLess[6], WireLess[7] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 9)
                                    {
                                        int index = 1;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[11], WireLess[12], WireLess[13] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 10)
                                    {
                                        int index = 2;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[17], WireLess[18], WireLess[19] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                    if (WLQueryBuffer.Count >= 11)
                                    {
                                        int index = 3;
                                        float value = ByteToFloatUSB(new byte[] { WireLess[23], WireLess[24], WireLess[25] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                        ValueEventArgs ee = new ValueEventArgs(value, index);
                                        OnValueChanged(ee);
                                    }
                                }
                            }
                        }
                        if (_noExist3 && Sensor2 != 100)
                            UnpackQueryTypeUSB(WLQueryBuffer.ToArray());
                        return;
                    }
                    #endregion
                    else
                    {
                        if (Started)
                        {
                            int index = 1;
                            var datas = Receivedata(buffer, 2, 3, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                            for (double i = readDataIndex2; i < datas.Count; i += Ble2SkipPoint)
                            {
                                int readIndex = (int)i;
                                ValueEventArgs ee = new ValueEventArgs(datas[readIndex], index);
                                OnValueChanged(ee);

                                readDataIndex2 += Ble2SkipPoint;
                            }
                            readDataIndex2 -= datas.Count;
                        }
                        if (Adjusted)
                        {
                            int index = 1;
                            if (--_skipPoint_2 == 0)
                            {
                                _skipPoint_2 = (int)_ReceiveFrequence_1 / Frequence;
                                float value = ByteToFloatUSB(new byte[] { buffer[9], buffer[10], buffer[11] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                            }
                        }
                    }


                }
                #endregion
                #region 声波 22字节
                else if (buffer.Length == 22)//声波
                {
                    voice = e.reportBuff;
                    _lastRefresh1 = DateTime.Now;
                    Sensor2 = 8;//声波编号
                    if (voice[0] == 0xa0)
                    {
                        if (Started)
                        {
                            int index = 1;
                            for (int i = 1; i <= 10; i++)
                            {
                                float value = ByteToFloatUSBVoice(new byte[] { voice[2 * i], voice[2 * i + 1] }, _portCollection.Ports[index].K, _portCollection.Ports[index].B);
                                ValueEventArgs ee = new ValueEventArgs(value, index);
                                OnValueChanged(ee);
                            }
                            //for (int i = 2; i <= 63; i++)
                            //{
                            //    ValueEventArgs ee = new ValueEventArgs(voice[i], index);
                            //    OnValueChanged(ee);
                            //}
                        }
                        //QueryBuffer[0] = 0xa5;
                        QueryBuffer[2] = 8;
                        //for (int i = 2; i < 8; i++)
                        //{
                        //    QueryBuffer[i] = 0;
                        //}
                        //UnpackQueryTypeUSB(QueryBuffer);
                    }

                }
                #endregion
                //ConnectEventArgs a = new ConnectEventArgs(true);//连接问题
                //OnConnectedChanged(a);
                //if (_noExist3)
                //{
                //    UnpackQueryTypeUSB(QueryBuffer);
                //}

            }
        }
        private void Ble1_BleDeleteChanged(object sender, BleDeleteArgs e)
        {
            lock (sensorBuffer)
            {
                SenserMsg sm = (from o in sensorBuffer where o.index == e.SensorInfo.index && o.connectName.Contains("ble") select o).ToList().FirstOrDefault();
                if (sm != null)
                {
                    sensorBuffer.Remove(sm);
                    btListHistory.RemoveAll(o => o.MAC == sm.bluetooth.MAC);
                    bluetoothList.RemoveAll(o => o.MAC == sm.bluetooth.MAC);
                }

                BluetoothConnect(btListHistory);
            }
        }

        private void AddSensorDataChanged(object sender, SenserMsg pSM)
        {
            lock (sensorBuffer)
            {
                //SenserMsg sm = (from o in sensorBuffer where o.sensorID == pSM.sensorID && o.connectName == pSM.connectName &&o.bluetooth.MAC ==pSM.bluetooth .MAC  select o).ToList().FirstOrDefault();
                SenserMsg sm = (from o in sensorBuffer where o.bluetooth.Adresse == pSM.bluetooth.Adresse && o.bluetooth.MAC == pSM.bluetooth.MAC select o).ToList().FirstOrDefault();
                if (sm == null)
                {
                    sm = new SenserMsg();
                    int aaaas = sensorBuffer.Count;
                    //byte count = Convert.ToByte(DataSourceNew.sensorBuffer.Count());

                    sm.index = pSM.index;
                    sm.sensorID = pSM.sensorID;
                    sm.connectName = pSM.connectName;
                    sm.Power = pSM.Power;
                    sm.bluetooth = pSM.bluetooth;
                    sensorBuffer.Add(sm);
                    sensorBuffer = sensorBuffer.OrderBy(o => o.index).ToList();
                }
            }
        }
        private void AddSensorDataChanged2(object sender, SenserMsg pSM)
        {
            lock (sensorBuffer)
            {
                //SenserMsg sm = (from o in sensorBuffer where o.sensorID == pSM.sensorID && o.connectName == pSM.connectName &&o.bluetooth.MAC ==pSM.bluetooth .MAC  select o).ToList().FirstOrDefault();
                SenserMsg sm = (from o in sensorBuffer where o.bluetooth.Adresse == pSM.bluetooth.Adresse && o.bluetooth.MAC == pSM.bluetooth.MAC select o).ToList().FirstOrDefault();
                if (sm == null)
                {
                    sm = new SenserMsg();
                    int aaaas = sensorBuffer.Count;
                    //byte count = Convert.ToByte(DataSourceNew.sensorBuffer.Count());

                    sm.index = pSM.index;
                    sm.sensorID = pSM.sensorID;
                    sm.connectName = pSM.connectName;
                    sm.Power = pSM.Power;
                    sm.bluetooth = pSM.bluetooth;
                    sensorBuffer.Add(sm);
                    sensorBuffer = sensorBuffer.OrderBy(o => o.index).ToList();
                }
            }
        }


        private void Ble1_BleWarchingChanged(object sender, BleWarchingArgs e)
        {
            IsAndWarchInfo(e.btInfo);
        }

        void IsAndWarchInfo(BluetoothInfo bt)
        {
            BluetoothInfo btInfo = (from o in bluetoothList where o.Adresse == bt.Adresse && o.MAC == bt.MAC select o).ToList().FirstOrDefault();
            if (btInfo == null)
            {
                btInfo = new BluetoothInfo();
                btInfo.Adresse = bt.Adresse;
                btInfo.MAC = bt.MAC;
                bluetoothList.Add(btInfo);
            }
        }

        void BLEDispose()
        {
            foreach (var item in sensorBuffer)
            {
                switch (item.connectName)
                {
                    case "ble1":
                        ble1.DisposeBLE();
                        break;
                    case "ble2":
                        ble2.bluetooth.Dispose();
                        break;
                    case "ble3":
                        ble3.bluetooth.Dispose();
                        break;
                    case "ble4":
                        ble4.bluetooth.Dispose();
                        break;
                }
            }
            //ble1 = null;
            //ble2 = null;
            //ble3 = null;
            //ble4 = null;
        }

        public int StillWaitTime()
        {
            return 30 - runningTime;
        }

        #endregion


    }

    public delegate SensorTypeDefine Method_GetSensorType(int portIndex, ref float k, ref float b);

    public class SenserMsg
    {
        public byte index;
        public byte sensorID;
        public string connectName;
        public float Power;
        public BluetoothInfo bluetooth;
    }



    public class CalculateSkipCount
    {
        public int bufferDataCount = 1;//每个数据包的数据个数
        public bool IsRecord = true;
        public DateTime StartDatatime;//开始记录数据时刻
        public int DataCount = 0;//接收总数据
        public int Frequency = 10;
        public double SkipPoint = 0;
        static double LastSkipPoint1 = 0;

        //GetSkipPoint(1, ref IsRecordBle1, ref Ble1Datatime, ref Ble1DataCount, ref BleSkipPoint, ref LastSkipPoint1);
        public void GetSkipPoint()
        {
            if (IsRecord)
            {
                IsRecord = false;
                StartDatatime = DateTime.Now;
                return;
            }
            TimeSpan tf = DateTime.Now - StartDatatime;
            DataCount += bufferDataCount;
            if (tf.TotalSeconds < 100)
            {
                SkipPoint = Convert.ToDouble(DataCount) / (tf.TotalSeconds * Frequency);//每几个数据取  skippoint=1时  全取
            }
        }
    }

}
