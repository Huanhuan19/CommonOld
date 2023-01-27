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


namespace WCYDisLab.Graph
{
    public partial class CurvePick : UserControl
    {
        public CurvePick()
        {
            InitializeComponent();
            listView_y2Axis.DoubleClick += new EventHandler(listView_y2Axis_DoubleClick);
            listView_yAxis.DoubleClick += new EventHandler(listView_yAxis_DoubleClick);
            this.Load += new EventHandler(CurvePick_Load);
        }

        void CurvePick_Load(object sender, EventArgs e)
        {
            listView_variables.Groups["listViewGroup_sensor"].Header = _resourceManager.GetString("Sensor");
            listView_variables.Groups["listViewGroup_expr"].Header = _resourceManager.GetString("Expr");

            listView_y2Axis.Groups["listViewGroup_sensor"].Header = _resourceManager.GetString("Sensor");
            listView_y2Axis.Groups["listViewGroup_expr"].Header = _resourceManager.GetString("Expr");

            listView_yAxis.Groups["listViewGroup_sensor"].Header = _resourceManager.GetString("Sensor");
            listView_yAxis.Groups["listViewGroup_expr"].Header = _resourceManager.GetString("Expr");
        }

        void listView_yAxis_DoubleClick(object sender, EventArgs e)
        {
            EditY();
        }

        void listView_y2Axis_DoubleClick(object sender, EventArgs e)
        {
            EditY2();
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.Graph.CurvePick", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        List<VirtualInstrument.Classes.GraphLineDefine> _lines;
        public List<VirtualInstrument.Classes.GraphLineDefine> SelectedLines
        {
            get
            {
                return _lines;
            }
        }
        #endregion

        #region Methods
        public void Initialize(Classes.DataEngine dataEngine, List<VirtualInstrument.Classes.GraphLineDefine> lines)
        {
            _dataEngine = dataEngine;
            _lines = new List<VirtualInstrument.Classes.GraphLineDefine>();
            for (int i = 0; i < lines.Count; i++)
            {
                _lines.Add(new VirtualInstrument.Classes.GraphLineDefine(lines[i].ToString() ));
            }
            FillList();
        }
        bool LineContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _lines.Count; i++)
            {
                if (string.Equals(_lines[i].ColumnDefine.ColumnName, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        void FillList()
        {
            FillVariables();
            FillY();
            FillY2();
        }
        void FillVariables()
        {
            listView_variables.Items.Clear();
            for (int i = 0; i < _dataEngine.DataManager.CurrentDataSection.Sensors.Count; i++)
            {
                TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.Sensors[i];
                if (!LineContains(dataStore.DataProps.Name))
                {
                    ListViewItem item = new ListViewItem(dataStore.DataProps.Caption);
                    item.Tag = i;
                    listView_variables.Items.Add(item);
                    item.Group = listView_variables.Groups["listViewGroup_sensor"];
                }
            }
            for (int i = 0; i < _dataEngine.DataManager.CurrentDataSection.DataStores.Count; i++)
            {
                TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.DataStores[i];
                if (!LineContains(dataStore.DataProps.Name))
                {
                    ListViewItem item = new ListViewItem(dataStore.DataProps.Caption);
                    item.Tag = i;
                    listView_variables.Items.Add(item);
                    item.Group = listView_variables.Groups["listViewGroup_expr"];
                }
            }
        }
        void FillY()
        {
            listView_yAxis.Items.Clear();
            for (int i = 0; i < _lines.Count; i++)
            {
                if (!_lines[i].IsY2Axis)
                {
                    ListViewItem item = new ListViewItem(_lines[i].ColumnDefine.ColumnCaption);
                    item.Tag = i;
                    item.ForeColor = _lines[i].LineColor;
                    listView_yAxis.Items.Add(item);
                    if (_lines[i].ColumnDefine.ColumnName.ToLower().Contains("sensor"))
                    {
                        item.Group = listView_yAxis.Groups["listViewGroup_sensor"];
                    }
                    else
                    {
                        item.Group = listView_yAxis.Groups["listViewGroup_expr"];
                    }
                }
            }
        }
        void FillY2()
        {
            listView_y2Axis.Items.Clear();
            for (int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].IsY2Axis)
                {
                    ListViewItem item = new ListViewItem(_lines[i].ColumnDefine.ColumnCaption);
                    item.Tag = i;
                    item.ForeColor = _lines[i].LineColor;
                    listView_y2Axis.Items.Add(item);
                    if (_lines[i].ColumnDefine.ColumnName.ToLower().Contains("sensor"))
                    {
                        item.Group = listView_y2Axis.Groups["listViewGroup_sensor"];
                    }
                    else
                    {
                        item.Group = listView_y2Axis.Groups["listViewGroup_expr"];
                    }
                }

            }
        }
        void EditY()
        {
            if (listView_yAxis.SelectedItems.Count > 0)
            {
                if (listView_yAxis.SelectedItems[0].Tag != null)
                {
                    int index = (int)listView_yAxis.SelectedItems[0].Tag;
                    if (index >= 0 && index < _lines.Count)
                    {
                        FormCurveEditor f = new FormCurveEditor(_lines[index]);
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            _lines[index].IsSmooth = f.SelectedIsSmooth;

                            _lines[index].LineColor = f.SelectedLineColor;
                            _lines[index].LineVisible = f.SelectedLineVisible;
                            _lines[index].LineWidthF = f.SelectedLineWidth;
                            _lines[index].PointVisible = f.SelectedPointVisible;
                            _lines[index].SmoothTention = f.SelectedSmoothTention;
                            FillY();
                        }
                    }
                }
            }
        }
        void EditY2()
        {
            if (listView_y2Axis.SelectedItems.Count > 0)
            {
                if (listView_y2Axis.SelectedItems[0].Tag != null)
                {
                    int index = (int)listView_y2Axis.SelectedItems[0].Tag;
                    if (index >= 0 && index < _lines.Count)
                    {
                        FormCurveEditor f = new FormCurveEditor(_lines[index]);
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            _lines[index].IsSmooth = f.SelectedIsSmooth;

                            _lines[index].LineColor = f.SelectedLineColor;
                            _lines[index].LineVisible = f.SelectedLineVisible;
                            _lines[index].LineWidthF = f.SelectedLineWidth;
                            _lines[index].PointVisible = f.SelectedPointVisible;
                            _lines[index].SmoothTention = f.SelectedSmoothTention;
                            FillY2();
                        }
                    }
                }
            }
        }
        #endregion

