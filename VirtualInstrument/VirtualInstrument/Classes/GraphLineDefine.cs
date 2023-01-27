using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.Classes
{
    public class GraphLineDefine
    {
        public GraphLineDefine()
        {
            LoadDefault();
        }
        public GraphLineDefine(string columnName, string columnCaption, int decimalCount, Color lineColor, bool pointVisible, bool lineVisible, float lineWidthF, bool isY2Axis, bool isSmooth, float smoothTention,bool draw3DPoint)
        {
            Initialize(columnName, columnCaption, decimalCount, lineColor, pointVisible, lineVisible, lineWidthF, isY2Axis, isSmooth, smoothTention, draw3DPoint);
        }
        public GraphLineDefine(string columnName, string columnCaption, int decimalCount, Color lineColor, bool pointVisible, bool lineVisible, float lineWidthF, bool isY2Axis, bool isSmooth, float smoothTention)
        {
            Initialize( columnName,columnCaption,decimalCount,lineColor,pointVisible,lineVisible,lineWidthF,isY2Axis,isSmooth,smoothTention ,false);
        }
        public GraphLineDefine(string value)
        {
            Parse(value);
        }
        #region Props
        ColumnDefine _columnDefine;
        Color _lineColor;
        bool _pointVisible, _lineVisible,_isY2Axis,_isSmooth;
        float _lineWidthf,_smoothTention;
        ZedGraph.LineItem _lineItem;
        bool _draw3DPoint = false;
        Image _pointImage = null;
        bool _isHighLighted = false;
        public Image PointImage
        {
            get { return _pointImage; }
            set { _pointImage = value; }
        }
        public bool Draw3DPoint
        {
            get { return _draw3DPoint; }
            set { _draw3DPoint = value; }
    }
        public ColumnDefine ColumnDefine
        {
            get { return _columnDefine; }
            set{ _columnDefine = value;}
        }
        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }
        public bool PointVisible
        {
            get { return _pointVisible; }
            set { _pointVisible = value; }
        }
        public bool LineVisible
        {
            get { return _lineVisible; }
            set { _lineVisible = value; }
        }
        public float LineWidthF
        {
            get { return _lineWidthf; }
            set { _lineWidthf = value; }
        }
        public bool IsSmooth
        {
            get { return _isSmooth; }
            set { _isSmooth = value; }
        }
        public float SmoothTention
        {
            get { return _smoothTention; }
            set { _smoothTention = value; }
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
            _columnDefine = new ColumnDefine();
            _lineColor = System.Drawing.SystemColors.ControlText;
            _pointVisible = false;
            _lineVisible = true;
            _lineWidthf = 1f;
            _isY2Axis = false;
            _isSmooth = false;
            _smoothTention = 1f;
            _draw3DPoint = false;
        }
        void Initialize(string columnName, string columnCaption, int decimalCount, Color lineColor, bool pointVisible, bool lineVisible, float lineWidthF,bool isY2Axis,bool isSmooth,float smoothTention,bool draw3DPoint)
        {
            _columnDefine = new ColumnDefine(columnName, columnCaption, decimalCount);
            _lineColor = lineColor;
            _lineVisible = lineVisible;
            _pointVisible = pointVisible;
            _lineWidthf = lineWidthF;
            _isY2Axis = isY2Axis;
            _isSmooth = isSmooth;
            _smoothTention = smoothTention;
            _draw3DPoint = draw3DPoint;
        }
        public ZedGraph.LineItem CreateLineItem(string sectionName)
        {
            ZedGraph.LineItem lineItem = new ZedGraph.LineItem(ColumnDefine.ColumnCaption);
            lineItem.Color = LineColor;
            lineItem.Line.IsVisible = LineVisible;
            lineItem.Line.IsSmooth = IsSmooth;
            lineItem.Line.SmoothTension = SmoothTention;
            //lineItem.Line.Width = Graph.DEFAULT_LINE_WIDTH;
            lineItem.Line.Width = LineWidthF;

            if (Draw3DPoint)
            {
                ZedGraph.Symbol symbol = new ZedGraph.Symbol();
                symbol.Type = ZedGraph.SymbolType.Circle;
                if (_pointImage != null)
                {
                    symbol.Fill = new ZedGraph.Fill(_pointImage, System.Drawing.Drawing2D.WrapMode.Tile);
                }
                else
                {
                    symbol.Fill = new ZedGraph.Fill(new Color[] { Color.LightGray, Color.White, Color.DarkGray }, 45f);
                }
                symbol.Size = Graph.POINTIMAGE_SIZE;
                lineItem.Symbol = symbol;
            }
            else
            {
                lineItem.Symbol = new ZedGraph.Symbol(ZedGraph.SymbolType.Circle, LineColor);
                lineItem.Symbol.Fill = new ZedGraph.Fill(LineColor);
                lineItem.Symbol.Size = Graph.POINT_SIZE;
            }
            lineItem.Symbol.IsVisible = PointVisible;
            lineItem.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.Line, sectionName, ColumnDefine.ColumnName, this.ToString());
            lineItem.IsY2Axis = IsY2Axis;
            _lineItem = lineItem;

            _lineItem.Line.IsAntiAlias = true;

            return lineItem;
        }
        public void AddPoint(double x, double y)
        {
            if (_lineItem != null)
            {
                _lineItem.AddPoint(new ZedGraph.PointPair(x, y, Graph.PointString(_columnDefine.ColumnCaption , x, y)));
            }
        }
        public void AddPoint(string sectionCaption, double x, double y)
        {
            if (_lineItem != null)
            {
                _lineItem.AddPoint(new ZedGraph.PointPair(x, y, Graph.PointString(_columnDefine.ColumnCaption + "/" + sectionCaption, x, y)));
                //_lineItem.AddPoint(new ZedGraph.PointPair(x, y));
            }
        }
        public void ClearPoints()
        {
            if (_lineItem != null)
            {
                _lineItem.Clear();
            }
        }
        public void SetHighLight()
        {
            if (_lineItem != null)
            {
                if (!_isHighLighted)
                {
                    _isHighLighted = true;
                    _lineItem.Line.Width = Graph.PROMPT_LINE_WIDTH;
                    _lineItem.Symbol.Size = Graph.PROMPT_POINT_SIZE;
                }
                else
                {
                    _isHighLighted = false;
                    _lineItem.Line.Width = Graph.DEFAULT_LINE_WIDTH;
                    _lineItem.Symbol.Size = Graph.POINT_SIZE;
                }
            }
        }
        public void SetHighLight(bool highLight)
        {
            if (_lineItem != null)
            {
                if (highLight)
                {
                    _isHighLighted = true;
                    _lineItem.Line.Width = Graph.PROMPT_LINE_WIDTH;
                    _lineItem.Symbol.Size = Graph.PROMPT_POINT_SIZE;
                }
                else
                {
                    _isHighLighted = false;
                    _lineItem.Line.Width = Graph.DEFAULT_LINE_WIDTH;
                    _lineItem.Symbol.Size = Graph.POINT_SIZE;
                }
            }
        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("ColumnDefine", _columnDefine.ToString());
            keyValue.Add("LineColor", KeyValue.KeyValue.ColorToString(_lineColor));
            keyValue.Add("PointVisible", _pointVisible.ToString());
            keyValue.Add("LineVisible", _lineVisible.ToString());
            keyValue.Add("LineWidthF", _lineWidthf.ToString());
            keyValue.Add("IsY2Axis", _isY2Axis.ToString());
            keyValue.Add("IsSmooth", _isSmooth.ToString());
            keyValue.Add("Draw3DPoint", _draw3DPoint.ToString());

            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _columnDefine = new ColumnDefine(keyValue.GetValueByKey("ColumnDefine"));
            _lineColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("LineColor"));
            bool.TryParse(keyValue.GetValueByKey("PointVisible"), out _pointVisible);
            bool.TryParse(keyValue.GetValueByKey("LineVisible"), out _lineVisible);
            float.TryParse(keyValue.GetValueByKey("LineWidthF"), out _lineWidthf);
            bool.TryParse(keyValue.GetValueByKey("IsY2Axis"), out _isY2Axis);
            bool.TryParse(keyValue.GetValueByKey("IsSmooth"), out _isSmooth);
            float.TryParse(keyValue.GetValueByKey("SmoothTention"), out _smoothTention);
            bool.TryParse(keyValue.GetValueByKey("Draw3DPoint"), out _draw3DPoint);

        }
        #endregion
    }
}
