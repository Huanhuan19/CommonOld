using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;

namespace ExperimentProperties
{
    public class PhotoGateProps
    {
        public PhotoGateProps()
        {
            LoadDefault();
        }
        public PhotoGateProps(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("ExperimentProperties.PhotoGateProps",Assembly.GetExecutingAssembly());

        bool _available;
        PhotoGateMode _openMode;
        PhotoGateMode _closeMode;
        string _openColumnName, _closeColumnName;
        double _openGateValue, _closeGateValue;
        bool _openCheckAbs,_closeCheckAbs;
        public bool Available
        {
            get { return _available; }
            set { _available = value; }
        }
        public PhotoGateMode OpenMode
        {
            get { return _openMode; }
            set { _openMode = value; }
        }
        public PhotoGateMode CloseMode
        {
            get { return _closeMode; }
            set { _closeMode = value; }
        }
        public string OpenColumnName
        {
            get { return _openColumnName; }
            set { _openColumnName = value; }
        }
        public string CloseColumnName
        {
            get { return _closeColumnName; }
            set { _closeColumnName = value; }
        }
        public double OpenGateValue
        {
            get { return _openGateValue; }
            set { _openGateValue = value; }
        }
        public double CloseGateValue
        {
            get { return _closeGateValue; }
            set { _closeGateValue = value; }
        }
        public bool OpenCheckAbs
        {
            get { return _openCheckAbs; }
            set { _openCheckAbs = value; }
        }
        public bool CloseCheckAbs
        {
            get { return _closeCheckAbs; }
            set { _closeCheckAbs = value; }
        }
        public string OpenPropsDescriptionString
        {
            get
            {
                string str = "";
                if (_available)
                {
                    str += _resourceManager.GetString("Shutter") + ":";
                    switch (_openMode)
                    {
                        default:
                        case PhotoGateMode.Manual:
                            str += _resourceManager.GetString("Manual");
                            break;
                        case PhotoGateMode.ValueIncrease:
                            str += _resourceManager.GetString("ValueIncrease");
                            break;
                        case PhotoGateMode.ValueDecrease:
                            str += _resourceManager.GetString("ValueDecrease");
                            str += ":" + _openGateValue.ToString() + ":" + (_openCheckAbs ? _resourceManager.GetString("Abs") : _resourceManager.GetString("Original"));
                            break;
                    }
                }
                else
                {
                    str += _resourceManager.GetString("Series");
                }
                return str;
            }
        }
        public string OpenPropsQuickDescriptionString
        {
            get
            {
                string str = "";
                if (_available)
                {
                    switch (_openMode)
                    {
                        default:
                        case PhotoGateMode.Manual:
                            str += _resourceManager.GetString("Manual");
                            break;
                        case PhotoGateMode.ValueIncrease:
                            str += _resourceManager.GetString("ValueIncrease");
                            break;
                        case PhotoGateMode.ValueDecrease:
                            str += _resourceManager.GetString("ValueDecrease");
                            break;
                    }
                }
                else
                {
                    str += _resourceManager.GetString("Series");
                }
                return str;
            }
        }
        public string ClosePropsDescriptionString
        {
            get
            {
                string str = "";
                if (_available)
                {
                    switch (_closeMode)
                    {
                        default:
                        case PhotoGateMode.Manual:
                            str += _resourceManager.GetString("Manual");
                            break;
                        case PhotoGateMode.ValueIncrease:
                            str += _resourceManager.GetString("ValueIncrease");
                            break;
                        case PhotoGateMode.ValueDecrease:
                            str += _resourceManager.GetString("ValueDecrease");
                            str += _closeGateValue.ToString() + (_closeCheckAbs ? _resourceManager.GetString("Abs") : _resourceManager.GetString("Original"));
                            break;
                    }
                }
                else
                {
                    str += _resourceManager.GetString("Series");
                }
                return str;
            }
        }
        public string ClosePropsQuickDescriptionString
        {
            get
            {
                string str = "";
                if (_available)
                {
                    switch (_closeMode)
                    {
                        default:
                        case PhotoGateMode.Manual:
                            str += _resourceManager.GetString("Manual");
                            break;
                        case PhotoGateMode.ValueIncrease:
                            str += _resourceManager.GetString("ValueIncrease");
                            break;
                        case PhotoGateMode.ValueDecrease:
                            str += _resourceManager.GetString("ValueDecrease");
                            break;
                    }
                }
                else
                {
                    str += _resourceManager.GetString("Series");
                }
                return str;
            }
        }
        public string DescriptionString
        {
            get
            {
                string str = "";
                if (_available)
                {
                    str += _resourceManager.GetString("Shutter") + ":";
                    switch (_openMode)
                    {
                        default:
                        case PhotoGateMode.Manual:
                            str += _resourceManager.GetString("Manual");
                            break;
                        case PhotoGateMode.ValueIncrease:
                            str += _resourceManager.GetString("ValueIncrease");
                            break;
                        case PhotoGateMode.ValueDecrease:
                            str += _resourceManager.GetString("ValueDecease");
                            str += ":"+_openGateValue.ToString() + ":"+(_openCheckAbs ? _resourceManager.GetString("Abs") : _resourceManager.GetString("Original"));
                            break;
                    }
                    //str += " 单点关闭模式：";
                    switch (_closeMode)
                    {
                        default:
                        case PhotoGateMode.Manual:
                            str += _resourceManager.GetString("Manual");
                            break;
                        case PhotoGateMode.ValueIncrease:
                            str += _resourceManager.GetString("ValueIncrease");
                            break;
                        case PhotoGateMode.ValueDecrease:
                            str += _resourceManager.GetString("ValueDecrease");
                            str += ":"+_closeGateValue.ToString() +":"+ (_closeCheckAbs ? _resourceManager.GetString("Abs") : _resourceManager.GetString("Original"));
                            break;
                    }
                }
                else
                {
                    str += _resourceManager.GetString("Series");
                }
                return str;
            }
        }

        #endregion

        #region Methods
        void LoadDefault()
        {
            _available = false;
            _openMode = PhotoGateMode.Manual;
            _openGateValue = 0;
            _openColumnName = "";
            _closeMode = PhotoGateMode.Manual;
            _closeGateValue = 0;
            _closeColumnName = "";
            _openCheckAbs = false;
            _closeCheckAbs = false;
        }

        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Available", _available.ToString());
            keyValue.Add("OpenMode", ((int)_openMode).ToString());
            keyValue.Add("OpenGateValue", _openGateValue.ToString());
            keyValue.Add("OpenColumnName", _openColumnName);
            keyValue.Add("CloseMode", ((int)_closeMode).ToString());
            keyValue.Add("CloseGateValue", _closeGateValue.ToString());
            keyValue.Add("CloseColumnName", _closeColumnName);
            keyValue.Add("OpenCheckAbs", _openCheckAbs.ToString());
            keyValue.Add("CloseCheckAbs", _closeCheckAbs.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            bool.TryParse(keyValue.GetValueByKey("Available"), out _available);
            int openMode, closeMode;
            int.TryParse(keyValue.GetValueByKey("OpenMode"), out openMode);
            int.TryParse(keyValue.GetValueByKey("CloseMode"), out closeMode);
            _openMode = (PhotoGateMode)openMode;
            _closeMode = (PhotoGateMode)closeMode;
            _openColumnName = keyValue.GetValueByKey("OpenColumnName");
            _closeColumnName = keyValue.GetValueByKey("CloseColumnName");
            double.TryParse(keyValue.GetValueByKey("OpenGateValue"), out _openGateValue);
            double.TryParse(keyValue.GetValueByKey("CloseGateValue"), out _closeGateValue);
            bool.TryParse(keyValue.GetValueByKey("OpenCheckAbs"), out _openCheckAbs);
            bool.TryParse(keyValue.GetValueByKey("CloseCheckAbs"), out _closeCheckAbs);
        }
        #endregion
    }
}
