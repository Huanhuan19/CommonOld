using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDisLab.Classes
{
    public class ExInfo
    {
        public ExInfo()
        {
            LoadDefault();
        }
        #region Props
        DateTime _startTime;
        int _lineCount;
        double _minValue, _maxValue, _minAbsValue, _maxAbsValue;
        bool _hadSet;
        ActionType _actionType;
        bool _photoGateOpen, _photoGateShouldOpen;
        int _photoGateStartTimeIndex, _photoGateStopTimeIndex;
        int _manualOpenSensorIndex;
        List<bool> _sensorValues = new List<bool>();
        public ActionType ActionType
        {
            get { return _actionType; }
            set { _actionType = value; }
        }
        public bool PhotoGateOpen
        {
            get { return _photoGateOpen; }
            set
            {
                _photoGateOpen = value;
            }
        }
        public bool PhotoGateShouldOpen
        {
            get { return _photoGateShouldOpen; }
            set { _photoGateShouldOpen = value; }
        }
        public double Seconds
        {
            get
            {
                double seconds = 0;
                if (_actionType == ActionType.Started)
                {
                    TimeSpan span = DateTime.Now - _startTime;
                    seconds = span.TotalSeconds;
                }
                return seconds;
            }
        }
        public int PhotoGateStartTimeIndex
        {
            get { return _photoGateStartTimeIndex; }
        }
        public int PhotoGateStopTimeIndex
        {
            get { return _photoGateStopTimeIndex; }
        }
        public int ManualOpenSensorIndex
        {
            get { return _manualOpenSensorIndex; }
            set { _manualOpenSensorIndex = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            Clear();
        }
        public void Clear()
        {
            _startTime = DateTime.Now;
            _lineCount = 0;
            _minValue = 0;
            _maxValue = 0;
            _minAbsValue = 0;
            _maxAbsValue = 0;
            _actionType = ActionType.Stoped;
            _hadSet = false;
            _photoGateOpen = false;
            _photoGateShouldOpen = false;
            _manualOpenSensorIndex = -1;
        }
        public void Prepair(int count)
        {
            Clear();
            _actionType = ActionType.Waiting;
            InitSensorValues(count);
        }
        public void AddValue(double value)
        {
            _lineCount++;
            if (_hadSet)
            {
                _minValue = Math.Min(_minValue, value);
                _maxValue = Math.Max(_maxValue, value);
                _minAbsValue = Math.Min(_minAbsValue, Math.Abs(value));
                _maxAbsValue = Math.Max(_maxAbsValue, Math.Abs(value));
            }
            else
            {
                _hadSet = true;
                _minValue = value;
                _maxValue = value;
                _minAbsValue = value;
                _maxAbsValue = value;
            }
        }
        public void ResetStarted()
        {
            _startTime = DateTime.Now;
            _hadSet = false;
            _minValue = 0;
            _maxValue = 0;
            _minAbsValue = 0;
            _maxAbsValue = 0;
            _lineCount = 0;
            _photoGateOpen = false;
            _photoGateShouldOpen = false;
        }
        public bool CheckStart(int lineCount)
        {
            bool shouldStart = _lineCount >= lineCount;
            return shouldStart;
        }
        public bool CheckStart(double seconds)
        {
            TimeSpan span = DateTime.Now - _startTime;
            bool shouldStart = span.TotalSeconds >= seconds;
            return shouldStart;
        }
        public bool CheckStart(double gateValue, bool increase, bool checkAbs)
        {
            bool canStart = false;
            if (_hadSet)
            {
                if (increase)
                {
                    if (checkAbs)
                    {
                        canStart = _maxAbsValue > gateValue;
                    }
                    else
                    {
                        canStart = _maxValue > gateValue;
                    }
                }
                else
                {
                    if (checkAbs)
                    {
                        canStart = _minAbsValue < gateValue;
                    }
                    else
                    {
                        canStart = _minValue < gateValue;
                    }
                }
            }
            return canStart;
        }
        public bool CheckStop(int lineCount)
        {
            bool shouldStop = _lineCount >= lineCount;
            return shouldStop;
        }
        public bool CheckStop(double seconds)
        {
            TimeSpan span = DateTime.Now - _startTime;
            bool shouldStop = span.TotalSeconds >= seconds;
            return shouldStop;
        }
        public bool CheckStop(double gateValue, bool increase, bool checkAbs)
        {
            bool canStop = false;
            if (_hadSet)
            {
                if (increase)
                {
                    if (checkAbs)
                    {
                        canStop = _maxAbsValue > gateValue;
                    }
                    else
                    {
                        canStop = _maxValue > gateValue;
                    }
                }
                else
                {
                    if (checkAbs)
                    {
                        canStop = _minAbsValue < gateValue;
                    }
                    else
                    {
                        canStop = _minValue < gateValue;
                    }
                }
            }
            return canStop;
        }
        public void SetPhotoGateStartTimeIndex(int timeIndex)
        {
            _photoGateStartTimeIndex = timeIndex;
        }
        public void SetPhotoGateStopTimeIndex(int timeIndex)
        {
            _photoGateStopTimeIndex = timeIndex;
        }
        public void InitSensorValues(int count)
        {
            _sensorValues.Clear();
            for (int i = 0; i < count; i++)
            {
                _sensorValues.Add(false);
            }
        }
        public void ResetSensorValues()
        {
            for (int i = 0; i < _sensorValues.Count; i++)
            {
                _sensorValues[i] = false;
            }
        }
        public void SetSensorValue(int index)
        {
            if (index >= 0 && index < _sensorValues.Count)
            {
                _sensorValues[index] = true;
            }
        }
        public bool SensorValue(int index)
        {
            bool value = false;
            if (index >= 0 && index < _sensorValues.Count)
            {
                value = _sensorValues[index];
            }
            return value;
        }
        public bool CheckSensorValues()
        {
            bool value = true;
            for (int i = 0; i < _sensorValues.Count; i++)
            {
                value &= _sensorValues[i];
            }
            return value;
        }
        #endregion
    }
    public enum ActionType : int
    {
        Stoped = 0, Started = 10, Waiting = 20
    }
}
