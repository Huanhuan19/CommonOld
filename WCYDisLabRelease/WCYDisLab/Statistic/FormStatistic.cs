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
using System.Drawing.Imaging;


namespace WCYDisLab.Statistic
{
    public partial class FormStatistic : Form
    {
        public FormStatistic(Classes.DataEngine dataEngine)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            InitDataEngine();
            FillList(true);
        }
        public FormStatistic(Classes.DataEngine dataEngine, string value)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            InitDataEngine();
            this.StartPosition = FormStartPosition.Manual;
            Parse(value);
            FillList(false);
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.Statistic.FormStatistic", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        DataAnalysis.Fitting _fitting = new DataAnalysis.Fitting();
        DataAnalysis.StatisticTypeDefine _method;
        DataAnalysis.StatisticTypeDefine Method
        {
            get { return _method; }
            set
            {
                _method = value;
                switch (_method)
                {
                    case DataAnalysis.StatisticTypeDefine.Average:
                        statistic1.Graph.GraphCaption = _resourceManager.GetString("Average");
                        break;
                    case DataAnalysis.StatisticTypeDefine.Maximum:
                        statistic1.Graph.GraphCaption = _resourceManager.GetString("Maximum");
                        break;
                    case DataAnalysis.StatisticTypeDefine.Median:
                        statistic1.Graph.GraphCaption = _resourceManager.GetString("Median");
                        break;
                    case DataAnalysis.StatisticTypeDefine.Minimum:
                        statistic1.Graph.GraphCaption = _resourceManager.GetString("Minimum");
                        break;
                    case DataAnalysis.StatisticTypeDefine.StandardError:
                        statistic1.Graph.GraphCaption = _resourceManager.GetString("StandardError");
                        break;
                    case DataAnalysis.StatisticTypeDefine.Sum:
                        statistic1.Graph.GraphCaption = _resourceManager.GetString("Summary");
                        break;
                }

            }
        }
        #endregion


        void InitDataEngine()
        {
            statistic1.Graph.Method_GetValue = _fitting.GetValue;
            _dataEngine.DataSource.WorkStatusChanged += new WCYDataSource.StartStopHandler(DataSource_WorkStatusChanged);
            _dataEngine.DataManager.SectionEvent += new TDataManager.SectionEventHandler(DataManager_SectionEvent);
            FormClosing += new FormClosingEventHandler(FormStatistic_FormClosing);
            statistic1.Graph.MouseEvent += new VirtualInstrument.Classes.GraphMouseActionHandler(Graph_MouseEvent);
        }

        void DataSource_WorkStatusChanged(object sender, WCYDataSource.StartStopEventArgs e)
        {
            if (!e.IsStart)
            {
                FillList(false);
            }
        }

        void DataManager_SectionEvent(object sender, TDataManager.SectionEventArgs e)
        {
            switch (e.EventType)
            {
                case TDataManager.DataEventType.Clear:
                case TDataManager.DataEventType.Modify:
                case TDataManager.DataEventType.Move:
                case TDataManager.DataEventType.MoveDown:
                case TDataManager.DataEventType.MoveUp:
                case TDataManager.DataEventType.Remove:
                case TDataManager.DataEventType.Shink:
                    FillList(false);
                    break;
            }
        }

