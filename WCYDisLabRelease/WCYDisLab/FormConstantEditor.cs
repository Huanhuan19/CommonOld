using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;

namespace WCYDisLab
{
    public partial class FormConstantEditor : Form
    {
        public FormConstantEditor(TDataManager.ConstantElement constantElement)
        {
            InitializeComponent();
            _constantElement = constantElement;
            FillList();
            this.textBox_caption.TextChanged += new EventHandler(textBox_caption_TextChanged);
            this.textBox_defaultValue.TextChanged += new EventHandler(textBox_defaultValue_TextChanged);
        }

        void textBox_caption_TextChanged(object sender, EventArgs e)
        {
            _constantElement.Caption = this.textBox_caption.Text;
        }

        void textBox_defaultValue_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_defaultValue.Text, out value) && textBox_defaultValue.Text != "-")
            {
                textBox_defaultValue.Clear();
            }
            else
            {
                _constantElement.DefaultValue = value;
            }
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.FormConstantEditor", Assembly.GetExecutingAssembly());
        TDataManager.ConstantElement _constantElement;
        public TDataManager.ConstantElement SelectedConstantElement
        {
            get { return _constantElement; }
        }
        #endregion

        #region Methods
        void FillList()
        {
            this.textBox_caption.Text = _constantElement.Caption;
            this.textBox_defaultValue.Text = _constantElement.DefaultValue.ToString();
            listView1.Items.Clear();
            for (int i = 0; i < _constantElement.Values.Count; i++)
            {
                ListViewItem item = new ListViewItem((i+1).ToString());
                item.SubItems.Add(_constantElement.Values[i].ToString());
                listView1.Items.Add(item);
            }
        }
        #endregion
        private void button_add_Click(object sender, EventArgs e)
        {
            FormInputDouble f = new FormInputDouble(_resourceManager.GetString("InputValue"), 0);
            if (f.ShowDialog() == DialogResult.OK)
            {
                _constantElement.Add(f.SelectedValue);
                FillList();
            }
        }
        private void button_remove_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _constantElement.Values.Count)
                {
                    _constantElement.Values.RemoveAt(index);
                    FillList();
                }
            }
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _constantElement.Values.Count)
                {
                    _constantElement.MoveUp(index);
                    FillList();
                }
            }

        }

        private void button_down_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _constantElement.Values.Count)
                {
                    _constantElement.MoveDown(index);
                    FillList();
                }
            }

        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            _constantElement.Clear();
            FillList();
        }

    }
}
