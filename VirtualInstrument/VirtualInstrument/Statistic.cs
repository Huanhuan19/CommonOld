using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;

namespace VirtualInstrument
{
    public partial class Statistic : UserControl
    {
        public Statistic()
        {
            InitializeComponent();
            timer1.Start();
            LoadDefault();
            this.dataGridView_x.SelectionChanged += new EventHandler(dataGridView_x_SelectionChanged);
            this.dataGridView_y.SelectionChanged += new EventHandler(dataGridView_y_SelectionChanged);
        }
        
        #region Props
        ResourceManager _resourceManager = new ResourceManager("VirtualInstrument.Statistic", Assembly.GetExecutingAssembly());
        DataTable _xTable,_yTable;
        string _xCurrentColumnName, _yCurrentColumnName;
        List<string> _yColumns = new List<string>();
        List<string> _xColumns = new List<string>();
        float _zoomFactor, _sizef = 9f;
        bool _needZoom = false;
        public static float DEFAULT_FONT_SIZE = 9f;

        public float ZoomFactor
        {
            get { return _zoomFactor; }
            set { _zoomFactor = value; _needZoom = true; }
        }
        public float FontSizeF
        {
            get
            {
                return _sizef;
                //return dataGridView1.RowTemplate.DefaultCellStyle.Font.Size; 
            }
            set
            {
                _sizef = value;
                Zoom(value);
            }
        }

        public List<string> YColumns
        {
            get { return _yColumns; }
        }
        public List<string> XColumns
        {
            get { return _xColumns; }
        }
        public string XColumnCurrentColumnName
        {
            get { return _xCurrentColumnName; }
            set { _xCurrentColumnName = value; }
        }
        public string YColumnCurrentColumnName
        {
            get { return _yCurrentColumnName; }
            set { _yCurrentColumnName = value; }
        }
        public DataTable XTable
        {
            get { return _xTable; }
            set { _xTable = value; _needRefreshAll = true; }
        }
        public DataTable YTable
        {
            get { return _yTable; }
            set { _yTable = value; _needRefreshAll = true; }
        }
        public Graph Graph
        {
            get { return graph1; }
        }
        //public string GraphCaption
        //{
        //    get { return zedGraphControl1.GraphPane.Title.Text; }
        //    set { zedGraphControl1.GraphPane.Title.Text = value; _needRefreshAll = true; }
        //}
        #endregion

        #region Methods
        void LoadDefault()
        {
            _yColumns.Clear();
            _xColumns.Clear();
            _xCurrentColumnName = "";
            _yCurrentColumnName = "";
            graph1.XAxis.AutoScale = true;
            graph1.YAxis.AutoScale = true;
            graph1.NeedSyncAxis();
        }
        public void Initialize(List<string> yColumns, List<string> xColumns, string xCurrentColumnName, string yCurrentColumnName)
        {
            _yColumns.Clear();
            _xColumns.Clear();
            _xColumns.AddRange(xColumns);
            _yColumns.AddRange(yColumns);
            _xCurrentColumnName = xCurrentColumnName;
            _yCurrentColumnName = yCurrentColumnName;
            _needMask = true;
        }
        public void Initialize(List<string> yColumns, List<string> xColumns)
        {
            _yColumns.Clear();
            _xColumns.Clear();
            _xColumns.AddRange(xColumns);
            _yColumns.AddRange(yColumns);
            _needMask = true;
        }
        public void NeedRefreshAll()
        {
            _needRefreshAll = true;
        }
        public void NeedMask()
        {
            _needMask = true;
        }
        void Zoom()
        {
            float size = DEFAULT_FONT_SIZE * _zoomFactor;
            if (size <= 0)
            {
                size = 9f;
            }
            int lineHeight = (int)Math.Round(size * 3, 0);

            dataGridView_x.RowTemplate.Height = lineHeight;
            dataGridView_x.RowTemplate.DefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView_x.ColumnHeadersHeight = lineHeight;
            dataGridView_x.ColumnHeadersDefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView_y.RowTemplate.Height = lineHeight;
            dataGridView_y.RowTemplate.DefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView_y.ColumnHeadersHeight = lineHeight;
            dataGridView_y.ColumnHeadersDefaultCellStyle.Font = new Font("TimesNewRoman", size);
            _needRefreshAll = true;
        }
        void Zoom(float fontSizef)
        {
            float size = fontSizef;
            if (size <= 0)
            {
                size = 9f;
            }
            int lineHeight = (int)Math.Round(size * 3, 0);
            dataGridView_x.RowTemplate.Height = lineHeight;
            dataGridView_x.RowTemplate.DefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView_x.ColumnHeadersHeight = lineHeight;
            dataGridView_x.ColumnHeadersDefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView_y.RowTemplate.Height = lineHeight;
            dataGridView_y.RowTemplate.DefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView_y.ColumnHeadersHeight = lineHeight;
            dataGridView_y.ColumnHeadersDefaultCellStyle.Font = new Font("TimesNewRoman", size);
            _needRefreshAll = true;
        }

