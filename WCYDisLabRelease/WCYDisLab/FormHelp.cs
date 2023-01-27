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
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
            Initialize();
        }
        public FormHelp(string url)
        {
            InitializeComponent();
            webBrowser1.Url = new Uri(url);
        }
        string _filename = "\\Help.htm";

        void Initialize()
        {
            if (System.IO.File.Exists(Application.StartupPath + _filename))
            {
                webBrowser1.Url = new Uri(Application.StartupPath + _filename);
            }
        }
    }
}
