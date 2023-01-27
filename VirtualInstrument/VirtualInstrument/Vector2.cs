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
    public partial class Vector2 : UserControl
    {
        public Vector2()
        {
            InitializeComponent();
            this.SizeChanged += new EventHandler(Vector2_SizeChanged);
            LoadDefault();
            timer1.Start();
        }

        void Vector2_SizeChanged(object sender, EventArgs e)
        {
            _needRefreshAll = true;
        }

        #region Props
        List<Classes.VectorLine> _lines = new List<VirtualInstrument.Classes.VectorLine>();
        List<Classes.VectorLineConnectCollection> _connectedLines = new List<VirtualInstrument.Classes.VectorLineConnectCollection>();
        double _max = 10, _min = 0;
        Color _backColor = Color.White, _axisColor = Color.Black, _scaleColor =Color.DarkCyan ,_connectLineColor = Color.LightPink;
        string _name, _caption = "",_unit = "";
        bool _scaleAuto = true, _drawGrid = true;
        int _segmentCount = 5;
        List<Classes.VectorLine> _flashLines = new List<VirtualInstrument.Classes.VectorLine>();
        bool _flashMode = false;
        public bool FlashMode
        {
            get { return _flashMode; }
            set { _flashMode = value; _needRefreshAll = true; }
        }
        public List<Classes.VectorLine> Lines
        {
            get { return _lines; }
        }
        public List<Classes.VectorLineConnectCollection> ConnectedLines
        {
            get { return _connectedLines; }
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
            set { _segmentCount = value; _needRefreshAll = true; }
        }
        public bool DrawGrid
        {
            get { return _drawGrid; }
            set { _drawGrid = value; _needRefreshAll = true; }
        }
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; _needRefreshAll = true; }
        }
        public bool HaveFlashLines
        {
            get { return _flashLines.Count > 0; }
        }
        #endregion

        #region Variables
        bool _needRefreshAll = false;
        float _radioRate = 0.46f,_captionHeightRate = 0.15f, _unitFontRate = 0.1f, _scaleFontRate = 0.05f,_valueFontRate = 0.03f;
        string _fontName = "Times New Roman";
        float Radio
        {
            get { return Math.Min(Width, Height * (1 - _captionHeightRate )) * _radioRate; }
        }
        RectangleF CaptionRectF
        {
            get
            {
                return new RectangleF(0, 0, Width, _captionHeightRate * Height);
            }
        }
        PointF CenterPoint
        {
            get
            {
                return new PointF(Width / 2,  Height * ((1 - _captionHeightRate ) / 2+_captionHeightRate));
            }
        }
        #endregion

        #region Init Methods
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

        public void Initialize(string name, string caption, Color backColor, Color axisColor, Color scaleColor, int segmentCount, double max, double min, bool scaleAuto)
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
        public void SaveFlashLines()
        {
            _flashLines.Clear();
            for (int i = 0; i < _lines.Count; i++)
            {
                _flashLines.Add(new VirtualInstrument.Classes.VectorLine(_lines[i].ToString()));
            }
        }
        #endregion

        #region Draw Methods
        public void Clip()
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                //g.TranslateTransform(0,0);
                this.DrawAll(g);
            }
            Clipboard.SetDataObject(bitmap, true);

        }
        void DrawAll(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//绘图模式 默认为粗糙模式，将会出现锯齿！

            DrawBackground(g);
            DrawCaption(g);
            DrawAxis(g);
            DrawLines(g,_flashMode);
            DrawLinkLines(g,_flashMode);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics g = e.Graphics;
            DrawAll(g);
        }
        void DrawBackground(Graphics g)
        {
            g.Clear(_backColor);
        }
        void DrawCaption(Graphics g)
        {
            g.ResetTransform();
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            g.DrawString(_caption, new Font(_fontName, CaptionRectF.Height * 0.6f, FontStyle.Bold), new SolidBrush(_axisColor), CaptionRectF, format);
        }
        void DrawAxis(Graphics g)
        {
            g.ResetTransform();
            g.TranslateTransform(CenterPoint.X, CenterPoint.Y);
            Pen axisPen = new Pen(_axisColor);
            axisPen.Width = 1.5f;
            axisPen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            axisPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            //画坐标轴
            g.DrawLine(axisPen, new PointF(-1.05f * Radio , 0), new PointF(Radio*1.05f, 0));
            g.DrawLine(axisPen, new PointF(0, 1.05f*Radio), new PointF(0, -1.05f * Radio));
            //画坐标单位（在X轴最左侧）
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            g.DrawString(_unit, new Font(_fontName, _unitFontRate * Radio, FontStyle.Bold), new SolidBrush(AxisColor), new PointF(0, -1.05f * Radio));

            Pen scaleFontPen = new Pen(_axisColor);
            scaleFontPen.Width = 1.5f;

            Pen scaleLinePen = new Pen(_scaleColor);
            scaleLinePen.DashOffset = 1.5f;
            scaleLinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            scaleLinePen.Width = 1f;

            format.Alignment = StringAlignment.Center;
            
            for (int i = 0; i <= _segmentCount; i++)
            {
                double value =Math.Round( Minimum + (Maximum - Minimum ) / SegmentCount * i,2 );
                float length = Radio / SegmentCount * i;

                //同轴圆
                if (_drawGrid)
                {
                    g.DrawEllipse(scaleLinePen, new RectangleF(-1 * length, -1 * length, 2 * length, 2 * length));
                }

                //X轴
                g.DrawLine(scaleFontPen, new PointF(length, 2f), new PointF(length, -2f));
                g.DrawString(value.ToString(), new Font(_fontName, _scaleFontRate * Radio, FontStyle.Italic), new SolidBrush(_axisColor), new PointF(length, 5f), format);

            }
        }
        void DrawLines(Graphics g,bool drawFlashLines)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            List<Classes.VectorLine> lines;
            if (drawFlashLines)
            {
                lines = _flashLines;
            }
            else
            {
                lines = _lines;
            }
            for (int i = 0; i < lines.Count; i++)
            {
                g.ResetTransform();
                g.TranslateTransform(CenterPoint.X, CenterPoint.Y);
                Pen pen = new Pen(lines[i].LineColor);
                pen.Width = lines[i].WidthF;
                if( lines[i].DrawArrow )
                {
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.DiamondAnchor;
                }
                g.RotateTransform(-90);
                g.RotateTransform(-1f*(float)lines[i].Radius );
                float length;
                if (Maximum > Minimum)
                {
                    length = (float)((lines[i].Length - Minimum) / (Maximum - Minimum) * Radio);
                }
                else
                {
                    length = 0;
                }
                g.DrawLine(pen, new PointF(0, 0), new PointF(0, (float)length));
                string str = lines[i].Caption + "("+lines[i].Length.ToString()+")";

                g.TranslateTransform(0, length);
                g.RotateTransform(90);
                g.RotateTransform( (float)lines[i].Radius);

                g.DrawString(str, new Font(_fontName, _valueFontRate * Radio, FontStyle.Bold), new SolidBrush(lines[i].LineColor), new PointF(0, 0), format);
            }
        }
        void DrawLinkLines(Graphics g,bool drawFlashLines)
        {
            g.ResetTransform();
            g.TranslateTransform(CenterPoint.X, CenterPoint.Y);
            for (int i = 0; i < _connectedLines.Count; i++)
            {
                Classes.VectorLine mainLine = GetLine(_connectedLines[i].MainLineName,drawFlashLines);
                if (mainLine != null)
                {
                    float mainLength;
                    if (Maximum > Minimum)
                    {
                        mainLength = (float)((mainLine.Length - Minimum) / (Maximum - Minimum) * Radio);
                    }
                    else
                    {
                        mainLength = 0;
                    }
                    PointF mainPoint = new PointF((float)(mainLength * Math.Cos(mainLine.Radius * Math.PI / 180)), -1 * (float)(mainLength * Math.Sin(mainLine.Radius * Math.PI / 180)));
                    foreach (string name in _connectedLines[i].SubLineNames)
                    {
                        Classes.VectorLine subLine = GetLine(name,drawFlashLines);
                        if (subLine != null)
                        {
                            float subLength;
                            if (Maximum > Minimum)
                            {
                                subLength = (float)((subLine.Length - Minimum) / (Maximum - Minimum) * Radio);
                            }
                            else
                            {
                                subLength = 0;
                            }
                            PointF subPoint = new PointF((float)(subLength * Math.Cos(subLine.Radius * Math.PI / 180)), -1 * (float)(subLength * Math.Sin(subLine.Radius * Math.PI / 180)));
                            Pen pen = new Pen(_connectLineColor);
                            pen.Width = 1f;
                            g.DrawLine(pen, mainPoint, subPoint);
                        }
                    }
                }
            }
        }
        Classes.VectorLine GetLine(string name,bool fromFlashLines)
        {
            List<Classes.VectorLine> lines;
            if (fromFlashLines)
            {
                lines = _flashLines;
            }
            else
            {
                lines = _lines;
            }

            Classes.VectorLine line = null;
            for (int i = 0; i < lines.Count; i++)
            {
                if (string.Equals(lines[i].Name, name))
                {
                    line = lines[i];
                    break;
                }
            }
            return line;
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
        string FlashLines2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _flashLines.Count; i++)
            {
                keyValue.Add(i.ToString(), _flashLines[i].ToString());
            }
            return keyValue.ToString();
        }

        void ParseFlashLines(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _flashLines.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _flashLines.Add(new VirtualInstrument.Classes.VectorLine(keyValue.GetValueByKey(i.ToString())));
            }

        }
        string LinkedLines2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < this._connectedLines.Count; i++)
            {
                keyValue.Add(i.ToString(), _connectedLines[i].ToString());
            }
            return keyValue.ToString();
        }

        void ParseLinkedLines(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _connectedLines.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                Classes.VectorLineConnectCollection line = new VirtualInstrument.Classes.VectorLineConnectCollection(keyValue.GetValueByKey(i.ToString()));
                _connectedLines.Add(line);
            }

        }

        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("Unit", _unit);
            keyValue.Add("Max", _max.ToString());
            keyValue.Add("Min",_min.ToString()  );
            keyValue.Add("BackColor", KeyValue.KeyValue.ColorToString(_backColor));
            keyValue.Add("AxisColor", KeyValue.KeyValue.ColorToString(_axisColor));
            keyValue.Add("ScaleColor", KeyValue.KeyValue.ColorToString(_scaleColor));
            keyValue.Add("SegmentCount", _segmentCount.ToString());
            keyValue.Add("ScaleAuto", _scaleAuto.ToString());
            keyValue.Add("DrawGrid", _drawGrid.ToString());
            keyValue.Add("Lines", Lines2Str());
            keyValue.Add("FlashLines", FlashLines2Str());
            keyValue.Add("LinkedLines", LinkedLines2Str());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            _unit = keyValue.GetValueByKey("Unit");
            double.TryParse(keyValue.GetValueByKey("Max"), out _max);
            double.TryParse(keyValue.GetValueByKey("Min"), out _min);
            _backColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("BackColor"));
            _axisColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("AxisColor"));
            _scaleColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("ScaleColor"));
            int.TryParse(keyValue.GetValueByKey("SegmentCount"), out _segmentCount);
            bool.TryParse(keyValue.GetValueByKey("ScaleAuto"), out _scaleAuto);
            bool.TryParse(keyValue.GetValueByKey("DrawGrid"), out _drawGrid);
            ParseLines(keyValue.GetValueByKey("Lines"));
            ParseFlashLines(keyValue.GetValueByKey("FlashLines"));
            ParseLinkedLines(keyValue.GetValueByKey("LinkedLines"));
            FlashMode = true;
            _needRefreshAll = true;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshAll)
            {
                _needRefreshAll = false;
                this.Invalidate();
            }
        }
    }
}
