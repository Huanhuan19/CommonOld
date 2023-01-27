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

namespace WCYDisLab.StartProps
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
            _startProps.StartPortName = e.Name;
        }

        void checkBox_abs_CheckedChanged(object sender, EventArgs e)
        {
            _startProps.CheckAbs = checkBox_abs.Checked;
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
                _startProps.GateValue = value;
            }
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.StartProps.ValueSet", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        ExperimentProperties.StartProps _startProps;
        public ExperimentProperties.StartProps SelectedStartProps
        {
            get 
            {
                _startProps.CheckAbs = checkBox_abs.Checked;
                _startProps.StartPortName = variablePicker1.SelectedVariableName;
                double value;
                double.TryParse(textBox_value.Text, out value);
                _startProps.GateValue = value;
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
            this.textBox_value.Text = _startProps.GateValue.ToString();
            this.variablePicker1.Initialize(_resourceManager.GetString("TriggerVariable"), _dataEngine, _startProps.StartPortName, _dataEngine.DataManager.GetCaption(_startProps.StartPortName));
            this.checkBox_abs.Checked = _startProps.CheckAbs;
        }
        #endregion

       
    }
}
