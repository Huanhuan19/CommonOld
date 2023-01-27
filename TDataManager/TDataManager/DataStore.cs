using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDataManager
{
    public class DataStore
    {
        public DataStore()//数据存储的默认构造函数；
        {
            LoadDefault();
        }
        public DataStore( int count)// 设置存储空间的一个参数的构造函数；
        {
            _dataProps = new DataProps();
            _dataCollection = new DataCollection(count);
        }
        public DataStore(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        DataProps _dataProps;
        DataCollection _dataCollection;
        public DataProps DataProps
        {
            get { return _dataProps; }
        }
        public DataCollection DataCollection
        {
            get { return _dataCollection; }
        }
        public int Count
        {
            get { return _dataCollection.Count; }
        }
        public bool CanAdd
        {
            get { return _dataCollection.Available; }
        }
        public string QuickDescription
        {
            get { return DataProps.QuickDescription + " " + DataCollection.QuickDescription; }
        }
        public int Capcity
        {
            get { return _dataCollection.Datas.Capacity; }
        }
        public int LastTimeIndex
        {
            get { return _dataCollection.LastTimeIndex; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _dataProps = new DataProps();
            _dataCollection = new DataCollection();
        }
        public void SetCount(int count)
        {
            _dataCollection = new DataCollection(count);
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("DataProps", _dataProps.ToString());
            keyValue.Add("DataCollection", _dataCollection.ToString());
            return keyValue.ToString();
        }
        public bool Parse(string value)//反序列化；
        {
            bool success = false;
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            if (keyValue.ContainsKey("DataProps") && keyValue.ContainsKey("DataCollection" ))
            {
                success = true;
                _dataProps.Parse(keyValue.GetValueByKey("DataProps"));
                _dataCollection.Parse(keyValue.GetValueByKey("DataCollection"));
            }
            return success;
        }
        public int Add(double value,double timeStamp,int timeIndex)
        {
            int index = -1;
            if (_dataCollection.Available)
            {
                if (_dataProps.OnlyDifference)//需要判断是不是变化的数据，没变化的数据不增加的
                {
                    if (value != _dataCollection.LastValue )//第一个如果刚好是0，就不会增加进去的
                    {
                        index = _dataCollection.Add(Math.Round(value-_dataProps.Calibration, _dataProps.Decimal), timeStamp, timeIndex);
                        DataCollectionEventArgs e = new DataCollectionEventArgs(DataEventType.Add, _dataCollection.Datas[index], "", _dataProps.Name);
                        OnDataCollectionEvent(e);//数据增加开始触发。DataCollectionEvent
                    }
                }
                else
                {
                    index = _dataCollection.Add(Math.Round(value - _dataProps.Calibration, _dataProps.Decimal), timeStamp, timeIndex);
                    DataCollectionEventArgs e = new DataCollectionEventArgs(DataEventType.Add, _dataCollection.Datas[index], "", _dataProps.Name);
                    OnDataCollectionEvent(e);//数据增加开始触发DataCollectionEvent
                }
            }
            return index;
        }
        public int Shink()
        {
            int count = _dataCollection.Shink();
            DataCollectionEventArgs e = new DataCollectionEventArgs(DataEventType.Shink, null, "", _dataProps.Name);
            OnDataCollectionEvent(e);//触发DataCollectionEvent
            return count;

        }
        public void RemoveAt(int index)
        {
            _dataCollection.RemoveAt(index);//删除数据索引值为index的数据。
            DataCollectionEventArgs e = new DataCollectionEventArgs(DataEventType.Remove, null, "", _dataProps.Name);
            OnDataCollectionEvent(e);
        }
        public void ArrangeDataByTimeLine(double interval, double startValue, double endValue)//TimeStamp优先判断，其次判断TimeIndex,非Available的数据也都去掉了
        {
            _dataCollection.ArrangeDataByTimeLine(interval, startValue, endValue);
            DataCollectionEventArgs e = new DataCollectionEventArgs(DataEventType.Move, null, "", _dataProps.Name);
            OnDataCollectionEvent(e);
        }
        public DataElement GetDataElementByIndex(int dataIndex)//按照数据索引获取数据
        {
            return _dataCollection.GetDataElementByIndex(dataIndex);
        }
        public double Statistic(DataAnalysis.StatisticTypeDefine method)// 对数据集进行统计
        {
            return _dataCollection.Statistic(method,_dataProps.Decimal);
        }
        public double Statistic(DataAnalysis.StatisticTypeDefine method, double startTime, double endTime)// 统计数据集
        {
            return _dataCollection.Statistic(method,startTime,endTime, _dataProps.Decimal);
        }
        public bool ExistsByTimeIndex(int timeIndex)// 判断指定时间索引的数据是否存在
        {
            return _dataCollection.ExistsByTimeIndex(timeIndex);
        }
        #endregion

        #region Events
        public event DataCollectionHandler DataCollectionEvent = null;//DataStore类的DataCollectionEvent触发事件；事件传递的参数类型一致；
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
