using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;

namespace TDataManager
{
    public class TimeLineProps
    {
        public TimeLineProps()//无参构造函数。
        {
            LoadDefault();
        }
        public TimeLineProps(double interval)//含有一个参数的构造函数
        {
            LoadDefault();
            if (interval > 0)
            {
                _interval = interval;
            }
        }
        public TimeLineProps(double interval, double startValue)//含有两个参数的构造函数。
        {
            LoadDefault();
            if (interval > 0)
            {
                _interval = interval;
            }
            _startValue = startValue;
        }
        public TimeLineProps(double interval, double startValue, double endValue)//含有三个参数的构造函数。
        {
            LoadDefault();
            _interval = interval;
            _startValue = startValue;
            _endValue = endValue;
        }
        public TimeLineProps(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("TDataManager.TimeLineProps", Assembly.GetExecutingAssembly());
        double _interval = 0.1;
        double _startValue = 0;
        double _endValue = 0;
        List<DataCountRecord> _dataCountRecords = new List<DataCountRecord>();//定义DataCountRecord序列化_dataCountRecords
        byte _shiftIndex = 0x00;
        DateTime _startTime = DateTime.MinValue;
        DateTime _lastTime = DateTime.MinValue;
        static int AUTO_COUNT_VALUE = 3;//实际送入的值个数超过应该送入的值个数
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value;  }
        }
        public DateTime LastTime
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }
        public List<DataCountRecord> DataCountRecords
        {
            get { return _dataCountRecords; }
        }
        public int MaxCount//最大的数；
        {
            get 
            {
                int count = 0;
                for (int i = 0; i < _dataCountRecords.Count; i++)
                {
                    if (i == 0)
                    {
                        count = _dataCountRecords[i].Count;
                    }
                    else
                    {
                        count = Math.Max(count, _dataCountRecords[i].Count);
                    }
                }
                return count; 
            }
        }
        public int NewTimeIndex//新的实验的时刻索引；
        {
            get
            {

                //return Math.Max(0, MaxCount -1);
                TimeSpan span = _lastTime - _startTime;
                int index= (int)Math.Round(span.TotalSeconds / _interval, 0)-1;
                return index < 0 ? 0 : index;

            }
        }
        public double NewTimeStamp//新的实验的时间间隔索引；
        {
            get
            {
                //return NewTimeIndex * _interval;
                return NewTimeIndex* _interval;
            }
        }
        public bool AutoCount//是否超数量AUTO_COUNT_VALUE；
        {
            get 
            { 
                TimeSpan span = _lastTime - _startTime;
                return ((int)Math.Round(span.TotalSeconds / _interval, 0)-MaxCount) > AUTO_COUNT_VALUE;
            }
        }
        public double Interval//间隔
        {
            get { return _interval; }
            set
            {
                if (value > 0)
                {
                    _interval = value;
                }
            }
        }
        public double Frequency//，频率；
        {
            get 
            {
                double frequency = 0;
                if (_interval > 0)
                {
                    frequency = 1 / _interval;
                }
                return frequency;
            }
        }
        public double StartValue
        {
            get { return _startValue; }
            set {  _startValue = value; }
        }
        public double EndValue
        {
            get { return _endValue; }
            set {  _endValue = value; }
        }
        public byte ShiftIndex
        {
            get { return _shiftIndex; }
            set { _shiftIndex = value; }
        }
        public int StartIndex
        {
            get
            {
                int index = 0;
                if (_interval > 0)
                {
                    index = (int)Math.Round(_startValue / _interval, 0);
                }
                return index;
            }
        }
        public int EndIndex
        {
            get
            {
                int index = 0;
                if (_interval > 0)
                {
                    index = (int)Math.Round(_endValue / _interval, 0);
                }
                return index;
            }
        }
        public string QuickDescription
        {
            get
            {
                return _resourceManager.GetString("Interval" )+ ":" + _interval.ToString();
            }
        }
        #endregion

        #region Methods
        void LoadDefault()//初始化数据
        {
            _interval = 0.1;
            _startValue = 0;
            _endValue = 0;
            _dataCountRecords.Clear();
            _startTime = DateTime.MinValue;
            _shiftIndex = 0x05;
        }
        public void Prepair()//修复时间；
        {
            _startTime = DateTime.Now;
            _lastTime = DateTime.Now;
            _dataCountRecords.Clear();
        }
        bool ContainsIndex(int index)//是否包含索引
        {
            bool contains = false;
            if (_dataCountRecords.Count > 0)
            {
                List<DataCountRecord> dataCountRecords = new List<DataCountRecord>(_dataCountRecords.Count);
                dataCountRecords.AddRange(_dataCountRecords);
                for (int i = 0; i < dataCountRecords.Count; i++)
                {
                    DataCountRecord record = dataCountRecords[i];
                    if (record.Index == index)
                    {
                        contains = true;
                        break;
                    }

                }
            }
            return contains;
        }
        int GetRecordIndex(int index)//获得记录的索引；
        {
            int recordIndex = -1;
            if (_dataCountRecords.Count > 0)
            {
                List<DataCountRecord> dataCountRecords = new List<DataCountRecord>(_dataCountRecords.Count);
                dataCountRecords.AddRange(_dataCountRecords);
                for (int i = 0; i < dataCountRecords.Count; i++)
                {
                    if (dataCountRecords[i].Index == index)
                    {
                        recordIndex = i;
                        break;
                    }
                }
            }
            return recordIndex;
        }
        public void AddColumnCount(int index)//增加列的数据；
        {
            if (ContainsIndex(index))
            {
                int recordIndex = GetRecordIndex(index);
                _dataCountRecords[recordIndex].Count += 1;
            }
            else
            {
                _dataCountRecords.Add(new DataCountRecord(index, 1));
            }
            _lastTime = DateTime.Now;
        }
        public int GetTimeIndexByTime(DateTime time)//时间的索引；
        {
            int index = 0;
            TimeSpan span = time - _startTime;
            if (_interval != 0)
            {
                index = Math.Abs((int)Math.Round(span.TotalSeconds / _interval, 0));
            }
            return index;
        }
        public int GetTimeIndex(DateTime time)
        {
            return AutoCount ? GetTimeIndexByTime(time ) :(MaxCount-1);
        }
        public double GetTimeStampByTimeIndex(int timeIndex)
        {
            double result = 0;
                result = _interval * timeIndex;
            return result;
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("ShiftIndex", _shiftIndex.ToString());
            keyValue.Add("Interval", _interval.ToString());
            keyValue.Add("StartValue", _startValue.ToString());
            keyValue.Add("EndValue", _endValue.ToString());
            keyValue.Add("StartTime", _startTime.ToShortTimeString());
            return keyValue.ToString();
        }
        public void Parse(string value)//反序列化value
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            byte.TryParse(keyValue.GetValueByKey("ShiftIndex"), out _shiftIndex);
            double interval;
            double.TryParse(keyValue.GetValueByKey("Interval"), out interval);
            if (interval > 0)
            {
                _interval = interval;
            }
            double.TryParse(keyValue.GetValueByKey("StartValue"), out _startValue);
            double.TryParse(keyValue.GetValueByKey("EndValue"), out _endValue);
            DateTime.TryParse(keyValue.GetValueByKey("StartTime"), out _startTime);
        }
        #endregion
    }
}
