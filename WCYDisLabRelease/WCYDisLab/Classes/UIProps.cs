using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDisLab.Classes
{
    public class UIProps
    {
        public UIProps()
        {
            LoadDefault();
        }
        public UIProps(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        List<WindowProps> _windows;
        KeyValue.KeyValue _others;
        public List<WindowProps> Windows
        {
            get { return _windows; }
        }
        public KeyValue.KeyValue Others
        {
            get { return _others; }
        }
        public string ExperimentFilename
        {
            get
            {
                string filename = "";
                if (_others.ContainsKey("Filename"))
                {
                    filename = _others.GetValueByKey("Filename");
                }
                return filename;
            }
            set
            {
                if (_others.ContainsKey("Filename"))
                {
                    _others.Replace("Filename", value);
                }
                else
                {
                    _others.Add("Filename", value);
                }
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _windows = new List<WindowProps>();
            _others = new KeyValue.KeyValue("Others");
        }
        #endregion

        #region Serialize
        string WindowsToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _windows.Count; i++)
            {
                keyValue.Add(i.ToString(), _windows[i].ToString());
            }
            return keyValue.ToString();

        }
        void WindowsParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _windows.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _windows.Add(new WindowProps(keyValue.GetValueByKey(i.ToString())));
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Windows", WindowsToString());
            keyValue.Add("Others", _others.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            WindowsParse(keyValue.GetValueByKey("Windows"));
            _others.Clear();
            _others.Parse(keyValue.GetValueByKey("Others"));
        }
        #endregion
    }
}
