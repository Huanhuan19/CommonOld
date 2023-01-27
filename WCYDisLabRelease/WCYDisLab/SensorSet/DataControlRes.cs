using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.SensorSet
{
    public partial class DataControlRes : UserControl
    {
        public DataControlRes()
        {
            InitializeComponent();
            ready = false;
            datacontrol = 0x30;
            waitUse1 = 0x00;
            waitUse2 = 0x00;
            value = 0x00;
        }

        #region Props
        public static byte datacontrol;
        public static byte waitUse1;
        public static byte waitUse2;
        public static byte value;
        public static bool ready = false;
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                value = byte.Parse(comboBox1.Text);
                if (value <= 0 || value > 255)
                    MessageBox.Show("电阻阻值范围是（0-255）", "提示：");
                else
                {
                    MessageBox.Show("设置成功", "提示：");
                    ready = true;
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
