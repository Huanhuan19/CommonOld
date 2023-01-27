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
    public partial class FormGraphProps : Form
    {
        public FormGraphProps(Classes.DataEngine dataEngine,List<VirtualInstrument.Classes.GraphLineDefine> curves,VirtualInstrument.Classes.GraphAxisDefine xAxisDefine,VirtualInstrument.Classes.GraphAxisDefine yAxisDefine,VirtualInstrument.Classes.GraphAxisDefine y2AxisDefine,string caption)
        {
            InitializeComponent();
            curvePick1.Initialize(dataEngine, curves);
            xAxisProps1.Initialize(dataEngine, xAxisDefine);
            yAxisProps1.Initialize(dataEngine, yAxisDefine);
            yAxisProps2.Initialize(dataEngine, y2AxisDefine);
            textBox_caption.Text = caption;
        }

        public VirtualInstrument.Classes.GraphAxisDefine SelectedXAxisDefine
        {
            get { return xAxisProps1.SelectedXAxisProps; }
        }
        public VirtualInstrument.Classes.GraphAxisDefine SelectedYAxisDefine
        {
            get { return yAxisProps1.SelectedYAxisProps; }
        }
        public VirtualInstrument.Classes.GraphAxisDefine SelectedY2AxisDefine
        {
            get { return yAxisProps2.SelectedYAxisProps; }
        }
        public List<VirtualInstrument.Classes.GraphLineDefine> SelectedLines
        {
            get { return curvePick1.SelectedLines; }
        }
        public string SelectedCapion
        {
            get { return textBox_caption.Text; }
        }
    }
}
