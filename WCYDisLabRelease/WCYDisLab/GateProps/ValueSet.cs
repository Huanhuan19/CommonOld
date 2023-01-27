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
    public partial class ValueSet : UserControl
    {
        public ValueSet()
        {
            InitializeComponent();
            textBox_value.TextChanged += new EventHandler(textBox_value_TextChanged);
        }

        void textBox_value_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_value.Text, out value) && textBox_value.Text != "-")
            {
                textBox_value.Clear();
            }
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.GateProps.ValueSet", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        public double SelectedValue
        {
            get
            {
                double value;
                double.TryParse(textBox_value.Text, out value);
                return value;
            }
        }
        public string SelectedColumnName
        {
            get { return variablePicker1.SelectedVariableName; }
        }
        public bool SelectedCheckAbs
        {
            get { return checkBox_abs.Checked; }
        }

        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine, double value,string columnName,bool checkAbs)
        {
            _dataEngine = dataEngine;
            this.textBox_value.Text = value.ToString();
            this.variablePicker1.Initialize(_resourceManager.GetString("Threshold"), _dataEngine, columnName, _dataEngine.DataManager.GetCaption(columnName));
            this.checkBox_abs.Checked = checkAbs;
        }
        #endregion

       
    }
}
