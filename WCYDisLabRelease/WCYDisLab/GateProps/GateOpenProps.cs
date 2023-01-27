using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace WCYDisLab.GateProps
{
    public partial class GateOpenProps : UserControl
    {
        public GateOpenProps()
        {
            InitializeComponent();
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.GateProps.GateOpenProps", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        ExperimentProperties.PhotoGateMode _openMode;
        bool _gateAvailable;
        public ExperimentProperties.PhotoGateMode SelectedOpenMode
        {
            get
            {
                return _openMode;
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
        public bool SelectedGateAvailable
        {
            get { return _gateAvailable; }
        }
        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine, ExperimentProperties.PhotoGateProps gateProps)
        {
            _dataEngine = dataEngine;
            
            valueSet1.Initialize(_dataEngine, gateProps.OpenGateValue, gateProps.OpenColumnName, gateProps.OpenCheckAbs);
            if (gateProps.Available)
            {
                switch (gateProps.OpenMode)
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
            else
            {
                Serial();
            }
        }
        void Serial()
        {
            this.button_mode.Text =_resourceManager.GetString("Always" );
            _gateAvailable = false;
            valueSet1.Visible = false;
        }
        void Manual()
        {
            this.button_mode.Text = _resourceManager.GetString("Manual");
            _gateAvailable = true;
            _openMode = ExperimentProperties.PhotoGateMode.Manual;
            valueSet1.Visible = false;
        }
        void ValueIncrease()
        {
            this.button_mode.Text = _resourceManager.GetString("ValueIncrease");
            _gateAvailable = true;
            _openMode = ExperimentProperties.PhotoGateMode.ValueIncrease;
            valueSet1.Visible = true;

        }
        void ValueDecrease()
        {
            this.button_mode.Text = _resourceManager.GetString("ValueDecrease");
            _gateAvailable = true;
            _openMode = ExperimentProperties.PhotoGateMode.ValueDecrease;
            valueSet1.Visible = true;
        }

        #endregion


        private void button_mode_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(new Point(button_mode.Left, button_mode.Top + button_mode.Height)));
        }

        private void toolStripMenuItem_serial_Click(object sender, EventArgs e)
        {
            Serial();
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
