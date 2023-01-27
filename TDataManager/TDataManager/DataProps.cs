using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class DataProps
    {
        /// <summary>
        /// 采样数据集属性
        /// </summary>
        public DataProps()//初始化数据值；
        {
            LoadDefault();
        }
        /// <summary>
        /// 采样数据集属性
        /// </summary>
        /// <param name="name">内部名称</param>
        /// <param name="caption">外部名称</param>
        /// <param name="expression">表达式</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="decimalcount">小数位数</param>
        /// <param name="calibration">校正值</param>
        /// <param name="onlyDifference">只接受与上一个值不同的值</param>
        /// <param name="isSensor">是传感器变量</param>
        /// <param name="unit">单位名称</param>
        /// <param name="flowSensorName">跟随变量</param>
        /// <param name="flowStep">滞后行数</param>
        public DataProps(string name, string caption,string expression, double maxValue, double minValue, int decimalcount, double calibration,bool onlyDifference,bool isSensor,string unit,string flowSensorName,int flowStep)
        {
            _name = name;
            _caption = caption;
            _maxValue = maxValue;
            _minValue = minValue;
            _decimal = decimalcount;
            _calibration = calibration;
            _expression = expression;
            _onlyDifference = onlyDifference;
            _isSensor = isSensor;
            _unit = unit;
            _flowSensorName = flowSensorName;
            _flowStep = flowStep;
            _sensorID = 0x00;
            _shiftIndex = 0x00;
            
        }
        /// <summary>
        /// 采样数据集属性
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public DataProps(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        string _name;
        string _caption;
        string _expression;
        double _maxValue, _minValue;
        int _decimal;
        double _calibration;
        bool _onlyDifference;
        bool _isSensor;
        string _unit;
        string _flowSensorName;
        int _flowStep;
        byte _shiftIndex;
        int _sensorID;
        /// <summary>
        /// 传感器标识
        /// </summary>
        public int SensorID
        {
            get { return _sensorID; }
            set { _sensorID = value; }
        }
        /// <summary>
        /// 档位标识
        /// </summary>
        public byte ShiftIndex
        {
            get { return _shiftIndex; }
            set { _shiftIndex = value; }
        }
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
        /// 表达式
        /// </summary>
        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
        /// <summary>
        /// 小数位数
        /// </summary>
        public int Decimal
        {
            get { return _decimal; }
            set { _decimal = value; }
        }
        /// <summary>
        /// 校正值
        /// </summary>
        public double Calibration
        {
            get { return _calibration; }
            set { _calibration = value; }
        }
        /// <summary>
        /// 只接受与上一个值不同的值
        /// </summary>
        public bool OnlyDifference
        {
            get { return _onlyDifference; }
            set { _onlyDifference = value; }
        }
        /// <summary>
        /// 是传感器变量
        /// </summary>
        public bool IsSensor
        {
            get { return _isSensor; }
            set { _isSensor = value; }
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }

        }
        /// <summary>
        /// 跟随变量
        /// </summary>
        public string FlowSensorName
        {
            get { return _flowSensorName; }
            set { _flowSensorName = value; }
        }
        /// <summary>
        /// 滞后行数
        /// </summary>
        public int FlowStep
        {
            get { return _flowStep; }
            set { _flowStep = value; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string QuickDescription
        {
            get
            {
                return _caption+":"+_minValue.ToString()+":"+_maxValue.ToString()+":"+_unit;
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _caption = "";
            _expression = "";
            _decimal = 4;
            _maxValue = 10;
            _minValue = 0;
            _calibration = 0;
            _onlyDifference = false;
            _isSensor = false;
            _unit = "";
            _flowStep = 0;
            _flowSensorName = "";
            _shiftIndex = 0x00;
            _sensorID = 0x00;
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
            keyValue.Add("Expression", _expression);
            keyValue.Add("Decimal", _decimal.ToString());
            keyValue.Add("MaxValue", _maxValue.ToString());
            keyValue.Add("MinValue", _minValue.ToString());
            keyValue.Add("Calibration", _calibration.ToString());
            keyValue.Add("OnlyDifference", _onlyDifference.ToString());
            keyValue.Add("IsSensor", _isSensor.ToString());
            keyValue.Add("FlowSensorName", _flowSensorName);
            keyValue.Add("FlowStep", _flowStep.ToString());
            keyValue.Add("Unit",_unit);
            keyValue.Add("ShiftIndex", _shiftIndex.ToString());
            keyValue.Add("SensorID", _sensorID.ToString());

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
            _expression = keyValue.GetValueByKey("Expression");
            int.TryParse(keyValue.GetValueByKey("Decimal"), out _decimal);
            double.TryParse(keyValue.GetValueByKey("MaxValue"), out _maxValue);
            double.TryParse(keyValue.GetValueByKey("MinValue"), out _minValue);
            double.TryParse(keyValue.GetValueByKey("Calibration"), out _calibration);
            bool.TryParse(keyValue.GetValueByKey("OnlyDifference"), out _onlyDifference);
            bool.TryParse(keyValue.GetValueByKey("IsSensor"), out _isSensor);
            _unit = keyValue.GetValueByKey("Unit");
            int.TryParse(keyValue.GetValueByKey("FlowStep"), out _flowStep);
            _flowSensorName = keyValue.GetValueByKey("FlowSensorName");
            byte.TryParse(keyValue.GetValueByKey("ShiftIndex"), out _shiftIndex);
            int.TryParse(keyValue.GetValueByKey("SensorID"), out _sensorID);
        }
        #endregion
    }
}
