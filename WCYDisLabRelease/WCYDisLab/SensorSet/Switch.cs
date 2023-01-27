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
    public partial class Switch : UserControl
    {
        public Switch()
        {
            InitializeComponent();
            ready = false;
            SwitchControl = 0x2f;
            voice = 0x02;
            light = 0x02;
            control = 0x02;
        }

        #region Props
        public static bool ready;
        public static byte SwitchControl;
        public static byte voice;
        public static byte light;
        public static byte control;
        #endregion

        private void button_voice_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(new Point(button_voice.Left, button_voice.Top + button_voice.Height)));
        }

        private void button_lignt_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(PointToScreen(new Point(button_light.Left, button_light.Top + button_light.Height)));
        }

        private void button_control_Click(object sender, EventArgs e)
        {
            contextMenuStrip3.Show(PointToScreen(new Point(button_control.Left, button_control.Top + button_control.Height)));
        }

        private void toolStripMenuItem_voiceOpen_Click(object sender, EventArgs e)
        {
            voice = 0x00;
            button_voice.Text = toolStripMenuItem_voiceOpen.Text;
        }

        private void toolStripMenuItem_voiceClose_Click(object sender, EventArgs e)
        {
            voice = 0x01;
            button_voice.Text = toolStripMenuItem_voiceClose.Text;
        }

        private void toolStripMenuItem_lightOpen_Click(object sender, EventArgs e)
        {
            light = 0x00;
            button_light.Text = toolStripMenuItem_lightOpen.Text;
        }

        private void toolStripMenuItem_lightClose_Click(object sender, EventArgs e)
        {
            light = 0x01;
            button_light.Text = toolStripMenuItem_lightClose.Text;
        }

        private void toolStripMenuItem_controlOpen_Click(object sender, EventArgs e)
        {
            control = 0x00;
            button_control.Text = toolStripMenuItem_controlOpen.Text;
        }

        private void toolStripMenuItem_controlClose_Click(object sender, EventArgs e)
        {
            control = 0x01;
            button_control.Text = toolStripMenuItem_controlClose.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (voice == 0x02)
                MessageBox.Show("请输入声音控制类型", "提示：");
            else if (light == 0x02)
                MessageBox.Show("请输入发光控制类型", "提示：");
            else if (control == 0x02)
                MessageBox.Show("请输入开闭控制类型", "提示：");
            else
            {
                MessageBox.Show("设置成功", "提示：");
                ready = true;
            }
        }        
    }
}
