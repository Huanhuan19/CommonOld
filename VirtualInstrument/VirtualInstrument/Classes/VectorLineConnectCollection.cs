using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class VectorLineConnectCollection
    {
        public VectorLineConnectCollection()
        {
        }
        public VectorLineConnectCollection(string value)
        {
            Parse(value);
        }
        #region Props
        string _mainLineName = "";
        List<string> _subLineNames = new List<string>();
        public string MainLineName
        {
            get { return _mainLineName; }
            set { _mainLineName = value; }
        }
        public List<string> SubLineNames
        {
            get { return _subLineNames; }
        }
        public bool Initialized
        {
            get { return !string.IsNullOrEmpty(_mainLineName) && _subLineNames.Count > 0; }
        }
        #endregion
        #region Serialize
        string List2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _subLineNames.Count; i++)
            {
                keyValue.Add(i.ToString(), _subLineNames[i]);
            }
            return keyValue.ToString();
        }
        void Str2List(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _subLineNames.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _subLineNames.Add(keyValue.GetValueByKey(i.ToString()));
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("MainLine", _mainLineName);
            keyValue.Add("SubLines", List2Str());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _mainLineName = keyValue.GetValueByKey("MainLine");
            Str2List(keyValue.GetValueByKey("SubLines"));
        }
        #endregion

    }
}
