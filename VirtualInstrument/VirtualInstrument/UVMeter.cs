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
    public partial class UVMeter : UserControl
    {
        public UVMeter()
        {
            InitializeComponent();
            this.SizeChanged += new EventHandler(UVMeter_SizeChanged);
            timer1.Start();
        }

        void UVMeter_SizeChanged(object sender, EventArgs e)
        {
            _needRefresh = true;
        }

        #region Props

        bool _scaleAuto, _needRefresh = false;
        double _value = 0,_maxValue = 10,_minValue = -10;
        string _caption = "", _unit = "";
        Color _captionColor = Color.Black, _unitColor = Color.Black, _scaleColor = Color.Black, _neeedleColor = Color.Red, 
            _originPointColor = Color.Red, _backgroundColor = Color.WhiteSmoke;
        public Color CaptionColor
        {
            get
            {
                return _captionColor;
            }
            set
            {
                _captionColor = value;
                _needRefresh = true;
            }
        }
        public Color UnitColor
        {
            get { return _unitColor; }
            set
            {
                _unitColor = value; _needRefresh = true;
            }
        }
        public Color ScaleColor
        {
            get { return _scaleColor; }
            set
            {
                _scaleColor = value; _needRefresh = true;
            }
        }
        public Color NeedleColor
        {
            get { return _neeedleColor; }
            set
            {
                _neeedleColor = value; _needRefresh = true;
            }
        }
        public Color OriginPointColor
        {
            get { return _originPointColor; }
            set
            {
                _originPointColor = value; _needRefresh = true;
            }
        }
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value; _needRefresh = true;
            }
        }

        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = (float)value; _needRefresh = true;
            }
        }
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = (float)value; _needRefresh = true;
            }
        }
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value; _needRefresh = true;
            }
        }
        public bool ScaleAuto
        {
            get { return _scaleAuto; }
            set
            {
                _scaleAuto = value; _needRefresh = true;
            }
        }
        public Color MeterBackColor
        {
            get { return BackColor; }
            set
            {
                BackColor = value; _needRefresh = true;
            }
        }

        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value; _needRefresh = true;
            }
        }
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value; _needRefresh = true;
            }
        }

        #endregion

        #region Variables
              //指针盘直径        
        float _radioRate = 0.9f, _fontRate = 0.1f,
            //标题字体宽度            标题字体高度                标题字体从上方往下的悬垂长度（到上沿）
            _captionWidthRate = 0.7f,_captionHeightRate = 0.12f,_captionDeclineRate = 0.6f,
            //单位字体宽度             单位字体高度            单位字体从上方往下的悬垂长度（到上沿）
            _unitWidthRate = 0.4f,_unitHeightRate = 0.1f,_unitDeclineRate = 0.15f,
            //中心原点从上方往下的悬垂长度（到上沿）  中心原点直径
            _originPointDeclineRate = 0.9f,_originPointRadioRate = 0.04f,
            //指针长度
            _needleLengthRate = 0.6f,_needleWidthRate = 0.02f,
            //主刻度长度                          主刻度起始点（远端）           辅刻度长度                           辅刻度起始点（远端）                 主刻度字体大小                主刻度字体起始位置
            _mainScaleLineLengthRate = 0.1f,_mainScaleLinePositionRate = 0.68f, _minorScaleLineLengthRate = 0.05f,_minorScaleLinePositionRate = 0.68f, _mainScaleTextRate  = 0.03f , _mainScaleTextPositionRate = 0.90f;
                //刻度开始角度     刻度结束角度
        float _startAngle =-240, _endAngle = -120;
              //主刻度数量        辅刻度数量
        int _mainScaleCount = 4,_minorScaleCount = 5;
        public int MainScaleCount
        {
            get { return _mainScaleCount; }
            set 
            { 
                if (value > 0) 
                    _mainScaleCount = value; 
                else 
                    _mainScaleCount = 4; 
            }
        }
        public int MinorScaleCount
        {
            get
            {
                return _minorScaleCount;

            }
            set
            {
                if (value > 0)
                    _minorScaleCount = value;
                else
                    _minorScaleCount = 5;
            }
        }
        public float StartAngle
        {
            get { return _startAngle; }
            set { _startAngle = value; }
        }
        public float EndAngle
        {
            get { return _endAngle; }
            set { _endAngle = value; }
        }
        RectangleF CaptionRectangleF
        {
            get
            {
                return new RectangleF(new PointF(Width * _captionWidthRate / 2, _captionDeclineRate * Height), new SizeF(Width * _captionWidthRate, Height * _captionHeightRate));
            }
        }
        float CaptionFontSizeF
        {
            get
            {
                return Height * _captionHeightRate * 0.8f;
            }
        }
        float UnitFontSizeF
        {
            get
            {
                return Height * _unitHeightRate * 0.8f;
            }
        }
        float ScaleFontSizeF
        {
            get
            {
                return _mainScaleTextRate * SourceLength;
            }
        }
        RectangleF UnitRectangleF
        {
            get
            {
                return new RectangleF(new PointF(Width * _unitWidthRate / 2, _unitDeclineRate * Height), new SizeF(Width * _unitWidthRate, Height * _unitHeightRate));
            }
        }
        float SourceLength
        {
            get
            {
                return Math.Min(Width, Height);
            }
        }
        float Radio
        {
            get
            {
                return _radioRate * SourceLength;
            }
        }
        float NeedleLength
        {
            get
            {
                return _needleLengthRate * SourceLength;
                
            }
        }
        float MainScaleLineLength
        {
            get
            {
                return _mainScaleLineLengthRate * SourceLength;
            }
        }
        float MinorScaleLineLength
        {
            get
            {
                return _minorScaleLineLengthRate * SourceLength;
            }
        }
        float ScaleFontSizef
        {
            get
            {
                return _fontRate * SourceLength;
            }
        }
        float MainScaleAngleUnit
        {
            get
            {
                return (_endAngle - _startAngle) / _mainScaleCount;
            }
        }
        float MinorScaleAngleUnit
        {
            get
            {
                return MainScaleAngleUnit / _minorScaleCount;
            }
        }
        float OriginPointRadio
        {
            get { return _originPointRadioRate * SourceLength; }
        }
        double MainScaleValueUnit
        {
            get
            {
                return (MaxValue - MinValue) / MainScaleCount;
            }
        }
        
        #endregion

        #region Methods
        void LoadDefault()
        {
            MaxValue = 100;
            MinValue = -100;
            Value = 0;
            ScaleAuto = false;
            MeterBackColor = Color.Empty;
            Caption = "";
            Unit = "";
        }
        public void Initialize(string caption, string unit, double maxValue, double minValue, bool scaleAuto, Color meterBackColor)
        {
            Caption = caption;
            Unit = unit;
            MaxValue = maxValue;
            MinValue = minValue;
            ScaleAuto = scaleAuto;
            MeterBackColor = meterBackColor;
        }
        void SetValue(double value)
        {
            Value = (float)value;
        }
        #endregion

        #region Draw
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
            DrawScale(g);
            DrawCaption(g);
            DrawUnit(g);
            //DrawOriginPoint();
            DrawNeedle(g);
        }
        float AngleToRadian(float angle)
        {
            return angle * (float)Math.PI / 180;
        }
        void DrawBackground(Graphics g)
        {
            g.Clear(BackgroundColor);
        }
        void DrawScale(Graphics g)
        {
            using (Pen pen = new Pen(ScaleColor))
            {
                float xSource = Width / 2  ;
                float ySource = _originPointDeclineRate  * SourceLength;
                g.TranslateTransform(xSource, ySource);
                
                //画圆心
                g.FillEllipse(new SolidBrush(OriginPointColor), new RectangleF(OriginPointRadio * -1, OriginPointRadio * -1, OriginPointRadio*2, OriginPointRadio*2));

                g.RotateTransform(StartAngle);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.FormatFlags = StringFormatFlags.NoWrap;
                for (int i = 0; i < MainScaleCount; i++)
                {
                    pen.Width = 2f;
                    g.DrawLine(pen, new PointF(0, _mainScaleLinePositionRate * SourceLength), new PointF(0, _mainScaleLinePositionRate * SourceLength + MainScaleLineLength));
                    g.RotateTransform(180);

                    g.DrawString((MinValue + MainScaleValueUnit * i).ToString(), new Font("Times New Roman", ScaleFontSizef), new SolidBrush(ScaleColor), 0,-1* _mainScaleTextPositionRate * SourceLength, format);
                    g.RotateTransform(180);
                    for (int j = 0; j < MinorScaleCount; j++)
                    {
                        g.RotateTransform(MinorScaleAngleUnit);
                        pen.Width = 1f;
                        g.DrawLine(pen, new PointF(0, _minorScaleLinePositionRate * SourceLength), new PointF(0, _minorScaleLinePositionRate * SourceLength + MinorScaleLineLength));
                    }
                    //g.RotateTransform(MinorScaleAngleUnit);

                }
                g.DrawLine(pen, new PointF(0, _mainScaleLinePositionRate * SourceLength), new PointF(0, _mainScaleLinePositionRate * SourceLength + MainScaleLineLength));
                g.RotateTransform(180);
                g.DrawString(MaxValue.ToString(), new Font("Times New Roman", ScaleFontSizef), new SolidBrush(ScaleColor), 0, -1*_mainScaleTextPositionRate * SourceLength, format);
                g.RotateTransform(180);
            }
        }
        void DrawCaption(Graphics g)
        {
            g.ResetTransform();
            float xSource = Width / 2;
            float ySource = _originPointDeclineRate * SourceLength;
            g.TranslateTransform(xSource, ySource);
            Font font = new Font(Form.DefaultFont.FontFamily, CaptionFontSizeF, FontStyle.Bold);
            using (SolidBrush brush = new SolidBrush(CaptionColor))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                g.DrawString(Caption, font, brush, new RectangleF(-1 * _captionWidthRate * SourceLength / 2,-1* _captionDeclineRate * SourceLength, _captionWidthRate * SourceLength, _captionHeightRate * SourceLength),format);
            }
        }
        void DrawUnit(Graphics g)
        {
            g.ResetTransform();
            float xSource = Width / 2;
            float ySource = _originPointDeclineRate * SourceLength;
            g.TranslateTransform(xSource, ySource);
            Font font = new Font(Form.DefaultFont.FontFamily, UnitFontSizeF, FontStyle.Bold);
            using (SolidBrush brush = new SolidBrush(UnitColor))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                g.DrawString(Unit, font, brush, new RectangleF(-1 * _unitWidthRate * SourceLength / 2,-1* _unitDeclineRate * SourceLength, _unitWidthRate * SourceLength, _unitHeightRate * SourceLength),format);
            }
        }
        void DrawNeedle(Graphics g)
        {
            g.ResetTransform();
            float xSource = Width / 2;
            float ySource = _originPointDeclineRate * SourceLength;
            g.TranslateTransform(xSource, ySource);
            float angle = (float)((_value - MinValue) / (MaxValue - MinValue) * (_endAngle - _startAngle) + _startAngle);
            if (angle < _startAngle)
            {
                angle = _startAngle;
            }
            else if (angle > _endAngle)
            {
                angle = _endAngle;
            }
            g.RotateTransform(angle);
            Pen pen = new Pen(NeedleColor);
            pen.Width = SourceLength * _needleWidthRate;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
           
            g.DrawLine(pen, new PointF(0, _needleLengthRate * SourceLength ), new PointF(0, 0));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            DrawAll(g);
        }

        #endregion


        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("MaxValue", MaxValue.ToString());
            keyValue.Add("MinValue", MinValue.ToString());
            keyValue.Add("Caption", Caption);
            keyValue.Add("Unit", Unit);
            keyValue.Add("BackColor", KeyValue.KeyValue.ColorToString(MeterBackColor));
            keyValue.Add("ScaleAuto", _scaleAuto.ToString());
            keyValue.Add("Value", _value.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            double maxValue, minValue;
            double.TryParse(keyValue.GetValueByKey("MaxValue"), out maxValue);
            double.TryParse(keyValue.GetValueByKey("MinValue"), out minValue);
            MaxValue = maxValue;
            MinValue = minValue;
            Caption = keyValue.GetValueByKey("Caption");
            Unit = keyValue.GetValueByKey("Unit");
            MeterBackColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("BackColor"));
            bool.TryParse(keyValue.GetValueByKey("ScaleAuto"), out _scaleAuto);
            double.TryParse(keyValue.GetValueByKey("Value"), out _value);
            _needRefresh = true;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefresh)
            {
                _needRefresh = false;
                this.Invalidate();
            }
        }
    }
}
