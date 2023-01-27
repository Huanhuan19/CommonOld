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

namespace WCYDisLab.Controls
{
    public partial class VariablesPad : UserControl
    {
        public VariablesPad()
        {
            InitializeComponent();
            timer1.Start();
            listView1.MouseUp += new MouseEventHandler(listView1_MouseUp);
            
            this.VisibleChanged += new EventHandler(VariablesPad_VisibleChanged);
            this.Load += new EventHandler(VariablesPad_Load);
        }

        void VariablesPad_Load(object sender, EventArgs e)
        {
            listView1.Groups["listViewGroup_sensor"].Header = _resourceManager.GetString("Sensor");
            listView1.Groups["listViewGroup_constant"].Header = _resourceManager.GetString("Constant");
            listView1.Groups["listViewGroup_expr"].Header = _resourceManager.GetString("Expr");
        }

        void VariablesPad_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                toolTip1.Show(toolTip1.GetToolTip(label2), this, PointToScreen(new Point(label2.Left + label2.Width / 2, label2.Top + label2.Height / 2)), 10000);
            }
        }

        void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    int index = (int)listView1.SelectedItems[0].Tag;
                    if (listView1.SelectedItems[0].Group == listView1.Groups["listViewGroup_sensor"])
                    {
                        if (_dataEngine.OffLine)
                        {
                            SensorManager.SensorDefine sensorDefine = _dataEngine.SensorCollection.GetSensorDefineBySensorID(_dataEngine.DataManager.DataSectionTemplete.Sensors[index].DataProps.SensorID);
                            toolStripMenuItem_shiftSetSensor.Enabled = sensorDefine.ShiftDefines.Count > 0;
                            toolStripMenuItem_removeSensor.Enabled = true;
                        }
                        else
                        {
                            SensorManager.SensorDefine sensorDefine = _dataEngine.SensorCollection.GetSensorDefineBySensorID(_dataEngine.DataManager.DataSectionActive.Sensors[index].DataProps.SensorID);
                            toolStripMenuItem_shiftSetSensor.Enabled = sensorDefine.ShiftDefines.Count > 0;
                            toolStripMenuItem_removeSensor.Enabled =false;
                        }
                        contextMenuStrip_sensor.Show(PointToScreen(e.Location));
                    }
                    else if (listView1.SelectedItems[0].Group == listView1.Groups["listViewGroup_constant"])
                    {
                        contextMenuStrip_constant.Show(PointToScreen(e.Location));
                    }
                    else if (listView1.SelectedItems[0].Group == listView1.Groups["listViewGroup_expr"])
                    {
                        contextMenuStrip_expr.Show(PointToScreen(e.Location));
                    }
                }
                else
                {
                    if (_dataEngine.OffLine)
                    {
                        toolStripMenuItem_addSensor.Enabled = true;
                    }
                    else
                    {
                        toolStripMenuItem_addSensor.Enabled = false;
                    }
                    contextMenuStrip_add.Show(PointToScreen(e.Location));
                }
            }
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.Controls.VariablesPad", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        TDataManager.DataSection Section
        {
            get 
            {
                TDataManager.DataSection section;
                if (_dataEngine.OffLine)
                {
                    section = _dataEngine.DataManager.DataSectionTemplete;
                }
                else
                {
                    section = _dataEngine.DataManager.DataSectionActive;
                }
                return section;
            }
        }
        bool _needLoadSensor = false,_needLoadConstant = false, _needLoadExpr = false,_needMaskSensorNormal = false,_needMaskSensorWarning = false;
        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine)
        {
            _dataEngine = dataEngine;
            _dataEngine.DataManager.SensorEvent += new TDataManager.DataStoreEventHandler(DataManager_SensorEvent);
            _dataEngine.DataManager.ExprEvent += new TDataManager.DataStoreEventHandler(DataManager_ExprEvent);
            _dataEngine.DataManager.ConstantEvent += new TDataManager.ConstantHandler(DataManager_ConstantEvent);
            _dataEngine.ConnectChanged += new WCYDataSource.ConnectedHandler(_dataEngine_ConnectChanged);
            _dataEngine.DataEngineEvent += new WCYDisLab.Classes.DataEngineHandler(_dataEngine_DataEngineEvent);
            _dataEngine.PortChanged += new WCYDataSource.PortCollectionHandler(_dataEngine_PortChanged);
            _dataEngine.ShiftSetChanged += new WCYDataSource.ShiftHandler(_dataEngine_ShiftSetChanged);
            _dataEngine.ValueRecieved += new WCYDataSource.ValueHandler(_dataEngine_ValueRecieved);
            _dataEngine.WorkStatusChanged += new WCYDataSource.StartStopHandler(_dataEngine_WorkStatusChanged);
            _dataEngine.OfflineEvent += new WCYDisLab.Classes.OfflineHandler(_dataEngine_OfflineEvent);
            _needLoadSensor = true;
            _needLoadConstant = true;
            _needLoadExpr = true;

        }

        void DataManager_ConstantEvent(object sender, TDataManager.ConstantArgs e)
        {
            _needLoadConstant = true;
            _needLoadExpr = true;
        }

        void DataManager_ExprEvent(object sender, TDataManager.DataStoreEventArgs e)
        {
            _needLoadExpr = true;
            _needLoadConstant = true;
        }

        void DataManager_SensorEvent(object sender, TDataManager.DataStoreEventArgs e)
        {
            _needLoadSensor = true;
        }

        void _dataEngine_OfflineEvent(object sender, WCYDisLab.Classes.OfflineEventArgs e)
        {
            if (e.IsOffline)
            {
                if (!_dataEngine.DataManager.TempleteEqualsActive)
                {
                    _dataEngine.DataManager.DataSectionActiveToTemplete(true);
                }

            }
            else
            {
                if (!_dataEngine.DataManager.TempleteEqualsActive)
                {
                    _dataEngine.DataManager.DataSectionTempleteToActive(true);
                }
                //_dataEngine.MaskSensorSet();
                if (!_dataEngine.DataManager.SensorMatch)
                {
                    toolTip1.Show(_resourceManager.GetString("SensorError"),this.Parent,PointToScreen(new Point(label2.Left + label2.Width /2 , label2.Top + label2.Height / 2)),5000);
                }

            }
            _needLoadConstant = true;
            _needLoadExpr = true;
            _needLoadSensor = true;
        }

        void _dataEngine_WorkStatusChanged(object sender, WCYDataSource.StartStopEventArgs e)
        {
            
        }

        void _dataEngine_ValueRecieved(object sender, WCYDataSource.ValueEventArgs e)
        {
            
        }

        void _dataEngine_ShiftSetChanged(object sender, WCYDataSource.ShiftEventArgs e)
        {
            
        }


        void _dataEngine_PortChanged(object sender, WCYDataSource.PortCollectionEventArgs e)
        {
            _needLoadSensor = true;
        }


        void _dataEngine_DataEngineEvent(object sender, WCYDisLab.Classes.DataEngineEventArgs e)
        {
            if (e.EventType == WCYDisLab.Classes.DataEngineEventType.Load || e.EventType == WCYDisLab.Classes.DataEngineEventType.New || e.EventType == WCYDisLab.Classes.DataEngineEventType.Recieved)
            {
                _needLoadConstant = true;
                _needLoadExpr = true;
                _needLoadSensor = true;

            }
        }

        void _dataEngine_ConnectChanged(object sender, WCYDataSource.ConnectEventArgs e)
        {
            if (e.Connected)
            {
                _needMaskSensorNormal = true;
            }
            else
            {
                _needMaskSensorWarning = true;
            }
        }
        void LoadSensor()
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Group == listView1.Groups["listViewGroup_sensor"])
                {
                    listView1.Items.RemoveAt(i);
                }
            }
            TDataManager.DataSection dataSection = Section;
            for (int i = 0; i < dataSection.Sensors.Count; i++)
            {
                ListViewItem item = new ListViewItem(dataSection.Sensors[i].DataProps.Caption);
                item.SubItems.Add(dataSection.Sensors[i].DataProps.Name);
                item.ImageKey = "sensor";
                item.Tag = i;
                listView1.Items.Add(item);
                item.Group = listView1.Groups["listViewGroup_sensor"];
            }
        }
        void MaskSensorWarning()
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Group == listView1.Groups["listViewGroup_sensor"])
                {
                    listView1.Items[i].ImageKey = "Warning";
                }
            }

        }
        void MaskSensorNormal()
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Group == listView1.Groups["listViewGroup_sensor"])
                {
                    listView1.Items[i].ImageKey = "sensor";
                }
            }

        }
        void LoadExpr()
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Group == listView1.Groups["listViewGroup_expr"])
                {
                    listView1.Items.RemoveAt(i);
                }
            }
            TDataManager.DataSection dataSection = Section;
            for (int i = 0; i < dataSection.DataStores.Count; i++)
            {
                ListViewItem item = new ListViewItem(dataSection.DataStores[i].DataProps.Caption);
                item.SubItems.Add(dataSection.DataStores[i].DataProps.Name);
                item.ImageKey = "expr";
                item.Tag = i;
                listView1.Items.Add(item);
                item.Group = listView1.Groups["listViewGroup_expr"];
            }
        }
        void LoadConstant()
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Group == listView1.Groups["listViewGroup_constant"])
                {
                    listView1.Items.RemoveAt(i);
                }
            }
            TDataManager.DataSection dataSection = Section;
            for (int i = 0; i < dataSection.Constants.Count; i++)
            {
                ListViewItem item = new ListViewItem(dataSection.Constants[i].Caption);
                item.SubItems.Add(dataSection.Constants[i].Name);
                item.ImageKey = "constant";
                item.Tag = i;
                listView1.Items.Add(item);
                item.Group = listView1.Groups["listViewGroup_constant"];
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needLoadSensor)
            {
                _needLoadSensor = false;
                LoadSensor();
            }
            if (_needLoadConstant)
            {
                _needLoadConstant = false;
                LoadConstant();
            }
            if (_needLoadExpr)
            {
                _needLoadExpr = false;
                LoadExpr();
            }
            if (_needMaskSensorNormal)
            {
                _needMaskSensorNormal = false;
                MaskSensorNormal();
            }
            if (_needMaskSensorWarning)
            {
                _needMaskSensorWarning = false;
                MaskSensorWarning();
            }
        }

        private void toolStripMenuItem_addSensor_Click(object sender, EventArgs e)
        {
            FormSensorPicker f = new FormSensorPicker(_dataEngine, "");
            if (f.ShowDialog() == DialogResult.OK)
            {
                SensorManager.SensorDefine sensorDefine = _dataEngine.SensorCollection.GetSensorDefineBySensorID(f.SelectedSensorID);
                if (sensorDefine != null)
                {
                    TDataManager.DataStore sensor= _dataEngine.DataManager.AddSensorNoIndex(sensorDefine.Caption, sensorDefine.MaxValue, sensorDefine.MinValue, sensorDefine.Decimal, sensorDefine.Calibration, sensorDefine.Unit, _dataEngine.DEFAULT_COUNT,f.SelectedSensorID);
                }
            }
        }

        private void toolStripMenuItem_addConstant_Click(object sender, EventArgs e)
        {
            FormConstantEditor f = new FormConstantEditor(new TDataManager.ConstantElement());
            if (f.ShowDialog() == DialogResult.OK)
            {
                _dataEngine.DataManager.AddConstant(f.SelectedConstantElement.Caption, f.SelectedConstantElement.DefaultValue, f.SelectedConstantElement.Values);

            }
        }

        private void toolStripMenuItem_addExpr_Click(object sender, EventArgs e)
        {
            FormExprEditor f = new FormExprEditor(_dataEngine, new TDataManager.DataStore());
            if (f.ShowDialog() == DialogResult.OK)
            {
                _dataEngine.DataManager.AddDataStore(f.SelectedExpr.DataProps.Caption, f.SelectedExpr.DataProps.MaxValue, f.SelectedExpr.DataProps.MinValue, f.SelectedExpr.DataProps.Decimal,
                    f.SelectedExpr.DataProps.Calibration, f.SelectedExpr.DataProps.Expression, f.SelectedExpr.DataProps.Unit, f.SelectedExpr.DataProps.FlowSensorName, f.SelectedExpr.DataProps.FlowStep, _dataEngine.DEFAULT_COUNT);
            }
        }

        private void toolStripMenuItem_removeSensor_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_sensor"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    _dataEngine.DataManager.RemoveSensor(index);
                }
            }
        }

        private void toolStripMenuItem_editConstant_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_constant"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < Section.Constants.Count)
                    {
                        FormConstantEditor f = new FormConstantEditor(Section.Constants[index]);
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            Section.Constants[index].Parse(f.SelectedConstantElement.ToString());
                        }
                    }
                }
            }

        }

        private void toolStripMenuItem_removeExpr_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_expr"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    _dataEngine.DataManager.RemoveExpr(index);
                }
            }

        }

        private void toolStripMenuItem_editExpr_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_expr"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < Section.DataStores.Count)
                    {
                        FormExprEditor f = new FormExprEditor(_dataEngine,Section.DataStores[index]);
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            Section.DataStores[index].Parse(f.SelectedExpr.ToString());
                        }
                    }
                }
            }

        }

        private void toolStripMenuItem_moveUpExpr_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_expr"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    _dataEngine.DataManager.MoveUpDataStore(index);
                }
            }

        }

        private void toolStripMenuItem_moveDownExpr_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_expr"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    _dataEngine.DataManager.MoveDownDataStore(index);
                }
            }

        }

        private void toolStripMenuItem_removeConstant_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_constant"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    _dataEngine.DataManager.RemoveConstant(index);
                }
            }

        }

        private void toolStripMenuItem_popGraphSensor_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_sensor"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.Sensors.Count)
                    {
                        TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.Sensors[index];
                        Graph.FormGraph f = new WCYDisLab.Graph.FormGraph(_dataEngine, dataStore.DataProps.Name, true);
                        f.MdiParent = this.ParentForm;
                        f.Dock = DockStyle.Fill;
                        f.Show();
                    }
                }
            }
            
        }
        private void toolStripMenuItem_popGraphExpr_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_expr"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.DataStores.Count)
                    {
                        TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.DataStores[index];
                        Graph.FormGraph f = new WCYDisLab.Graph.FormGraph(_dataEngine, dataStore.DataProps.Name, false);
                        f.MdiParent = this.ParentForm;
                        f.Dock = DockStyle.Fill;
                        f.Show();
                    }
                }
            }

        }


        private void toolStripMenuItem_popTableSensor_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_sensor"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.Sensors.Count)
                    {
                        TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.Sensors[index];
                        Table.FormTable f = new WCYDisLab.Table.FormTable(_dataEngine, dataStore.DataProps.Name, true);
                        f.MdiParent = this.ParentForm;
                        f.Dock = DockStyle.Fill;
                        f.Show();
                    }
                }
            }

        }

        private void toolStripMenuItem_popTableExpr_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_expr"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.DataStores.Count)
                    {
                        TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.DataStores[index];
                        Table.FormTable f = new WCYDisLab.Table.FormTable(_dataEngine, dataStore.DataProps.Name, true);
                        f.MdiParent = this.ParentForm;
                        f.Dock = DockStyle.Fill;
                        f.Show();
                    }
                }
            }

        }

        private void toolStripMenuItem_popDigitalMeterSensor_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_sensor"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.Sensors.Count)
                    {
                        TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.Sensors[index];
                        DigitalMeter.FormDigitalMeter f = new WCYDisLab.DigitalMeter.FormDigitalMeter(_dataEngine, dataStore.DataProps.Name, true);
                        f.MdiParent = this.ParentForm;
                        f.Show();
                    }
                }
            }

        }

        private void toolStripMenuItem_popDigitalMeterExpr_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_expr"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.DataStores.Count)
                    {
                        TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.DataStores[index];
                        DigitalMeter.FormDigitalMeter f = new WCYDisLab.DigitalMeter.FormDigitalMeter(_dataEngine, dataStore.DataProps.Name, true);
                        f.MdiParent = this.ParentForm;
                        f.Show();
                    }
                }
            }

        }

        private void toolStripMenuItem_popMeterSensor_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_sensor"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.Sensors.Count)
                    {
                        TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.Sensors[index];
                        Meter.FormMeter f = new WCYDisLab.Meter.FormMeter(_dataEngine, dataStore.DataProps.Name, true);
                        f.MdiParent = this.ParentForm;
                        f.Show();
                    }
                }
            }


        }

        private void toolStripMenuItem_popMeterExpr_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item.Group == listView1.Groups["listViewGroup_expr"] && item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.DataStores.Count)
                    {
                        TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.DataStores[index];
                        Meter.FormMeter f = new WCYDisLab.Meter.FormMeter(_dataEngine, dataStore.DataProps.Name, true);
                        f.MdiParent = this.ParentForm;
                        f.Show();
                    }
                }
            }

        }

        private void toolStripMenuItem_monitorConstant_Click(object sender, EventArgs e)
        {
            bool contains = false;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].GetType() == Type.GetType("WCYDisLab.FormConstantControler"))
                {
                    contains = true;
                    Application.OpenForms[i].Activate();
                }
            }
            if (!contains)
            {
                FormConstantControler f = new FormConstantControler(_dataEngine);
                f.Show();
            }
        }
    }
}
