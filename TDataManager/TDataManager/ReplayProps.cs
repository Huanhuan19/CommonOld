using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class ReplayProps
    {
        public ReplayProps()//重放接口；Interval、SectionName和Count
        {
            LoadDefault();
        }
        public ReplayProps(string sectionName,int count,double interval)
        {
            _sectionName = sectionName;
            _count = count;
            _interval = interval;
        }
        public ReplayProps(string value)
        {
            Parse(value);
        }
        #region Props
        string _sectionName;
        int _count;
        double _interval;
        DateTime _startTime;
        public double Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _sectionName = "";
            _count = 0;
            _interval = 0.1;
            _startTime = DateTime.Now;
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("SectionName", _sectionName);
            keyValue.Add("Count", _count.ToString());
            keyValue.Add("Interval", _interval.ToString());

            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _sectionName = keyValue.GetValueByKey("SectionName");
            int.TryParse(keyValue.GetValueByKey("Count"), out _count);
            double.TryParse(keyValue.GetValueByKey("Interval"), out _interval);
        }
        #endregion
    }
}