        private void button_add2YAxis_Click(object sender, EventArgs e)
        {
            if (listView_variables.SelectedItems.Count > 0)
            {
                if (listView_variables.SelectedItems[0].Tag != null)
                {
                    int index = (int)listView_variables.SelectedItems[0].Tag;
                    if (listView_variables.SelectedItems[0].Group == listView_variables.Groups["listViewGroup_sensor"])
                    {
                        if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.Sensors.Count)
                        {
                            TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.Sensors[index];
                            _lines.Add(new VirtualInstrument.Classes.GraphLineDefine(dataStore.DataProps.Name, dataStore.DataProps.Caption, dataStore.DataProps.Decimal, Color.Red, false, true, 1f, false, false, 1f));
                            FillList();
                        }
                    }
                    else
                    {
                        if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.DataStores.Count)
                        {
                            TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.DataStores[index];
                            _lines.Add(new VirtualInstrument.Classes.GraphLineDefine(dataStore.DataProps.Name, dataStore.DataProps.Caption, dataStore.DataProps.Decimal, Color.Green, false, true, 1f, false, false, 1f));
                            FillList();
                        }
                    }
                }
            }
        }

        private void button_add2Y2Axis_Click(object sender, EventArgs e)
        {
            if (listView_variables.SelectedItems.Count > 0)
            {
                if (listView_variables.SelectedItems[0].Tag != null)
                {
                    int index = (int)listView_variables.SelectedItems[0].Tag;
                    if (listView_variables.SelectedItems[0].Group == listView_variables.Groups["listViewGroup_sensor"])
                    {
                        if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.Sensors.Count)
                        {
                            TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.Sensors[index];
                            _lines.Add(new VirtualInstrument.Classes.GraphLineDefine(dataStore.DataProps.Name, dataStore.DataProps.Caption, dataStore.DataProps.Decimal, Color.Red, false, true, 1f, true, false, 1f));
                            FillList();
                        }
                    }
                    else
                    {
                        if (index >= 0 && index < _dataEngine.DataManager.CurrentDataSection.DataStores.Count)
                        {
                            TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.DataStores[index];
                            _lines.Add(new VirtualInstrument.Classes.GraphLineDefine(dataStore.DataProps.Name, dataStore.DataProps.Caption, dataStore.DataProps.Decimal, Color.Green, false, true, 1f, true, false, 1f));
                            FillList();
                        }
                    }
                }

           }

        }

        private void button_removeFromY2Axis_Click(object sender, EventArgs e)
        {
            if (listView_y2Axis.SelectedItems.Count > 0)
            {
                if (listView_y2Axis.SelectedItems[0].Tag != null)
                {
                    int index = (int)listView_y2Axis.SelectedItems[0].Tag;
                    if (index >= 0 && index < _lines.Count)
                    {
                        _lines.RemoveAt(index);
                        FillList();
                    }
                }
            }
        }

        private void button_removeFromYAxis_Click(object sender, EventArgs e)
        {
            if (listView_yAxis.SelectedItems.Count > 0)
            {
                if (listView_yAxis.SelectedItems[0].Tag != null)
                {
                    int index = (int)listView_yAxis.SelectedItems[0].Tag;
                    if (index >= 0 && index < _lines.Count)
                    {
                        _lines.RemoveAt(index);
                        FillList();
                    }
                }
            }

        }

        private void button_editYAxisCurve_Click(object sender, EventArgs e)
        {
            EditY();
        }

        private void button_editY2AxisCurve_Click(object sender, EventArgs e)
        {
            EditY2();
        }
    }
}
