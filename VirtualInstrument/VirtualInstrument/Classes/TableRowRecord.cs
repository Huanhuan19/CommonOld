using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class TableRowRecord
    {
        public TableRowRecord()
        {
            _sectionName = "";
            _columnName = "";
            _timeIndex = -1;
            _value = 0;
        }
        public TableRowRecord(string sectionName, string columnName, int timeIndex, double value)
        {
            _sectionName = sectionName;
            _columnName = columnName;
            _timeIndex = timeIndex;
            _value = value;
        }

        #region Props
        string _sectionName;
        string _columnName;
        int _timeIndex;
        double _value;
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }

        }
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }
        public int TimeIndex
        {
            get { return _timeIndex; }
            set { _timeIndex = value; }
        }
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        #region Methods
        public void Initialize(string sectionName, string columnName, int timeIndex, double value)
        {
            _sectionName = sectionName;
            _columnName = columnName;
            _timeIndex = timeIndex;
            _value = value;
        }
        #endregion
    }
}
