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
    public partial class FormInputPosition : Form
    {
        public FormInputPosition(double x,double y)
        {
            InitializeComponent();
            this.textBox_x.Text = x.ToString();
            this.textBox_y.Text = y.ToString();
            textBox_x.TextChanged += new EventHandler(textBox_x_TextChanged);
            textBox_y.TextChanged += new EventHandler(textBox_y_TextChanged);
        }

        void textBox_y_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_y.Text, out value) && textBox_y.Text != "-")
            {
                textBox_y.Clear();
            }
        }

        void textBox_x_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_x.Text, out value) && textBox_x.Text != "-")
            {
                textBox_x.Clear();
            }
        }
        public double SelectX
        {
            get
            {
                double x;
                double.TryParse(textBox_x.Text, out x);
                return x;
            }
        }
        public double SelectY
        {
            get
            {
                double y;
                double.TryParse(textBox_y.Text, out y);
                return y;
            }
        }

        private void toolStripMenuItem_confirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void toolStripMenuItem_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
