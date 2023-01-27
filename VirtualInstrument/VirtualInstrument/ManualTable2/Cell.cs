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
    public partial class Cell : UserControl
    {
        public Cell()
        {
            InitializeComponent();
            LoadDefault();
            label1.DoubleClick += new EventHandler(label1_DoubleClick);
            textBox1.LostFocus += new EventHandler(textBox1_LostFocus);
            textBox1.DoubleClick += new EventHandler(textBox1_DoubleClick);
            timer1.Start();
        }


        void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (!_designMode)
            {
                textBox1.Visible = false;
                label1.Visible = true;
            }
            else
            {
                OnCellSelected(new CellDesignModeEventArgs(_columnIndex, _rowIndex));
            }
        }

        void textBox1_LostFocus(object sender, EventArgs e)
        {
                textBox1.Visible = false;
                label1.Visible = true;
        }

        void label1_DoubleClick(object sender, EventArgs e)
        {
            if (!_designMode)
            {
                if (!ReadOnly)
                {
                    label1.Visible = false;
                    textBox1.Visible = true;
                }
            }
            else
            {
                OnCellSelected(new CellDesignModeEventArgs(_columnIndex, _rowIndex));
            }

        }
        #region Props
        bool _autoChange;
        string _variableName;
        string _expression;
        bool _needRefreshValue = false,_needRefreshText = false;
        string _text = "";
        double _value = 0;
        double _min = 0, _max =0;
        int _sectionIndex = -1,_columnIndex =-1, _rowIndex = -1;
        bool _designMode = false;
        bool _changedBySelect = false;
        DataAnalysis.StatisticTypeDefine _statisticType = DataAnalysis.StatisticTypeDefine.Average;
        int _decimalCount = 4;
        public bool ChangedBySelect
        {
            get { return _changedBySelect; }
            set { _changedBySelect = value; }
        }
        public bool IsDesignMode
        {
            get { return _designMode; }
            set { _designMode = value; SetImage(); }
        }
        public string VariableName
        {
            get { return _variableName; }
            set { _variableName = value; }
        }
        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }
        public DataAnalysis.StatisticTypeDefine StatisticType
        {
            get { return _statisticType; }
            set { _statisticType = value; }
        }
        public Color CellBackColor
        {
            get { return label1.BackColor; }
            set { label1.BackColor = value; textBox1.BackColor = value; }
        }
        public Color CellForeColor
        {
            get { return label1.ForeColor; }
            set { label1.ForeColor = value; textBox1.ForeColor = value; }
        }

        public string CellText
        {
            get { return _text; }
            set 
            {
                _text = value;
                _needRefreshText = true;
            }
        }
        public int DecimalCount
        {
            get { return _decimalCount; }
            set { _decimalCount = value >=0?value:0; }
        }
        public double Value
        {
            get
            {
                //double value = 0;
                //double.TryParse(label1.Text, out value);
                //return value;
                return _value;
                     
            }
            set
            {
                _value = Math.Round(value,_decimalCount );
                //label1.Text = _value.ToString();
                _needRefreshValue = true;
            }
        }
        public int SectionIndex
        {
            get
            {
                return _sectionIndex;
            }
            set 
            { 
                _sectionIndex = value; 
            }
        }
        public int RowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }
        public int ColumnIndex
        {
            get { return _columnIndex; }
            set { _columnIndex = value; }
        }
        public ContentAlignment CellTextAlignment
        {
            get
            {
                return label1.TextAlign;
            }
            set
            {
                label1.TextAlign = value;
            }
        }
        public float FontSizeF
        {
            get { return label1.Font.Size; }
            set 
            {
                float sizef = value;
                if (sizef <= 0f)
                {
                    sizef = 9f;
                }
                label1.Font = new Font(label1.Font.FontFamily, sizef); textBox1.Font = new Font(label1.Font.FontFamily, sizef); }
        }
        public bool ReadOnly
        {
            get { return textBox1.ReadOnly; }
            set { textBox1.ReadOnly = value; }
        }
        public bool AutoChange
        {
            get { return _autoChange; }
            set { _autoChange = value; }
        }
        public double Min
        {
            get { return _min; }
            set { _min= value; }
        }
        public double Max
        {
            get { return _max; }
            set { _max = value; }
        }
        bool Modified
        {
            get
            {
                return !string.IsNullOrEmpty(VariableName) || !string.IsNullOrEmpty(Expression) || !string.IsNullOrEmpty(CellText);
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            textBox1.BackColor = label1.BackColor;
            textBox1.ForeColor = label1.ForeColor;
            textBox1.Text = label1.Text;
            CellTextAlignment = ContentAlignment.MiddleCenter;
            //FontSizeF = label1.Height *0.6f;
            VariableName = "";
            Expression = "";
            CellText = "";
            ReadOnly = true;
            _autoChange = true;
            SetImage();
        }
        public void Initialize(string text, Color backColor, Color foreColor,ContentAlignment alignment, float fontSizef, bool readOnly,bool autoChange,bool changedBySelect,int decimalCount)
        {
            CellText = text;
            CellBackColor = backColor;
            CellForeColor = foreColor;
            CellTextAlignment = alignment;
            FontSizeF = fontSizef;
            ReadOnly = readOnly;
            AutoChange = autoChange;
            _changedBySelect = changedBySelect;
            DecimalCount = decimalCount;
            SetImage();
        }
        public void Initialize(Color backColor, Color foreColor, ContentAlignment alignment, float fontSizef, bool readOnly, bool autoChange, bool changedBySelect)
        {
            CellText = "";
            CellBackColor = backColor;
            CellForeColor = foreColor;
            CellTextAlignment = alignment;
            FontSizeF = fontSizef;
            ReadOnly = readOnly;
            AutoChange = autoChange;
            _changedBySelect = changedBySelect;
            SetImage();
        }
        public void Initialize(Color backColor, Color foreColor, bool readOnly, bool autoChange)
        {
            Initialize(backColor, foreColor, ContentAlignment.MiddleRight,/* label1.Height * 0.6f*/9f, readOnly, autoChange,false);
        }
        public void Clear()
        {
            label1.Text = "";
            SetImage();
        }
        void SetImage()
        {
            if (IsDesignMode)
            {
                if (Modified)
                {
                    label1.ImageKey = "fill";
                }
                else
                {
                    label1.ImageKey = "empty";
                }
            }
            else
            {
                label1.ImageKey = "";
            }
        }
        #endregion

        #region Serialize
        
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Text", CellText);
            keyValue.Add("BackColor", KeyValue.KeyValue.ColorToString(CellBackColor));
            keyValue.Add("ForeColor", KeyValue.KeyValue.ColorToString(CellForeColor));
            keyValue.Add("Alignment", KeyValue.KeyValue.ContentAlignmentToString(CellTextAlignment));
            keyValue.Add("FontSizeF", FontSizeF.ToString());
            keyValue.Add("ReadOnly", ReadOnly.ToString());
            keyValue.Add("AutoChange", AutoChange.ToString());
            keyValue.Add("StatisticType", ((byte)_statisticType).ToString());
            keyValue.Add("VariableName", _variableName);
            keyValue.Add("Expression", _expression);
            keyValue.Add("SectionIndex", _sectionIndex.ToString());
            keyValue.Add("ColumnIndex", _columnIndex.ToString());
            keyValue.Add("RowIndex", _rowIndex.ToString());
            keyValue.Add("ChangedBySelect", _changedBySelect.ToString());
            keyValue.Add("DecimalCount", DecimalCount.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            CellText = keyValue.GetValueByKey("Text");
            CellBackColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("BackColor"));
            CellForeColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("ForeColor"));
            CellTextAlignment = KeyValue.KeyValue.ParseContentAlignment(keyValue.GetValueByKey("Alignment"));
            float sizef;
            float.TryParse(keyValue.GetValueByKey("FontSizeF"), out sizef);
            FontSizeF = sizef;
            bool readOnly,autoChange;
            bool.TryParse(keyValue.GetValueByKey("ReadOnly"), out readOnly);
            ReadOnly = readOnly;
            bool.TryParse(keyValue.GetValueByKey("AutoChange") ,out autoChange );
            AutoChange = autoChange;
            byte type;
            byte.TryParse(keyValue.GetValueByKey("StatisticType"), out type);
            _statisticType = (DataAnalysis.StatisticTypeDefine)type;
            _variableName = keyValue.GetValueByKey("VariableName");
            _expression = keyValue.GetValueByKey("Expression");
            int.TryParse(keyValue.GetValueByKey("SectionIndex"), out _sectionIndex);
            int.TryParse(keyValue.GetValueByKey("ColumnIndex"), out _columnIndex);
            int.TryParse(keyValue.GetValueByKey("RowIndex"), out _rowIndex);
            bool.TryParse(keyValue.GetValueByKey("ChangedBySelect"), out _changedBySelect);
            int.TryParse(keyValue.GetValueByKey("DecimalCount"), out _decimalCount);
            SetImage();
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshValue)
            {
                _needRefreshValue = false;
                label1.Text =Math.Round( _value,_decimalCount) .ToString("F"+_decimalCount.ToString());
                textBox1.Text = label1.Text;
            }
            if (_needRefreshText)
            {
                _needRefreshText = false;
                label1.Text = _text; textBox1.Text = _text;
                double cellValue = 0;
                double.TryParse(label1.Text, out cellValue);
                _value = cellValue;

            }
        }
        #region Events
        public event CellDesignModeHandler CellSelected = null;
        protected void OnCellSelected(CellDesignModeEventArgs e)
        {
            if (CellSelected != null)
            {
                CellSelected(this, e);
            }
        }
        #endregion
    }
    public class CellDesignModeEventArgs : EventArgs
    {
        public CellDesignModeEventArgs(int columnIndex, int rowIndex)
        {
            _columnIndex = columnIndex;
            _rowIndex = rowIndex;
        }

        int _columnIndex, _rowIndex;
        public int ColumnIndex
        {
            get { return _columnIndex; }
        }
        public int RowIndex
        {
            get { return _rowIndex; }
        }
    }
    public delegate void CellDesignModeHandler(object sender,CellDesignModeEventArgs e );
}