        void MaskColumns()
        {
            List<string> columnNames = new List<string>();
            columnNames.Add("SectionIndex");
            columnNames.AddRange(_dataEngine.DataManager.CurrentDataSection.ColumnNames);
            statistic1.Initialize(columnNames, columnNames);

        }
        void Graph_MouseEvent(object sender, VirtualInstrument.Classes.GraphMouseActionEventArgs e)
        {
            switch (e.SelectedType)
            {
                case VirtualInstrument.Classes.GraphicsSelectType.SelectData:
                    Graph.FormDataAnalysisSetup f = new WCYDisLab.Graph.FormDataAnalysisSetup(e.SelectedCurves);
                    if (f.ShowDialog() == DialogResult.OK)
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
                                    statistic1.Graph.AddFitLine(selectedCurves[i].SectionName, selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption,
                                        f.SelectedMethodName, para[0], para[1], para[2], para[3], selectedCurves[i].LineDefine.IsY2Axis,
                                        selectedCurves[i].StartX, selectedCurves[i].EndX, true);
                                    statistic1.Graph.AddLabel(_resourceManager.GetString("FittingResult"), fitting.GetStr(f.SelectedMethodName, para, para[1]), selectedCurves[i].Points[0].X, selectedCurves[i].Points[0].Y, 0, true);

                                }
                            }
                            else
                            {
                                double value = Math.Round(fitting.CalcStatistic(f.SelectedMethodName, xList, yList), 6);
                                if (fitting.IsCalculateMethod(f.SelectedMethodName))
                                {
                                    statistic1.Graph.AddFitLine(selectedCurves[i].SectionName, selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption,
                                            f.SelectedMethodName, selectedCurves[i].Points, selectedCurves[i].LineDefine.IsY2Axis);
                                    statistic1.Graph.AddLabel(_resourceManager.GetString("CalculateResult"),
                                        selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption + _dataEngine.DataManager.GetCaption(selectedCurves[i].SectionName)
                                        + fitting.GetFittingExpression(f.SelectedMethodName) + " : " + value.ToString(),
                                        selectedCurves[i].Points[0].X, selectedCurves[i].Points[0].Y, 0, true);

                                }
                                else
                                {
                                    statistic1.Graph.AddFitLine(selectedCurves[i].SectionName, selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption,
                                            f.SelectedMethodName, 0, value, 0, 0, selectedCurves[i].LineDefine.IsY2Axis,
                                            selectedCurves[i].StartX, selectedCurves[i].EndX, true);
                                    statistic1.Graph.AddLabel(_resourceManager.GetString("StatisticResult"),
                                        selectedCurves[i].LineDefine.ColumnDefine.ColumnCaption + _dataEngine.DataManager.GetCaption(selectedCurves[i].SectionName)
                                        + fitting.GetFittingExpression(f.SelectedMethodName) + " : " + value.ToString(),
                                        selectedCurves[i].StartX, value, 0, true);
                                }
                            }
                        }
                    }

                    break;
            }
        }

        void FormStatistic_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dataEngine.DataSource.WorkStatusChanged -= DataSource_WorkStatusChanged;
            _dataEngine.DataManager.SectionEvent -= DataManager_SectionEvent;
        }

        void FillList(bool needMask)
        {

            DataTable t = _dataEngine.DataManager.Statistic(_method);
            statistic1.XTable = t.Copy();
            statistic1.YTable = t.Copy();
            statistic1.NeedRefreshAll();
            if (needMask)
            {
                MaskColumns();
            }
            else
            {
                statistic1.NeedMask();
            }
        }
        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", Name);
            keyValue.Add("Caption", Text);
            keyValue.Add("Bounds", Classes.PublicMethods.Bounds2Str(Bounds));
            keyValue.Add("Method", ((int)_method).ToString());
            keyValue.Add("Statistic", this.statistic1.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Name = keyValue.GetValueByKey("Name");
            Text = keyValue.GetValueByKey("Caption");
            Bounds = Classes.PublicMethods.RectangleParse(keyValue.GetValueByKey("Bounds"));
            int methodIndex;
            int.TryParse(keyValue.GetValueByKey("Method"), out methodIndex);
            Method = (DataAnalysis.StatisticTypeDefine)methodIndex;
            statistic1.Parse(keyValue.GetValueByKey("Statistic"));

        }

        #endregion
        private void toolStripMenuItem_xAxis_Click(object sender, EventArgs e)
        {
           FormVariablePicker f = new FormVariablePicker(_dataEngine,true,"", statistic1.XColumns);
            if (f.ShowDialog() == DialogResult.OK)
            {
                List<string> columnNames = new List<string>();
                columnNames.Add("SectionIndex");
                columnNames.AddRange(f.SelectedSensorNames);
                columnNames.AddRange(f.SelectedExprNames);
                statistic1.XColumns.Clear();
                statistic1.XColumns.AddRange(columnNames);
                statistic1.NeedMask();
            }

        }

        private void toolStripMenuItem_yAxis_Click(object sender, EventArgs e)
        {
            FormVariablePicker f = new FormVariablePicker(_dataEngine, true, "", statistic1.YColumns);
            if (f.ShowDialog() == DialogResult.OK)
            {
                List<string> columnNames = new List<string>();
                columnNames.Add("SectionIndex");
                columnNames.AddRange(f.SelectedSensorNames);
                columnNames.AddRange(f.SelectedExprNames);
                statistic1.YColumns.Clear();
                statistic1.YColumns.AddRange(columnNames);
                statistic1.NeedMask();
            }

        }

        private void toolStripMenuItem_method_Click(object sender, EventArgs e)
        {
            FormStatisticMethodPicker f = new FormStatisticMethodPicker(Method);
            if (f.ShowDialog() == DialogResult.OK)
            {
                Method = f.SelectedMethod;
                FillList(false);
            }

        }

        private void toolStripButton_move_Click(object sender, EventArgs e)
        {
            statistic1.Graph.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.None;
            this.toolStripButton_analysis.Checked = false;
            this.toolStripButton_move.Checked = true;
            this.toolStripButton_pen.Checked = false;
            this.toolStripButton_selectBandPoint.Checked = false;
            this.toolStripButton_selectLabel.Checked = false;
            this.toolStripButton_zoom.Checked = false;

        }

        private void toolStripButton_analysis_Click(object sender, EventArgs e)
        {
            statistic1.Graph.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectData;
            this.toolStripButton_analysis.Checked = true;
            this.toolStripButton_move.Checked = false;
            this.toolStripButton_pen.Checked = false;
            this.toolStripButton_selectBandPoint.Checked = false;
            this.toolStripButton_selectLabel.Checked = false;
            this.toolStripButton_zoom.Checked = false;


        }

        private void toolStripButton_zoom_Click(object sender, EventArgs e)
        {
            statistic1.Graph.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectArea;
            this.toolStripButton_analysis.Checked = false;
            this.toolStripButton_move.Checked = false;
            this.toolStripButton_pen.Checked = false;
            this.toolStripButton_selectBandPoint.Checked = false;
            this.toolStripButton_selectLabel.Checked = false;
            this.toolStripButton_zoom.Checked = true;

        }

        private void toolStripButton_selectLabel_Click(object sender, EventArgs e)
        {
            statistic1.Graph.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectLabel;
            this.toolStripButton_analysis.Checked = false;
            this.toolStripButton_move.Checked = false;
            this.toolStripButton_pen.Checked = false;
            this.toolStripButton_selectBandPoint.Checked = false;
            this.toolStripButton_selectLabel.Checked =true;
            this.toolStripButton_zoom.Checked = false;

        }

        private void toolStripButton_selectBandPoint_Click(object sender, EventArgs e)
        {
            statistic1.Graph.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.SelectPoint;
            this.toolStripButton_analysis.Checked = false;
            this.toolStripButton_move.Checked = false;
            this.toolStripButton_pen.Checked = false;
            this.toolStripButton_selectBandPoint.Checked = true;
            this.toolStripButton_selectLabel.Checked = false;
            this.toolStripButton_zoom.Checked = false;

        }

        private void toolStripButton_pen_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = statistic1.Graph.HighLighterColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                statistic1.Graph.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.HighLighter;
                this.toolStripButton_analysis.Checked = false;
                this.toolStripButton_move.Checked = false;
                this.toolStripButton_pen.Checked = true;
                this.toolStripButton_selectBandPoint.Checked = false;
                this.toolStripButton_selectLabel.Checked = false;
                this.toolStripButton_zoom.Checked = false;

            }


        }

        private void toolStripButton_lock_Click(object sender, EventArgs e)
        {
            Graph.FormGraphLock f = new WCYDisLab.Graph.FormGraphLock(statistic1.Graph.YAxis.LockRange, statistic1.Graph.Y2Axis.LockRange, statistic1.Graph.XAxis.LockRange);
            if (f.ShowDialog() == DialogResult.OK)
            {
                statistic1.Graph.XAxis.LockRange = f.SelectedXLock;
                statistic1.Graph.Y2Axis.LockRange = f.SelectedY2Lock;
                statistic1.Graph.YAxis.LockRange = f.SelectedYLock;
            }
        }

        private void toolStripButton_showAll_Click(object sender, EventArgs e)
        {
            statistic1.Graph.NeedZoomAuto();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog SavePicDlg = new SaveFileDialog();
            SavePicDlg.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|GIF (*.gif)|*.gif|TIFF (*.tif)|*.tif|EMF (*.emf)|*.emf";
            SavePicDlg.FilterIndex = 2;
            SavePicDlg.RestoreDirectory = true;
            if (SavePicDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Drawing.Imaging.ImageFormat imageFormat = null;

                    if (SavePicDlg.FileName.EndsWith("bmp"))
                    {
                        imageFormat = ImageFormat.Bmp;

                    }
                    else if (SavePicDlg.FileName.EndsWith("jpg"))
                    {
                        imageFormat = ImageFormat.Jpeg;
                    }
                    else if (SavePicDlg.FileName.EndsWith("gif"))
                    {
                        imageFormat = ImageFormat.Gif;
                    }
                    else if (SavePicDlg.FileName.EndsWith("png"))
                    {
                        imageFormat = ImageFormat.Png;
                    }
                    else if (SavePicDlg.FileName.EndsWith("tif"))
                    {
                        imageFormat = ImageFormat.Tiff;
                    }
                    else if (SavePicDlg.FileName.EndsWith("emf"))
                    {
                        imageFormat = ImageFormat.Emf;
                    }
                    Bitmap bmp = new Bitmap(this.Width, this.Height);
                    this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
                    bmp.Save(SavePicDlg.FileName, imageFormat);
                }
                catch (Exception ex)
                {
                    string strMsg = ex.Message.ToString();
                }
            }
        }

        private void toolStripMenuItem_lineName_Click(object sender, EventArgs e)
        {
            FormLineNameSet f = new FormLineNameSet(statistic1 .Graph .GraphCaption);
            if (f.ShowDialog() == DialogResult.OK)
            {
                statistic1.Graph.GraphCaption = f.GraphName;
            }
        }
    }
}
