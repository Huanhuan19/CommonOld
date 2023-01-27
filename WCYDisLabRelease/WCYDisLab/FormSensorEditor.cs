using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab
{
    public partial class FormSensorEditor : Form
    {
        public FormSensorEditor(SensorManager.SensorDefine sensorDefine)
        {
            InitializeComponent();
            _sensorDefine = new SensorManager.SensorDefine(sensorDefine.ToString());
            textBox_max.TextChanged += new EventHandler(textBox_max_TextChanged);
            textBox_min.TextChanged += new EventHandler(textBox_min_TextChanged);
            textBox_sensorID.TextChanged += new EventHandler(textBox_sensorID_TextChanged);
            textBox_b.TextChanged += new EventHandler(textBox_b_TextChanged);
            textBox_k.TextChanged += new EventHandler(textBox_k_TextChanged);
            FillList();
        }
        
        void textBox_k_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_k.Text, out value)&&textBox_k.Text != "-")
            {
                textBox_k.Clear();
            }
        }

        void textBox_b_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_b.Text, out value)&& textBox_b.Text != "-")
            {
                textBox_b.Clear();
            }
        }

        void textBox_sensorID_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(textBox_sensorID.Text, out value))
            {
                textBox_sensorID.Clear();
            }
        }

        void textBox_min_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_min.Text, out value) && textBox_min.Text != "-")
            {
                textBox_min.Clear();
            }
        }

        void textBox_max_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_max.Text, out value) && textBox_max.Text != "-")
            {
                textBox_max.Clear();
            }
        }
        #region Props
        SensorManager.SensorDefine _sensorDefine;
        public SensorManager.SensorDefine SelectedSensorDefine
        {
            get
            {

                double max, min,k,b;
                double.TryParse(textBox_max.Text, out max);
                double.TryParse(textBox_min.Text, out min);
                double.TryParse(textBox_k.Text, out k);
                double.TryParse(textBox_b.Text, out b);
                int sensorID;
                int.TryParse(this.textBox_sensorID.Text, out sensorID);
                _sensorDefine.Caption = textBox_caption.Text;
                _sensorDefine.Decimal = (int)numericUpDown_decimal.Value;
                _sensorDefine.MaxValue = max;
                _sensorDefine.MinValue = min;
                _sensorDefine.SensorID = sensorID;
                _sensorDefine.Unit = textBox_unit.Text;
                _sensorDefine.K = k;
                _sensorDefine.B = b;
                return _sensorDefine;
            }
        }
        
        #endregion
        void FillList()
        {
            this.textBox_caption.Text = _sensorDefine.Caption;
            this.textBox_max.Text = _sensorDefine.MaxValue.ToString();
            this.textBox_min.Text = _sensorDefine.MinValue.ToString();
            this.textBox_sensorID.Text = _sensorDefine.SensorID.ToString();
            this.textBox_unit.Text = _sensorDefine.Unit;
            this.numericUpDown_decimal.Value = _sensorDefine.Decimal;
            this.textBox_k.Text = _sensorDefine.K.ToString();
            this.textBox_b.Text = _sensorDefine.B.ToString();
            string typeName = toolStripMenuItem_narmal.Text;
            switch (_sensorDefine.TypeID)
            {
                default:
                case 0://Nomal
                    typeName = toolStripMenuItem_narmal.Text;
                    break;
                case 1://PhotoGate
                    typeName = toolStripMenuItem_photoGate.Text;
                    break;
                case 2://Heart
                    typeName =toolStripMenuItem_heartRate.Text;
                    break;
            }

            this.button_typeID.Text = typeName;
        }
        private void button_typeID_Click(object sender, EventArgs e)
        {
            contextMenuStrip_type.Show(PointToScreen(new Point(button_typeID.Left, button_typeID.Top + button_typeID.Height)));
        }

        private void toolStripMenuItem_narmal_Click(object sender, EventArgs e)
        {
            _sensorDefine.TypeID = 0;
            this.button_typeID.Text = toolStripMenuItem_narmal.Text;

        }

        private void toolStripMenuItem_photoGate_Click(object sender, EventArgs e)
        {
            _sensorDefine.TypeID = 1;
            this.button_typeID.Text = toolStripMenuItem_photoGate.Text;

        }

        private void toolStripMenuItem_heartRate_Click(object sender, EventArgs e)
        {
            _sensorDefine.TypeID = 2;
            this.button_typeID.Text = toolStripMenuItem_heartRate.Text;

        }
    }
}
