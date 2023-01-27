using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Resources;

namespace WCYDisLab.Classes
{
    public class DataEngine
    {
        public DataEngine()
        {
            //Thread.Sleep(500);
            LoadDefault();
            _dataSource.ConnectChanged += new WCYDataSource.ConnectedHandler(_dataSource_ConnectChanged);
            _dataSource.PortChanged += new WCYDataSource.PortCollectionHandler(_dataSource_PortChanged);
            _dataSource.ShiftChanged += new WCYDataSource.ShiftHandler(_dataSource_ShiftChanged);
            _dataSource.ValueChanged += new WCYDataSource.ValueHandler(_dataSource_ValueChanged);
            _dataSource.WorkStatusChanged += new WCYDataSource.StartStopHandler(_dataSource_WorkStatusChanged);
            _dataManager.ConstantEvent += new TDataManager.ConstantHandler(_dataManager_ConstantEvent);
            _dataManager.DataCollectionEvent += new TDataManager.DataCollectionHandler(_dataManager_DataCollectionEvent);
            _dataManager.ExprEvent += new TDataManager.DataStoreEventHandler(_dataManager_ExprEvent);
            _dataManager.OffLineEvent += new TDataManager.OffLineHandler(_dataManager_OffLineEvent);
            _dataManager.ReplayEvent += new TDataManager.ReplayHandler(_dataManager_ReplayEvent);
            _dataManager.SectionEvent += new TDataManager.SectionEventHandler(_dataManager_SectionEvent);
            _dataManager.SensorEvent += new TDataManager.DataStoreEventHandler(_dataManager_SensorEvent);



        }

        void _dataManager_SensorEvent(object sender, TDataManager.DataStoreEventArgs e)
        {

        }

        void _dataManager_SectionEvent(object sender, TDataManager.SectionEventArgs e)
        {

        }

        void _dataManager_ReplayEvent(object sender, TDataManager.ReplayEventArgs e)
        {

        }

        void _dataManager_OffLineEvent(object sender, TDataManager.OffLineEventArgs e)
        {
            OfflineEventArgs a = new OfflineEventArgs(_dataManager.OffLine);
            OnOfflineEvent(a);
        }

        void _dataManager_ExprEvent(object sender, TDataManager.DataStoreEventArgs e)
        {

        }

        void _dataManager_DataCollectionEvent(object sender, TDataManager.DataCollectionEventArgs e)
        {

        }

        void _dataManager_ConstantEvent(object sender, TDataManager.ConstantArgs e)
        {

        }


        void _dataSource_WorkStatusChanged(object sender, WCYDataSource.StartStopEventArgs e)
        {

            OnWorkStatusChanged(e);
        }

        void _dataSource_ValueChanged(object sender, WCYDataSource.ValueEventArgs e)
        {
            if (!_adjusting)
            {
                TDataManager.DataSection lastDataSection = _dataManager.LastDataSection;
                lastDataSection.TimeLineProps.AddColumnCount(e.Index);

                //检查是否阀值字段
                if (_exInfo.ActionType == ActionType.Waiting)
                {
                    AddStartValue(e.Index, e.Value);
                }
                if (_exInfo.ActionType == ActionType.Started)
                {

                    AddStopValue(e.Index, e.Value);
                    if (lastDataSection != null)
                    {
                        if (e.Index >= 0 && e.Index < lastDataSection.Sensors.Count)
                        {
                            lastDataSection.TimeLineProps.LastTime = DateTime.Now;
                            int newTimeIndex = GetNewTimeIndex(e.Index);
                            //bool switchPhotoGate = false;
                            if (_workArguments.PhotoGateProperties.Available)
                            {

                                if (!_exInfo.PhotoGateOpen)
                                {
                                    bool open = CheckPhotoGateOpen(e.Index, e.Value);
                                    if (open)
                                    {
                                        //switchPhotoGate = true;
                                        _exInfo.PhotoGateOpen = open;
                                        _exInfo.SetPhotoGateStartTimeIndex(newTimeIndex);
                                        _exInfo.ResetSensorValues();
                                    }
                                }
                                else
                                {
                                    bool close = CheckPhotoGateClose(e.Index, e.Value);
                                    if (close)
                                    {
                                        //switchPhotoGate = true;
                                        _exInfo.PhotoGateOpen = !close;
                                        _exInfo.SetPhotoGateStopTimeIndex(newTimeIndex);
                                    }
                                }
                            }
                            if (_exInfo.PhotoGateOpen)
                            {
                                _exInfo.SetSensorValue(e.Index);
                                if (_workArguments.PhotoGateProperties.Available && _exInfo.CheckSensorValues())
                                {
                                    _exInfo.PhotoGateOpen = false;
                                }
                                int timeIndex;
                                if (_workArguments.PhotoGateProperties.Available)
                                {
                                    timeIndex = _exInfo.PhotoGateStartTimeIndex;
                                }
                                else
                                {
                                    timeIndex = newTimeIndex;
                                }
                                double timeStamp = lastDataSection.TimeLineProps.GetTimeStampByTimeIndex(timeIndex);
                                if (_dataSource.PortCollection.Ports[e.Index].SensorType == WCYDataSource.SensorTypeDefine.PhotoGate)
                                {
                                    timeStamp = _dataSource.PortCollection.Ports[e.Index].DownSideValue;
                                }
                                int index = lastDataSection.Sensors[e.Index].Add(e.Value, timeStamp, timeIndex);
                                TDataManager.DataElement newDataElement = lastDataSection.Sensors[e.Index].DataCollection.Datas[index];
                                TDataManager.DataCollectionEventArgs a = new TDataManager.DataCollectionEventArgs(TDataManager.DataEventType.Add, newDataElement, lastDataSection.Name, lastDataSection.Sensors[e.Index].DataProps.Name);
                                OnSensorDataEvent(a);
                            }
                        }
                    }
                    //}
                }

                //OnValueRecieved(e);
            }
        }

