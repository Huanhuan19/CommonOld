using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;

namespace ExperimentProperties
{
    public class StartProps
    {
        public StartProps()
        {
            LoadDefault();

        }
        public StartProps(StartMode startMode, int reservedPoints, string startPortName,bool isSensor, double gateValue)
        {
            Initialize(startMode, reservedPoints, startPortName,isSensor, gateValue,CheckAbs,ReservedSeconds);
        }
        public StartProps(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("ExperimentProperties.StartProps", Assembly.GetExecutingAssembly());
        StartMode _startMode;
        int _reservedPoints;
        string _startPortName;
        bool _isSensor;
        double _gateValue;
        bool _checkAbs;
        double _reservedSeconds;
        public StartMode StartMode
        {
            get { return _startMode; }
            set { _startMode = value; }
        }
        public int ReservedPoints
        {
            get { return _reservedPoints; }
            set { _reservedPoints = value; }
        }
        public string StartPortName
        {
            get { return _startPortName; }
            set { _startPortName = value; }
        }
        public bool IsSensor
        {
            get { return _isSensor; }
            set { _isSensor = value; }
        }

        public double GateValue
        {
            get { return _gateValue; }
            set { _gateValue = value; }
        }
        public bool CheckAbs
        {
            get { return _checkAbs; }
            set { _checkAbs = value; }
        }
        public double ReservedSeconds
        {
            get { return _reservedSeconds; }
            set { _reservedSeconds = value; }
        }
        public string QuickDescriptionString
        {
            get
            {
                string str = "";
                switch (_startMode)
                {
                    default:
                    case StartMode.Manual:
                        str += _resourceManager.GetString("Manual");
                        break;
                    case StartMode.IndexCount:
                        str += _resourceManager.GetString("IndexCount");
                        str += ":" + _reservedPoints.ToString() + "r";
                        break;
                    case StartMode.TimeStamp:
                        str += _resourceManager.GetString("TimeStamp");
                        str += ":" + _reservedSeconds.ToString() + "s";
                        break;
                    case StartMode.ValueIncrease:
                        str += _resourceManager.GetString("ValueIncrease");
                        break;
                    case StartMode.ValueDecrease:
                        str += _resourceManager.GetString("ValueDecrease");
                        break;
                    case StartMode.Remote:
                        str += _resourceManager.GetString("Remote");
                        break;
                }
                return str;
            }
        }
        public string DescriptionString
        {
            get
            {
                string str = "";
                str += _resourceManager.GetString("StartMode")+":";
                switch (_startMode)
                {
                    default:
                    case StartMode.Manual:
                        str += _resourceManager.GetString("Manual");
                        break;
                    case StartMode.IndexCount:
                        str += _resourceManager.GetString("IndexCount");
                        str += ":"+_reservedPoints.ToString()+"r";
                        break;
                    case StartMode.TimeStamp:
                        str += _resourceManager.GetString("TimeStamp");
                        str += ":"+_reservedSeconds.ToString()+"s";
                        break;
                    case StartMode.ValueIncrease:
                        str += _resourceManager.GetString("ValueIncrease");
                        str += ":"+_gateValue.ToString() +":"+ (_checkAbs ? _resourceManager.GetString("Abs") : _resourceManager.GetString("Original"));
                        break;
                    case StartMode.ValueDecrease:
                        str += _resourceManager.GetString("ValueDecrease");
                        str += ":"+_gateValue.ToString() +":"+ (_checkAbs ? _resourceManager.GetString("Abs") : _resourceManager.GetString("Original"));
                        break;
                    case StartMode.Remote:
                        str += _resourceManager.GetString("Remote");
                        break;
                }
                return str;
            }
        }

        #endregion

        #region Methods
        void LoadDefault()
        {
            _startMode = StartMode.Manual;
            _reservedPoints = 0;
            _startPortName = "";
            _isSensor = true;
            _gateValue = 0;
            _reservedSeconds = 0;
            _checkAbs = false;
        }
        void Initialize(StartMode startMode, int reservedPoints, string startPortName,bool isSensor, double gateValue,bool checkAbs,double reservedSeconds)
        {
            _startMode = startMode;
            _reservedPoints = reservedPoints;
            _startPortName = startPortName;
            _isSensor = isSensor;
            _gateValue = gateValue;
            _checkAbs = checkAbs;
            _reservedSeconds = reservedPoints;
        }
        StartMode StartModeParse(string value)
        {
            ExperimentProperties.StartMode startMode = StartMode.Manual;
            if (value.Contains(StartMode.Manual.ToString()))
            {
                startMode = StartMode.Manual;
            }
            else if (value.Contains(StartMode.Remote.ToString()))
            {
                startMode = StartMode.Remote;
            }
            else if (value.Contains(StartMode.TimeStamp.ToString()))
            {
                startMode = StartMode.TimeStamp;
            }
            else if (value.Contains(StartMode.ValueDecrease.ToString()))
            {
                startMode = StartMode.ValueDecrease;
            }
            else if (value.Contains(StartMode.ValueIncrease.ToString()))
            {
                startMode = StartMode.ValueIncrease;
            }
            else if (value.Contains(StartMode.IndexCount.ToString()))
            {
                startMode = StartMode.IndexCount;
            }
            return startMode;
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("StartMode", _startMode.ToString());
            keyValue.Add("ReversedPoints", _reservedPoints.ToString());
            keyValue.Add("ReversedSeconds", _reservedSeconds.ToString());
            keyValue.Add("StartPortName", _startPortName);
            keyValue.Add("IsSensor", _isSensor.ToString());

            keyValue.Add("GateValue", _gateValue.ToString());
            keyValue.Add("CheckAbs", _checkAbs.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _startMode = StartModeParse(keyValue.GetValueByKey("StartMode"));
            int.TryParse(keyValue.GetValueByKey("ReversedPoints"), out _reservedPoints);
            double.TryParse(keyValue.GetValueByKey("ReversedSeconds"), out _reservedSeconds);
            double.TryParse(keyValue.GetValueByKey("GateValue"), out _gateValue);
            _startPortName = keyValue.GetValueByKey("StartPortName");
            bool.TryParse(keyValue.GetValueByKey("IsSensor"), out _isSensor);

            bool.TryParse(keyValue.GetValueByKey("CheckAbs"), out _checkAbs);
                
        }
        public static List<string> StartModeListStr
        {
            get
            {
                List<string> list = new List<string>();
                list.Add(StartMode.IndexCount.ToString());
                list.Add(StartMode.Manual.ToString());
                list.Add(StartMode.Remote.ToString());
                list.Add(StartMode.TimeStamp.ToString());
                list.Add(StartMode.ValueDecrease.ToString());
                list.Add(StartMode.ValueIncrease.ToString());
                return list;
            }
        }
        #endregion
    }
}
