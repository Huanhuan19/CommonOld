using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class StandardCurveDefine
    {
        public StandardCurveDefine()
        {
            LoadDefault();
        }
        public StandardCurveDefine(string name,string caption,string methodName, double a, double b, double c, double d,bool isY2Axis, StandardCurveGetValue getValue)
        {
            Initialize(name,caption,methodName, a, b, c, d,isY2Axis, getValue);
        }
        public StandardCurveDefine(string value, StandardCurveGetValue getValue)
        {
            _getValue = getValue;
            Parse(value);
            //_initialized = true;

        }
        public StandardCurveDefine(string value)
        {
            Parse(value);
            //_initialized = false;
        }
        #region Props
        string _name, _caption;
        double _a, _b, _c, _d;
        string _methodName;
        bool _isY2Axis;
        //bool _initialized;
        StandardCurveGetValue _getValue = null;
        ZedGraph.LineItem _lineItem;
        public StandardCurveGetValue GetValueMethod
        {
            get { return _getValue; }
            set { _getValue = value; }
        }
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
        public double A
        {
            get { return _a; }
            set { _a = value; }
        }
        public double B
        {
            get { return _b; }
            set { _b = value; }
        }
        public double C
        {
            get { return _c; }
            set { _c = value; }
        }
        public double D
        {
            get { return _d; }
            set { _d = value; }
        }
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }
        public bool IsY2Axis
        {
            get { return _isY2Axis; }
            set { _isY2Axis = value; }
        }
        public ZedGraph.LineItem LineItem
        {
            get { return _lineItem; }
            set { _lineItem = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _caption = "";
            _a = 0;
            _b = 0;
            _c = 0;
            _c = 0;
            _methodName = "";
            _getValue = null;
            _isY2Axis = false;
            //_initialized = false;
        }
        public void Initialize(string name,string caption,string methodName,double a, double b, double c, double d,bool isY2Axis, StandardCurveGetValue getValue)
        {
            _name = name;
            _caption = caption;
            _methodName = methodName;
            _a = a;
            _b = b;
            _c = c;
            _d = d;
            _isY2Axis = isY2Axis;
            _getValue = getValue;
            //_initialized = true;
        }
        public double GetValue(double x)
        {
            double value = 0;
            if (_getValue != null)
            {
                value = _getValue(_methodName,x,_a,_b,_c,_d);
            }
            return value;
        }
        public ZedGraph.LineItem CreateLineItem(string sectionName)
        {
            ZedGraph.LineItem lineItem = new ZedGraph.LineItem(Caption);
            lineItem.Color =Graph. STANDARD_CURVE_COLOR;
            lineItem.Line.IsVisible = true;
            lineItem.Line.IsSmooth = true;
            lineItem.Line.SmoothTension = 1f;
            //lineItem.Line.Style = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            //lineItem.Line.DashOff = 0.1f;
            //lineItem.Line.DashOn = 0.8f;
            lineItem.Line.Width = Graph.STANDARD_CURVE_WIDTH;
            lineItem.Symbol = new ZedGraph.Symbol(ZedGraph.SymbolType.Circle,Graph. STANDARD_CURVE_COLOR);
            lineItem.Symbol.IsVisible = false;
            lineItem.Symbol.Size = Graph.POINT_SIZE;
            lineItem.Symbol.Fill = new ZedGraph.Fill(Graph.STANDARD_CURVE_COLOR);
            lineItem.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.StandardCurve, sectionName, Name, this.ToString());
            lineItem.IsY2Axis = IsY2Axis;
            lineItem.Line.IsAntiAlias = true;
            _lineItem = lineItem;
            return lineItem;

        }
        public void Fill(double start, double end, int count)
        {
            if (_lineItem != null && _getValue != null)
            {
                _lineItem.Clear();
                if (count > 0)
                {
                    DataAnalysis.Fitting fitting = new DataAnalysis.Fitting();
                    double interval = (Math.Max(start, end) - Math.Min(start, end)) / count;
                    for (int i = 0; i < count; i++)
                    {
                        double x = start + interval * i;
                        double y = _getValue(MethodName, x, A, B, C, D);
                        _lineItem.AddPoint(new ZedGraph.PointPair(x, y, Graph.PointString(Caption+"/"+fitting.GetFittingExpression( MethodName),x, y)));
                    }
                }
            }
        }

        #endregion

        #region Serialize 
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("MethodName", _methodName);
            keyValue.Add("A", _a.ToString());
            keyValue.Add("B", _b.ToString());
            keyValue.Add("C", _c.ToString());
            keyValue.Add("D", _d.ToString());
            keyValue.Add("IsY2Axis", _isY2Axis.ToString());
            return keyValue.ToString();

        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            _methodName = keyValue.GetValueByKey("MethodName");
            bool.TryParse(keyValue.GetValueByKey("IsY2Axis"), out _isY2Axis);
            double.TryParse(keyValue.GetValueByKey("A"), out _a);
            double.TryParse(keyValue.GetValueByKey("B"), out _b);
            double.TryParse(keyValue.GetValueByKey("C"), out _c);
            double.TryParse(keyValue.GetValueByKey("D"), out _d);
        }
        #endregion
    }
    public delegate double StandardCurveGetValue( string methodName,double x ,double a,double b,double c ,double d);
}
