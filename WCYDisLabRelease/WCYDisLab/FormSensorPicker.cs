using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab
{
    public partial class FormSensorPicker : Form
    {
        public FormSensorPicker(Classes.DataEngine dataEngine,string selectedName)
        {
            InitializeComponent();
            listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
            FillList(dataEngine, selectedName);
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #region Props

        public string SelectedSensorName
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
        public byte SelectedSensorID
        {
            get 
            {
                byte sensorID = 0x00;
                if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Tag != null)
                {
                    sensorID = (byte)listView1.SelectedItems[0].Tag;
                }
                return sensorID;
            }
        }
        #endregion

        #region Methods
        void FillList(Classes.DataEngine dataEngine,string selectedName)
        {
            listView1.Items.Clear();
            for (int i = 0; i < dataEngine.SensorCollection.SensorDefines.Count; i++)
            {
                SensorManager.SensorDefine sensorDefine = dataEngine.SensorCollection.SensorDefines[i];
                ListViewItem item = new ListViewItem(sensorDefine.SensorIndex.ToString());
                item.SubItems.Add(sensorDefine.Caption);
                item.SubItems.Add(dataEngine.GetSensorTypeName(sensorDefine.SensorID));
                item.SubItems.Add(sensorDefine.MinValue.ToString());
                item.SubItems.Add(sensorDefine.MaxValue.ToString());
                item.SubItems.Add(sensorDefine.Unit);
                item.SubItems.Add(sensorDefine.Name);
                item.Tag = sensorDefine.SensorID;
                listView1.Items.Add(item);

                if (!string.IsNullOrEmpty(selectedName))
                {
                    item.Selected = string.Equals(sensorDefine.Name, selectedName);
                }
            }

        }
        #endregion

        void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_confirm.Enabled = listView1.SelectedItems.Count > 0;
        }
    }
}
