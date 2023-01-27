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
    public partial class FormFrequencySet : Form
    {
        public FormFrequencySet(ExperimentProperties.IntervalCollection intervalCollection, int index)
        {
            InitializeComponent();
            _intervalCollection = intervalCollection;
            _index = index;
            this.trackBar1.ValueChanged += new EventHandler(trackBar1_ValueChanged);
            Initialize();
        }


        #region Props
        ExperimentProperties.IntervalCollection _intervalCollection;
        int _index;
        public int SelectedIndex
        {
            get { return _index; }
        }
        public ExperimentProperties.IntervalDefine SelectedIntervalDefine
        {
            get { return _intervalCollection.IntervalDefines[_index]; }
        }
        #endregion

        #region Methods
        void Initialize()
        {
            if (_intervalCollection.IntervalDefines.Count > 0)
            {

                trackBar1.Minimum = 0;
                trackBar1.Maximum = _intervalCollection.IntervalDefines.Count - 1;
                if (_index >= 0 && _index < _intervalCollection.IntervalDefines.Count)
                {
                    trackBar1.Value = _index;
                    this.textBox_interval.Text = _intervalCollection.IntervalDefines[_index].Interval.ToString();
                    this.textBox_frequency.Text = _intervalCollection.IntervalDefines[_index].Frequency.ToString();
                }
            }
        }
        void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            _index = trackBar1.Value;
            if (_index >= 0 && _index < _intervalCollection.IntervalDefines.Count)
            {
                this.textBox_interval.Text = _intervalCollection.IntervalDefines[_index].Interval.ToString();
                this.textBox_frequency.Text = _intervalCollection.IntervalDefines[_index].Frequency.ToString();
            }
        }

        #endregion
    }
}
