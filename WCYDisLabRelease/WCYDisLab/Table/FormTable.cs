using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Resources;
using System.Reflection;

namespace WCYDisLab.Table
{
    public partial class FormTable : Form
    {
        public FormTable(Classes.DataEngine dataEngine)
        {
            InitializeComponent();
            timer1.Start();
            _dataEngine = dataEngine;
            InitDataEngine();
            Initialize();
            FillList();
        }
        public FormTable(Classes.DataEngine dataEngine, string value)
        {
            InitializeComponent();
            timer1.Start();
            _dataEngine = dataEngine;
            InitDataEngine();
            this.StartPosition = FormStartPosition.Manual;
            Parse(value);
            FillList();
        }
        public FormTable(Classes.DataEngine dataEngine, string name, bool isSensor)
        {
            InitializeComponent();
            timer1.Start();
            _dataEngine = dataEngine;
            InitDataEngine();
            Initialize(name, isSensor);
            FillList();
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.Table.FormTable", Assembly.GetExecutingAssembly());
        bool _needReleaseMerge = false;
        Classes.DataEngine _dataEngine;
        List<string> _sensorNames = new List<string>();
        //List<string> _constantNames = new List<string>();
        List<string> _exprNames = new List<string>();
        string _dataSectionName = "";
        bool _alwaysNew = true, _timeIndexVisible = false, _timeStampVisible = false;
        int _fontSizeRate = 0;
        VirtualInstrument.Classes.ColumnDefine TimeIndexColumn
        {
            get
            {
                return new VirtualInstrument.Classes.ColumnDefine(TDataManager.DataManager.TIMEINDEX_NAME, _dataEngine.DataManager.GetCaption(TDataManager.DataManager.TIMEINDEX_NAME), 0);

            }
        }
        VirtualInstrument.Classes.ColumnDefine TimeStampColumn
        {
            get
            {
                return new VirtualInstrument.Classes.ColumnDefine(TDataManager.DataManager.TIMESTAMP_NAME, _dataEngine.DataManager.GetCaption(TDataManager.DataManager.TIMESTAMP_NAME), 0);

            }
        }
        #endregion

        #region Methods
        void Initialize()
        {
            _timeIndexVisible = true;
            _timeStampVisible = true;
            for (int i = 0; i < _dataEngine.CurrentDataSection.Sensors.Count; i++)
            {
                _sensorNames.Add(_dataEngine.CurrentDataSection.Sensors[i].DataProps.Name);
            }
            for (int i = 0; i < _dataEngine.CurrentDataSection.DataStores.Count; i++)
            {
                this._exprNames.Add(_dataEngine.CurrentDataSection.DataStores[i].DataProps.Name);
            }
            MaskColumns();
            FillList();
        }

        void Initialize(string name, bool isSensor)
        {
            if (isSensor)
            {
                _sensorNames.Add(name);
            }
            else
            {
                _exprNames.Add(name);
            }
            TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.GetDataStoreByColumnName(name);
            if (dataStore != null)
            {
                table1.TableTitle = dataStore.DataProps.Caption + "(" + dataStore.DataProps.Unit + ")";
            }
            MaskColumns();
            FillList();
        }
        void InitDataEngine()
        {
            _dataEngine.DataManager.DataCollectionEvent += new TDataManager.DataCollectionHandler(DtManager_DataCollectionEvent);
            _dataEngine.DataManager.SectionEvent += new TDataManager.SectionEventHandler(DtManager_SectionEvent);
            _dataEngine.DataManager.ReplayEvent += new TDataManager.ReplayHandler(DataManager_ReplayEvent);
            _dataEngine.DataSource.WorkStatusChanged += new WCYDataSource.StartStopHandler(DataSource_WorkStatusChanged);
            this.FormClosing += new FormClosingEventHandler(FormTable_FormClosing);

            _timeIndexVisible = true;
            _timeStampVisible = true;
            _dataSectionName = _dataEngine.DataManager.LastDataSection.Name;
        }

        void DataSource_WorkStatusChanged(object sender, WCYDataSource.StartStopEventArgs e)
        {
            if (!e.IsStart)
            {
                table1.HideWindowTable();
            }
        }

        void DataManager_ReplayEvent(object sender, TDataManager.ReplayEventArgs e)
        {
            switch (e.ReplayStatus)
            {
                case TDataManager.ReplayStatus.End:
                    SwitchToDataSection(_dataSectionName);
                    table1.HideWindowTable();
                    break;
                case TDataManager.ReplayStatus.Pause:
                    break;
                case TDataManager.ReplayStatus.Playing:
                    table1.NeedMarkLine(e.TimeIndex);
                    break;
                case TDataManager.ReplayStatus.Start:
                    SwitchToDataSection(e.SectionName);
                    table1.ShowWindowTable();
                    break;
                case TDataManager.ReplayStatus.SwitchSection:
                    SwitchToDataSection(e.SectionName);
                    break;
            }
        }
        void MaskColumns()
        {
            List<VirtualInstrument.Classes.ColumnDefine> columnDefines = new List<VirtualInstrument.Classes.ColumnDefine>();
            toolStripButton_timeIndex.Checked = _timeIndexVisible;
            toolStripButton_timeStamp.Checked = _timeStampVisible;
            if (_timeIndexVisible)
            {
                
                columnDefines.Add(new VirtualInstrument.Classes.ColumnDefine(TDataManager.DataManager.TIMEINDEX_NAME, _dataEngine.DataManager.GetCaption(TDataManager.DataManager.TIMEINDEX_NAME), 0));
            }
            if (_timeStampVisible)
            {
                columnDefines.Add(new VirtualInstrument.Classes.ColumnDefine(TDataManager.DataManager.TIMESTAMP_NAME, _dataEngine.DataManager.GetCaption(TDataManager.DataManager.TIMESTAMP_NAME), 0));
            }
            for (int i = 0; i < _sensorNames.Count; i++)
            {
                TDataManager.DataStore dataStore = _dataEngine.DataManager.LastDataSection.GetSensorByName(_sensorNames[i]);
                if (dataStore != null)
                {
                    columnDefines.Add(new VirtualInstrument.Classes.ColumnDefine(dataStore.DataProps.Name, dataStore.DataProps.Caption, dataStore.DataProps.Decimal));
                }
            }
            for (int i = 0; i < this._exprNames.Count; i++)
            {
                TDataManager.DataStore dataStore = _dataEngine.DataManager.LastDataSection.GetDataStoreByName(_exprNames[i]);
                if (dataStore != null)
                {
                    columnDefines.Add(new VirtualInstrument.Classes.ColumnDefine(dataStore.DataProps.Name, dataStore.DataProps.Caption, dataStore.DataProps.Decimal));
                }
            }
            table1.SetColumnDefines(columnDefines);
        }
        void PrepairSection(string sectionName)
        {
            TDataManager.DataSection section = _dataEngine.DataManager.GetDataSectionByName(sectionName);
            if (section != null)
            {
                double interval = section.TimeLineProps.Interval;
                double seconds = _dataEngine.WorkArguments.EndProperties.ReservedSeconds;
                int count = (int)Math.Round(seconds / interval, 0);
                table1.SetDataTable(section.GetDataTable(false, count, false));
                table1.TableCaption = section.Caption;
            }
            //FillList();
        }

        void FormTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dataEngine.DataManager.DataCollectionEvent -= DtManager_DataCollectionEvent;
            _dataEngine.DataManager.SectionEvent -= DtManager_SectionEvent;
            _dataEngine.DataManager.ReplayEvent -= DataManager_ReplayEvent;
            _dataEngine.DataSource.WorkStatusChanged -= DataSource_WorkStatusChanged;
        }

