using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;

namespace WCYDisLab.Guid
{
    public partial class FormGuid : Form
    {
        public FormGuid()
        {
            InitializeComponent();
        }
        public FormGuid(string value)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            Parse(value);
        }
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.Guid.FormGuid", Assembly.GetExecutingAssembly());
        private void toolStripButton_go_Click(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Url = new Uri(this.toolStripTextBox_url.Text);
            }
            catch
            {
                MessageBox.Show(_resourceManager.GetString("FileErrorMSG"));
            }
        }
        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", Name);
            keyValue.Add("Caption", Text);
            keyValue.Add("Bounds", Classes.PublicMethods.Bounds2Str(Bounds));
            keyValue.Add("Filename", (this.webBrowser1.Url == null) ? "" : this.webBrowser1.Url.AbsoluteUri);
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Name = keyValue.GetValueByKey("Name");
            Text = keyValue.GetValueByKey("Caption");
            Bounds = Classes.PublicMethods.RectangleParse(keyValue.GetValueByKey("Bounds"));
            try
            {
                webBrowser1.Url = new Uri(keyValue.GetValueByKey("Filename"));
            }
            catch
            {
            }
        }

        #endregion

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                this.openFileDialog1.Filter = "实验指导文件*.txt;*.html;*.htm;*.xml|*.txt;*.html;*.htm;*.xml|office文件*.doc;*.docx*.ppt*.xls|*.doc;*.docx*.ppt*.xls|所有文件|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string MyFileName = openFileDialog1.FileName;
                    webBrowser1.Navigate(MyFileName);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

    }
}
