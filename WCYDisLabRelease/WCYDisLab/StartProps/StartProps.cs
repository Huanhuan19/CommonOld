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
    public partial class StartProps : UserControl
    {
        public StartProps()
        {
            InitializeComponent();
        }
        #region Props
        Classes.DataEngine _dataEngine;
        ExperimentProperties.StartProps _startProps;
        public ExperimentProperties.StartProps SelectedStartProps
        {
            get 
            {
                return _startProps; 
            }
        }
        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine, ExperimentProperties.StartProps startProps)
        {
            _dataEngine = dataEngine;
            _startProps = new ExperimentProperties.StartProps(startProps.ToString());
            FillList();
        }
        void FillList()
        {
            switch (_startProps.StartMode)
            {
                case ExperimentProperties.StartMode.Manual:
                    Manual();
                    break;
                case ExperimentProperties.StartMode.ValueIncrease:
                    ValueIncrease();
                    break;
                case ExperimentProperties.StartMode.ValueDecrease:
                    ValueDecrease();
                    break;
                case ExperimentProperties.StartMode.IndexCount:
                    IndexCount();
                    break;
                case ExperimentProperties.StartMode.TimeStamp:
                    TimeStamp();
                    break;
                case ExperimentProperties.StartMode.Remote:
                    break;
            }
        }
        void Manual()
        {
            this.button_mode.Text =toolStripMenuItem_manualStart.Text;
            _startProps.StartMode = ExperimentProperties.StartMode.Manual;
            panel1.Controls.Clear();
        }
        void ValueIncrease()
        {
            this.button_mode.Text = toolStripMenuItem_valueIncreaseStart.Text;
            _startProps.StartMode = ExperimentProperties.StartMode.ValueIncrease;
            panel1.Controls.Clear();
            ValueSet valueSet = new ValueSet();
            valueSet.Initialize(_dataEngine, _startProps);
            valueSet.Dock = DockStyle.Fill;
            panel1.Controls.Add(valueSet);
            
        }
        void ValueDecrease()
        {
            this.button_mode.Text = toolStripMenuItem_valueDecreaseStart.Text;
            _startProps.StartMode = ExperimentProperties.StartMode.ValueDecrease;
            panel1.Controls.Clear();
            ValueSet valueSet = new ValueSet();
            valueSet.Initialize(_dataEngine, _startProps);
            valueSet.Dock = DockStyle.Fill;
            panel1.Controls.Add(valueSet);
        }
        void IndexCount()
        {
            this.button_mode.Text = toolStripMenuItem_timeIndex.Text;
            _startProps.StartMode = ExperimentProperties.StartMode.IndexCount;
            panel1.Controls.Clear();
            IndexCount indexCount = new IndexCount();
            indexCount.Initialize(_dataEngine, _startProps);
            indexCount.Dock = DockStyle.Fill;
            panel1.Controls.Add(indexCount);
        }
        void TimeStamp()
        {
            this.button_mode.Text = toolStripMenuItem_timeStamp.Text;
            _startProps.StartMode = ExperimentProperties.StartMode.TimeStamp;

            panel1.Controls.Clear();
            TimeStamp timeStamp = new TimeStamp();
            timeStamp.Initialize(_dataEngine, _startProps);
            timeStamp.Dock = DockStyle.Fill;
            panel1.Controls.Add(timeStamp);
        }

        #endregion

        private void button_mode_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(new Point(button_mode.Left, button_mode.Top + button_mode.Height)));
        }

        private void toolStripMenuItem_manualStart_Click(object sender, EventArgs e)
        {
            Manual();
        }

        private void toolStripMenuItem_valueIncreaseStart_Click(object sender, EventArgs e)
        {
            ValueIncrease();
        }

        private void toolStripMenuItem_valueDecreaseStart_Click(object sender, EventArgs e)
        {
            ValueDecrease();
        }

        private void toolStripMenuItem_timeIndex_Click(object sender, EventArgs e)
        {
            IndexCount();
        }

        private void toolStripMenuItem_timeStamp_Click(object sender, EventArgs e)
        {
            TimeStamp();
        }
    }
}
