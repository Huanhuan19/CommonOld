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

namespace WCYDisLab.StopProps
{
    public partial class ValueSet : UserControl
    {
        public ValueSet()
        {
            InitializeComponent();
            textBox_value.TextChanged += new EventHandler(textBox_value_TextChanged);
            checkBox_abs.CheckedChanged += new EventHandler(checkBox_abs_CheckedChanged);
            variablePicker1.VariablePicked += new WCYDisLab.Controls.VariablePickerHandler(variablePicker1_VariablePicked);
        }

        void variablePicker1_VariablePicked(object sender, WCYDisLab.Controls.VariablePickedEventArgs e)
        {
            _stopProps.EndPortName = e.Name;
        }

        void checkBox_abs_CheckedChanged(object sender, EventArgs e)
        {
            _stopProps.CheckAbs = checkBox_abs.Checked;
        }

        void textBox_value_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_value.Text, out value) && textBox_value.Text != "-")
            {
                textBox_value.Clear();
            }
            else
            {
                _stopProps.GateValue = value;
            }
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.StopProps.ValueSet", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        ExperimentProperties.EndProps _stopProps;
        public ExperimentProperties.EndProps SelectedEndProps
        {
            get 
            {
                _stopProps.CheckAbs = checkBox_abs.Checked;
                _stopProps.EndPortName = variablePicker1.SelectedVariableName;
                double value;
                double.TryParse(textBox_value.Text, out value);
                _stopProps.GateValue = value;
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
            this.textBox_value.Text = _stopProps.GateValue.ToString();
            this.variablePicker1.Initialize(_resourceManager.GetString("TriggerVariable"), _dataEngine, _stopProps.EndPortName, _dataEngine.DataManager.GetCaption(_stopProps.EndPortName));
            this.checkBox_abs.Checked = _stopProps.CheckAbs;
        }
        #endregion

       
    }
}
