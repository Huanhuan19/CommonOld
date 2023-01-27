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
    public partial class FormNetProps : Form
    {
        public FormNetProps(string value,bool netOpen,int thisPort)
        {
            InitializeComponent();
            DisLabComm.Address address = new DisLabComm.Address(value);
            this.textBox_port.Text = address.Port.ToString();
            this.textBox1.Text = address.IP[0].ToString();
            this.textBox2.Text = address.IP[1].ToString();
            this.textBox3.Text = address.IP[2].ToString();
            this.textBox4.Text = address.IP[3].ToString();
            checkBox_acceptConnect.Checked = netOpen;
            this.textBox_thisPort.Text = thisPort.ToString();
            this.textBox_port.TextChanged += new EventHandler(textBox_port_TextChanged);
            this.textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
            this.textBox2.TextChanged += new EventHandler(textBox2_TextChanged);
            this.textBox3.TextChanged += new EventHandler(textBox3_TextChanged);
            this.textBox4.TextChanged += new EventHandler(textBox4_TextChanged);
        }

        public DisLabComm.Address SelectedAddress
        {
            get
            {
                byte seg1, seg2, seg3, seg4;
                int port;
                byte.TryParse(textBox4.Text, out seg4);
                byte.TryParse(textBox3.Text, out seg3);
                byte.TryParse(textBox2.Text, out seg2);
                byte.TryParse(textBox1.Text, out seg1);
                int.TryParse(textBox_port.Text, out port);
                return new DisLabComm.Address(seg1, seg2, seg3, seg4, port);
            }
        }
        public int SelectedThisPort
        {
            get
            {
                int value;
                int.TryParse(this.textBox_thisPort.Text, out value);
                return value;
            }
        }
        public bool SelectedNetOpen
        {
            get { return checkBox_acceptConnect.Checked; }
        }
        void textBox4_TextChanged(object sender, EventArgs e)
        {
            byte value;
            if (!byte.TryParse(((TextBox)sender).Text, out value))
            {
                ((TextBox)sender).Clear();
            }
        }

        void textBox3_TextChanged(object sender, EventArgs e)
        {
            byte value;
            if (!byte.TryParse(((TextBox)sender).Text, out value))
            {
                ((TextBox)sender).Clear();
            }
        }

        void textBox2_TextChanged(object sender, EventArgs e)
        {
            byte value;
            if (!byte.TryParse(((TextBox)sender).Text, out value))
            {
                ((TextBox)sender).Clear();
            }
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            byte value;
            if (!byte.TryParse(((TextBox)sender).Text, out value))
            {
                ((TextBox)sender).Clear();
            }
        }

        void textBox_port_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(((TextBox)sender).Text, out value))
            {
                ((TextBox)sender).Clear();
            }
        }

        private void textBox_thisPort_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(((TextBox)sender).Text, out value))
            {
                ((TextBox)sender).Clear();
            }

        }

    }
}
