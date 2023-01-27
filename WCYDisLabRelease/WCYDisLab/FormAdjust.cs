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
namespace WCYDisLab
{
    public partial class FormAdjust : Form
    {
        public FormAdjust(Classes.DataEngine dataEngine)
        {
            InitializeComponent();
            timer1.Start();
            _dataEngine = dataEngine;
            _dataEngine.ConnectChanged += new WCYDataSource.ConnectedHandler(_dataEngine_ConnectChanged);
            _dataEngine.OfflineEvent += new WCYDisLab.Classes.OfflineHandler(_dataEngine_OfflineEvent);
            _dataEngine.PortChanged += new WCYDataSource.PortCollectionHandler(_dataEngine_PortChanged);
            _dataEngine.DataSource.ValueChanged +=new WCYDataSource.ValueHandler(DataSource_ValueChanged);
            _dataEngine.WorkStatusChanged += new WCYDataSource.StartStopHandler(_dataEngine_WorkStatusChanged);
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
            FillList();
            FormClosing += new FormClosingEventHandler(FormAdjust_FormClosing);
        }

        void DataSource_ValueChanged(object sender, WCYDataSource.ValueEventArgs e)
        {
            for (int i = 0; i < _records.Count; i++)
            {
                if (_records[i].Index == e.Index)
                {
                    _records[i].Add(e.Value);
                }
            }

        }

