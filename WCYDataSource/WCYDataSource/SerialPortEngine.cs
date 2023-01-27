using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WCYDataSource
{
    public class SerialPortEngine
    {
        public SerialPortEngine(byte[] connectCommand)
        {
            string fileName = "SerialPortSet.txt";//Blue
            System.IO.StreamReader reader = System.IO.File.OpenText(fileName);
            Com = reader.ReadLine();
            //BandRate = Convert.ToInt32(reader.ReadLine());
            BandRate = 1382400;
            if (BandRate == 115200)
                IsBlueCom = true;
            else
                IsBlueCom = false;

            InitialSerialPort();
            _connectCommand = connectCommand;
            _dataCatch = new DataCatch();
            _serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(_serialPort_DataReceived);
            _thread = new Thread(Looping);
            _thread.Start();

        }
        public SerialPortEngine(bool Is6p6c)
        {
            _is6P6C = Is6p6c;
        }

        void _serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            _lastRefresh = DateTime.Now;
            switch (_status)
            {
                case SerialPortStatusDefine.Connecting:
                    ClearCatch();
                    _status = SerialPortStatusDefine.Connected;
                    ReceivingData();
                    ConnectEventArgs a = new ConnectEventArgs(true);
                    OnConnectedChanged(a);
                    break;
                case SerialPortStatusDefine.Connected:
                    ReceivingData();
                    break;
                default:
                    break;

            }

        }
        #region Props
        static int WAIT_CONNECTTIME = 200;
        static TimeSpan CONNECT_SPAN = new TimeSpan(0, 0, 0, 0, 400);
        static TimeSpan LOST_CONNECT_SPAN = new TimeSpan(0, 0, 0, 1);
        DateTime _lastRefresh = DateTime.Now;
        System.IO.Ports.SerialPort _serialPort;
        DataCatch _dataCatch;
        byte[] _connectCommand;

        string _com = "COM2";//可设端口和波特率
        int _baudRate = 1382400;
        bool _isBuleCom;
        public static bool _is6P6C;

        public string Com
        {
            get { return _com; }
            set { _com = value; }
        }
        public int BandRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }
        public bool IsBlueCom
        {
            get { return _isBuleCom; }
            set { _isBuleCom = value; }
        }

        public DataCatch DataCatch
        {
            get { return _dataCatch; }
        }
        bool _looping = true;
        Thread _thread;
        SerialPortStatusDefine _status;
        bool _working = false;
        public bool Working
        {
            get { return _working; }
            set { _working = value; }
        }
        public bool Connected
        {
            get { return _status == SerialPortStatusDefine.Connected; }
        }
        bool NeedTryConnected
        {
            get
            {
                return _status == SerialPortStatusDefine.Connected && !_working && DateTime.Now - _lastRefresh >= CONNECT_SPAN;
            }
        }
        bool LostConnect
        {
            get
            {
                return _status == SerialPortStatusDefine.Connected && !_working && DateTime.Now - _lastRefresh > LOST_CONNECT_SPAN;
            }
        }
        #endregion

        #region Methods
        void InitialSerialPort()
        {
            this._serialPort = new System.IO.Ports.SerialPort();
            _serialPort.BaudRate = 1382400;
            _serialPort.Encoding = Encoding.ASCII;
            _serialPort.ReadTimeout = 100;
            _serialPort.WriteTimeout = 100;
        }

        public void Dispose()
        {
            _status = SerialPortStatusDefine.Clossing;
            if (_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Close();
                }
                catch 
                {
                    
                }
            }
            _looping = false;
        }
        void Looping()
        {
            while (_looping)
            {
                if (_status == SerialPortStatusDefine.Initialize)
                {
                    Connecting();
                }
                else if (LostConnect)
                {
                    _status = SerialPortStatusDefine.Initialize;
                    ConnectEventArgs e = new ConnectEventArgs(false);
                    OnConnectedChanged(e);

                }
                else if (NeedTryConnected)
                {
                    Send(_connectCommand);
                }
                Thread.Sleep(WAIT_CONNECTTIME);
            }
        }
        void ClearCatch()
        {
            _dataCatch.ClearTempData();
        }
        void Connecting()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            _status = SerialPortStatusDefine.Connecting;
            //if (IsBlueCom == false)
            {
                if (_is6P6C == true)//6p6c
                {
                    string[] coms = System.IO.Ports.SerialPort.GetPortNames();
                    if (coms.Length <= 0)
                    {
                        _status = SerialPortStatusDefine.Initialize;
                    }
                    else
                    {
                        for (int i = 0; i < coms.Length; i++)
                        {
                            if (!_serialPort.IsOpen)
                            {
                                _serialPort.PortName = coms[i];
                                _serialPort.BaudRate = BandRate;
                                try
                                {
                                    _serialPort.Open();
                                }
                                catch
                                {
                                }
                            }
                            if (_serialPort.IsOpen)
                            {
                                Send(_connectCommand);

                                Thread.Sleep(WAIT_CONNECTTIME);
                                if (_status == SerialPortStatusDefine.Connected)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            //else
            //{
            //    //if (Com == "")
            //    //{
            //    //    _status = SerialPortStatusDefine.Initialize;
            //    //}
            //    else
            //    {
            //        if (!_serialPort.IsOpen)
            //        {
            //            _serialPort.PortName = Com;
            //            _serialPort.BaudRate = BandRate;
            //            try
            //            {
            //                _serialPort.Open();
            //            }
            //            catch
            //            {
            //            }
            //        }
            //        if (_serialPort.IsOpen)
            //        {
            //            Send(_connectCommand);

            //            Thread.Sleep(WAIT_CONNECTTIME);
            //            if (_status == SerialPortStatusDefine.Connected)
            //            {
            //                return;
            //            }
            //        }

            //    }
            //}
        }
        void ReceivingData()
        {
            byte[] tempData;
            int dataLength = this._serialPort.BytesToRead;
            if (dataLength > 0)
            {
                tempData = new byte[dataLength];
            }
            else
            {
                return;
            }
            try
            {
                this._serialPort.BaseStream.Read(tempData, 0, tempData.Length);
            }
            catch
            {
                
            }

            Add(tempData);

        }
        void Add(byte[] data)
        {
            _dataCatch.Add(data, data.Length);
        }
        public void Send(byte[] command)
        {
            if (this._serialPort.IsOpen)
            {
                try
                {

                    this._serialPort.Write(command, 0, command.Length);
                }
                catch 
                {
                }
            }
        }

        #endregion
        
        #region Events
        public event ConnectedHandler ConnectChanged = null;
        protected void OnConnectedChanged(ConnectEventArgs e)
        {
            if (ConnectChanged != null)
            {
                ConnectChanged(this, e);
            }
        }
        #endregion
    }
}
