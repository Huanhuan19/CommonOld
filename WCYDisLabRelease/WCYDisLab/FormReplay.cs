using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab
{
    public partial class FormReplay : Form
    {
        public FormReplay(Classes.DataEngine dataEngine)
        {
            InitializeComponent();

            _dataEngine = dataEngine;
            InitRateList();
            FillList();
            _dataEngine.DataManager.Replay.ReplayEvent += new TDataManager.ReplayHandler(Replay_ReplayEvent);
            FormClosing += new FormClosingEventHandler(FormReplayControl_FormClosing);
            trackBar1.ValueChanged += new EventHandler(trackBar1_ValueChanged);
            timer1.Start();
        }

        #region Props
        Classes.DataEngine _dataEngine;
        bool _needSetStart = false, _needSetStop = false, _needSetPause = false, _needSetPlaying = false, _needSetSwitchSection = false;
        List<string> SelectedSectionNames
        {
            get
            {
                List<string> sectionNames = new List<string>();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Checked)
                    {
                        if (i >= 0 && i < _dataEngine.DataManager.DataSections.Count)
                        {
                            sectionNames.Add(_dataEngine.DataManager.DataSections[i].Name);
                        }
                    }
                }
                return sectionNames;
            }
        }
        double SelectedRate
        {
            get
            {
                double rate = 1;
                int index = trackBar1.Value;
                if (index >= 0 && index < _dataEngine.DataManager.ReplayRateList.Count)
                {
                    rate = _dataEngine.DataManager.ReplayRateList[index];
                }
                return rate;
            }
        }
        #endregion

        #region Methods
        void InitRateList()
        {
            trackBar1.Minimum = 0;
            trackBar1.Maximum = _dataEngine.DataManager.ReplayRateList.Count - 1;
            trackBar1.Value = _dataEngine.DataManager.DefaultReplayRateIndex;
            label_rate.Text = "1X";
        }
        void FillList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _dataEngine.DataManager.DataSections.Count; i++)
            {
                TDataManager.DataSection section = _dataEngine.DataManager.DataSections[i];
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(section.Caption);
                item.SubItems.Add(section.TimeLineProps.Interval.ToString() + "s");
                item.ImageKey = "stop";
                item.Tag = section.Name;
                listView1.Items.Add(item);
            }
        }
        void SetStart()
        {
            button_pause.Enabled = true;
            button_start.Enabled = false;
            button_stop.Enabled = true;
            listView1.Enabled = false;
            progressBar1.Visible = true;
            label_sectionCaption.Visible = true;
            trackBar1.Enabled = false;
            SetSwitchSection();
        }
        void SetPause()
        {
            button_pause.Enabled = true;
            button_start.Enabled = false;
            button_stop.Enabled = true;
            listView1.Enabled = false;
            progressBar1.Visible = true;
            label_sectionCaption.Visible = true;
            trackBar1.Enabled = false;
            string sectionName = _dataEngine.DataManager.Replay.CurrentSectionName;
            TDataManager.DataSection section = _dataEngine.DataManager.GetDataSectionByName(sectionName);
            if (section != null)
            {
                //label_sectionCaption.Text = section.Caption;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Tag != null && string.Equals(listView1.Items[i].Tag.ToString(), sectionName))
                    {
                        listView1.Items[i].ImageKey = "pause";
                    }
                    else
                    {
                        listView1.Items[i].ImageKey = "stop";
                    }
                }
            }
        }
        void SetPlaying()
        {
            progressBar1.Value = (int)Math.Round(_dataEngine.DataManager.Replay.Process * 100, 0);
        }
        void SetSwitchSection()
        {
            string sectionName = _dataEngine.DataManager.Replay.CurrentSectionName;
            TDataManager.DataSection section = _dataEngine.DataManager.GetDataSectionByName(sectionName);
            if (section != null)
            {
                label_sectionCaption.Text = section.Caption;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Tag != null && string.Equals(listView1.Items[i].Tag.ToString(), sectionName))
                    {
                        listView1.Items[i].ImageKey = "play";
                    }
                    else
                    {
                        listView1.Items[i].ImageKey = "stop";
                    }
                }
            }
        }
        void SetStop()
        {
            this.button_pause.Enabled = false;
            this.button_start.Enabled = true;
            this.button_stop.Enabled = false;
            listView1.Enabled = true;
            progressBar1.Visible = false;
            label_sectionCaption.Visible = false;
            trackBar1.Enabled = true;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].ImageKey = "stop";
            }
        }
        void Replay_ReplayEvent(object sender, TDataManager.ReplayEventArgs e)
        {
            switch (e.ReplayStatus)
            {
                case TDataManager.ReplayStatus.End:
                    _needSetStop = true;
                    break;
                case TDataManager.ReplayStatus.Pause:
                    _needSetPause = true;
                    break;
                case TDataManager.ReplayStatus.Playing:
                    _needSetPlaying = true;
                    break;
                case TDataManager.ReplayStatus.Start:
                    _needSetStart = true;
                    break;
                case TDataManager.ReplayStatus.SwitchSection:
                    _needSetSwitchSection = true;
                    break;
            }
        }
        void trackBar1_ValueChanged(object sender, EventArgs e)
        {

            label_rate.Text = SelectedRate.ToString() + "X";
        }

        void FormReplayControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dataEngine.DataManager.Replay.Status != TDataManager.ReplayStatus.End)
            {
                _dataEngine.DataManager.Replay.Stop();
            }
            _dataEngine.DataManager.Replay.ReplayEvent -= Replay_ReplayEvent;
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needSetPause)
            {
                _needSetPause = false;
                SetPause();
            }
            if (_needSetPlaying)
            {
                _needSetPlaying = false;
                SetPlaying();
            }
            if (_needSetStart)
            {
                _needSetStart = false;
                SetStart();
            }
            if (_needSetStop)
            {
                _needSetStop = false;
                SetStop();
            }
            if (_needSetSwitchSection)
            {
                _needSetSwitchSection = false;
                SetSwitchSection();
            }

        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (SelectedSectionNames.Count > 0)
            {
                _dataEngine.DataManager.SetReplayProps(SelectedSectionNames, SelectedRate);
                _dataEngine.DataManager.Replay.Start();
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            _dataEngine.DataManager.Replay.Stop();

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            if (_dataEngine.DataManager.Replay.Status != TDataManager.ReplayStatus.End)
            {
                _dataEngine.DataManager.Replay.Stop();
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();


        }

        private void button_pause_Click(object sender, EventArgs e)
        {
            _dataEngine.DataManager.Replay.Pause();
        }

        private void trackBar_opacity_Scroll(object sender, EventArgs e)
        {
            this.Opacity = ((double)trackBar_opacity.Value)/100;
        }

    }
}
