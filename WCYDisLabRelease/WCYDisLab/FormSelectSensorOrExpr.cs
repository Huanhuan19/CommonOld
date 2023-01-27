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
    public partial class FormSelectSensorOrExpr : Form
    {
        public FormSelectSensorOrExpr(List<TDataManager.DataStore> dataStores, int selectedIndex)
        {
            InitializeComponent();
            FillList(dataStores, selectedIndex);
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);

            this.KeyPreview = true;
            listView1.KeyDown += new KeyEventHandler(listView1_KeyDown);
        }


        void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }


        void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #region Props
        public int SelectedIndex
        {
            get
            {
                int index = -1;
                if (listView1.SelectedItems.Count > 0)
                {
                    index = listView1.SelectedItems[0].Index;

                }
                return index;
            }
        }
        #endregion

        void FillList(List<TDataManager.DataStore> dataStores, int selectedIndex)
        {
            listView1.Items.Clear();
            for (int i = 0; i < dataStores.Count; i++)
            {
                ListViewItem item = new ListViewItem(dataStores[i].DataProps.Caption);
                item.Tag = i;
                item.ImageKey = "sensor";
                listView1.Items.Add(item);
                if (selectedIndex == i)
                {
                    item.Selected = true;
                }
            }
        }
    }
}
