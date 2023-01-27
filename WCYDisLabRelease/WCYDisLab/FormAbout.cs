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
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            this.label_version.Text = "7.152.07.82-6";//1.   14    6.1    11   1.6    1-5            
        }
    }
}
