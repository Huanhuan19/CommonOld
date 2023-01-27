using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExperimentProperties
{
    public class IntervalCollection
    {
        public IntervalCollection()
        {
            LoadDefault();
        }
        public IntervalCollection(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        List<IntervalDefine> _intervalDefines = new List<IntervalDefine>();
        public List<IntervalDefine> IntervalDefines
        {
            get { return _intervalDefines; }
        }

        #endregion

        #region Methods
        void LoadDefault()
        {
            _intervalDefines.Clear();
        }
        public bool Contains(byte shiftIndex)
        {
            bool contains = false;
            foreach (IntervalDefine intervaldefine in _intervalDefines)
            {
                if (intervaldefine.ShiftIndex == shiftIndex)
                {
                    contains = true;
                    break;
                        
                }
            }
            return contains;
        }
        public int MoveUp(int index)
        {
            int realIndex = index;
            if (index > 0 && index < _intervalDefines.Count)
            {
                IntervalDefine intervalDefine = new IntervalDefine(_intervalDefines[index].ToString());
                _intervalDefines.RemoveAt(index);
                _intervalDefines.Insert(index - 1, intervalDefine);
                realIndex= index - 1;
            }
            return realIndex;
        }

        public int MoveDown(int index)
        {
            int realIndex = index;
            if (index >= 0 && index < _intervalDefines.Count - 1)
            {
                IntervalDefine intervalDefine = new IntervalDefine(_intervalDefines[index].ToString());
                _intervalDefines.RemoveAt(index);
                _intervalDefines.Insert(index + 1, intervalDefine);
                realIndex = index + 1;
            }
            return realIndex;
        }

        public int GetIndexByShiftIndex(byte shiftIndex)
        {
            int index = -1;
            for (int i = 0; i < _intervalDefines.Count; i++)
            {
                if (_intervalDefines[i].ShiftIndex == shiftIndex)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public double GetIntervalByShiftIndex(byte shiftIndex)
        {
            double interval = 0;
            foreach (IntervalDefine intervalDefine in _intervalDefines)
            {
                if (intervalDefine.ShiftIndex == shiftIndex)
                {
                    interval = intervalDefine.Interval;
                    break;
                }
            }
            return interval;
        }
        public double GetFrequencyByShitIndex(byte shiftIndex)
        {
            double frequency = 0;
            foreach (IntervalDefine intervalDefine in _intervalDefines)
            {
                if (intervalDefine.ShiftIndex == shiftIndex)
                {
                    frequency = intervalDefine.Frequency;
                    break;
                }
            }
            return frequency;
        }
        public int GetShiftIndexByInterval(double interval)
        {
            byte shiftIndex = 0x00;
            foreach (IntervalDefine intervalDefine in _intervalDefines)
            {
                if (intervalDefine.Interval == interval)
                {
                    shiftIndex = intervalDefine.ShiftIndex;
                    break;
                }
            }
            return shiftIndex;
        }
        public int GetShiftIndexByFrequency(double frequency)
        {
            byte shiftIndex = 0x00;
            foreach (IntervalDefine intervalDefine in _intervalDefines)
            {
                if (intervalDefine.Frequency == frequency)
                {
                    shiftIndex = intervalDefine.ShiftIndex;
                    break;
                }
            }
            return shiftIndex;
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _intervalDefines.Count; i++)
            {
                keyValue.Add(i.ToString(), _intervalDefines[i].ToString());
            }
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _intervalDefines.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _intervalDefines.Add(new IntervalDefine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        #endregion
    }
}