        void _dataSource_ShiftChanged(object sender, WCYDataSource.ShiftEventArgs e)
        {
            OnShiftSetChanged(e);
        }

        void _dataSource_PortChanged(object sender, WCYDataSource.PortCollectionEventArgs e)
        {
            MaskSensor();
            OnPortChanged(e);
        }

        void _dataSource_ConnectChanged(object sender, WCYDataSource.ConnectEventArgs e)
        {
            OnConnectChanged(e);
        }
        #region props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.Classes.DataEngine", Assembly.GetExecutingAssembly());
        public int DEFAULT_COUNT = 100;
        WCYDataSource.DataSource _dataSource;
        TDataManager.DataManager _dataManager;
        ExperimentProperties.WorkArguments _workArguments;
        SensorManager.SensorCollection _sensorCollection;
        ExInfo _exInfo = new ExInfo();
        UIProps _uiProps;
        WCYDataSource.SerialPortEngine _serialPortEngine;
        bool _netOpen = false;

        bool _adjusting;
        bool _looping = false;

        bool _USBlooping = false;//USB查询
        bool _startCalc = false;//开始计算Expr
        bool _waitStopCalc = false;//开始等待把剩余的Expr计算完毕
        int _calcSectionIndex = 0;//针对计算的Section
        DataAnalysis.Formula _formula;
        //int _photoGateOpenSensorIndex = -1;
        Thread _thread = null;
        Thread _USBthread = null;

        public bool IsUsbSet
        {
            get { return _dataSource.IsUSBCOM; }
        }

        public int Frequence//USB
        {
            set { _dataSource.Frequence = value; }
        }

        public bool Started
        {
            get { return _dataSource.Started; }
            set
            {

                _dataSource.Started = value;
                if (FormSelectLanguage._isBlueToothUsed)
                {
                    _dataSource.StartSendCMDToDeviceBLE();
                }
                else
                {
                    _dataSource.StartSendCMDToDevice(value);
                }
            }
        }
        public bool Adjusted
        {
            get { return _dataSource.Adjusted; }
            set { _dataSource.Adjusted = value; }
        }
        public ExInfo ExInfo
        {
            get { return _exInfo; }
        }

        public bool NetOpen
        {
            get { return _netOpen; }
            set { _netOpen = value; }
        }
        public bool Adjusting
        {
            get { return _adjusting; }
            set { _adjusting = value; }
        }
        public UIProps UIProps
        {
            get { return _uiProps; }
        }
        public ExperimentProperties.WorkArguments WorkArguments
        {
            get { return _workArguments; }
        }
        public SensorManager.SensorCollection SensorCollection
        {
            get { return _sensorCollection; }
        }
        public TDataManager.DataManager DataManager
        {
            get { return _dataManager; }
        }
        public WCYDataSource.DataSource DataSource
        {
            get { return _dataSource; }
        }
        public bool OffLine
        {
            get { return _dataManager.OffLine; }
            set { _dataManager.OffLine = value; }
        }
        public TDataManager.DataSection CurrentDataSection
        {
            get
            {
                return _dataManager.CurrentDataSection;
            }
        }
        public bool Connected
        {
            get
            {
                return _dataSource.Connected;
            }
        }
        /// <summary>
        /// 实验剩余时间
        /// </summary>
        public double RemainSeconds
        {
            get
            {
                double seconds = _workArguments.EndProperties.ReservedSeconds - _exInfo.Seconds;
                return seconds >= 0 ? seconds : 0;
            }
        }
        /// <summary>
        /// 实验已经花费的时间
        /// </summary>
        public double PassedSeconds
        {
            get
            {
                return _exInfo.Seconds;
            }
        }

