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
    public partial class FormIntervalEditor : Form
    {
        public FormIntervalEditor(byte shiftIndex,double intervalValue)
        {
            InitializeComponent();
            this.textBox_shiftIndex.TextChanged += new EventHandler(textBox_shiftIndex_TextChanged);
            this.textBox_interval.TextChanged += new EventHandler(textBox_interval_TextChanged);
            textBox_shiftIndex.Text = shiftIndex.ToString();
            textBox_interval.Text = (intervalValue * 10000).ToString();
            KeyDown += new KeyEventHandler(FormIntervalEditor_KeyDown);
            
        }

        void FormIntervalEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }


        void textBox_interval_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_interval.Text, out value) || value <= 0)
            {
                textBox_interval.Clear();
                textBox_frequency.Clear();
            }
            else
            {
                textBox_frequency.Text = (1 /( value * 0.0001)).ToString();
            }
        }

        void textBox_shiftIndex_TextChanged(object sender, EventArgs e)
        {
            byte value;
            if (!byte.TryParse(textBox_shiftIndex.Text, out value))
            {
                textBox_shiftIndex.Clear();
            }
        }

        public byte SelectedShiftIndex
        {
            get
            {
                byte value;
                byte.TryParse(textBox_shiftIndex.Text, out value);
                return value;
                    
            }
        }
        public double SelectedIntervalValue
        {
            get
            {
                double value;
                double.TryParse(textBox_interval.Text, out value);
                return value *0.0001;
            }
        }

      
    }
}
