using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDataSource
{
    public class PortInfo
    {
        public PortInfo()
        {
            LoadDefault();
        }
        public PortInfo(int portIndex,SensorTypeDefine sensorType,float k ,float b)
        {
            _portIndex = portIndex;
            _portAvailable = true;
            _plugIn = portIndex > 0x00;
            _lastValue = 0f; 
            _upSideValue = 0f;
            _downSideValue = 0f;
            _k = k;
            _b = b;
        }
        #region Props
        public static byte DEFAULT_PORTINDEX = 0x00;
        int _portIndex;
        bool _portAvailable,_plugIn;
        float _lastValue;
        float _upSideValue, _downSideValue;
        SensorTypeDefine _sensorType;
        float _k, _b;
        public float K
        {
            get { return _k; }
            set { _k = value; }
        }
        public float B
        {
            get { return _b; }
            set { _b = value; }
        }
        public float UpSideValue
        {
            get { return _upSideValue; }
            set { _upSideValue = value; }
        }
        public float DownSideValue
        {
            get { return _downSideValue; }
            set { _downSideValue = value; }
        }
        public SensorTypeDefine SensorType
        {
            get { return _sensorType; }
            set { _sensorType = value; }
        }
        public int PortIndex
        {
            get { return _portIndex; }
            set { _portIndex = value; }
        }
        public bool PortAvailable
        {
            get { return _portAvailable; }
            set { _portAvailable = value; }
        }
        public float LastValue
        {
            get { return _lastValue; }
            set { _lastValue = value; }
        }
        public bool IsPlugIn
        {
            get { return _plugIn; }
            set { _plugIn = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _portAvailable = false;
            _plugIn = false;
            _portIndex = DEFAULT_PORTINDEX;
            _lastValue = 0f;
            _upSideValue = 0f;
            _downSideValue = 0f;
            _k = 1;
            _b = 0;
            _sensorType = SensorTypeDefine.Normal;
        }
        public void PlugIn(int portIndex,SensorTypeDefine sensorType,float k ,float b)
        {
            _portAvailable = true;
            _plugIn = portIndex > 0x00;
            _portIndex = portIndex;
            _lastValue = 0f;
            _upSideValue = 0f;
            _downSideValue = 0f;
            _k = k;
            _b = b;
            _sensorType = sensorType;
        }
        public void PlugOut()
        {
            _portAvailable = false;
            _plugIn = false;
            _portIndex = DEFAULT_PORTINDEX;
            _lastValue = 0f;
            _upSideValue = 0f;
            _downSideValue = 0f;

            _sensorType = SensorTypeDefine.Normal;
        }
        #endregion
    }
}
