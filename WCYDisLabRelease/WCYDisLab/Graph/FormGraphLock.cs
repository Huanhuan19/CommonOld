using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.Graph
{
    public partial class FormGraphLock : Form
    {
        public FormGraphLock(bool yLock,bool y2Lock,bool xLock)
        {
            InitializeComponent();
            checkBox_x.Checked = xLock;
            checkBox_y.Checked = yLock;
            checkBox_y2.Checked = y2Lock;
        }
        public bool SelectedXLock
        {
            get { return checkBox_x.Checked; }
        }
        public bool SelectedYLock
        {
            get { return checkBox_y.Checked; }
        }
        public bool SelectedY2Lock
        {
            get { return checkBox_y2.Checked; }
        }
    }
}
