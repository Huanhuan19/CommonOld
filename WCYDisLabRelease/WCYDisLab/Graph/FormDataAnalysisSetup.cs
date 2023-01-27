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
    public partial class FormDataAnalysisSetup : Form
    {
        public FormDataAnalysisSetup(List<VirtualInstrument.Classes.GraphLine> selectedCurves)
        {
            InitializeComponent();
            _selectedCurves.AddRange(selectedCurves);
            FillList();
        }
        #region Props
        List<VirtualInstrument.Classes.GraphLine> _selectedCurves = new List<VirtualInstrument.Classes.GraphLine>();
        DataAnalysis.Fitting _fitting = new DataAnalysis.Fitting();
        string _methodName = "";
        public List<VirtualInstrument.Classes.GraphLine> SelectedCurves
        {
            get
            {
                List<VirtualInstrument.Classes.GraphLine> list = new List<VirtualInstrument.Classes.GraphLine>();
                for (int i = 0; i < listView1.CheckedItems.Count; i++)
                {
                    int index = listView1.CheckedItems[i].Index;
                    if (index >= 0 && index < _selectedCurves.Count)
                    {
                        list.Add(new VirtualInstrument.Classes.GraphLine(_selectedCurves[index].ToString()));
                    }
                }
                return list;
            }
        }
        public string SelectedMethodName
        {
            get
            {
                return _methodName;
            }
        }
        public bool SelectedAddLabel
        {
            get { return checkBox_addLabel.Checked; }
        }
        #endregion

        #region Methods
        void FillList()
        {
            FillCurvesList();
            FillMethodsList();
        }
        void FillCurvesList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _selectedCurves.Count; i++)
            {
                ListViewItem item = new ListViewItem(_selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption);
                item.SubItems.Add(_selectedCurves[i].SectionName);
                item.SubItems.Add(_selectedCurves[i].Count.ToString());
                listView1.Items.Add(item);
            }
        }
        void FillMethodsList()
        {
            contextMenuStrip1.Items.Clear();
            for (int i = 0; i < _fitting.Methods.Count; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(_fitting.Methods[i].Expression);
                item.Tag =i; 
                item.Click += new EventHandler(item_Click);
                contextMenuStrip1.Items.Add(item);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            if( ((ToolStripMenuItem)sender).Tag !=null )
            {
                int index = (int)((ToolStripMenuItem)sender).Tag;
                if (index >= 0 && index < _fitting.Methods.Count)
                {
                    button_method.Text = _fitting.Methods[index].Expression;
                    _methodName = _fitting.Methods[index].Name;
                }
            }
        }

        #endregion

        private void button_method_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(new Point(button_method.Left, button_method.Top + button_method.Height)));
        }


    }
}