        void DtManager_SectionEvent(object sender, TDataManager.SectionEventArgs e)
        {
            if (e.EventType == TDataManager.DataEventType.Add && _alwaysNew)
            {
                _dataSectionName = e.SectionName;


                PrepairSection(_dataSectionName);
                _needReleaseMerge = true;
                table1.ShowWindowTable();
            }
            else if (e.EventType == TDataManager.DataEventType.Clear)
            {
                _dataSectionName = "";
                FillList();
            }
            else if (e.EventType == TDataManager.DataEventType.Remove)
            {
                if (string.Equals(e.SectionName, _dataSectionName))
                {
                    _dataSectionName = _dataEngine.DataManager.LastDataSection.Name;
                    FillList();
                }
            }
            else
            {
                if (string.Equals(e.SectionName, _dataSectionName))
                {
                    FillList();
                }
            }
        }
        void DtManager_DataCollectionEvent(object sender, TDataManager.DataCollectionEventArgs e)
        {
            if (string.Equals(_dataSectionName, e.SectionName))
            {
                if (_sensorNames.Contains(e.ColumnName))
                {
                    table1.AddValue(e.SectionName, e.ColumnName, e.DataElement.TimeIndex, e.DataElement.Value);
                }
                if (_exprNames.Contains(e.ColumnName))
                {
                    table1.AddValue(e.SectionName, e.ColumnName, e.DataElement.TimeIndex, e.DataElement.Value);
                }
            }

        }
        void FillList()
        {
            FillList(_dataSectionName);
        }
        void FillList(string sectionName)
        {
            TDataManager.DataSection dataSection = _dataEngine.DataManager.GetDataSectionByName(sectionName);
            if (dataSection != null)
            {
                table1.TableCaption = dataSection.Caption;

                table1.SetDataTable(dataSection.GetDataTable(true, 0, table1.Merge));
            }
        }
        void SwitchToDataSection(string sectionName)
        {
            FillList(sectionName);
        }
        #endregion

