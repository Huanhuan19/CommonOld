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
    public partial class DigitalMeter : UserControl
    {
        public DigitalMeter()
        {
            InitializeComponent();
            this.timer1.Start();
            LoadDefault();

            //Initialize();
            this.SizeChanged += new EventHandler(DigitalMeter_SizeChanged);
        }

        void DigitalMeter_SizeChanged(object sender, EventArgs e)
        {
            label_value.Font = ValueFont;
            float size = ValueFont.Size * 0.2f;
            size = size < 9f ? 9f : size;
            label_caption.Font = new Font(ValueFont.FontFamily, size);
        }

        #region Props
        double  _value;
        float _fontSize;
        int _decimal;
        public double Value
        {
            get { return _value; }
            set { _value = value; _needRefreshValue = true; }
        }
        public Color MeterBackColor
        {
            get { return label_value.BackColor; }
            set { label_value.BackColor = value; }
        }
        public Color DigitalColor
        {
            get { return label_value.ForeColor; }
            set { label_value.ForeColor = value; }
        }
        public float DigitalFontSize
        {
            get { return label_value.Font.Size; }
            set 
            { 
                Font font = label_value.Font;
                label_value.Font = new Font(font.FontFamily, value);
            }
        }
        public int DecimalCount
        {
            get { return _decimal; }
            set { _decimal = value; }
        }
        public string Caption
        {
            get { return label_caption.Text; }
            set { label_caption.Text = value; }
        }
        public string Unit
        {
            get { return label_unit.Text; }
            set { label_unit.Text = value; }
        }
        Font ValueFont
        {
            get
            {
                Font font = label_value.Font;
                float size = Math.Min(this.Size.Height, this.Size.Width) * 0.6f;
                size = size < 9f ? 9f : size;
                return new Font(font.FontFamily, size);
            }
        }
        #endregion

        #region Variables
        bool _needRefreshValue = false;
        #endregion

        #region Methods
        void LoadDefault()
        {
            _value = 0;
            BackColor = System.Drawing.SystemColors.Control;
            DigitalColor =System.Drawing.SystemColors.ControlText;
            _decimal = 4;
            
            Caption = "";
            Unit = "";
        }
        public void Initialize(Color backColor, Color digitalColor, int decimalCount, string caption, string unit)
        {
            BackColor = backColor;
            DigitalColor = digitalColor;
            _decimal = decimalCount;
            Caption = caption;
            Unit = unit;
            _value = 0;
            
        }

        void RefreshValue()
        {
            label_value.Text = Math.Round(_value, _decimal).ToString("F" + _decimal.ToString()) + " " + Unit;
            int length = label_value.Text.Length;
            Font font = label_value.Font;
            float size = Math.Min(((float)label_value.Width / (float)length * 0.6f), label_value.Height * 0.6f);
            label_value.Font = new Font(font.FontFamily, size);

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshValue)
            {
                _needRefreshValue = false;
                RefreshValue();
            }

        }
        #endregion


        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Caption", label_caption.Text);
            keyValue.Add("Unit", label_unit.Text);
            keyValue.Add("FontSize", _fontSize.ToString());
            keyValue.Add("MeterBackColor", KeyValue.KeyValue.ColorToString(BackColor));
            keyValue.Add("DigitalColor", KeyValue.KeyValue.ColorToString(DigitalColor));
            keyValue.Add("Decimal", _decimal.ToString());
            keyValue.Add("Value", _value.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Caption = keyValue.GetValueByKey("Caption");
            Unit = keyValue.GetValueByKey("Unit");
            float.TryParse(keyValue.GetValueByKey("FontSize"), out _fontSize);
            BackColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("MeterBackColor"));
            DigitalColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("DigitalColor"));
            int.TryParse(keyValue.GetValueByKey("Decimal"), out _decimal);
            double.TryParse(keyValue.GetValueByKey("Value"), out _value);
            _needRefreshValue = true;
        }
        #endregion
    }
}
