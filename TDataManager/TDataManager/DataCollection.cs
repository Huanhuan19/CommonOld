using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class DataCollection
    {
        /// <summary>
        /// 数据集
        /// </summary>
        public DataCollection()
        {
            LoadDefault();
        }
        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="count">默认容量</param>
        public DataCollection(int count)
        {
            SetCount(count);
        }
        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public DataCollection(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        public static int DEFAULTCOUNT = 1000;
        List<DataElement> _datas;
        bool _available = false;
        List<DadaIndex> _dataIndexList;
        /// <summary>
        /// 数据
        /// </summary>
        public List<DataElement> Datas
        {
            get { return _datas; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Available
        {
            get { return _available; }
        }

        /// <summary>
        /// 数据总量
        /// </summary>
        public int Count
        {
            get { return _datas.Count; }
        }
        /// <summary>
        /// 最后的值
        /// </summary>
        public double LastValue
        {
            get
            {
                double value = 0;
                if (_datas.Count>0)
                {
                    value = _datas[_datas.Count-1].Value;
                }
                return value;
            }
        }
        /// <summary>
        /// 最后的时间索引
        /// </summary>
        public int LastTimeIndex
        {
            get
            {
                int timeIndex = -1;
                if (_datas.Count > 0)
                {
                    int index = _datas.Count - 1;
                    if (index >= 0 && index < _datas.Count && _datas[index] != null)
                    {
                        timeIndex = _datas[index].TimeIndex;
                    }
                }
                return timeIndex;
            }
        }
        /// <summary>
        /// 数据索引集
        /// </summary>
        public List<DadaIndex> DataIndexList
        {
            get
            {
                return _dataIndexList;
            }
        }
        /// <summary>
        /// 数据索引量
        /// </summary>
        public int DataIndexCount
        {
            get
            {
                return _dataIndexList.Count;
            }
        }
        /// <summary>
        /// 数据集描述
        /// </summary>
        public string QuickDescription
        {
            get
            {
                return ":"+ Count.ToString();

            }
        }
        #endregion

        #region Methods
        void LoadDefault()//默认构造函数。DEFAULTCOUNT=1000；
        {
            _datas = new List<DataElement> (DEFAULTCOUNT);
            _dataIndexList = new List<DadaIndex>(DEFAULTCOUNT);
            _available = true;
        }
        void SetCount(int count)//含一个参数的默认构造函数。
        {
            int realCount = count > 0 ? count : DEFAULTCOUNT;
            _datas = new List<DataElement>(realCount);
            _dataIndexList = new List<DadaIndex>(realCount);
            Initialize(realCount);
            _available = true;
        }
        /// <summary>
        /// 初始化数据集
        /// </summary>
        /// <param name="count">默认容量</param>
        public void Initialize(int count)
        {
            _dataIndexList.Clear();
            _dataIndexList.Capacity = count;            
            for (int i = 0; i < count; i++)
            {
                _dataIndexList.Add(new DadaIndex(-1));
            }
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="timeIndex">时间索引</param>
        /// <returns></returns>
        public int Add(double value, double timeStamp, int timeIndex)
        {

            int index = -1;
            if (_available)
            {
                lock (_datas)
                {
                    _datas.Add(new DataElement(timeIndex, timeStamp, value));
                    index = _datas.Count - 1;
                    if (timeIndex >= 0 && timeIndex < _dataIndexList.Count)
                    {
                        lock (_dataIndexList)
                        {
                            _dataIndexList[timeIndex].Index = index;
                        }
                    }
                }
            }
            return index;
        }
        /// <summary>
        /// 缩小到实际容量
        /// </summary>
        /// <returns></returns>
        public int Shink()
        {
            ArrangeDataByTimeLine(0, _datas.Count);
            return _datas.Count;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="index">被删除索引</param>
        public void RemoveAt(int index)
        {
            if (_available)
            {
                if (index >= 0 && index < _datas.Count)
                {
                    int timeIndex = _datas[index].TimeIndex;
                    if (timeIndex >= 0 && timeIndex < _dataIndexList.Count)
                    {
                        _dataIndexList[timeIndex].Index = -1;
                    }
                    _datas[index].Available = false;
                }
            }
        }
        /// <summary>
        /// TimeStamp优先判断，其次判断TimeIndex,非Available的数据也都去掉了
        /// </summary>
        /// <param name="interval">时间间隔</param>
        /// <param name="startValue">起始时间</param>
        /// <param name="endValue">结束时间</param>
        public void ArrangeDataByTimeLine(double interval,double startValue, double endValue)
        {
            List<DataElement> datas = new List<DataElement>(_datas.Count);
            for (int i = 0; i < _datas.Count; i++)
            {
                if (_datas[i].Available)
                {
                    if (_datas[i].TimeStamp >= 0)
                    {
                        if (_datas[i].TimeStamp >= startValue && _datas[i].TimeStamp <= endValue)
                        {
                            datas.Add(new DataElement(_datas[i].ToString()));
                        }
                    }
                    else if (_datas[i].TimeIndex >= 0)
                    {
                        if (_datas[i].TimeIndex * interval >= startValue && _datas[i].TimeIndex * interval <= endValue)
                        {
                            datas.Add(new DataElement(_datas[i].ToString()));
                        }
                    }
                }
            }
            _datas = datas;
        }
        /// <summary>
        /// 按照时间排列数据
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        public void ArrangeDataByTimeLine(int startIndex, int endIndex)
        {
            List<DataElement> datas = new List<DataElement>(_datas.Count);
            for (int i = 0; i < _datas.Count; i++)
            {

                if (_datas[i].TimeIndex >= 0 && _datas[i].Available)
                {
                    if (_datas[i].TimeIndex >= startIndex && _datas[i].TimeIndex <= endIndex)
                    {
                        datas.Add(new DataElement(_datas[i].ToString()));
                    }
                }

            }
            _datas = datas;
        }
        /// <summary>
        /// 按照数据索引获取数据
        /// </summary>
        /// <param name="dataIndex">数据索引</param>
        /// <returns>数据，如果没找到则为null</returns>
        public DataElement GetDataElementByIndex(int dataIndex)
        {
            DataElement dataElement;
            if (dataIndex >= 0 && dataIndex < _datas.Count)
            {
                dataElement = _datas[dataIndex];
            }
            else
            {
                dataElement = new DataElement();
            }
            return dataElement;
        }
        /// <summary>
        /// 按照时间索引获取数据
        /// </summary>
        /// <param name="timeIndex">时间索引</param>
        /// <returns>数据，未找到则为默认值，不是null</returns>
        public DataElement GetDataElementByTimeIndex(int timeIndex)
        {
            DataElement dataElement = new DataElement();
            if (timeIndex >= 0 && timeIndex < _dataIndexList.Count)
            {
                int index = _dataIndexList[timeIndex].Index;
                if (index >= 0 && index < _datas.Count)
                {
                    dataElement = _datas[index];
                }
            }
            return dataElement;
        }
        /// <summary>
        /// 判断指定时间索引的数据是否存在
        /// </summary>
        /// <param name="timeIndex">时间索引</param>
        /// <returns>是否存在</returns>
        public bool ExistsByTimeIndex(int timeIndex)
        {
            bool exists = false;
            if (timeIndex >= 0 && timeIndex < _dataIndexList.Count)
            {
                exists = _dataIndexList[timeIndex].Available ;
            }
            return exists;
        }
        /// <summary>
        /// 按照时间索引获取数据索引
        /// </summary>
        /// <param name="timeIndex">时间索引</param>
        /// <returns>数据索引</returns>
        public int GetDataIndexByTimeIndex(int timeIndex)
        {
            int dataIndex = -1;
            if (timeIndex >= 0 && timeIndex < _dataIndexList.Count)
            {
                dataIndex = _dataIndexList[timeIndex].Index;
            }
            return dataIndex;
        }
        #endregion

        #region Statistic Methods
        /// <summary>
        /// 对数据集进行统计
        /// </summary>
        /// <param name="method">统计方法</param>
        /// <returns>统计值</returns>
        public double Statistic(DataAnalysis.StatisticTypeDefine method)
        {
            double[] values = new double[_datas.Count];
            for (int i = 0; i < _datas.Count; i++)
            {
                if (i < values.Length)
                {
                    values[i] = _datas[i].Value;
                }
            }
            double value = 0;
            DataAnalysis.Fitting fitting = new DataAnalysis.Fitting();
            switch (method)
            {
                case DataAnalysis.StatisticTypeDefine.Average:
                    value = fitting.Average(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.Maximum:
                    value = fitting.Max(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.Median:
                    value = fitting.Median(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.Minimum:
                    value = fitting.Min(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.StandardError:
                    value = fitting.StandardError(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.Sum:
                    value = fitting.Sum(values);
                    break;
            }
            return value;
        }
        /// <summary>
        /// 对数据集进行统计
        /// </summary>
        /// <param name="method">统计值</param>
        /// <param name="decimalCount">小数位数</param>
        /// <returns>统计值</returns>
        public double Statistic(DataAnalysis.StatisticTypeDefine method,int decimalCount)
        {
            double[] values = new double[_datas.Count];
            for (int i = 0; i < _datas.Count; i++)
            {
                if (i < values.Length)
                {
                    values[i] = _datas[i].Value;
                }
            }
            double value = 0;
            DataAnalysis.Fitting fitting = new DataAnalysis.Fitting();
            switch (method)
            {
                case DataAnalysis.StatisticTypeDefine.Average:
                    value = fitting.Average(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.Maximum:
                    value = fitting.Max(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.Median:
                    value = fitting.Median(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.Minimum:
                    value = fitting.Min(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.StandardError:
                    value = fitting.StandardError(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.Sum:
                    value = fitting.Sum(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.First:
                    value = fitting.First(values);
                    break;
                case DataAnalysis.StatisticTypeDefine.Last:
                    value = fitting.Last(values);
                    break;
            }
            return Math.Round(value,decimalCount );
        }
        /// <summary>
        /// 统计数据集
        /// </summary>
        /// <param name="method">统计方法</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="decimalCount">小数位数</param>
        /// <returns>统计值</returns>
        public double Statistic(DataAnalysis.StatisticTypeDefine method,double startTime,double endTime, int decimalCount)
        {
            List<double> values = new List<double>(_datas.Count);
            for (int i = 0; i < _datas.Count; i++)
            {
                if (_datas[i].TimeStamp >= startTime && _datas[i].TimeStamp <= endTime)
                {
                    values.Add(_datas[i].Value);
                }
            }
            double value = 0;
            DataAnalysis.Fitting fitting = new DataAnalysis.Fitting();
            switch (method)
            {
                case DataAnalysis.StatisticTypeDefine.Average:
                    value = fitting.Average(values.ToArray());
                    break;
                case DataAnalysis.StatisticTypeDefine.Maximum:
                    value = fitting.Max(values.ToArray());
                    break;
                case DataAnalysis.StatisticTypeDefine.Median:
                    value = fitting.Median(values.ToArray());
                    break;
                case DataAnalysis.StatisticTypeDefine.Minimum:
                    value = fitting.Min(values.ToArray());
                    break;
                case DataAnalysis.StatisticTypeDefine.StandardError:
                    value = fitting.StandardError(values.ToArray());
                    break;
                case DataAnalysis.StatisticTypeDefine.Sum:
                    value = fitting.Sum(values.ToArray());
                    break;
                case DataAnalysis.StatisticTypeDefine.First:
                    value = fitting.First(values.ToArray());
                    break;
                case DataAnalysis.StatisticTypeDefine.Last:
                    value = fitting.Last(values.ToArray());
                    break;
                case DataAnalysis.StatisticTypeDefine.Diff:
                    if (endTime - startTime != 0)
                    {
                        value = (fitting.Last(values.ToArray()) - fitting.First(values.ToArray())) / (endTime - startTime);
                    }
                    else
                    {
                        value = 0;
                    }
                    break;
                case DataAnalysis.StatisticTypeDefine.Rang:
                    if (endTime - startTime != 0)
                    {
                        value = endTime - startTime;
                    }
                    else
                    {
                        value = 0;
                    }
                    break;
            }
            return Math.Round(value, decimalCount);
        }
        #endregion

        #region Serialize
        string DataIndexList2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Count", _dataIndexList.Count.ToString());
            for (int i = 0; i < _dataIndexList.Count; i++)
            {
                if (_dataIndexList[i].Available)
                {
                    keyValue.Add(i.ToString(), _dataIndexList[i].ToString());
                }
            }
            return keyValue.ToString();
        }
        //void DataIndexListParse(string value)
        //{
        //    KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
        //    keyValue.Parse(value);
        //    int count;
        //    if (keyValue.ContainsKey("Count"))
        //    {
        //        int.TryParse(keyValue.GetValueByKey("Count"), out count);
        //    }
        //    else
        //    {
        //        count = keyValue.Count;
        //    }
        //    _dataIndexList.Clear();
        //    _dataIndexList.Capacity = count;
        //    //_dataIndexList.Capacity = keyValue.Count;
        //    for (int i = 0; i < count; i++)
        //    {
        //        if (keyValue.ContainsKey(i.ToString()))
        //        {
        //            _dataIndexList.Add(new DadaIndex(keyValue.GetValueByKey(i.ToString())));
        //        }
        //        else
        //        {
        //            _dataIndexList.Add(new DadaIndex( -1) );
        //        }
        //    }
        //}
        //string Datas2Str()
        //{
        //    KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
        //    for (int i = 0; i < _datas.Count; i++)
        //    {
        //        keyValue.Add(i.ToString(), _datas[i].ToString());
        //    }
        //    return keyValue.ToString();
        //}
        //void DatasParse(string value)
        //{
        //    KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
        //    keyValue.Parse(value);
        //    _datas.Clear();
        //    _datas.Capacity = keyValue.Count;
        //    for (int i = 0; i < keyValue.Count; i++)
        //    {
        //        _datas.Add(new DataElement(keyValue.GetValueByKey(i.ToString())));
        //    }
        //}
        void DataIndexListParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            int count;
            if (keyValue.ContainsKey("Count"))
            {
                int.TryParse(keyValue.GetValueByKey("Count"), out count);
            }
            else
            {
                count = keyValue.Count;
            }
            _dataIndexList.Clear();
            _dataIndexList.Capacity = count;
            int lastIndex = -1;
            for (int i = 0; i < keyValue.DataTable.Rows.Count; i++)
            {
                int index;
                if (int.TryParse(keyValue.DataTable.Rows[i]["Key"].ToString(), out index))
                {
                    for (int j = lastIndex + 1; j < index; j++)
                    {
                        _dataIndexList.Add(new DadaIndex(-1));
                    }
                    lastIndex = index;
                    _dataIndexList.Add(new DadaIndex(keyValue.DataTable.Rows[i]["Value"].ToString()));
                }
            }

        }

        string Datas2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _datas.Count; i++)
            {
                if (_datas[i].Available)
                {
                    keyValue.Add(i.ToString(), _datas[i].ToString());
                }
            }
            return keyValue.ToString();
        }
        void DatasParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);////从Xml 序列化的字符串得到DataTable
            _datas.Clear();
            _datas.Capacity = keyValue.Count;
            int lastIndex = -1;
            for (int i = 0; i < keyValue.DataTable.Rows.Count; i++)
            {
                int index;
                int.TryParse(keyValue.DataTable.Rows[i]["Key"].ToString(), out index);
                for (int j = lastIndex +1; j <index; j++)
                {
                    _datas.Add(new DataElement());
                }
                lastIndex = index;
                _datas.Add(new DataElement(keyValue.DataTable.Rows[i]["Value"].ToString()));
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns>序列化字符串</returns>
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("DataIndexList", DataIndexList2Str());
            keyValue.Add("Datas", Datas2Str());
            return keyValue.ToString();
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);//从Xml 序列化的字符串得到DataTable
            DatasParse(keyValue.GetValueByKey("Datas"));
            DataIndexListParse(keyValue.GetValueByKey("DataIndexList"));
        }

        #endregion
    }

}