        #region Serialize
        string List2Str(List<string> list)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();

            for (int i = 0; i < list.Count; i++)
            {
                keyValue.Add(i.ToString(), list[i]);
            }
            return keyValue.ToString();
        }
        List<string> Str2List(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            List<string> list = new List<string>(keyValue.Count);
            for (int i = 0; i < keyValue.Count; i++)
            {
                list.Add(keyValue.GetValueByKey(i.ToString()));
            }
            return list;
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", Name);
            keyValue.Add("Caption", Text);
            keyValue.Add("Bounds", Classes.PublicMethods.Bounds2Str(Bounds));
            //keyValue.Add("ConstantNames", List2Str(_constantNames));
            keyValue.Add("Table", table1.ToString());
            keyValue.Add("SensorNames", List2Str(_sensorNames));
            keyValue.Add("ExprNames", List2Str(_exprNames));
            keyValue.Add("DataSectionName", _dataSectionName);
            keyValue.Add("AlwaysNew", _alwaysNew.ToString());
            keyValue.Add("TimeIndexVisible", _timeIndexVisible.ToString());
            keyValue.Add("TimeStampVisible", _timeStampVisible.ToString());
            keyValue.Add("FontSizeRate", _fontSizeRate.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Name = keyValue.GetValueByKey("Name");
            Text = keyValue.GetValueByKey("Caption");
            Bounds = Classes.PublicMethods.RectangleParse(keyValue.GetValueByKey("Bounds"));
            //_constantNames.Clear();
            //_constantNames.AddRange(Str2List(keyValue.GetValueByKey("ConstantNames")));
            _sensorNames.Clear();
            _sensorNames.AddRange(Str2List(keyValue.GetValueByKey("SensorNames")));
            _exprNames.Clear();
            _exprNames.AddRange(Str2List(keyValue.GetValueByKey("ExprNames")));
            _dataSectionName = keyValue.GetValueByKey("DataSectionName");
            table1.Parse(keyValue.GetValueByKey("Table"));
            bool.TryParse(keyValue.GetValueByKey("AlwaysNew"), out _alwaysNew);
            bool.TryParse(keyValue.GetValueByKey("TimeIndexVisible"), out _timeIndexVisible);
            bool.TryParse(keyValue.GetValueByKey("TimeStampVisible"), out _timeStampVisible);
            int.TryParse(keyValue.GetValueByKey("FontSizeRate"), out _fontSizeRate);
            table1.FontSizeF = Classes.PublicMethods.CalcFontSize(_fontSizeRate);
            MaskColumns();
            FillList();
            toolStripButton_merge.Checked = table1.Merge;
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needReleaseMerge)
            {
                _needReleaseMerge = false;
                toolStripButton_merge.Checked = false;
                table1.Merge = false;
            }

        }

