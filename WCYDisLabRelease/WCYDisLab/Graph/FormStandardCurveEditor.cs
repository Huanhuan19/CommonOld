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
    public partial class FormStandardCurveEditor : Form
    {
        public FormStandardCurveEditor(string caption,string methodName,double a,double b,double c,double d)
        {
            InitializeComponent();
            this.listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            textBox_caption.Text = caption;
            _methodName = methodName;
            textBox1.Text = a.ToString();
            textBox2.Text = b.ToString();
            textBox3.Text = c.ToString();
            textBox4.Text = d.ToString();
            FillList();
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
            textBox2.TextChanged += new EventHandler(textBox2_TextChanged);
            textBox3.TextChanged += new EventHandler(textBox3_TextChanged);
            textBox4.TextChanged += new EventHandler(textBox4_TextChanged);
        }

        void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                _methodName = listView1.SelectedItems[0].Text;
                button_confirm.Enabled = true;
            }
            else
            {
                _methodName = "";
                button_confirm.Enabled = false;
            }
        }

        void textBox4_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox4.Text, out value) && textBox4.Text != "-")
            {
                textBox4.Clear();
            }
        }

        void textBox3_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox3.Text, out value) && textBox3.Text != "-")
            {
                textBox3.Clear();
            }
        }

        void textBox2_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox2.Text, out value) && textBox2.Text != "-")
            {
                textBox2.Clear();
            }

        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox1.Text, out value) && textBox1.Text != "-")
            {
                textBox1.Clear();
            }

        }
        public string SelectedCaption
        {
            get { return textBox_caption.Text; }
        }
        string _methodName;
        public string SelectedMethodName
        {
            get { return _methodName; }
        }
        public double SelectedA
        {
            get
            {
                double value;
                double.TryParse(textBox1.Text, out value);
                return value;
            }
        }
        public double SelectedB
        {
            get
            {
                double value;
                double.TryParse(textBox2.Text, out value);
                return value;
            }
        
        }
        public double SelectedC
        {
            get
            {
                double value;
                double.TryParse(textBox3.Text, out value);
                return value;
            }
        }
        public double SelectedD
        {
            get
            {
                double value;
                double.TryParse(textBox4.Text, out value);
                return value;
            }
        }

        void FillList()
        {
            DataAnalysis.Fitting fitting = new DataAnalysis.Fitting();
            listView1.Items.Clear();
            for (int i = 0; i < fitting.FittingMethods.Count; i++)
            {
                ListViewItem item = new ListViewItem(fitting.FittingMethods[i].Name);
                item.SubItems.Add(fitting.FittingMethods[i].Expression);
                listView1.Items.Add(item);
                if (string.Equals(_methodName, fitting.FittingMethods[i].Name))
                {
                    item.Selected = true;
                }
            }
        }
    }
}
