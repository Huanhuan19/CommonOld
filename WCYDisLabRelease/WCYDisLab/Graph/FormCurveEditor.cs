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
    public partial class FormCurveEditor : Form
    {
        public FormCurveEditor(VirtualInstrument.Classes.GraphLineDefine graphLineDefine)
        {
            InitializeComponent();
            SelectedCaption = graphLineDefine.ColumnDefine.ColumnCaption;
            SelectedLineColor = graphLineDefine.LineColor;
            SelectedLineVisible = graphLineDefine.LineVisible;
            SelectedLineWidth = graphLineDefine.LineWidthF;
            SelectedPointVisible = graphLineDefine.PointVisible;
            SelectedIsY2Axis = graphLineDefine.IsY2Axis;
            SelectedIsSmooth = graphLineDefine.IsSmooth;
            SelectedSmoothTention = graphLineDefine.SmoothTention;
        }

        #region Props
        bool _isY2Axis;
        public string SelectedCaption
        {
            get { return textBox_caption.Text; }
            set { textBox_caption.Text = value; }
        }
        public bool SelectedLineVisible
        {
            get { return checkBox_lineVisible.Checked; }
            set { checkBox_lineVisible.Checked = value; }
        }
        public bool SelectedPointVisible
        {
            get { return checkBox_pointVisible.Checked; }
            set { checkBox_pointVisible.Checked = value; }
        }
        public Color SelectedLineColor
        {
            get { return button_color.ForeColor; }
            set { button_color.ForeColor = value; }
        }
        public float SelectedLineWidth
        {
            get { return (float)numericUpDown_width.Value; }
            set { numericUpDown_width.Value = (decimal)value; }
        }
        public bool SelectedIsY2Axis
        {
            get { return _isY2Axis; }
            set { _isY2Axis = value; }
        }
        public bool SelectedIsSmooth
        {
            get { return checkBox_isSmooth.Checked; }
            set { checkBox_isSmooth.Checked = value; }
        }
        public float SelectedSmoothTention
        {
            get
            {
                float value;
                float.TryParse(this.textBox_smoothTention.Text, out value);
                return value;
            }
            set { this.textBox_smoothTention.Text = value.ToString(); }
        }
        #endregion

        private void checkBox_isSmooth_CheckedChanged(object sender, EventArgs e)
        {
            textBox_smoothTention.Enabled = checkBox_isSmooth.Checked;
        }

        private void button_color_Click(object sender, EventArgs e)
        {
            FormSelectColor f = new FormSelectColor();
            if (f.ShowDialog() == DialogResult.OK)
            {
                SelectedLineColor = f.SelectedColor;
            }
        }
    }
}
