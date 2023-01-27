using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.Classes
{
    public class GraphManualLine
    {
        public GraphManualLine()
        {
            LoadDefault();
        }
        public GraphManualLine(string name,string xCaption, string yCaption,Color color)
        {
            Initialize(name,xCaption, yCaption,color );
        }
        public GraphManualLine(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        string _name;
        List<Classes.GraphPointDefine> _points = new List<GraphPointDefine>();
        string _xCaption, _yCaption;
        Color _color;
        ZedGraph.LineItem _lineItem = null;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public List<Classes.GraphPointDefine> Points
        {
            get { return _points; }
        }
        public string XCaption
        {
            get { return _xCaption; }
            set { _xCaption = value; }
        }
        public string YCaption
        {
            get { return _yCaption; }
            set { _yCaption = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public ZedGraph.LineItem LineItem
        {
            get { return _lineItem; }
            set { _lineItem = value; }
        }
        public int Count
        {
            get { return _points.Count; }
        }
        public string Caption
        {
            get { return YCaption + " - " + XCaption; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _xCaption = "X";
            _yCaption = "Y";
            _color = PublicMethods.GetNewColor();
            _points.Clear();
            _lineItem = null;
        }
        public void Initialize(string name,string xCaption, string yCaption,Color color)
        {
            _name = name;
            _xCaption = xCaption;
            _yCaption = yCaption;
            _color = color;
            _points.Clear();
            _lineItem = null;
        }
        public int Add(double x, double y)
        {
            _points.Add(new GraphPointDefine(x, y));
            
            return _points.Count - 1;
        }
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < _points.Count)
            {
                _points.RemoveAt(index);
                if (_lineItem != null)
                {
                    if (index >= 0 && index < _lineItem.Points.Count)
                    {
                        _lineItem.RemovePoint(index);
                    }
                }
            }
        }
        public void Clear()
        {
            _points.Clear();
            if (_lineItem != null)
            {
                _lineItem.Clear();
            }
        }
        public ZedGraph.LineItem CreateLineItem(string sectionName)
        {
            ZedGraph.LineItem lineItem = new ZedGraph.LineItem(Caption);
            lineItem.Color = _color;
            lineItem.Line.IsVisible = true;
            lineItem.Line.IsSmooth = false;
            lineItem.Line.SmoothTension = 1f;
            lineItem.Line.Width = Graph.DEFAULT_LINE_WIDTH;
            //lineItem.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            //lineItem.Line.DashOff = 0.2f;
            //lineItem.Line.DashOn = 0.8f;
            lineItem.Symbol = new ZedGraph.Symbol(ZedGraph.SymbolType.Circle,_color);
            lineItem.Symbol.IsVisible = true;
            lineItem.Symbol.Size = Graph.POINT_SIZE;
            lineItem.Symbol.Fill = new ZedGraph.Fill(_color);
            lineItem.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.ManualLine, sectionName,Name, this.ToString());
            lineItem.IsY2Axis = false;
            _lineItem = lineItem;
            lineItem.Line.IsAntiAlias = true;
            Fill();
            return lineItem;

        }
        public void Fill()
        {
            if (_lineItem != null)
            {
                _lineItem.Clear();
                for (int i = 0; i < _points.Count; i++)
                {
                    ZedGraph.PointPair p = new ZedGraph.PointPair(_points[i].X, _points[i].Y, Caption + "(" + _points[i].Y.ToString() + "," + _points[i].X.ToString() + ")");

                    _lineItem.AddPoint(p);
                }
            }
        }

        #endregion

        #region Serialize
        string Points2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _points.Count; i++)
            {
                keyValue.Add(i.ToString(), _points[i].ToString());
            }
            return keyValue.ToString();
        }
        void PointsParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _points.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _points.Add(new GraphPointDefine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("XCaption", _xCaption);
            keyValue.Add("YCaption", _yCaption);
            keyValue.Add("Points", Points2Str());
            keyValue.Add("Color", KeyValue.KeyValue.ColorToString(_color));
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _xCaption = keyValue.GetValueByKey("XCaption");
            _yCaption = keyValue.GetValueByKey("YCaption");
            _color = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("Color"));
            PointsParse(keyValue.GetValueByKey("Points"));
        }
        #endregion
    }
}
