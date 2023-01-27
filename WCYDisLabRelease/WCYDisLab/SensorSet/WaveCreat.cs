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
    public partial class WaveCreat : UserControl
    {
        public WaveCreat()
        {
            InitializeComponent();
            ready = false;
            WaveType = 0x03;
            Wave = 0x2d;
            value = 0x00;
            time = 0x00;
            value2 = 0.0;
            fre = 0;
        }

        #region Props
        public static bool ready;
        public static byte WaveType;
        public static byte Wave;
        public static byte value, time;
        public double value2;
        public int fre;
        #endregion

        private void toolStripMenuItem_square_Click(object sender, EventArgs e)
        {
            this.button_waveType.Text = toolStripMenuItem_square.Text;
            WaveType = 0x00;
        }

        private void toolStripMenuItem_sin_Click(object sender, EventArgs e)
        {
            this.button_waveType.Text = toolStripMenuItem_sin.Text;
            WaveType = 0x01;
        }

        private void toolStripMenuItem_triangle_Click(object sender, EventArgs e)
        {
            this.button_waveType.Text = toolStripMenuItem_triangle.Text;
            WaveType = 0x02;
        }

        private void button_waveType_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(new Point(button_waveType.Left, button_waveType.Top + button_waveType.Height)));
        }

        private void button_IsOk_Click(object sender, EventArgs e)
        {
            try
            {
                value2 = double.Parse(textBox_value .Text );
                fre = int.Parse(textBox_fre .Text);
                if (WaveType == 0x03)
                    MessageBox.Show("请输入波形发生器的类型", "提示");

                else if (value2 <= 0 || value2 > 5)
                {

                    MessageBox.Show("请输入范围为（0-5）V的波形幅值", "出错啦");
                }
                else if (fre < 0 || fre > 255)
                    MessageBox.Show("请输入范围为（0-255）ms的波形周期", "出错啦");
                else
                {
                    MessageBox.Show("设置成功", "提示：");
                    ready = true;
                    value = Convert.ToByte(value2 * 51);
                    time = Convert.ToByte(fre);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
