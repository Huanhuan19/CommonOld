using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager//DataStore的触发事件；
{
    
    public class DataStoreEventArgs : EventArgs//含有三个参数的构造函数；
    {
        public DataStoreEventArgs(string sectionName,DataStore dataStore, DataEventType eventType)
        {
            _sectionName = sectionName;
            _dataStore = dataStore;
            _eventType = eventType;
        }
        #region Props
        string _sectionName;
        
        DataStore _dataStore;
        DataEventType _eventType;
        public string SectionName
        {
            get { return _sectionName; }
        }
        public DataStore DatStore
        {
            get { return _dataStore; }
        }
        public DataEventType EventType
        {
            get { return _eventType; }
        }
        #endregion
    }

    public delegate void DataStoreEventHandler(object sender,DataStoreEventArgs e );
}
