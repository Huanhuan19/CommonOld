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
    public partial class Thermometer : UserControl
    {
        public Thermometer()
        {
            InitializeComponent();
            timer1.Start();
            LoadDefault();
        }

        #region Props
        double _maxValue, _minValue, _value;
        bool _scaleAuto;
        public double MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; _needRefreshAll = true; }
        }
        public double MinValue
        {
            get { return _minValue; }
            set { _minValue = value; _needRefreshAll = true; }
        }
        public double Value
        {
            get { return _value; }
            set { _value = value; _needRefreshValue = true; }
        }
        public bool ScaleAuto
        {
            get { return _scaleAuto; }
            set { _scaleAuto = value; _needRefreshAll = true; }
        }
        public string Caption
        {
            get { return label_Caption.Text; }
            set { label_Caption.Text = value; }
        }
        public string Unit1
        {
            get { return label_UnitName.Text; }
            set { label_UnitName.Text = value; }
        }
        public string Unit2
        {
            get { return label_UnitName2.Text; }
            set { label_UnitName2.Text = value; }
        }
        #endregion

        #region Variables
        bool _needRefreshAll = false, _needRefreshValue = false;
        #endregion

        #region Methods
        void LoadDefault()
        {
            _maxValue = 0;
            _minValue = 0;
            _value = 0;
            _scaleAuto = false;
            Caption = "";
            Unit1 = "℃";
            Unit2 = "℉";
        }
        public void Initialize(string caption, double maxValue, double minValue, bool scaleAuto)
        {
            Caption = caption;
            _maxValue = maxValue;
            _minValue = minValue;
            _scaleAuto = scaleAuto;
        }
        double C2F(double value)
        {
            return (value - 10) * 9 / 5 + 50;
        }
        double F2C(double value)
        {
            return (value - 50) * 5 / 9 + 10;
        }

        void DrawScale(Graphics g)
        {
            DrawScaleLeft();
            DrawScaleRight();
        }
        void DrawScaleLeft()
        {
            double interval = (MaxValue - MinValue) / 4;
            label_l0.Text = Math.Round((MinValue + interval * 0), 2).ToString();
            label_l1.Text = Math.Round((MinValue + interval * 1), 2).ToString();
            label_l2.Text = Math.Round((MinValue + interval * 2), 2).ToString();
            label_l3.Text = Math.Round((MinValue + interval * 3), 2).ToString();
            label_l4.Text = Math.Round((MinValue + interval * 4), 2).ToString();
        }
        void DrawScaleRight()
        {
            double min = C2F(MinValue), max = C2F(MaxValue), interval = (max - min) / 4;
            label_r0.Text = Math.Round((min + interval * 0), 2).ToString();
            label_r1.Text = Math.Round((min + interval * 1), 2).ToString();
            label_r2.Text = Math.Round((min + interval * 2), 2).ToString();
            label_r3.Text = Math.Round((min + interval * 3), 2).ToString();
            label_r4.Text = Math.Round((min + interval * 4), 2).ToString();
        }
        void RefreshAll(Graphics g)
        {
            DrawScale(g);
            RefreshValue(g);
        }
        void RefreshValue(Graphics g)
        {
            int value0y = this.label_l0.Location.Y + label_l0.Height / 2;
            int value4y = label_l4.Location.Y + label_l4.Height / 2;
            int lenth = value0y - value4y;
            int valuey = (int)Math.Round(((Value - MinValue) / (MaxValue - MinValue) * lenth), 0);
            int x = pictureBox_Needle.Location.X, width = pictureBox_Needle.Width, bottomy = value0y + 2;
            this.pictureBox_Needle.Location = new Point(x, bottomy - valuey - 2);
            this.pictureBox_Needle.Size = new Size(width, valuey + 2);

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            _needRefreshAll = true;
            base.OnPaint(e);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshAll)
            {
                _needRefreshAll = false;
                RefreshAll(this.CreateGraphics());
            }
            if (_needRefreshValue)
            {
                _needRefreshValue = false;
                RefreshValue(this.CreateGraphics());
            }

        }

        #endregion


        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("MaxValue", _maxValue.ToString());
            keyValue.Add("MinValue", _minValue.ToString());
            keyValue.Add("Caption", Caption);
            keyValue.Add("Unit1", Unit1);
            keyValue.Add("Unit2", Unit2);
            keyValue.Add("ScaleAuto", _scaleAuto.ToString());
            keyValue.Add("Value", _value.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            double.TryParse(keyValue.GetValueByKey("MaxValue"), out _maxValue);
            double.TryParse(keyValue.GetValueByKey("MinValue"), out _minValue);
            Caption = keyValue.GetValueByKey("Caption");
            Unit1 = keyValue.GetValueByKey("Unit1");
            Unit2 = keyValue.GetValueByKey("Unit2");
            bool.TryParse(keyValue.GetValueByKey("ScaleAuto"), out _scaleAuto);
            double.TryParse(keyValue.GetValueByKey("Value"), out _value);
            _needRefreshValue = true;

        }
        #endregion

    }
}
