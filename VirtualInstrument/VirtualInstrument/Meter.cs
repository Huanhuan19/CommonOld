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
    public partial class Meter : UserControl
    {
        public Meter()
        {
            InitializeComponent();
            LoadDefault();
            timer1.Start();
        }        
        #region Props
        
        bool _scaleAuto, _needRefresh = false;
        double _value =0;
        
        public double MaxValue
        {
            get { return aquaGauge1.MaxValue; }
            set { aquaGauge1.MaxValue = (float)value; }
        }
        public double MinValue
        {
            get { return aquaGauge1.MinValue; }
            set { aquaGauge1.MinValue = (float)value; }
        }
        public double Value
        {
            get { return aquaGauge1.Value; }
            set 
            {
                _value = value; _needRefresh = true;
            }
        }
        public bool ScaleAuto
        {
            get { return _scaleAuto; }
            set { _scaleAuto = value; }
        }
        public Color MeterBackColor
        {
            get { return aquaGauge1.BackColor; }
            set { aquaGauge1.BackColor = value; }
        }
        
        public string Caption
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public string Unit
        {
            get { return aquaGauge1.DialText; }
            set { aquaGauge1.DialText = value; }
        }
        
        #endregion

        #region Variables
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
            aquaGauge1.Value = (float)value;
            aquaGauge1.DialText = aquaGauge1.DialText;
            //if (_scaleAuto)
            //{
            //    if (aquaGauge1.Value >= aquaGauge1.MaxValue)
            //    {
            //        aquaGauge1.MaxValue = aquaGauge1.Value > 0 ? aquaGauge1.Value * 1.2f : aquaGauge1.Value * 0.8f;
            //    }
            //    if (aquaGauge1.Value <= aquaGauge1.MinValue)
            //    {
            //        aquaGauge1.MinValue = aquaGauge1.Value > 0 ? aquaGauge1.Value * 0.8f : aquaGauge1.Value * 1.2f;
            //    }
            //}

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
                SetValue(_value);
            }
        }
    }
}