        #endregion

        #region Init Methods
        void LoadDefault()
        {
            _dataManager = new TDataManager.DataManager();
            _workArguments = new ExperimentProperties.WorkArguments();
            _sensorCollection = new SensorManager.SensorCollection();
            _uiProps = new UIProps();
            _adjusting = false;
            _exInfo = new ExInfo();
            _serialPortEngine = new WCYDataSource.SerialPortEngine(FormSelectLanguage._is6p6c);

            _dataSource = new WCYDataSource.DataSource(GetSensorType);
            if (FormSelectLanguage._isBlueToothUsed)//2021 添加蓝牙是否开启判断  蓝牙和USB 至此分开
            {
                _dataSource.BLEStart();
            }
            else
            {
                _dataSource.CMDHidStart();
                _USBlooping = true;
                _USBthread = new Thread(QueryType);
                _USBthread.Start();
            }
            _formula = new DataAnalysis.Formula(_dataManager.GetCaption, _dataManager.CheckName);
        }
        public void New()
        {
            //LoadDefault();
            _dataManager.New();
            _workArguments.New();
            _uiProps = new UIProps();
            _exInfo = new ExInfo();
            _formula = new DataAnalysis.Formula(_dataManager.GetCaption, _dataManager.CheckName);
            //_dataManager.OffLine = true;
            DataEngineEventArgs e = new DataEngineEventArgs(DataEngineEventType.New, this);
            OnDataEngineEvent(e);

        }

