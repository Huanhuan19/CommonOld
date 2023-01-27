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
    public partial class FormConstantControler : Form
    {
        public FormConstantControler(Classes.DataEngine dataEngine)
        {
            InitializeComponent();
            _dataEngine = dataEngine;
            _dataEngine.DataManager.ConstantEvent += new TDataManager.ConstantHandler(DataManager_ConstantEvent);
            _dataEngine.DataManager.OffLineEvent += new TDataManager.OffLineHandler(DataManager_OffLineEvent);
            _dataEngine.DataManager.SectionEvent += new TDataManager.SectionEventHandler(DataManager_SectionEvent);
            timer1.Start();
            FillList();
        }

        void DataManager_SectionEvent(object sender, TDataManager.SectionEventArgs e)
        {
            _needReload = true;
        }

        void DataManager_OffLineEvent(object sender, TDataManager.OffLineEventArgs e)
        {
            _needReload = true;
        }

        void DataManager_ConstantEvent(object sender, TDataManager.ConstantArgs e)
        {
            _needReload = true;
        }

        #region Props
        Classes.DataEngine _dataEngine;
        bool _needReload = false;
        #endregion

        #region Methods
        void FillList()
        {
            flowLayoutPanel1.Controls.Clear();
            TDataManager.DataSection section = _dataEngine.DataManager.LastDataSection;
            if( section == null )
            {
                section = _dataEngine.DataManager.CurrentDataSection;
            }
            for (int i = 0; i < section.Constants.Count; i++)
            {
                WCYDisLab.Controls.ConstantControler control = new WCYDisLab.Controls.ConstantControler();
                control.Initialize(section.Constants[i]);
                flowLayoutPanel1.Controls.Add(control);
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needReload)
            {
                _needReload = false;
                FillList();
            }
        }

        private void trackBar_opacity_Scroll(object sender, EventArgs e)
        {
            this.Opacity = ((double)trackBar_opacity.Value) / 100;

        }
    }
}
