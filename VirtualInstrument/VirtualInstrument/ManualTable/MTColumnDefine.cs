using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.ManualTable
{
    public class MTColumnDefine
    {
        public MTColumnDefine()
        {
            LoadDefault();
        }
        public MTColumnDefine(string name, string caption, string typeName, Color backColor, Color foreColor, bool readOnly)
        {
            LoadDefault();
            Initialize(name, caption, typeName, backColor, foreColor, readOnly);
        }
        public MTColumnDefine(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        string _name;
        string _caption;
        string _typeName;
        Color _backColor;
        Color _foreColor;
        bool _readOnly;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }
        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }
        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }
        public Color ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }
        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _caption = "";
            _typeName = "System.Double";
            _backColor = System.Drawing.SystemColors.Window;
            _foreColor = System.Drawing.SystemColors.WindowText;
            _readOnly = true;
        }
        void Initialize(string name, string caption, string typeName, Color backColor, Color foreColor,bool readOnly)
        {
            _name = name;
            _caption = caption;
            _typeName = typeName;
            _backColor = backColor;
            _foreColor = foreColor;
            _readOnly = readOnly;
        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("TypeName", _typeName);
            keyValue.Add("BackColor", KeyValue.KeyValue.ColorToString(_backColor));
            keyValue.Add("ForeColor", KeyValue.KeyValue.ColorToString(_foreColor));
            keyValue.Add("ReadOnly", _readOnly.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            _typeName = keyValue.GetValueByKey("TypeName");
            _backColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("BackColor"));
            _foreColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("ForeColor"));
            bool.TryParse(keyValue.GetValueByKey("ReadOnly"), out _readOnly);

        }
        #endregion
    }
}
