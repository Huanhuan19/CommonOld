using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAnalysis
{
    public class FittingFormula
    {
        public FittingFormula()
        {
            LoadDefault();
        }
        public FittingFormula(string methodName, double a, double b, double c, double d)
        {
            _methodName = methodName;
            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }
        #region Props
        string _methodName = "";
        double _a, _b, _c, _d;
        public string MethodName
        {
            get { return _methodName; }
        }
        public double A
        {
            get { return _a; }
        }
        public double B
        {
            get { return _b; }
        }
        public double C
        {
            get { return _c; }
        }
        public double D
        {
            get { return _d; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _methodName = "";
            _a = 0;
            _b = 0;
            _c = 0;
            _d = 0;
        }
        #endregion

        #region Serialize

        public override string ToString()
        {
            string[] strList = new string[5];
            strList[0] = MethodName;
            strList[1] = A.ToString();
            strList[2] = B.ToString();
            strList[3] = C.ToString();
            strList[4] = D.ToString();
            return String.Join("|", strList); ;
        }
        public void Parse(string value)
        {
            string[] strList = value.Split('|');
            if (strList.Length == 5)
            {
                _methodName = strList[0];
                double.TryParse(strList[1], out _a);
                double.TryParse(strList[2], out _b);
                double.TryParse(strList[3], out _c);
                double.TryParse(strList[4], out _d);
            }
        }
        #endregion
    }
}
