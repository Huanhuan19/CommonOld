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
    public partial class FormFontSizeSetup : Form
    {
        public FormFontSizeSetup(int fontSizeRate)
        {
            InitializeComponent();
            trackBar1.Value = (fontSizeRate >= trackBar1.Minimum && fontSizeRate <= trackBar1.Maximum) ? fontSizeRate : trackBar1.Minimum;
            FillList(SelectedFontSizeRate);

            trackBar1.ValueChanged += new EventHandler(trackBar1_ValueChanged);
        }


        #region Props
        public int SelectedFontSizeRate
        {
            get { return trackBar1.Value; }
        }
        #endregion

        #region Methods
        void FillList(int fontSizeRate)
        {
            Font font = label_sample.Font;
            label_sample.Font = new Font(font.FontFamily, Classes.PublicMethods.CalcFontSize(SelectedFontSizeRate), font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);

        }
        void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            FillList(SelectedFontSizeRate);
        }

        #endregion

    }
}
