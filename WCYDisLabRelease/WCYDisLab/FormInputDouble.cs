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
    public partial class FormInputDouble : Form
    {
        public FormInputDouble(string caption,double value)
        {
            InitializeComponent();
            this.textBox_value.Text = value.ToString();
            this.label_caption.Text = caption;
            this.textBox_value.TextChanged += new EventHandler(textBox_value_TextChanged);
        }
        public double SelectedValue
        {
            get
            {
                double value = 0;
                double.TryParse(textBox_value.Text, out value);
                return value;
            }
        }
        void textBox_value_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_value.Text, out value) && textBox_value.Text != "-")
            {
                textBox_value.Clear();
            }
        }

        private void toolStripMenuItem_confirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void toolStripMenuItem_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
