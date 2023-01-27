using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.Controls
{
    public partial class ConstantControler : UserControl
    {
        public ConstantControler()
        {
            InitializeComponent();
            timer1.Start();
        }
        #region Props
        TDataManager.ConstantElement _constantElement;
        public TDataManager.ConstantElement SelectedConstantElement
        {
            get { return _constantElement; }
        }
        bool _needReload = false;
        #endregion

        #region Methods
        public void Initialize(TDataManager.ConstantElement constant)
        {
            _constantElement = constant;
            _constantElement.ConstantEvent += new TDataManager.ConstantHandler(_constantElement_ConstantEvent);
            _needReload = true;
            this.Disposed += new EventHandler(ConstantPopup_Disposed);
        }
        #endregion

        void ConstantPopup_Disposed(object sender, EventArgs e)
        {
            _constantElement.ConstantEvent -= _constantElement_ConstantEvent;
        }

        void _constantElement_ConstantEvent(object sender, TDataManager.ConstantArgs e)
        {
            _needReload = true;
        }
        void FillList()
        {
            this.label_caption.Text = _constantElement.Caption;
            this.textBox_current.Text = _constantElement.CurrentValue.ToString();
            //this.textBox_manual.Text = _constantElement.NextValue.ToString();
            listView1.Items.Clear();
            for (int i = 0; i < _constantElement.Values.Count; i++)
            {
                ListViewItem item = new ListViewItem(_constantElement.Values[i].ToString());
                if (_constantElement.Index == i)
                {
                    item.ImageKey = "Selected";
                }
                else
                {
                    item.ImageKey = "Unselected";
                }
                listView1.Items.Add(item);
            }
        }

        private void button_modify_Click(object sender, EventArgs e)
        {
            double value;
            if (double.TryParse(this.textBox_manual.Text, out value))
            {
                _constantElement.SetNextValue(value);
                _needReload = true;
            }

        }

        private void textBox_manual_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(textBox_manual.Text, out value) && textBox_manual.Text != "-")
            {
                textBox_manual.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needReload)
            {
                _needReload = false;
                FillList();
            }
        }
    }
}
