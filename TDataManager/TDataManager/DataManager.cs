using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Resources;

namespace TDataManager
{
    public class DataManager
    {
         /// <summary>
         /// 实验数据管理，包含了实验模板，多次实验采样数据等
         /// </summary>
        public DataManager()
        {
            LoadDefault();
            
        }
        /// <summary>
        /// 实验数据管理
        /// </summary>
        /// <param name="name">内部名称</param>
        /// <param name="caption">外部名称</param>
        /// <param name="description">描述</param>
        public DataManager(string name, string caption, string description)
        {
            LoadDefault();
            Initialize(name, caption, description);
        }
        /// <summary>
        /// 实验数据管理
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public DataManager(string value)
        {
            LoadDefault();
            Parse(value);
        }
            
        #region Props
        ResourceManager _resourceManager = new ResourceManager("TDataManager.DataManager", Assembly.GetExecutingAssembly());
        List<DataSection> _dataSections = new List<DataSection>();
        DataSection _dataSectionTemplete;
        DataSection _dataSectionActive;
        string _name;
        string _caption;
        string _description;
        bool _offLine ;
        Replay _replay;
        List<double> _replayRateList = new List<double>();
        static string DATASECTION_ACTIVE_NAME = "DataSectionActive";
        static string DATASECTION_TEMPLETE_NAME = "DataSectionTemplete";
        public static string TIMEINDEX_NAME = "TimeIndex";
        public static string TIMESTAMP_NAME = "TimeStamp";
        /// <summary>
        /// 采样数据集
        /// </summary>
        public List<DataSection> DataSections
        {
            get { return _dataSections; }
        }
        /// <summary>
        /// 内部名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 外部名称
        /// </summary>
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// 实验设计模板
        /// </summary>
        public DataSection DataSectionTemplete
        {
            get { return _dataSectionTemplete; }
        }
        /// <summary>
        /// 实际工作模板
        /// </summary>
        public DataSection DataSectionActive
        {
            get { return _dataSectionActive; }
        }
        /// <summary>
        /// 现有采样总次数
        /// </summary>
        public int Count
        {
            get { return _dataSections.Count; }
        }
        /// <summary>
        /// 回放控制器
        /// </summary>
        public Replay Replay
        {
            get { return _replay; }
        }
        /// <summary>
        /// 实验数据集描述
        /// </summary>
        public string QuickDescription
        {
            get
            {
                return "DataManager:" + _name + ":" + _caption + ":" + Count.ToString();
            }
        }
        /// <summary>
        /// 是否离线
        /// </summary>
        public bool OffLine
        {
            get{return _offLine;}
            set
            {
                bool oldStatus = _offLine;
                _offLine = value;
                if (oldStatus != _offLine)
                {
                    OffLineEventArgs e = new OffLineEventArgs(_offLine);
                    OnOffLineEvent(e);
                }
            }
        }
        /// <summary>
        /// 当前模板（离线状态时为设计模板，连线状态时为工作模板）
        /// </summary>
        public DataSection CurrentDataSection
        {
            get
            {
                return _offLine ? _dataSectionTemplete : _dataSectionActive;
                
            }
        }
        /// <summary>
        /// 设计模板是否完全等同工作模板
        /// </summary>
        public bool TempleteEqualsActive
        {
            get
            {
                DataSection templete = new DataSection(_dataSectionTemplete.ToString());
                DataSection active = new DataSection(_dataSectionActive.ToString());
                templete.Name = "";
                templete.Caption = "";
                active.Name = templete.Name;
                active.Caption = templete.Caption;
                return string.Equals(templete.ToString() ,active.ToString());
            }
        }
        /// <summary>
        /// 最后一次采样数据集，如果没有任何数据则等同CurrentSection
        /// </summary>
        public DataSection LastDataSection
        {
            get
            {
                DataSection dataSection = CurrentDataSection;
                if (_dataSections.Count > 0)
                {
                    dataSection = _dataSections[_dataSections.Count - 1];
                }
                return dataSection;
            }
        }
        /// <summary>
        /// 采样数据集的名称集
        /// </summary>
        public List<string> SectionNames
        {
            get
            {
                List<string> sectionNames = new List<string>();
                for (int i = 0; i < _dataSections.Count; i++)
                {
                    sectionNames.Add(_dataSections[i].Name);
                }
                return sectionNames;
            }
        }
        /// <summary>
        /// 重放速率集
        /// </summary>
        public List<double> ReplayRateList
        {
            get { return _replayRateList; }
        }
        /// <summary>
        /// 默认的重放速率索引
        /// </summary>
        public int DefaultReplayRateIndex
        {
            get { return 3; }
        }
        /// <summary>
        /// 判断模板要求的探头类型、数量、顺序是否与实际插入的符合
        /// </summary>
        public bool SensorMatch
        {
            get
            {
                bool equal = true;
                if (_dataSectionActive.Sensors.Count == _dataSectionTemplete.Sensors.Count)
                {
                    for (int i = 0; i < _dataSectionTemplete.Sensors.Count; i++)
                    {
                        if (_dataSectionTemplete.Sensors[i].DataProps.SensorID != _dataSectionActive.Sensors[i].DataProps.SensorID)
                        {
                            equal = false;
                            break;
                        }
                    }
                }
                else
                {
                    equal = false;
                }
                return equal;
                //return string.Equals(_dataSectionTemplete.Sensor2Str(), _dataSectionActive.Sensor2Str());
            }
        }
        #endregion

