using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.Graph
{
    public partial class FormLabelManager : Form
    {
        public FormLabelManager(List<VirtualInstrument.Classes.GraphLabelDefine> labels)
        {
            InitializeComponent();
            _labels = new List<VirtualInstrument.Classes.GraphLabelDefine>();
            for (int i = 0; i < labels.Count; i++)
            {
                _labels.Add(new VirtualInstrument.Classes.GraphLabelDefine(labels[i].ToString()));
            }
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }
        #region Props
        List<VirtualInstrument.Classes.GraphLabelDefine> _labels;
        public List<VirtualInstrument.Classes.GraphLabelDefine> SelectedLabels
        {
            get { return _labels; }
        }
        #endregion

        #region Methods
        void FillList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _labels.Count; i++)
            {
                ListViewItem item = new ListViewItem(_labels[i].LabelText);
                item.SubItems.Add(_labels[i].Position.X.ToString() + "," + _labels[i].Position.Y.ToString());
                listView1.Items.Add(item);
            }
        }
        void Add()
        {
            FormInputText f = new FormInputText("");
            if (f.ShowDialog() == DialogResult.OK)
            {
                _labels.Add(new VirtualInstrument.Classes.GraphLabelDefine("label", "label", f.SelectedText, 0, 0, 0));
                FillList();
            }
        }
        void Remove()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _labels.Count)
                {
                    _labels.RemoveAt(index);
                    FillList();
                }
            }
        }
        void Edit()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _labels.Count)
                {
                    FormInputText f = new FormInputText(_labels[index].LabelText);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        _labels[index].LabelText = f.SelectedText;
                        FillList();
                    }
                }
            }
        }
        void EditPosition()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _labels.Count)
                {
                    FormInputPosition f = new FormInputPosition(_labels[index].Position.X, _labels[index].Position.Y);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        _labels[index].Position = new VirtualInstrument.Classes.GraphPointDefine(f.SelectX, f.SelectY, 0);
                        FillList();
                    }
                }
            }

        }
        void Clear()
        {
            _labels.Clear();
            FillList();
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

        private void button_clear_Click(object sender, EventArgs e)
        {
            Clear();            
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void button_position_Click(object sender, EventArgs e)
        {
            EditPosition();
        }
    }
}
