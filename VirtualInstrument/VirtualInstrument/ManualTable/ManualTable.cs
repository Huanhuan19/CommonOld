using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualInstrument.ManualTable
{
    public partial class ManualTable : UserControl
    {
        public ManualTable()
        {
            InitializeComponent();
            dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            LoadDefault();
        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!_initMode)
            {
                if (_cellValidate != null)
                {
                    if (!_cellValidate(e.ColumnIndex, e.RowIndex, dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex]))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        #region delegate
        ManualDataTableCellValidate _cellValidate = null;
        public ManualDataTableCellValidate CellValidate
        {
            get { return _cellValidate; }
            set { _cellValidate = value; }
        }
        #endregion

        #region Props
        DataTable _t;
        List<MTColumnDefine> _columns = new List<MTColumnDefine>();
        bool _isVertical = true;
        bool _initMode = false;
        public DataGridView DataGridView
        {
            get { return dataGridView1; }
        }
        public bool IsVertical
        {
            get { return _isVertical; }
            set { _isVertical = value; MaskTable(); MaskGrid(); }
        }
        public int RowHeight
        {
            get
            {
                return dataGridView1.RowTemplate.Height;
            }
            set
            {
                dataGridView1.RowTemplate.Height = value;
            }
        }
        public int ColumnHeight
        {
            get
            {
                return dataGridView1.ColumnHeadersHeight;
            }
            set
            {
                dataGridView1.ColumnHeadersHeight = value;
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _columns.Clear();
            _isVertical = true;
            _t = new DataTable();
            dataGridView1.DataSource = _t;
        }
        public void Initialize(bool isVertical,List<MTColumnDefine> columns)
        {
            _isVertical = isVertical;
            _columns.Clear();
            _columns.AddRange(columns);
            MaskTable();
            MaskGrid();
        }
        public void AddColumn(MTColumnDefine column)
        {
            _columns.Add(column);
            MaskTable();
            MaskGrid();
        }
        void MaskTable()
        {
            _t.Rows.Clear();
            _t.Columns.Clear();
            if (_isVertical)
            {
                for (int i = 0; i < _columns.Count; i++)
                {
                    DataColumn column = new DataColumn(_columns[i].Name, Type.GetType(_columns[i].TypeName), "", MappingType.Element);
                    column.Caption = _columns[i].Caption;
                    _t.Columns.Add(column);
                }
            }
            else
            {
                DataColumn column = new DataColumn(_t.Columns.Count.ToString(), Type.GetType("System.String"), "", MappingType.Element);
                _t.Columns.Add(column);
                for (int i = 0; i < _columns.Count; i++)
                {
                    DataRow r = _t.NewRow();
                   
                    r["0"] = _columns[i].Caption;
                    _t.Rows.Add(r);
                }
            }
        }
        void MaskGrid()
        {
            dataGridView1.DataSource = _t;
            if (_isVertical)
            {
                dataGridView1.ColumnHeadersVisible = true;
                for (int i = 0; i < _columns.Count; i++)
                {
                    dataGridView1.Columns[i].HeaderText = _columns[i].Caption;
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = _columns[i].BackColor;
                    dataGridView1.Columns[i].DefaultCellStyle.ForeColor = _columns[i].ForeColor;
                }
            }
            else
            {
                dataGridView1.ColumnHeadersVisible = false;
                for (int i = 0; i < _columns.Count; i++)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = _columns[i].BackColor;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = _columns[i].ForeColor;
                }
            }
        }
        public void SetRowCount(int count)
        {
            _initMode = true;
            if (_isVertical)
            {
                int currentCount = _t.Rows.Count;
                if (count > currentCount)
                {
                    int rowCount = count - currentCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow r = _t.NewRow();
                        _t.Rows.Add(r);
                    }
                }
            }
            else
            {
                int currentCount = _t.Columns.Count - 1;
                if (count > currentCount)
                {
                    int columnCount = count - currentCount;
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn column = new DataColumn(_t.Columns.Count.ToString(), Type.GetType("System.Double"), "", MappingType.Element);
                        _t.Columns.Add(column);
                    }
                }
            }
            _initMode = false;
        }
        public void AddRow()
        {
            _initMode = true;
            if (_isVertical)
            {
                _t.Rows.Add(_t.NewRow());
            }
            else
            {
                _t.Columns.Add(new DataColumn(_t.Columns.Count.ToString(), Type.GetType("System.Double"), "", MappingType.Element));
            }
            _initMode = false;
        }
        public void RemoveRow(int rowIndex)
        {
            _initMode = true;
            if (_isVertical)
            {
                if (rowIndex >= 0 && rowIndex < _t.Rows.Count)
                {
                    _t.Rows.RemoveAt(rowIndex);
                }
            }
            else
            {
                if (rowIndex >= 0 && rowIndex < _t.Columns.Count - 1)
                {
                    _t.Columns.RemoveAt(rowIndex + 1);
                }
            }
            _initMode = false;
        }
        public void ClearRows()
        {
            _initMode = true;
            if (_isVertical)
            {
                _t.Rows.Clear();
            }
            else
            {
                for (int i = _t.Columns.Count - 1; i > 0; i--)
                {
                    _t.Columns.RemoveAt(i);
                }
            }
            _initMode = false;
        }
        public void SetValue( int columnIndex, int rowIndex,double value)
        {
            _initMode = true;
            if (_isVertical)
            {
                if (columnIndex >= 0 && columnIndex < _t.Columns.Count)
                {
                    if (rowIndex >= 0 && rowIndex < _t.Rows.Count)
                    {
                        _t.Rows[rowIndex][columnIndex] = value;
                    }
                }
            }
            else
            {
                if (columnIndex >= 0 && columnIndex < _t.Rows.Count)
                {
                    if (rowIndex >= 0 && rowIndex < _t.Columns.Count - 1)
                    {
                        _t.Rows[columnIndex][rowIndex + 1] = value;
                    }
                }
            }
            _initMode = false;
        }
        public void SetValue(string columnName, int rowIndex, double value)
        {
            _initMode = true;
            if (_isVertical)
            {
                if (rowIndex >= 0 && rowIndex < _t.Rows.Count)
                {
                    for (int i = 0; i < _columns.Count; i++)
                    {
                        if (string.Equals(_columns[i].Name, columnName))
                        {
                            _t.Rows[rowIndex][i] = value;
                        }
                    }
                }
            }
            else
            {
                if (rowIndex >= 0 && rowIndex < _t.Columns.Count - 1)
                {
                    for (int i = 0; i < _columns.Count; i++)
                    {
                        if (string.Equals(_columns[i].Name, columnName))
                        {
                            _t.Rows[i][rowIndex +1] = value;
                        }
                    }
                }
            }
            _initMode = false;
        }
        public double GetValue(string columnName, int rowIndex)
        {
            double value = 0;
            if (_isVertical)
            {
                if (rowIndex >= 0 && rowIndex < _t.Rows.Count)
                {
                    if (_t.Columns.Contains(columnName))
                    {
                        double.TryParse(_t.Rows[rowIndex][columnName].ToString(), out value);
                    }
                }
            }
            else
            {
                if (rowIndex >= 0 && rowIndex < _t.Columns.Count - 1)
                {
                    for (int i = 0; i < _columns.Count; i++)
                    {
                        if (string.Equals(_columns[i].Name, columnName))
                        {
                            double.TryParse(_t.Rows[i][0].ToString(), out value);
                        }
                    }
                }
            }
            return value;
        }
        public double GetValue(int columnIndex, int rowIndex)
        {
            double value = 0;
            if (_isVertical)
            {
                if (rowIndex >= 0 && rowIndex < _t.Rows.Count && columnIndex >= 0 && columnIndex < _t.Columns.Count)
                {
                    double.TryParse(_t.Rows[rowIndex][columnIndex].ToString(), out value);
                }
            }
            else
            {
                if (rowIndex >= 0 && rowIndex < _t.Columns.Count - 1 && columnIndex >= 0 && columnIndex < _t.Rows.Count)
                {
                    double.TryParse(_t.Rows[columnIndex + 1][rowIndex].ToString(), out value);
                }
            }
            return value;
        }
        #endregion


    }
    public delegate bool ManualDataTableCellValidate(int columnIndex, int rowIndex, object obj);

}
