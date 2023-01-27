using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class StatisticBox
    {
        public StatisticBox()
        {
            _values.Capacity = 1000;
        }
        #region Props
        List<double> _values = new List<double>();

        public double Average
        {
            get
            {
                return _values.Average();
            }
        }
        public double Sum
        {
            get
            {
                return _values.Sum();
            }
        }
        public double Max
        {
            get
            {
                return _values.Max();
            }
        }
        public double Min
        {
            get
            {
                return _values.Min();
            }
        }
        public double First
        {
            get
            {
                double value = 0;
                if (_values.Count > 0)
                {
                    value = _values[0];
                }
                return value;
            }
        }
        public double Last
        {
            get
            {
                double value = 0;
                if (_values.Count > 0)
                {
                    value = _values[_values.Count - 1];
                }
                return value;
            }
        }
        #endregion

        #region Methods
        public void Clear()
        {
            _values.Clear();
        }
        public void Add(double value)
        {
            if (_values.Count >= _values.Capacity * 0.9)
            {
                _values.Capacity *= 2;
            }
            _values.Add(value);
        }
        #endregion
    }
}
