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
    public partial class FormInputText : Form
    {
        public FormInputText(string text)
        {
            InitializeComponent();
            textBox1.Text = text;
        }
        public string SelectedText
        {
            get { return textBox1.Text; }
        }
        private void toolStripMenuItem_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void toolStripMenuItem_confirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
