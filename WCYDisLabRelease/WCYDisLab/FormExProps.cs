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
    public partial class FormExProps : Form
    {
        static string strSensor = "";
        public FormExProps(Classes.DataEngine dataEngine)
        {
            strSensor = "";
            InitializeComponent();
            _dataEngine = dataEngine;
            Initialize();
            _dataEngine.DataManager.DataCollectionEvent += new TDataManager.DataCollectionHandler(DtManager_DataCollectionEvent);
        }

        #region Props
        Classes.DataEngine _dataEngine;
        public ExperimentProperties.PhotoGateProps SelectedGateProps
        {
            get
            {
                ExperimentProperties.PhotoGateProps props = new ExperimentProperties.PhotoGateProps();
                props.Available = gateOpenProps1.SelectedGateAvailable;
                props.CloseCheckAbs = gateCloseProps1.SelectedCheckAbs;
                props.CloseColumnName = gateCloseProps1.SelectedColumName;
                props.CloseGateValue = gateCloseProps1.SelectedValue;
                props.CloseMode = gateCloseProps1.SelectedCloseMode;
                props.OpenCheckAbs = gateOpenProps1.SelectedCheckAbs;
                props.OpenColumnName = gateOpenProps1.SelectedColumName;
                props.OpenGateValue = gateOpenProps1.SelectedValue;
                props.OpenMode = gateOpenProps1.SelectedOpenMode;


                return props;
            }

        }
        public ExperimentProperties.StartProps SelectedStartProps
        {
            get { return startProps1.SelectedStartProps; }
        }
        public ExperimentProperties.EndProps SelectedEndProps
        {
            get { return stopProps1.SelectedEndProps; }
        }

        #endregion

        #region Methods
        void Initialize()
        {
            startProps1.Initialize(_dataEngine, new ExperimentProperties.StartProps(_dataEngine.WorkArguments.StartProperties.ToString()));
            stopProps1.Initialize(_dataEngine, new ExperimentProperties.EndProps(_dataEngine.WorkArguments.EndProperties.ToString()));
            gateOpenProps1.Initialize(_dataEngine, new ExperimentProperties.PhotoGateProps(_dataEngine.WorkArguments.PhotoGateProperties.ToString()));
            gateCloseProps1.Initialize(_dataEngine, new ExperimentProperties.PhotoGateProps(_dataEngine.WorkArguments.PhotoGateProperties.ToString()));
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (tabControl2.SelectedIndex == 1)
            {
                timer1.Start();
                if (_dataEngine.Started == false)
                {
                    _dataEngine.Started = true;
                    _dataEngine.Start();
                }
            }
            if (FormSelectLanguage._is6p6c)//
            {

                if (WCYDisLab.SensorSet.WaveCreat.ready)
                {
                    _dataEngine.Wave(WCYDisLab.SensorSet.WaveCreat.Wave, WCYDisLab.SensorSet.WaveCreat.WaveType, WCYDisLab.SensorSet.WaveCreat.value, WCYDisLab.SensorSet.WaveCreat.time);
                }
                if (WCYDisLab.SensorSet.Switch.ready)
                {
                    _dataEngine.Wave(WCYDisLab.SensorSet.Switch.SwitchControl, WCYDisLab.SensorSet.Switch.voice, WCYDisLab.SensorSet.Switch.light, WCYDisLab.SensorSet.Switch.control);
                }
                if (WCYDisLab.SensorSet.DataControlRes.ready)
                {
                    _dataEngine.Wave(WCYDisLab.SensorSet.DataControlRes.datacontrol, WCYDisLab.SensorSet.DataControlRes.waitUse1, WCYDisLab.SensorSet.DataControlRes.waitUse2, WCYDisLab.SensorSet.DataControlRes.value);
                }
            }
            else//USB
            {
                if (WCYDisLab.SensorSet.WaveCreat.ready)
                {
                    _dataEngine.USBWave(WCYDisLab.SensorSet.WaveCreat.WaveType, WCYDisLab.SensorSet.WaveCreat.value, WCYDisLab.SensorSet.WaveCreat.time, 00, 00);
                    _dataEngine.Wave(WCYDisLab.SensorSet.WaveCreat.Wave, WCYDisLab.SensorSet.WaveCreat.WaveType, WCYDisLab.SensorSet.WaveCreat.value, WCYDisLab.SensorSet.WaveCreat.time);
                }
                if (WCYDisLab.SensorSet.Switch.ready)
                {
                    byte DD = 0;
                    DD = Convert.ToByte(WCYDisLab.SensorSet.Switch.voice == 0x00 ? (DD | Convert.ToByte(0x00)) : (DD | Convert.ToByte(0x01)));
                    DD = Convert.ToByte(WCYDisLab.SensorSet.Switch.light == 0x00 ? (DD | 0x00) : (DD | 0x02));
                    DD = Convert.ToByte(WCYDisLab.SensorSet.Switch.control == 0x00 ? (DD | 0x00) : (DD | 0x04));
                    _dataEngine.USBWave(00, 00, 00, DD, 0);
                    //_dataEngine.Wave(WCYDisLab.SensorSet.Switch.SwitchControl, WCYDisLab.SensorSet.Switch.voice, WCYDisLab.SensorSet.Switch.light, WCYDisLab.SensorSet.Switch.control);
                }
                if (WCYDisLab.SensorSet.DataControlRes.ready)
                {
                    _dataEngine.USBWave(00, 00, 00, 00, WCYDisLab.SensorSet.DataControlRes.value);
                    //_dataEngine.Wave(WCYDisLab.SensorSet.DataControlRes.datacontrol, WCYDisLab.SensorSet.DataControlRes.value, WCYDisLab.SensorSet.DataControlRes.waitUse1, WCYDisLab.SensorSet.DataControlRes.waitUse2);
                }
            }

        }

        private void FormExProps_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TDataManager.DataSection section = _dataEngine.OffLine ? _dataEngine.DataManager.DataSectionTemplete : _dataEngine.DataManager.DataSectionActive;
            FormSelectSensorOrExpr f = new FormSelectSensorOrExpr(section.Sensors, -1);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int index = f.SelectedIndex;
                if (index >= 0 && index < section.Sensors.Count)
                {
                    //_name = section.Sensors[index].DataProps.Name;
                    textBox1.Text = section.Sensors[index].DataProps.Caption;
                    strSensor = section.Sensors[index].DataProps.Name;
                    //VariablePickedEventArgs a = new VariablePickedEventArgs(section.Sensors[index].DataProps.Name, section.Sensors[index].DataProps.Caption);
                    //OnVariablePicked(a);
                }
            }
        }
        bool IsUpZhiXing = false;
        bool IsDownZhiXing = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                textBox3.Text = val.ToString();
                if (_dataEngine.ExInfo.ActionType == Classes.ActionType.Started)
                {
                    if (val <= Convert.ToDouble(textBox2.Text))
                    {
                        if (!IsUpZhiXing)
                        {
                            IsUpZhiXing = true;
                            IsDownZhiXing = false;
                            if (FormSelectLanguage._is6p6c)
                            {
                                _dataEngine.Wave(WCYDisLab.SensorSet.Switch.SwitchControl, 0x00, 0x00, 0x00);
                            }
                            else
                            {
                                _dataEngine.USBWave(00, 00, 00, 05, 0);
                            }
                        }
                    }
                    else
                    {
                        if (!IsDownZhiXing)
                        {
                            IsUpZhiXing = false;
                            IsDownZhiXing = true;
                            if (FormSelectLanguage._is6p6c)
                            {
                                _dataEngine.Wave(WCYDisLab.SensorSet.Switch.SwitchControl, 0x01, 0x01, 0x01);
                            }
                            else
                            {
                                _dataEngine.USBWave(00, 00, 00, 00, 0);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void FormExProps_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dataEngine.DataManager.DataCollectionEvent -= DtManager_DataCollectionEvent;
        }
        private double val;
        void DtManager_DataCollectionEvent(object sender, TDataManager.DataCollectionEventArgs e)
        {

            if (string.Compare(strSensor, TDataManager.DataManager.TIMEINDEX_NAME) == 0)
            {
                val = e.DataElement.TimeIndex;
            }
            else if (string.Compare(strSensor, TDataManager.DataManager.TIMESTAMP_NAME) == 0)
            {
                val = e.DataElement.TimeStamp;
            }
            else if (string.Compare(e.ColumnName, strSensor) == 0)
            {
                val = e.DataElement.Value;
            }

        }


    }
}
