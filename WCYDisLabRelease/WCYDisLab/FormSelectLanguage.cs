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

namespace WCYDisLab
{
    public partial class FormSelectLanguage : Form
    {
        public FormSelectLanguage()
        {
            InitializeComponent();
            Initialize();
            timer1.Start();
        }
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.FormSelectLanguage", Assembly.GetExecutingAssembly());
        string _languageName = "zh-CN";
        public string SelectedLanguageName
        {
            get
            {
                return _languageName;
            }
        }
        DateTime _startTime = DateTime.Now;
        void Initialize()
        {
            label_version.Text = "V" + ProductVersion;
            _startTime = DateTime.Now;
            string languageName = string.Empty;
            if (KeyValue.FileStream.Load(Application.StartupPath+ "\\Culture.set", ref languageName))
            {
                if (!string.IsNullOrEmpty(languageName))
                {
                    _languageName = languageName;
                    SwitchLanguage(languageName);
                }
            }
            comboBox2.Text = "6P6C";

        }
        void SwitchLanguage(string languageName)
        {
            if (string.Equals(languageName, "zh-CN"))
            {
                pictureBox1.Image = Properties.Resources.zh_CN;
                comboBox1.Text = "简体中文";
            }
            else if (string.Equals(languageName, "en-GB"))
            {
                pictureBox1.Image = Properties.Resources.en_GB;
                comboBox1.Text = "English";
            }
            else if (string.Equals(languageName, "ru-RU"))
            {
                pictureBox1.Image = Properties.Resources.ru_RU;
                comboBox1.Text = "Russ";
            }
            else if (string.Equals(languageName, "mn"))
            {
                pictureBox1.Image = Properties.Resources.mn;
                comboBox1.Text = "Mongolian";
            }
            else if (string.Equals(languageName, "es-ES"))
            {
                pictureBox1.Image = Properties.Resources.mn;
                comboBox1.Text = "Spana";
            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(_languageName);

            label_title.Text = _resourceManager.GetString("label_title.Text");
            label_copyRight.Text = _resourceManager.GetString("label_copyRight.Text");
            label_copyRightDate.Text = _resourceManager.GetString("label_copyRightDate.Text");
            label_factoryName.Text = _resourceManager.GetString("label_factoryName.Text");
            lb_Language.Text = _resourceManager.GetString("lb_Language.Text");
            button_in.Text = _resourceManager.GetString("button_in.Text");
            button_out.Text = _resourceManager.GetString("button_out.Text");
            buttonUSB_in.Text = _resourceManager.GetString("buttonUSB_in.Text");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //TimeSpan span = DateTime.Now - _startTime;
            //if (span.TotalSeconds >= 10)
            //{
            //    DialogResult = DialogResult.OK;
            //    Close();
            //}
            //else
            //{
            //    progressBar1.Value = (int)Math.Round(span.TotalSeconds * 10, 0);
            //}
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //if (string.Equals(_languageName, "zh-CN"))
            //{
            //    _languageName = "en-GB";
            //}
            //else if (string.Equals(_languageName, "en-GB"))
            //{
            //    _languageName = "zh-CN";
            //}
            //SwitchLanguage(_languageName);
            //KeyValue.FileStream.Save(Application.StartupPath + "\\Culture.set", _languageName);

        }

        private void button_skip_Click(object sender, EventArgs e)//由之前的6p6c(不再用了) 改成蓝牙
        {
            _isBlueToothUsed = true;
            _is6p6c = false;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex  == 0)
            {
                _languageName = "zh-CN";
            }
            if (comboBox1.SelectedIndex == 1)
            {
                _languageName = "en-GB";
            }
            if (comboBox1.SelectedIndex == 2)
            {
                _languageName = "ru-RU";
            }
            if (comboBox1.SelectedIndex == 3)
            {
                _languageName = "mn";
            }
            if (comboBox1.SelectedIndex == 4)
            {
                _languageName = "es-ES";
            }
            SwitchLanguage(_languageName);
            KeyValue.FileStream.Save(Application.StartupPath + "\\Culture.set", _languageName);
        }

        private void button_out_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static bool _is6p6c = false;
        public static bool _isBlueToothUsed = false;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex  == 0)
                _is6p6c = true;
            else
            {
                _is6p6c = false;
            }
        }

        private void buttonUSB_in_Click(object sender, EventArgs e)
        {
            _is6p6c = false;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
