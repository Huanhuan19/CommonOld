using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDisLab.Classes
{
    public class WindowProps
    {
        public WindowProps(WindowType type, string value)
        {
            _type = type;
            _windowInfo = value;
        }
        public WindowProps(string value)
        {
            Parse(value);
        }
        #region Props
        WindowType _type;
        string _windowInfo;
        public WindowType WindowType
        {
            get { return _type; }
            set { _type = value; }
        }
        public string WindowInfo
        {
            get { return _windowInfo; }
            set { _windowInfo = value; }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("WindowType", ((int)_type).ToString());
            keyValue.Add("WindowInfo", _windowInfo);
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            int type;
            int.TryParse(keyValue.GetValueByKey("WindowType"), out type);
            _type = (WindowType)type;
            _windowInfo = keyValue.GetValueByKey("WindowInfo");
        }
        #endregion
    }
    public enum WindowType : int
    {
        Other = 0, Graph = 10, Table = 20, Vector = 30, Meter = 40, DigitalMeter = 50, Thermometer = 60, Camera = 70, VideoPlayer = 80, Statistic = 90, Report = 100, Guid = 110, QuickViewer = 120
    }

}
