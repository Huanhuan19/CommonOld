using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualInstrument.ManualTable2
{
    public partial class ManualTable2 : UserControl
    {
        public ManualTable2()
        {
            InitializeComponent();
        }

        #region Props
        List<Cell> _columnHeaders = new List<Cell>();
        List<Cell> _rowHeaders = new List<Cell>();
        
        public int ColumnCount
        {
            get
            {
                return tableLayoutPanel1.ColumnCount;
            }
        }
        public int RowCount
        {
            get
            {
                return tableLayoutPanel1.RowCount;
            }
        }
        public bool IsDesignMode
        {
            set
            {
                for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
                {
                    (tableLayoutPanel1.Controls[i] as Cell).IsDesignMode = value;
                }
            }
        }
        #endregion

        #region Methods
        public void Clear()
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (((Cell)Controls[i]).AutoChange)
                {
                    ((Cell)Controls[i]).Clear();
                }
            }
        }
        public void InitTable(int columnCount, int rowCount)
        {
            InitTable(columnCount, rowCount, false);
        }

        public void InitTable(int columnCount, int rowCount, bool isDesignMode)
        {
            tableLayoutPanel1.SuspendLayout();
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                (control as Cell).CellSelected -= cell_CellSelected;
            }
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = columnCount;
            tableLayoutPanel1.RowCount = rowCount;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                tableLayoutPanel1.ColumnStyles[i].Width = tableLayoutPanel1.Width / tableLayoutPanel1.ColumnCount;
            }
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent));
                tableLayoutPanel1.RowStyles[i].Height = tableLayoutPanel1.Height / tableLayoutPanel1.RowCount;
            }
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    Cell cell = new Cell();
                    cell.Dock = DockStyle.Fill;
                    cell.IsDesignMode = isDesignMode;
                    cell.ColumnIndex = columnIndex;
                    cell.RowIndex = rowIndex;
                    tableLayoutPanel1.Controls.Add(cell, columnIndex, rowIndex);
                    cell.CellSelected += new CellDesignModeHandler(cell_CellSelected);
                }
            }
            tableLayoutPanel1.ResumeLayout(true);
        }

        void cell_CellSelected(object sender, CellDesignModeEventArgs e)
        {
            OnCellSelected(sender, e);
        }
        public void InitCell(int columnIndex, int rowIndex,string cellText, Color backColor, Color foreColor, ContentAlignment alignment, float fontSizef, bool readOnly, bool autoChange, string variableName, DataAnalysis.StatisticTypeDefine statisticType, int sectionIndex, string expression,bool changedBySelect,int decimalCount)
        {
            Cell cell = (Cell)tableLayoutPanel1.GetControlFromPosition(columnIndex, rowIndex);
            if (cell != null)
            {
                cell.ColumnIndex = columnIndex;
                cell.RowIndex = rowIndex;
                cell.Initialize(backColor, foreColor, alignment, fontSizef, readOnly, autoChange,changedBySelect);
                cell.CellText = cellText;
                cell.VariableName = variableName;
                cell.StatisticType = statisticType;
                cell.SectionIndex = sectionIndex;
                cell.Expression = expression;
                cell.DecimalCount = decimalCount;
            }

        }
        public void SetCellValue(int columnIndex, int rowIndex, double value)
        {
            Cell cell = (Cell)tableLayoutPanel1.GetControlFromPosition(columnIndex, rowIndex);
            if (cell != null)
            {
                cell.Value = value;
            }
        }
        public void SetCellValue(int columnIndex, int rowIndex, string value)
        {
            Cell cell = (Cell)tableLayoutPanel1.GetControlFromPosition(columnIndex, rowIndex);
            if (cell != null)
            {
                cell.CellText = value;
                
                
            }
        }
        public void SetColumnSame(Cell templeteCell, int columnIndex)
        {
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                Cell cell = (Cell)tableLayoutPanel1.GetControlFromPosition(columnIndex, i);
                if (cell != null)
                {
                    cell.Parse(templeteCell.ToString());
                }
            }
        }
        public void SetRowSame(Cell templeteCell, int rowIndex)
        {
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                Cell cell = (Cell)tableLayoutPanel1.GetControlFromPosition(i,rowIndex);
                if (cell != null)
                {
                    cell.Parse(templeteCell.ToString());
                }
            }
        }
        public double GetValue(int columnIndex, int rowIndex)
        {
            double value = 0;
            Cell cell = (Cell)tableLayoutPanel1.GetControlFromPosition(columnIndex, rowIndex);
            if (cell != null)
            {
                value = cell.Value;
            }
            return value;
        }
        public Cell GetCell(int columnIndex, int rowIndex)
        {
            Cell cell = null;
            if (columnIndex >= 0 && columnIndex < ColumnCount && rowIndex >= 0 && rowIndex < RowCount)
            {
                cell = (Cell)tableLayoutPanel1.GetControlFromPosition(columnIndex, rowIndex);
            }
            return cell;
        }
        public void RemoveSection(int sectionIndex)
        {
            foreach (Cell cell in tableLayoutPanel1.Controls)
            {
                if (cell.AutoChange && cell.SectionIndex == sectionIndex)
                {
                    cell.CellText = "";
                }
            }
        }
        #endregion

        #region Events
        public event CellDesignModeHandler CellSelected = null;
        protected void OnCellSelected(object sender, CellDesignModeEventArgs e)
        {
            if (CellSelected != null)
            {
                CellSelected(sender, e);
            }
        }
        #endregion

        #region Serialize
        string List2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                keyValue.Add(i.ToString(), ((Cell)tableLayoutPanel1.Controls[i]).ToString());
            }
            return keyValue.ToString();
        }
        void Str2List(int columnCount,int rowCount,string value)
        {
            tableLayoutPanel1.Controls.Clear();
            InitTable(columnCount, rowCount);
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            for (int i = 0; i < keyValue.Count; i++)
            {
                Cell cell = new Cell();
                cell.Parse(keyValue.GetValueByKey(i.ToString()));
                InitCell(cell.ColumnIndex, cell.RowIndex, cell.CellText,cell.CellBackColor, cell.CellForeColor, cell.CellTextAlignment, cell.FontSizeF, cell.ReadOnly, cell.AutoChange, cell.VariableName, cell.StatisticType, cell.SectionIndex, cell.Expression,cell.ChangedBySelect,cell.DecimalCount);
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("ColumnCount", ColumnCount.ToString());
            keyValue.Add("RowCount", RowCount.ToString());
            keyValue.Add("Cells", List2Str());            
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            int columnCount, rowCount;
            int.TryParse(keyValue.GetValueByKey("ColumnCount"), out columnCount);
            int.TryParse(keyValue.GetValueByKey("RowCount"), out rowCount);
            Str2List(columnCount, rowCount, keyValue.GetValueByKey("Cells"));

        }
        #endregion
    }
}
