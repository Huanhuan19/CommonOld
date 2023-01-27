using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualInstrument
{
    public partial class Table : UserControl
    {
        public Table()
        {
            InitializeComponent();
            LoadDefault();
            timer1.Start();
            _needHideWindowTable = true;
        }

        #region Props
        List<Classes.ColumnDefine> _columns = new List<VirtualInstrument.Classes.ColumnDefine>();
        float _zoomFactor,_sizef = 9f;
        bool _merge;
        string _sectionName;
        string _caption;
        DataTable _t = new DataTable();
        DataTable _windowTable = new DataTable();
        public string TableCaption
        {
            get { return _caption; }
            set { _caption = value; _needRefreshCaption = true; }
        }
        public string TableTitle
        {
            get { return label_title.Text; }
            set { label_title.Text = value; }
        }
        public List<Classes.ColumnDefine> Columns
        {
            get { return _columns; }
        }
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
        public bool Merge
        {
            get { return _merge; }
            set 
            { 
                _merge = value; 
            }
        }
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        //public int Count
        //{
        //    get { return _t.Rows.Count; }
        //}
        #endregion

        #region Variables
        bool _needRefreshAll = false, _needZoom = false, _needMarkLine = false,_needRefreshCaption = false,_needShowWindowTable = false,_needHideWindowTable = false;
        public static float DEFAULT_FONT_SIZE = 9f;
        Classes.TableRowRecord[] _records = new VirtualInstrument.Classes.TableRowRecord[100];
        int _writeIndex, _readIndex;
        int _markLineIndex = -1;
        #endregion

        #region Methods
        void LoadDefault()
        {
            label_caption.Text = "";
            label_title.Text = "";
            _sectionName = "";
            _columns.Clear();
            _zoomFactor = 1f;
            _merge = true;
            //_t.Columns.Clear();
            //_t.Rows.Clear();
            _needRefreshAll = false;
        }
        public void Initialize(string sectionName,string title, string caption, float zoomFactor, bool merge, List<Classes.ColumnDefine> columns)
        {
            _sectionName = sectionName;
            TableTitle = title;
            TableCaption = caption;
            _zoomFactor = zoomFactor;
            _merge = merge;
            _columns.Clear();
            _columns.AddRange(columns);
            _needRefreshAll = true;
        }
        void Zoom()
        {
            float size = DEFAULT_FONT_SIZE * _zoomFactor;
            if (size <= 0)
            {
                size = 9f;
            }
            int lineHeight = (int)Math.Round(size * 3, 0);
            
            dataGridView1.RowTemplate.Height = lineHeight;
            dataGridView1.RowTemplate.DefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView1.ColumnHeadersHeight = lineHeight;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView2.RowTemplate.Height = lineHeight;
            dataGridView2.RowTemplate.DefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView2.ColumnHeadersHeight = lineHeight;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("TimesNewRoman", size);
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
            dataGridView1.RowTemplate.Height = lineHeight;
            dataGridView1.RowTemplate.DefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView1.ColumnHeadersHeight = lineHeight;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView2.RowTemplate.Height = lineHeight;
            dataGridView2.RowTemplate.DefaultCellStyle.Font = new Font("TimesNewRoman", size);
            dataGridView2.ColumnHeadersHeight = lineHeight;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("TimesNewRoman", size);
            _needRefreshAll = true;
        }
        public void ShowWindowTable()
        {
            _needShowWindowTable = true;
        }
        public void HideWindowTable()
        {
            _needHideWindowTable = true;
        }
        public void SetDataTable(DataTable t)
        {
            _sectionName = t.TableName;
            
            _t = t;
            Prepair(_t.Rows.Count);

            _needRefreshAll = true;
        }

        void CreateWindowTable()
        {
            _windowTable = _t.Clone();
            dataGridView2.DataSource = _windowTable;
            _windowTable.Rows.Add(_windowTable.NewRow());
        }
        public void Prepair(int count)
        {
            _writeIndex = -1;
            _readIndex = -1;
            _records = new VirtualInstrument.Classes.TableRowRecord[count*_columns.Count];
            for (int i = 0; i < _records.Length; i++)
            {
                _records[i] = new VirtualInstrument.Classes.TableRowRecord();
            }
        }
        void RefreshColumn()
        {
            dataGridView1.DataSource = _t;

            if (_t != null)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].HeaderText = _t.Columns[i].Caption;
                    dataGridView1.Columns[i].Visible = ColumnContains(dataGridView1.Columns[i].Name);
                }
            }
            if (_windowTable != null)
            {
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].HeaderText = _windowTable.Columns[i].Caption;
                    dataGridView2.Columns[i].Visible = ColumnContains(dataGridView2.Columns[i].Name);
                }
            }
            CreateWindowTable();

        }
        public void AddColumnDefine(string columnName, string caption, int decimalCount)
        {
            _columns.Add(new VirtualInstrument.Classes.ColumnDefine(columnName, caption, decimalCount));
        }
        public void RemoveColumnDefine(string columnName)
        {
            for (int i = _columns.Count - 1; i >= 0; i--)
            {
                if (string.Equals(_columns[i].ColumnName, columnName))
                {
                    _columns.RemoveAt(i);
                }
            }
        }
        public void ClearColumnDefines()
        {
            _columns.Clear();
        }
        public void SetColumnDefines(List<Classes.ColumnDefine> columnDefines)
        {
            _columns.Clear();
            _columns.AddRange(columnDefines);
            _needRefreshAll = true;
        }
        public void AddValue(string sectionName, string columnName, int timeIndex, double value)
        {
            _writeIndex++;
            if (_writeIndex >= 0 && _writeIndex < _records.Length)
            {
                _records[_writeIndex].Initialize(sectionName, columnName, timeIndex, value);
            }
        }
        public void SetValue(string sectionName, string columnName, int timeIndex, double value)
        {
            if (string.Equals(_sectionName, sectionName) && _t.Columns.Contains(columnName))
            {
                if (timeIndex >= 0 && timeIndex < _t.Rows.Count)
                {
                    _t.Rows[timeIndex][columnName] = value;
                    if (_windowTable.Rows.Count <= 0)
                    {
                        _windowTable.Rows.Add(_windowTable.NewRow());
                    }
                    for (int i = 0; i < _windowTable.Columns.Count; i++)
                    {
                        if (string.Equals(_windowTable.Columns[i].ColumnName, "TimeIndex") || string.Equals(_windowTable.Columns[i].ColumnName, "TimeStamp"))
                        {
                            _windowTable.Rows[0][_windowTable.Columns[i].ColumnName] = _t.Rows[timeIndex][_windowTable.Columns[i].ColumnName];
                        }
                    }
                    _windowTable.Rows[0][columnName] = value;
                }
            }
        }
        public void NeedMarkLine(int index)
        {
            _markLineIndex = index;
            _needMarkLine = true;
        }
        void MarkLine()
        {
            if (_markLineIndex >= 0 && _markLineIndex < dataGridView1.Rows.Count)
            {
                dataGridView1.Rows[_markLineIndex].Selected = true;
                if (_windowTable.Rows.Count <= 0)
                {
                    _windowTable.Rows.Add(_windowTable.NewRow());
                }
                for (int i = 0; i < _windowTable.Columns.Count; i++)
                {
                    if (_t.Columns.Contains(_windowTable.Columns[i].ColumnName))
                    {
                        _windowTable.Rows[0][_windowTable.Columns[i].ColumnName] = _t.Rows[_markLineIndex][_windowTable.Columns[i].ColumnName];
                    }
                }
            }
        }
        Classes.ColumnDefine GetColumnDefine(string name)
        {
            Classes.ColumnDefine column = new VirtualInstrument.Classes.ColumnDefine();
            for (int i = 0; i < _columns.Count; i++)
            {
                if (string.Equals(_columns[i].ColumnName, name))
                {
                    column = _columns[i];
                    break;
                }
            }
            return column;
        }
        bool TableContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _t.Columns.Count; i++)
            {
                if (string.Equals(_t.Columns[i].ColumnName, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        bool ColumnContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _columns.Count; i++)
            {
                if (string.Equals(_columns[i].ColumnName, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        #endregion

        #region Serialize
        string Columns2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _columns.Count; i++)
            {
                keyValue.Add(i.ToString(), _columns[i].ToString());
            }
            return keyValue.ToString();
        }
        void Str2Columns(string value)
        {
            _columns.Clear();
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            for (int i = 0; i < keyValue.Count; i++)
            {
                _columns.Add(new Classes.ColumnDefine( keyValue.GetValueByKey(i.ToString())));
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("TableTitle", TableTitle);
            keyValue.Add("TableCaption", TableCaption);
            keyValue.Add("SectionName", _sectionName);
            keyValue.Add("Columns", Columns2Str());
            keyValue.Add("ZoomFactor", _zoomFactor.ToString());
            keyValue.Add("Merge", _merge.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            TableTitle = keyValue.GetValueByKey("TableTitle");
            TableCaption = keyValue.GetValueByKey("TableCaption");
            _sectionName = keyValue.GetValueByKey("SectionName");
            Str2Columns(keyValue.GetValueByKey("Columns"));
            float.TryParse(keyValue.GetValueByKey("ZoomFactor"), out _zoomFactor);
            bool.TryParse(keyValue.GetValueByKey("Merge"), out _merge);
            _needRefreshAll = true;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshAll)
            {
                _needRefreshAll = false;
                RefreshColumn();
                
            }
            if (_needZoom)
            {
                _needZoom = false;
                Zoom();
            }
            if (_readIndex < _writeIndex)
            {
                int count = 0;
                while (_readIndex < _writeIndex && count < 100)
                {
                    _readIndex++;
                    if (_readIndex <= _writeIndex && _readIndex >= 0 && _readIndex < _records.Length)
                    {
                        SetValue(_records[_readIndex].SectionName, _records[_readIndex].ColumnName, _records[_readIndex].TimeIndex, _records[_readIndex].Value);
                        count++;
                    }
                }
            } 
            if (_needMarkLine)
            {
                _needMarkLine = false;
                MarkLine();
            }
            if (_needRefreshCaption)
            {
                _needRefreshCaption = false;
                label_caption.Text = _caption;

            }
            if (_needHideWindowTable)
            {
                _needHideWindowTable = false;
                panel2.Visible = false;
            }
            if (_needShowWindowTable)
            {
                _needShowWindowTable = false;
                panel2.Visible = true;
            }
        }
    }
}