        #region Methods
        void LoadDefault()//初始化DataManager；
        {
            _dataSections.Clear();//dataSection定义的类的序列清空；
            _dataSectionTemplete = new DataSection();//_dataSectionActive和 _dataSectionTemplete都是DataSection的对象；
            _replay = new Replay();
            _dataSectionActive = new DataSection();
            _replayRateList.Clear();//清空double类型的重放速率；
            _replayRateList.AddRange(new double[] { 0.1, 0.2, 0.5, 1, 2, 4, 8, 16 });//对double类型的重放速率进行初始化；
            _dataSectionActive.Name = DATASECTION_ACTIVE_NAME;
            _dataSectionTemplete.Name = DATASECTION_TEMPLETE_NAME;//两种方式的名称初始化；
            _name = "";
            _caption = "";
            _description = "";
            _offLine = false;
            _replay.ReplayEvent += new ReplayHandler(_replay_ReplayEvent);//重放事件进行加载；
        }

        void _replay_ReplayEvent(object sender, ReplayEventArgs e)
        {
            OnReplay(e);
        }
        void Initialize(string name, string caption, string description)
        {
            _name = name;
            _caption = caption;
            _description = description;
            _dataSections.Clear();
        }
        public void New()
        {
            LoadDefault();
        }
        /// <summary>
        /// 判断名称是否是时间索引
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>是否是</returns>
        public static bool IsTimeIndex(string name)
        {
            return string.Compare(name, TIMEINDEX_NAME) == 0;
        }
        /// <summary>
        /// 判断名称是否是时间戳
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>是否是</returns>
        public static bool IsTimeStamp(string name)
        {
            return string.Compare(name, TIMESTAMP_NAME) == 0;
        }
        #endregion

        #region DataSection Methods
        /// <summary>
        /// 将设计模板复制到工作模板
        /// </summary>
        public void DataSectionTempleteToActive()
        {
            _dataSectionActive.Parse(_dataSectionTemplete.ToString());
            _dataSectionActive.Name = DATASECTION_ACTIVE_NAME;
        }
        /// <summary>
        /// 将工作模板复制到设计模板
        /// </summary>
        public void DataSectionActiveToTemplete()
        {
            _dataSectionTemplete.Parse(_dataSectionActive.ToString());
            _dataSectionTemplete.Name = DATASECTION_TEMPLETE_NAME;
        }
        /// <summary>
        /// 将设计模板复制到工作模板
        /// </summary>
        /// <param name="withoutSensors">是否不复制传感器连接情况</param>
        public void DataSectionTempleteToActive(bool withoutSensors)
        {
            List<DataStore> sensors = new List<DataStore>();
            sensors.AddRange(_dataSectionActive.Sensors);
            _dataSectionActive.Parse(_dataSectionTemplete.ToString(withoutSensors),withoutSensors );
            if (!withoutSensors)
            {
                for (int i = 0; i < this._dataSectionTemplete.Sensors.Count; i++)
                {
                    if (i < _dataSectionTemplete.Sensors.Count)
                    {
                        _dataSectionActive.Sensors[i].DataProps.ShiftIndex = _dataSectionTemplete.Sensors[i].DataProps.ShiftIndex;
                    }
                }
            }
            else
            {
                for (int i = 0; i < sensors.Count; i++)
                {
                    _dataSectionActive.Sensors.Add(new DataStore(sensors[i].ToString()));
                    _dataSectionActive.Sensors[i].DataCollectionEvent += new DataCollectionHandler(_dataSectionActive.DataSection_DataCollectionEvent);
                }
            }
            _dataSectionActive.Name = DATASECTION_ACTIVE_NAME;
        }
        /// <summary>
        /// 将工作模板复制到设计模板
        /// </summary>
        /// <param name="withoutSensors">是否不复制传感器连接情况</param>
        public void DataSectionActiveToTemplete(bool withoutSensors)
        {
            List<DataStore> sensors = new List<DataStore>();
            sensors.AddRange(this._dataSectionTemplete.Sensors);
            _dataSectionTemplete.Parse(_dataSectionActive.ToString(withoutSensors), withoutSensors);
            if (!withoutSensors)
            {
                for (int i = 0; i < _dataSectionActive.Sensors.Count; i++)
                {
                    if (i < this._dataSectionActive.Sensors.Count)
                    {
                        this._dataSectionTemplete.Sensors[i].DataProps.ShiftIndex = this._dataSectionActive.Sensors[i].DataProps.ShiftIndex;
                    }
                }
            }
            else
            {
                for (int i = 0; i < sensors.Count; i++)
                {
                    _dataSectionTemplete.Sensors.Add(new DataStore(sensors[i].ToString()));
                    _dataSectionTemplete.Sensors[i].DataCollectionEvent += new DataCollectionHandler(_dataSectionTemplete.DataSection_DataCollectionEvent);
                }
            }
            _dataSectionTemplete.Name = DATASECTION_TEMPLETE_NAME;
        }
        /// <summary>
        /// 增加一个采样数据集
        /// </summary>
        public void AddDataSection()
        {
            DataSection dataSection = new DataSection(_dataSectionActive.ToString());
            dataSection.Name = GetNewName();
            dataSection.Caption ="采样次数:"+(_dataSections.Count+1);
            _dataSections.Add(dataSection);
            dataSection.TimeLineProps.StartTime = DateTime.Now;
            dataSection.DataCollectionEvent += new DataCollectionHandler(dataSection_DataCollectionEvent);
            SectionEventArgs e = new SectionEventArgs(DataEventType.Add, dataSection.Name, _dataSections.Count-1);
            OnSectionEvent(e);
        }

