using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.Classes
{
    public class VectorLine
    {
        public VectorLine()
        {
            LoadDefault();
        }
        public VectorLine(string name, string caption, double radius, double length, float widthf, Color lineColor, bool drawArrow)
        {
            LoadDefault();
            Initialize(name, caption, radius, length, widthf, lineColor, drawArrow);
        }
        public VectorLine(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        string _name;
        string _caption;
        double _radius, _length;
        float _widthF;
        Color _lineColor;
        bool _drawArrow;

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
        public double Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }
        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }
        public float WidthF
        {
            get { return _widthF; }
            set { _widthF = value; }
        }
        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }
        public bool DrawArrow
        {
            get { return _drawArrow; }
            set { _drawArrow = value; }
        }
             
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _caption = "";
            _radius = 0;
            _length = 0;
            _widthF = 2.5f;
            _lineColor = System.Drawing.SystemColors.ControlText;
            _drawArrow = true;
        }
        void Initialize(string name, string caption, double radius, double length, float widthf, Color lineColor, bool drawArrow)
        {
            _name = name;
            _caption = caption;
            _radius = radius;
            _widthF = widthf;
            _lineColor = lineColor;
            _drawArrow = drawArrow;
        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("Radius", _radius.ToString());
            keyValue.Add("Length", _length.ToString());
            keyValue.Add("WidthF", _widthF.ToString());
            keyValue.Add("LineColor", KeyValue.KeyValue.ColorToString(_lineColor));
            keyValue.Add("DrawArrow", _drawArrow.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            double.TryParse(keyValue.GetValueByKey("Radius"), out _radius);
            double.TryParse(keyValue.GetValueByKey("Length"), out _length);
            float.TryParse(keyValue.GetValueByKey("WidthF"), out _widthF);
            _lineColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("LineColor"));
            bool.TryParse(keyValue.GetValueByKey("DrawArrow"), out _drawArrow);
        }
             
        #endregion
    }
}
