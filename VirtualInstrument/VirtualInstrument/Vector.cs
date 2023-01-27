using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualInstrument
{
    public partial class Vector : UserControl
    {
        public Vector()
        {
            InitializeComponent();
            LoadDefault();
            timer1.Start();
        }
        #region Props
        List<Classes.VectorLine> _lines = new List<VirtualInstrument.Classes.VectorLine>();
        double _max, _min;
        Color _backColor,_axisColor, _scaleColor;
        string _name, _caption;
        bool _scaleAuto,_drawGrid;
        int _segmentCount = 5;
        float _captionHRate = 0.2f, _unitVRate = 0.2f, _fontRate = 0.3f;

        public List<Classes.VectorLine> Lines
        {
            get { return _lines; }
        }
        public double Maximum
        {
            get { return _max; }
            set { _max = value; _needRefreshAll = true; }
        }
        public double Minimum
        {
            get { return _min; }
            set { _min = value; _needRefreshAll = true; }
        }
        public bool ScaleAuto
        {
            get { return _scaleAuto; }
            set { _scaleAuto = value; _needRefreshAll = true; }
        }
        public Color VectorBackColor
        {
            get { return _backColor; }
            set { _backColor = value; _needRefreshAll = true; }
        }
        public Color AxisColor
        {
            get { return _axisColor; }
            set { _axisColor = value; _needRefreshAll = true; }
        }
        public Color ScaleColor
        {
            get { return _scaleColor; }
            set { _scaleColor = value; _needRefreshAll = true; }
        }
        public string VectorName
        {
            get { return _name; }
            set { _name = value; _needRefreshAll = true; }
        }
        public string VectorCaption
        {
            get { return _caption; }
            set { _caption = value; _needRefreshAll = true; }
        }
        public int SegmentCount
        {
            get { return _segmentCount; }
            set { _segmentCount = value; }
        }
        public bool DrawGrid
        {
            get { return _drawGrid; }
            set { _drawGrid = value; }
        }
        #endregion

        #region Variables
        bool _needRefreshAll = false, _needRefreshValue = true;
        /// <summary>
        /// 图框
        /// </summary>
        Rectangle GraphPanel
        {
            get
            {
                Rectangle rect;
                if (this.Bounds.Width < 20 || this.Bounds.Height < 20)
                {
                    rect = this.Bounds;
                }
                else
                {
                    rect = new Rectangle(10, 10, this.Bounds.Width - 20, this.Bounds.Height - 20);
                }
                return rect;
            }
        }
        /// <summary>
        /// 作图中心点
        /// </summary>
        Point CenterPoint
        {
            get
            {
                return new Point(GraphPanel.Width / 2, GraphPanel.Height / 2);
            }
        }
        /// <summary>
        /// 值的变化范围
        /// </summary>
        double ValueScale
        {
            get
            {
                return _max - _min;

            }
        }
        /// <summary>
        /// 作图最大半径
        /// </summary>
        int GraphRadios
        {
            get
            {
                return Math.Min(GraphPanel.Width / 2, GraphPanel.Height / 2);
            }
        }
        /// <summary>
        /// 值与作图半径的换算因子
        /// </summary>
        float ScaleFactor
        {
            get
            {
                if (ValueScale == 0)
                {
                    return 1;
                }
                else
                {
                    return (float)(GraphRadios * 2 / ValueScale);
                }
            }
        }
        float FontScaleFactor
        {
            get
            {
                return GraphRadios / 100;
            }
        }
        float FontScaledSize
        {
            get
            {
                float emSize = FontScaleFactor * 8f;
                if (emSize <= 0)
                {
                    emSize = 6f;
                }
                return emSize;
            }

        }
        /// <summary>
        /// 能否作图
        /// </summary>
        public bool CanDraw
        {
            get
            {
                return ValueScale > 0;
            }
        }
        /// <summary>
        /// 线帽长度
        /// </summary>
        double CapLength
        {
            get { return (Maximum - Minimum) / 40; }
        }
        /// <summary>
        /// 线帽夹角
        /// </summary>
        int CapAngle
        {
            get { return 60; }
        }
        Rectangle CaptionRect
        {
            get
            {
                int h = (int)Math.Round(Height * _captionHRate, 0);
                int w = (int)Math.Round(Width * (1 - _unitVRate), 0);
                return new Rectangle(0, 0, w, h);
            }
        }

        #endregion

        #region Methods
        void LoadDefault()
        {
            _lines.Clear();
            _caption = "";
            _backColor = System.Drawing.SystemColors.Control;
            _axisColor = System.Drawing.SystemColors.ActiveBorder;
            _segmentCount = 5;
            _max = 10;
            _min = 0;
            _scaleAuto = false;
            _scaleColor = System.Drawing.SystemColors.InactiveBorder;
            _name = "";
            _drawGrid = false;
            
        }
        public void Initialize(string name, string caption, Color backColor,Color axisColor, Color scaleColor, int segmentCount, double max, double min,bool scaleAuto)
        {
            _name = name;
            _caption = caption;
            _backColor = backColor;
            _axisColor = axisColor;
            _scaleColor = scaleColor;
            _segmentCount = segmentCount;
            _max = max;
            _min = min;
            _scaleAuto = scaleAuto;
            _lines.Clear();
            _needRefreshAll = true;
            _drawGrid = false;
        }
        public void SyncLines(List<Classes.VectorLine> lines)
        {
            _lines.Clear();
            _lines.AddRange(lines);
            _needRefreshAll = true;
        }
        public void RefreshAll()
        {
            _needRefreshAll = true;
        }
        Point ValueEndPoint(double value, double angle)
        {
            return new Point(CenterPoint.X + (int)Math.Round(Math.Cos(angle * Math.PI / 180) * value * ScaleFactor, 0), CenterPoint.Y - (int)Math.Round(Math.Sin(angle * Math.PI / 180) * value * ScaleFactor, 0));
        }
        Point EndCapPoint1(Point endPoint, double value, double angle)
        {
            int bx, by, x, y;
            double capLengthAbs = Math.Abs(CapLength) * ScaleFactor;
            bx = (int)Math.Round(capLengthAbs * Math.Abs(Math.Cos((CapAngle / 2 + angle) * Math.PI / 180)));
            by = (int)Math.Round(capLengthAbs * Math.Abs(Math.Sin((CapAngle / 2 + angle) * Math.PI / 180)));
            if (endPoint.X > CenterPoint.X)
            {
                x = endPoint.X - bx;
            }
            else
            {
                x = endPoint.X + bx;
            }
            if (endPoint.Y > CenterPoint.Y)
            {
                y = endPoint.Y - by;
            }
            else
            {
                y = endPoint.Y + by;
            }

            return new Point(x, y);
        }
        Point EndCapPoint2(Point endPoint, double value, double angle)
        {
            int bx, by, x, y;
            double capLengthAbs = Math.Abs(CapLength) * ScaleFactor;
            bx = (int)Math.Round(capLengthAbs * Math.Abs(Math.Cos((angle - CapAngle / 2) * Math.PI / 180)));
            by = (int)Math.Round(capLengthAbs * Math.Abs(Math.Sin((angle - CapAngle / 2) * Math.PI / 180)));
            if (endPoint.X > CenterPoint.X)
            {
                x = endPoint.X - bx;
            }
            else
            {
                x = endPoint.X + bx;
            }
            if (endPoint.Y > CenterPoint.Y)
            {
                y = endPoint.Y - by;
            }
            else
            {
                y = endPoint.Y + by;
            }

            return new Point(x, y);
        }

        void DrawBackground(Graphics g)
        {
            //using (SolidBrush brush = new SolidBrush(_backColor))
            //{
            //    g.FillRectangle(brush, GraphPanel);
            //}
            g.Clear(_backColor);
        }
        void DrawAxis(Graphics g)
        {
            System.Drawing.SolidBrush brush = new SolidBrush(AxisColor);
            System.Drawing.SolidBrush scaleBrush = new SolidBrush(ScaleColor);
            Pen scalePen = new Pen(scaleBrush);
            scalePen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            scalePen.DashOffset = 1f;
            scalePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            using (System.Drawing.Pen pen = new Pen(brush))
            {
                pen.Width = 0.8f;
                g.DrawLine(pen, new Point(CenterPoint.X - GraphRadios, CenterPoint.Y), new Point(CenterPoint.X + GraphRadios, CenterPoint.Y));
                g.DrawLine(pen, new Point(CenterPoint.X, CenterPoint.Y - GraphRadios), new Point(CenterPoint.X, CenterPoint.Y + GraphRadios));
                double centerValue = Math.Round((Maximum + Minimum) / 2, 2);
                g.DrawString(centerValue.ToString(), new Font("TimesNewRoman", FontScaledSize, FontStyle.Regular, GraphicsUnit.Pixel), brush, new PointF(CenterPoint.X, CenterPoint.Y - FontScaledSize));
                int interval = GraphRadios / _segmentCount;
                for (int i = 0; i < _segmentCount; i++)
                {
                    if (_drawGrid)
                    {
                        g.DrawEllipse(scalePen, CenterPoint.X - interval * (i + 1), CenterPoint.Y - interval * (i + 1), interval * 2 * (i + 1), interval * 2 * (i + 1));
                    }
                    double maxValue = Math.Round(Maximum / _segmentCount * (i + 1), 2);
                    double minValue = Math.Round(Minimum / _segmentCount * (i + 1), 2);
                    g.DrawString(maxValue.ToString(), new Font("Times New Roman", FontScaledSize, FontStyle.Regular, GraphicsUnit.Pixel), brush, new PointF(CenterPoint.X, CenterPoint.Y - interval * (i + 1)));
                    g.DrawString(maxValue.ToString(), new Font("Times New Roman", FontScaledSize, FontStyle.Regular, GraphicsUnit.Pixel), brush, new PointF(CenterPoint.X + interval * (i + 1), CenterPoint.Y - FontScaledSize));
                    g.DrawString(minValue.ToString(), new Font("Times New Roman", FontScaledSize, FontStyle.Regular, GraphicsUnit.Pixel), brush, new PointF(CenterPoint.X - interval * (i + 1), CenterPoint.Y - FontScaledSize));
                    g.DrawString(minValue.ToString(), new Font("Times New Roman", FontScaledSize, FontStyle.Regular, GraphicsUnit.Pixel), brush, new PointF(CenterPoint.X, CenterPoint.Y + interval * (i + 1) - FontScaledSize));
                }
            }

        }
        void DrawLines(Graphics g)
        {
            foreach (Classes.VectorLine line in _lines)
            {
                DrawLine(g, line);
            }
        }
        void DrawLine(Graphics g, Classes.VectorLine line)
        {
            SolidBrush brush = new SolidBrush(line.LineColor);
            using (Pen pen = new Pen(brush))
            {
                pen.Width = 2f;
                //pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;

                //pen.CustomEndCap.StrokeJoin = System.Drawing.Drawing2D.LineJoin.MiterClipped;
                if (CanDraw)
                {
                    double capLengthAbs = Math.Abs(CapLength) * ScaleFactor;
                    Point endPoint = ValueEndPoint(line.Length, line.Radius);
                    g.DrawLine(pen, CenterPoint, endPoint);

                    if (Math.Abs(line.Length) > CapLength)
                    {
                        g.DrawLine(pen, endPoint, EndCapPoint1(endPoint, line.Length, line.Radius));
                        g.DrawLine(pen, endPoint, EndCapPoint2(endPoint, line.Length, line.Radius));
                    } 
                    g.DrawString(line.Caption + "(" + line.Length.ToString() + "," + line.Radius.ToString() + ")", new Font("TimesNewRoman", FontScaledSize * 1.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Pixel), brush, new PointF(ValueEndPoint(line.Length, line.Radius).X, ValueEndPoint(line.Length, line.Radius).Y));

                }
            }

        }
        void DrawCaption(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(_scaleColor))
            {
                Rectangle rect = CaptionRect;
                float sizef = Classes.PublicMethods.GetFontSizef(rect.Width, rect.Height, _caption, _fontRate);
                g.DrawString(_caption, new Font("TimesNewRoman", sizef, FontStyle.Bold), brush, new PointF((float)rect.Left, (float)rect.Top));
            }

        }
        void DrawAll(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//绘图模式 默认为粗糙模式，将会出现锯齿！

            DrawBackground(g);
            DrawAxis( g );
            DrawCaption(g);
            DrawLines(g);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshAll)
            {
                _needRefreshAll = false;
                DrawAll(CreateGraphics());
            }
            if (_needRefreshValue)
            {
                _needRefreshValue = false;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            _needRefreshAll = true;
            base.OnPaint(e);
        }
        public void Clip()
        {
            Bitmap bitmap = new Bitmap(GraphPanel.Width, GraphPanel.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.TranslateTransform(GraphPanel.Top, GraphPanel.Left);
                this.DrawAll(g);
            }
            Clipboard.SetDataObject(bitmap, true);

        }
        #endregion

        #region Serialize
        string Lines2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _lines.Count; i++)
            {
                keyValue.Add(i.ToString(), _lines[i].ToString());
            }
            return keyValue.ToString();
        }

        void ParseLines(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _lines.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _lines.Add(new VirtualInstrument.Classes.VectorLine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("Max", _max.ToString());
            keyValue.Add("Min",_min.ToString()  );
            keyValue.Add("BackColor", KeyValue.KeyValue.ColorToString(_backColor));
            keyValue.Add("AxisColor", KeyValue.KeyValue.ColorToString(_axisColor));
            keyValue.Add("ScaleColor", KeyValue.KeyValue.ColorToString(_scaleColor));
            keyValue.Add("SegmentCount", _segmentCount.ToString());
            keyValue.Add("ScaleAuto", _scaleAuto.ToString());
            keyValue.Add("DrawGrid", _drawGrid.ToString());
            keyValue.Add("Lines", Lines2Str());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            double.TryParse(keyValue.GetValueByKey("Max"), out _max);
            double.TryParse(keyValue.GetValueByKey("Min"), out _min);
            _backColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("BackColor"));
            _axisColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("AxisColor"));
            _scaleColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("ScaleColor"));
            int.TryParse(keyValue.GetValueByKey("SegmentCount"), out _segmentCount);
            bool.TryParse(keyValue.GetValueByKey("ScaleAuto"), out _scaleAuto);
            bool.TryParse(keyValue.GetValueByKey("DrawGrid"), out _drawGrid);
            ParseLines(keyValue.GetValueByKey("Lines"));
        }
        #endregion
    }
}
