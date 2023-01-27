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
    public partial class FormInputScaleRange : Form
    {
        public FormInputScaleRange(double min,double max,bool auto)
        {
            InitializeComponent();
            textBox_max.TextChanged += new EventHandler(textBox_max_TextChanged);
            textBox_min.TextChanged += new EventHandler(textBox_min_TextChanged);
            textBox_max.Text = max.ToString();
            textBox_min.Text = min.ToString();
            checkBox_autoScale.Checked = auto;

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

        public double SelectedMax
        {
            get
            {
                double value;
                double.TryParse(textBox_max.Text, out value);
                return value;
            }
        }
        public double SelectedMin
        {
            get
            {
                double value;
                double.TryParse(textBox_min.Text, out value);
                return value;
            }
        }
        public bool SelectedAutoScale
        {
            get { return checkBox_autoScale.Checked; }
        }
        private void toolStripMenuItem_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();

        }

        private void toolStripMenuItem_confirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