        public bool LoadSensorCollection(string filename)
        {
            bool success = false;
            try
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(filename);
                _sensorCollection.Parse(reader.ReadToEnd());
                reader.Close();
                //RefreshDataSourceSensorTypeClassList();
                success = true;
            }
            catch
            {
                _sensorCollection = new SensorManager.SensorCollection();
                success = false;
            }
            return success;
        }
        public bool SaveSensorCollection(string filename)
        {

            bool success = false;
            try
            {
                System.IO.StreamWriter writer = System.IO.File.CreateText(filename);
                if (writer != null)
                {
                    writer.Write(_sensorCollection.ToString());
                    writer.Close();
                    //RefreshDataSourceSensorTypeClassList();
                    success = true;

                }
            }
            catch
            {
                success = false;

            }
            return success;
        }
        public bool LoadDefaultIntervalCollection(string filename)
        {
            bool success = false;
            try
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(filename);
                this._workArguments.IntervalCollection.Parse(reader.ReadToEnd());
                this._dataManager.DataSectionActive.TimeLineProps.ShiftIndex = (byte)_workArguments.IntervalCollection.GetShiftIndexByFrequency(_dataManager.DataSectionActive.TimeLineProps.Frequency);
                this._dataManager.DataSectionTemplete.TimeLineProps.ShiftIndex = (byte)_workArguments.IntervalCollection.GetShiftIndexByFrequency(_dataManager.DataSectionTemplete.TimeLineProps.Frequency);
                reader.Close();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }
        public bool SaveDefaultIntervalCollection(string filename)
        {
            bool success = false;
            try
            {
                System.IO.StreamWriter writer = System.IO.File.CreateText(filename);
                if (writer != null)
                {
                    writer.Write(this._workArguments.IntervalCollection.ToString());
                    writer.Close();
                    success = true;

                }
            }
            catch
            {
                success = false;

            }
            return success;
        }
        public bool LoadDataEngine(string filename)
        {
            bool success = false;
            try
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(filename);
                this.Parse(reader.ReadToEnd());
                reader.Close();
                success = true;
                //_dataSource.QueryTypeAll();

                DataEngineEventArgs e = new DataEngineEventArgs(DataEngineEventType.Load, this);
                OnDataEngineEvent(e);

            }
            catch
            {
                success = false;
            }
            return success;
        }
        public bool SaveDataEngine(string filename)
        {
            bool success = false;
            try
            {
                System.IO.StreamWriter writer = System.IO.File.CreateText(filename);
                if (writer != null)
                {
                    writer.Write(this.ToString());
                    writer.Close();
                    success = true;
                    DataEngineEventArgs e = new DataEngineEventArgs(DataEngineEventType.Save, this);
                    OnDataEngineEvent(e);

                }
            }
            catch
            {
                success = false;

            }
            return success;
        }

        #endregion

        #region Methods
        public void QueryType()
        {
            while (_USBlooping)
            {
                _dataSource.QueryType();
                Thread.Sleep(500);
            }
        }
        public void Dispose()
        {
            _looping = false;
            _USBlooping = false;
            _dataSource.Dispose();
        }
        public void Wave(byte Wave, byte Wavetype, byte value, byte time)//波形发生器，数控功率电阻，控制开关
        {
            _dataSource.Wave(Wave, Wavetype, value, time);
        }
        public void USBWave(byte Type, byte YY, byte XX, byte DD, byte PP)
        {
            _dataSource.USBWave(Type, YY, XX, DD, PP);
        }

        public WCYDataSource.SensorTypeDefine GetSensorType(int sensorID, ref float k, ref float b)
        {
            WCYDataSource.SensorTypeDefine type = WCYDataSource.SensorTypeDefine.Normal;
            SensorManager.SensorDefine sensor = _sensorCollection.GetSensorDefineBySensorID(sensorID);
            if (sensor != null)
            {
                //Thread.Sleep(200);
                k = (float)sensor.K;
                b = (float)sensor.B;
                switch (sensor.TypeID)
                {
                    default:
                    case 0://Nomal
                        type = WCYDataSource.SensorTypeDefine.Normal;
                        break;
                    case 1://PhotoGate
                        type = WCYDataSource.SensorTypeDefine.PhotoGate;
                        break;
                    case 2://Heart
                        type = WCYDataSource.SensorTypeDefine.Heart;
                        break;
                }
            }
            return type;
        }
        public string GetSensorTypeName(byte sensorID)
        {
            string name = _resourceManager.GetString("Normal");
            SensorManager.SensorDefine sensor = _sensorCollection.GetSensorDefineBySensorID(sensorID);
            if (sensor != null)
            {
                switch (sensor.TypeID)
                {
                    default:
                    case 0://Nomal
                        name = _resourceManager.GetString("Normal");
                        break;
                    case 1://PhotoGate
                        name = _resourceManager.GetString("PhotoGate");
                        break;
                    case 2://Heart
                        name = _resourceManager.GetString("HeartRate");
                        break;
                }
            }
            return name;
        }
        public string GetSensorTypeName(int typeID)
        {
            string name = _resourceManager.GetString("Normal");
            switch (typeID)
            {
                default:
                case 0://Nomal
                    name = _resourceManager.GetString("Normal");
                    break;
                case 1://PhotoGate
                    name = _resourceManager.GetString("PhotoGate");
                    break;
                case 2://Heart
                    name = _resourceManager.GetString("HeartRate");
                    break;
            }
            return name;
        }
        public void MaskSensor()
        {
            _dataManager.ClearSensor();
            for (int i = 0; i < _dataSource.PortCollection.Ports.Length; i++)
            {
                if (_dataSource.PortCollection.Ports[i].PortAvailable)
                {
                    WCYDataSource.PortInfo portInfo = _dataSource.PortCollection.Ports[i];
                    SensorManager.SensorDefine sensorDefine = _sensorCollection.GetSensorDefineBySensorID(portInfo.PortIndex);
                    if (portInfo.IsPlugIn)
                    {
                        _dataManager.AddActiveSensorNoIndex(sensorDefine.Caption, sensorDefine.MaxValue, sensorDefine.MinValue, sensorDefine.Decimal, sensorDefine.Calibration, sensorDefine.Unit, TDataManager.DataCollection.DEFAULTCOUNT, portInfo.PortIndex);
                    }
                    else
                    {
                        _dataManager.AddActiveSensorNoIndex("-", sensorDefine.MaxValue, sensorDefine.MinValue, sensorDefine.Decimal, sensorDefine.Calibration, sensorDefine.Unit, TDataManager.DataCollection.DEFAULTCOUNT, 0x00);
                    }
                }

            }
        }
        public void StartAdjust()
        {
            if (_dataSource.Connected && !_dataManager.OffLine && !_dataSource.Working)
            {
                int capcity = _workArguments.CalcCapcity(CurrentDataSection.TimeLineProps.Interval);
                _adjusting = true;
                byte[] intervalBytes = ExperimentProperties.IntervalDefine.CalcBytes(_dataManager.DataSectionActive.TimeLineProps.Interval);

                _dataSource.Start(intervalBytes[0], intervalBytes[1], intervalBytes[2]);
            }
        }
        public void StopAdjust()
        {
            if (_adjusting)
            {
                _adjusting = false;
                _dataSource.Stop();
            }
        }

        public void Start()
        {
            if (_dataSource.Connected && !_dataManager.OffLine && !_dataSource.Working)
            {
                _looping = false;
                if (_thread != null)
                {
                    _thread.Abort();
                }
                int realSensorCount = 0;
                for (int i = 0; i < _dataManager.LastDataSection.Sensors.Count; i++)
                {
                    if (_dataManager.LastDataSection.Sensors[i].DataProps.SensorID > 0x00)
                    {
                        realSensorCount++;
                    }
                }
                _exInfo.Prepair(realSensorCount);
                SetManualOpenSensorIndex();
                int capcity = _workArguments.CalcCapcity(_dataManager.LastDataSection.TimeLineProps.Interval);

                _dataManager.AddDataSection(_workArguments.CalcCapcity(_dataManager.DataSectionActive.TimeLineProps.Interval));

                byte[] intervalBytes = ExperimentProperties.IntervalDefine.CalcBytes(_dataManager.DataSectionActive.TimeLineProps.Interval);
                _looping = true;

                _thread = new Thread(Looping);

                _thread.Start();

                _dataSource.Start(intervalBytes[0], intervalBytes[1], intervalBytes[2]);
            }
        }
        public void Stop()
        {
            if (_dataSource.Working)
            {
                _dataSource.Stop();
            }
            _dataSource.Stop();

        }
        public void OpenPhotoGate()
        {
            _exInfo.PhotoGateShouldOpen = true;
        }

        public void SetOffLine()
        {
            _dataManager.OffLine = true;
        }
        public void SetOnLine()
        {
            _dataManager.OffLine = false;
        }
        bool CheckStart()
        {
            bool shouldStart = false;
            switch (_workArguments.StartProperties.StartMode)
            {
                case ExperimentProperties.StartMode.IndexCount:
                    shouldStart = _exInfo.CheckStart(_workArguments.StartProperties.ReservedPoints);
                    break;
                case ExperimentProperties.StartMode.Manual:
                    shouldStart = true;
                    break;
                case ExperimentProperties.StartMode.Remote:
                    shouldStart = true;
                    break;
                case ExperimentProperties.StartMode.TimeStamp:
                    shouldStart = _exInfo.CheckStart(_workArguments.StartProperties.ReservedSeconds);
                    break;
                case ExperimentProperties.StartMode.ValueDecrease:
                    shouldStart = _exInfo.CheckStart(_workArguments.StartProperties.GateValue, false, _workArguments.StartProperties.CheckAbs);
                    break;
                case ExperimentProperties.StartMode.ValueIncrease:
                    shouldStart = _exInfo.CheckStart(_workArguments.StartProperties.GateValue, true, _workArguments.StartProperties.CheckAbs);
                    break;
            }
            if (shouldStart)
            {
                _exInfo.ActionType = ActionType.Started;
            }
            return shouldStart;
        }
        bool CheckStop()
        {
            bool shouldStop = false;
            switch (_workArguments.EndProperties.EndMode)
            {
                case ExperimentProperties.EndMode.IndexCount:
                    shouldStop = _exInfo.CheckStop(_workArguments.EndProperties.ReservedPoints);
                    break;
                case ExperimentProperties.EndMode.Manual:
                    break;
                case ExperimentProperties.EndMode.Remote:
                    break;
                case ExperimentProperties.EndMode.TimeStamp:
                    shouldStop = _exInfo.CheckStop(_workArguments.EndProperties.ReservedSeconds);
                    break;
                case ExperimentProperties.EndMode.ValueDecrease:
                    shouldStop = _exInfo.CheckStop(_workArguments.EndProperties.GateValue, false, _workArguments.EndProperties.CheckAbs);
                    break;
                case ExperimentProperties.EndMode.ValueIncrease:
                    shouldStop = _exInfo.CheckStop(_workArguments.EndProperties.GateValue, true, _workArguments.EndProperties.CheckAbs);
                    break;

            }
            //if (shouldStop)
            //{
            //    _exInfo.ActionType = ActionType.Stoped;
            //}
            return shouldStop;
        }
        bool CheckPhotoGateOpen(int index, double value)
        {
            bool open = false;
            if (_workArguments.PhotoGateProperties.Available)
            {
                TDataManager.DataSection lastDataSection = _dataManager.LastDataSection;
                if (lastDataSection != null)
                {
                    if (index >= 0 && index < lastDataSection.Sensors.Count)
                    {
                        switch (_workArguments.PhotoGateProperties.OpenMode)
                        {
                            case ExperimentProperties.PhotoGateMode.Manual:
                                if (_exInfo.PhotoGateShouldOpen && index == _exInfo.ManualOpenSensorIndex)
                                {
                                    _exInfo.PhotoGateShouldOpen = false;
                                    //_photoGateOpenSensorIndex = index;
                                    open = true;
                                }
                                break;
                            case ExperimentProperties.PhotoGateMode.ValueDecrease:
                                if (string.Equals(lastDataSection.Sensors[index].DataProps.Name, _workArguments.PhotoGateProperties.OpenColumnName))
                                {
                                    double checkValue = _workArguments.PhotoGateProperties.OpenCheckAbs ? Math.Abs(value) : value;
                                    open = checkValue <= _workArguments.PhotoGateProperties.OpenGateValue;
                                }
                                break;
                            case ExperimentProperties.PhotoGateMode.ValueIncrease:
                                if (string.Equals(lastDataSection.Sensors[index].DataProps.Name, _workArguments.PhotoGateProperties.OpenColumnName))
                                {
                                    double checkValue = _workArguments.PhotoGateProperties.OpenCheckAbs ? Math.Abs(value) : value;
                                    open = checkValue >= _workArguments.PhotoGateProperties.OpenGateValue;
                                }
                                break;
                        }

                    }
                }
            }
            else
            {
                open = true;
            }
            return open;

        }
        bool CheckPhotoGateClose(int index, double value)
        {
            bool close = false;
            if (_workArguments.PhotoGateProperties.Available)
            {
                TDataManager.DataSection lastDataSection = _dataManager.LastDataSection;
                if (index >= 0 && index < lastDataSection.Sensors.Count)
                {
                    switch (_workArguments.PhotoGateProperties.CloseMode)
                    {
                        case ExperimentProperties.PhotoGateMode.Manual:
                            //if (index == _exInfo.ManualOpenSensorIndex)
                            if (_exInfo.CheckSensorValues())
                            {
                                close = true;
                            }
                            break;
                        case ExperimentProperties.PhotoGateMode.ValueDecrease:
                            if (string.Equals(lastDataSection.Sensors[index].DataProps.Name, _workArguments.PhotoGateProperties.CloseColumnName))
                            {
                                double checkValue = _workArguments.PhotoGateProperties.CloseCheckAbs ? Math.Abs(value) : value;
                                close = checkValue <= _workArguments.PhotoGateProperties.CloseGateValue;
                            }
                            break;
                        case ExperimentProperties.PhotoGateMode.ValueIncrease:
                            if (string.Equals(lastDataSection.Sensors[index].DataProps.Name, _workArguments.PhotoGateProperties.CloseColumnName))
                            {
                                double checkValue = _workArguments.PhotoGateProperties.CloseCheckAbs ? Math.Abs(value) : value;
                                close = checkValue >= _workArguments.PhotoGateProperties.CloseGateValue;
                            }
                            break;
                    }
                }
            }
            return close;
        }
        void SetManualOpenSensorIndex()
        {
            List<int> list = _dataSource.PortCollection.PlugInPortIndexes;
            for (int i = 0; i < list.Count; i++)
            {
                if (_dataSource.PortCollection.Ports[list[i]].SensorType != WCYDataSource.SensorTypeDefine.PhotoGate)
                {
                    _exInfo.ManualOpenSensorIndex = i;
                    break;
                }
            }

        }
        void AddStartValue(int index, double value)
        {
            switch (_workArguments.StartProperties.StartMode)
            {
                case ExperimentProperties.StartMode.IndexCount:
                case ExperimentProperties.StartMode.TimeStamp:
                case ExperimentProperties.StartMode.ValueDecrease:
                case ExperimentProperties.StartMode.ValueIncrease:
                    TDataManager.DataSection lastDataSection = _dataManager.LastDataSection;
                    if (lastDataSection != null)
                    {
                        if (index >= 0 && index < lastDataSection.Sensors.Count)
                        {
                            if (string.Equals(lastDataSection.Sensors[index].DataProps.Name, _workArguments.StartProperties.StartPortName))
                            {
                                _exInfo.AddValue(value);
                            }
                        }
                    }
                    break;
            }
        }
        void AddStopValue(int index, double value)
        {
            switch (_workArguments.EndProperties.EndMode)
            {
                case ExperimentProperties.EndMode.IndexCount:
                case ExperimentProperties.EndMode.TimeStamp:
                case ExperimentProperties.EndMode.ValueDecrease:
                case ExperimentProperties.EndMode.ValueIncrease:
                    TDataManager.DataSection lastDataSection = _dataManager.LastDataSection;
                    if (lastDataSection != null)
                    {
                        if (index >= 0 && index < lastDataSection.Sensors.Count)
                        {
                            if (string.Equals(lastDataSection.Sensors[index].DataProps.Name, _workArguments.EndProperties.EndPortName))
                            {
                                _exInfo.AddValue(value);
                            }
                        }
                    }
                    break;
            }
        }
        int GetNewTimeIndex(int sensorIndex)
        {
            TDataManager.DataSection lastSection = _dataManager.LastDataSection;
            int newTimeIndex = lastSection.TimeLineProps.NewTimeIndex;
            if (!_workArguments.PhotoGateProperties.Available)
            {
                if (sensorIndex >= 0 && sensorIndex < _dataSource.PortCollection.Ports.Length)
                {
                    WCYDataSource.SensorTypeDefine sensorClass = _dataSource.PortCollection.Ports[sensorIndex].SensorType;
                    if (sensorClass != WCYDataSource.SensorTypeDefine.PhotoGate)
                    {
                        int count = lastSection.Sensors[sensorIndex].DataCollection.Datas.Count;
                        newTimeIndex = count;
                    }
                }
            }
            return newTimeIndex;
        }

        void CalcExprByTimeIndex(int sectionIndex)
        {
            if (sectionIndex >= 0 && sectionIndex < _dataManager.DataSections.Count)
            {
                TDataManager.DataSection dataSection = _dataManager.DataSections[sectionIndex];
                for (int i = 0; i < dataSection.DataStores.Count; i++)
                {
                    TDataManager.DataStore dataStore = dataSection.DataStores[i];

                    int newTimeIndex = dataStore.LastTimeIndex + 1;
                    TDataManager.DataStore flowDataStore = dataSection.GetDataStoreByColumnName(dataStore.DataProps.FlowSensorName);
                    if (flowDataStore != null)
                    {
                        int flowLastTimeIndex = flowDataStore.LastTimeIndex;
                        int step = dataStore.DataProps.FlowStep;
                        while (newTimeIndex + step <= flowLastTimeIndex)
                        {
                            if (!dataSection.DataExistsByTimeIndex(dataStore.DataProps.Name, newTimeIndex))
                            {
                                if (dataSection.DataExistsByTimeIndex(dataStore.DataProps.FlowSensorName, newTimeIndex + step))
                                {

                                    _formula.Initialize(sectionIndex, newTimeIndex, _dataManager.GetDataValueByTimeIndex, _dataManager.GetData, _dataManager.GetDataTimeStampByTimeIndex, _dataManager.GetDataTimeStamp, _dataManager.GetDataIndexByTimeIndex, _dataManager.CheckName);
                                    double result = _formula.Calculate(dataStore.DataProps.Expression);
                                    int index = dataStore.Add(result, dataSection.TimeLineProps.GetTimeStampByTimeIndex(newTimeIndex), newTimeIndex);
                                    TDataManager.DataElement newDataElement = dataStore.GetDataElementByIndex(index);
                                    TDataManager.DataCollectionEventArgs e = new TDataManager.DataCollectionEventArgs(TDataManager.DataEventType.Add, newDataElement, _dataManager.DataSections[sectionIndex].Name, dataStore.DataProps.Name);
                                    OnExprDataEvent(e);

                                }
                            }
                            newTimeIndex++;

                        }
                    }
                }
            }
        }
        bool CalcOverByTimeIndex(int sectionIndex)
        {
            bool calcOver = true;
            if (sectionIndex >= 0 && sectionIndex < _dataManager.DataSections.Count)
            {
                TDataManager.DataSection dataSection = _dataManager.DataSections[sectionIndex];
                for (int i = 0; i < dataSection.DataStores.Count; i++)
                {
                    TDataManager.DataStore dataStore = dataSection.DataStores[i];
                    int step = dataStore.LastTimeIndex + dataStore.DataProps.FlowStep;
                    if (step < 0)
                    {
                        calcOver = true;
                    }
                    else if (!dataSection.DataExistsByTimeIndex(dataStore.DataProps.FlowSensorName, step))
                    {
                        calcOver = false;
                        break;
                    }
                }
            }
            return calcOver;
        }
        void Looping()
        {
            while (_looping)
            {
                //检查是否阀值字段
                if (_exInfo.ActionType == ActionType.Waiting)
                {
                    //AddStartValue(e.Index, e.Value);

                    if (CheckStart())
                    {
                        TDataManager.DataSection lastDataSection = _dataManager.LastDataSection;
                        if (lastDataSection != null)
                        {
                            _startCalc = true;
                            _waitStopCalc = false;
                            _calcSectionIndex = _dataManager.DataSections.Count - 1;
                            lastDataSection.TimeLineProps.Prepair();
                            //lastDataSection.TimeLineProps.AddColumnCount(e.Index);
                            _exInfo.ResetStarted();
                            _exInfo.PhotoGateOpen = !_workArguments.PhotoGateProperties.Available;
                        }
                    }
                }
                if (_exInfo.ActionType == ActionType.Started)
                {

                    //AddStopValue(e.Index, e.Value);
                    if (!_waitStopCalc && CheckStop())
                    {
                        _waitStopCalc = true;
                        Stop();
                    }
                }
                if (_startCalc)
                {
                    if (_waitStopCalc)
                    {
                        if (CalcOverByTimeIndex(_calcSectionIndex))
                        {
                            _startCalc = false;
                            _looping = false;
                        }
                    }
                    CalcExprByTimeIndex(_calcSectionIndex);
                }
                Thread.Sleep(1);
            }
        }
        #endregion



        #region Events
        public event WCYDataSource.ConnectedHandler ConnectChanged = null;
        protected void OnConnectChanged(WCYDataSource.ConnectEventArgs e)
        {
            if (ConnectChanged != null)
            {
                ConnectChanged(this, e);
            }
        }
        public event WCYDataSource.ValueHandler ValueRecieved = null;
        protected void OnValueRecieved(WCYDataSource.ValueEventArgs e)
        {
            if (ValueRecieved != null)
            {
                ValueRecieved(this, e);
            }
        }
        public event WCYDataSource.StartStopHandler WorkStatusChanged = null;
        protected void OnWorkStatusChanged(WCYDataSource.StartStopEventArgs e)
        {
            if (WorkStatusChanged != null)
            {
                WorkStatusChanged(this, e);
            }
        }
        public event WCYDataSource.PortCollectionHandler PortChanged = null;
        protected void OnPortChanged(WCYDataSource.PortCollectionEventArgs e)
        {
            if (PortChanged != null)
            {
                PortChanged(this, e);
            }
        }
        public event WCYDataSource.ShiftHandler ShiftSetChanged = null;
        protected void OnShiftSetChanged(WCYDataSource.ShiftEventArgs e)
        {
            if (ShiftSetChanged != null)
            {
                ShiftSetChanged(this, e);
            }
        }
        public event DataEngineHandler DataEngineEvent = null;
        protected void OnDataEngineEvent(DataEngineEventArgs e)
        {
            if (DataEngineEvent != null)
            {
                DataEngineEvent(this, e);
            }
        }
        public event OfflineHandler OfflineEvent = null;
        protected void OnOfflineEvent(OfflineEventArgs e)
        {
            if (OfflineEvent != null)
            {
                OfflineEvent(this, e);
            }
        }
        public event TDataManager.DataCollectionHandler ExprDataEvent = null;
        protected void OnExprDataEvent(TDataManager.DataCollectionEventArgs e)
        {
            if (ExprDataEvent != null)
            {
                ExprDataEvent(this, e);
            }
        }
        public event TDataManager.DataCollectionHandler SensorDataEvent = null;
        protected void OnSensorDataEvent(TDataManager.DataCollectionEventArgs e)
        {
            if (SensorDataEvent != null)
            {
                SensorDataEvent(this, e);
            }
        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("DataManager", _dataManager.ToString());
            //keyValue.Add("SystemProps", _systemProps.ToString());
            keyValue.Add("WorkArgument", _workArguments.ToString());
            keyValue.Add("UIProps", _uiProps.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _dataManager.Parse(keyValue.GetValueByKey("DataManager"));
            //_systemProps.Parse(keyValue.GetValueByKey("SystemProps"));
            _workArguments.Parse(keyValue.GetValueByKey("WorkArgument"));
            _uiProps.Parse(keyValue.GetValueByKey("UIProps"));
        }
        #endregion

    }
    public enum DataEngineEventType : int
    {
        New = 0, Load = 1, Save = 2, SaveAs = 3, Sended = 4, Recieved = 5
    }

    public class DataEngineEventArgs : EventArgs
    {
        public DataEngineEventArgs(DataEngineEventType type, DataEngine dataEngine)
        {
            _dataEngine = dataEngine;
            _type = type;
        }
        #region Props
        DataEngineEventType _type;
        DataEngine _dataEngine;
        public DataEngineEventType EventType
        {
            get { return _type; }
        }
        public DataEngine DataEngine
        {
            get { return _dataEngine; }
        }

        #endregion
    }

    public delegate void DataEngineHandler(object sender, DataEngineEventArgs e);

    public class OfflineEventArgs : EventArgs
    {
        public OfflineEventArgs(bool isOffline)
        {
            _isOffline = isOffline;
        }
        #region Props
        bool _isOffline;
        public bool IsOffline
        {
            get { return _isOffline; }
        }
        #endregion
    }

    public delegate void OfflineHandler(object sender, OfflineEventArgs e);
}