        void MaskColumns()
        {
            for (int i = 0; i < dataGridView_x.Columns.Count; i++)
            {
                dataGridView_x.Columns[i].Visible = _xColumns.Contains(dataGridView_x.Columns[i].Name);
                if (string.Equals(dataGridView_x.Columns[i].Name, _xCurrentColumnName))
                {
                    dataGridView_x.Columns[i].Selected = true;
                }
            }
            for (int i = 0; i < dataGridView_y.Columns.Count; i++)
            {
                dataGridView_y.Columns[i].Visible = _yColumns.Contains(dataGridView_y.Columns[i].Name);
                if (string.Equals(dataGridView_y.Columns[i].Name, _yCurrentColumnName))
                {
                    dataGridView_y.Columns[i].Selected = true;
                }
            }
        }
        void RefreshTable()
        {

            dataGridView_x.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView_x.DataSource = _xTable;
            
            for (int i = 0; i < dataGridView_x.Columns.Count; i++)
            {
                dataGridView_x.Columns[i].HeaderText = _xTable.Columns[dataGridView_x.Columns[i].Name].Caption;
                //dataGridView_x.Columns[i].Visible = _xColumns.Contains(dataGridView_x.Columns[i].Name);
                dataGridView_x.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (string.Equals(dataGridView_x.Columns[i].Name, _xCurrentColumnName))
                {
                    dataGridView_x.Columns[i].Selected = true;
                }
            }
            dataGridView_x.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            dataGridView_y.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView_y.DataSource = _yTable;
            for (int i = 0; i < dataGridView_y.Columns.Count; i++)
            {
                dataGridView_y.Columns[i].HeaderText = _yTable.Columns[dataGridView_y.Columns[i].Name].Caption;
                //dataGridView_y.Columns[i].Visible = _yColumns.Contains(dataGridView_y.Columns[i].Name);
                dataGridView_y.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (string.Equals(dataGridView_y.Columns[i].Name, _yCurrentColumnName))
                {
                    dataGridView_y.Columns[i].Selected = true;
                }
            }
            dataGridView_y.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            MaskColumns();
        }
        void RefreshGraph()
        {
            graph1.ClearFitLineExtands(true);
            graph1.ClearFitLines(true);
            graph1.ClearLabels(true);
            graph1.ClearLines(true);
            graph1.SectionClear();
            if (!string.IsNullOrEmpty(_xCurrentColumnName) && !string.IsNullOrEmpty(_yCurrentColumnName) 
                && _xTable.Columns.Contains( _xCurrentColumnName ) && _yTable.Columns.Contains(_yCurrentColumnName ))
            {

                graph1.AddCurve(_yCurrentColumnName, _yCurrentColumnName, 4, Classes.PublicMethods.GetNewColor(), true, true, Graph.CURRENT_LINE_WIDTH, false, true, 1f, true);
                graph1.CloneSectionFromTemplete("Statistic", _resourceManager.GetString("Statistic" ), true);
                graph1.XAxis.Caption = _xTable.Columns[_xCurrentColumnName].Caption;
                graph1.YAxis.Caption = _yTable.Columns[_yCurrentColumnName].Caption;
                
                for (int i = 0; i < _yTable.Rows.Count; i++)
                {
                    if (i >= 0 && i < _xTable.Rows.Count)
                    {
                        double x, y;
                        double.TryParse( _xTable.Rows[i][_xCurrentColumnName].ToString(),out x );
                        double.TryParse( _yTable.Rows[i][_yCurrentColumnName].ToString(),out y );
                        graph1.AddPoint("Statistic", _yCurrentColumnName, x, y, _resourceManager.GetString("Statistic"));
                    }
                }
            }
        }
        void dataGridView_y_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_y.SelectedColumns.Count > 0)
            {
                _yCurrentColumnName = dataGridView_y.SelectedColumns[0].Name;
                _needRefreshGraph = true;
            }
        }

        void dataGridView_x_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_x.SelectedColumns.Count > 0)
            {
                _xCurrentColumnName = dataGridView_x.SelectedColumns[0].Name;
                _needRefreshGraph = true;
            }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshAll)
            {
                _needRefreshAll = false;
                RefreshTable();
                RefreshGraph();
            }
            if (_needRefreshGraph)
            {
                _needRefreshGraph = false;
                RefreshGraph();
            }
            if (_needMask)
            {
                _needMask = false;
                MaskColumns();
            }
            if (_needZoom)
            {
                _needZoom = false;
                Zoom();
            }

        }
        #endregion


        #region Variables
        bool _needRefreshAll = false, _needRefreshGraph = false,_needMask = false;

        #endregion

        #region Serialize
        string YColumns2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _yColumns.Count; i++)
            {
                keyValue.Add(i.ToString(), _yColumns[i]);
            }
            return keyValue.ToString();
        }
        void ParseYColumns(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _yColumns.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _yColumns.Add(keyValue.GetValueByKey(i.ToString()));
            }
        }
        string XColumns2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _xColumns.Count; i++)
            {
                keyValue.Add(i.ToString(), _xColumns[i]);
            }
            return keyValue.ToString();
        }
        void ParseXColumns(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _xColumns.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _xColumns.Add(keyValue.GetValueByKey(i.ToString()));
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("YColumns", YColumns2Str());
            keyValue.Add("XColumns", XColumns2Str());
            keyValue.Add("XCurrentColumnName", _xCurrentColumnName);
            keyValue.Add("YCurrentColumnName", _yCurrentColumnName);
            keyValue.Add("GraphCaption", graph1.GraphCaption);
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            ParseYColumns(keyValue.GetValueByKey("YColumns"));
            ParseXColumns(keyValue.GetValueByKey("XColumns"));
            _xCurrentColumnName = keyValue.GetValueByKey("XCurrentColumnName");
            _yCurrentColumnName = keyValue.GetValueByKey("YCurrentColumnName");
            graph1.GraphCaption = keyValue.GetValueByKey("GraphCaption");
            _needRefreshAll = true;
        }
        #endregion
    }
}
