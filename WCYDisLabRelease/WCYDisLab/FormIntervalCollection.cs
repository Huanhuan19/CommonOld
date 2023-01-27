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
    public partial class FormIntervalCollection : Form
    {
        public FormIntervalCollection(ExperimentProperties.IntervalCollection intervalCollection)
        {
            InitializeComponent();
            _intervalCollection = new ExperimentProperties.IntervalCollection(intervalCollection.ToString());
            FillList();
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }
        #region Props
        ExperimentProperties.IntervalCollection _intervalCollection;
        public ExperimentProperties.IntervalCollection SelectedIntervalCollection
        {
            get { return _intervalCollection; }
        }
        #endregion

        #region Methods
        void FillList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _intervalCollection.IntervalDefines.Count; i++)
            {
                ListViewItem item = new ListViewItem(_intervalCollection.IntervalDefines[i].ShiftIndex.ToString());
                item.SubItems.Add((_intervalCollection.IntervalDefines[i].Interval*1000).ToString() + "ms");
                item.SubItems.Add(_intervalCollection.IntervalDefines[i].Frequency.ToString() + "HZ");
                listView1.Items.Add(item);

            }
        }
        void Add()
        {
            FormIntervalEditor f = new FormIntervalEditor(0x05, 100);
            if (f.ShowDialog() == DialogResult.OK)
            {
                _intervalCollection.IntervalDefines.Add(new ExperimentProperties.IntervalDefine( f.SelectedIntervalValue,f.SelectedShiftIndex));
                FillList();
            }
        }
        void Remove()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _intervalCollection.IntervalDefines.Count)
                {
                    _intervalCollection.IntervalDefines.RemoveAt(index);
                    FillList();
                }
            }
        }
        void Edit()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _intervalCollection.IntervalDefines.Count)
                {
                    FormIntervalEditor f = new FormIntervalEditor(_intervalCollection.IntervalDefines[index].ShiftIndex, _intervalCollection.IntervalDefines[index].Interval);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        _intervalCollection.IntervalDefines[index].ShiftIndex = f.SelectedShiftIndex;
                        _intervalCollection.IntervalDefines[index].Interval = f.SelectedIntervalValue;
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
