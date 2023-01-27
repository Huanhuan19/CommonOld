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
    public partial class StopProps : UserControl
    {
        public StopProps()
        {
            InitializeComponent();
        }
        #region Props
        Classes.DataEngine _dataEngine;
        ExperimentProperties.EndProps _stopProps;
        public ExperimentProperties.EndProps SelectedEndProps
        {
            get
            {
                return _stopProps;
            }
        }
        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine, ExperimentProperties.EndProps stopProps)
        {
            _dataEngine = dataEngine;
            _stopProps = new ExperimentProperties.EndProps(stopProps.ToString());
            FillList();
        }
        void FillList()
        {
            switch (_stopProps.EndMode)
            {
                case ExperimentProperties.EndMode.Manual:
                    Manual();
                    break;
                case ExperimentProperties.EndMode.ValueIncrease:
                    ValueIncrease();
                    break;
                case ExperimentProperties.EndMode.ValueDecrease:
                    ValueDecrease();
                    break;
                case ExperimentProperties.EndMode.IndexCount:
                    IndexCount();
                    break;
                case ExperimentProperties.EndMode.TimeStamp:
                    TimeStamp();
                    break;
                case ExperimentProperties.EndMode.Remote:
                    break;
            }
        }
        void Manual()
        {
            this.button_mode.Text = toolStripMenuItem_manualStop.Text;
            _stopProps.EndMode = ExperimentProperties.EndMode.Manual;
            panel1.Controls.Clear();
        }
        void ValueIncrease()
        {
            this.button_mode.Text = toolStripMenuItem_valueIncreaseStop.Text;
            _stopProps.EndMode = ExperimentProperties.EndMode.ValueIncrease;
            panel1.Controls.Clear();
            ValueSet valueSet = new ValueSet();
            valueSet.Initialize(_dataEngine, _stopProps);
            valueSet.Dock = DockStyle.Fill;
            panel1.Controls.Add(valueSet);

        }
        void ValueDecrease()
        {
            this.button_mode.Text = toolStripMenuItem_valueDecreaseStop.Text;
            _stopProps.EndMode = ExperimentProperties.EndMode.ValueDecrease;
            panel1.Controls.Clear();
            ValueSet valueSet = new ValueSet();
            valueSet.Initialize(_dataEngine, _stopProps);
            valueSet.Dock = DockStyle.Fill;
            panel1.Controls.Add(valueSet);
        }
        void IndexCount()
        {
            this.button_mode.Text = toolStripMenuItem_timeIndex.Text;
            _stopProps.EndMode = ExperimentProperties.EndMode.IndexCount;
            panel1.Controls.Clear();
            IndexCount indexCount = new IndexCount();
            indexCount.Initialize(_dataEngine, _stopProps);
            indexCount.Dock = DockStyle.Fill;
            panel1.Controls.Add(indexCount);
        }
        void TimeStamp()
        {
            this.button_mode.Text = toolStripMenuItem_timeStamp.Text;
            _stopProps.EndMode = ExperimentProperties.EndMode.TimeStamp;

            panel1.Controls.Clear();
            TimeStamp timeStamp = new TimeStamp();
            timeStamp.Initialize(_dataEngine, _stopProps);
            timeStamp.Dock = DockStyle.Fill;
            panel1.Controls.Add(timeStamp);
        }

        #endregion




        private void button_mode_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(new Point(button_mode.Left, button_mode.Top + button_mode.Height)));
        }

        private void toolStripMenuItem_manualStop_Click(object sender, EventArgs e)
        {
            Manual();
        }

        private void toolStripMenuItem_valueIncreaseStop_Click(object sender, EventArgs e)
        {
            ValueIncrease();
        }

        private void toolStripMenuItem_valueDecreaseStop_Click(object sender, EventArgs e)
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
