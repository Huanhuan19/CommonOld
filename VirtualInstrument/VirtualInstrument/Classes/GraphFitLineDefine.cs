using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.Classes
{
    public class GraphFitLineDefine
    {
        public GraphFitLineDefine()
        {
            LoadDefault();
        }
        public GraphFitLineDefine(Classes.StandardCurveGetValue getValue, double start, double end, StandardCurveDefine curveDefine)
        {
            Initialize(getValue,start, end, curveDefine);
        }
        public GraphFitLineDefine(List<GraphPointDefine> points, StandardCurveDefine curveDefine)
        {
            Initialize(points, curveDefine);
        }
        public GraphFitLineDefine(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        double _start,_end;
        StandardCurveDefine _curveDefine;
        ZedGraph.LineItem _lineItem;
        Classes.StandardCurveGetValue _getValue;
        List<GraphPointDefine> _points = new List<GraphPointDefine>();
        public double StartPosition
        {
            get { return _start; }
            set { _start = value; }
        }
        public double EndPosition
        {
            get { return _end; }
            set { _end = value; }
        }
        public StandardCurveDefine CurveDefine
        {
            get { return _curveDefine; }
            set { _curveDefine = value; }
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
            _start = 0;
            _end = 1.2;
            _curveDefine = new StandardCurveDefine();
        }
        void Initialize(Classes.StandardCurveGetValue getValue, double start, double end, StandardCurveDefine curveDefine)
        {
            _start = start;
            _end = end;
            _getValue = getValue;
            _curveDefine = new StandardCurveDefine(curveDefine.ToString());
        }
        void Initialize(List<GraphPointDefine> points, StandardCurveDefine curveDefine)
        {
            _points.Clear();
            _points.AddRange(points);
            _curveDefine = new StandardCurveDefine(curveDefine.ToString());
        }
        public ZedGraph.LineItem CreateLineItem(string sectionName)
        {

            ZedGraph.LineItem lineItem = new ZedGraph.LineItem(CurveDefine.Caption);
            lineItem.Color = Graph.FIT_LINE_COLOR;
            lineItem.Line.IsVisible = true;
            lineItem.Line.IsSmooth = true;
            lineItem.Line.SmoothTension = 1f;
            lineItem.Line.Width = Graph.FIT_LINE_WIDTH;
            //lineItem.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            //lineItem.Line.DashOff = 0.2f;
            //lineItem.Line.DashOn = 0.8f;
            lineItem.Symbol = new ZedGraph.Symbol(ZedGraph.SymbolType.Circle,Graph. FIT_LINE_COLOR);
            lineItem.Symbol.IsVisible = false;
            lineItem.Symbol.Size = Graph. POINT_SIZE;
            lineItem.Symbol.Fill = new ZedGraph.Fill(Graph.FIT_LINE_COLOR);
            lineItem.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.FitLine, sectionName, CurveDefine.Name, this.ToString());
            lineItem.IsY2Axis = CurveDefine.IsY2Axis;
            _lineItem = lineItem;
            lineItem.Line.IsAntiAlias = true;
            if (string.Equals(CurveDefine.MethodName, "CalcArea"))
            {
                _lineItem.Line.Fill = new ZedGraph.Fill(Color.FromArgb(50, Graph.FIT_LINE_COLOR.R, Graph.FIT_LINE_COLOR.G, Graph.FIT_LINE_COLOR.B));
            }
            if (_points.Count > 0)
            {
                Fill(_points);
            }
            else
            {
                Fill(1000);
            }
            return lineItem;

        }
        public void Fill(int count)
        {
            if (_lineItem != null && _getValue != null)
            {
                _lineItem.Clear();
                if (count > 0)
                {
                    DataAnalysis.Fitting fitting = new DataAnalysis.Fitting();
                    double interval = (Math.Max(StartPosition, EndPosition) - Math.Min(StartPosition, EndPosition)) / count;
                    for (int i = 0; i < count; i++)
                    {
                        double x = StartPosition + interval * i;
                        double y = _getValue(CurveDefine.MethodName, x, CurveDefine.A, CurveDefine.B, CurveDefine.C, CurveDefine.D);
                        _lineItem.AddPoint(new ZedGraph.PointPair(x, y, Graph.PointString(_curveDefine.Caption+"/"+fitting.GetFittingExpression(CurveDefine.MethodName),x, y)));

                    }
                }
            }
        }
        public void Fill(List<GraphPointDefine> points)
        {
            if (_lineItem != null)
            {
                _lineItem.Clear();
                DataAnalysis.Fitting fitting = new DataAnalysis.Fitting();
                for (int i = 0; i < points.Count; i++)
                {
                    double x = points[i].X, y = points[i].Y;
                    _lineItem.AddPoint(new ZedGraph.PointPair(x, y, Graph.PointString(_curveDefine.Caption + "/" + fitting.GetFittingExpression(CurveDefine.MethodName), x, y)));
                }
            }
        }

        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Start", _start.ToString());
            keyValue.Add("End", _end.ToString());
            keyValue.Add("CurveDefine", _curveDefine.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            double.TryParse(keyValue.GetValueByKey("Start"), out _start);
            double.TryParse(keyValue.GetValueByKey("End"), out _end);
            CurveDefine.Parse(keyValue.GetValueByKey("CurveDefine"));
        }
             
        #endregion
    }
}
