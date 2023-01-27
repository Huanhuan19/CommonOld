using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualInstrument.QuickView
{
    public partial class QuickText : UserControl
    {
        public QuickText()
        {
            InitializeComponent();
            timer1.Start();
        }

        #region Props
        bool _editMode = false;
        string _name;
        int _decimal = 4;
        double _value = 0;
        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                if (_editMode)
                {
                    textBox_caption.BackColor = System.Drawing.SystemColors.Window;
                    textBox_caption.BorderStyle = BorderStyle.Fixed3D;
                    textBox_caption.ReadOnly = false;

                    textBox_unit.BackColor = System.Drawing.SystemColors.Window;
                    textBox_unit.BorderStyle = BorderStyle.Fixed3D;
                    textBox_unit.ReadOnly = false;

                }
                else
                {
                    textBox_caption.BackColor = System.Drawing.SystemColors.Control;
                    textBox_caption.BorderStyle = BorderStyle.None;
                    textBox_caption.ReadOnly = true;

                    textBox_unit.BackColor = System.Drawing.SystemColors.Control;
                    textBox_unit.BorderStyle = BorderStyle.None;
                    textBox_unit.ReadOnly = true;

                }

            }
        }
        public int Decimal
        {
            get { return _decimal; }
            set 
            { 
                _decimal = value;
            //    _value = Math.Round(_value, _decimal); _needRefresh = true; 
            }
        }
        public string VariableName
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Caption
        {
            get { return textBox_caption.Text; }
            set { textBox_caption.Text = value; }
        }
        public string Unit
        {
            get { return textBox_unit.Text; }
            set { textBox_unit.Text = value; }
        }
        public double Value
        {
            get 
            {
                return _value;
            }
            set { _value = Math.Round(value,_decimal ); _needRefresh = true; }
        }
        #endregion

        #region Variables
        bool _needRefresh = false,_needClearValue = false;
        #endregion

        #region Methods
        public void Clear()
        {
            _needClearValue = true;
        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Caption", Caption);
            keyValue.Add("Name", VariableName);
            keyValue.Add("Unit", Unit);
            keyValue.Add("Value", Value.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Caption = keyValue.GetValueByKey("Caption");
            Unit = keyValue.GetValueByKey("Unit");
            VariableName = keyValue.GetValueByKey("Name");
            double variableValue;
            double.TryParse(keyValue.GetValueByKey("Value"), out variableValue);
            Value = variableValue;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefresh)
            {
                _needRefresh = false;
                textBox_value.Text = _value.ToString("F"+_decimal.ToString());

            }
            if (_needClearValue)
            {
                _needClearValue = false;
                textBox_value.Clear();
            }
        }


    }
}
