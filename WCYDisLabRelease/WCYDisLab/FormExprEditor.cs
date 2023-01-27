using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace WCYDisLab
{
    public partial class FormExprEditor : Form
    {
        public FormExprEditor(Classes.DataEngine dataEngine,TDataManager.DataStore dataStore)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            _dataStore = dataStore;
            this.textBox_ex.TextChanged += new EventHandler(textBox_ex_TextChanged);
            this.textBox_max.TextChanged += new EventHandler(textBox_max_TextChanged);
            this.textBox_min.TextChanged += new EventHandler(textBox_min_TextChanged);
            this.textBox_ex.MouseClick += new MouseEventHandler(textBox_ex_MouseClick);
            this.textBox_caption.TextChanged += new EventHandler(textBox_caption_TextChanged);
            FillList();
        }

        void textBox_caption_TextChanged(object sender, EventArgs e)
        {
            _dataStore.DataProps.Caption = textBox_caption.Text;
        }

        void textBox_ex_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(PointToScreen(e.Location));
            }
        }

        void textBox_min_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_min.Text, out value) && textBox_min.Text != "-")
            {
                textBox_min.Clear();
            }
            else
            {
                _dataStore.DataProps.MinValue = value;
            }
        }

        void textBox_max_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_max.Text, out value) && textBox_max.Text != "-")
            {
                textBox_max.Clear();
            }
            else
            {
                _dataStore.DataProps.MaxValue = value;
            }
        }

        void textBox_ex_TextChanged(object sender, EventArgs e)
        {
            DataAnalysis.Formula formula = new DataAnalysis.Formula(_dataEngine.DataManager.CurrentDataSection.GetCaption,_dataEngine.DataManager.CurrentDataSection.CheckName );
            textBox_description.Text = formula.Description(this.textBox_ex.Text);
            button_confirm.Enabled = formula.CheckSyntax(this.textBox_ex.Text);
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.FormExprEditor", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        TDataManager.DataStore _dataStore;
        public TDataManager.DataStore SelectedExpr
        {
            get 
            {
                _dataStore.DataProps.Decimal = (int)numericUpDown_decimal.Value;
                _dataStore.DataProps.FlowStep = (int)numericUpDown_step.Value;
                _dataStore.DataProps.IsSensor = false;
                _dataStore.DataProps.Unit = textBox_unit.Text;
                _dataStore.DataProps.Expression = textBox_ex.Text;
                _dataStore.DataProps.FlowSensorName = variablePicker1.SelectedVariableName;
                return _dataStore; 
            }
        }
        #endregion

        #region Methods
        void FillList()
        {
            this.textBox_caption.Text = _dataStore.DataProps.Caption;
            this.textBox_ex.Text = _dataStore.DataProps.Expression;
            this.textBox_max.Text = _dataStore.DataProps.MaxValue.ToString();
            this.textBox_min.Text = _dataStore.DataProps.MinValue.ToString();
            this.textBox_unit.Text = _dataStore.DataProps.Unit;
            this.numericUpDown_decimal.Value = _dataStore.DataProps.Decimal;
            this.numericUpDown_step.Value = _dataStore.DataProps.FlowStep;
            this.variablePicker1.Initialize(_resourceManager.GetString("Follow"), _dataEngine, _dataStore.DataProps.FlowSensorName, _dataEngine.DataManager.CurrentDataSection.GetCaption( _dataStore.DataProps.FlowSensorName));
        }
        #endregion

        private void toolStripMenuItem_formula_Click(object sender, EventArgs e)
        {
            FormSelectFormula f = new FormSelectFormula();
            if (f.ShowDialog() == DialogResult.OK)
            {
                textBox_ex.SelectedText = f.SelectedFormula;
            }
        }

        private void toolStripMenuItem_timeIndex_Click(object sender, EventArgs e)
        {
            this.textBox_ex.SelectedText = TDataManager.DataManager.TIMEINDEX_NAME;
        }

        private void toolStripMenuItem_timeStamp_Click(object sender, EventArgs e)
        {
            this.textBox_ex.SelectedText = TDataManager.DataManager.TIMESTAMP_NAME;
        }

        private void toolStripMenuItem_sensor_Click(object sender, EventArgs e)
        {
            TDataManager.DataSection section =_dataEngine.OffLine ? _dataEngine.DataManager.DataSectionTemplete : _dataEngine.DataManager.DataSectionActive;
            FormSelectSensorOrExpr f = new FormSelectSensorOrExpr(section.Sensors, -1);
            if (f.ShowDialog() == DialogResult.OK)
            {
                TDataManager.DataStore dataStore = section.Sensors[f.SelectedIndex];
                this.textBox_ex.SelectedText = dataStore.DataProps.Name;
            }
        }

        private void toolStripMenuItem_expr_Click(object sender, EventArgs e)
        {
            TDataManager.DataSection section = _dataEngine.OffLine ? _dataEngine.DataManager.DataSectionTemplete : _dataEngine.DataManager.DataSectionActive;
            FormSelectSensorOrExpr f = new FormSelectSensorOrExpr(section.DataStores, -1);
            if (f.ShowDialog() == DialogResult.OK)
            {
                TDataManager.DataStore dataStore = section.DataStores[f.SelectedIndex];
                this.textBox_ex.SelectedText = dataStore.DataProps.Name;
            }

        }

    }
}
