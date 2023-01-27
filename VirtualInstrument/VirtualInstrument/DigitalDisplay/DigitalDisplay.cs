using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualInstrument.DigitalDisplay
{
    public partial class DigitalDisplay : UserControl
    {
        public DigitalDisplay()
        {
            InitializeComponent();
            LoadDefault();
            timer1.Start();
        }
        #region Props
        double _currentValue, _currentValuePrev1, _currentValuePrev2, _recordValue, _recordValuePrev1, _recordValuePrev2;
        bool _needRefreshRecordValue, _needRefreshCurrentValue;
        int _decimal;
        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValuePrev2 = _currentValuePrev1;
                _currentValuePrev1 = _currentValue;
                _currentValue = value;
                _needRefreshCurrentValue = true;
            }
        }
        public double RecordValue
        {
            get { return _recordValue; }
            set
            {
                _recordValuePrev2 = _recordValuePrev1;
                _recordValuePrev1 = _recordValue;
                _recordValue = value;
                _needRefreshRecordValue = true;
            }
        }
        public Color DisplayBackColor
        {
            get { return BackColor; }
            set { BackColor = value; }
        }
        public Color DigitalColor
        {
            get { return label_recordValue.ForeColor; }
            set { label_recordValue.ForeColor = value; }
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
        public int DecimalCount
        {
            get { return _decimal; }
            set { _decimal = value; }
        }
        string DecimalString
        {
            get
            {
                return "F"+DecimalCount.ToString();
            }
        }
        #endregion
        
        #region Methods
        void LoadDefault()
        {
            Clear();
            DisplayBackColor = System.Drawing.SystemColors.Control;
            DigitalColor = System.Drawing.SystemColors.ControlText;
            _decimal = 4;

            Caption = "";
            Unit = "";

        }
        public void Clear()
        {
            _recordValue = 0;
            _recordValuePrev1 = 0;
            _recordValuePrev2 = 0;
            _currentValue = 0;
            _currentValuePrev1 = 0;
            _currentValuePrev2 = 0;
            _needRefreshRecordValue = true;
            _needRefreshCurrentValue = true;
        }

        void RefreshRecordValue()
        {
            label_recordValuePrev1.Text = this._recordValuePrev1.ToString(DecimalString);
            label_recordValuePrev2.Text = this._recordValuePrev2.ToString(DecimalString);
            label_recordValue.Text = Math.Round(this._recordValue, DecimalCount).ToString(DecimalString);
            
        }
        void RefreshCurrentValue()
        {
            label_currentValuePrev1.Text = _currentValuePrev1.ToString(DecimalString);
            label_currentValuePrev2.Text = _currentValuePrev2.ToString(DecimalString);
            label_currentValue.Text = Math.Round(_currentValue, DecimalCount).ToString(DecimalString);
            
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshCurrentValue)
            {
                _needRefreshCurrentValue = false;
                RefreshCurrentValue();
            }
            if (_needRefreshRecordValue)
            {
                _needRefreshRecordValue = false;
                RefreshRecordValue();
            }
        }
        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Caption", label_caption.Text);
            keyValue.Add("Unit", label_unit.Text);
            keyValue.Add("DisplayBackColor", KeyValue.KeyValue.ColorToString(DisplayBackColor));
            keyValue.Add("DigitalColor", KeyValue.KeyValue.ColorToString(DigitalColor));
            keyValue.Add("Decimal", _decimal.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Caption = keyValue.GetValueByKey("Caption");
            Unit = keyValue.GetValueByKey("Unit");
            DisplayBackColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("DisplayBackColor"));
            DigitalColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("DigitalColor"));
            int.TryParse(keyValue.GetValueByKey("Decimal"), out _decimal);
        }
        #endregion

    }
}
