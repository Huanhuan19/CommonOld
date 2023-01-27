using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.Graph
{
    public partial class YAxisProps : UserControl
    {
        public YAxisProps()
        {
            InitializeComponent();
            textBox_max.TextChanged += new EventHandler(textBox_max_TextChanged);
            textBox_min.TextChanged += new EventHandler(textBox_min_TextChanged);

        }
        void textBox_min_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_min.Text, out value) && textBox_min.Text != "-")
            {
                textBox_min.Clear();
            }
            else
            {
                _axisDefine.Minimum = value;
            }
        }

        void textBox_max_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_max.Text, out value) && textBox_max.Text != "-")
            {
                textBox_max.Clear();
            }
            else
            {
                _axisDefine.Maximum = value;
            }
        }
        #region Props
        Classes.DataEngine _dataEngine;
        VirtualInstrument.Classes.GraphAxisDefine _axisDefine;
        public VirtualInstrument.Classes.GraphAxisDefine SelectedYAxisProps
        {
            get
            {
                _axisDefine.Visible = checkBox_visible.Checked;
                _axisDefine.AutoScale = checkBox_auto.Checked;
                _axisDefine.Caption = textBox_caption.Text;
                _axisDefine.Unit = textBox_unit.Text;

                return _axisDefine;
            }
        }
        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine, VirtualInstrument.Classes.GraphAxisDefine axisDefine)
        {
            _dataEngine = dataEngine;
            _axisDefine = new VirtualInstrument.Classes.GraphAxisDefine(axisDefine.ToString());
            FillList();
        }
        void FillList()
        {
            checkBox_visible.Checked = _axisDefine.Visible;
            this.textBox_caption.Text = _axisDefine.Caption;
            this.textBox_max.Text = _axisDefine.Maximum.ToString();
            this.textBox_min.Text = _axisDefine.Minimum.ToString();
            this.textBox_unit.Text = _axisDefine.Unit;
            this.checkBox_auto.Checked = _axisDefine.AutoScale;
        }
        #endregion

        private void checkBox_auto_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Enabled = checkBox_auto.Checked;
        }

        private void checkBox_visible_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = checkBox_visible.Checked;
        }

    }
}
