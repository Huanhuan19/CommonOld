using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.StartProps
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
                _startProps.ReservedSeconds = value;
            }
        }
        #region Props
        Classes.DataEngine _dataEngine;
        ExperimentProperties.StartProps _startProps;
        public ExperimentProperties.StartProps SelectedStartProps
        {
            get
            {
                double value;
                double.TryParse(this.textBox1.Text, out value);
                _startProps.ReservedSeconds = value;
                return _startProps;
            }
        }

        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine, ExperimentProperties.StartProps startProps)
        {
            _dataEngine = dataEngine;
            _startProps = startProps;
            FillList();
        }
        void FillList()
        {
            this.textBox1.Text = _startProps.ReservedSeconds.ToString();
        }
        #endregion



    }
}
