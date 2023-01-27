using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class GraphPointDefine
    {
        public GraphPointDefine()
        {
            LoadDefault();
        }
        public GraphPointDefine(double x, double y, double z)
        {
            Initialize(x, y, z);
        }
        public GraphPointDefine(double x, double y)
        {
            Initialize(x, y, y);
        }
        public GraphPointDefine(string value)
        {
            Parse(value);
        }
        #region Props
        double _x, _y, _z;
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }
        #endregion

        #region Methods
        #endregion

        #region Serialize
        void LoadDefault()
        {
            _x = 0;
            _y = 0;
            _z = 0;
        }
        void Initialize(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public override string ToString()
        {
            string[] strList = new string[3];
            strList[0] = _x.ToString();
            strList[1] = _y.ToString();
            strList[2] = _z.ToString();
            return string.Join("|", strList);
        }
        public void Parse(string value)
        {
            string[] strList = value.Split('|');
            LoadDefault();
            if (strList.Length == 3)
            {
                double.TryParse(strList[0], out _x);
                double.TryParse(strList[1], out _y);
                double.TryParse(strList[2], out _z);
            }
        }
        #endregion
    }
}
