using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.Meter
{
    public partial class FormMeter : Form
    {
        public FormMeter(Classes.DataEngine dataEngine)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            InitDataEngine();
            FillList();
        }
        public FormMeter(Classes.DataEngine dataEngine, string value)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            InitDataEngine();
            this.StartPosition = FormStartPosition.Manual;
            Parse(value);

            FillList();
        }
        public FormMeter(Classes.DataEngine dataEngine, string name, bool isSensor)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            InitDataEngine();
            Initialize(name, isSensor);
            FillList();
        }
        #region Props
        Classes.DataEngine _dataEngine;
        string _columnName = "";
        #endregion

        #region Methods

        void FormMeter_SizeChanged(object sender, EventArgs e)
        {
            float meterRate = 194f / 272f, windowRate = 210f / 335f;
            if ((float)(this.Width / this.Height) > windowRate)
            {
                float meterHeightf = this.Height * 272f / 335f;
                float meterWidthf = meterHeightf * meterRate;
                this.meter1.Size = new Size((int)Math.Round(meterWidthf, 0), (int)Math.Round(meterHeightf, 0));
            }
            else
            {
                float meterWidthf = this.Width * 194f / 210f;
                float meterHeightf = meterWidthf / meterRate;
                this.meter1.Size = new Size((int)Math.Round(meterWidthf, 0), (int)Math.Round(meterHeightf, 0));
            }
        }
        void InitDataEngine()
        {
            _dataEngine.DataManager.DataCollectionEvent += new TDataManager.DataCollectionHandler(DtManager_DataCollectionEvent);
            _dataEngine.DataManager.Replay.ReplayEvent += new TDataManager.ReplayHandler(Replay_ReplayEvent);
            this.FormClosing += new FormClosingEventHandler(FormMeter_FormClosing);
        }

        void Replay_ReplayEvent(object sender, TDataManager.ReplayEventArgs e)
        {
            switch (e.ReplayStatus)
            {
                case TDataManager.ReplayStatus.End:
                    break;
                case TDataManager.ReplayStatus.Pause:
                    break;
                case TDataManager.ReplayStatus.Playing:
                    meter1.Value = GetReplayValue(e.SectionName, e.TimeIndex);
                    break;
                case TDataManager.ReplayStatus.Start:
                    break;
                case TDataManager.ReplayStatus.SwitchSection:
                    break;
            }
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
        void FormMeter_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dataEngine.DataManager.DataCollectionEvent -= DtManager_DataCollectionEvent;
        }

        void DtManager_DataCollectionEvent(object sender, TDataManager.DataCollectionEventArgs e)
        {
            if (string.Compare(_columnName, TDataManager.DataManager.TIMEINDEX_NAME) == 0)
            {
                meter1.Value = e.DataElement.TimeIndex;
            }
            else if (string.Compare(_columnName, TDataManager.DataManager.TIMESTAMP_NAME) == 0)
            {
                meter1.Value = e.DataElement.TimeStamp;
            }
            else if (string.Compare(e.ColumnName, _columnName) == 0)
            {
                meter1.Value = e.DataElement.Value;
            }
        }
        void Initialize(string name, bool isSensor)
        {
            TDataManager.DataStore dataStore = null;
            if (isSensor)
            {
                dataStore = _dataEngine.CurrentDataSection.GetSensorByName(name);
            }
            else
            {
                dataStore = _dataEngine.CurrentDataSection.GetDataStoreByName(name);
            }
            if (dataStore != null)
            {
                _columnName = dataStore.DataProps.Name;
                FillList();
            }

        }
        void FillList()
        {
            TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.GetDataStoreByColumnName(_columnName);
            if (dataStore != null)
            {
                meter1.Initialize(dataStore.DataProps.Caption, dataStore.DataProps.Unit, dataStore.DataProps.MaxValue, dataStore.DataProps.MinValue, true, System.Drawing.SystemColors.Control);
            }
            else
            {
                meter1.Initialize("", "", 100, 0, true, System.Drawing.SystemColors.Window);
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
            keyValue.Add("Meter", meter1.ToString());
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
            meter1.Parse(keyValue.GetValueByKey("Meter"));
        }

        #endregion

        private void toolStripButton_props_Click(object sender, EventArgs e)
        {
            FormVariablePicker f = new FormVariablePicker(_dataEngine,false, _columnName,new List<string>());
            if (f.ShowDialog() == DialogResult.OK)
            {
                _columnName = f.SelectedVariableName;
                FillList();
            }

        }

        private void toolStripButton_caption_Click(object sender, EventArgs e)
        {
            FormInputText f = new FormInputText(meter1.Caption);
            if (f.ShowDialog() == DialogResult.OK)
            {
                meter1.Caption = f.SelectedText;
            }

        }

        private void toolStripButton_scaleRange_Click(object sender, EventArgs e)
        {
            FormInputScaleRange f = new FormInputScaleRange(meter1.MinValue, meter1.MaxValue, meter1.ScaleAuto);
            if (f.ShowDialog() == DialogResult.OK)
            {
                meter1.MaxValue = f.SelectedMax;
                meter1.MinValue = f.SelectedMin;
                meter1.ScaleAuto = f.SelectedAutoScale;
            }

        }
    }
}
