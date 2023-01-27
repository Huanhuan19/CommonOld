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
    public partial class FormSelectFormula : Form
    {
        public FormSelectFormula()
        {
            InitializeComponent(); 
            FillList();

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
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        public string SelectedFormula
        {
            get
            {
                string formula = "";
                if (listView1.SelectedItems.Count > 0)
                {
                    formula = listView1.SelectedItems[0].Text;
                }
                return formula;
            }
        }
        void FillList()
        {
            DataAnalysis.FormulaCollection formulaCollection = new DataAnalysis.FormulaCollection();
            listView1.Items.Clear();
            for (int i = 0; i < formulaCollection.Functions.Count; i++)
            {
                ListViewItem item = new ListViewItem(formulaCollection.Formulas[i].Expression);
                item.SubItems.Add(formulaCollection.Formulas[i].Description);
                listView1.Items.Add(item);

            }
        }
    }
}
