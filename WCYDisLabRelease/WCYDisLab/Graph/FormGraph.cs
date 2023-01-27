using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace WCYDisLab.Graph
{
    public partial class FormGraph : Form
    {
        public FormGraph(Classes.DataEngine dataEngine)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            InitDataEngine();

            SwitchToTemplete();
            //graph1.Initialize(graph1.Name, "");
            FillList();

        }
        public FormGraph(Classes.DataEngine dataEngine, string value)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            InitDataEngine();

            SwitchToTemplete();
            //graph1.Initialize(graph1.Name, "");
            this.StartPosition = FormStartPosition.Manual;
            FillList();
            Parse(value);
        }
        public FormGraph(Classes.DataEngine dataEngine, string name, bool isSensor)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            InitDataEngine();
            SwitchToTemplete();
            Initialize(name, isSensor);
            FillList();
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.Graph.FormGraph", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine; 
        TDataManager.DataSection _dataSection;

        bool _alwaysNew = true;
        DataAnalysis.Fitting _fitting;
        TDataManager.ReplayStatus _replayStatus = TDataManager.ReplayStatus.End;
        GraphAddPointRecord[] _pointRecords;
        int _addIndex = -1, _readIndex = -1;

        #endregion

        #region Methods
        void InitDataEngine()
        {
            _dataEngine.DataManager.SectionEvent += new TDataManager.SectionEventHandler(DataManager_SectionEvent);
            _dataEngine.DataManager.DataCollectionEvent += new TDataManager.DataCollectionHandler(DataManager_DataCollectionEvent);
            _dataEngine.DataManager.ReplayEvent += new TDataManager.ReplayHandler(DataManager_ReplayEvent);
            this.FormClosing += new FormClosingEventHandler(FormGraph_FormClosing);
            _fitting = new DataAnalysis.Fitting();
            _pointRecords =new GraphAddPointRecord[_dataEngine.WorkArguments.CalcCapcity(_dataEngine.DataManager.DataSectionActive.TimeLineProps.Interval) * TDataManager.DataCollection.DEFAULTCOUNT];
            graph1.Method_GetValue = _fitting.GetValue;
            SyncSections();
            SetDefaultXAxis();
            timer1.Start();
        }

        void FormGraph_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dataEngine.DataManager.DataCollectionEvent -= DataManager_DataCollectionEvent;
            _dataEngine.DataManager.SectionEvent -= DataManager_SectionEvent;
            _dataEngine.DataManager.ReplayEvent -= DataManager_ReplayEvent;
        }

        void DataManager_ReplayEvent(object sender, TDataManager.ReplayEventArgs e)
        {
            _replayStatus = e.ReplayStatus;
            switch (e.ReplayStatus)
            {
                case TDataManager.ReplayStatus.End:
                    SyncDatas();
                    break;
                case TDataManager.ReplayStatus.Pause:
                    break;
                case TDataManager.ReplayStatus.Playing:
                    SetReplayValue(e.SectionName, e.TimeIndex);
                    break;
                case TDataManager.ReplayStatus.Start:
                    graph1.ClearLinePoints(_dataEngine.DataManager.Replay.ReplaySectionNames);
                    break;
                case TDataManager.ReplayStatus.SwitchSection:
                    //graph1.MaskReplaySections(_dataEngine.DataManager.Replay.ReplaySectionNames);
                    break;
            }

        }
        void SetReplayValue(string sectionName, int timeIndex)
        {
            for (int i = 0; i < graph1.SectionTemplete.GraphLines.Count; i++)
            {
                string columnName = graph1.SectionTemplete.GraphLines[i].ColumnDefine.ColumnName;
                TDataManager.DataSection section = _dataEngine.DataManager.GetDataSectionByName(sectionName);
                if (section != null)
                {
                    TDataManager.DataStore dataStore = section.GetDataStoreByColumnName(columnName);
                    if (dataStore != null)
                    {
                        if (dataStore.ExistsByTimeIndex(timeIndex))
                        {
                            TDataManager.DataElement dataElement = section.GetDataElementByTimeIndex(columnName, timeIndex);
                            if (dataElement != null)
                            {
                                if (_replayStatus == TDataManager.ReplayStatus.Playing)
                                {
                                    bool success = false;
                                    double x = GetXValue(sectionName, graph1.XAxis.Name, dataElement, ref success);
                                    if (success)
                                    {
                                        graph1.AddPoint(sectionName, columnName, x, dataElement.Value, section.Caption);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void DataManager_DataCollectionEvent(object sender, TDataManager.DataCollectionEventArgs e)
        {
            switch (e.EventType)
            {
                case TDataManager.DataEventType.Add:
                    if (graph1.SectionTemplete.LineContains(e.ColumnName))
                    {
                        AddPointRecord(e.SectionName, e.ColumnName, e.DataElement);
                    }
                    break;
            }
        }

        void DataManager_SectionEvent(object sender, TDataManager.SectionEventArgs e)
        {
            switch (e.EventType)
            {
                case TDataManager.DataEventType.Add:
                    TDataManager.DataSection dataSection = _dataEngine.DataManager.GetDataSectionByName(e.SectionName);
                    if (dataSection != null)
                    {
                        if (_alwaysNew)
                        {
                            graph1.VisibleSectionNames.Add(dataSection.Name);
                        }
                        graph1.CloneSectionFromTemplete(e.SectionName, dataSection.Caption, false);
                        graph1.NeedSetLastSectionLineWidth();
                        _addIndex = -1;
                        _readIndex = -1;
                        int lineCount = graph1.SectionTemplete.GraphLines.Count;
                        _pointRecords = new GraphAddPointRecord[_dataEngine.WorkArguments.CalcCapcity(dataSection.TimeLineProps.Interval) * lineCount];

                    }
                    break;
                case TDataManager.DataEventType.Remove:
                    if (e.SectionIndex >= 0 && e.SectionIndex < graph1.Count)
                    {
                        graph1.SectionsRemoveAt(e.SectionIndex);
                    }
                    break;
                case TDataManager.DataEventType.Clear:
                    graph1.SectionClear();
                    break;
            }
        }
        void AddPointRecord(string sectionName, string columnName, TDataManager.DataElement dataElement)
        {
            _addIndex++;
            if (_addIndex >= 0 && _addIndex < _pointRecords.Length)
            {
                _pointRecords[_addIndex] = new GraphAddPointRecord(sectionName, columnName, dataElement);
            }
        }
        double VoiceLastValue=0;
        double ValueX = 0;
        bool IsFirstValue = true;
        
        void AddPoint(string sectionName, string columnName, TDataManager.DataElement dataElement)
        {
            TDataManager.DataSection dataSection = _dataEngine.DataManager.GetDataSectionByName(sectionName);
            TDataManager.DataStore dataStore = null;
            if (dataSection.SensorContains(columnName))
            {
                dataStore = dataSection.GetSensorByName(columnName);
            }
            else if (dataSection.DataStoreContains(columnName))
            {
                dataStore = dataSection.GetDataStoreByName(columnName);
            }
            if (dataStore != null)
            {
                if (dataStore.ExistsByTimeIndex(dataElement.TimeIndex))
                {
                    bool success = false;
                    double x = 0.000005 * Form1.IndexX + GetXValue(sectionName, graph1.XAxis.Name, dataElement, ref success);
                    if (success)
                    {
                        if (Form1.IsVoice)//10WHZ
                        {
                            //if (VoiceLastValue == 0)
                            //{
                            //    return;
                            //}
                            Form1.IndexX += 19;
                            if (IsFirstValue)
                            {
                                IsFirstValue = false;
                                VoiceLastValue = dataElement.Value;
                                return;
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (x <_dataEngine.WorkArguments.EndProperties.ReservedSeconds)
                                    graph1.AddPoint(sectionName, columnName, x + 0.000005 * i, VoiceLastValue + (dataElement.Value - VoiceLastValue) / 20 * i, dataSection.Caption);

                            }
                            VoiceLastValue = dataElement.Value;
                        }
                        else
                            graph1.AddPoint(sectionName, columnName, x, dataElement.Value, dataSection.Caption);
                    }

                }
                
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
                graph1.Initialize(graph1.Name, dataStore.DataProps.Caption + " - "+_resourceManager.GetString("SamplingTime"));
                graph1.AddCurve(dataStore.DataProps.Name, dataStore.DataProps.Caption, dataStore.DataProps.Decimal, VirtualInstrument.Classes.PublicMethods.GetNewColor(), true, true, VirtualInstrument.Graph.DEFAULT_LINE_WIDTH, false, false, 1f, true);
                graph1.SetVisibleSections(_dataEngine.DataManager.SectionNames);
                SyncDatas();
                SetDefaultXAxis();
                graph1.YAxis.Caption = dataStore.DataProps.Caption;
                graph1.YAxis.Maximum = dataStore.DataProps.MaxValue;
                graph1.YAxis.Minimum = dataStore.DataProps.MinValue;
                graph1.YAxis.Unit = dataStore.DataProps.Unit;
                graph1.YAxis.AutoScale = true;
                //graph1.YAxis.AutoScale = false;
                graph1.ResetAxis();
                graph1.RefreshAll();
            }
            else
            {
                graph1.Initialize(graph1.Name, "");
            }
            //SyncSections();
        }
        void SetDefaultXAxis()
        {
            graph1.SetVisibleSections(_dataEngine.DataManager.SectionNames);
            graph1.XAxis.Name = TDataManager.DataManager.TIMESTAMP_NAME;
            graph1.XAxis.Caption = _dataEngine.DataManager.GetCaption(TDataManager.DataManager.TIMESTAMP_NAME);
            graph1.XAxis.Minimum = 0;
            graph1.XAxis.Maximum = _dataEngine.WorkArguments.EndProperties.ReservedSeconds;
            graph1.XAxis.Unit = "s";
            graph1.XAxis.AutoScale = true;
            //graph1.XAxis.AutoScale = false;
        }

        void SyncSections()
        {
            graph1.SectionClear();
            foreach (TDataManager.DataSection section in _dataEngine.DataManager.DataSections)
            {
                graph1.CloneSectionFromTemplete(section.Name, section.Caption, false);
            }
            graph1.NeedSyncAll();
        }
        public void SyncDatas()
        {
            for (int i = 0; i < _dataEngine.DataManager.DataSections.Count; i++)
                                                                                                               {
                SyncDatas(_dataEngine.DataManager.DataSections[i].Name);
            }
        }
        public void SyncDatas(string sectionName)
        {
            TDataManager.DataSection dataSection = _dataEngine.DataManager.GetDataSectionByName(sectionName);
            if (dataSection != null)
            {
                for (int i = 0; i < dataSection.Sensors.Count; i++)
                {
                    if (graph1.SectionTemplete.LineContains(dataSection.Sensors[i].DataProps.Name))
                    {
                        VirtualInstrument.Classes.GraphObjCollection section = graph1.GetSectionByName(dataSection.Name);
                        if (section != null)
                        {
                            VirtualInstrument.Classes.GraphLineDefine line = section.GetLineDefineByName(dataSection.Sensors[i].DataProps.Name);
                            if (line != null)
                            {
                                line.ClearPoints();
                                for (int j = 0; j < dataSection.Sensors[i].DataCollection.Count; j++)
                                {
                                    TDataManager.DataElement dataElement = dataSection.Sensors[i].DataCollection.Datas[j];
                                    bool success = false;
                                    double x = GetXValue(sectionName, graph1.XAxis.Name, dataElement, ref success);
                                    double y = dataElement.Value;
                                    if (success)
                                    {
                                        line.AddPoint(dataSection.Caption, x, y);
                                        graph1.ResetScale(x, y, line.IsY2Axis);
                                    }
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < dataSection.DataStores.Count; i++)
                {
                    if (graph1.SectionTemplete.LineContains(dataSection.DataStores[i].DataProps.Name))
                    {
                        VirtualInstrument.Classes.GraphObjCollection section = graph1.GetSectionByName(dataSection.Name);
                        if (section != null)
                        {
                            VirtualInstrument.Classes.GraphLineDefine line = section.GetLineDefineByName(dataSection.DataStores[i].DataProps.Name);
                            if (line != null)
                            {
                                line.ClearPoints();
                                for (int j = 0; j < dataSection.DataStores[i].DataCollection.Count; j++)
                                {
                                    TDataManager.DataElement dataElement = dataSection.DataStores[i].DataCollection.Datas[j];
                                    bool success = false;
                                    double x = GetXValue(sectionName, graph1.XAxis.Name, dataElement, ref success);
                                    double y = dataElement.Value;
                                    if (success)
                                    {
                                        line.AddPoint(dataSection.Caption, x, y);
                                        graph1.ResetScale(x, y, line.IsY2Axis);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void SyncDatas(string sectionName, string columnName)
        {
            TDataManager.DataSection dataSection = _dataEngine.DataManager.GetDataSectionByName(sectionName);
            if (dataSection != null)
            {
                TDataManager.DataStore dataStore = dataSection.GetSensorByName(columnName);
                if (dataStore != null)
                {
                    VirtualInstrument.Classes.GraphObjCollection section = graph1.GetSectionByName(sectionName);
                    if (section != null)
                    {
                        VirtualInstrument.Classes.GraphLineDefine line = section.GetLineDefineByName(dataStore.DataProps.Name);
                        if (line != null)
                        {
                            line.ClearPoints();

                            for (int i = 0; i < dataStore.DataCollection.Count; i++)
                            {
                                TDataManager.DataElement dataElement = dataStore.DataCollection.Datas[i];
                                bool success = false;
                                double x = GetXValue(sectionName, graph1.XAxis.Name, dataElement, ref success);
                                double y = dataElement.Value;
                                if (success)
                                {
                                    line.AddPoint(dataSection.Caption, x, y);
                                    graph1.ResetScale(x, y, line.IsY2Axis);
                                }
                            }
                        }
                    }

                }
                else
                {
                    dataStore = dataSection.GetDataStoreByName(columnName);
                    if (dataStore != null)
                    {
                        VirtualInstrument.Classes.GraphObjCollection section = graph1.GetSectionByName(sectionName);
                        if (section != null)
                        {
                            VirtualInstrument.Classes.GraphLineDefine line = section.GetLineDefineByName(dataStore.DataProps.Name);
                            if (line != null)
                            {
                                line.ClearPoints();

                                for (int i = 0; i < dataStore.DataCollection.Count; i++)
                                {
                                    TDataManager.DataElement dataElement = dataStore.DataCollection.Datas[i];
                                    bool success = false;
                                    double x = GetXValue(sectionName, graph1.XAxis.Name, dataElement, ref success);
                                    double y = dataElement.Value;
                                    if (success)
                                    {
                                        line.AddPoint(dataSection.Caption, x, y);
                                        graph1.ResetScale(x, y, line.IsY2Axis);
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
        double GetXValue(string sectionName, string columnName, TDataManager.DataElement yDataElement, ref bool success)
        {
            success = false;
            double value = 0;
            if (string.Equals(TDataManager.DataManager.TIMEINDEX_NAME, columnName))
            {
                success = true;
                value = yDataElement.TimeIndex;
            }
            else if (string.Equals(TDataManager.DataManager.TIMESTAMP_NAME, columnName))
            {
                success = true;
                value = yDataElement.TimeStamp;
            }
            else
            {
                TDataManager.DataSection dataSection = _dataEngine.DataManager.GetDataSectionByName(sectionName);
                if (dataSection != null)
                {
                    TDataManager.DataStore dataStore = dataSection.GetDataStoreByColumnName(columnName);
                    if (dataStore != null && dataStore.ExistsByTimeIndex(yDataElement.TimeIndex))
                    {
                        TDataManager.DataElement dataElement = dataSection.GetDataElementByTimeIndex(columnName, yDataElement.TimeIndex);
                        if (dataElement != null)
                        {
                            success = true;
                            value = dataElement.Value;
                        }
                    }
                }
            }
            return value;
        }
        void FillList()
        {
            graph1.SyncAxis();

            graph1.MouseEvent += new VirtualInstrument.Classes.GraphMouseActionHandler(graph1_MouseEvent);
            graph1.Y2Axis.Visible = false;
        }
        double A = 0,B=0;
        void graph1_MouseEvent(object sender, VirtualInstrument.Classes.GraphMouseActionEventArgs e)
        {
            switch (e.SelectedType)
            {
                case VirtualInstrument.Classes.GraphicsSelectType.SelectData:
                    FormDataAnalysisSetup f = new FormDataAnalysisSetup(e.SelectedCurves);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (f.SelectedMethodName == "求导")
                        {
                            VirtualInstrument.Classes.GraphManualLine line = new VirtualInstrument.Classes.GraphManualLine();
                            line.Color = Color.Blue;
                            //line.LineItem.Line.Width = 5f;
                            line.XCaption = "t";
                            line.YCaption = "s";

                           List<VirtualInstrument.Classes.GraphLine> selectedCurves = new List<VirtualInstrument.Classes.GraphLine>();
                            selectedCurves.AddRange(f.SelectedCurves);
                            double[] xList = new double[selectedCurves[0].Count];
                            double[] yList = new double[selectedCurves[0].Count];
                            if (A != 0)
                            {
                                for (int j = 0; j < selectedCurves[0].Points.Count; j++)
                                {

                                    if (xList[0] == 0)
                                    {
                                        B =selectedCurves[0].Points[0].Y-selectedCurves[0].Points[0].X * A;
                                        xList[0] = selectedCurves[0].Points[0].X;
                                        yList[0] = selectedCurves[0].Points[0].Y;
                                    }
                                    else
                                    {
                                        xList[j] = selectedCurves[0].Points[j].X;
                                        yList[j] = selectedCurves[0].Points[j].X * A+B;
                                    }
                                    
                                    line.Add(xList[j], yList[j]);
                                    
                                }
                                graph1.AddLine(graph1.SectionTemplete.Name, line.XCaption, line.YCaption, line.Color, line.Points);
                                
                            }
                        }
                        else
                        {
                            List<VirtualInstrument.Classes.GraphLine> selectedCurves = new List<VirtualInstrument.Classes.GraphLine>();
                            selectedCurves.AddRange(f.SelectedCurves);
                            for (int i = 0; i < selectedCurves.Count; i++)
                            {
                                DataAnalysis.Fitting fitting = new DataAnalysis.Fitting();

                                double[] xList = new double[selectedCurves[i].Count];
                                double[] yList = new double[selectedCurves[i].Count];
                                for (int j = 0; j < selectedCurves[i].Points.Count; j++)
                                {
                                    xList[j] = selectedCurves[i].Points[j].X;
                                    yList[j] = selectedCurves[i].Points[j].Y;
                                }
                                if (fitting.IsFittingMethod(f.SelectedMethodName))
                                {
                                    double[] newYList = new double[selectedCurves[i].Count];
                                    double[] para = new double[] { 0, 0, 0, 0 };
                                    bool success = fitting.CalcFit(f.SelectedMethodName, xList, yList, ref  newYList, ref para);
                                    if (success)
                                    {
                                        graph1.AddFitLine(selectedCurves[i].SectionName, selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption,
                                            f.SelectedMethodName, para[0], para[1], para[2], para[3], selectedCurves[i].LineDefine.IsY2Axis,
                                            selectedCurves[i].StartX, selectedCurves[i].EndX, true);
                                        if (f.SelectedAddLabel)
                                        {
                                            graph1.AddLabel(_resourceManager.GetString("FittingResult"), fitting.GetStr(f.SelectedMethodName, para, para[1]), selectedCurves[i].Points[0].X, selectedCurves[i].Points[0].Y, 0, true);
                                        }
                                    }
                                    if (f.SelectedMethodName == "PolyFitting2")
                                    {
                                        A = para[2] * 2;
                                    }
                                }
                                else
                                {
                                    double value = Math.Round(fitting.CalcStatistic(f.SelectedMethodName, xList, yList), 6);
                                    if (fitting.IsCalculateMethod(f.SelectedMethodName))
                                    {
                                        graph1.AddFitLine(selectedCurves[i].SectionName, selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption,
                                                f.SelectedMethodName, selectedCurves[i].Points, selectedCurves[i].LineDefine.IsY2Axis);
                                        if (f.SelectedAddLabel)
                                        {
                                            graph1.AddLabel(_resourceManager.GetString("CalculateResult"),
                                                selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption + _dataEngine.DataManager.GetCaption(selectedCurves[i].SectionName)
                                                + fitting.GetFittingExpression(f.SelectedMethodName) + " : " + value.ToString(),
                                                selectedCurves[i].Points[0].X, selectedCurves[i].Points[0].Y, 0, true);
                                        }
                                    }
                                    else
                                    {
                                        graph1.AddFitLine(selectedCurves[i].SectionName, selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption,
                                                f.SelectedMethodName, 0, value, 0, 0, selectedCurves[i].LineDefine.IsY2Axis,
                                                selectedCurves[i].StartX, selectedCurves[i].EndX, true);
                                        if (f.SelectedAddLabel)
                                        {
                                            graph1.AddLabel(_resourceManager.GetString("StatisticResult"),
                                                selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption + _dataEngine.DataManager.GetCaption(selectedCurves[i].SectionName)
                                                + fitting.GetFittingExpression(f.SelectedMethodName) + " : " + value.ToString(),
                                                selectedCurves[i].StartX, value, 0, true);
                                        }
                                    }
                                }

                            }
                        }
                    }
                    break;
            }
        }
        void SwitchToTemplete()
        {
            _dataSection = _dataEngine.CurrentDataSection;
        }
        void SwitchToDataSection(int index)
        {
            if (index >= 0 && index < _dataEngine.DataManager.DataSections.Count)
            {
                _dataSection = _dataEngine.DataManager.DataSections[index];
            }
        }
        void SwitchToDataSection(string sectionName)
        {
            foreach (TDataManager.DataSection section in _dataEngine.DataManager.DataSections)
            {
                if (string.Equals(section.Name, sectionName))
                {
                    _dataSection = section;
                    break;
                }
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
            keyValue.Add("AlwaysNew", _alwaysNew.ToString());
            keyValue.Add("Graph", this.graph1.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Name = keyValue.GetValueByKey("Name");
            Text = keyValue.GetValueByKey("Caption");
            Bounds = Classes.PublicMethods.RectangleParse(keyValue.GetValueByKey("Bounds"));
            bool.TryParse(keyValue.GetValueByKey("AlwaysNew"), out _alwaysNew);
            graph1.Parse(keyValue.GetValueByKey("Graph"));
            SyncDatas();
            graph1.Method_GetValue = _fitting.GetValue;
            //toolStripButton_lockXAxis.Checked = graph1.LockXAxis;
            //toolStripButton_lockY2Axis.Checked = graph1.LockY2Axis;
            //toolStripButton_lockYAxis.Checked = graph1.LockYAxis;
        }

        #endregion

        private void toolStripMenuItem_move_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.None;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = true;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked = true;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = false;
            toolStripMenuItem_zoom.Checked = false;
           
        }

        private void toolStripMenuItem_analysis_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectData;
            toolStripButton_analysis.Checked = true;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = true;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked =false;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = false;
            toolStripMenuItem_zoom.Checked = false;

        }

        private void toolStripMenuItem_zoom_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectArea;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = true;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked = false;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = false;
            toolStripMenuItem_zoom.Checked = true;


        }

        private void toolStripMenuItem_pen_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.HighLighter;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = true;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked = false;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = true;
            toolStripMenuItem_zoom.Checked = false;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                graph1.HighLighterColor = colorDialog1.Color;
            }

        }

        private void toolStripMenuItem_moveLabel_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectLabel;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = true;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked = false;
            toolStripMenuItem_moveLabel.Checked = true;
            toolStripMenuItem_pen.Checked = true;
            toolStripMenuItem_zoom.Checked = false;

        }

        private void toolStripMenuItem_bandPoint_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectPoint;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = true;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = true;
            toolStripMenuItem_move.Checked = false;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = false;
            toolStripMenuItem_zoom.Checked = false;

        }
        private void toolStripButton_move_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.None;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = true;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked = true;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = false;
            toolStripMenuItem_zoom.Checked = false;

        }

        private void toolStripButton_analysis_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectData;
            toolStripButton_analysis.Checked = true;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = true;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked = false;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = false;
            toolStripMenuItem_zoom.Checked = false;

        }

        private void toolStripButton_zoom_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectArea;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = true;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked = false;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = false;
            toolStripMenuItem_zoom.Checked = true;

        }

        private void toolStripButton_selectLabel_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectLabel;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = true;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked = false;
            toolStripMenuItem_moveLabel.Checked = true;
            toolStripMenuItem_pen.Checked = true;
            toolStripMenuItem_zoom.Checked = false;

        }

        private void toolStripButton_selectBandPoint_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectPoint;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = false;
            toolStripButton_selectBandPoint.Checked = true;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = true;
            toolStripMenuItem_move.Checked = false;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = false;
            toolStripMenuItem_zoom.Checked = false;

        }

        private void toolStripButton_pen_Click(object sender, EventArgs e)
        {
            graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.HighLighter;
            toolStripButton_analysis.Checked = false;
            toolStripButton_lock.Checked = false;
            toolStripButton_move.Checked = false;
            toolStripButton_pen.Checked = true;
            toolStripButton_selectBandPoint.Checked = false;
            toolStripButton_selectLabel.Checked = false;
            toolStripButton_zoom.Checked = false;
            toolStripMenuItem_analysis.Checked = false;
            toolStripMenuItem_bandPoint.Checked = false;
            toolStripMenuItem_move.Checked = false;
            toolStripMenuItem_moveLabel.Checked = false;
            toolStripMenuItem_pen.Checked = true;
            toolStripMenuItem_zoom.Checked = false;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                graph1.HighLighterColor = colorDialog1.Color;
            }
        }

        private void toolStripButton_lock_Click(object sender, EventArgs e)
        {
            FormGraphLock f = new FormGraphLock(graph1.LockYAxis, graph1.LockY2Axis, graph1.LockXAxis);
            if (f.ShowDialog() == DialogResult.OK)
            {
                //graph1.LockXAxis = f.SelectedXLock;
                //graph1.LockYAxis = f.SelectedYLock;
                //graph1.LockY2Axis = f.SelectedY2Lock;
                graph1.LockX(f.SelectedXLock);
                graph1.LockY(f.SelectedYLock | f.SelectedY2Lock);
                ////graph1.LockY(!f.SelectedY2Lock);
                //graph1.LockYAxis = f.SelectedYLock;
                graph1.LockY2Axis = f.SelectedY2Lock;
            }

        }

        private void toolStripButton_showAll_Click(object sender, EventArgs e)
        {
            graph1.NeedZoomAuto();
        }

        private void toolStripMenuItem_lock_Click(object sender, EventArgs e)
        {
            FormGraphLock f = new FormGraphLock(graph1.LockYAxis, graph1.LockY2Axis, graph1.LockXAxis);
            if (f.ShowDialog() == DialogResult.OK)
            {
                graph1.LockX(f.SelectedXLock);
                graph1.LockY(f.SelectedYLock | f.SelectedY2Lock);
                graph1.LockXAxis = f.SelectedXLock;
                graph1.LockYAxis = f.SelectedYLock;
                graph1.LockY2Axis = f.SelectedY2Lock;
            }
        }

        private void toolStripMenuItem_label_Click(object sender, EventArgs e)
        {
            FormLabelManager f = new FormLabelManager(graph1.SectionTemplete.GraphLabels);
            if (f.ShowDialog() == DialogResult.OK)
            {
                graph1.ClearLabels(true);
                foreach (VirtualInstrument.Classes.GraphLabelDefine label in f.SelectedLabels)
                {
                    if (graph1.DGetLabelDefine(graph1.SectionTemplete.Name, label.LabelName) == null)
                    {
                        graph1.AddLabel(label.LabelCaption, label.LabelText, label.Position.X, label.Position.Y, label.Position.Z, true);
                    }
                }
                //graph1.NeedSyncAll();
            }

        }

        private void toolStripMenuItem_manualCurve_Click(object sender, EventArgs e)
        {
            FormManualCurveManager f = new FormManualCurveManager(graph1.SectionTemplete.ManualLines);
            if (f.ShowDialog() == DialogResult.OK)
            {
                graph1.ClearManualLines(true);
                for (int i = 0; i < f.SelectedManualLines.Count; i++)
                {
                    graph1.AddManualLine(graph1.SectionTemplete.Name, f.SelectedManualLines[i].XCaption, f.SelectedManualLines[i].YCaption, f.SelectedManualLines[i].Color, f.SelectedManualLines[i].Points);
                }

            }
        }

        private void toolStripMenuItem_standardCurve_Click(object sender, EventArgs e)
        {
            FormStandardCurveManager f = new FormStandardCurveManager(graph1.SectionTemplete.StandardCurves,_fitting.GetValue);
            if (f.ShowDialog() == DialogResult.OK)
            {
                graph1.ClearStandardCurves(graph1.CurrentSection.Name, true);
                foreach (VirtualInstrument.Classes.StandardCurveDefine curve in f.SelectedStandardCurves)
                {
                    graph1.AddStandardCurve(graph1.CurrentSection.Name, curve.Caption, curve.MethodName, curve.A, curve.B, curve.C, curve.D, curve.IsY2Axis);
                }

            }
        }

        private void toolStripMenuItem_clear_Click(object sender, EventArgs e)
        {
            graph1.ClearFitLineExtands(true);
            graph1.ClearFitLines(true);
            graph1.ClearLabels(true);
            graph1.ClearHighLighters(true);

        }
        private void toolStripMenuItem_lineAndAxis_Click(object sender, EventArgs e)
        {
            FormGraphProps f = new FormGraphProps(_dataEngine, graph1.SectionTemplete.GraphLines, graph1.XAxis, graph1.YAxis, graph1.Y2Axis,graph1.GraphCaption);
            if (f.ShowDialog() == DialogResult.OK)
            {
                graph1.XAxis.Parse(f.SelectedXAxisDefine.ToString());
                graph1.YAxis.Parse(f.SelectedYAxisDefine.ToString());
                graph1.Y2Axis.Parse(f.SelectedY2AxisDefine.ToString());
                graph1.GraphCaption = f.SelectedCapion;
                graph1.SyncAxis();
                //Sync Lines
                graph1.ClearLines(true);
                double ymax = 0, ymin = 0, y2max = 0, y2min = 0;
                List<VirtualInstrument.Classes.GraphLineDefine> lines = f.SelectedLines;
                for (int i = 0; i < lines.Count; i++)
                {

                    TDataManager.DataType dataType = _dataEngine.CurrentDataSection.GetDataType(lines[i].ColumnDefine.ColumnName);
                    TDataManager.DataStore dataStore = null;
                    if (dataType == TDataManager.DataType.Expr)
                    {
                        dataStore = _dataEngine.CurrentDataSection.GetDataStoreByName(lines[i].ColumnDefine.ColumnName);
                    }
                    else if (dataType == TDataManager.DataType.Sensor)
                    {
                        dataStore = _dataEngine.CurrentDataSection.GetSensorByName(lines[i].ColumnDefine.ColumnName);
                    }
                    if (dataStore != null)
                    {
                        if (i == 0)
                        {
                            if (lines[i].IsY2Axis)
                            {
                                y2max = dataStore.DataProps.MaxValue;
                                y2min = dataStore.DataProps.MinValue;
                            }
                            else
                            {
                                ymax = dataStore.DataProps.MaxValue;
                                ymin = dataStore.DataProps.MinValue;
                            }
                        }
                        else
                        {
                            if (lines[i].IsY2Axis)
                            {
                                y2max = Math.Max(dataStore.DataProps.MaxValue, y2max);
                                y2min = Math.Min(dataStore.DataProps.MinValue, y2min);
                            }
                            else
                            {
                                ymax = Math.Max(dataStore.DataProps.MaxValue, ymax);
                                ymin = Math.Min(dataStore.DataProps.MinValue, ymin);
                            }
                        }
                        if (lines[i].IsY2Axis)
                        {
                            this.graph1.Y2Axis.Unit = dataStore.DataProps.Unit;
                        }
                        else
                        {
                            this.graph1.YAxis.Unit = dataStore.DataProps.Unit;

                        }

                    }
                    if (lines[i].IsY2Axis)
                    {
                        this.graph1.Y2Axis.Visible = true;
                    }
                    else
                    {
                        this.graph1.YAxis.Visible = true;
                    }
                    VirtualInstrument.Classes.GraphLineDefine line = lines[i];
                    this.graph1.AddCurve(line.ColumnDefine.ColumnName, line.ColumnDefine.ColumnCaption, line.ColumnDefine.Decimal, line.LineColor, line.PointVisible, line.LineVisible, line.LineWidthF, line.IsY2Axis, line.IsSmooth, line.SmoothTention, true);

                    for (int j = 0; j < _dataEngine.DataManager.DataSections.Count; j++)
                    {
                        SyncDatas(_dataEngine.DataManager.DataSections[j].Name, lines[i].ColumnDefine.ColumnName);
                    }
                }
                //Sync Axis

                //graph1.XAxis.Parse(f.SelectedXAxisDefine.ToString());
                //graph1.YAxis.Parse(f.SelectedYAxisDefine.ToString());
                //graph1.Y2Axis.Parse(f.SelectedY2AxisDefine.ToString());
                //graph1.GraphCaption = f.SelectedCapion;
                
            }
        }

        private void toolStripMenuItem_times_Click(object sender, EventArgs e)
        {
            FormTimeMultiPicker f = new FormTimeMultiPicker(_dataEngine, graph1.VisibleSectionNames);
            if (f.ShowDialog() == DialogResult.OK)
            {
                graph1.SetVisibleSections(f.SelectedSectionNames);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_addIndex > _readIndex)
            {
                int index = 0;
                while (_readIndex < _addIndex && index <5000)
                {
                    _readIndex++;
                    if (_readIndex >= 0 && _readIndex < _pointRecords.Length)
                    {
                        GraphAddPointRecord record = _pointRecords[_readIndex];
                        if (record != null)
                        {                            
                            AddPoint(record.SectionName, record.ColumnName, record.DataElement);
                        }
                    }
                    index++;
                }
            }

        }

        private void toolStripButton_photo_Click(object sender, EventArgs e)
        {
            graph1.CopyToClipboard();
        }

    }
}
          