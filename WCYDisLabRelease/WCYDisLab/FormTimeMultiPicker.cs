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
    public partial class FormTimeMultiPicker : Form
    {
        public FormTimeMultiPicker(Classes.DataEngine dataEngine,List<string> sectionNames)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            _sectionNames = sectionNames;
            FillList();
            this.KeyPreview = true;
            listView1.KeyDown += new KeyEventHandler(listView1_KeyDown);
        }
        #region Props
        Classes.DataEngine _dataEngine;
        List<string> _sectionNames;
        public List<string> SelectedSectionNames
        {
            get
            {
                List<string> names = new List<string>();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Checked)
                    {
                        if (i < _dataEngine.DataManager.DataSections.Count)
                        {
                            names.Add(_dataEngine.DataManager.DataSections[i].Name);
                        }
                    }
                }
                return names;
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
                item.Checked = _sectionNames.Contains(_dataEngine.DataManager.DataSections[i].Name);

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
