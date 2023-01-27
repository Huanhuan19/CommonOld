using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;

namespace WCYDisLab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Load += new EventHandler(Form1_Load);
            StartProcess();
        }

        //void Form1_Load(object sender, EventArgs e)
        //{
        //    FormSelectLanguage f = new FormSelectLanguage();
        //    f.ShowDialog();
        //}
        public Form1(string ipaddress, string filename)
        {
            InitializeComponent();
            StartProcess();
            this.Text += " - " + ipaddress;
            LoadDataFromFile(filename);
            this.toolStripDropDownButton_onlineStatus.Visible = false;
        }
        void toolStripMenuItem_edit_DropDownOpening(object sender, EventArgs e)
        {
            for (int i = toolStripMenuItem_edit.DropDownItems.Count - 1; i >= 0; i--)
            {
                if (i >= 2)
                {
                    toolStripMenuItem_edit.DropDownItems.RemoveAt(i);
                }
            }
            for (int i = 0; i < _dataEngine.DataManager.DataSections.Count; i++)
            {
                //ToolStripMenuItem item = new ToolStripMenuItem("删除第" + (i + 1).ToString() + "次数据[" +_dataEngine.DataManager.DataSections[i].Caption +"]");
                ToolStripMenuItem item = new ToolStripMenuItem(_resourceManager.GetString("RemoveTimeMSG") + (i + 1).ToString() + "[" + _dataEngine.DataManager.DataSections[i].Caption + "]");
                item.Tag = _dataEngine.DataManager.DataSections[i].Name;
                item.Click += new EventHandler(item_Click);
                toolStripMenuItem_edit.DropDownItems.Add(item);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Tag != null)
            {
                string sectionName = ((ToolStripMenuItem)sender).Tag.ToString();
                if (MessageBox.Show(((ToolStripMenuItem)sender).Text + _resourceManager.GetString("ConfirmContinueMSG"), _resourceManager.GetString("RemoveDataMSG"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {

                    _dataEngine.DataManager.RemoveDataSection(sectionName);
                }
            }
        }

        void toolStripMenuItem_Windows_DropDownOpening(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                Form activeChild = this.ActiveMdiChild;
                ActivateMdiChild(null);
                ActivateMdiChild(activeChild);
            }

        }



        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                case CloseReason.FormOwnerClosing:
                case CloseReason.None:
                    if (MessageBox.Show(_resourceManager.GetString("ConfirmQuitMSG"), _resourceManager.GetString("ConfirmQuit"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        _dataEngine.Dispose();
                    }
                    break;
                default:
                    _dataEngine.Dispose();
                    break;
            }
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.Form1", Assembly.GetExecutingAssembly());
        string _netPropsFilename = "\\NetProps.set";
        DisLabComm.Address _address;
        string _defaultIntervalCollectionFileName = Application.StartupPath + "\\frequency.set";
        string _defaultSensorDefineFileName = Application.StartupPath + "\\sensors.set";
        string DefaultSensorDefineFileName
        {
            get
            {
                if (FormSelectLanguage._is6p6c)
                {
                    return Application.StartupPath + "\\" + System.Threading.Thread.CurrentThread.CurrentUICulture.Name + "\\sensors.set";
                }
                else
                    return Application.StartupPath + "\\" + System.Threading.Thread.CurrentThread.CurrentUICulture.Name + "\\USBsensors.set";
            }
        }
        Classes.DataEngine _dataEngine;
        bool _needMaskConnected = false, _needMaskUnConnected = false, _needMaskOnline = false, _needMaskOffLine = false, _needMaskStarting = false,
             _needMaskStoped = false, _needMaskAdjusting = false, _needMaskReplaying = false, _needMaskStatus = false;
        bool _needSendData = false;
        int _thisPort = 12344;
        DateTime _netLastSendTime = DateTime.Now;
        DisLabComm.Listener _listener = new DisLabComm.Listener();
        #endregion

        #region Methods
        void StartProcess()
        {
            Initialize();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.menuStrip1.MdiWindowListItem = this.toolStripMenuItem_window;
            this.toolStripMenuItem_window.DropDownOpening += new EventHandler(toolStripMenuItem_Windows_DropDownOpening);
            this.toolStripMenuItem_edit.DropDownOpening += new EventHandler(toolStripMenuItem_edit_DropDownOpening);
            timer1.Start();
            if (FormSelectLanguage._isBlueToothUsed)//2021 添加蓝牙搜索是否显示
            {
                toolStripButton1.Visible = true;
            }
            else
            {
                toolStripButton1.Visible = false;
            }
        }


        void Initialize()
        {
            string languageName = string.Empty;
            if (KeyValue.FileStream.Load(Application.StartupPath + "\\Culture.set", ref languageName))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(languageName);
            }
            _dataEngine = new WCYDisLab.Classes.DataEngine();
            _dataEngine.LoadSensorCollection(DefaultSensorDefineFileName);

            _dataEngine.ConnectChanged += new WCYDataSource.ConnectedHandler(_dataEngine_ConnectChanged);
            _dataEngine.DataEngineEvent += new WCYDisLab.Classes.DataEngineHandler(_dataEngine_DataEngineEvent);
            _dataEngine.WorkStatusChanged += new WCYDataSource.StartStopHandler(_dataEngine_WorkStatusChanged);
            _dataEngine.OfflineEvent += new WCYDisLab.Classes.OfflineHandler(_dataEngine_OfflineEvent);
            _dataEngine.LoadDefaultIntervalCollection(_defaultIntervalCollectionFileName);
            
            _dataEngine.MaskSensor();
            LoadProperties();
            variablesPad1.Initialize(_dataEngine);
            _needMaskStatus = true;
            if (_dataEngine.Connected)
            {
                _needMaskConnected = true;

            }
            else
            {
                _needMaskUnConnected = true;
            }
            _listener.DataRecieved += new DisLabComm.ListenerHandler(_listener_DataRecieved);

        }

        void _dataEngine_OfflineEvent(object sender, WCYDisLab.Classes.OfflineEventArgs e)
        {
            if (e.IsOffline)
            {
                _needMaskOffLine = true;
            }
            else
            {
                _needMaskOnline = true;
            }
        }

        void _dataEngine_WorkStatusChanged(object sender, WCYDataSource.StartStopEventArgs e)
        {
            if (e.IsStart)
            {
                _needMaskStarting = true;
            }
            else
            {
                _needMaskStoped = true;
            }
        }


        void _dataEngine_DataEngineEvent(object sender, WCYDisLab.Classes.DataEngineEventArgs e)
        {
            switch (e.EventType)
            {
                case WCYDisLab.Classes.DataEngineEventType.New:
                case WCYDisLab.Classes.DataEngineEventType.Load:
                case WCYDisLab.Classes.DataEngineEventType.Recieved:
                    _dataEngine.OffLine = true;
                    break;
            }
        }

        void _dataEngine_ConnectChanged(object sender, WCYDataSource.ConnectEventArgs e)
        {
            if (e.Connected)
            {
                _needMaskConnected = true;
            }
            else
            {
                _needMaskUnConnected = true;
            }
        }
        void MaskStatus()
        {
            this.toolStripStatusLabel_endProps.Text = _dataEngine.WorkArguments.EndProperties.DescriptionString;
            this.toolStripStatusLabel_frequency.Text = _dataEngine.DataManager.DataSectionActive.TimeLineProps.Frequency + "HZ";
            this.toolStripStatusLabel_shotProps.Text = _dataEngine.WorkArguments.PhotoGateProperties.DescriptionString;
            this.toolStripStatusLabel_startProps.Text = _dataEngine.WorkArguments.StartProperties.DescriptionString;
            this.toolStripStatusLabel_variables.Text = variablesPad1.Visible ? _resourceManager.GetString("HidePad") : _resourceManager.GetString("ShowPad");
            this.toolStripStatusLabel_ip.Text = _address.IPString;
            this.toolStripStatusLabel_port.Text = _address.Port.ToString();
            this.toolStripStatusLabel_netConnect.Text = _dataEngine.NetOpen ? _resourceManager.GetString("Connect") : _resourceManager.GetString("Unconnect");
        }

        void MaskConnected()
        {
            this.toolStripButton_adjust.Enabled = !_dataEngine.OffLine;
            this.toolStripButton_shot.Enabled = false;
            this.toolStripButton_start.Enabled = !_dataEngine.OffLine;
            this.toolStripButton_stop.Enabled = false;
            //this.toolStripDropDownButton_onlineStatus.Enabled = true;
        }
        void MaskUnConnected()
        {
            this.toolStripButton_adjust.Enabled = false;
            this.toolStripButton_shot.Enabled = false;
            this.toolStripButton_start.Enabled = false;
            this.toolStripButton_stop.Enabled = false;
            //this.toolStripDropDownButton_onlineStatus.Enabled = false;
        }
        void MaskOnLine()
        {
            this.toolStripButton_adjust.Enabled = _dataEngine.Connected;
            this.toolStripButton_shot.Enabled = false;
            this.toolStripButton_start.Enabled = _dataEngine.Connected;
            this.toolStripButton_stop.Enabled = false;
            toolStripDropDownButton_onlineStatus.Text = _resourceManager.GetString("Online");
            toolStripDropDownButton_onlineStatus.Image = Properties.Resources.chinaz45;
            toolStripMenuItem_online.Checked = true;
            toolStripMenuItem_offline.Checked = false;

        }
        void MaskOffLine()
        {
            this.toolStripButton_adjust.Enabled = false;
            this.toolStripButton_shot.Enabled = false;
            this.toolStripButton_start.Enabled = false;
            this.toolStripButton_stop.Enabled = false;
            toolStripDropDownButton_onlineStatus.Text = _resourceManager.GetString("Offline");
            toolStripDropDownButton_onlineStatus.Image = Properties.Resources.chinaz81;
            toolStripMenuItem_online.Checked = false;
            toolStripMenuItem_offline.Checked = true;
        }
        void MaskAdjusting()
        {
            this.toolStripButton_adjust.Enabled = false;
            this.toolStripButton_shot.Enabled = false;
            this.toolStripButton_start.Enabled = false;
            this.toolStripButton_stop.Enabled = false;

        }
        void MaskStarted()
        {
            this.toolStripButton_adjust.Enabled = false;
            this.toolStripButton_shot.Enabled = _dataEngine.WorkArguments.PhotoGateProperties.Available && (_dataEngine.WorkArguments.PhotoGateProperties.OpenMode == ExperimentProperties.PhotoGateMode.Manual);
            this.toolStripButton_start.Enabled = false;
            this.toolStripButton_stop.Enabled = true;
            this.toolStripStatusLabel_process.Visible = true;
            this.toolStripProgressBar1.Visible = true;

        }
        void MaskStoped()
        {
            this.toolStripButton_adjust.Enabled = true;
            this.toolStripButton_shot.Enabled = false;
            this.toolStripButton_start.Enabled = true;
            this.toolStripButton_stop.Enabled = false;
            this.toolStripStatusLabel_process.Visible = false;
            this.toolStripProgressBar1.Visible = false;

        }
        void MaskReplay()
        {
            this.toolStripButton_adjust.Enabled = false;
            this.toolStripButton_shot.Enabled = false;
            this.toolStripButton_start.Enabled = false;
            this.toolStripButton_stop.Enabled = true;
        }

        void SendSignal(int port)
        {
            DisLabComm.Sender sender = new DisLabComm.Sender();

            DisLabComm.Address localAddress = new DisLabComm.Address();
            localAddress.Port = port;

            if (_dataEngine.DataSource.Working)
            {
                sender.Send(_address.IP, _address.Port, DisLabComm.CommandPack.WorkingCommand(localAddress));
            }
            else
            {
                int[] pack = DisLabComm.CommandPack.ConnectCommand(localAddress, _dataEngine.DataSource.PortCollection.SensorTypes);

                //sender.Send(_address.IP, _address.Port, pack);
            }
            _netLastSendTime = DateTime.Now;
        }
        void startListener(int port)
        {
            DisLabComm.Address adress = new DisLabComm.Address();
            adress.Port = port;
            _listener.Start(adress.IP, adress.Port);
        }
        void StopListener()
        {
            _listener.Stop();
        }
        void _listener_DataRecieved(object sender, DisLabComm.ListenerEventArgs e)
        {
            if (e.Buffer.Length > 2)
            {
                byte length = e.Buffer[0];
                if (e.Buffer[0] == (byte)DisLabComm.CommandDefine.GetData)
                {
                    _needSendData = true;
                }
            }
        }
        void SendData()
        {
            if (!_dataEngine.DataManager.TempleteEqualsActive)
            {
                _dataEngine.DataManager.DataSectionActiveToTemplete(false);
            }
            string filename = System.IO.Path.GetTempFileName();
            _dataEngine.UIProps.ExperimentFilename = filename;
            StoreWindows();
            if (_dataEngine.OffLine)
            {
                _dataEngine.DataManager.DataSectionTempleteToActive(true);
            }
            else
            {
                _dataEngine.DataManager.DataSectionActiveToTemplete();
            }
            _dataEngine.SaveDataEngine(filename);
            DisLabComm.Address localAddress = new DisLabComm.Address();
            localAddress.Port = _thisPort;

            FormSendData f = new FormSendData(filename, _address, localAddress);
            f.ShowDialog();
        }
        #endregion

        #region Serialize
        void New()
        {
            ClearWindows();
            _dataEngine.New();
            if (_dataEngine.Connected)
            {
                _needMaskConnected = true;

            }
            else
            {
                _needMaskUnConnected = true;
            }
            if (_dataEngine.OffLine)
            {
                _needMaskOffLine = true;
            }
            else
            {
                _needMaskOnline = true;
            }
            _needMaskStatus = true;
        }
        void LoadDataFromFile(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                _dataEngine.LoadDataEngine(filename);
                RestoreWindows();
                OffLine(true);
                _needMaskStatus = true;
            }
        }
        void LoadData()
        {
            openFileDialog_data.FileName = _dataEngine.UIProps.ExperimentFilename;
            if (openFileDialog_data.ShowDialog() == DialogResult.OK)
            {
                _dataEngine.LoadDataEngine(openFileDialog_data.FileName);
                RestoreWindows();
                OffLine(true);
                _needMaskStatus = true;
                //bool withoutSensors = false;
                //if (!_dataEngine.DataManager.TempleteEqualsActive)
                //{
                //    if (MessageBox.Show("实际连接的传感器与模板要求的传感器存在差异，需要更新模板设置的传感器配置吗？", "传感器设置", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                //    {
                //        withoutSensors = false;
                //    }
                //    else
                //    {
                //        withoutSensors = true;
                //    }
                //}
                //OffLine(withoutSensors);
                //_refreshStatusBar = true;
            }
        }
        void LoadData(string path)
        {
            openFileDialog_data.InitialDirectory = path;
            if (openFileDialog_data.ShowDialog() == DialogResult.OK)
            {
                _dataEngine.LoadDataEngine(openFileDialog_data.FileName);
                RestoreWindows();
                OffLine(true);
                _needMaskStatus = true;
                //bool withoutSensors = false;
                //if (!_dataEngine.DataManager.TempleteEqualsActive)
                //{
                //    if (MessageBox.Show("实际连接的传感器与模板要求的传感器存在差异，需要更新模板设置的传感器配置吗？", "传感器设置", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                //    {
                //        withoutSensors = false;
                //    }
                //    else
                //    {
                //        withoutSensors = true;
                //    }
                //}
                //OffLine(withoutSensors);
                //_refreshStatusBar = true;
            }
        }
        void SaveData()
        {
            if (string.IsNullOrEmpty(_dataEngine.UIProps.ExperimentFilename))
            {
                SaveAsData();
            }
            else
            {
                StoreWindows();
                if (_dataEngine.OffLine)
                {
                    _dataEngine.DataManager.DataSectionTempleteToActive(true);
                }
                else
                {
                    _dataEngine.DataManager.DataSectionActiveToTemplete();
                }
                _dataEngine.SaveDataEngine(_dataEngine.UIProps.ExperimentFilename);
            }
        }
        void SaveAsData()
        {
            if (saveFileDialog_data.ShowDialog() == DialogResult.OK)
            {
                _dataEngine.UIProps.ExperimentFilename = saveFileDialog_data.FileName;
                StoreWindows();
                if (_dataEngine.OffLine)
                {
                    _dataEngine.DataManager.DataSectionTempleteToActive(true);
                }
                else
                {
                    _dataEngine.DataManager.DataSectionActiveToTemplete();
                }
                _dataEngine.SaveDataEngine(saveFileDialog_data.FileName);
            }
        }
        void OnLine()
        {
            if (!_dataEngine.DataManager.TempleteEqualsActive)
            {
                //FormTempleteVSActive f = new FormTempleteVSActive(_dataEngine);
                //if (f.ShowDialog() == DialogResult.OK)
                //{
                //}
                _dataEngine.DataManager.DataSectionTempleteToActive(true);
            }
            //_dataEngine.MaskSensorSet();
            if (!_dataEngine.DataManager.SensorMatch)
            {
                MessageBox.Show("实际连接的传感器与模板设置存在差异。", "传感器设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            _dataEngine.OffLine = false;
            _needMaskStatus = true;

        }
        void OffLine(bool withoutSensors)
        {
            if (!_dataEngine.DataManager.TempleteEqualsActive)
            {
                //FormTempleteVSActive f = new FormTempleteVSActive(_dataEngine);
                //if (f.ShowDialog() == DialogResult.OK)
                //{
                //}
                _dataEngine.DataManager.DataSectionActiveToTemplete(withoutSensors);
            }
            _dataEngine.OffLine = true;
            _needMaskStatus = true;

        }
        void OffLine()
        {
            if (!_dataEngine.DataManager.TempleteEqualsActive)
            {
                //FormTempleteVSActive f = new FormTempleteVSActive(_dataEngine);
                //if (f.ShowDialog() == DialogResult.OK)
                //{
                //}
                if (MessageBox.Show("实际连接的传感器与模板设置存在差异。要将实际连接的传感器情况更新到模板中吗？", "传感器设置", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    _dataEngine.DataManager.DataSectionActiveToTemplete(false);
                }
                else
                {
                    _dataEngine.DataManager.DataSectionActiveToTemplete(true);
                }
            }
            _dataEngine.OffLine = true;
            _needMaskStatus = true;

        }

        void ClearWindows()
        {
            for (int i = MdiChildren.Length - 1; i >= 0; i--)
            {
                MdiChildren[i].Close();
            }

        }
        void RestoreWindows()
        {
            ClearWindows();
            foreach (Classes.WindowProps window in _dataEngine.UIProps.Windows)
            {
                switch (window.WindowType)
                {
                    default:
                    case Classes.WindowType.Other:
                        break;
                    case Classes.WindowType.Camera:
                        break;
                    case Classes.WindowType.DigitalMeter:
                        DigitalMeter.FormDigitalMeter formDigitalMater = new WCYDisLab.DigitalMeter.FormDigitalMeter(_dataEngine, window.WindowInfo);
                        formDigitalMater.MdiParent = this;
                        formDigitalMater.Show();
                        break;
                    case Classes.WindowType.Graph:
                        Graph.FormGraph formGraph = new WCYDisLab.Graph.FormGraph(_dataEngine, window.WindowInfo);
                        //formGraph.Dock = DockStyle.Fill;
                        formGraph.MdiParent = this;
                        formGraph.Show();
                        break;
                    case Classes.WindowType.Guid:
                        Guid.FormGuid formGuid = new WCYDisLab.Guid.FormGuid(window.WindowInfo);
                        formGuid.MdiParent = this;
                        //formGuid.Dock = DockStyle.Fill;
                        formGuid.Show();
                        break;
                    case Classes.WindowType.Meter:
                        Meter.FormMeter meter = new WCYDisLab.Meter.FormMeter(_dataEngine, window.WindowInfo);
                        meter.MdiParent = this;
                        //meter.Dock = DockStyle.Fill;
                        meter.Show();
                        break;
                    case Classes.WindowType.QuickViewer:
                        break;
                    case Classes.WindowType.Report:
                        Report.FormReport report = new WCYDisLab.Report.FormReport(window.WindowInfo);
                        report.MdiParent = this;
                        //report.Dock = DockStyle.Fill;
                        report.Show();
                        break;
                    case Classes.WindowType.Statistic:
                        Statistic.FormStatistic statistic = new WCYDisLab.Statistic.FormStatistic(_dataEngine, window.WindowInfo);
                        statistic.MdiParent = this;
                        //statistic.Dock = DockStyle.Fill;
                        statistic.Show();
                        break;
                    case Classes.WindowType.Table:
                        Table.FormTable table = new WCYDisLab.Table.FormTable(_dataEngine, window.WindowInfo);
                        table.MdiParent = this;
                        //table.Dock = DockStyle.Fill;
                        table.Show();
                        break;
                    case Classes.WindowType.Thermometer:
                        break;
                    case Classes.WindowType.Vector:
                        break;
                    case Classes.WindowType.VideoPlayer:
                        break;
                }
            }
        }
        void StoreWindows()
        {
            _dataEngine.UIProps.Windows.Clear();
            foreach (Form form in MdiChildren)
            {
                if (form.GetType() == Type.GetType("WCYDisLab.DigitalMeter.FormDigitalMeter"))
                {
                    _dataEngine.UIProps.Windows.Add(new Classes.WindowProps(Classes.WindowType.DigitalMeter, ((DigitalMeter.FormDigitalMeter)form).ToString()));
                }
                else if (form.GetType() == Type.GetType("WCYDisLab.Graph.FormGraph"))
                {
                    _dataEngine.UIProps.Windows.Add(new Classes.WindowProps(Classes.WindowType.Graph, ((Graph.FormGraph)form).ToString()));
                }
                else if (form.GetType() == Type.GetType("WCYDisLab.Guid.FormGuid"))
                {
                    _dataEngine.UIProps.Windows.Add(new Classes.WindowProps(Classes.WindowType.Guid, ((Guid.FormGuid)form).ToString()));
                }
                else if (form.GetType() == Type.GetType("WCYDisLab.Meter.FormMeter"))
                {
                    _dataEngine.UIProps.Windows.Add(new Classes.WindowProps(Classes.WindowType.Meter, ((Meter.FormMeter)form).ToString()));
                }
                else if (form.GetType() == Type.GetType("WCYDisLab.Report.FormReport"))
                {
                    _dataEngine.UIProps.Windows.Add(new Classes.WindowProps(Classes.WindowType.Report, ((Report.FormReport)form).ToString()));
                }
                else if (form.GetType() == Type.GetType("WCYDisLab.Statistic.FormStatistic"))
                {
                    _dataEngine.UIProps.Windows.Add(new Classes.WindowProps(Classes.WindowType.Statistic, ((Statistic.FormStatistic)form).ToString()));
                }
                else if (form.GetType() == Type.GetType("WCYDisLab.Table.FormTable"))
                {
                    _dataEngine.UIProps.Windows.Add(new Classes.WindowProps(Classes.WindowType.Table, ((Table.FormTable)form).ToString()));
                }
            }

        }
        bool LoadProperties()
        {
            string filename = Application.StartupPath + _netPropsFilename;
            bool success = false;
            try
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(filename);
                _address = new DisLabComm.Address(reader.ReadToEnd());
                reader.Close();
                //RefreshDataSourceSensorTypeClassList();
                success = true;
            }
            catch
            {
                _address = new DisLabComm.Address();
                success = false;
            }
            return success;

        }
        public bool SaveProperties()
        {
            string filename = Application.StartupPath + _netPropsFilename;
            bool success = false;
            try
            {
                System.IO.StreamWriter writer = System.IO.File.CreateText(filename);
                if (writer != null)
                {
                    writer.Write(_address.ToString());
                    writer.Close();
                    //RefreshDataSourceSensorTypeClassList();
                    success = true;

                }
            }
            catch
            {
                success = false;

            }
            return success;
        }


        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", Name);
            keyValue.Add("Caption", Text);
            keyValue.Add("Bounds", Classes.PublicMethods.Bounds2Str(Bounds));
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Name = keyValue.GetValueByKey("Name");
            //WindowCaption = keyValue.GetValueByKey("Caption");
            Bounds = Classes.PublicMethods.RectangleParse(keyValue.GetValueByKey("Bounds"));
        }

        #endregion

        private void toolStripButton_graph_Click(object sender, EventArgs e)
        {
            Graph.FormGraph f = new WCYDisLab.Graph.FormGraph(_dataEngine);
            f.MdiParent = this;
            f.Dock = DockStyle.Fill;
            f.Show();
        }

        private void toolStripStatusLabel_variables_Click(object sender, EventArgs e)
        {
            variablesPad1.Visible = !variablesPad1.Visible;
            if (variablesPad1.Visible)
            {
                toolStripStatusLabel_variables.Text = _resourceManager.GetString("HidePad");
            }
            else
            {
                toolStripStatusLabel_variables.Text = _resourceManager.GetString("ShowPad");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //_dataEngine.QueryType();
            if (_needMaskAdjusting)
            {
                _needMaskAdjusting = false;
                MaskAdjusting();
            }
            if (_needMaskConnected)
            {
                _needMaskConnected = false;
                MaskConnected();
            }
            if (_needMaskOffLine)
            {
                _needMaskOffLine = false;
                MaskOffLine();
            }
            if (_needMaskOnline)
            {
                _needMaskOnline = false;
                MaskOnLine();
            }
            if (_needMaskReplaying)
            {
                _needMaskReplaying = false;
                MaskReplay();
            }
            if (_needMaskStarting)
            {
                _needMaskStarting = false;
                MaskStarted();
            }
            if (_needMaskStoped)
            {
                _needMaskStoped = false;
                MaskStoped();
                _dataEngine.Started = false;
            }
            if (_needMaskUnConnected)
            {
                _needMaskUnConnected = false;
                MaskUnConnected();
            }
            if (_needMaskStatus)
            {
                _needMaskStatus = false;
                MaskStatus();
            }
            if (_dataEngine.NetOpen)
            {
                if (DateTime.Now - _netLastSendTime >= new TimeSpan(0, 0, 5))
                {
                    SendSignal(_thisPort);//扩展ID需要屏蔽了send方法
                    startListener(_thisPort);
                }
            }
            if (_needSendData)
            {
                _needSendData = false;
                SendData();
            }
            if (_dataEngine.Connected && !_dataEngine.OffLine && _dataEngine.ExInfo.ActionType == Classes.ActionType.Started)
            {
                double passedSeconds = _dataEngine.PassedSeconds;
                double totalSeconds = _dataEngine.WorkArguments.EndProperties.ReservedSeconds;
                int rate = 0;
                if (passedSeconds >= 0 && totalSeconds > 0)
                {
                    rate = (int)Math.Round(passedSeconds / totalSeconds * 100, 0);
                }
                this.toolStripProgressBar1.Value = (rate >= this.toolStripProgressBar1.Minimum && rate <= toolStripProgressBar1.Maximum) ? rate : toolStripProgressBar1.Maximum;
                if (rate == this.toolStripProgressBar1.Maximum)
                {
                    _dataEngine.Stop();
                }
                this.toolStripStatusLabel_process.Text = "[" + Math.Round(passedSeconds, 2).ToString("0.00") + "s/" + Math.Round(totalSeconds, 2).ToString() + "s][" + _dataEngine.DataManager.DataSections.Count + "]";
            }
        }

        private void toolStripMenuItem_online_Click(object sender, EventArgs e)
        {

            _dataEngine.OffLine = false;

        }

        private void toolStripMenuItem_offline_Click(object sender, EventArgs e)
        {

            _dataEngine.OffLine = true;

        }


        private void toolStripButton_table_Click(object sender, EventArgs e)
        {
            Table.FormTable f = new WCYDisLab.Table.FormTable(_dataEngine);
            f.MdiParent = this;
            f.Dock = DockStyle.Fill;
            f.Show();
        }

        private void toolStripButton_digital_Click(object sender, EventArgs e)
        {
            DigitalMeter.FormDigitalMeter f = new WCYDisLab.DigitalMeter.FormDigitalMeter(_dataEngine);
            f.MdiParent = this;
            f.Show();
        }

        private void toolStripButton_meter_Click(object sender, EventArgs e)
        {
            Meter.FormMeter f = new WCYDisLab.Meter.FormMeter(_dataEngine);
            f.MdiParent = this;
            f.Show();
        }

        private void toolStripButton_resultAnalysis_Click(object sender, EventArgs e)
        {
            Statistic.FormStatistic f = new WCYDisLab.Statistic.FormStatistic(_dataEngine);
            f.MdiParent = this;
            f.Dock = DockStyle.Fill;
            f.Show();
        }

        private void toolStripButton_guid_Click(object sender, EventArgs e)
        {
            Guid.FormGuid f = new WCYDisLab.Guid.FormGuid();
            f.MdiParent = this;
            f.Dock = DockStyle.Fill;
            f.Show();
        }

        private void toolStripButton_report_Click(object sender, EventArgs e)
        {
            Report.FormReport f = new WCYDisLab.Report.FormReport();
            f.MdiParent = this;
            f.Dock = DockStyle.Fill;
            f.Show();
        }

        private void toolStripButton_adjust_Click(object sender, EventArgs e)
        {
            FormAdjust f = new FormAdjust(_dataEngine);
            f.ShowDialog();
        }

        private void toolStripButton_replay_Click(object sender, EventArgs e)
        {
            bool contains = false;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].GetType() == Type.GetType("WCYDisLab.FormReplay"))
                {
                    contains = true;
                    break;
                }
            }
            if (!contains)
            {
                FormReplay f = new FormReplay(_dataEngine);
                f.Show();
            }
        }

        private void toolStripMenuItem_cascade_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Length; i++)
            {
                MdiChildren[i].Dock = DockStyle.None;
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void toolStripMenuItem_parrel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Length; i++)
            {
                MdiChildren[i].Dock = DockStyle.None;
            }
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void toolStripMenuItem_new_Click(object sender, EventArgs e)
        {
            New();
        }

        private void toolStripMenuItem_open_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void toolStripMenuItem_save_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void toolStripMenuItem_saveAs_Click(object sender, EventArgs e)
        {
            SaveAsData();
        }

        private void toolStripButton_start_Click(object sender, EventArgs e)
        {
            IndexX = 0;//采集频率20 0000Hz添加
            _dataEngine.Started = true;
            if (_dataEngine.Connected && !_dataEngine.OffLine && !_dataEngine.Adjusting)
            {
                _needMaskStarting = true;
                _dataEngine.Start();
            }

        }

        private void toolStripButton_shot_Click(object sender, EventArgs e)
        {
            _dataEngine.OpenPhotoGate();

        }

        private void toolStripButton_stop_Click(object sender, EventArgs e)
        {
            //_dataEngine.Started = false;
            if (_dataEngine.Connected && !_dataEngine.OffLine && !_dataEngine.Adjusting)
            {
                _needMaskStoped = true;
                _dataEngine.Stop();
            }

        }



        private void toolStripMenuItem_about_Click(object sender, EventArgs e)
        {
            FormAbout f = new FormAbout();
            f.ShowDialog();
        }

        private void toolStripMenuItem_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem_localHelp_Click(object sender, EventArgs e)
        {
            FormHelp f = new FormHelp();
            f.Show();
        }

        private void toolStripMenuItem_netHelp_Click(object sender, EventArgs e)
        {
            FormHelp f = new FormHelp("http://www.weichengya.cn/");
            f.Show();

        }

        private void toolStripMenuItem_search_Click(object sender, EventArgs e)
        {
            FormHelp f = new FormHelp("Http://www.goole.com");
            f.Show();
        }

        private void toolStripMenuItem_workArguments_Click(object sender, EventArgs e)
        {
            FormExProps f = new FormExProps(_dataEngine);
            if (f.ShowDialog() == DialogResult.OK)
            {
                _dataEngine.WorkArguments.StartProperties.Parse(f.SelectedStartProps.ToString());
                _dataEngine.WorkArguments.EndProperties.Parse(f.SelectedEndProps.ToString());
                _dataEngine.WorkArguments.PhotoGateProperties.Parse(f.SelectedGateProps.ToString());
                _needMaskStatus = true;

            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                FormBluetoothWarch f = new FormBluetoothWarch(_dataEngine);
                f.ShowDialog();
            }
            catch (Exception exc)
            {

                throw;
            }

        }

        public static bool IsVoice = false;
        public static long IndexX = 0;
        private void toolStripMenuItem_selectFrequency_Click(object sender, EventArgs e)
        {
            IsVoice = false;
            FormFrequencySet f = new FormFrequencySet(_dataEngine.WorkArguments.IntervalCollection, _dataEngine.WorkArguments.IntervalCollection.GetIndexByShiftIndex(_dataEngine.CurrentDataSection.TimeLineProps.ShiftIndex));
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (f.SelectedIndex >= 0 && f.SelectedIndex < _dataEngine.WorkArguments.IntervalCollection.IntervalDefines.Count)
                {
                    ExperimentProperties.IntervalDefine intervalDefine = f.SelectedIntervalDefine;
                    if (intervalDefine.Interval >= 0.0001)
                    {
                        _dataEngine.DataManager.DataSectionTemplete.TimeLineProps.ShiftIndex = intervalDefine.ShiftIndex;
                        _dataEngine.DataManager.DataSectionActive.TimeLineProps.ShiftIndex = intervalDefine.ShiftIndex;
                        _dataEngine.DataManager.DataSectionTemplete.TimeLineProps.Interval = intervalDefine.Interval;
                        _dataEngine.DataManager.DataSectionActive.TimeLineProps.Interval = intervalDefine.Interval;
                        this.toolStripStatusLabel_frequency.Text = Math.Round(_dataEngine.CurrentDataSection.TimeLineProps.Frequency, 0).ToString() + "HZ";

                        _dataEngine.Frequence = Convert.ToInt32(1 / intervalDefine.Interval);//USB
                    }
                    else//10 0000HZ
                    {
                        _dataEngine.DataManager.DataSectionTemplete.TimeLineProps.ShiftIndex = 17;
                        _dataEngine.DataManager.DataSectionActive.TimeLineProps.ShiftIndex = 17;
                        _dataEngine.DataManager.DataSectionTemplete.TimeLineProps.Interval = 0.000005;
                        _dataEngine.DataManager.DataSectionActive.TimeLineProps.Interval = 0.000005;
                        this.toolStripStatusLabel_frequency.Text = Math.Round(_dataEngine.CurrentDataSection.TimeLineProps.Frequency, 0).ToString() + "HZ";
                        IsVoice = true;
                    }
                }
            }
        }

        private void toolStripMenuItem_sensorDefines_Click(object sender, EventArgs e)
        {
            FormPassword formPassword = new FormPassword();
            if (formPassword.ShowDialog() == DialogResult.OK)
            {
                FormSensorCollection f = new FormSensorCollection(_dataEngine, _dataEngine.SensorCollection);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _dataEngine.SensorCollection.Parse(f.SelectedSensorCollection.ToString());
                    if (!_dataEngine.SaveSensorCollection(DefaultSensorDefineFileName))
                    {
                        MessageBox.Show(_resourceManager.GetString("SaveSensorDefineFailMSG"));
                    }
                }
            }
        }

        private void toolStripMenuItem_intervalSet_Click(object sender, EventArgs e)
        {
            FormPassword formPassword = new FormPassword();
            if (formPassword.ShowDialog() == DialogResult.OK)
            {
                FormIntervalCollection f = new FormIntervalCollection(_dataEngine.WorkArguments.IntervalCollection);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _dataEngine.WorkArguments.IntervalCollection.Parse(f.SelectedIntervalCollection.ToString());
                    if (!_dataEngine.SaveDefaultIntervalCollection(_defaultIntervalCollectionFileName))
                    {
                        MessageBox.Show(_resourceManager.GetString("SaveIntervalSetFailMSG"));
                    }
                }
            }
        }

        private void toolStripMenuItem_netProps_Click(object sender, EventArgs e)
        {
            FormPassword formPassword = new FormPassword();
            if (formPassword.ShowDialog() == DialogResult.OK)
            {

                FormNetProps f = new FormNetProps(_address.ToString(), _dataEngine.NetOpen, _thisPort);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _address = new DisLabComm.Address(f.SelectedAddress.ToString());
                    _thisPort = f.SelectedThisPort;
                    SaveProperties();
                    _dataEngine.NetOpen = f.SelectedNetOpen;
                    _needMaskStatus = true;
                    DisLabComm.Address localAddress = new DisLabComm.Address();
                    _listener.Start(localAddress.IP, _thisPort);
                }
                else
                {
                    _listener.Stop();
                }
            }
        }

        private void toolStripMenuItem_remoeAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_resourceManager.GetString("ClearDataMSG"), _resourceManager.GetString("RemoveDataMSG"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                _dataEngine.DataManager.ClearDataSection();
            }

        }

        private void toolStripMenuItem_SetBlueCom_Click(object sender, EventArgs e)
        {
            FormBlueComSet f = new FormBlueComSet();
            f.ShowDialog();
        }

    }
}
