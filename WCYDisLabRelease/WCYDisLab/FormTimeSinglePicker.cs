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
    public partial class FormTimeSinglePicker : Form
    {
        public FormTimeSinglePicker(Classes.DataEngine dataEngine ,string sectionName)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            _sectionName = sectionName;
            FillList();
            this.KeyPreview = true;
            listView1.KeyDown += new KeyEventHandler(listView1_KeyDown);
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        #region Props
        Classes.DataEngine _dataEngine;
        string _sectionName;
        public string SelectedSectionName
        {
            get
            {
                string name = "";
                if (listView1.SelectedItems.Count > 0)
                {
                    int index = listView1.SelectedItems[0].Index;
                    if (index >= 0 && index < _dataEngine.DataManager.DataSections.Count)
                    {
                        name = _dataEngine.DataManager.DataSections[index].Name;
                    }
                }
                return name;
            }
        }
        #endregion

        #region Methods
        void FillList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _dataEngine.DataManager.DataSections.Count; i++)
            {
                ListViewItem item = new ListViewItem(_dataEngine.DataManager.DataSections[i].Caption);
                item.SubItems.Add(_dataEngine.DataManager.DataSections[i].Description);
                item.Selected = string.Equals(_dataEngine.DataManager.DataSections[i].Name, _sectionName);
                listView1.Items.Add(item);
            }
        }
        #endregion

        void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