        void dataSection_DataCollectionEvent(object sender, DataCollectionEventArgs e)
        {
            OnDataCollectionEvent(e);
        }
        /// <summary>
        /// 增加一个采样数据集
        /// </summary>
        /// <param name="count">默认数据行数</param>
        public void AddDataSection(int count)
        {
            DataSection dataSection = new DataSection(_dataSectionActive.ToString());
            dataSection.Name = GetNewName();
            dataSection.Caption = _resourceManager.GetString("SamplingTime")+ ":" +( _dataSections.Count + 1);
            dataSection.SetCount(count);
            _dataSections.Add(dataSection);
            dataSection.DataCollectionEvent += new DataCollectionHandler(dataSection_DataCollectionEvent);
            SectionEventArgs e = new SectionEventArgs(DataEventType.Add, dataSection.Name, _dataSections.Count -1);
            OnSectionEvent(e);
        }
        /// <summary>
        /// 删除采样数据集
        /// </summary>
        /// <param name="index">被删除的采样数据集索引</param>
        public void RemoveDataSection(int index)
        {
            if (index >= 0 && index < _dataSections.Count)
            {
                string name = _dataSections[index].Name;
                _dataSections.RemoveAt(index);
                SectionEventArgs e = new SectionEventArgs(DataEventType.Remove, name,index);
                OnSectionEvent(e);
            }
        }
        /// <summary>
        /// 删除采样数据集
        /// </summary>
        /// <param name="sectionName">采样数据集名称</param>
        public void RemoveDataSection(string sectionName)
        {
            for (int i = _dataSections.Count - 1; i >= 0; i--)
            {
                if (string.Compare(_dataSections[i].Name, sectionName) == 0)
                {
                    
                    _dataSections.RemoveAt(i);
                    SectionEventArgs e = new SectionEventArgs(DataEventType.Remove, sectionName, i);
                    OnSectionEvent(e);
                }
            }
        }
        /// <summary>
        /// 清除所有采样数据集
        /// </summary>
        public void ClearDataSection()
        {
            _dataSections.Clear();
            SectionEventArgs e = new SectionEventArgs(DataEventType.Clear, "", _dataSections.Count);
            OnSectionEvent(e);
        }
        /// <summary>
        /// 获取一个新的不重复的采样数据集内部名称
        /// </summary>
        /// <param name="seed">名称种子</param>
        /// <returns>新内部名称</returns>
        public string GetNewName(string seed)
        {
            int index = 0;
            string name = seed + index.ToString();
            while (Contains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;

        }
        /// <summary>
        /// 获取一个以Section_起始的新的不重复的采样数据集内部名称
        /// </summary>
        /// <returns>新的内部名称</returns>
        public string GetNewName()
        {
            return GetNewName("Section_");
        }
        /// <summary>
        /// 判断是否已有该内部名称的采样数据集
        /// </summary>
        /// <param name="name">内部名称</param>
        /// <returns>是否已有</returns>
        public bool Contains(string name)
        {
            bool contains = false;
            foreach (DataSection dataSection in _dataSections)
            {
                if (string.Equals(dataSection.Name, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        /// <summary>
        /// 检查内部名称是否存在
        /// </summary>
        /// <param name="sectionIndex">采样数据集索引</param>
        /// <param name="name">内部名称</param>
        /// <returns>是否存在</returns>
        public bool CheckName(int sectionIndex, string name)
        {
            bool isColumn = false;
            if (sectionIndex >= 0 && sectionIndex < _dataSections.Count)
            {
                isColumn = _dataSections[sectionIndex].CheckName(name);
            }
            return isColumn;
        }
        /// <summary>
        /// 根据采样数据集内部名称获取采样数据集索引
        /// </summary>
        /// <param name="sectionName">内部名称</param>
        /// <returns>索引，如果找不到则返回-1</returns>
        public int GetSectionIndexBySectionName(string sectionName)
        {
            int sectionIndex = -1;
            for (int i = 0; i < _dataSections.Count; i++)
            {
                if (string.Equals(_dataSections[i].Name, sectionName))
                {
                    sectionIndex = i;
                    break;
                }
            }
            return sectionIndex;
        }
        #endregion

        #region Constant Methods
        /// <summary>
        /// 新增常量
        /// </summary>
        /// <param name="caption">外部名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="values">数值列表</param>
        /// <returns>新生成的常量</returns>
        public ConstantElement AddConstant(string caption, double defaultValue, List<double> values)
        {
            ConstantElement obj = CurrentDataSection.AddConstant(caption,defaultValue,values);
            for (int i = 0; i < _dataSections.Count; i++)
            {
                _dataSections[i].AddConstant(obj.ToString() );
            }
            ConstantArgs e = new ConstantArgs(DataEventType.Add, obj);
            OnConstantEvent(e);
            DataStoreEventArgs e2 = new DataStoreEventArgs(this.CurrentDataSection.Name, CurrentDataSection.DataStores[CurrentDataSection.DataStores.Count - 1], DataEventType.Add);
            OnExprEvent(e2);

            return obj;
        }
        /// <summary>
        /// 根据内部名称删除常量
        /// </summary>
        /// <param name="name"></param>
        public void RemoveConstant(string name)
        {
            if (CurrentDataSection.ConstantRemove(name))
            {
                for (int i = 0; i < _dataSections.Count; i++)
                {
                    _dataSections[i].ConstantRemove(name);
                }
                ConstantArgs e = new ConstantArgs(DataEventType.Remove, null);
                OnConstantEvent(e);
                DataStoreEventArgs e2 = new DataStoreEventArgs(this.CurrentDataSection.Name,null, DataEventType.Remove);
                OnExprEvent(e2);
                
            }
        }
        /// <summary>
        /// 根据索引删除常量
        /// </summary>
        /// <param name="index"></param>
        public void RemoveConstant(int index)
        {
            if (CurrentDataSection.ConstantRemove(index))
            {
                for (int i = 0; i < _dataSections.Count; i++)
                {
                    _dataSections[i].ConstantRemove(index);
                }
                ConstantArgs e = new ConstantArgs(DataEventType.Remove, null);
                OnConstantEvent(e);
                DataStoreEventArgs e2 = new DataStoreEventArgs(this.CurrentDataSection.Name, null, DataEventType.Remove);
                OnExprEvent(e2);
            }
        }
        /// <summary>
        /// 清除所有常量
        /// </summary>
        public void ClearConstant()
        {
            CurrentDataSection.ClearConstants();
            for (int i = 0; i < _dataSections.Count; i++)
            {
                _dataSections[i].ClearConstants();
            }

            ConstantArgs e = new ConstantArgs(DataEventType.Clear, null);
            OnConstantEvent(e);

        }
        #endregion
        
        #region Sensor Methods
        /// <summary>
        /// 在当前模板中新增传感器变量，并在变量集中按照SensorID排序
        /// </summary>
        /// <param name="caption">外部名称</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="decimalCount">小数位数</param>
        /// <param name="calibration">校正值</param>
        /// <param name="unit">单位名称</param>
        /// <param name="count">默认数据集大小</param>
        /// <param name="sensorID">传感器标识码</param>
        /// <returns>新增的传感器变量</returns>
        public DataStore AddSensor(string caption, double maxValue, double minValue, int decimalCount, double calibration,string unit, int count,byte sensorID)
        {
            DataStore obj =  CurrentDataSection.AddSensor(caption, maxValue, minValue, decimalCount, calibration,unit, count,sensorID);
            DataStoreEventArgs e = new DataStoreEventArgs(this.CurrentDataSection.Name, obj, DataEventType.Add);
            OnSensorEvent(e);
            return obj;
        }
        /// <summary>
        /// 在当前模板中新增传感器变量，在变量集中按照先后顺序排序
        /// </summary>
        /// <param name="caption">外部名称</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="decimalCount">小数位数</param>
        /// <param name="calibration">校正值</param>
        /// <param name="unit">单位名称</param>
        /// <param name="count">默认数据集大小</param>
        /// <param name="sensorID">传感器标识</param>
        /// <returns>新增的传感器变量</returns>
        public DataStore AddSensorNoIndex(string caption, double maxValue, double minValue, int decimalCount, double calibration, string unit, int count, byte sensorID)
        {
            DataStore obj = CurrentDataSection.AddSensorNoIndex(caption, maxValue, minValue, decimalCount, calibration, unit, count, sensorID);
            DataStoreEventArgs e = new DataStoreEventArgs(this.CurrentDataSection.Name, obj, DataEventType.Add);
            OnSensorEvent(e);
            return obj;
        }
        /// <summary>
        /// 在运行模板中新增传感器，并在变量集中按照SensorID排序
        /// </summary>
        /// <param name="caption">外部名称</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="decimalCount">小数位数</param>
        /// <param name="calibration">校正值</param>
        /// <param name="unit">单位名称</param>
        /// <param name="count">默认数据集大小</param>
        /// <param name="sensorID">传感器标识</param>
        /// <returns>新增的传感器变量</returns>
        public DataStore AddActiveSensor(string caption, double maxValue, double minValue, int decimalCount, double calibration, string unit, int count, byte sensorID)
        {
            DataStore obj = this._dataSectionActive.AddSensor(caption, maxValue, minValue, decimalCount, calibration, unit, count,sensorID);
            DataStoreEventArgs e = new DataStoreEventArgs(this._dataSectionActive.Name, obj, DataEventType.Add);
            OnSensorEvent(e);
            return obj;
        }
        /// <summary>
        /// 在运行模板中新增传感器变量，在变量集中按照先后顺序排序
        /// </summary>
        /// <param name="caption">外部名称</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="decimalCount">小数位数</param>
        /// <param name="calibration">校正值</param>
        /// <param name="unit">单位名称</param>
        /// <param name="count">默认数据集大小</param>
        /// <param name="sensorID">传感器标识</param>
        /// <returns>新增的传感器变量</returns>
        public DataStore AddActiveSensorNoIndex(string caption, double maxValue, double minValue, int decimalCount, double calibration, string unit, int count, int sensorID)
        {
            DataStore obj = this._dataSectionActive.AddSensorNoIndex(caption, maxValue, minValue, decimalCount, calibration, unit, count, 0);
            DataStoreEventArgs e = new DataStoreEventArgs(this._dataSectionActive.Name, obj, DataEventType.Add);
            OnSensorEvent(e);
            return obj;
        }
        /// <summary>
        /// 删除传感器变量
        /// </summary>
        /// <param name="name">被删除的传感器变量内部名称</param>
        public void RemoveSensor(string name)
        {
            if (CurrentDataSection.SensorRemove(name))
            {
                DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, null, DataEventType.Remove);
                OnSensorEvent(e);
            }
        }
        /// <summary>
        /// 删除传感器变量
        /// </summary>
        /// <param name="index">被删除的传感器变量顺序索引</param>
        public void RemoveSensor(int index)
        {
            if (CurrentDataSection.SensorRemoveAt( index ))
            {
                DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, null, DataEventType.Remove);
                OnSensorEvent(e);
            }
        }
        /// <summary>
        /// 将指定的传感器变量上移一位
        /// </summary>
        /// <param name="index">被移动前的顺序索引</param>
        /// <returns>移动后顺序索引</returns>
        public int MoveUpSensor(int index)
        {
            int newIndex = CurrentDataSection.SensorMoveUp(index);

            if (index != newIndex)
            {
                if (newIndex >= 0 && newIndex < CurrentDataSection.Sensors.Count)
                {
                    DataStore dataStore = CurrentDataSection.Sensors[newIndex];
                    DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, dataStore, DataEventType.MoveUp);
                    OnSensorEvent(e);
                }
            }
            return index;
        }
        /// <summary>
        /// 将指定的传感器变量下移一位
        /// </summary>
        /// <param name="index">被移动前的顺序索引</param>
        /// <returns>移动后顺序索引</returns>
        public int MoveDownSensor(int index)
        {
            int newIndex = CurrentDataSection.SensorMoveDown(index);

            if (index != newIndex)
            {
                if (newIndex >= 0 && newIndex < CurrentDataSection.Sensors.Count)
                {
                    DataStore dataStore = CurrentDataSection.Sensors[newIndex];
                    DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, dataStore, DataEventType.MoveDown);
                    OnSensorEvent(e);
                }
            }
            return index;
        }
        /// <summary>
        /// 清除所有的传感器变量
        /// </summary>
        public void ClearSensor()
        {
            CurrentDataSection.ClearSensors();
            DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, null, DataEventType.Clear);
            OnSensorEvent(e);
        }
        #endregion
        
