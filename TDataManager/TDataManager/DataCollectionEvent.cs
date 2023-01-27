using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class DataCollectionEventArgs : EventArgs//DataCollectionEvent的响应对象e；含有四个参数的构造函数；
    {
        public DataCollectionEventArgs(DataEventType eventType, DataElement dataElement, string sectionName, string columnName)
        {
            _eventType = eventType;
            _dataElement = dataElement;
            _sectionName = sectionName;
            _columnName = columnName;
        }
        #region Props
        DataEventType _eventType;
        DataElement _dataElement;
        string _sectionName;
        string _columnName;

        public DataElement DataElement//数据元素；
        {
            get { return _dataElement; }
        }
        public DataEventType EventType//枚举的定义
        {
            get { return _eventType; }
        }
        public string SectionName//片段名称；
        {
            get { return _sectionName; }
        }
        public string ColumnName//列名；
        {
            get { return _columnName; }
        }
        #endregion
    }

    public delegate void DataCollectionHandler( object sender,DataCollectionEventArgs e );
}
