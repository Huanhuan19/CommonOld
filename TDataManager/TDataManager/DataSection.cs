using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Resources;
using System.Reflection;

namespace TDataManager
{
    public class DataSection
    {
        public DataSection()//无参构造函数；
        {
            LoadDefault();
        }
        public DataSection(string name, string caption, string description, double interval, double startValue, double endValue, int count)//多参构造函数
        {
            Initialize(name, caption, description, interval, startValue, endValue, count);
        }
        public DataSection(string value)//一个参数的构造函数；
        {
            LoadDefault();
            Parse(value);//反序列化value;
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("TDataManager.DataSection", Assembly.GetExecutingAssembly());
        //DataAnalysis.Formula _formula;
        List<ConstantElement> _constants = new List<ConstantElement>();
        List<DataStore> _dataStores = new List<DataStore>();
        List<PresetExpr> _presetExprs = new List<PresetExpr>();//定义三个类的序列化数组；
        TimeLineProps _timeLineProps = new TimeLineProps();
        string _name = "";
        string _caption = "";
        string _description = "";
        List<DataStore> _sensors = new List<DataStore>();//定义DataStore类的序列化数组；
        public List<DataStore> Sensors//传感器
        {
            get { return _sensors; }
        }
        public List<ConstantElement> Constants//常数；
        {
            get { return _constants; }
        }
        public List<DataStore> DataStores
        {
            get { return _dataStores; }
        }
        public List<PresetExpr> PresetExprs
        {
            get { return _presetExprs; }
        }
        public TimeLineProps TimeLineProps
        {
            get { return _timeLineProps; }
        }
        public string Name// 内部名称
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Caption// 外部名称
        {
            get { return _caption; }
            set { _caption = value; }
        }
        //public int Capcity
        //{
        //    get { return Math.Max(ExprsCapcity, SensorsCapcity); }
        //}
        //public int ExprsCapcity
        //{
        //    get
        //    {
        //        int capcity = 0;
        //        foreach (DataStore dataStore in _dataStores)
        //        {
        //            capcity = Math.Max(dataStore.Capcity, capcity);
        //        }
        //        return capcity;
        //    }
        //}
        //public int SensorsCapcity
        //{
        //    get
        //    {
        //        int capcity = 0;
        //        foreach (DataStore dataStore in _sensors)
        //        {
        //            capcity = Math.Max(dataStore.Capcity, capcity);
        //        }
        //        return capcity;

        //    }
        //}
        public int Count//DataSection中的ExprsCount和SensorsCount的最大值；
        {
            get { return Math.Max(ExprsCount, SensorsCount); }
        }
        public int ExprsCount//取出_dataStores序列的Count的最大值；
        {
            get
            {
                int count = 0;
                foreach (DataStore dataStore in _dataStores)
                {
                    count = Math.Max(dataStore.Count, count);
                }
                return count;

            }
        }
        public int SensorsCount//_sensors序列的Count最大值；
        {
            get
            {
                int count = 0;
                foreach (DataStore dataStore in _sensors)
                {
                    count = Math.Max(dataStore.Count, count);
                }
                return count;
            }
        }
        public int SensorsTimeIndexCount//_sensors最后一次时间索引；
        {
            get
            {
                int lastTimeIndex = 0;
                for (int i = 0; i < _sensors.Count; i++)
                {
                    if (i == 0)
                    {
                        lastTimeIndex = _sensors[i].Count > 0 ? _sensors[i].LastTimeIndex : -1;
                    }
                    else
                    {
                        lastTimeIndex = Math.Max((_sensors[i].Count > 0 ? _sensors[i].LastTimeIndex : -1), lastTimeIndex);

                    }
                }
                return lastTimeIndex + 1;
            }
        }
        public int ExprsTimeIndexCount//_dataStores最后一次时间索引；
        {
            get
            {
                int lastTimeIndex = 0;
                for (int i = 0; i < _dataStores.Count; i++)
                {
                    if (i == 0)
                    {
                        lastTimeIndex = _dataStores[i].Count > 0 ? _dataStores[i].LastTimeIndex : -1;
                    }
                    else
                    {
                        lastTimeIndex = Math.Max((_dataStores[i].Count > 0 ? _dataStores[i].LastTimeIndex : -1), lastTimeIndex);

                    }
                }
                return lastTimeIndex + 1;
            }
        }
        public int MaxTimeIndexCount//_dataStores和_sensors最后一次时间索引；
        {
            get { return Math.Max(SensorsTimeIndexCount, ExprsTimeIndexCount); }
        }
        public string Description// 表达式
        {
            get { return _description; }
            set { _description = value; }
        }
        public string QuickDescription//字符串_timeLineProps和_resourceManager
        {
            get
            {
                string[] constList = new string[_constants.Count];//常量
                for (int i = 0; i < _constants.Count; i++)
                {
                    constList[i] = _constants[i].QuickDescription;
                }
                string constStr = string.Join(" ", constList);
                string[] dataStoreList = new string[_dataStores.Count];//计算公式
                for (int i = 0; i < _dataStores.Count; i++)
                {
                    dataStoreList[i] = _dataStores[i].QuickDescription;
                }
                string exprStr = string.Join(" ", dataStoreList);
                string[] sensorList = new string[_sensors.Count];//传感器
                for (int i = 0; i < _sensors.Count; i++)
                {
                    sensorList[i] = _sensors[i].QuickDescription;
                }
                string sensorStr = string.Join(" ", sensorList);
                return _caption + ":" + _timeLineProps.QuickDescription + _resourceManager.GetString("Sensor") + ":" + sensorStr + _resourceManager.GetString("Constant") + ":" + constStr + _resourceManager.GetString("Expression") + ":" + exprStr;
            }
        }
        public int LastSensorDataIndex//最后的传感器数据索引
        {
            get
            {
                int index = 0;
                foreach (DataStore dataStore in _sensors)
                {
                    index = Math.Max(dataStore.DataCollection.Count - 1, index);
                }
                return index;
            }
        }
        public int LastExprDataIndex//最后的计算公式索引
        {
            get
            {
                int index = 0;
                foreach (DataStore dataStore in _dataStores)
                {
                    index = Math.Max(dataStore.DataCollection.Count - 1, index);
                }
                return index;
            }
        }
        public List<string> ColumnNames//列的名称
        {
            get
            {
                List<string> list = new List<string>();//datastores类型的DataProps中的Name；
                for (int i = 0; i < _dataStores.Count; i++)
                {
                    list.Add(_dataStores[i].DataProps.Name);
                }
                for (int i = 0; i < _sensors.Count; i++)
                {
                    list.Add(_sensors[i].DataProps.Name);
                }
                return list;
            }
        }
        #endregion

        #region Methods
        void LoadDefault()//初始化无参构造函数；
        {
            _constants.Clear();//ConstantElement
            _dataStores.Clear();//DataStores类型的_dataStores清空
            _presetExprs.Clear();//PresetExpr清空；
            _sensors.Clear();//DataStores类型的_sensors清空；
            _timeLineProps = new TimeLineProps();
            _name = "";
            _caption = "";
            _description = "";
        }
        //初始化一个参数的构造函数；
        void Initialize(string name, string caption, string description, double interval, double startValue, double endValue, int count)
        {
            _constants.Clear();
            _dataStores.Clear();
            _sensors.Clear();
            _presetExprs.Clear();
            _name = name;
            _caption = caption;
            _description = description;
            _timeLineProps.Interval = interval;
            _timeLineProps.StartValue = startValue;
            _timeLineProps.EndValue = endValue;
            SetCount(count);

        }
        public DataTable GetDataTable(bool emptyTable, int dataCount)//返回DataTable类型的含有两个参数 bool类型和int类型的 名称为GetDataTable的函数；
        {
            DataTable t = new DataTable(_name);
            DataColumn timeIndexColumn = new DataColumn(TDataManager.DataManager.TIMEINDEX_NAME, Type.GetType("System.Double"), "", MappingType.Element);
            timeIndexColumn.Caption = GetCaption(TDataManager.DataManager.TIMEINDEX_NAME);
            DataColumn timeStampColumn = new DataColumn(TDataManager.DataManager.TIMESTAMP_NAME, Type.GetType("System.Double"), "", MappingType.Element);
            timeStampColumn.Caption = GetCaption(TDataManager.DataManager.TIMESTAMP_NAME);
            t.Columns.Add(timeIndexColumn);
            t.Columns.Add(timeStampColumn);//添加两列时间索引和时间；
            for (int i = 0; i < _sensors.Count; i++)//添加传感器的列数；
            {
                DataColumn column = new DataColumn(_sensors[i].DataProps.Name, Type.GetType("System.Double"), "", MappingType.Element);

                column.Caption = _sensors[i].DataProps.Caption;
                if (!string.IsNullOrEmpty(_sensors[i].DataProps.Unit))
                {
                    column.Caption += "(" + _sensors[i].DataProps.Unit + ")";
                }
                t.Columns.Add(column);
            }
            for (int i = 0; i < _dataStores.Count; i++)//添加计算公式的列数；
            {
                DataColumn column = new DataColumn(_dataStores[i].DataProps.Name, Type.GetType("System.Double"), "", MappingType.Element);
                column.Caption = _dataStores[i].DataProps.Caption;
                if (!string.IsNullOrEmpty(_dataStores[i].DataProps.Unit))
                {
                    column.Caption += "(" + _dataStores[i].DataProps.Unit + ")";
                }
                t.Columns.Add(column);
            }
            int count = dataCount;
            if (!emptyTable)
            {
                count = MaxTimeIndexCount;
            }
            for (int i = 0; i < count; i++)//添加两列为时间索引和时间的行数；
            {
                DataRow newRow = t.NewRow();
                if (!emptyTable)
                {
                    newRow[TDataManager.DataManager.TIMEINDEX_NAME] = i;
                    newRow[TDataManager.DataManager.TIMESTAMP_NAME] = Math.Round(i * _timeLineProps.Interval, 5);
                }
                t.Rows.Add(newRow);
            }
            if (!emptyTable)//添加传感器和计算公式的行数；
            {
                for (int i = 0; i < _sensors.Count; i++)
                {
                    for (int rowIndex = 0; rowIndex < _sensors[i].Count; rowIndex++)
                    {
                        int timeIndex = _sensors[i].DataCollection.Datas[rowIndex].TimeIndex;
                        if (timeIndex >= 0 && timeIndex < t.Rows.Count)
                        {
                            t.Rows[timeIndex][_sensors[i].DataProps.Name] = _sensors[i].DataCollection.Datas[rowIndex].Value;
                        }
                    }
                }
                for (int i = 0; i < _dataStores.Count; i++)
                {
                    for (int rowIndex = 0; rowIndex < _dataStores[i].Count; rowIndex++)
                    {
                        int timeIndex = _dataStores[i].DataCollection.Datas[rowIndex].TimeIndex;
                        if (timeIndex >= 0 && timeIndex < t.Rows.Count)
                        {
                            t.Rows[timeIndex][_dataStores[i].DataProps.Name] = _dataStores[i].DataCollection.Datas[rowIndex].Value;
                        }
                    }
                }
            }
            return t;
        }
        public DataTable GetDataTable(bool fillData, int dataCount, bool merge)//返回DataTable类型的含有三个参数 bool类型、int类型和bool类型的 名称为GetDataTable的函数；
        {
            DataTable t = new DataTable(_name);
            DataColumn timeIndexColumn = new DataColumn(TDataManager.DataManager.TIMEINDEX_NAME, Type.GetType("System.Double"), "", MappingType.Element);
            timeIndexColumn.Caption = GetCaption(TDataManager.DataManager.TIMEINDEX_NAME);
            DataColumn timeStampColumn = new DataColumn(TDataManager.DataManager.TIMESTAMP_NAME, Type.GetType("System.Double"), "", MappingType.Element);
            timeStampColumn.Caption = GetCaption(TDataManager.DataManager.TIMESTAMP_NAME);
            t.Columns.Add(timeIndexColumn);
            t.Columns.Add(timeStampColumn);
            for (int i = 0; i < _sensors.Count; i++)
            {
                DataColumn column = new DataColumn(_sensors[i].DataProps.Name, Type.GetType("System.Double"), "", MappingType.Element);

                column.Caption = _sensors[i].DataProps.Caption;
                if (!string.IsNullOrEmpty(_sensors[i].DataProps.Unit))
                {
                    column.Caption += "(" + _sensors[i].DataProps.Unit + ")";
                }
                t.Columns.Add(column);
            }
            for (int i = 0; i < _dataStores.Count; i++)
            {
                DataColumn column = new DataColumn(_dataStores[i].DataProps.Name, Type.GetType("System.Double"), "", MappingType.Element);
                column.Caption = _dataStores[i].DataProps.Caption;
                if (!string.IsNullOrEmpty(_dataStores[i].DataProps.Unit))
                {
                    column.Caption += "(" + _dataStores[i].DataProps.Unit + ")";
                }
                t.Columns.Add(column);
            }
            int count = dataCount;
            if (fillData)
            {
                count = MaxTimeIndexCount;
            }
            for (int i = 0; i < count; i++)
            {
                DataRow newRow = t.NewRow();
                newRow[TDataManager.DataManager.TIMEINDEX_NAME] = i;
                newRow[TDataManager.DataManager.TIMESTAMP_NAME] = Math.Round(i * _timeLineProps.Interval, 5);
                //if (fillData)
                //{
                //    for (int j = 0; j < _sensors.Count; j++)
                //    {
                //        if (i >= 0 && i < _sensors[j].DataCollection.Datas.Count)
                //        {
                //            newRow[_sensors[j].DataProps.Name] = _sensors[j].DataCollection.Datas[i].Value;
                //        }
                //    }
                //    for (int j = 0; j < _dataStores.Count; j++)
                //    {
                //        if (i >= 0 && i < _dataStores[j].DataCollection.Datas.Count)
                //        {
                //            newRow[_dataStores[j].DataProps.Name] = _dataStores[j].DataCollection.Datas[i].Value;
                //        }
                //    }
                //}
                t.Rows.Add(newRow);
            }
            for (int i = 0; i < _sensors.Count; i++)
            {
                for (int rowIndex = 0; rowIndex < _sensors[i].Count; rowIndex++)
                {
                    int timeIndex = _sensors[i].DataCollection.Datas[rowIndex].TimeIndex;
                    if (timeIndex >= 0 && timeIndex < t.Rows.Count)
                    {
                        t.Rows[timeIndex][_sensors[i].DataProps.Name] = _sensors[i].DataCollection.Datas[rowIndex].Value;
                    }
                }
            }
            for (int i = 0; i < _dataStores.Count; i++)
            {
                for (int rowIndex = 0; rowIndex < _dataStores[i].Count; rowIndex++)
                {
                    int timeIndex = _dataStores[i].DataCollection.Datas[rowIndex].TimeIndex;
                    if (timeIndex >= 0 && timeIndex < t.Rows.Count)
                    {
                        t.Rows[timeIndex][_dataStores[i].DataProps.Name] = _dataStores[i].DataCollection.Datas[rowIndex].Value;
                    }
                }
            }
            if (merge)//合并有数据的列；
            {
                for (int i = t.Rows.Count - 1; i >= 0; i--)
                {
                    bool isEmpty = true;
                    foreach (DataColumn column in t.Columns)
                    {
                        if (!string.Equals(TDataManager.DataManager.TIMEINDEX_NAME, column.ColumnName) && !string.Equals(TDataManager.DataManager.TIMESTAMP_NAME, column.ColumnName))
                        {
                            double value;
                            if (double.TryParse(t.Rows[i][column.ColumnName].ToString(), out value))
                            {
                                isEmpty = false;
                                break;
                            }
                        }
                    }
                    if (isEmpty)
                    {
                        t.Rows.RemoveAt(i);
                    }
                }
            }
            return t;
        }
        public void SaveToExcel(string filename, byte[] filenBytes, bool merge)//导出到Excel
        {
            DataTable t = GetDataTable(true, Count, merge);
            string tempFile = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllBytes(tempFile, filenBytes);
            ExcelService excelService = new ExcelService(tempFile, filename);
            excelService.InitExcel(true);
            excelService.InitWorkBook();
            excelService.InitSheet(1);
            excelService.InitWorkSheet(1);
            for (int columnIndex = 0; columnIndex < t.Columns.Count; columnIndex++)
            {
                //excelService.WriteDesignationValue(columnIndex + 1, 1, t.Columns[columnIndex].Caption);
                excelService.CreateRow(1);
                excelService.WriterCell(columnIndex, t.Columns[columnIndex].Caption);
            }
            for (int rowIndex = 0; rowIndex < t.Rows.Count; rowIndex++)
            {
                excelService.CreateRow(rowIndex + 2);
                for (int columnIndex = 0; columnIndex < t.Columns.Count; columnIndex++)
                {
                    excelService.WriterCell(columnIndex+1, t.Rows[rowIndex][columnIndex].ToString());
                    //excelService.WriteDesignationValue(columnIndex + 1, rowIndex + 2, t.Rows[rowIndex][columnIndex].ToString());
                }
            }
            excelService.SaveToTemplateAndExit();

        }
        public bool DataExistsByTimeIndex(string columnName, int timeIndex)//数据是否存在；
        {
            bool exists = false;
            DataStore dataStore = GetDataStoreByColumnName(columnName);
            if (dataStore != null)
            {
                exists = dataStore.ExistsByTimeIndex(timeIndex);
            }
            return exists;
        }
        #region DataStore Methods
        public string GetNewDataStoreName(string seed)//获得新的数据存储名称；
        {
            int index = 0;
            string name = seed + index.ToString();
            while (DataStoreContains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;

        }
        public string GetNewDataStoreName()
        {
            return GetNewDataStoreName("data_");
        }
        public bool DataStoreContains(string name)//是否包含名称为name的数据接口名称；
        {
            bool contains = false;
            if (!string.IsNullOrEmpty(name))
            {
                for (int i = 0; i < _dataStores.Count; i++)
                {
                    DataStore dataStore = _dataStores[i];
                    if (string.Equals(dataStore.DataProps.Name.ToLower(), name.ToLower()))
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }
        public int GetDataStoreIndexByName(string name)//获得同名称的索引；
        {
            int index = -1;
            for (int i = 0; i < _dataStores.Count; i++)
            {
                if (string.Equals(_dataStores[i].DataProps.Name.ToLower(), name.ToLower()))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        /// <summary>
        /// Only Expr
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataStore GetDataStoreByName(string name)//由名称获取数据存储；
        {
            DataStore dataStore = null;
            for (int i = 0; i < _dataStores.Count; i++)
            {
                if (string.Equals(_dataStores[i].DataProps.Name.ToLower(), name.ToLower()))
                {
                    dataStore = _dataStores[i];
                    break;
                }
            }
            return dataStore;
        }
        public void SetCount(int count)//重置DataStore的值；
        {
            foreach (DataStore dataStore in _dataStores)
            {
                dataStore.SetCount(count);
            }
            foreach (DataStore dataStore in _sensors)
            {
                dataStore.SetCount(count);
            }
        }

        public int AddData(string name, double value)//增加数据含有两个参数；
        {
            int index = -1;
            int storeIndex = GetDataStoreIndexByName(name);
            if (storeIndex >= 0 && storeIndex < _dataStores.Count)
            {
                DataStore dataStore = _dataStores[storeIndex];
                if (dataStore.CanAdd)
                {
                    int timeIndex = dataStore.Count;
                    double timeStamp = _timeLineProps.GetTimeStampByTimeIndex(timeIndex);
                    index = dataStore.Add(value, timeStamp, timeIndex);
                }
            }

            return index;
        }
        public int AddData(string name, double value, double timeStamp)//增加数据含有三个参数；
        {
            int index = -1;
            int storeIndex = GetDataStoreIndexByName(name);
            if (storeIndex >= 0 && storeIndex < _dataStores.Count)
            {
                DataStore dataStore = _dataStores[storeIndex];
                if (dataStore.CanAdd)
                {
                    int timeIndex = dataStore.Count;
                    index = dataStore.Add(value, timeStamp, timeIndex);
                }
            }
            return index;
        }
        public int DataStoreShink(string name)
        {
            int index = -1;
            int storeIndex = GetDataStoreIndexByName(name);
            if (storeIndex >= 0 && storeIndex < _dataStores.Count)
            {
                index = _dataStores[storeIndex].Shink();

            }
            return index;
        }
        public void DataStoreRemoveAt(string name, int index)//删除；
        {
            int storeIndex = GetDataStoreIndexByName(name);
            if (storeIndex >= 0 && storeIndex < _dataStores.Count)
            {
                _dataStores[storeIndex].RemoveAt(index);
            }
        }
        public bool ArrangeDataByTimeLine()
        {
            bool success = false;
            if (_timeLineProps.Interval > 0 && _timeLineProps.StartValue >= 0 && _timeLineProps.EndValue >= _timeLineProps.StartValue)
            {
                success = true;
                foreach (DataStore dataStore in _dataStores)
                {
                    dataStore.ArrangeDataByTimeLine(_timeLineProps.Interval, _timeLineProps.StartValue, _timeLineProps.EndValue);
                }
            }
            return success;
        }
        //增加DataStore数据；返回int型数据；
        public int AddDataStore(string name, string caption, double maxValue, double minValue, int decimalCount, double calibration, string expression, int count, bool isSensor, string unit, string flowSensorName, int flowStep)
        {
            string realname = DataStoreContains(name) ? GetNewDataStoreName(name) : name;
            DataStore dataStore = new DataStore(count);
            dataStore.DataProps.Name = realname;
            dataStore.DataProps.Caption = caption;
            dataStore.DataProps.Calibration = calibration;
            dataStore.DataProps.Decimal = decimalCount;
            dataStore.DataProps.MaxValue = maxValue;
            dataStore.DataProps.MinValue = minValue;
            dataStore.DataProps.Expression = expression;
            dataStore.DataProps.IsSensor = isSensor;
            dataStore.DataProps.Unit = unit;
            dataStore.DataProps.FlowSensorName = flowSensorName;
            dataStore.DataProps.FlowStep = flowStep;
            dataStore.DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            _dataStores.Add(dataStore);

            return _dataStores.Count - 1;
        }
        //按照索引返回存储的对象；
        public DataStore AddDataStore(string caption, double maxValue, double minValue, int decimalCount, double calibration, string expression, string unit, string flowSensorName, int flowStep, int count)
        {
            int index = AddDataStore(GetNewDataStoreName(), caption, maxValue, minValue, decimalCount, calibration, expression, count, false, unit, flowSensorName, flowStep);
            return _dataStores[index];
        }
        public int AddDataStore()
        {
            string name = GetNewDataStoreName();
            string caption = name;
            return AddDataStore(name, caption, 0, 0, 0, 0, "", 1000, false, "", "", 0);
        }
        public int AddDataStore(string value)
        {
            DataStore dataStore = new DataStore(value);
            dataStore.DataProps.Name = GetNewDataStoreName();
            _dataStores.Add(dataStore);
            dataStore.DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            return _dataStores.Count - 1;
        }
        public int AddDataStore(string value, string seed)
        {
            DataStore dataStore = new DataStore(value);
            dataStore.DataProps.Name = GetNewDataStoreName(seed);
            _dataStores.Add(dataStore);
            dataStore.DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            return _dataStores.Count - 1;
        }
        public bool DataStoreRemove(string name)//按照名称索引是否完成数据的移除；
        {
            bool success = false;
            for (int i = _dataStores.Count - 1; i >= 0; i--)
            {
                if (string.Equals(_dataStores[i].DataProps.Name, name))
                {
                    _dataStores.RemoveAt(i);
                    success = true;
                    break;
                }
            }
            return success;
        }
        public bool DataStoreRemove(int index)//按照次序索引是否完成数据移除；
        {
            bool success = false;
            if (index >= 0 && index < _dataStores.Count)
            {
                success = true;
                _dataStores.RemoveAt(index);
            }
            return success;
        }
        public int DataStoreMoveUp(int index)//数据存储向上移动；
        {
            int newIndex = index;
            if (index > 0 && index < this._dataStores.Count)
            {
                newIndex = index - 1;
                DataStore dataStore = new DataStore(_dataStores[index].ToString());
                _dataStores.RemoveAt(index);
                _dataStores.Insert(newIndex, dataStore);
                dataStore.DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            }
            return newIndex;
        }
        public int DataStoreMoveDown(int index)//数据存储向下移动；
        {
            int newIndex = index;
            if (index >= 0 && index < _dataStores.Count - 1)
            {
                newIndex = index + 1;
                DataStore dataStore = new DataStore(_dataStores[index].ToString());
                _dataStores.RemoveAt(index);
                _dataStores.Insert(newIndex, dataStore);
                dataStore.DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            }
            return newIndex;
        }
        public bool ExprDataExistByTimeIndex(string exprName, int timeIndex)//公式按照时间索引是否存在；
        {
            bool exists = false;
            DataStore expr = GetDataStoreByName(exprName);
            if (expr != null)
            {
                exists = expr.ExistsByTimeIndex(timeIndex);
            }
            return exists;
        }

        public string DataStores2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _dataStores.Count; i++)
            {
                keyValue.Add(i.ToString(), _dataStores[i].ToString());
            }
            return keyValue.ToString();
        }
        public void DataStoresParse(string value)
        {
            _dataStores.Clear();
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _dataStores.Capacity = keyValue.Count;
            for (int i = 0; i < keyValue.Count; i++)
            {
                string key = i.ToString();
                if (keyValue.ContainsKey(key))
                {
                    _dataStores.Add(new DataStore(keyValue.GetValueByKey(key)));
                    _dataStores[_dataStores.Count - 1].DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
                }
            }
        }

        public void DataSection_DataCollectionEvent(object sender, DataCollectionEventArgs e)
        {
            DataCollectionEventArgs a = new DataCollectionEventArgs(e.EventType, e.DataElement, _name, e.ColumnName);
            OnDataCollectionEvent(a);
        }

        //public void DataStoreRewind()
        //{
        //    int count = SensorsCount;
        //    for (int i = 0; i < _dataStores.Count; i++)
        //    {
        //        _dataStores[i].SetCount(count);
        //    }
        //}

        #endregion

        #region Sensor Methods
        public bool SensorContains(string name)//是否包含有Sensor
        {
            bool contains = false;
            if (!string.IsNullOrEmpty(name))
            {
                for (int i = 0; i < _sensors.Count; i++)
                {
                    DataStore dataStore = _sensors[i];
                    if (string.Equals(dataStore.DataProps.Name.ToLower(), name.ToLower()))
                    {
                        contains = true;
                        break;

                    }
                }
            }
            return contains;
        }
        public string GetNewSensorName()//获得传感器的名称；
        {
            string seed = "Sensor_";
            int index = 0;
            string name = seed + index.ToString();
            while (SensorContains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;
        }
        public int AddSensorNoIndex(string name, string caption, double maxValue, double minValue, int decimalCount, double calibration, int count, bool isSensor, string unit, int sensorID)
        {
            string realname = SensorContains(name) ? GetNewSensorName() : name;
            DataStore dataStore = new DataStore(count);
            dataStore.DataProps.Name = realname;
            dataStore.DataProps.Caption = caption;
            dataStore.DataProps.Calibration = calibration;
            dataStore.DataProps.Decimal = decimalCount;
            dataStore.DataProps.MaxValue = maxValue;
            dataStore.DataProps.MinValue = minValue;
            dataStore.DataProps.IsSensor = isSensor;
            dataStore.DataProps.Unit = unit;
            dataStore.DataProps.SensorID = sensorID;
            dataStore.DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            _sensors.Add(dataStore);
            return _sensors.Count - 1;
        }
        //添加传感器；
        public int AddSensor(string name, string caption, double maxValue, double minValue, int decimalCount, double calibration, int count, bool isSensor, string unit, int sensorID)
        {
            string realname = SensorContains(name) ? GetNewSensorName() : name;
            DataStore dataStore = new DataStore(count);
            dataStore.DataProps.Name = realname;
            dataStore.DataProps.Caption = caption;
            dataStore.DataProps.Calibration = calibration;
            dataStore.DataProps.Decimal = decimalCount;
            dataStore.DataProps.MaxValue = maxValue;
            dataStore.DataProps.MinValue = minValue;
            dataStore.DataProps.IsSensor = isSensor;
            dataStore.DataProps.Unit = unit;
            dataStore.DataProps.SensorID = sensorID;
            dataStore.DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            _sensors.Add(dataStore);
            DataStore temp;
            bool ok = false;
            while (!ok)
            {
                ok = true;
                for (int i = _sensors.Count - 1; i > 0; i--)
                {
                    if (_sensors[i].DataProps.SensorID < _sensors[i - 1].DataProps.SensorID)
                    {
                        temp = _sensors[i];
                        _sensors[i] = _sensors[i - 1];
                        _sensors[i - 1] = temp;
                        ok = false;

                    }
                }
            }
            int newIndex = -1;
            for (int i = 0; i < _sensors.Count; i++)
            {
                if (object.Equals(dataStore, _sensors[i]))
                {
                    newIndex = i;
                    break;
                }
            }
            return newIndex;
        }

        public DataStore AddSensorNoIndex(string caption, double maxValue, double minValue, int decimalCount, double calibration, string unit, int count, int sensorID)
        {
            int index = AddSensorNoIndex(GetNewSensorName(), caption, maxValue, minValue, decimalCount, calibration, count, true, unit, sensorID);
            _sensors[_sensors.Count - 1].DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            return _sensors[index];
        }
        public DataStore AddSensor(string caption, double maxValue, double minValue, int decimalCount, double calibration, string unit, int count, int sensorID)
        {
            int index = AddSensor(GetNewSensorName(), caption, maxValue, minValue, decimalCount, calibration, count, true, unit, sensorID);
            _sensors[_sensors.Count - 1].DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            return _sensors[index];
        }
        public bool SensorRemove(string name)//传感器是否移除按照名称；
        {
            bool success = false;
            for (int i = _sensors.Count - 1; i >= 0; i--)
            {
                if (string.Compare(_sensors[i].DataProps.Name, name) == 0)
                {
                    _sensors.RemoveAt(i);
                    success = true;
                    break;
                }
            }
            return success;

        }
        public bool SensorRemoveAt(int index)//传感器是否移除按照索引；
        {
            bool success = false;
            if (index >= 0 && index < _sensors.Count)
            {
                _sensors.RemoveAt(index);
                success = true;
            }
            return success;

        }
        public int SensorMoveUp(int index)
        {
            int newIndex = index;
            if (index > 0 && index < _sensors.Count)
            {
                newIndex = index - 1;
                DataStore dataStore = new DataStore(_sensors[index].ToString());
                _sensors.RemoveAt(index);
                _sensors.Insert(newIndex, dataStore);
                dataStore.DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            }
            return newIndex;
        }
        public int SensorMoveDown(int index)
        {
            int newIndex = index;
            if (index >= 0 && index < _sensors.Count - 1)
            {
                newIndex = index + 1;
                DataStore dataStore = new DataStore(_sensors[index].ToString());
                _sensors.RemoveAt(index);
                _sensors.Insert(newIndex, dataStore);
                dataStore.DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
            }
            return newIndex;
        }
        public DataStore GetSensorByName(string name)
        {
            DataStore dataStore = null;
            foreach (DataStore data in _sensors)
            {
                if (string.Equals(data.DataProps.Name, name))
                {
                    dataStore = data;
                    break;
                }
            }
            return dataStore;
        }
        public DataElement GetSensorDataByIndex(string sensorName, int dataIndex)
        {
            DataElement dataElement = null;
            DataStore sensor = GetSensorByName(sensorName);
            if (sensor != null)
            {
                if (dataIndex >= 0 && dataIndex < sensor.Count)
                {
                    dataElement = sensor.GetDataElementByIndex(dataIndex);
                }
            }
            return dataElement;
        }
        public bool SensorDataExist(string sensorName, int dataIndex)//传感器数据是否存在；
        {
            bool exists = false;
            DataStore sensor = GetSensorByName(sensorName);
            if (sensor != null)
            {
                if (dataIndex >= 0 && dataIndex < sensor.Count)
                {
                    exists = true;
                }
            }
            return exists;
        }
        public bool SensorDataExistByTimeIndex(string sensorName, int timeIndex)
        {
            bool exists = false;
            DataStore sensor = GetSensorByName(sensorName);
            if (sensor != null)
            {
                exists = sensor.ExistsByTimeIndex(timeIndex);
            }
            return exists;
        }
        public void ClearSensors()
        {
            _sensors.Clear();
        }
        public string Sensor2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _sensors.Count; i++)
            {
                keyValue.Add(i.ToString(), _sensors[i].ToString());
            }
            return keyValue.ToString();
        }
        public void SensorParse(string value)
        {
            _sensors.Clear();
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _sensors.Capacity = keyValue.Count;
            for (int i = 0; i < keyValue.Count; i++)
            {
                string key = i.ToString();
                if (keyValue.ContainsKey(key))
                {
                    _sensors.Add(new DataStore(keyValue.GetValueByKey(key)));
                    _sensors[_sensors.Count - 1].DataCollectionEvent += new DataCollectionHandler(DataSection_DataCollectionEvent);
                }
            }
        }
        #endregion

        #region Constant Methods
        public void ConstantRewind()
        {
            for (int i = 0; i < _constants.Count; i++)
            {
                _constants[i].Rewind();
            }
        }
        public string GetNewConstantName(string seed)
        {
            int index = 0;
            string name = seed + index.ToString();
            while (ConstantContains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;

        }
        public string GetNewConstantName()
        {
            return GetNewConstantName("constant_");
        }
        public bool ConstantContains(string name)//包含有常量；
        {
            bool contains = false;
            for (int i = 0; i < _constants.Count; i++)
            {
                ConstantElement constant = _constants[i];
                if (string.Equals(constant.Name, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        public int GetConstantIndexByName(string name)
        {
            int index = -1;
            for (int i = 0; i < _constants.Count; i++)
            {
                if (string.Equals(_constants[i].Name, name))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public ConstantElement GetConstantByName(string name)//由名称返回变量；
        {
            ConstantElement constant;
            int index = GetConstantIndexByName(name);
            if (index >= 0 && index < _constants.Count)
            {
                constant = _constants[index];
            }
            else
            {
                constant = new ConstantElement();
            }
            return constant;
        }
        public void SetConstantDefaultValue(string name, double value)
        {
            int constantIndex = GetConstantIndexByName(name);
            if (constantIndex >= 0 && constantIndex < _dataStores.Count)
            {
                ConstantElement constant = _constants[constantIndex];
                constant.DefaultValue = value;
            }
        }
        public int AddConstant(string name, double value)//增加常量
        {
            int index = -1;
            int constantIndex = GetDataStoreIndexByName(name);
            if (constantIndex >= 0 && constantIndex < _dataStores.Count)
            {
                ConstantElement constant = _constants[constantIndex];
                constant.Add(value);
                DataStore expr = new DataStore();
                expr.DataProps.Name = GetNewDataStoreName();
                expr.DataProps.Expression = constant.Name;
                expr.DataProps.Caption = constant.Caption;
                if (_sensors.Count > 0)
                {
                    expr.DataProps.FlowSensorName = _sensors[0].DataProps.Name;
                }
                _dataStores.Add(expr);
                index = constant.Count - 1;
            }
            return index;
        }
        public int AddConstant(string name, string caption, double defaultValue)//增加常量
        {
            string realname = ConstantContains(name) ? GetNewConstantName(name) : name;
            ConstantElement constant = new ConstantElement(realname, caption, defaultValue);
            _constants.Add(constant);
            DataStore expr = new DataStore();
            expr.DataProps.Name = GetNewDataStoreName();
            expr.DataProps.Expression = constant.Name;
            expr.DataProps.Caption = constant.Caption;
            if (_sensors.Count > 0)
            {
                expr.DataProps.FlowSensorName = _sensors[0].DataProps.Name;
            }
            _dataStores.Add(expr);
            return _constants.Count - 1;
        }
        public ConstantElement AddConstant(string caption, double defaultValue, List<double> values)//增加常量
        {
            string name = GetNewConstantName();
            ConstantElement constant = new ConstantElement(name, caption, defaultValue);
            constant.Values.AddRange(values);
            _constants.Add(constant);
            DataStore expr = new DataStore();
            expr.DataProps.Name = GetNewDataStoreName();
            expr.DataProps.Expression = constant.Name;
            expr.DataProps.Caption = constant.Caption;
            if (_sensors.Count > 0)
            {
                expr.DataProps.FlowSensorName = _sensors[0].DataProps.Name;
            }
            _dataStores.Add(expr);

            return constant;
        }
        public int AddConstant()//增加常量
        {
            string name = GetNewConstantName();
            string caption = name;
            return AddConstant(name, caption, 0);
        }
        public int AddConstant(string value)//增加常量
        {
            ConstantElement constant = new ConstantElement(value);
            constant.Name = GetNewConstantName();
            _constants.Add(constant);
            DataStore expr = new DataStore();
            expr.DataProps.Name = GetNewDataStoreName();
            expr.DataProps.Expression = constant.Name;
            expr.DataProps.Caption = constant.Caption;
            if (_sensors.Count > 0)
            {
                expr.DataProps.FlowSensorName = _sensors[0].DataProps.Name;
            }
            _dataStores.Add(expr);

            return _constants.Count - 1;
        }
        public int AddConstant(string value, string seed)//增加常量
        {
            ConstantElement constant = new ConstantElement(value);
            constant.Name = GetNewConstantName(seed);
            _constants.Add(constant);
            DataStore expr = new DataStore();
            expr.DataProps.Name = GetNewDataStoreName();
            expr.DataProps.Expression = constant.Name;
            expr.DataProps.Caption = constant.Caption;
            if (_sensors.Count > 0)
            {
                expr.DataProps.FlowSensorName = _sensors[0].DataProps.Name;
            }
            _dataStores.Add(expr);
            return _constants.Count - 1;
        }
        public void ConstantRemoveAt(string name, int index)
        {
            int constantIndex = GetConstantIndexByName(name);
            if (constantIndex >= 0 && constantIndex < _constants.Count)
            {
                _constants[constantIndex].RemoveAt(index);
            }
        }
        public bool ConstantRemove(string name)
        {
            bool success = false;
            for (int i = _constants.Count - 1; i >= 0; i--)
            {
                if (string.Equals(_constants[i].Name, name))
                {
                    for (int j = _dataStores.Count - 1; j >= 0; j--)
                    {
                        if (string.Equals(_dataStores[j].DataProps.Expression, _constants[i].Name))
                        {
                            _dataStores.RemoveAt(j);
                        }
                    }
                    _constants.RemoveAt(i);
                    success = true;
                    break;
                }
            }
            return success;
        }
        public bool ConstantRemove(int index)
        {
            bool success = false;
            if (index >= 0 && index < _constants.Count)
            {
                ConstantElement constant = _constants[index];
                for (int i = _dataStores.Count - 1; i >= 0; i--)
                {
                    if (string.Equals(_dataStores[i].DataProps.Expression, constant.Name))
                    {
                        _dataStores.RemoveAt(i);
                    }
                }
                _constants.RemoveAt(index);
                success = true;
            }
            return success;
        }
        public string Constants2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _constants.Count; i++)
            {
                keyValue.Add(i.ToString(), _constants[i].ToString());
            }
            return keyValue.ToString();
        }
        public void ConstantsParse(string value)
        {
            _constants.Clear();
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _constants.Capacity = keyValue.Count;
            for (int i = 0; i < keyValue.Count; i++)
            {
                string key = i.ToString();
                if (keyValue.ContainsKey(key))
                {
                    _constants.Add(new ConstantElement(keyValue.GetValueByKey(key)));
                }
            }
        }
        #endregion

        #region PresetExpr Methods
        public void AddPresetExpr(string caption, string description, string methodName)//添加公式；
        {
            string seed = "Preset_";
            int index = 0;
            string name = seed + index.ToString();
            while (ContainsPresetExpr(name))
            {
                index++;
                name = seed + index.ToString();
            }
            _presetExprs.Add(new PresetExpr(name, caption, description, methodName));
        }
        public void RemovePresetExpr(string name)//移除公式；
        {
            for (int i = _presetExprs.Count - 1; i >= 0; i--)
            {
                if (string.Equals(_presetExprs[i].Name, name))
                {
                    _presetExprs.RemoveAt(i);

                }
            }
        }
        public void ClearPresetExpr()
        {
            _presetExprs.Clear();
        }

        public bool ContainsPresetExpr(string name)//是否包含公式；
        {
            bool contains = false;
            for (int i = 0; i < _presetExprs.Count; i++)
            {
                if (string.Equals(_presetExprs[i].Name, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        public string PresetExpr2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _presetExprs.Count; i++)
            {
                keyValue.Add(i.ToString(), _presetExprs[i].ToString());
            }
            return keyValue.ToString();
        }
        public void PresetExprParse(string value)
        {
            _sensors.Clear();
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            for (int i = 0; i < keyValue.Count; i++)
            {
                string key = i.ToString();
                if (keyValue.ContainsKey(key))
                {
                    _presetExprs.Add(new PresetExpr(keyValue.GetValueByKey(key)));
                }
            }
        }
        #endregion

        public string ToString(bool withoutSensors)//重载ToString；序列化
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("Description", _description);
            keyValue.Add("DataStores", DataStores2Str());
            keyValue.Add("Constants", Constants2Str());
            keyValue.Add("PresetExprs", PresetExpr2Str());
            if (!withoutSensors)
            {
                keyValue.Add("Sensors", Sensor2Str());
            }
            keyValue.Add("TimeLineProps", _timeLineProps.ToString());
            return keyValue.ToString();

        }
        public void Parse(string value, bool withoutSensors)//反序列化；
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            _description = keyValue.GetValueByKey("Description");
            _timeLineProps.Parse(keyValue.GetValueByKey("TimeLineProps"));
            ConstantsParse(keyValue.GetValueByKey("Constants"));
            PresetExprParse(keyValue.GetValueByKey("PresetExprs"));
            if (!withoutSensors)
            {
                SensorParse(keyValue.GetValueByKey("Sensors"));
            }
            DataStoresParse(keyValue.GetValueByKey("DataStores"));
        }
        public override string ToString()
        {
            return ToString(false);
        }
        public void Parse(string value)
        {
            Parse(value, false);
        }
        public void Clear()//清空传感器，常量和公式；
        {
            ClearConstants();
            ClearExpr();
            ClearSensors();
        }
        public void ClearConstants()
        {
            for (int i = _constants.Count - 1; i >= 0; i--)
            {
                string name = _constants[i].Name;
                for (int j = _dataStores.Count - 1; j >= 0; j--)
                {
                    if (string.Equals(_dataStores[j].DataProps.Expression, name))
                    {
                        _dataStores.RemoveAt(j);
                    }
                }
                _constants.RemoveAt(i);
            }
        }
        public void ClearExpr()
        {
            for (int i = _dataStores.Count - 1; i >= 0; i--)
            {
                if (!ConstantContains(_dataStores[i].DataProps.Expression))
                {
                    _dataStores[i].SetCount(_dataStores[i].LastTimeIndex + 1);
                }
            }
        }
        public void Rewind()
        {
            ConstantRewind();
            //DataStoreRewind();
        }
        public bool CheckName(string name)
        {
            return string.Equals(name, TDataManager.DataManager.TIMEINDEX_NAME) || string.Equals(name, TDataManager.DataManager.TIMESTAMP_NAME) ||
                                    ConstantContains(name) || SensorContains(name) || DataStoreContains(name);
        }
        public string GetCaption(string name)
        {
            string caption = "";
            if (!string.IsNullOrEmpty(name))
            {
                if (string.Compare(name.ToLower(), TDataManager.DataManager.TIMEINDEX_NAME.ToLower()) == 0)
                {
                    caption = _resourceManager.GetString("TimeIndex");
                }
                else if (string.Compare(name.ToLower(), TDataManager.DataManager.TIMESTAMP_NAME.ToLower()) == 0)
                {
                    caption = _resourceManager.GetString("TimeStamp");
                }
                else if (ConstantContains(name))
                {
                    ConstantElement constant = GetConstantByName(name);
                    caption = constant.Caption;
                }
                else if (SensorContains(name))
                {
                    DataStore dataStore = GetSensorByName(name);
                    caption = dataStore.DataProps.Caption;
                }
                else if (DataStoreContains(name))
                {
                    DataStore dataStore = GetDataStoreByName(name);
                    caption = dataStore.DataProps.Caption;
                }
            }
            return caption;
        }
        public string GetConstantCaption(string name)
        {
            string caption = "";
            if (ConstantContains(name))
            {
                ConstantElement constant = GetConstantByName(name);
                caption = constant.Caption;
            }
            return caption;
        }
        public DataType GetDataType(string name)
        {
            DataType dataType = DataType.Other;
            if (string.Compare(name.ToLower(), TDataManager.DataManager.TIMEINDEX_NAME.ToLower()) == 0)
            {
                dataType = DataType.TimeIndex;
            }
            else if (string.Compare(name.ToLower(), TDataManager.DataManager.TIMEINDEX_NAME.ToLower()) == 0)
            {
                dataType = DataType.TimeStamp;
            }
            if (ConstantContains(name))
            {
                dataType = DataType.Constant;
            }
            else if (SensorContains(name))
            {
                dataType = DataType.Sensor;
            }
            else if (DataStoreContains(name))
            {
                dataType = DataType.Expr;
            }
            return dataType;
        }
        public double GetData(string name, int index)
        {
            double value = 0;
            if (string.Compare(name.ToLower(), TDataManager.DataManager.TIMEINDEX_NAME.ToLower()) == 0)
            {
                value = index;
            }
            else if (string.Compare(name.ToLower(), TDataManager.DataManager.TIMEINDEX_NAME.ToLower()) == 0)
            {
                value = _timeLineProps.Interval * index;
            }
            if (ConstantContains(name))
            {
                ConstantElement constant = GetConstantByName(name);
                value = constant.CurrentValue;
                constant.Next();
            }
            else if (SensorContains(name))
            {
                DataStore dataStore = GetSensorByName(name);
                value = dataStore.GetDataElementByIndex(index).Value;
            }
            else if (DataStoreContains(name))
            {
                DataStore dataStore = GetDataStoreByName(name);
                value = dataStore.GetDataElementByIndex(index).Value;
            }
            return value;
        }
        /// <summary>
        /// Expr and Sensor
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public DataStore GetDataStoreByColumnName(string columnName)
        {
            DataStore dataStore = null;
            if (SensorContains(columnName))
            {
                dataStore = GetSensorByName(columnName);
            }
            else if (DataStoreContains(columnName))
            {
                dataStore = GetDataStoreByName(columnName);
            }
            return dataStore;
        }
        public DataElement GetDataElementByTimeIndex(string columnName, int timeIndex)
        {
            DataElement dataElement = new DataElement();
            if (string.Equals(columnName, TDataManager.DataManager.TIMEINDEX_NAME))
            {
                dataElement = new DataElement(timeIndex, timeIndex * _timeLineProps.Interval, timeIndex);
            }
            else if (string.Equals(columnName, TDataManager.DataManager.TIMESTAMP_NAME))
            {

                dataElement = new DataElement(timeIndex, timeIndex * _timeLineProps.Interval, timeIndex * _timeLineProps.Interval);
            }
            else if (ConstantContains(columnName))
            {
                ConstantElement constant = GetConstantByName(columnName);
                dataElement = new DataElement(timeIndex, timeIndex * _timeLineProps.Interval, constant.CurrentValue);
                constant.Next();
            }
            else if (SensorContains(columnName))
            {
                DataStore dataStore = GetSensorByName(columnName);
                if (dataStore != null)
                {
                    dataElement = dataStore.DataCollection.GetDataElementByTimeIndex(timeIndex);
                }
            }
            else if (DataStoreContains(columnName))
            {
                DataStore dataStore = GetDataStoreByName(columnName);
                if (dataStore != null)
                {
                    dataElement = dataStore.DataCollection.GetDataElementByTimeIndex(timeIndex);
                }
            }
            return dataElement;
        }
        public DataElement GetDataElementByDataIndex(string columnName, int dataIndex)
        {
            DataElement dataElement = new DataElement();
            if (string.Equals(columnName, TDataManager.DataManager.TIMEINDEX_NAME))
            {
                dataElement = new DataElement(dataIndex, dataIndex * _timeLineProps.Interval, dataIndex);
            }
            else if (string.Equals(columnName, TDataManager.DataManager.TIMESTAMP_NAME))
            {

                dataElement = new DataElement(dataIndex, dataIndex * _timeLineProps.Interval, dataIndex * _timeLineProps.Interval);
            }
            else if (ConstantContains(columnName))
            {
                ConstantElement constant = GetConstantByName(columnName);
                dataElement = new DataElement(dataIndex, dataIndex * _timeLineProps.Interval, constant.CurrentValue);
                constant.Next();
            }
            else if (SensorContains(columnName))
            {
                DataStore dataStore = GetSensorByName(columnName);
                if (dataStore != null)
                {
                    dataElement = dataStore.DataCollection.GetDataElementByIndex(dataIndex);
                }
            }
            else if (DataStoreContains(columnName))
            {
                DataStore dataStore = GetDataStoreByName(columnName);
                if (dataStore != null)
                {
                    dataElement = dataStore.DataCollection.GetDataElementByIndex(dataIndex);
                }
            }
            return dataElement;
        }
        public int CalcCapcity(double seconds)
        {
            int capcity = 0;
            if (_timeLineProps.Interval > 0)
            {
                capcity = (int)Math.Round(seconds / _timeLineProps.Interval, 0);
            }
            return capcity;
        }
        public double Statistic(DataAnalysis.StatisticTypeDefine method, string columnName)
        {
            double value = 0;
            DataStore dataStore = GetDataStoreByColumnName(columnName);
            if (dataStore != null)
            {
                value = dataStore.Statistic(method);
            }
            //if (SensorContains(columnName))
            //{
            //    DataStore dataStore = GetSensorByName(columnName);
            //    if (dataStore != null)
            //    {
            //        value = dataStore.Statistic(method);
            //    }
            //}
            //else if (DataStoreContains(columnName))
            //{
            //    DataStore dataStore = GetDataStoreByName(columnName);
            //    if (dataStore != null)
            //    {
            //        value = dataStore.Statistic(method);
            //    }
            //}
            return value;
        }
        public double Statistic(DataAnalysis.StatisticTypeDefine method, string columnName, double startTime, double endTime)
        {
            double value = 0;
            DataStore dataStore = GetDataStoreByColumnName(columnName);
            if (dataStore != null)
            {
                value = dataStore.Statistic(method, startTime, endTime);
            }
            //if (SensorContains(columnName))
            //{
            //    DataStore dataStore = GetSensorByName(columnName);
            //    if (dataStore != null)
            //    {
            //        value = dataStore.Statistic(method,startTime,endTime);
            //    }
            //}
            //else if (DataStoreContains(columnName))
            //{
            //    DataStore dataStore = GetDataStoreByName(columnName);
            //    if (dataStore != null)
            //    {
            //        value = dataStore.Statistic(method,startTime,endTime);
            //    }
            //}
            return value;
        }
        #endregion

        #region Events
        public event DataCollectionHandler DataCollectionEvent = null;//DataSection类的DataCollectionEvent触发事件；事件传递的参数类型一致；
        protected void OnDataCollectionEvent(DataCollectionEventArgs e)
        {
            if (DataCollectionEvent != null)
            {
                DataCollectionEvent(this, e);
            }
        }
        #endregion
    }
}