        #region Expr Methods
        /// <summary>
        /// 在当前模板中新增自定义变量，按照SensorID排序
        /// </summary>
        /// <param name="caption">外部名称</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="decimalCount">小数位数</param>
        /// <param name="calibration">校正值</param>
        /// <param name="expression">表达式</param>
        /// <param name="unit">单位名称</param>
        /// <param name="flowSensorName">跟随变量</param>
        /// <param name="flowStep">滞后行数</param>
        /// <param name="count">默认数据集大小</param>
        /// <returns>新增的变量</returns>
        public DataStore AddDataStore(string caption, double maxValue, double minValue, int decimalCount, double calibration, string expression, string unit,string flowSensorName,int flowStep,int count)
        {
            DataStore obj = CurrentDataSection.AddDataStore(caption, maxValue, minValue, decimalCount, calibration,expression,unit,flowSensorName,flowStep, count);
            for (int i = 0; i < _dataSections.Count; i++)
            {
                _dataSections[i].AddDataStore(obj.ToString());
            }
            DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, obj, DataEventType.Add);
            OnExprEvent(e);
            return obj;
        }
        /// <summary>
        /// 删除自定义变量
        /// </summary>
        /// <param name="name">变量内部名称</param>
        public void RemoveExpr(string name)
        {
            if (CurrentDataSection.DataStoreRemove(name))
            {
                for (int i = 0; i < _dataSections.Count; i++)
                {
                    _dataSections[i].DataStoreRemove(name);
                }
                DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, null, DataEventType.Remove);
                OnExprEvent(e);
            }
        }
        /// <summary>
        /// 删除自定义变量
        /// </summary>
        /// <param name="index">变量顺序索引</param>
        public void RemoveExpr(int index)
        {
            if (CurrentDataSection.DataStoreRemove(index))
            {
                for (int i = 0; i < _dataSections.Count; i++)
                {
                    _dataSections[i].DataStoreRemove(index);
                }
                DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, null, DataEventType.Remove);
                OnExprEvent(e);
            }
        }
        /// <summary>
        /// 自定义变量上移一位
        /// </summary>
        /// <param name="index">被移动前顺序索引</param>
        /// <returns>移动后顺序索引</returns>
        public int MoveUpDataStore(int index)
        {
            int newIndex = CurrentDataSection.DataStoreMoveUp(index);

            if (index != newIndex)
            {
                if (newIndex >= 0 && newIndex < CurrentDataSection.DataStores.Count)
                {
                    DataStore dataStore = CurrentDataSection.DataStores[newIndex];
                    DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, dataStore, DataEventType.MoveUp);
                    OnExprEvent(e);
                }
            }
            return index;
        }
        /// <summary>
        /// 自定义变量下移一位
        /// </summary>
        /// <param name="index">被移动前顺序索引</param>
        /// <returns>移动后顺序索引</returns>
        public int MoveDownDataStore(int index)
        {
            int newIndex = CurrentDataSection.DataStoreMoveDown(index);

            if (index != newIndex)
            {
                if (newIndex >= 0 && newIndex < CurrentDataSection.DataStores.Count)
                {
                    DataStore dataStore = CurrentDataSection.DataStores[newIndex];
                    DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, dataStore, DataEventType.MoveDown);
                    OnExprEvent(e);
                }
            }
            return index;
        }
        /// <summary>
        /// 清除自定义变量
        /// </summary>
        public void ClearExpr()
        {
            CurrentDataSection.ClearExpr();
            for (int i = 0; i < _dataSections.Count; i++)
            {
                _dataSections[i].ClearExpr();
            }
            DataStoreEventArgs e = new DataStoreEventArgs(CurrentDataSection.Name, null, DataEventType.Clear);
            OnExprEvent(e);
            
        }
        #endregion

        #region Serialize
        string DataSections2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _dataSections.Count; i++)
            {
                keyValue.Add(i.ToString(), _dataSections[i].ToString());
            }
            return keyValue.ToString();
        }
        void RestoreDataSections(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _dataSections.Clear();
            _dataSections.Capacity = keyValue.Count;
            for (int i = 0; i < keyValue.Count; i++)
            {
                string key = i.ToString();
                if (keyValue.ContainsKey(key))
                {
                    _dataSections.Add(new DataSection(keyValue.GetValueByKey(key)));
                }

            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns>序列化字符串</returns>
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("Description", _description);
            keyValue.Add("DataSections", DataSections2Str());
            keyValue.Add("DataSectionTemplete", _dataSectionTemplete.ToString());
            keyValue.Add("DataSectionActive", _dataSectionActive.ToString());
            return keyValue.ToString();

        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            _description = keyValue.GetValueByKey("Description");
            RestoreDataSections(keyValue.GetValueByKey("DataSections"));
            _dataSectionTemplete.Parse(keyValue.GetValueByKey("DataSectionTemplete"));
            _dataSectionActive.Parse(keyValue.GetValueByKey("DataSectionActive"));
        }

        #endregion

        #region Get Methods
        /// <summary>
        /// 检查变量内部名称是否在设计模板中存在
        /// </summary>
        /// <param name="name">内部名称</param>
        /// <returns>是否存在</returns>
        public bool CheckName(string name)
        {
            return _dataSectionTemplete.CheckName(name);
        }
        /// <summary>
        /// 从设计模板中获取外部名称
        /// </summary>
        /// <param name="name">内部名称</param>
        /// <returns>外部名称</returns>
        public string GetCaption(string name)
        {
            return _dataSectionTemplete.GetCaption(name);
        }
        /// <summary>
        /// 获取采样数据集顺序索引
        /// </summary>
        /// <param name="name">采样数据集内部名称</param>
        /// <returns>顺序索引</returns>
        public int GetDataSectionIndexByName(string name)
        {
            int index = -1;
            for (int i = 0; i < _dataSections.Count; i++)
            {
                if (string.Equals(_dataSections[i].Name, name))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        /// <summary>
        /// 获取采样数据集
        /// </summary>
        /// <param name="name">内部名称</param>
        /// <returns>采样数据集</returns>
        public DataSection GetDataSectionByName(string name)
        {
            DataSection dataSection = null;
            if (string.Equals(DATASECTION_ACTIVE_NAME, name))
            {
                dataSection = _dataSectionActive;
            }
            else if (string.Equals(DATASECTION_TEMPLETE_NAME, name))
            {
                dataSection = _dataSectionTemplete;
            }
            else
            {
                foreach (DataSection section in _dataSections)
                {
                    if (string.Equals(section.Name, name))
                    {
                        dataSection = section;
                        break;
                    }
                }
            }
            return dataSection;
        }
        /// <summary>
        /// 获取常量
        /// </summary>
        /// <param name="name">内部名称</param>
        /// <returns>获得的常量，找不到会得到一个默认值，非null</returns>
        public ConstantElement GetConstantElement(string name)
        {
            return CurrentDataSection.GetConstantByName(name);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sectionIndex">指定采样数据集顺序索引</param>
        /// <param name="dataStoreName">变量内部名称</param>
        /// <param name="dataIndex">数据顺序索引</param>
        /// <returns>得到的数据，如果找不到，则返回一个默认值，非null</returns>
        public DataElement GetDataElement(int sectionIndex, string dataStoreName, int dataIndex)
        {
            DataElement dataElement = new DataElement();
            if (sectionIndex >= 0 && sectionIndex < _dataSections.Count)
            {
                DataSection dataSection = _dataSections[sectionIndex];
                dataElement = dataSection.GetDataElementByDataIndex(dataStoreName, dataIndex);
            }
            return dataElement;
        }
        /// <summary>
        /// 根据时间索引获取数据
        /// </summary>
        /// <param name="sectionIndex">采样数据集顺序索引</param>
        /// <param name="dataStoreName">变量内部名称</param>
        /// <param name="timeIndex">时间索引</param>
        /// <returns>得到的数据，如果找不到，则返回一个默认值，非null</returns>
        public DataElement GetDataElementByTimeIndex(int sectionIndex, string dataStoreName, int timeIndex)
        {
            DataElement dataElement = new DataElement();
            if (sectionIndex >= 0 && sectionIndex < _dataSections.Count)
            {
                DataSection dataSection = _dataSections[sectionIndex];
                dataElement = dataSection.GetDataElementByTimeIndex(dataStoreName,timeIndex);
            }
            return dataElement;
        }
        /// <summary>
        /// 优先获取常量，其次dataStore中的数据
        /// </summary>
        /// <param name="sectionIndex">采样数据集顺序索引</param>
        /// <param name="name">变量内部名称</param>
        /// <param name="dataIndex">数据顺序索引</param>
        /// <returns>值，默认为0</returns>
        public double GetData(int sectionIndex, string name, int dataIndex)
        {
            double value = 0;
            if (sectionIndex >= 0 && sectionIndex < _dataSections.Count)
            {
                value = _dataSections[sectionIndex].GetData(name, dataIndex);
            }
            return value;
        }
        /// <summary>
        /// 获取数据值
        /// </summary>
        /// <param name="sectionIndex">采样数据集顺序索引</param>
        /// <param name="dataStoreName">变量内部名称</param>
        /// <param name="dataIndex">数据顺序索引</param>
        /// <returns>值，默认为0</returns>
        public double GetDataValue(int sectionIndex, string dataStoreName, int dataIndex)
        {
            DataElement dataElement = GetDataElement(sectionIndex, dataStoreName, dataIndex);
            return dataElement.Value;
        }
        /// <summary>
        /// 获取数据的时间戳
        /// </summary>
        /// <param name="sectionIndex">采样数据集顺序索引</param>
        /// <param name="dataStoreName">变量内部名称</param>
        /// <param name="dataIndex">数据顺序索引</param>
        /// <returns>时间戳，默认为0</returns>
        public double GetDataTimeStamp(int sectionIndex, string dataStoreName, int dataIndex)
        {
            DataElement dataElement = GetDataElement(sectionIndex, dataStoreName, dataIndex);
            return dataElement.TimeStamp;
        }
        /// <summary>
        /// 根据时间索引获取时间戳
        /// </summary>
        /// <param name="sectionIndex">采样数据集顺序索引</param>
        /// <param name="dataStoreName">变量内部名称</param>
        /// <param name="timeIndex">时间索引</param>
        /// <returns>时间戳，默认为0</returns>
        public double GetDataTimeStampByTimeIndex(int sectionIndex, string dataStoreName, int timeIndex)
        {
            DataElement dataElement = GetDataElementByTimeIndex(sectionIndex, dataStoreName, timeIndex);
            return dataElement.TimeStamp;
        }
        /// <summary>
        /// 获取数据的时间索引
        /// </summary>
        /// <param name="sectionIndex">采样数据集顺序索引</param>
        /// <param name="dataStoreName">变量内部名称</param>
        /// <param name="dataIndex">数据顺序索引</param>
        /// <returns>时间索引，默认为0</returns>
        public int GetDataTimeIndex(int sectionIndex, string dataStoreName, int dataIndex)
        {
            DataElement dataElement = GetDataElement(sectionIndex, dataStoreName, dataIndex);
            return dataElement.TimeIndex;
        }
        /// <summary>
        /// 根据时间索引获取数据顺序索引
        /// </summary>
        /// <param name="sectionIndex">采样数据集顺序索引</param>
        /// <param name="dataStoreName">变量内部名称</param>
        /// <param name="timeIndex">时间索引</param>
        /// <returns>数据顺序索引，默认为-1</returns>
        public int GetDataIndexByTimeIndex(int sectionIndex, string dataStoreName, int timeIndex)
        {
            int dataIndex = -1;
            if (sectionIndex >= 0 && sectionIndex < _dataSections.Count)
            {
                DataSection dataSection = _dataSections[sectionIndex];
                DataStore dataStore = dataSection.GetDataStoreByColumnName(dataStoreName);
                if (dataStore != null)
                {
                    dataIndex = dataStore.DataCollection.GetDataIndexByTimeIndex(timeIndex);
                }
            }
            return dataIndex;

        }
        /// <summary>
        /// 根据时间索引获取数据值
        /// </summary>
        /// <param name="sectionIndex">采样数据集顺序索引</param>
        /// <param name="dataStoreName">变量内部名称</param>
        /// <param name="timeIndex">时间索引</param>
        /// <returns>数据值，默认为0</returns>
        public double GetDataValueByTimeIndex(int sectionIndex, string dataStoreName, int timeIndex)
        {
            DataElement dataElement = GetDataElementByTimeIndex(sectionIndex, dataStoreName, timeIndex);
            return dataElement.Value;
        }
        #endregion

        #region Calc Methods
        /// <summary>
        /// 对某次采样重新计算所有的变量
        /// </summary>
        /// <param name="sectionIndex">采样数据集顺序索引</param>
        public void Recalculate(int sectionIndex)
        {
            if (sectionIndex >= 0 && sectionIndex < _dataSections.Count)
            {
                DataSection section = _dataSections[sectionIndex];
                section.ClearExpr();
                //section.ClearConstants();
                //section.ConstantsParse(_dataSectionActive.Constants2Str());
                //section.DataStoresParse(_dataSectionActive.DataStores2Str());
                //section.Rewind();
                int count = section.MaxTimeIndexCount;

                for (int i = 0; i < count; i++)
                {
                    CalculateByTimeIndex(sectionIndex, i, section);
                }
            }
        }
        /// <summary>
        /// 对给定的采样数据集计算某一时间索引的计算变量
        /// </summary>
        /// <param name="sectionIndex">给定的采样数据集顺序索引</param>
        /// <param name="timeIndex">指定的时间顺序</param>
        /// <param name="section">给定的采样数据集</param>
        public void CalculateByTimeIndex(int sectionIndex, int timeIndex, DataSection section)
        {
            DataAnalysis.Formula formula = new DataAnalysis.Formula(sectionIndex, timeIndex, GetDataValueByTimeIndex,GetData, GetDataTimeStampByTimeIndex,GetDataTimeStamp, GetDataIndexByTimeIndex, CheckName);
            for (int i = 0; i < section.DataStores.Count; i++)
            {
                DataStore dataStore = section.DataStores[i];
                if (!section.ConstantContains(dataStore.DataProps.Expression))
                {
                    int step = dataStore.DataProps.FlowStep;
                    DataStore sensor = section.GetDataStoreByColumnName(dataStore.DataProps.FlowSensorName);
                    if (sensor != null)
                    {
                        if (sensor.ExistsByTimeIndex(timeIndex + step))
                        {
                            double value = formula.Calculate(dataStore.DataProps.Expression);
                            dataStore.Add(value, section.TimeLineProps.GetTimeStampByTimeIndex(timeIndex), timeIndex);
                        }
                    }
                }
            }
        }
        #endregion

        #region Replay Methods
        /// <summary>
        /// 设置重放参数
        /// </summary>
        /// <param name="sectionNames">被重放的采样数据集内部名称列表</param>
        /// <param name="rate">重放速率比率</param>
        public void SetReplayProps(List<string> sectionNames, double rate)
        {
            List<ReplayProps> replayPropsList = new List<ReplayProps>();
            foreach (string sectionName in sectionNames)
            {
                DataSection section = GetDataSectionByName(sectionName);
                if (section != null)
                {
                    replayPropsList.Add(new ReplayProps(section.Name, section.MaxTimeIndexCount+1, section.TimeLineProps.Interval));

                }
            }
            _replay.Initialize(replayPropsList, rate);
        }
        #endregion

        #region Statistic Methods
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="method">统计方法</param>
        /// <param name="sectionName">被统计的采样数据集内部名称</param>
        /// <param name="columnName">被统计的变量内部名称</param>
        /// <param name="startTime">起始时间戳</param>
        /// <param name="endTime">结束时间戳</param>
        /// <returns>统计值</returns>
        public double Statistic(DataAnalysis.StatisticTypeDefine method, string sectionName, string columnName, double startTime, double endTime)
        {
            double value = 0;
            DataSection section = GetDataSectionByName(sectionName);
            if (section != null)
            {
                value = section.Statistic(method, columnName, startTime, endTime);
            }
            return value;
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="method">统计方法</param>
        /// <param name="columnName">被统计的变量内部名称</param>
        /// <returns>每次采样数据集获取一个统计值的值列表</returns>
        public List<double> Statistic(DataAnalysis.StatisticTypeDefine method, string columnName)
        {
            List<double> values = new List<double>();
            for (int i = 0; i < _dataSections.Count; i++)
            {
                values.Add(_dataSections[i].Statistic(method, columnName));
            }
            return values;
        }
        /// <summary>
        /// 对所有的采样数据集及所有变量进行统计
        /// </summary>
        /// <param name="method">统计方法</param>
        /// <returns>横向为所有的变量，纵向为每次采样</returns>
        public DataTable Statistic(DataAnalysis.StatisticTypeDefine method)
        {
            DataTable t = new DataTable();
            DataColumn sectionColumn = new DataColumn("SectionIndex", Type.GetType("System.Double"), "", MappingType.Element);
            sectionColumn.Caption = _resourceManager.GetString("SamplingTime");
            t.Columns.Add(sectionColumn);
            for (int i = 0; i < this.CurrentDataSection.Sensors.Count; i++)
            {
                DataColumn column = new DataColumn(CurrentDataSection.Sensors[i].DataProps.Name, Type.GetType("System.Double"), "", MappingType.Element);
                column.Caption = CurrentDataSection.Sensors[i].DataProps.Caption;
                t.Columns.Add(column);
            }
            for (int i = 0; i < CurrentDataSection.DataStores.Count; i++)
            {
                DataColumn column = new DataColumn(CurrentDataSection.DataStores[i].DataProps.Name, Type.GetType("System.Double"), "", MappingType.Element);
                column.Caption = CurrentDataSection.DataStores[i].DataProps.Caption;
                t.Columns.Add(column);
            }
            for (int i = 0; i < _dataSections.Count; i++)
            {
                DataRow newRow = t.NewRow();
                foreach (DataColumn column in t.Columns)
                {
                    if (string.Equals(column.ColumnName, "SectionIndex"))
                    {
                        newRow[column.ColumnName] = i+1;
                    }
                    else
                    {
                        newRow[column.ColumnName] = _dataSections[i].Statistic(method, column.ColumnName);
                    }
                }
                t.Rows.Add(newRow);
            }
            return t;
        }
        #endregion

        #region Event
        /// <summary>
        /// 采样数据集发生变化
        /// </summary>
        public event SectionEventHandler SectionEvent = null;
        protected void OnSectionEvent(SectionEventArgs e)
        {
            if (SectionEvent != null)
            {
                SectionEvent(this, e);
            }
        }
        /// <summary>
        /// 常量发生变化
        /// </summary>
        public event ConstantHandler ConstantEvent = null;
        protected void OnConstantEvent(ConstantArgs e)
        {
            if (ConstantEvent != null)
            {
                ConstantEvent(this, e);
            }
        }
        /// <summary>
        /// 自定义变量发生变化
        /// </summary>
        public event DataStoreEventHandler ExprEvent = null;
        protected void OnExprEvent(DataStoreEventArgs e)
        {
            if (ExprEvent != null)
            {
                ExprEvent(this, e);
            }
        }
        /// <summary>
        /// 传感器变量发生变化
        /// </summary>
        public event DataStoreEventHandler SensorEvent = null;
        protected void OnSensorEvent(DataStoreEventArgs e)
        {
            if (SensorEvent != null)
            {
                SensorEvent(this, e);
            }
        }
        /// <summary>
        /// 数据集发生变化
        /// </summary>
        public event DataCollectionHandler DataCollectionEvent = null;
        protected void OnDataCollectionEvent(DataCollectionEventArgs e)
        {
            if (DataCollectionEvent != null)
            {
                DataCollectionEvent(this, e);
            }
        }
        /// <summary>
        /// 在线离线状态发生变化
        /// </summary>
        public event OffLineHandler OffLineEvent = null;
        protected void OnOffLineEvent(OffLineEventArgs e)
        {
            if (OffLineEvent != null)
            {
                OffLineEvent(this, e);
            }
        }
        /// <summary>
        /// 重放控制器发生变化
        /// </summary>
        public event ReplayHandler ReplayEvent = null;
        protected void OnReplay(ReplayEventArgs e)
        {
            if (ReplayEvent != null)
            {
                ReplayEvent(this, e);
            }
        }
        #endregion
    }
}
