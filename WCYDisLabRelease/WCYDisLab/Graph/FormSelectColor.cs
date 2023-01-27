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
    public partial class FormSelectColor : Form
    {
        public FormSelectColor()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += new KeyEventHandler(FormSelectColor_KeyDown);
        }
        Color _color = Color.Red;
        public Color SelectedColor
        {
            get { return _color; }
        }
        void FormSelectColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void button_red_Click(object sender, EventArgs e)
        {
            _color = Color.Red;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_green_Click(object sender, EventArgs e)
        {
            _color = Color.Green;
            DialogResult = DialogResult.OK;
            Close();

        }

        private void button_blue_Click(object sender, EventArgs e)
        {
            _color = Color.Blue;
            DialogResult = DialogResult.OK;
            Close();

        }
    }
}
