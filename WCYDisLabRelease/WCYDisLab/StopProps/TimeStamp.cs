using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.StopProps
{
    public partial class TimeStamp : UserControl
    {
        public TimeStamp()
        {
            InitializeComponent();
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox1.Text, out value) || value < 0)
            {
                textBox1.Clear();
            }
            else
            {
                _stopProps.ReservedSeconds = value;
            }
        }
        #region Props
        Classes.DataEngine _dataEngine;
        ExperimentProperties.EndProps _stopProps;
        public ExperimentProperties.EndProps SelectedEndProps
        {
            get
            {
                double value;
                double.TryParse(this.textBox1.Text, out value);
                _stopProps.ReservedSeconds = value;
                return _stopProps;
            }
        }

        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine, ExperimentProperties.EndProps stopProps)
        {
            _dataEngine = dataEngine;
            _stopProps = stopProps;
            FillList();
        }
        void FillList()
        {
            this.textBox1.Text = _stopProps.ReservedSeconds.ToString();
        }
        #endregion



    }
}
