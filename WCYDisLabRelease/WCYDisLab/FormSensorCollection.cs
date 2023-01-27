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
    public partial class FormSensorCollection : Form
    {
        public FormSensorCollection(Classes.DataEngine dataEngine,SensorManager.SensorCollection sensorCollection )
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            _sensorCollection = new SensorManager.SensorCollection(sensorCollection.ToString());
            FillList();
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }
        #region Props
        Classes.DataEngine _dataEngine;
        SensorManager.SensorCollection _sensorCollection;
        public SensorManager.SensorCollection SelectedSensorCollection
        {
            get { return _sensorCollection; }
        }
        #endregion

        #region Methods
        void FillList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _sensorCollection.SensorDefines.Count; i++)
            {
                ListViewItem item = new ListViewItem(_sensorCollection.SensorDefines[i].SensorID.ToString("X"));
                item.SubItems.Add(_sensorCollection.SensorDefines[i].Caption);
                item.SubItems.Add(_dataEngine.GetSensorTypeName(_sensorCollection.SensorDefines[i].TypeID));
                item.SubItems.Add(_sensorCollection.SensorDefines[i].MinValue.ToString());
                item.SubItems.Add(_sensorCollection.SensorDefines[i].MaxValue.ToString());
                item.SubItems.Add(_sensorCollection.SensorDefines[i].Unit);
                item.SubItems.Add(_sensorCollection.SensorDefines[i].Name);
                listView1.Items.Add(item);
            }
        }
        void Add()
        {
            FormSensorEditor f = new FormSensorEditor(new SensorManager.SensorDefine());
            if (f.ShowDialog() == DialogResult.OK)
            {
                _sensorCollection.Add(f.SelectedSensorDefine.ToString());
                FillList();
            }
        }
        void Remove()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _sensorCollection.SensorDefines.Count)
                {
                    _sensorCollection.SensorDefines.RemoveAt(index);
                    FillList();
                }
            }
        }
        void Edit()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _sensorCollection.SensorDefines.Count)
                {
                    FormSensorEditor f = new FormSensorEditor(_sensorCollection.SensorDefines[index]);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        _sensorCollection.SensorDefines[index].Parse(f.SelectedSensorDefine.ToString());
                        FillList();
                    }
                }
            }
        }
        #endregion

        private void button_add_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            Edit();
        }
    }
}
