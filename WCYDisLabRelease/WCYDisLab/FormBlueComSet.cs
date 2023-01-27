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
    public partial class FormBlueComSet : Form
    {
        public string Com;
        public int baudRate;
        string fileName;
        string baudRateName;
        public FormBlueComSet()
        {
            InitializeComponent();

            fileName =Application .StartupPath + "\\SerialPortSet.txt";
            System.IO.StreamReader reader = System.IO.File.OpenText(fileName);
            Com = reader.ReadLine();
            baudRate = Convert.ToInt32(reader.ReadLine());
            comboBox1.Text = Com;
            //comboBox2.Text = baudRate.ToString ();
            if (baudRate == 115200)
                baudRateName = "无线";
            else
                baudRateName = "有线";
            comboBox2.Text = baudRateName;
            reader.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Com = comboBox1.Text;
            }
            catch
            { }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                baudRateName = comboBox2.Text;
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (baudRateName == "无线")
                baudRate = 115200;
            else
                baudRate = 1382400;

            try
            {
                System.IO.StreamWriter writer = System.IO.File.CreateText(fileName);
                if (writer != null)
                {
                    writer.WriteLine(Com);
                    writer.WriteLine(baudRate);
                    writer.Close();

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
