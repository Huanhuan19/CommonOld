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

namespace WCYDisLab.Graph
{
    public partial class FormStandardCurveManager : Form
    {
        public FormStandardCurveManager(List<VirtualInstrument.Classes.StandardCurveDefine> curves,VirtualInstrument.Classes.StandardCurveGetValue getValue)
        {
            InitializeComponent();
            _getValue = getValue;
            _curves = new List<VirtualInstrument.Classes.StandardCurveDefine>();
            for (int i = 0; i > curves.Count; i++)
            {
                _curves.Add(new VirtualInstrument.Classes.StandardCurveDefine(curves[i].ToString()));
            }
            FillList();
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.Graph.FormStandardCurveManager", Assembly.GetExecutingAssembly());
        VirtualInstrument.Classes.StandardCurveGetValue _getValue;
        List<VirtualInstrument.Classes.StandardCurveDefine> _curves;
        public List<VirtualInstrument.Classes.StandardCurveDefine> SelectedStandardCurves
        {
            get { return _curves; }
        }

        #endregion

        #region Methods
        void FillList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _curves.Count; i++)
            {
                ListViewItem item = new ListViewItem(_curves[i].Caption);
                item.SubItems.Add(_curves[i].MethodName);
                item.SubItems.Add(_curves[i].A.ToString() );
                item.SubItems.Add(_curves[i].B.ToString() );
                item.SubItems.Add(_curves[i].C.ToString() );
                item.SubItems.Add(_curves[i].D.ToString());
                listView1.Items.Add(item);
            }
        }
        void Add()
        {
            FormStandardCurveEditor f = new FormStandardCurveEditor(_resourceManager.GetString("StandardCurve"),"", 0, 0, 0, 0);
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(f.SelectedMethodName))
                {
                    _curves.Add(new VirtualInstrument.Classes.StandardCurveDefine("StandardCurve", f.SelectedCaption, 
                        f.SelectedMethodName, f.SelectedA, f.SelectedB, f.SelectedC, f.SelectedD,false,_getValue));
                    FillList();

                }
            }
        }
        void Edit()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _curves.Count)
                {
                    FormStandardCurveEditor f = new FormStandardCurveEditor(_curves[index].Caption,_curves[index].MethodName,_curves[index].A,_curves[index].B,_curves[index].C,_curves[index].D);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(f.SelectedMethodName))
                        {
                            _curves[index].Caption = f.SelectedCaption;
                            _curves[index].MethodName = f.SelectedMethodName;
                            _curves[index].A = f.SelectedA;
                            _curves[index].B = f.SelectedB;
                            _curves[index].C = f.SelectedC;
                            _curves[index].D = f.SelectedD;
                            FillList();

                        }
                    }
                }
            }
        }
        void Remove()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _curves.Count)
                {
                    _curves.RemoveAt(index);
                    FillList();
                }
            }

        }
        void Clear()
        {
            _curves.Clear();
            FillList();
        }
        #endregion

        private void button_add_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
