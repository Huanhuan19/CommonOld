using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;

namespace WCYDisLab.GateProps
{
    public partial class GateCloseProps : UserControl
    {
        public GateCloseProps()
        {
            InitializeComponent();
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.GateProps.GateCloseProps", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        ExperimentProperties.PhotoGateMode _closeMode;
        public ExperimentProperties.PhotoGateMode SelectedCloseMode
        {
            get
            {
                return _closeMode;
            }
        }
        public double SelectedValue
        {
            get { return valueSet1.SelectedValue; }
        }
        public string SelectedColumName
        {
            get { return valueSet1.SelectedColumnName; }
        }
        public bool SelectedCheckAbs
        {
            get { return valueSet1.SelectedCheckAbs; }
        }
        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine, ExperimentProperties.PhotoGateProps gateProps)
        {
            _dataEngine = dataEngine;

            valueSet1.Initialize(_dataEngine, gateProps.CloseGateValue, gateProps.CloseColumnName, gateProps.CloseCheckAbs);
            switch (gateProps.CloseMode)
            {
                case ExperimentProperties.PhotoGateMode.Manual:
                    Manual();
                    break;
                case ExperimentProperties.PhotoGateMode.ValueIncrease:
                    ValueIncrease();
                    break;
                case ExperimentProperties.PhotoGateMode.ValueDecrease:
                    ValueDecrease();
                    break;
            }

        }
        void Manual()
        {
            this.button_mode.Text = _resourceManager.GetString("Manual");
            _closeMode = ExperimentProperties.PhotoGateMode.Manual;
            valueSet1.Visible = false;
        }
        void ValueIncrease()
        {
            this.button_mode.Text = _resourceManager.GetString("ValueIncrease");
            _closeMode = ExperimentProperties.PhotoGateMode.ValueIncrease;
            valueSet1.Visible = true;

        }
        void ValueDecrease()
        {
            this.button_mode.Text = _resourceManager.GetString("ValueDecrease");
            _closeMode = ExperimentProperties.PhotoGateMode.ValueDecrease;
            valueSet1.Visible = true;
        }

        #endregion


        private void button_mode_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(new Point(button_mode.Left, button_mode.Top + button_mode.Height)));
        }

        private void toolStripMenuItem_manual_Click(object sender, EventArgs e)
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
    }
}