        private void toolStripButton_caption_Click(object sender, EventArgs e)
        {
            FormInputText f = new FormInputText(table1.TableTitle);
            if (f.ShowDialog() == DialogResult.OK)
            {
                table1.TableTitle = f.SelectedText;
            }

        }

        private void toolStripButton_props_Click(object sender, EventArgs e)
        {
            List<string> names = new List<string>();
            foreach (string name in _sensorNames)
            {
                names.Add(name);
            }
            foreach (string name in _exprNames)
            {
                names.Add(name);
            }
            FormVariablePicker f = new FormVariablePicker(_dataEngine, true, "", names);
            if (f.ShowDialog() == DialogResult.OK)
            {
                _sensorNames.Clear();
                _sensorNames.AddRange(f.SelectedSensorNames);
                _exprNames.Clear();
                _exprNames.AddRange(f.SelectedVariableNames);
                if (string.IsNullOrEmpty(_dataSectionName))
                {
                    _dataSectionName = _dataEngine.DataManager.LastDataSection.Name;
                }
                MaskColumns();
                SwitchToDataSection(_dataSectionName);
            }

        }

        private void toolStripButton_time_Click(object sender, EventArgs e)
        {
            FormTimeSinglePicker f = new FormTimeSinglePicker(_dataEngine, _dataSectionName);
            if (f.ShowDialog() == DialogResult.OK)
            {
                _dataSectionName = f.SelectedSectionName;
                SwitchToDataSection(f.SelectedSectionName);
            }

        }

        private void toolStripButton_excel_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TDataManager.DataSection dataSection = _dataEngine.DataManager.GetDataSectionByName(_dataSectionName);
                if (dataSection != null)
                {
                    byte[] filenBytes = Properties.Resources.Templete;

                    dataSection.SaveToExcel(saveFileDialog1.FileName, filenBytes, table1.Merge);
                    if (MessageBox.Show(_resourceManager.GetString("OpenExcelMSG"), _resourceManager.GetString("ExportCaption" ), MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo(saveFileDialog1.FileName);
                        startInfo.WindowStyle = ProcessWindowStyle.Normal;

                        Process.Start(startInfo);

                    }
                }
            }

        }

        private void toolStripButton_fontSize_Click(object sender, EventArgs e)
        {
            FormFontSizeSetup f = new FormFontSizeSetup(_fontSizeRate);
            if (f.ShowDialog() == DialogResult.OK)
            {
                _fontSizeRate = f.SelectedFontSizeRate;
                table1.FontSizeF = Classes.PublicMethods.CalcFontSize(_fontSizeRate);
                if (_dataSectionName != null)
                {
                    SwitchToDataSection(_dataSectionName);
                }
            }

        }

        private void toolStripButton_merge_Click(object sender, EventArgs e)
        {
            toolStripButton_merge.Checked = !toolStripButton_merge.Checked;
            table1.Merge = toolStripButton_merge.Checked;
            FillList();

        }

        private void toolStripButton_timeIndex_Click(object sender, EventArgs e)
        {
            toolStripButton_timeIndex.Checked = !toolStripButton_timeIndex.Checked;
            _timeIndexVisible = toolStripButton_timeIndex.Checked;
            MaskColumns();
            SwitchToDataSection(_dataSectionName);
        }

        private void toolStripButton_timeStamp_Click(object sender, EventArgs e)
        {
            toolStripButton_timeStamp.Checked = !toolStripButton_timeStamp.Checked;
            _timeStampVisible = toolStripButton_timeStamp.Checked;
            MaskColumns();
            SwitchToDataSection(_dataSectionName);

        }
    }
}
