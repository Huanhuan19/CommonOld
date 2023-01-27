using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExperimentProperties
{
    public class IntervalDefine
    {
        public IntervalDefine()
        {
            LoadDefault();
        }
        public IntervalDefine(double interval, byte shiftIndex)
        {
            _interval = interval;
            _shiftIndex = shiftIndex;
        }
        public IntervalDefine(string value)
        {
            LoadDefault();
            Parse(value);
        }

        #region Props
        double _interval;
        byte _shiftIndex;
        
        public double Interval
        {
            get { return _interval; }
            set { _interval = value; }
            
        }
        public byte[] IntervalBytes
        {
            get
            {
                byte[] intervalBytes = new byte[3];
                double intervalDouble = Math.Round(_interval / 0.0001, 0);
                double c = Math.IEEERemainder(intervalDouble , 256); 
                double b = Math.IEEERemainder(intervalDouble / 256, 256);
                double a = intervalDouble / 256 / 256;
                //byte.TryParse(c.ToString(), out intervalBytes[2]);                                          已改
                //byte.TryParse(b.ToString(), out intervalBytes[1]);
                //byte.TryParse(a.ToString(), out intervalBytes[0]);
                intervalBytes[2] = Convert .ToByte (c);
                intervalBytes[1] = Convert.ToByte(b);
                intervalBytes[0] = Convert.ToByte(a);
                return intervalBytes;
            }
        }
        public byte ShiftIndex
        {
            get { return _shiftIndex; }
            set { _shiftIndex = value; }
        }
        public double Frequency
        {
            get 
            {
                double frequency = 0;
                if (_interval > 0)
                {
                    frequency = Math.Round( 1 / _interval,4);
                }
                return frequency;
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _interval = 0;
            _shiftIndex = 0x00;
        }
        public override string ToString()
        {
            string[] strList = new string[2];
            int i = 0;
            strList[i++] = _interval.ToString();
            strList[i++] = _shiftIndex.ToString();
            return string.Join("|",strList );
        }
        public bool Parse(string value)
        {
            bool success = false;
            string[] strList = value.Split('|');
            if (strList.Length == 2)
            {
                success = true;
                int i = 0;
                success &= double.TryParse(strList[i++],out _interval );
                success &= byte.TryParse( strList[i++],out _shiftIndex );
            }
            return success;
        }
        public static byte[] CalcBytes(double interval )
        {
                byte[] intervalBytes = new byte[3];
                int intervalDouble = (int)Math.Round(interval / 0.0001, 0);
                int a, b, c;
                b = Math.DivRem(intervalDouble , 256,out c); 
                a = Math.DivRem(b, 256,out b);
                byte.TryParse(c.ToString(), out intervalBytes[2]);
                byte.TryParse(b.ToString(), out intervalBytes[1]);
                byte.TryParse(a.ToString(), out intervalBytes[0]);
                return intervalBytes;
        }
        public static byte[] CalcBytes128(double interval)
        {
            byte[] intervalBytes = new byte[3];
            int intervalDouble = (int)Math.Round(interval / 0.0001, 0);
            int a, b, c;
            b = Math.DivRem(intervalDouble, 128, out c);
            a = Math.DivRem(b, 128, out b);
            byte.TryParse(c.ToString(), out intervalBytes[2]);
            byte.TryParse(b.ToString(), out intervalBytes[1]);
            byte.TryParse(a.ToString(), out intervalBytes[0]);
            return intervalBytes;
        }
        #endregion
    }
}
