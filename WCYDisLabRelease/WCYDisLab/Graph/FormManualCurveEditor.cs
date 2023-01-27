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
    public partial class FormManualCurveEditor : Form
    {
        public FormManualCurveEditor(string xCaption,string yCaption, List<VirtualInstrument.Classes.GraphPointDefine> points)
        {
            InitializeComponent();
            _points = points;
            textBox_xCaption.Text = xCaption;
            textBox_yCaption.Text = yCaption;
            Initialize();
            this.KeyPreview = true;
            dataGridView1.KeyDown += new KeyEventHandler(dataGridView1_KeyDown);
        }

        void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #region Props
        List<VirtualInstrument.Classes.GraphPointDefine> _points;
        DataTable _table;
        public List<VirtualInstrument.Classes.GraphPointDefine> SelectedPoints
        {
            get
            {
                dataGridView1.EndEdit();
                _points.Clear();
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    double x,y;
                    double.TryParse( _table.Rows[i]["X"].ToString(), out x );
                    double.TryParse( _table.Rows[i]["Y"].ToString(),out y );

                    VirtualInstrument.Classes.GraphPointDefine point = new VirtualInstrument.Classes.GraphPointDefine(x, y, 0);
                    _points.Add(point);
                }
                return _points;
            }
        }
        public string SelectedXCaption
        {
            get { return textBox_xCaption.Text; }
        }
        public string SelectedYCaption
        {
            get { return textBox_yCaption.Text; }
        }
        #endregion

        #region Methods
        void Initialize()
        {
            InitTable();
            for (int i = 0; i < _points.Count; i++)
            {
                DataRow newRow = _table.NewRow();
                newRow["X"] = _points[i].X;
                newRow["Y"] = _points[i].Y;
                _table.Rows.Add(newRow);
            }
        }
        void InitTable()
        {
            _table = new DataTable("Points");
            _table.Columns.AddRange(new DataColumn[] { 
                new DataColumn("X",Type.GetType( "System.Double" ),"",MappingType.Element),
                new DataColumn("Y",Type.GetType( "System.Double" ),"",MappingType.Element)
            });
            dataGridView1.DataSource = _table;
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double value;
            if (!double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out value))
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            }
        }
        #endregion


    }
}
