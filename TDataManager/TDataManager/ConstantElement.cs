using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class ConstantElement//常量元素；
    {
        public ConstantElement()
        {
            LoadDefault();
        }
        /// <summary>
        ///  常量定义
        /// </summary>
        /// <param name="name">内部名称</param>
        /// <param name="caption">外部名称</param>
        /// <param name="defaultValue">默认值</param>
        public ConstantElement(string name, string caption, double defaultValue)
        {
            Initialize(name, caption, defaultValue);
        }
        /// <summary>
        /// 常量定义
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public ConstantElement(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        string _name;
        string _caption;
        double _defaultValue;
        List<double> _values = new List<double>();
        //bool _available;
        int _index;
        /// <summary>
        /// 内部名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 外部名称
        /// </summary>
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }
        /// <summary>
        /// 默认值
        /// </summary>
        public double DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                _defaultValue = value;
                ConstantArgs e = new ConstantArgs(DataEventType.Modify, this);
                OnConstantEvent(e);
            }
        }
        /// <summary>
        /// 值列表
        /// </summary>
        public List<double> Values
        {
            get { return _values; }
        }
        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        {
            get
            {
                return _index;
            }
        }
        //public bool Available
        //{
        //    get { return _available; }
        //}
        /// <summary>
        /// 值数量
        /// </summary>
        public int Count
        {
            get { return _values.Count; }
        }
        /// <summary>
        /// 当前值
        /// </summary>
        public double CurrentValue
        {
            get
            {
                double value = _defaultValue;
                if (_values.Count > 0)
                {
                    if (_index >= 0 && _index < _values.Count)
                    {
                        value = _values[_index];
                    }
                }
                return value;
            }
        }
        /// <summary>
        /// 前一个值
        /// </summary>
        public double PrevValue
        {
            get
            {
                double value = _defaultValue;
                if (_values.Count > 0)
                {
                    if (_index >= 0 && _index < _values.Count)
                    {
                        value = _index == 0 ? _values[_index] : _values[_index - 1];
                    }
                }
                return value;
            }
        }
        /// <summary>
        /// 后一个值
        /// </summary>
        public double NextValue
        {
            get
            {
                double value = _defaultValue;
                if (_values.Count > 0)
                {
                    if (_index >= 0 && _index < _values.Count)
                    {
                        value = _index == _values.Count - 1 ? _values[_index] : _values[_index + 1];
                    }
                }
                return value;
            }
        }
        /// <summary>
        /// 值列表转换为字符串
        /// </summary>
        public string ValuesStr
        {
            get
            {
                string[] strList = new string[_values.Count];
                for (int i = 0; i < _values.Count; i++)
                {
                    strList[i] = _values[i].ToString();
                }
                return string.Join(" ", strList);
            }
        }
        /// <summary>
        /// 常量描述
        /// </summary>
        public string QuickDescription
        {
            get
            {
                return "Constant:"+_name + ":" + _caption + ":" + _defaultValue.ToString() + ":" + ValuesStr;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// 常量发生变化时触发
        /// </summary>
        public event ConstantHandler ConstantEvent = null;
        protected void OnConstantEvent(ConstantArgs e)
        {
            if (ConstantEvent != null)
            {
                ConstantEvent(this, e);
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _caption = "";
            _values.Clear();
            _defaultValue = 0;
            _index = 0;
            //_available = false;
            ConstantArgs e = new ConstantArgs(DataEventType.Clear, this);
            OnConstantEvent(e);

        }
        void Initialize(string name, string caption, double defaultValue)
        {
            _name = name;
            _caption = caption;
            _defaultValue = defaultValue;
            _values.Clear();
            _index = 0;
            //available = true;
            ConstantArgs e = new ConstantArgs(DataEventType.Clear, this);
            OnConstantEvent(e);

        }
        /// <summary>
        /// 增加常量
        /// </summary>
        /// <param name="value">常量值</param>
        public void Add(double value)
        {
            _values.Add(value);
            ConstantArgs e = new ConstantArgs(DataEventType.Add, this);
            OnConstantEvent(e);

        }
        /// <summary>
        /// 删除常量
        /// </summary>
        /// <param name="index">列表索引</param>
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < _values.Count)
            {
                _values.RemoveAt(index);
                ConstantArgs e = new ConstantArgs(DataEventType.Remove, this);
                OnConstantEvent(e);
            }
        }
        /// <summary>
        /// 将指定索引的常量上升一个位置
        /// </summary>
        /// <param name="index">被移动前的索引</param>
        /// <returns>移动后索引</returns>
        public int MoveUp(int index)
        {
            int newIndex = index;
            if (index > 0 && index < _values.Count)
            {
                double value = _values[index];
                _values.RemoveAt(index);
                _values.Insert(index - 1, value);
                newIndex = index - 1;
            }
            ConstantArgs e = new ConstantArgs(DataEventType.MoveUp, this);
            OnConstantEvent(e);

            return newIndex;
        }
        /// <summary>
        /// 将指定索引的常量下降一个位置
        /// </summary>
        /// <param name="index">移动前的索引</param>
        /// <returns>移动后索引</returns>
        public int MoveDown(int index)
        {
            int newIndex = index;
            if (index >= 0 && index < _values.Count - 1)
            {
                double value = _values[index];
                _values.RemoveAt(index);
                _values.Insert(index + 1, value);
                newIndex = index + 1;
                     
            }
            ConstantArgs e = new ConstantArgs(DataEventType.MoveDown, this);
            OnConstantEvent(e);

            return newIndex;
        }
        /// <summary>
        /// 当前位置下移一个位置
        /// </summary>
        public void Next()
        {
            if (_values.Count > 0)
            {
                _index++;
                if (_index >= _values.Count)
                {
                    _index = _values.Count - 1;
                }
            }
            ConstantArgs e = new ConstantArgs(DataEventType.Move, this);
            OnConstantEvent(e);
        }
        /// <summary>
        /// 将下一个值强行指定为某一个值
        /// </summary>
        /// <param name="value">新的指定的值</param>
        public void SetNextValue(double value)
        {
            if (_values.Count <= 0)
            {
                _defaultValue = value;
            }
            else
            {
                int index = _index+1;
                if (index >= _values.Count)
                {
                    index = _values.Count - 1;
                }
                if (index >= 0 && index < _values.Count)
                {
                    _values[index] = value;
                }
            }
            ConstantArgs e = new ConstantArgs(DataEventType.Modify, this);
            OnConstantEvent(e);

        }
        /// <summary>
        /// 将当前值的索引归零
        /// </summary>
        public void Rewind()
        {
            _index = 0;
            ConstantArgs e = new ConstantArgs(DataEventType.Move, this);
            OnConstantEvent(e);
        }
        /// <summary>
        /// 将列表清空
        /// </summary>
        public void Clear()
        {
            _values.Clear();
            Rewind();
        }
        /// <summary>
        /// 将列表序列化
        /// </summary>
        /// <returns>序列化字符串</returns>
        string Values2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _values.Count; i++)
            {
                keyValue.Add(i.ToString(), _values[i].ToString());
            }
            return keyValue.ToString();
        }
        /// <summary>
        /// 列表反序列化
        /// </summary>
        /// <param name="value">序列化字符串</param>
        void ValuesParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _values.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                double result;
                double.TryParse(keyValue.GetValueByKey(i.ToString()), out result);
                _values.Add(result);
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns>序列化字符串</returns>
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            //keyValue.Add("Index", _index.ToString());
            keyValue.Add("DefaultValue", _defaultValue.ToString());
            keyValue.Add("Values", Values2Str());
            //keyValue.Add("Available", _available.ToString());
            return keyValue.ToString();

        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            //int.TryParse(keyValue.GetValueByKey("Index"), out _index);
            double.TryParse(keyValue.GetValueByKey("DefaultValue"), out _defaultValue);
            //bool.TryParse(keyValue.GetValueByKey("Available"), out _available);
            ValuesParse(keyValue.GetValueByKey("Values"));
            ConstantArgs e = new ConstantArgs(DataEventType.Modify, this);
            OnConstantEvent(e);

        }
        #endregion
    }

}
