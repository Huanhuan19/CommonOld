using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;

namespace ExperimentProperties
{
    public class EndProps
    {
        public EndProps()
        {
            LoadDefault();

        }
        public EndProps(EndMode endMode, int reservedPoints, string endPortName,bool isSensor, double gateValue,double reversedSeconds,bool checkAbs)
        {
            Initialize(endMode, reservedPoints, endPortName,isSensor, gateValue,reversedSeconds,checkAbs);
        }
        public EndProps(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("ExperimentProperties.EndProps",
        Assembly.GetExecutingAssembly());

        EndMode _endMode;
        int _reservedPoints;
        string _endPortName;
        bool _isSensor;
        double _gateValue;
        bool _checkAbs;
        double _reservedSeconds;
        public EndMode EndMode
        {
            get { return _endMode; }
            set { _endMode = value; }
        }
        public int ReservedPoints
        {
            get { return _reservedPoints; }
            set { _reservedPoints = value; }
        }
        public string EndPortName
        {
            get { return _endPortName; }
            set { _endPortName = value; }
        }
        public double GateValue
        {
            get { return _gateValue; }
            set { _gateValue = value; }
        }
        public bool IsSensor
        {
            get { return _isSensor; }
            set { _isSensor = value; }
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
                switch (_endMode)
                {
                    default:
                    case EndMode.Manual:
                        str += _resourceManager.GetString("Manual");
                        break;
                    case EndMode.IndexCount:
                        str += _resourceManager.GetString("IndexCount");
                        if (string.IsNullOrEmpty(_endPortName))
                        {
                            str += ":" + _reservedPoints.ToString()+"r";
                        }
                        break;
                    case EndMode.TimeStamp:
                        str += _resourceManager.GetString("TimeStamp");
                        str += ":" + _reservedSeconds.ToString() + "s";
                        break;
                    case EndMode.ValueIncrease:
                        str += _resourceManager.GetString("ValueIncrease");
                        break;
                    case EndMode.ValueDecrease:
                        str += _resourceManager.GetString("ValueDecrease");
                        break;
                    case EndMode.Remote:
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
                str += _resourceManager.GetString("EndMode")+":";
                switch (_endMode)
                {
                    default:
                    case EndMode.Manual:
                        str += _resourceManager.GetString("Manual");
                        break;
                    case EndMode.IndexCount:
                        str += _resourceManager.GetString("IndexCount");
                        if (string.IsNullOrEmpty(_endPortName))
                        {
                            str +=":"+ _reservedPoints.ToString()+"r";
                        }
                        break;
                    case EndMode.TimeStamp:
                        str += _resourceManager.GetString("TimeStamp");
                        str += ":"+_reservedSeconds.ToString()+"s";
                        break;
                    case EndMode.ValueIncrease:
                        str += _resourceManager.GetString("ValueIncrease");
                        str += ":"+_gateValue.ToString() +":"+ (_checkAbs ? _resourceManager.GetString("Abs") : _resourceManager.GetString("Original"));
                        break;
                    case EndMode.ValueDecrease:
                        str += _resourceManager.GetString("ValueDecrease");
                        str += ":"+_gateValue.ToString() +":"+ (_checkAbs ? _resourceManager.GetString("Abs") : _resourceManager.GetString("Original"));
                        break;
                    case EndMode.Remote:
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
            _endMode = EndMode.TimeStamp;
            _reservedSeconds = 100;
            _reservedPoints = 0;
            _checkAbs = false;
            _isSensor = true;
            _endPortName = "";
            _gateValue = 0;
        }
        void Initialize(EndMode endMode, int reservedPoints, string startPortName,bool isSensor, double gateValue,double reversedSeconds,bool checkAbs)
        {
            _endMode = endMode;
            _reservedPoints = reservedPoints;
            _reservedSeconds = reversedSeconds;
            _endPortName = startPortName;
            _isSensor = isSensor;
            _gateValue = gateValue;
            _checkAbs = checkAbs;
        }
        EndMode EndModeParse(string value)
        {
            ExperimentProperties.EndMode endMode = EndMode.Manual;
            if (value.Contains(EndMode.Manual.ToString()))
            {
                endMode = EndMode.Manual;
            }
            else if (value.Contains(EndMode.Remote.ToString()))
            {
                endMode = EndMode.Remote;
            }
            else if (value.Contains(EndMode.TimeStamp.ToString()))
            {
                endMode = EndMode.TimeStamp;
            }
            else if (value.Contains(EndMode.ValueDecrease.ToString()))
            {
                endMode = EndMode.ValueDecrease;
            }
            else if (value.Contains(EndMode.ValueIncrease.ToString()))
            {
                endMode = EndMode.ValueIncrease;
            }
            else if (value.Contains(EndMode.IndexCount.ToString()))
            {
                endMode = EndMode.IndexCount;
            }
            return endMode;
        }
        
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            //keyValue.Add("EndMode", _endMode.ToString());
            keyValue.Add("EndMode", ((int)_endMode).ToString());
            keyValue.Add("ReversedPoints", _reservedPoints.ToString());
            keyValue.Add("ReversedSeconds", _reservedSeconds.ToString());
            keyValue.Add("EndPortName", _endPortName);
            keyValue.Add("IsSensor", _isSensor.ToString());
            keyValue.Add("GateValue", _gateValue.ToString());
            keyValue.Add("CheckAbs", _checkAbs.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            //EndModeParse(keyValue.GetValueByKey("EndMode"));
            int endModeValue;
            int.TryParse(keyValue.GetValueByKey("EndMode"), out endModeValue);
            _endMode = (EndMode)endModeValue;
            int.TryParse(keyValue.GetValueByKey("ReversedPoints"), out _reservedPoints);
            double.TryParse(keyValue.GetValueByKey("ReversedSeconds"), out _reservedSeconds);
            _endPortName = keyValue.GetValueByKey("EndPortName");
            bool.TryParse(keyValue.GetValueByKey("IsSensor"), out _isSensor);
            double.TryParse(keyValue.GetValueByKey("GateValue"), out _gateValue);
            bool.TryParse(keyValue.GetValueByKey("CheckAbs"), out _checkAbs);

        }
        public static List<string> EndModeListStr
        {
            get
            {
                List<string> list = new List<string>();
                list.Add(EndMode.IndexCount.ToString());
                list.Add(EndMode.Manual.ToString());
                list.Add(EndMode.Remote.ToString());
                list.Add(EndMode.TimeStamp.ToString());
                list.Add(EndMode.ValueDecrease.ToString());
                list.Add(EndMode.ValueIncrease.ToString());
                return list;
            }
        }

        #endregion
    }
}
