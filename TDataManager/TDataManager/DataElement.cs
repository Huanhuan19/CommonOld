using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class DataElement
    {
        public DataElement()//无参构造函数
        {
            LoadDefault();
        }
        /// <summary>
        /// 一个数据
        /// </summary>
        /// <param name="timeIndex">时间索引</param>
        /// <param name="value">数值</param>
        public DataElement(int timeIndex, double value)//两个变量的构造函数timeIndex和value
        {
            _timeIndex = timeIndex;
            _value = value;
            _available = true;
        }
        /// <summary>
        /// 一个数据
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="value">数值</param>
        public DataElement(double timeStamp, double value)//两个变量的构造函数timeStamp和value
        {
            _timeStamp = timeStamp;
            _value = value;
            _available = true;
        }
        /// <summary>
        /// 一个数据
        /// </summary>
        /// <param name="timeIndex">时间索引</param>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="value">数值</param>
        public DataElement(int timeIndex, double timeStamp, double value)//三个变量的构造函数timeIndex、timeStamp和value
        {
            _timeIndex = timeIndex;
            _timeStamp = timeStamp;
            _value = value;
            _available = true;
        }
        /// <summary>
        /// 一个数据
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public DataElement(string value)//一个变量的构造函数
        {
            LoadDefault();
            Parse(value);//反序列化value并把得到的值赋予_available、_timeIndex、_timeStamp、_value
        }
        #region Props
        double _value ;
        double _timeStamp ;
        int _timeIndex;
        bool _available;
        /// <summary>
        /// 数值
        /// </summary>
        public double Value//属性值的封装
        {
            get { return _value; }
            set { if( _available) _value = value; }
        }
        /// <summary>
        /// 时间戳
        /// </summary>
        public double TimeStamp
        {
            get { return _timeStamp; }
            set { if (_available) _timeStamp = value; }
        }
        /// <summary>
        /// 数据索引
        /// </summary>
        public int TimeIndex
        {
            get { return _timeIndex; }
            set { if (_available) _timeIndex = value; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Available
        {
            get { return _available; }
            set { _available = value; }
        }
        /// <summary>
        /// 数据描述
        /// </summary>
        public string QuickDescription//查询描述。
        {
            get { return "DataElement:" + TimeIndex.ToString() + ":" + TimeStamp.ToString() + ":" + Value.ToString() + ":" + Available.ToString(); }
        }
        #endregion

        #region Methods
        void LoadDefault()//加载时数据 数据量的初始化
        {
            _value = 0;
            _timeStamp = -1;
            _timeIndex = -1;
            _available = false;
        }
        /// <summary>
        /// 清除内容
        /// </summary>
        public void Clear()//清除数据为 加载时数据。
        {
            LoadDefault();
        }
        /// <summary>
        /// 序列化，用"_"分隔
        /// </summary>
        /// <returns>序列化字符串</returns>
        public override string ToString()
        {
            string[] strList = new string[4];
            int i=0;
            strList[i++] = _available.ToString();
            strList[i++] = _timeIndex.ToString();
            strList[i++] = _timeStamp.ToString();
            strList[i++] = _value.ToString();
            return string.Join("_", strList);
        }
        /// <summary>
        /// 反序列化，用"_"分隔
        /// </summary>
        /// <param name="value">序列化字符串</param>
        /// <returns>反序列化是否成功</returns>
        public bool Parse(string value)
        {
            bool success = false;
            string[] strList = value.Split('_');
            if (strList.Length == 4)
            {
                success = true;
                int i = 0;
                success &= bool.TryParse(strList[i++], out _available);
                success &= int.TryParse(strList[i++], out _timeIndex);
                success &= double.TryParse(strList[i++], out _timeStamp);
                success &= double.TryParse(strList[i++], out _value);
            }
            return success;
        }
        /// <summary>
        /// 判断两个数据是否相同
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (obj != null)
            {
                DataElement newData = (DataElement)obj;
                equals = string.Equals(newData.ToString(), ToString());
            }
            return equals;
        }
        /// <summary>
        /// 获取Hash值
        /// </summary>
        /// <returns>Hash值</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