        void FormAdjust_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dataEngine.ConnectChanged -= _dataEngine_ConnectChanged;
            _dataEngine.OfflineEvent -= _dataEngine_OfflineEvent;
            _dataEngine.PortChanged -= _dataEngine_PortChanged;
            _dataEngine.DataSource.ValueChanged -= DataSource_ValueChanged;
            _dataEngine.WorkStatusChanged -= _dataEngine_WorkStatusChanged;
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                double value;
                double.TryParse(listView1.Items[index].SubItems[1].Text, out value);
                FormInputDouble f = new FormInputDouble(_resourceManager.GetString("TargetValue"), value);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    listView1.Items[index].SubItems[1].Text = f.SelectedValue.ToString();
                }
            }
        }

        void _dataEngine_WorkStatusChanged(object sender, WCYDataSource.StartStopEventArgs e)
        {
            if (e.IsStart)
            {
                _needMaskStart = true;
            }
            else
            {
                _needCalcAdjust = true;
                _needMaskStop = true;
            }
        }

        void _dataEngine_PortChanged(object sender, WCYDataSource.PortCollectionEventArgs e)
        {
            _needFresh = true;
        }

        void _dataEngine_OfflineEvent(object sender, WCYDisLab.Classes.OfflineEventArgs e)
        {
            if (!e.IsOffline)
            {
                _needMaskCanDo = true;
            }
            else
            {
                _needMaskCannotDo = true;
            }
        }

        void _dataEngine_ConnectChanged(object sender, WCYDataSource.ConnectEventArgs e)
        {
            if (e.Connected)
            {
                _needMaskCanDo = true;
            }
            else
            {
                _needMaskCannotDo = true;
            }
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.FormAdjust", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        List<AdjustRecord> _records = new List<AdjustRecord>();
        bool _needFresh = false, _needMaskCanDo = false, _needMaskCannotDo = false,_needMaskStart = false,_needMaskStop = false,_needCalcAdjust = false,_adjusting = false;
        DateTime _startTime;
        TimeSpan _totalTime = new TimeSpan(0, 0, 30);
        public List<double> SelectedAdjustValues
        {
            get
            {
                List<double> values = new List<double>();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Tag != null)
                    {
                        double value;
                        double.TryParse(listView1.Items[i].Tag.ToString(), out value);
                        values.Add(value);
                    }
                    else
                    {
                        values.Add(0);
                    }
                }
                return values;
            }
        }
        #endregion

        #region Methods
        void FillList()
        {
            listView1.Items.Clear();
            _records.Clear();
            for (int i = 0; i < _dataEngine.DataManager.CurrentDataSection.Sensors.Count; i++)
            {
                TDataManager.DataStore dataStore = _dataEngine.DataManager.CurrentDataSection.Sensors[i];
                _records.Add(new AdjustRecord(i));
                ListViewItem item = new ListViewItem(dataStore.DataProps.Caption);
                item.SubItems.Add("0");
                item.Tag = dataStore.DataProps.Calibration;
                item.ImageKey = "sensor";
                listView1.Items.Add(item);
            }
        }
        void MaskCanDo()
        {
            button_start.Enabled = true;
            button_stop.Enabled = false;
            progressBar1.Visible = false;
        }
        void MaskCannotDo()
        {
            button_start.Enabled = false;
            button_stop.Enabled = false;
            progressBar1.Visible = false;
        }
        void MaskStart()
        {
            button_start.Enabled = false;
            button_stop.Enabled = true;
            progressBar1.Visible = true;
        }
        void MaskStop()
        {
            _dataEngine.Adjusted = false;
            button_start.Enabled = true;
            button_stop.Enabled = false;
            progressBar1.Visible = false;
        }
        void CalcAdjust()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Checked)
                {
                    if (i >= 0 && i < _records.Count)
                    {
                        double value;
                        double.TryParse(listView1.Items[i].SubItems[1].Text, out value);
                        listView1.Items[i].Tag = _records[i].Average - value;
                    }
                }
            }
        }
        #endregion

        private void button_start_Click(object sender, EventArgs e)
        {
            _dataEngine.Adjusted = true;
            if (_dataEngine.Connected && !_dataEngine.OffLine)
            {
                for (int i = 0; i < _records.Count; i++)
                {
                    _records[i].Clear();
                }
                _startTime = DateTime.Now;
                
                _dataEngine.StartAdjust();
                _adjusting = true;
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            _dataEngine.StopAdjust();
            _dataEngine.Adjusted = false;
            _adjusting = false;
            //_needCalcAdjust = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needFresh)
            {
                _needFresh = false;
                FillList();
            }
            if (_needMaskCanDo)
            {
                _needMaskCanDo = false;
                MaskCanDo();
            }
            if (_needMaskCannotDo)
            {
                _needMaskCannotDo = false;
                MaskCannotDo();
            }
            if (_needMaskStart)
            {
                _needMaskStart = false;
                MaskStart();
            }
            if (_needMaskStop)
            {
                _needMaskStop = false;
                MaskStop();
            }
            if (_adjusting)
            {
                if (DateTime.Now - _startTime >= _totalTime)
                {
                    _dataEngine.StopAdjust();
                    _adjusting = false;
                }
                else
                {
                    int rate =(int)Math.Round( (DateTime.Now - _startTime).TotalSeconds / _totalTime.TotalSeconds * 100,0);
                    if (rate >= progressBar1.Minimum && rate <= progressBar1.Maximum)
                    {
                        progressBar1.Value = rate;
                    }
                }
            }
            if (_needCalcAdjust)
            {
                _needCalcAdjust = false;
                CalcAdjust();
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
                
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Checked)
                {
                    if (i >= 0 && i < _dataEngine.DataManager.CurrentDataSection.Sensors.Count)
                    {
                        if (listView1.Items[i].Tag != null)
                        {
                            double value;
                            double.TryParse(listView1.Items[i].Tag.ToString(), out value);
                            _dataEngine.DataManager.CurrentDataSection.Sensors[i].DataProps.Calibration = value;
                        }
                    }
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

    }

    class AdjustRecord
    {
        int _index;
        int _count;
        double _sum;
        public int Index
        {
            get { return _index; }
        }
        public int Count
        {
            get { return _count; }
        }
        public double Sum
        {
            get { return _sum; }
        }
          
        public double Average
        {
            get
            {
                double value = 0;
                if (_count > 0)
                {
                    value = _sum / _count;
                }
                return value;
            }
        }
        public AdjustRecord(int index)
        {
            _index = index;
            _count = 0;
            _sum = 0;
        }
        public void Add(double value)
        {
            _count++;
            _sum += value;
        }
        public void Clear()
        {
            _count = 0;
            _sum = 0;
        }
    }
}
