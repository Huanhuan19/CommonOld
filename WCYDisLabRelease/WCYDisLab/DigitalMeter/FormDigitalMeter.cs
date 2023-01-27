using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.DigitalMeter
{
    public partial class FormDigitalMeter : Form
    {
        public FormDigitalMeter(Classes.DataEngine dataEngine)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            _columnName = "";
            
            InitDataEngine();
        }
        public FormDigitalMeter(Classes.DataEngine dataEngine, string value)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            _columnName = "";
            this.StartPosition = FormStartPosition.Manual;
            Parse(value);
            InitDataEngine();
        }
        public FormDigitalMeter(Classes.DataEngine dataEngine, string name, bool isSensor)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            _columnName = name;
            InitDataEngine();
            Initialize(name);
        }

        #region Props
        Classes.DataEngine _dataEngine;
        string _columnName;
        #endregion

        #region Methods

        void InitDataEngine()
        {
            _dataEngine.DataManager.DataCollectionEvent += new TDataManager.DataCollectionHandler(DtManager_DataCollectionEvent);
            _dataEngine.DataManager.ReplayEvent += new TDataManager.ReplayHandler(DataManager_ReplayEvent);
            this.FormClosing += new FormClosingEventHandler(FormDigitalMeter_FormClosing);
        }
        double GetReplayValue(string sectionName, int timeIndex)
        {
            double value = 0;
            TDataManager.DataSection section = _dataEngine.DataManager.GetDataSectionByName(sectionName);
            if (section != null)
            {
                TDataManager.DataElement dataElement = section.GetDataElementByTimeIndex(_columnName, timeIndex);
                if (dataElement != null)
                {
                    value = dataElement.Value;
                }
            }
            return value;

        }


        void DataManager_ReplayEvent(object sender, TDataManager.ReplayEventArgs e)
        {
            switch (e.ReplayStatus)
            {
                case TDataManager.ReplayStatus.End:
                    break;
                case TDataManager.ReplayStatus.Pause:
                    break;
                case TDataManager.ReplayStatus.Playing:
                    this.digitalMeter1.Value = GetReplayValue(e.SectionName, e.TimeIndex);
                    break;
                case TDataManager.ReplayStatus.Start:
                    break;
                case TDataManager.ReplayStatus.SwitchSection:
                    break;
            }

        }

        void FormDigitalMeter_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dataEngine.DataManager.DataCollectionEvent -= DtManager_DataCollectionEvent;
            _dataEngine.DataManager.ReplayEvent -= DataManager_ReplayEvent;
        }
        void Initialize(string name)
        {
            _columnName = name;
            SetCaptionUnit();
        }

        void DtManager_DataCollectionEvent(object sender, TDataManager.DataCollectionEventArgs e)
        {
            if (string.Compare(_columnName, TDataManager.DataManager.TIMEINDEX_NAME) == 0)
            {
                digitalMeter1.Value = e.DataElement.TimeIndex;
            }
            else if (string.Compare(_columnName, TDataManager.DataManager.TIMESTAMP_NAME) == 0)
            {
                digitalMeter1.Value = e.DataElement.TimeStamp;
            }
            else if (string.Compare(e.ColumnName, _columnName) == 0)
            {
                digitalMeter1.Value = e.DataElement.Value;
            }
        }
        void SetCaptionUnit()
        {
            TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.GetDataStoreByColumnName(_columnName);
            if (dataStore != null)
            {
                digitalMeter1.Caption = dataStore.DataProps.Caption;
                digitalMeter1.Unit = dataStore.DataProps.Unit;
                digitalMeter1.DecimalCount = dataStore.DataProps.Decimal;
            }

        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", Name);
            keyValue.Add("Caption", Text);
            keyValue.Add("Bounds", Classes.PublicMethods.Bounds2Str(Bounds));
            keyValue.Add("ColumnName", _columnName);
            keyValue.Add("DigitalMeter", digitalMeter1.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Name = keyValue.GetValueByKey("Name");
            Text = keyValue.GetValueByKey("Caption");
            Bounds = Classes.PublicMethods.RectangleParse(keyValue.GetValueByKey("Bounds"));
            _columnName = keyValue.GetValueByKey("ColumnName");
            digitalMeter1.Parse(keyValue.GetValueByKey("DigitalMeter"));
        }
        #endregion

        private void toolStripButton_props_Click(object sender, EventArgs e)
        {
            FormVariablePicker f = new FormVariablePicker (_dataEngine,false, _columnName,new List<string>());
            if (f.ShowDialog() == DialogResult.OK)
            {
                _columnName = f.SelectedVariableName;
                SetCaptionUnit();
            }


        }

        private void toolStripButton_backColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = digitalMeter1.MeterBackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                digitalMeter1.MeterBackColor = colorDialog1.Color;
                //InitDataEngine();
            }

        }

        private void toolStripButton_foreColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = digitalMeter1.DigitalColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                digitalMeter1.DigitalColor = colorDialog1.Color;
                
                //InitDataEngine();
            }

        }
    }
}
