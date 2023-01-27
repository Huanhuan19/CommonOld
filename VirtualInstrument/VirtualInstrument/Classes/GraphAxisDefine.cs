using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.Classes
{
    public class GraphAxisDefine
    {
        public GraphAxisDefine()
        {
            LoadDefault();
        }
        public GraphAxisDefine(string name, string caption, string unit, Color axisColor, Color mainMarginColor, Color minorMarginColor,double max,double min,bool autoScale,bool lockRange,bool visible)
        {
            Initialize(name, caption, unit, axisColor, mainMarginColor, minorMarginColor,max,min,autoScale,lockRange,visible);
        }
        public GraphAxisDefine(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        string _name,_caption,_unit;
        Color _axisColor,_mainMarginColor,_minorMarginColor;
        bool _visible,_autoScale,_lockRange;
        double _min, _max;
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
        public Color AxisColor
        {
            get { return _axisColor; }
            set { _axisColor = value; }
        }

        public Color MainMarginColor
        {
            get { return _mainMarginColor; }
            set { _mainMarginColor = value; }
        }
        public Color MinorMarginColor
        {
            get { return _minorMarginColor; }
            set { _minorMarginColor = value; }
        }
        public double Minimum
        {
            get { return _min; }
            set { _min = value; }
        }
        public double Maximum
        {
            get { return _max; }
            set { _max = value; }
        }
        public double ScaleRange
        {
            get
            {
                return Maximum - Minimum;
            }
        }
        public bool LockRange
        {
            get { return _lockRange; }
            set { _lockRange = value; }
        }
        public bool AutoScale
        {
            get { return _autoScale; }
            set { _autoScale = value; }
        }
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _caption = "";
            _unit = "";
            _axisColor =System.Drawing.SystemColors.ActiveCaption ;
            _mainMarginColor = Color.FromArgb( 100,Color.DarkGreen );
            _minorMarginColor = Color.FromArgb(50, Color.DarkGreen); ;
            _max = 1.2;
            _min = 0;
            _autoScale = true;
            _lockRange = false;
            _visible = true;
        }
        void Initialize(string name, string caption, string unit, Color axisColor, Color mainMarginColor, Color minorMarginColor,double max,double min,bool autoScale,bool lockRange,bool visible)
        {
            _name = name;
            _caption = caption;
            _unit = unit;
            _axisColor = axisColor;
            _minorMarginColor = minorMarginColor;
            _mainMarginColor = mainMarginColor;
            _max = max;
            _min = min;
            _autoScale = autoScale;
            _lockRange =lockRange;
            _visible = visible;
        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("Unit", _unit);
            keyValue.Add("AxisColor", KeyValue.KeyValue.ColorToString(_axisColor));
            keyValue.Add("MainMarginColor", KeyValue.KeyValue.ColorToString(_mainMarginColor));
            keyValue.Add("MinorMarginColor", KeyValue.KeyValue.ColorToString(_minorMarginColor));
            keyValue.Add("Maximum", _max.ToString());
            keyValue.Add("Minimum", _min.ToString());
            keyValue.Add("AutoScale", _autoScale.ToString());
            keyValue.Add("LockRange", _lockRange.ToString());
            keyValue.Add("Visible", _visible.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            _unit = keyValue.GetValueByKey("Unit");
            _axisColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("AxisColor"));
            _mainMarginColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("MainMarginColor"));
            _minorMarginColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("MinorMarginColor"));
            double.TryParse(keyValue.GetValueByKey("Minimum"), out _min);
            double.TryParse(keyValue.GetValueByKey("Maximum"), out _max);
            bool.TryParse(keyValue.GetValueByKey("AutoScale"),out _autoScale );
            bool.TryParse(keyValue.GetValueByKey("LockRange"), out _lockRange);
            bool.TryParse(keyValue.GetValueByKey("Visible"), out _visible);

        }
        #endregion
    }
}
