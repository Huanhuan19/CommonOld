using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDisLab.Graph
{
    public class GraphAddPointRecord
    {
        public GraphAddPointRecord(string sectionName, string columnName, TDataManager.DataElement dataElement)
        {
            _sectionName = sectionName;
            _columnName = columnName;
            _dataElement = dataElement;
        }
        #region Pros
        string _sectionName;
        string _columnName;
        TDataManager.DataElement _dataElement;
        public string SectionName
        {
            get { return _sectionName; }
        }
        public string ColumnName
        {
            get { return _columnName; }
        }
        public TDataManager.DataElement DataElement
        {
            get { return _dataElement; }
        }
        #endregion
    }
}
