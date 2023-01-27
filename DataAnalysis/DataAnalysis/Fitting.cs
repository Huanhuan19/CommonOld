using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;

namespace DataAnalysis
{
    public class Fitting
    {
        public Fitting()
        {
            InitialzieFittings();
            InitialzieFittingMethods();
            InitialzieStatisticMethods();
        }
        #region Props
        ResourceManager _resourceManager = new ResourceManager("DataAnalysis.Fitting",
				Assembly.GetExecutingAssembly() );

        List<FittingMethodExpression> _fittings = new List<FittingMethodExpression>();
        List<FittingMethodExpression> _statisticMethods = new List<FittingMethodExpression>();
        List<FittingMethodExpression> _fittingMethods = new List<FittingMethodExpression>();
        public List<FittingMethodExpression> Methods
        {
            get
            {
                return _fittings;
            }
        }
        public List<FittingMethodExpression> StatisticMethods
        {
            get
            {
                return _statisticMethods;
            }
        }
        public List<FittingMethodExpression> FittingMethods
        {
            get
            {
                return _fittingMethods;
            }
        }
        #endregion

        #region Methods

        public void InitialzieFittings()
        {
            //_fittings.Add(  new FittingMethodExpression("LineFitting", "直线函数Y=AX+B",FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("CurveFitting1", "乘幂函数", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("CurveFitting2", "第n级反比函数", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("CurveFitting3", "自然指数函数", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("CurveFitting4", "反比函数", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("CurveFitting5", "平方倒数函数", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("CurveFitting6", "自然对数函数", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("CurveFitting7", "10基对数函数", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("CurveFitting8", "常用对数函数", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("PolyFitting2", "二次多项式Y=AX^2+BX+C", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("PolyFitting3", "三次多项式Y+AX^3+BX^2+CX+D", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("SinFitting", "正弦函数Y=A*Sin(BX+C)+D", FittingMethodType.Fitting));
            //_fittings.Add(new FittingMethodExpression("CalcArea", "计算面积", FittingMethodType.Statistic));
            //_fittings.Add(new FittingMethodExpression("Cycle", "计算周期", FittingMethodType.Statistic));
            //_fittings.Add(new FittingMethodExpression("Frequency", "计算频率", FittingMethodType.Statistic));
            //_fittings.Add(new FittingMethodExpression("Average", "平均值", FittingMethodType.Statistic));
            //_fittings.Add(new FittingMethodExpression("Maximum", "最大值", FittingMethodType.Statistic));
            //_fittings.Add(new FittingMethodExpression("Minimum", "最小值", FittingMethodType.Statistic));
            //_fittings.Add(new FittingMethodExpression("Median", "中值", FittingMethodType.Statistic));
            //_fittings.Add(new FittingMethodExpression("StandardError", "标准差", FittingMethodType.Statistic));
            _fittings.Clear();
            _fittings.Add(new FittingMethodExpression("LineFitting", _resourceManager.GetString( "LineFitting"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("CurveFitting1", _resourceManager.GetString("CurveFitting1"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("CurveFitting2", _resourceManager.GetString("CurveFitting2"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("CurveFitting3", _resourceManager.GetString("CurveFitting3"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("CurveFitting4", _resourceManager.GetString("CurveFitting4"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("CurveFitting5", _resourceManager.GetString("CurveFitting5"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("CurveFitting6", _resourceManager.GetString("CurveFitting6"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("CurveFitting7", _resourceManager.GetString("CurveFitting7"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("CurveFitting8", _resourceManager.GetString("CurveFitting8"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("PolyFitting2", _resourceManager.GetString("PolyFitting2"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("PolyFitting3", _resourceManager.GetString("PolyFitting3"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("SinFitting", _resourceManager.GetString("SinFitting"), FittingMethodType.Fitting));
            _fittings.Add(new FittingMethodExpression("CalcArea", _resourceManager.GetString("CalcArea"), FittingMethodType.Statistic));
            _fittings.Add(new FittingMethodExpression("Cycle", _resourceManager.GetString("Cycle"), FittingMethodType.Statistic));
            _fittings.Add(new FittingMethodExpression("Frequency", _resourceManager.GetString("Frequency"), FittingMethodType.Statistic));
            _fittings.Add(new FittingMethodExpression("Average", _resourceManager.GetString("Average"), FittingMethodType.Statistic));
            _fittings.Add(new FittingMethodExpression("Maximum", _resourceManager.GetString("Maximum"), FittingMethodType.Statistic));
            _fittings.Add(new FittingMethodExpression("Minimum", _resourceManager.GetString("Minimum"), FittingMethodType.Statistic));
            _fittings.Add(new FittingMethodExpression("Median", _resourceManager.GetString("Median"), FittingMethodType.Statistic));
            _fittings.Add(new FittingMethodExpression("StandardError", _resourceManager.GetString("StandardError"), FittingMethodType.Statistic));
            _fittings.Add(new FittingMethodExpression("Derivation", _resourceManager.GetString("Derivation"), FittingMethodType.Fitting));


        }
        public void InitialzieFittingMethods()
        {
            this._fittingMethods.Clear();
            _fittingMethods.Add(new FittingMethodExpression("LineFitting", _resourceManager.GetString("LineFitting"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("CurveFitting1", _resourceManager.GetString("CurveFitting1"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("CurveFitting2", _resourceManager.GetString("CurveFitting2"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("CurveFitting3", _resourceManager.GetString("CurveFitting3"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("CurveFitting4", _resourceManager.GetString("CurveFitting4"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("CurveFitting5", _resourceManager.GetString("CurveFitting5"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("CurveFitting6", _resourceManager.GetString("CurveFitting6"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("CurveFitting7", _resourceManager.GetString("CurveFitting7"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("CurveFitting8", _resourceManager.GetString("CurveFitting8"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("PolyFitting2", _resourceManager.GetString("PolyFitting2"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("PolyFitting3", _resourceManager.GetString("PolyFitting3"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("SinFitting", _resourceManager.GetString("SinFitting"), FittingMethodType.Fitting));
            _fittingMethods.Add(new FittingMethodExpression("Derivation", _resourceManager.GetString("Derivation"), FittingMethodType.Fitting));

        }
        public void InitialzieStatisticMethods()
        {
            this._statisticMethods.Clear();
            _statisticMethods.Add(new FittingMethodExpression("CalcArea", _resourceManager.GetString("CalcArea"), FittingMethodType.Statistic));
            _statisticMethods.Add(new FittingMethodExpression("Cycle", _resourceManager.GetString("Cycle"), FittingMethodType.Statistic));
            _statisticMethods.Add(new FittingMethodExpression("Frequency", _resourceManager.GetString("Frequency"), FittingMethodType.Statistic));
            _statisticMethods.Add(new FittingMethodExpression("Average", _resourceManager.GetString("Average"), FittingMethodType.Statistic));
            _statisticMethods.Add(new FittingMethodExpression("Maximum", _resourceManager.GetString("Maximum"), FittingMethodType.Statistic));
            _statisticMethods.Add(new FittingMethodExpression("Minimum", _resourceManager.GetString("Minimum"), FittingMethodType.Statistic));
            _statisticMethods.Add(new FittingMethodExpression("Median", _resourceManager.GetString("Median"), FittingMethodType.Statistic));
            _statisticMethods.Add(new FittingMethodExpression("StandardError", _resourceManager.GetString("StandardError"), FittingMethodType.Statistic));

        }
        public bool CalcFit(string methodName, double[] xList, double[] yList,ref double[] newYList, ref double[] para)
        {
            bool success = false;
            if (string.Equals(methodName, "LineFitting"))
            {
                success = CalcLineFitting(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "CurveFitting1"))
            {
                success = CalcCurveFitting1(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "CurveFitting2"))
            {
                success = CalcCurveFitting2(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "CurveFitting3"))
            {
                success = CalcCurveFitting3(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "CurveFitting4"))
            {
                success = CalcCurveFitting4(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "CurveFitting5"))
            {
                success = CalcCurveFitting5(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "CurveFitting6"))
            {
                success = CalcCurveFitting6(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "CurveFitting7"))
            {
                success = CalcCurveFitting7(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "CurveFitting8"))
            {
                success = CalcCurveFitting8(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "PolyFitting2"))
            {
                success = CalcPolyFitting2(xList, yList, ref para, ref newYList);

            }
            else if (string.Equals(methodName, "PolyFitting3"))
            {
                success = CalcPolyFitting3(xList, yList, ref para, ref newYList);
            }
            else if (string.Equals(methodName, "SinFitting"))
            {
                success = CalcSinFitting(xList, yList, ref para, ref newYList);

            }
            else if (string.Equals(methodName, "Derivation"))
            {
                success = CalcDerivation(xList, yList, ref para, ref newYList);

            }
            
            return success;

        }
        public double CalcStatistic(string methodName,double[] xList, double[] yList)
        {
            double value = 0;
            if (string.Equals(methodName, "CalcArea"))
            {
                value = CalculateQtrap(xList, yList);
            }
            else if (string.Equals(methodName, "Cycle"))
            {
                CalCurveCycle(xList, yList,ref value);
            }
            else if (string.Equals(methodName, "Frequency"))
            {
                CalFrequency(xList, yList, ref value);
                //CalCurveFreq(xList, yList, ref value);
            }
            else if (string.Equals(methodName, "Average"))
            {
                value = Average(yList);
            }
            else if (string.Equals(methodName, "Maximum"))
            {
                value = Max(yList);
            }
            else if (string.Equals(methodName, "Minimum"))
            {
                value = Min(yList);
            }
            else if (string.Equals(methodName, "Median"))
            {
                value = Median(yList);
            }
            else if (string.Equals(methodName, "StandardError"))
            {
                value = StandardError(yList);

            }
            else if (string.Equals(methodName, "First"))
            {
                value = First(yList);

            }
            else if (string.Equals(methodName, "Last"))
            {
                value = Last(yList);

            }
            return value;
        }
        public double GetValue(string methodName, double x, double a, double b, double c, double d)
        {
            double value = 0;
            if (string.Equals(methodName, "LineFitting"))
            {
                value = CalculateLineFit(x, a, b);
            }
            else if (string.Equals(methodName, "CurveFitting1"))
            {
                value = CalculateCurveFitting1(x, a, b);
            }
            else if (string.Equals(methodName, "CurveFitting2"))
            {
                value = CalculateCurveFitting2(x, a, b);
            }
            else if (string.Equals(methodName, "CurveFitting3"))
            {
                value = CalculateCurveFitting3(x, a, b);
            }
            else if (string.Equals(methodName, "CurveFitting4"))
            {
                value = CalculateCurveFitting4(x, a, b);
            }
            else if (string.Equals(methodName, "CurveFitting5"))
            {
                value = CalculateCurveFitting5(x, a, b);
            }
            else if (string.Equals(methodName, "CurveFitting6"))
            {
                value = CalculateCurveFitting6(x, a, b);
            }
            else if (string.Equals(methodName, "CurveFitting7"))
            {
                value = CalculateCurveFitting7(x, a, b);
            }
            else if (string.Equals(methodName, "CurveFitting8"))
            {
                value = CalculateCurveFitting8(x, a, b);
            }
            else if (string.Equals(methodName, "PolyFitting2"))
            {
                value = CalculatePolyFitting2(x, c, b, a);

            }
            else if (string.Equals(methodName, "PolyFitting3"))
            {
                value = CalculatePolyFitting3(x, d, c, b,a);
            }
            else if (string.Equals(methodName, "SinFitting"))
            {
                value = CalculateSinFitting(x, a, b, c,d);

            }
            else if (string.Equals(methodName, "CalcArea"))
            {
                value = b;
            }
            else if (string.Equals(methodName, "Cycle"))
            {
                value = b;
            }
            else if (string.Equals(methodName, "Frequency"))
            {
                value = b;
            }
            else if (string.Equals(methodName, "Average"))
            {
                value = b;
            }
            else if (string.Equals(methodName, "Maximum"))
            {
                value = b;
            }
            else if (string.Equals(methodName, "Minimum"))
            {
                value = b;
            }
            else if (string.Equals(methodName, "Median"))
            {
                value = b;
            }
            else if (string.Equals(methodName, "StandardError"))
            {
                value = b;
            }
            else if (string.Equals(methodName, "First"))
            {
                value =b;

            }
            else if (string.Equals(methodName, "Last"))
            {
                value = b;

            }
            return value;
        }
        public string GetStr(string methodName, double[] A,double value )
        {
            string str = GetFittingExpression(methodName) ;
            if (string.Equals(methodName, "LineFitting"))
            {
                str = LineFittingStr(A);
            }
            else if (string.Equals(methodName, "CurveFitting1"))
            {
                str = CurveFitting1Str(A);
            }
            else if (string.Equals(methodName, "CurveFitting2"))
            {
                str = CurveFitting2Str(A);
            }
            else if (string.Equals(methodName, "CurveFitting3"))
            {
                str = CurveFitting3Str(A);
            }
            else if (string.Equals(methodName, "CurveFitting4"))
            {
                str = CurveFitting4Str(A);
            }
            else if (string.Equals(methodName, "CurveFitting5"))
            {
                str = CurveFitting5Str(A);
            }
            else if (string.Equals(methodName, "CurveFitting6"))
            {
                str = CurveFitting6Str(A);
            }
            else if (string.Equals(methodName, "CurveFitting7"))
            {
                str = CurveFitting7Str(A);
            }
            else if (string.Equals(methodName, "CurveFitting8"))
            {
                str = CurveFitting8Str(A);
            }
            else if (string.Equals(methodName, "PolyFitting2"))
            {
                str = PolyFitting2Str(A);

            }
            else if (string.Equals(methodName, "PolyFitting3"))
            {
                str = PolyFitting3Str(A);
            }
            else if (string.Equals(methodName, "SinFitting"))
            {
                str = SinFittingStr(A);

            }
            else if (string.Equals(methodName, "CalcArea"))
            {
                str = CalcAreaStr(value);
            }
            else if (string.Equals(methodName, "Cycle"))
            {
                str = CycleStr(value);
            }
            else if (string.Equals(methodName, "Frequency"))
            {
                str = FrequencyStr(value);
            }
            else if (string.Equals(methodName, "Average"))
            {
                str = AverageStr(value);
            }
            else if (string.Equals(methodName, "Maximum"))
            {
                str = MaxStr(value);
            }
            else if (string.Equals(methodName, "Minimum"))
            {
                str = MinStr(value);
            }
            else if (string.Equals(methodName, "Median"))
            {
                str = MedianStr(value);
            }
            else if (string.Equals(methodName, "StandardError"))
            {
                str = StandardErrorStr(value);
            }
            else if (string.Equals(methodName, "First"))
            {
                str= FirstStr(value);

            }
            else if (string.Equals(methodName, "Last"))
            {
                str = LastStr(value);

            }

            return str;
        }
        public bool IsFittingMethod( string methodName )
        {
            bool isFitting = false;
            for (int i = 0; i < FittingMethods.Count; i++)
            {
                if (string.Equals(FittingMethods[i].Name, methodName))
                {
                    isFitting = true;
                    break;
                }
            }
            return isFitting;
        }
        public bool IsCalculateMethod(string methodName)
        {
            bool isCalculateMethod = false;
            if (string.Equals(methodName, "CalcArea"))
            {
                isCalculateMethod = true;
            }
            else if (string.Equals(methodName, "Cycle"))
            {
                isCalculateMethod = true;
            }
            else if (string.Equals(methodName, "Frequency"))
            {
                isCalculateMethod = true;
            }
            return isCalculateMethod;
        }
        public string GetFittingExpression(string methodName)
        {
            string expression = methodName;
            for (int i = 0; i < this.Methods.Count; i++)
            {
                if(string.Equals( this.Methods[i].Name , methodName ))
                {
                    expression = this.Methods[i].Expression;
                    break;
                }
            }
            return expression;
        }
        public bool CalcLineFitting(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            double[] ax = new double[a.Length];
            bool sucess = LineFitting(x, y, ref ax);
            a[0] = ax[1];
            a[1] = ax[0];
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateLineFit(x[i], a[0], a[1]);
                }
            }
            return sucess;
            
        }
        /// <summary>
        /// 线性拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool LineFitting(double[] X, double[] Y, ref double[] A)
        {
            //X,Y --  X,Y两轴的坐标
            //A   --  结果参数

            int len = X.Length;
            if (len <= 1)
            {
                return false;
            }

            double tmp = 0;
            long i, j, k;
            double Z, D1, D2, C, P, G, Q;
            double[] B = new double[len];
            double[] T = new double[len];
            double[] S = new double[len];

            for (i = 0; i < 2; i++)
                A[i] = 0;

            Z = 0;
            B[0] = 1;
            D1 = len;
            P = 0;
            C = 0;
            for (i = 0; i < len; i++)
            {
                P = P + X[i] - Z;
                C = C + Y[i];
            }

            //加除零判断
            if (D1 >= 0.0 && D1 <= 0.0)
            {
                return false;
            }

            C = C / D1;
            P = P / D1;
            A[0] = C * B[0];

            if (2 > 1)
            {
                T[1] = 1;
                T[0] = -P;
                D2 = 0;
                C = 0;
                G = 0;
                for (i = 0; i < len; i++)
                {
                    Q = X[i] - Z - P;
                    D2 = D2 + Q * Q;
                    C = Y[i] * Q + C;
                    G = (X[i] - Z) * Q * Q + G;
                }

                //加除零判断
                if (D2 >= 0.0 && D2 <= 0.0)
                {
                    return false;
                }

                C = C / D2;
                P = G / D2;

                //加除零判断
                if (D1 >= 0.0 && D1 <= 0.0)
                {
                    return false;
                }

                Q = D2 / D1;
                D1 = D2;
                A[1] = C * T[1];
                A[0] = C * T[0] + A[0];

            }
            for (j = 2; j < 2; j++)
            {
                S[j] = T[j - 1];
                S[j - 1] = -P * T[j - 1] + T[j - 2];
                if (j >= 3)
                {
                    for (k = j - 2; k >= 1; k--)
                        S[k] = -P * T[k] + T[k - 1] - Q * B[k];
                }
                S[0] = -P * T[0] - Q * B[0];
                D2 = 0;
                C = 0;
                G = 0;
                for (i = 0; i < len; i++)
                {
                    Q = S[j];
                    for (k = j - 1; k >= 0; k--)
                        Q = Q * (X[i] - Z) + S[k];

                    D2 = D2 + Q * Q;
                    C = Y[i] * Q + C;
                    G = (X[i] - Z) * Q * Q + G;

                }

                //加除零判断
                if (D2 >= 0.0 && D2 <= 0.0)
                {
                    return false;
                }

                C = C / D2;
                P = G / D2;

                //加除零判断
                if (D1 >= 0.0 && D1 <= 0.0)
                {
                    return false;
                }

                Q = D2 / D1;
                D1 = D2;
                A[j] = C * S[j];

                T[j] = S[j];
                for (k = j - 1; k >= 0; k--)
                {
                    A[k] = C * S[k] + A[k];
                    B[k] = T[k];
                    T[k] = S[k];
                }
            }

            for (int nk = 0; nk < 2; nk++)
                tmp = A[nk];

            return true;
        }

        private bool PolySolve(double[] a, double[] b, int n)
        {
            for (int i = 0; i < n; i++)
            {
                // find pivot
                double mag = 0;
                int pivot = -1;

                for (int j = i; j < n; j++)
                {
                    double mag2 = Math.Abs(a[i + j * n]);
                    if (mag2 > mag)
                    {
                        mag = mag2;
                        pivot = j;
                    }
                }

                // no pivot: error
                if (pivot == -1 || mag == 0)
                    return false;

                // move pivot row into position
                if (pivot != i)
                {
                    double temp;
                    for (int j = i; j < n; j++)
                    {
                        temp = a[j + i * n];
                        a[j + i * n] = a[j + pivot * n];
                        a[j + pivot * n] = temp;
                    }

                    temp = b[i];
                    b[i] = b[pivot];
                    b[pivot] = temp;
                }

                // normalize pivot row
                mag = a[i + i * n];
                for (int j = i; j < n; j++)
                    a[j + i * n] /= mag;
                b[i] /= mag;

                // eliminate pivot row component from other rows
                for (int i2 = 0; i2 < n; i2++)
                {
                    if (i2 == i) continue;

                    double mag2 = a[i + i2 * n];

                    for (int j = i; j < n; j++)
                        a[j + i2 * n] -= mag2 * a[j + i * n];

                    b[i2] -= mag2 * b[i];
                }
            }

            return true;
        }
        /// <summary>
        /// 线形拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double CalculateLineFit(double X, double A, double B)
        {
            return A * X + B;
        }

        public string LineFittingStr(double[] A)
        {
            string str = "";
            if (A.Length >= 2)
            {
                str = "Y = " + Math.Round(A[0], Formula.Decimal) + " * X  " + (A[1] < 0 ?( Math.Round(A[1], Formula.Decimal)).ToString() :( " + " + Math.Round(A[1], Formula.Decimal)));
            }
            return str;
        }

        public bool CalcCurveFitting1(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = CurveFitting1(x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateCurveFitting1(x[i], a[0], a[1]);
                }
            }
            return sucess;

        }

        /// <summary>
        /// 乘幂拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool CurveFitting1(double[] X, double[] Y, ref double[] A)
        {
            double[] newX = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] > 0)//排除零值
                {
                    newX[i] = Math.Log(X[i]);
                }
                else
                {
                    if (i > 0)
                    {
                        newX[i] = newX[i - 1];
                    }
                    else
                    {
                        newX[i] = Math.Log(0.0001);
                    }
                }
            }
            double[] newY = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                if (Y[i] > 0)//排除零值
                {
                    newY[i] = Math.Log(Y[i]);
                }
                else
                {
                    if (i > 0)
                    {
                        newY[i] = newY[i - 1];
                    }
                    else
                    {
                        newY[i] = Math.Log(0.0001);
                    }
                }
            }
            return LineFitting(newX, newY, ref A);
        }
        /// <summary>
        /// 乘幂拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double CalculateCurveFitting1(double X, double A, double B)
        {
            double Y = B * Math.Log(X) + A;
            if (Y > Math.Log(1.7E308))
            {
                return 0;
            }
            else
            {
                return Math.Exp(Y);
            }

        }

        public string CurveFitting1Str(double[] A)
        {
            string str = "";
            if (A.Length >= 2)
            {
                str = "Y = Exp((" + Math.Round(A[1], Formula.Decimal) + ")*X+(" + Math.Round(A[0], Formula.Decimal) + "))";
            }
            return str;
        }

        public bool CalcCurveFitting2(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = CurveFitting2(x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateCurveFitting2(x[i], a[0], a[1]);
                }
            }
            return sucess;

        }


        /// <summary>
        /// 第n级反比拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool CurveFitting2(double[] X, double[] Y, ref double[] A)
        {
            double[] newX = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] > 0)//排除零值
                {
                    newX[i] = -1 * Math.Log(X[i]);
                }
                else
                {
                    if (i > 0)
                    {
                        newX[i] = newX[i - 1];
                    }
                    else
                    {
                        newX[i] = -1 * Math.Log(0.0001);
                    }
                }
            }
            double[] newY = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                if (Y[i] > 0)//排除零值
                {
                    newY[i] = Math.Log(Y[i]);
                }
                else
                {
                    if (i > 0)
                    {
                        newY[i] = newY[i - 1];
                    }
                    else
                    {
                        newY[i] = Math.Log(0.0001);
                    }
                }
            }
            return LineFitting(newX, newY, ref A);
        }
        /// <summary>
        /// 第N级反比拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double CalculateCurveFitting2(double X, double A, double B)
        {
            double Y = B * -1 * Math.Log(X) + A;
            if (Y > Math.Log(1.7E308))
            {
                return 0;
            }
            else
            {
                return Math.Exp(Y);
            }
        }

        public string CurveFitting2Str(double[] A)
        {
            string str = "";
            if (A.Length >= 2)
            {
                str = "Y = Exp(-1*(" + Math.Round(A[1], Formula.Decimal) + ")Log(X)+(" + Math.Round(A[0], Formula.Decimal) + "))";
            }
            return str;
        }

        public bool CalcCurveFitting3(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = CurveFitting3(x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateCurveFitting3(x[i], a[0], a[1]);
                }
            }
            return sucess;

        }


        /// <summary>
        /// 自然指数拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool CurveFitting3(double[] X, double[] Y, ref double[] A)
        {
            double[] newX = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                newX[i] = X[i];
            }
            double[] newY = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                newY[i] = Math.Log(Y[i],Math.E);

                //if (Y[i] == 0)//排除零值
                //{
                //    newY[i] = Math.Log(Y[i],Math.E);
                //}
                //else
                //{
                //    if (i > 0)
                //    {
                //        newY[i] = newY[i - 1];
                //    }
                //    else
                //    {
                //        newY[i] = Math.Log(0.0001);
                //    }
                //}
            }
            return LineFitting(newX, newY, ref A);
        }
        /// <summary>
        /// 自然指数拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double CalculateCurveFitting3(double X, double A, double B)
        {
            double Y = B*X + A;
            if (Y > Math.Log(1.7E308))
            {
                return 0;
            }
            else
            {
                return Math.Exp(Y);
            }

        }

        public string CurveFitting3Str(double[] A)
        {
            string str = "";
            if (A.Length >= 2)
            {
                str = "Y=Exp((" + Math.Round(A[1], Formula.Decimal) + ")*X+(" + Math.Round(A[0], Formula.Decimal) + "))";
            }
            return str;
        }

        public bool CalcCurveFitting4(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = CurveFitting4(x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateCurveFitting4(x[i], a[0], a[1]);
                }
            }
            return sucess;

        }

        /// <summary>
        /// 反比拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool CurveFitting4(double[] X, double[] Y, ref double[] A)
        {
            double[] newX = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] > 0)//排除零值
                {
                    newX[i] = 1 / X[i];
                }
                else
                {
                    if (i > 0)
                    {
                        newX[i] = newX[i - 1];
                    }
                    else
                    {
                        newX[i] = Math.Log(0.0001);
                    }
                }
            }
            double[] newY = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                newY[i] = Y[i];
            }
            return LineFitting(newX, newY, ref A);
        }
        /// <summary>
        /// 反比拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double CalculateCurveFitting4(double X, double A, double B)
        {
            if (X != 0)
            {
                return B / X + A;
            }
            else
            {
                return A;
            }
        }

        public string CurveFitting4Str(double[] A)
        {
            string str = "";
            if (A.Length >= 2)
            {
                str = "Y=(" + Math.Round(A[1], Formula.Decimal) + ")/X+(" + Math.Round(A[0], Formula.Decimal) + ")";
            }
            return str;
        }

        public bool CalcCurveFitting5(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = CurveFitting5(x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateCurveFitting5(x[i], a[0], a[1]);
                }
            }
            return sucess;

        }

        /// <summary>
        /// 平方倒数拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool CurveFitting5(double[] X, double[] Y, ref double[] A)
        {

            double[] newX = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] > 0)//排除零值
                {
                    newX[i] = 1 / (X[i] * X[i]);
                }
                else
                {
                    if (i > 0)
                    {
                        newX[i] = newX[i - 1];
                    }
                    else
                    {
                        newX[i] = Math.Log(0.0001);
                    }
                }
            }
            double[] newY = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                newY[i] = Y[i];
            }
            return LineFitting(newX, newY, ref A);
        }
        /// <summary>
        /// 平方倒数拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double CalculateCurveFitting5(double X, double A, double B)
        {
            if (X != 0)
            {
                return B / (X * X) + A;
            }
            else
            {
                return A;
            }
        }
        public string CurveFitting5Str(double[] A)
        {
            string str = "";
            if (A.Length >= 2)
            {
                str = "Y=(" + Math.Round(A[1], Formula.Decimal) + ")/(X^2)+(" + Math.Round(A[0], Formula.Decimal) + ")";
            }
            return str;
        }

        public bool CalcCurveFitting6(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = CurveFitting6(x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateCurveFitting6(x[i], a[0], a[1]);
                }
            }
            return sucess;

        }

        /// <summary>
        /// 自然对数拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool CurveFitting6(double[] X, double[] Y, ref double[] A)
        {
            double[] newX = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] > 0)//排除零值
                {
                    newX[i] = Math.Log(X[i]);
                }
                else
                {
                    if (i > 0)
                    {
                        newX[i] = newX[i - 1];
                    }
                    else
                    {
                        newX[i] = Math.Log(0.0001);
                    }
                }
            }
            double[] newY = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                newY[i] = Y[i];
            }
            return LineFitting(newX, newY, ref A);
        }


        /// <summary>
        /// 自然对数拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double CalculateCurveFitting6(double X, double A, double B)
        {
            //if (X != 0)
            //{
            //    return B * Math.Log(X) + A;
            //}
            //else
            //{
            //    return A;
            //}
            double Y = B * X + A;
            if (Y > Math.Log(1.7E308))
            {
                return 0;
            }
            if (X != 0)
            {
                return Math.Log(B * X + A);
            }
            else
            {
                return 0;
            }

        }

        public string CurveFitting6Str(double[] A)
        {
            string str = "";
            if (A.Length >= 2)
            {
                str = "Y=(" + Math.Round(A[1], Formula.Decimal) + ")Log(X)+(" + Math.Round(A[0], Formula.Decimal) + ")";
            }
            return str;
        }

        public bool CalcCurveFitting7(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = CurveFitting7(x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateCurveFitting7(x[i], a[0], a[1]);
                }
            }
            return sucess;

        }

        /// <summary>
        /// 10基对数拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool CurveFitting7(double[] X, double[] Y, ref double[] A)
        {
            double[] newX = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {

                newX[i] = X[i];
            }
            double[] newY = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                if (Y[i] > 0)//排除零值
                {
                    newY[i] = Math.Log10(Y[i]);
                }
                else
                {
                    if (i > 0)
                    {
                        newY[i] = newY[i - 1];
                    }
                    else
                    {
                        newY[i] = Math.Log10(0.0001);
                    }
                }
            }
            return LineFitting(newX, newY, ref A);
        }
        /// <summary>
        /// 10基指数拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double CalculateCurveFitting7(double X, double A, double B)
        {
            double Y = B * X + A;
            if (Y > Math.Log10(1.7E308))
            {
                return 0;
            }
            if (X != 0)
            {
                return Math.Log10(B * X + A);
            }
            else
            {
                return 0;
            }
        }

        public string CurveFitting7Str(double[] A)
        {
            string str = "";
            if (A.Length >= 2)
            {
                str = "Y=(" + Math.Round(A[1], Formula.Decimal) + ")Log10(X)+(" + Math.Round(A[0], Formula.Decimal) + ")";
            }
            return str;
        }
        public bool CalcCurveFitting8(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = CurveFitting8(x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateCurveFitting8(x[i], a[0], a[1]);
                }
            }
            return sucess;

        }

        /// <summary>
        /// 常用对数拟合
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool CurveFitting8(double[] X, double[] Y, ref double[] A)
        {
            double[] newX = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] > 0)//排除零值
                {
                    newX[i] = Math.Log(X[i]);
                }
                else
                {
                    if (i > 0)
                    {
                        newX[i] = newX[i - 1];
                    }
                    else
                    {
                        newX[i] = Math.Log(0.0001);
                    }
                }
            }
            double[] newY = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                newY[i] = Y[i];
            }
            return LineFitting(newX, newY, ref A);
        }
        /// <summary>
        /// 常用对数拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double CalculateCurveFitting8(double X, double A, double B)
        {
            return B * Math.Log(X) + A;
        }
        public string CurveFitting8Str(double[] A)
        {
            string str = "";
            if (A.Length >= 2)
            {
                str = "Y=(" + Math.Round(A[1], Formula.Decimal) + ")Log(X)+(" + Math.Round(A[0], Formula.Decimal) + ")";
            }
            return str;
        }
        public bool CalcPolyFitting2(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = PolyFitting(2,x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculatePolyFitting2(x[i], a[2], a[1],a[0]);
                }
            }
            return sucess;

        }
        public string PolyFitting2Str(double[] A)
        {
            string str = "";
            if (A.Length >= 3)
            {
                str = "Y=(" + Math.Round(A[2], Formula.Decimal) + ")X^2+(" + Math.Round(A[1], Formula.Decimal) + ")X+(" + Math.Round(A[0], Formula.Decimal) + ")";
            }
            return str;
        }
        public bool CalcPolyFitting3(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = PolyFitting(3, x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculatePolyFitting3(x[i], a[3], a[2], a[1],a[0]);
                }
            }
            return sucess;

        }
        public string PolyFitting3Str(double[] A)
        {
            string str = "";
            if (A.Length >= 4)
            {
                str = "Y=(" + Math.Round(A[3], Formula.Decimal) + ")X^3+(" + Math.Round(A[2], Formula.Decimal) + ")X^2+(" + Math.Round(A[1], Formula.Decimal) + ")X+(" + Math.Round(A[0], Formula.Decimal) + ")";
            }
            return str;
        }

        /// <summary>
        /// 多项式拟合
        /// </summary>
        /// <param name="d">次数</param>
        /// <param name="X">X数组</param>
        /// <param name="Y">Y数组</param>
        /// <param name="C">多项式因子数组</param>
        /// <returns>拟合是否成功</returns>
        public bool PolyFitting(int d, double[] X, double[] Y, ref double[] C)
        {
            int order = d + 1;
            int rows = X.Length;

            double[] Base = new double[order * rows];
            double[] alpha = new double[order * order];
            double[] alpha2 = new double[order * order];
            double[] beta = new double[order];

            try
            {
                // calc base
                for (int i = 0; i < order; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        int k = i + j * order;
                        Base[k] = i == 0 ? 1.0 : X[j] * Base[k - 1];
                    }
                }

                // calc alpha2
                for (int i = 0; i < order; i++)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        int k2 = 0;
                        int k3 = 0;
                        double sum = 0.0;
                        for (int k = 0; k < rows; k++)
                        {
                            k2 = i + k * order;
                            k3 = j + k * order;
                            sum += Base[k2] * Base[k3];
                        }

                        k2 = i + j * order;

                        alpha2[k2] = sum;

                        if (i != j)
                        {
                            k2 = j + i * order;
                            alpha2[k2] = sum;
                        }
                    }
                }

                // calc beta
                for (int j = 0; j < order; j++)
                {
                    double sum = 0;
                    int k3 = 0;
                    for (int k = 0; k < rows; k++)
                    {
                        k3 = j + k * order;
                        sum += Y[k] * Base[k3];
                    }

                    beta[j] = sum;
                }

                // get alpha
                for (int j = 0; j < order * order; j++)
                    alpha[j] = alpha2[j];

                // solve for params
                bool bRes = PolySolve(alpha, beta, order);

                for (int j = 0; j < order; j++)
                    C[j] = beta[j];
                return bRes;
            }
            catch
            {
                for (int v = 0; v < C.Length; v++)
                {
                    C[v] = 0;
                }
                return false;
            }

        }
        /// <summary>
        /// 2次拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public double CalculatePolyFitting2(double X, double A, double B, double C)
        {
            return A * X * X + B * X + C;
        }
        /// <summary>
        /// 3次拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public double CalculatePolyFitting3(double X, double A, double B, double C, double D)
        {
            return A * X * X * X + B * X * X + C * X + D;
        }

        /// <summary>
        /// 梯形面积计算
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public double CalculateQtrap(double[] X, double[] Y)
        {
            double result = 0;
            int len = Math.Min(X.Length, Y.Length);
            for (int i = 1; i < len; i++)
            {
                result +=  0.5 * (X[i] - X[i - 1]) * (Y[i] + Y[i - 1]);
            }
            return Math.Round ( result,Formula.Decimal );
        }
        public string CalcAreaStr(double value)
        {
            return  _resourceManager.GetString("CalcArea")+"=" + Math.Round(value, Formula.Decimal);
        }
        /// <summary>
        /// 求全周期
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cycleValue"></param>
        /// <returns></returns>
        public bool CalCurveCycle(double[] X, double[] Y, ref double cycleValue)
        {
            
            bool success = CalCurveFreq(X, Y, ref cycleValue);
            cycleValue = 2 * cycleValue;
            return success;
        }
        /// <summary>
        /// 周期字符串
        /// </summary>
        /// <param name="cycleValue"></param>
        /// <returns></returns>
        public string CycleStr(double cycleValue)
        {
            string str =  _resourceManager.GetString("Cycle")+"="+Math.Round(cycleValue,Formula.Decimal );

            return str;
        }
        /// <summary>
        /// 求频率
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="frequencyValue"></param>
        /// <returns></returns>
        public bool CalFrequency(double[] X, double[] Y, ref double frequencyValue)
        {
            bool success = CalCurveCycle(X, Y, ref frequencyValue);
            if (frequencyValue != 0)
            {
                frequencyValue = 1 / frequencyValue;
            }
            else
            {
                frequencyValue = double.MaxValue;
            }
            return success;
        }
        /// <summary>
        /// 频率字符串
        /// </summary>
        /// <param name="frequencyValue"></param>
        /// <returns></returns>
        public string FrequencyStr(double frequencyValue)
        {
            string str =  _resourceManager.GetString("Frequency")+"=" + Math.Round(frequencyValue, Formula.Decimal);

            return str;
        }
        /// <summary>
        /// 求半周期
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="fFreqlValue"></param>
        /// <returns></returns>
        public bool CalCurveFreq(double[] X, double[] Y, ref double fFreqlValue)
        {
            if (X.Length < 2 || Y.Length < 2 || X.Length != Y.Length)
            {
                return false;
            }

            double[] Xsort = new double[X.Length];
            double[] Ysort = new double[X.Length];
            double[] Xtemp = new double[X.Length];
            int Xindex = -1;
            X.CopyTo(Xtemp, 0);
            for (int i = 0; i < X.Length; i++)//按照X排序
            {
                double xmin = this.Min(Xtemp);
                Xsort[++Xindex] = xmin;
                for (int j = 0; j < Y.Length; j++)
                {
                    if (X[j] == xmin)
                    {
                        Ysort[Xindex] = Y[j];
                    }
                }
                double[] Xtemp2 = new double[Xtemp.Length -1 ];
                int tempno = -1;
                for (int j = 0; j < Xtemp.Length; j++)
                {
                    if (Xtemp[j] != xmin)
                    {
                        Xtemp2[++tempno] = Xtemp[j];
                    }
                }
                Xtemp = Xtemp2;
            }

            double[] Xmark = new double[X.Length];
            int no = -1;
            double Ysum = this.Average(Y);
            double prevY = Ysort[0];
            bool up = Ysort[0] < Ysum && Ysort[0] < Ysort[1];
            for (int i = 0; i < Ysort.Length; i++)
            {
                if (up && prevY > Ysum)//向上过平均点
                {
                    Xmark[++no] = Xsort[i];
                    up = false;
                }
                if (!up && prevY < Ysum)//向下过平均点
                {
//                    Xmark[++no] = Xsort[i];
                    up = true;
                }
                prevY = Ysort[i];
            }
            if (no < 1)
            {
                return false;
            }
            double Xsum = 0; ;
            for (int i = 0; i < no; i++)
            {
                Xsum += Math.Abs(Xmark[i + 1] - Xmark[i]);
            }
//            fFreqlValue = Xsum / no;
            fFreqlValue = Xsum / no /2;
            return true;
        }
        public bool CalcSinFitting(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            bool sucess = SinFitting( x, y, ref a);
            if (sucess)
            {
                newY = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newY[i] = CalculateSinFitting(x[i], a[0], a[1], a[2],a[3]);
                }
            }
            return sucess;

        }

        /// <summary>
        /// Sin拟合Y = A * Sin( 2Pie / B * X + C ) + D
        /// </summary>
        /// <param name="X">X轴数列</param>
        /// <param name="?">Y轴数列</param>
        /// <param name="A">参数数列</param>
        /// <returns>拟合参数</returns>
        public bool SinFitting(double[] X, double[] Y, ref double[] A)
        {
            if (A.Length < 4)
            {
                return false;
            }
            if (!this.CalCurveFreq(X, Y, ref A[1]))
            {
                return false;
            }
            double maxY = this.Max(Y);
            double medianY = this.Median(Y);
            double minY = this.Min(Y);
            A[1] = 2 * A[1];
            A[0] = Math.Abs(maxY - medianY);
            A[3] = medianY;
            A[2] = 0;
            double m = medianY;
            bool begin = false;
            for (int i = 1; i < Y.Length; i++)
            {
                double n = Y[i];
                if (n < m)
                {
                    begin = true;
                }
                if (begin)
                {
                    if (n > m)
                    {
                        A[2] = -1 * X[i];
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 正弦拟合求Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <returns></returns>
        public double CalculateSinFitting(double X, double A, double B, double C, double D)
        {
            return A * Math.Sin(2 * Math.PI / B * (X + C)) + D;
        }

        public string SinFittingStr(double[] A)
        {
            string str = "";
            if (A.Length >= 4)
            {
                str = "Y=(" + Math.Round(A[0], Formula.Decimal) + ")Sin(2*Pie/(" + Math.Round(A[1], Formula.Decimal) + ")(X+(" + Math.Round(A[2], Formula.Decimal) + ")))+(" + Math.Round(A[3], Formula.Decimal) + ")";
            }
            return str;
        }
        /// <summary>
        /// 求导
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <param name="newY"></param>
        /// <returns></returns>
        public bool CalcDerivation(double[] x, double[] y, ref double[] a, ref double[] newY)
        {
            //bool sucess = Derivation(x, y, ref a);
            newY = new double[x.Length-1];
            for (int i = 0; i < x.Length-1; i++)
            {
                newY[i] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
            }
            return true;
        }

        /// <summary>
        /// 求平均值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double Average(double[] values)
        {
            if (values.Length <= 0)
            {
                return 0;
            }
            double sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
            return sum / values.Length;
        }
        public string AverageStr(double value)
        {
            return  _resourceManager.GetString("Average")+"=" + Math.Round(value, Formula.Decimal);
        }

        /// <summary>
        /// 求中值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double Median(double[] values)
        {
            if (values.Length < 2)
            {
                return 0;
            }
            double max = values[0];
            double min = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (max < values[i])
                {
                    max = values[i];
                }
                if (min > values[i])
                {
                    min = values[i];
                }
            }
            return (max + min) / 2;
        }
        public string MedianStr(double value)
        {
            return  _resourceManager.GetString("Median")+"=" + Math.Round(value, Formula.Decimal);
        }

        /// <summary>
        /// Max
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double Max(double[] values)
        {
            if (values.Length <= 0)
            {
                return 0;
            }
            double max = values[0];
            for (int i = 0; i < values.Length; i++)
            {
                if (max < values[i])
                {
                    max = values[i];
                }
            }
            return max;
        }
        public string MaxStr(double value)
        {
            return  _resourceManager.GetString("Maximum")+"=" + Math.Round(value, Formula.Decimal);
        }

        /// <summary>
        /// Min
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double Min(double[] values)
        {
            if (values.Length <= 0)
            {
                return 0;
            }
            double min = values[0];
            for (int i = 0; i < values.Length; i++)
            {
                if (min > values[i])
                {
                    min = values[i];
                }
            }
            return min;
        }
        public string MinStr(double value)
        {
            return  _resourceManager.GetString("Minimum")+"=" + Math.Round(value, Formula.Decimal);
        }

        /// <summary>
        /// 第1个
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double First(double[] values)
        {
            if (values.Length > 0)
            {
                return values[0];
            }
            return 0;
        }
        public string FirstStr(double value)
        {
            return  _resourceManager.GetString("First")+"=" + Math.Round(value, Formula.Decimal);
        }

        /// <summary>
        /// 最后1个
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double Last(double[] values)
        {
            if (values.Length > 0)
            {
                return values[values.Length - 1];
            }
            return 0;
        }
        public string LastStr(double value)
        {
            return  _resourceManager.GetString("Last")+"=" + Math.Round(value, Formula.Decimal);
        }

        /// <summary>
        /// 求标准误差
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double StandardError(double[] values)
        {
            if (values.Length <= 1)
            {
                return 0;
            }
            double average = this.Average(values);
            double error = 0;
            for (int i = 0; i < values.Length; i++)
            {
                error += Math.Pow((values[i] - average), 2);
            }
            return Math.Sqrt(error / (values.Length - 1));
        }
        public string StandardErrorStr(double value)
        {
            return  _resourceManager.GetString("StandardError")+"=" + Math.Round(value, Formula.Decimal);
        }

        /// <summary>
        /// 求合计
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double Sum(double[] values)
        {
            double sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
            return sum;

        }
        public string SumStr(double value)
        {
            return _resourceManager.GetString("Sum") + "=" + Math.Round(value, Formula.Decimal);
        }

        #endregion
    }
    public enum FittingMethodType
    {
        Fitting , Statistic
    }
    public class FittingMethodExpression
    {
        string _name = "";
        string _expression = "";
        FittingMethodType _type = FittingMethodType.Fitting;
        public FittingMethodExpression( string name , string expression,FittingMethodType type )
        {
            _name = name;
            _expression = expression;
            _type = type;
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public string Expression
        {
            get
            {
                return _expression;
            }
        }
        public FittingMethodType MethodType
        {
            get
            {
                return _type;
            }
        }
        public static StatisticTypeDefine StatisticTypeDefineParse(string value)
        {
            StatisticTypeDefine type = StatisticTypeDefine.Average;
            int typeIndex;
            int.TryParse(value, out typeIndex);
            type = (StatisticTypeDefine)typeIndex;
            return type;
        }
    }
    public enum StatisticTypeDefine : int
    {
        Average = 0 , Maximum = 10 , Minimum = 20 ,Median = 30 , StandardError = 40 ,  Sum = 50,First = 60,Last = 70,Rang = 80,Diff = 90
    }
}
