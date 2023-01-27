using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.Controls
{
    public partial class VariablePicker : UserControl
    {
        public VariablePicker()
        {
            InitializeComponent();
        }

        #region Props
        Classes.DataEngine _dataEngine;
        string _name;
        public string SelectedVariableName
        {
            get { return _name; }
        }
        public string SelectedVariableCaption
        {
            get { return textBox1.Text; }
        }
        #endregion

        #region Methods
        public void Initialize(string title,Classes.DataEngine dataEngine, string name, string caption)
        {
            label1.Text = title;
            _dataEngine = dataEngine;
            _name = name;
            textBox1.Text = caption;
        }
        #endregion

        private void toolStripMenuItem_timeIndex_Click(object sender, EventArgs e)
        {
            _name = TDataManager.DataManager.TIMEINDEX_NAME;
            textBox1.Text = _dataEngine.DataManager.GetCaption(_name);
            VariablePickedEventArgs a = new VariablePickedEventArgs(_name, _dataEngine.DataManager.GetCaption( _name));
            OnVariablePicked(a);

        }

        private void toolStripMenuItem_timeStamp_Click(object sender, EventArgs e)
        {
            _name = TDataManager.DataManager.TIMESTAMP_NAME;
            textBox1.Text = _dataEngine.DataManager.GetCaption(_name);
            VariablePickedEventArgs a = new VariablePickedEventArgs(_name, _dataEngine.DataManager.GetCaption(_name));
            OnVariablePicked(a);
        }

        private void toolStripMenuItem_sensor_Click(object sender, EventArgs e)
        {
            TDataManager.DataSection section = _dataEngine.OffLine ? _dataEngine.DataManager.DataSectionTemplete : _dataEngine.DataManager.DataSectionActive;
            FormSelectSensorOrExpr f = new FormSelectSensorOrExpr(section.Sensors, -1);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int index = f.SelectedIndex;
                if (index >= 0 && index < section.Sensors.Count)
                {
                    _name = section.Sensors[index].DataProps.Name;
                    textBox1.Text = section.Sensors[index].DataProps.Caption;
                    VariablePickedEventArgs a = new VariablePickedEventArgs(section.Sensors[index].DataProps.Name, section.Sensors[index].DataProps.Caption);
                    OnVariablePicked(a);
                }
            }
        }

        private void toolStripMenuItem_expr_Click(object sender, EventArgs e)
        {
            TDataManager.DataSection section = _dataEngine.OffLine ? _dataEngine.DataManager.DataSectionTemplete : _dataEngine.DataManager.DataSectionActive;
            FormSelectSensorOrExpr f = new FormSelectSensorOrExpr(section.DataStores, -1);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int index = f.SelectedIndex;
                if (index >= 0 && index < section.DataStores.Count)
                {
                    _name = section.DataStores[index].DataProps.Name;
                    textBox1.Text = section.DataStores[index].DataProps.Caption;
                    VariablePickedEventArgs a = new VariablePickedEventArgs(section.DataStores[index].DataProps.Name, section.DataStores[index].DataProps.Caption);
                    OnVariablePicked(a);

                }
            }

        }

        private void button_pick_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(new Point(button_pick.Left, button_pick.Top + button_pick.Height)));
        }

        #region Event
        public event VariablePickerHandler VariablePicked = null;
        protected void OnVariablePicked(VariablePickedEventArgs e)
        {
            if (VariablePicked != null)
            {
                VariablePicked(this, e);
            }
        }
        #endregion
    }

    public class VariablePickedEventArgs : EventArgs
    {
        string _name, _caption;
        public VariablePickedEventArgs(string name, string caption)
        {
            _name = name;
            _caption = caption;
        }
        public string Name
        {
            get { return _name; }
        }
        public string Caption
        {
            get { return _caption; }
        }
    }
    public delegate void VariablePickerHandler( object sender,VariablePickedEventArgs e );
}
