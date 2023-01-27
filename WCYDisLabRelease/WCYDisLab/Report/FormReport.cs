using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.Report
{
    public partial class FormReport : Form
    {
        public FormReport()
        {
            InitializeComponent();
        }

        public FormReport( string value)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;

            Parse(value);
        }

        #region Methods
        private void toolStripButton_new_Click(object sender, EventArgs e)
        {
            report1.Clear();
        }

        private void toolStripButton_open_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                report1.LoadFile(openFileDialog1.FileName);
            }

        }

        private void toolStripButton_save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                report1.SaveFile(saveFileDialog1.FileName);
            }

        }

        private void toolStripButton_cut_Click(object sender, EventArgs e)
        {
            report1.Cut();
        }

        private void toolStripButton_copy_Click(object sender, EventArgs e)
        {
            report1.Copy();
        }

        private void toolStripButton_paste_Click(object sender, EventArgs e)
        {
            report1.Paste();
        }

        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", Name);
            keyValue.Add("Caption", Text);
            keyValue.Add("Bounds", Classes.PublicMethods.Bounds2Str(Bounds));
            keyValue.Add("Report", report1.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Name = keyValue.GetValueByKey("Name");
            Text = keyValue.GetValueByKey("Caption");
            Bounds = Classes.PublicMethods.RectangleParse(keyValue.GetValueByKey("Bounds"));
            report1.Parse(keyValue.GetValueByKey("Report"));
        }

        #endregion
    }
}
