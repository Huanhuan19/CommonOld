using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.Statistic
{
    public partial class FormLineNameSet : Form
    {
        public FormLineNameSet(string graphName)
        {
            InitializeComponent();
            _graphName = graphName;
            textBox1.Text = _graphName;
        }
        string _graphName;
        public string GraphName
        {
            get { return textBox1 .Text; }
        }
    }
}
