using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WCYDisLab.Graph
{
    public partial class FormManualCurveManager : Form
    {
        public FormManualCurveManager(List<VirtualInstrument.Classes.GraphManualLine> lines)
        {
            InitializeComponent();
            _lines = new List<VirtualInstrument.Classes.GraphManualLine>();
            for (int i = 0; i < lines.Count; i++)
            {
                _lines.Add(new VirtualInstrument.Classes.GraphManualLine( lines[i].ToString()));
            }
            FillList();
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        #region Props
        List<VirtualInstrument.Classes.GraphManualLine> _lines;
        public List<VirtualInstrument.Classes.GraphManualLine> SelectedManualLines
        {
            get { return _lines; }
        }
        #endregion

        #region Methods
        void FillList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _lines.Count; i++)
            {
                ListViewItem item = new ListViewItem(_lines[i].Caption);
                item.SubItems.Add(_lines[i].Count.ToString());
                listView1.Items.Add(item);
            }
        }
        void Edit()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _lines.Count)
                {
                    FormManualCurveEditor f = new FormManualCurveEditor(_lines[index].XCaption, _lines[index].YCaption, _lines[index].Points);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        VirtualInstrument.Classes.GraphManualLine line = _lines[index];
                        line.Color = Color.Black;
                        line.XCaption = f.SelectedXCaption;
                        line.YCaption = f.SelectedYCaption;
                        VirtualInstrument.Classes.GraphPointDefine[] points = new VirtualInstrument.Classes.GraphPointDefine[f.SelectedPoints.Count];
                        f.SelectedPoints.CopyTo(points, 0);
                        line.Clear();
                        for (int i = 0; i < points.Length; i++)
                        {
                            line.Add(points[i].X, points[i].Y);
                        }
                        FillList();

                    }
                }
            }

        }
        void Import(string filename)
        {
            DataTable t = new DataTable();
            try
            {
                string strPdbcCon = @"Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=False;Data Source=" + openFileDialog1.FileName + ";Extended Properties=Excel 8.0";
                OleDbConnection OleDB = new OleDbConnection(strPdbcCon);
                OleDbDataAdapter OleDat = new OleDbDataAdapter("Select * from [Sheet1$]", OleDB);
                OleDat.Fill(t);
                if (t.Columns.Contains("X") && t.Columns.Contains("Y"))
                {
                    List<VirtualInstrument.Classes.GraphPointDefine> points = new List<VirtualInstrument.Classes.GraphPointDefine>();
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        double x, y;
                        double.TryParse(t.Rows[i]["X"].ToString(), out x);
                        double.TryParse(t.Rows[i]["Y"].ToString(), out y);
                        points.Add(new VirtualInstrument.Classes.GraphPointDefine(x, y));
                    }
                    FormManualCurveEditor f = new FormManualCurveEditor("X", "Y", points);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        VirtualInstrument.Classes.GraphManualLine line = new VirtualInstrument.Classes.GraphManualLine();
                        line.Color = Color.Black;
                        line.XCaption = f.SelectedXCaption;
                        line.YCaption = f.SelectedYCaption;
                        VirtualInstrument.Classes.GraphPointDefine[] ps = new VirtualInstrument.Classes.GraphPointDefine[f.SelectedPoints.Count];
                        f.SelectedPoints.CopyTo(ps, 0);
                        for (int i = 0; i < ps.Length; i++)
                        {
                            line.Add(ps[i].X, ps[i].Y);
                        }
                        _lines.Add(line);
                        FillList();

                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        private void button_open_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            _lines.Clear();
            FillList();
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _lines.Count)
                {
                    _lines.RemoveAt(index);
                    FillList();
                }
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            FormManualCurveEditor f = new FormManualCurveEditor("X","Y",new List<VirtualInstrument.Classes.GraphPointDefine>());
            if (f.ShowDialog() == DialogResult.OK)
            {
                VirtualInstrument.Classes.GraphManualLine line = new VirtualInstrument.Classes.GraphManualLine();
                line.Color = Color.Black;
                line.XCaption = f.SelectedXCaption;
                line.YCaption = f.SelectedYCaption;
                VirtualInstrument.Classes.GraphPointDefine[] points = new VirtualInstrument.Classes.GraphPointDefine[f.SelectedPoints.Count];
                f.SelectedPoints.CopyTo(points,0);
                for (int i = 0; i < points.Length; i++)
                {
                    line.Add(points[i].X, points[i].Y);
                }
                _lines.Add(line);
                FillList();

            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            Edit();
        }
    }
}
