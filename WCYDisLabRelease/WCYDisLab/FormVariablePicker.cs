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
    public partial class FormVariablePicker : Form
    {
        public FormVariablePicker(Classes.DataEngine dataEngine,bool isMulti,string vName,List<string> vNames)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            _isMulti = isMulti;
            listView1.CheckBoxes = _isMulti;
            if (!_isMulti)
            {
                FillList(vName);
            }
            else
            {
                FillList(vNames);
            }
            Load += new EventHandler(FormVariablePicker_Load);
        }

        void FormVariablePicker_Load(object sender, EventArgs e)
        {
            listView1.Groups["listViewGroup_sensor"].Header = _resourceManager.GetString("Sensor");
            listView1.Groups["listViewGroup_expr"].Header = _resourceManager.GetString("Expr");
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.FormVariablePicker", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        bool _isMulti;
        public string SelectedVariableName
        {
            get 
            { 
                string name = "";
                if (listView1.SelectedItems.Count > 0)
                {
                    name = listView1.SelectedItems[0].SubItems[listView1.SelectedItems[0].SubItems.Count - 1].Text;
                }
                return name;
            }
        }
        public List<string> SelectedVariableNames
        {
            get
            {
                List<string> names = new List<string>();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Checked)
                    {
                        names.Add(listView1.Items[i].SubItems[listView1.Items[i].SubItems.Count - 1].Text);
                    }
                }
                return names;
            }
        }
        public List<string> SelectedSensorNames
        {
            get
            {
                List<string> names = new List<string>();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Checked &&listView1.Items[i].ImageKey == "sensor")
                    {
                        names.Add(listView1.Items[i].SubItems[listView1.Items[i].SubItems.Count - 1].Text);
                    }
                }
                return names;
            }
        }
        public List<string> SelectedExprNames
        {
            get
            {
                List<string> names = new List<string>();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Checked && listView1.Items[i].ImageKey == "expr")
                    {
                        names.Add(listView1.Items[i].SubItems[listView1.Items[i].SubItems.Count - 1].Text);
                    }
                }
                return names;
            }
        }

        #endregion

        #region Methods
        void FillVariable()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _dataEngine.DataManager.CurrentDataSection.Sensors.Count; i++)
            {
                ListViewItem item = new ListViewItem(_dataEngine.DataManager.CurrentDataSection.Sensors[i].DataProps.Caption);
                item.SubItems.Add(_dataEngine.DataManager.CurrentDataSection.Sensors[i].DataProps.Name);
                item.Tag = i;
                item.ImageKey="sensor";
                listView1.Items.Add( item );
                item.Group = listView1.Groups["listViewGroup_sensor"];
            }
            for (int i = 0; i < _dataEngine.DataManager.CurrentDataSection.DataStores.Count; i++)
            {
                ListViewItem item = new ListViewItem(_dataEngine.DataManager.CurrentDataSection.DataStores[i].DataProps.Caption);
                item.SubItems.Add(_dataEngine.DataManager.CurrentDataSection.DataStores[i].DataProps.Name);
                item.Tag = i;
                item.ImageKey = "expr";
                listView1.Items.Add(item);
                item.Group = listView1.Groups["listViewGroup_expr"];
            }
        }
        void FillList(string vName)
        {
            FillVariable();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (string.Equals(listView1.Items[i].SubItems[listView1.Items[i].SubItems.Count - 1].Text, vName))
                {
                    listView1.Items[i].Selected = true;
                    break;
                }
            }
        }
        void FillList(List<string> vNames)
        {
            FillVariable();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (vNames.Contains(listView1.Items[i].SubItems[listView1.Items[i].SubItems.Count - 1].Text))
                {
                    listView1.Items[i].Checked = true;
                }
            }
        }
        #endregion
    }
}
