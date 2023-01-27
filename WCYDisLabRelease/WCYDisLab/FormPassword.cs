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
    public partial class FormPassword : Form
    {
        public FormPassword()
        {
            InitializeComponent();
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.FormPassword", Assembly.GetExecutingAssembly());
        string _defaultPassword = "3.14159";
        string _setedPassword = "";
        string _passwordFileName = "\\Sec.xml";
        string InputedPasssword
        {
            get { return textBox_password.Text; }
        }
        bool NewPasswordMatched
        {
            get { return string.Equals(textBox_new1.Text, textBox_new2.Text); }
        }
        string NewPassword
        {
            get { return textBox_new2.Text; }
        }
        #endregion

        #region Methods
        bool LoadFile()
        {
            string filename = Application.StartupPath + _passwordFileName;
            bool success = false;
            try
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(filename);
                _setedPassword = reader.ReadToEnd();
                reader.Close();
                //RefreshDataSourceSensorTypeClassList();
                success = true;
            }
            catch
            {
                _setedPassword = "";
                success = false;
            }
            return success;

        }
        bool SaveFile()
        {
            string filename = Application.StartupPath + _passwordFileName;
            bool success = false;
            try
            {
                System.IO.StreamWriter writer = System.IO.File.CreateText(filename);
                if (writer != null)
                {
                    writer.Write(NewPassword);
                    writer.Close();
                    //RefreshDataSourceSensorTypeClassList();
                    success = true;

                }
            }
            catch
            {
                success = false;

            }
            return success;

        }
        bool CheckPassword()
        {
            bool matched = false;
            if (LoadFile())
            {
                matched = string.Equals(InputedPasssword, _setedPassword);
            }
            else
            {
                matched = string.Equals(InputedPasssword, _defaultPassword);
            }
            return matched;
        }
        #endregion

        private void button_check_Click(object sender, EventArgs e)
        {
            panel2.Visible = CheckPassword();
        }
        private void button_go_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_changePassword_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty( textBox_new1.Text ) && NewPasswordMatched)
            {
                if (SaveFile())
                {
                    MessageBox.Show(_resourceManager.GetString("PasswordSavedMSG"));
                }

            }
            else
            {
                MessageBox.Show(_resourceManager.GetString("NewPasswordErrorMSG"));
            }
        }
    }
}
