using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAnalysis
{
    public class Formula
    {
        public Formula(int sectionIndex,int timeIndex,GetData getDataByTimeIndex,GetData getDataByDataIndex, GetDataTimeStamp getDataTimeStamp,GetDataTimeStamp getDataTimeStampByDataIndex,GetDataIndex getDataIndex, CheckName checkName)
        {
            _sectionIndex = sectionIndex;
            _timeIndex = timeIndex;
            _getDataByDataIndex = getDataByDataIndex;
            _getDataTimeStamp = getDataTimeStamp;
            _getDataTimeStampByDataIndex = getDataTimeStampByDataIndex;
            _getDataByTimeIndex = getDataByTimeIndex;
            _getDataIndex = getDataIndex;
            _checkName = checkName;
            _designMode = false;
        }
        public Formula(GetCaption getCaption, CheckTemplelteName checkTempleteName)
        {
            _getCaption = getCaption;
            _checkTempleteName = checkTempleteName;
            _designMode = true;
        }
        #region delegate
        public delegate double GetData(int sectionIndex, string name, int timeIndex);
        public delegate double GetDataTimeStamp(int sectionIndex, string name, int timeIndex);
        public delegate int GetDataIndex(int sectionIndex, string name, int timeIndex);
        public delegate bool CheckName(int sectionIndex, string name);
        public delegate string GetCaption(string name);
        public delegate bool CheckTemplelteName(string name);
        
        public GetData _getDataByTimeIndex = null;
        public GetData _getDataByDataIndex = null;
        public GetDataIndex _getDataIndex = null;
        public GetDataTimeStamp _getDataTimeStamp = null;
        public GetDataTimeStamp _getDataTimeStampByDataIndex = null;
        public GetCaption _getCaption = null;
        public CheckName _checkName = null;
        public CheckTemplelteName _checkTempleteName = null;
        #endregion

        #region Interface
        public static int Decimal = 6;

        bool _designMode;
        public bool DesignMode
        {
            get { return _designMode; }

        }
        int _sectionIndex;
        int _timeIndex;
        public int SctionIndex
        {
            get { return _sectionIndex; }
        }
        public int TimeIndex
        {
            get { return _timeIndex; }
        }
        bool isOperator(char singleChar)
        {
            switch (singleChar)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '(':
                case ')':
                case ',':
                    return true;
                default:
                    return false;
            }
        }
        bool IsOperator(string exprStr)
        {
            return (exprStr.Trim().Length == 1 && isOperator(exprStr[0]));
        }
        bool IsDigital(string exprStr)
        {
            double r;
            return double.TryParse(exprStr, out r);
        }
        FormulaCollection _formulaCollection = new FormulaCollection();
        bool IsFunction(string exprStr)
        {
            return _formulaCollection.IsFormula(exprStr);
        }
        bool IsColumn(string exprStr)
        {
            bool isColumn = false;
            if (_designMode)
            {
                if (_checkTempleteName != null)
                {
                    isColumn = _checkTempleteName(exprStr);
                }
            }
            else
            {
                if (_checkName != null)
                {
                    isColumn = _checkName(_sectionIndex, exprStr);
                }
            }
            return isColumn;
        }
        #endregion

        #region Methods
        public void Initialize(int sectionIndex, int timeIndex, GetData getDataByTimeIndex,GetData getDataByDataIndex, GetDataTimeStamp getDataTimeStamp, GetDataTimeStamp getDataTimeStampByDataIndex, GetDataIndex getDataIndex, CheckName checkName)
        {
            _sectionIndex = sectionIndex;
            _timeIndex = timeIndex;
            _getDataTimeStamp = getDataTimeStamp;
            _getDataTimeStampByDataIndex = getDataTimeStampByDataIndex;
            _getDataByTimeIndex = getDataByTimeIndex;
            _getDataByDataIndex = getDataByDataIndex;
            _checkName = checkName;
            _getDataIndex = getDataIndex;
            _designMode = false;
        }
        public string Description(string exprStr)
        {
            string desc = "";
            string[] strList = Divid(exprStr).ToArray();
            for (int i = 0; i < strList.Length; i++)
            {
                if (D_CheckTempleteNameConstans(strList[i]))
                {
                    desc += D_GetNameCaption(strList[i]);
                }
                else if (IsDigital(strList[i]))
                {
                    desc += strList[i];
                }
                else if (IsFunction(strList[i]))
                {
                    desc += strList[i];
                }
                else if (IsOperator(strList[i]))
                {
                    desc += strList[i];
                }
                else
                {
                    desc += strList[i];
                }
            }
            return desc;
        }

        public double D_GetDataByTimeIndex(string name, int timeIndex)
        {
            double value = 0;
            try
            {
                if (_getDataByTimeIndex != null)
                {
                    if (!double.TryParse(name, out value))
                    {
                        value = _getDataByTimeIndex(_sectionIndex, name, timeIndex);
                    }
                }
            }
            catch
            {
                value = 0;
            }
            return value;
        }
        public double D_GetDataByDataIndex(string name, int dataIndex)
        {
            double value = 0;
            try
            {
                if (_getDataByTimeIndex != null)
                {
                    if (!double.TryParse(name, out value))
                    {
                        value = _getDataByDataIndex(_sectionIndex, name, dataIndex);
                    }
                }
            }
            catch
            {
                value = 0;
            }
            return value;
        }
        public double D_GetTimeStampByTimeIndex(string name, int timeIndex)
        {
            double value = 0;
            try
            {
                if (_getDataTimeStamp != null)
                {
                    value = _getDataTimeStamp(_sectionIndex, name, timeIndex);
                }
            }
            catch
            {
                value = 0;
            }
            return value;
        }
        public double D_GetTimeStampByDataIndex(string name, int dataIndex)
        {
            double value = 0;
            try
            {
                if (_getDataTimeStamp != null)
                {
                    value = _getDataTimeStampByDataIndex(_sectionIndex, name, dataIndex);
                }
            }
            catch
            {
                value = 0;
            }
            return value;
        }
        public int D_GetDataIndexByTimeIndex(string name, int timeIndex)
        {
            int value = 0;
            try
            {
                if (_getDataIndex != null)
                {
                    value = _getDataIndex(_sectionIndex, name, timeIndex);
                }
            }
            catch
            {
                value = 0;
            }
            return value;
        }
        public bool D_CheckTempleteNameConstans(string name)
        {
            bool constains = false;
            try
            {
                if (_checkTempleteName != null)
                {
                    constains = _checkTempleteName(name);
                }
            }
            catch
            {
                constains = false;
            }
            return constains;
        }

        public string D_GetNameCaption(string name)
        {
            string caption = "";
            try
            {
                if (_getCaption != null)
                {
                    caption = _getCaption(name);
                }
            }
            catch
            {
                caption = "";
            }
            return caption;
        }
        public List<string> Divid(string exprStr)
        {
            char[] chars = exprStr.ToCharArray();
            string singleStr = "";
            bool afterOperatorFunction = true;
            List<string> indExpression = new List<string>();
            for (int i = 0; i < chars.Length; i++)
            {
                if (isOperator(chars[i]))
                {
                    if (singleStr.Trim().Length > 0)
                    {
                        indExpression.Add(singleStr.Trim());
                    }
                    if (afterOperatorFunction && chars[i] == '-' && (i < chars.Length - 1 && char.IsDigit(chars[i + 1])))
                    {
                        singleStr = "-";
                    }
                    else
                    {
                        indExpression.Add(chars[i].ToString());
                        singleStr = "";
                    }
                }
                else if (chars[i] == ' ')
                {
                    if (singleStr.Trim().Length > 0)
                    {
                        indExpression.Add(singleStr.Trim());
                    }
                    singleStr = "";

                }
                else
                {
                    afterOperatorFunction = false;
                    singleStr += chars[i].ToString();
                }
            }
            if (singleStr.Trim().Length > 0)
            {
                indExpression.Add(singleStr);
            }
            return indExpression;

        }
        bool GetSub(int index, string[] dividedStr,out List<string> sub)
        {
            if (index >= dividedStr.Length - 2 || dividedStr[index] != "(")
            {
                sub = null;
                return false;
            }
            sub = new List<string>();
            int sCount = 0;
            int eCount = 0;
            //sub.Add("(");
            for (int i = 1; i < dividedStr.Length; i++)
            {
                if (dividedStr[i] == "(")
                {
                    sCount++;
                }
                else if (dividedStr[i] == ")")
                {
                    eCount++;
                }
                sub.Add(dividedStr[i]);
                if (sCount == eCount)
                {
                    break;
                }
            }
            if (sCount != eCount)
            {
                return false;
            }
            return true;
        }
        bool CheckSegment(List<string> sub, int parts)
        {
            int sCount = 0;
            int eCount = 0;
            int segCount = 0;
            for (int i = 0; i < sub.Count; i++)
            {
                if (sub[i] == "(")
                {
                    sCount++;
                }
                else if (sub[i] == ")")
                {
                    eCount++;
                    if (sCount == eCount)
                    {
                        segCount++;
                    }
                }
                else if (sub[i] == ",")
                {
                    if (sCount - 1 == eCount)
                    {
                        segCount++;
                    }
                }
                //else
                //{
                //    segCount++;
                //}
            }
            return (segCount == parts);
        }

        public bool CheckSyntax(string exprStr)
        {
            bool haveError = false;
            bool afterOperatorFunction = true;
            string[] dividStrs = Divid(exprStr).ToArray();
            //bool afterDigital = false;
            for (int i = 0; i < dividStrs.Length; i++)
            {
                if (afterOperatorFunction && (IsOperator(dividStrs[i]) && (dividStrs[i] != "(" && dividStrs[i] != ")" 
                    && !(dividStrs[i] == "-" && i < dividStrs.Length -1 && IsDigital( dividStrs[i+1])) ))) // 连续两个计算符
                {
                    haveError = true;
                }
                //是否有非法字符串
                if (!IsOperator(dividStrs[i]) && !IsFunction(dividStrs[i]) && !IsColumn(dividStrs[i]) && !IsDigital(dividStrs[i]))
                {
                    haveError = true;
                }
                if (IsOperator(dividStrs[i]) && i == dividStrs.Length - 1 && dividStrs[dividStrs.Length - 1] != ")")//最后一个是计算符
                {
                    haveError = true;
                }
                if (IsFunction(dividStrs[i]))
                {
                    List<string> sub;
                    if (!GetSub(i + 1,dividStrs, out sub))//判断函数的括号配对
                    {
                        haveError = true;
                    }
                    else
                    {
                        if (!CheckSegment(sub, this._formulaCollection.Get(dividStrs[i]).Parts))//判断函数体内段数量
                        {
                            haveError = true;
                        }
                    }
                }
                afterOperatorFunction = IsOperator(dividStrs[i]) && (dividStrs[i] != "(" && dividStrs[i] != ")");

            }
            return !haveError;
        }
        public double Calculate(string exprString)
        {
            return CalculateDivid(Divid(exprString));
        }
        double CalculateDivid(List<string> dividExpression)
        {
            List<string> afterFunc = new List<string>();
            //int newExpNumber = 0;
            //计算函数
            for (int i = 0; i < dividExpression.Count; i++)
            {
                if (IsFunction(dividExpression[i].ToString()))
                {
                    int jumpStep = 1;
                    double result = this.CalcFunc(dividExpression[i], dividExpression, i + 1, ref jumpStep);
                    afterFunc.Add(result.ToString());
                    i += jumpStep;
                }
                else
                {
                    afterFunc.Add(dividExpression[i]);
                }
            }
            List<string> afterBracket = new List<string>();
            //计算括号
            for (int i = 0; i < afterFunc.Count; i++)
            {
                if (afterFunc[i] == "(")
                {
                    List<string> temp = this.FindBracketPair(afterFunc, i);
                    afterBracket.Add(CalculateDivid(temp).ToString());
                    i += temp.Count + 1;
                }
                else
                {
                    afterBracket.Add(afterFunc[i]);
                }
            }
            return CalcMD(afterBracket);
        }
        private double CalcFunc(string funcName, List<string> source, int startNumber, ref int JumpStep)
        {
            List<string> pair = this.FindBracketPair(source, startNumber);
            double result = 0;
            switch (funcName.ToLower())
            {
                case "delayindex":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.DelayIndex(pair[0]);
                    }
                    break;
                case "index":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Index(pair[0]);
                    }
                    break;
                case "delayindexdata":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.DelayIndexData(pair[0], pair[2]);
                    }
                    break;
                case "indexdata":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.IndexData(pair[0], pair[2]);
                    }
                    break;
                case "tm":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.TimeStamp(pair[0]);
                    }
                    break;
                case "time":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Time(pair[0]);
                    }
                    break;
                case "abs":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Abs(pair[0]);
                    }
                    break;
                case "sin":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Sin(pair[0]);
                    }
                    break;
                case "cos":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Cos(pair[0]);
                    }
                    break;

                case "tan":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Tan(pair[0]);
                    }
                    break;

                case "cot":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Cot(pair[0]);
                    }
                    break;

                case "exp":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Exp(pair[0]);
                    }
                    break;

                case "log10":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Log10(pair[0]);
                    }
                    break;

                case "sqrt":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.Sqrt(pair[0]);
                    }
                    break;
                case "rem":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Rem(pair[0].ToString(), pair[2].ToString());
                    }
                    break;

                //result = this.Rem(Calculate(FindFuncV(pair, 2)[0]).ToString(), Calculate(FindFuncV(pair, 2)[1]).ToString());
                //break;
                case "log":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Log(pair[0].ToString(), pair[2].ToString());
                    }
                    break;

                //result = this.Log(Calculate(FindFuncV(pair, 2)[0]).ToString(), Calculate(FindFuncV(pair, 2)[1]).ToString());
                //break;
                case "pow":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Pow(pair[0].ToString(), pair[2].ToString());
                    }
                    break;
                //result = this.Pow(Calculate(FindFuncV(pair, 2)[0]).ToString(), Calculate(FindFuncV(pair, 2)[1]).ToString());
                //break;
                case "rnd":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Round(pair[0].ToString(), pair[2].ToString());
                    }
                    break;
                //result = this.Round(Calculate(FindFuncV(pair, 2)[0]).ToString(), Calculate(FindFuncV(pair, 2)[1]).ToString());
                //break;
                case "smth":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Smooth(pair[0].ToString(), pair[2].ToString());
                    }
                    break;
                case "int":
                    if (pair.Count > 5)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else
                    {
                        result = this.Integral(FindFuncV(pair, 3)[0][0].ToString(), FindFuncV(pair, 3)[1][0].ToString(), FindFuncV(pair, 3)[2][0].ToString());
                    }
                    break;
                case "dif":
                    if (pair.Count > 5)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else
                    {
                        result = this.Dif(FindFuncV(pair, 3)[0][0].ToString(), FindFuncV(pair, 3)[1][0].ToString(), FindFuncV(pair, 3)[2][0].ToString());
                    }
                    break;
                case "tsl":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.TimeDif(pair[0].ToString());
                    }
                    break;
                case "tad":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.TimeAdd(pair[0].ToString());
                    }
                    break;
                case "tsl2":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Tsl2(pair[0].ToString(), pair[2].ToString());
                    }
                    break;
                case "min":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Min(pair[0].ToString(), pair[2].ToString());
                    }
                    break;

                //result = this.Min(Calculate(FindFuncV(pair, 2)[0]).ToString(), Calculate(FindFuncV(pair, 2)[1]).ToString());
                //break;
                case "max":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Max(pair[0].ToString(), pair[2].ToString());
                    }
                    break;

                //result = this.Max(Calculate(FindFuncV(pair, 2)[0]).ToString(), Calculate(FindFuncV(pair, 2)[1]).ToString());
                //break;
                case "Avg":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Avg(pair[0].ToString(), pair[2].ToString());
                    }
                    break;

                //result = this.Avg(Calculate(FindFuncV(pair, 2)[0]).ToString(), Calculate(FindFuncV(pair, 2)[1]).ToString());
                //break;
                case "hr":
                    if (pair.Count > 1)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 1)
                    {
                        result = this.HeartRate(pair[0].ToString());
                    }
                    break;
                case "get":
                    if (pair.Count > 3)
                    {
                        result = this.CalculateDivid(pair);
                    }
                    else if (pair.Count == 3)
                    {
                        result = this.Get(pair[0].ToString(),pair[2].ToString());
                    }
                    break;

            }
            JumpStep = pair.Count + 2;//传人的是前括号位置，现包含后括号
            return result;
        }
        private double CalcMD(List<string> dividExpression)
        {
            List<string> afterMD = new List<string>();
            int ind = 0;
            while (ind < dividExpression.Count)
            {
                if (dividExpression[ind].ToString() == "*")
                {
                    afterMD[afterMD.Count - 1] = this.Multiply(afterMD[afterMD.Count - 1].ToString(), dividExpression[ind + 1].ToString()).ToString();
                    ind++;
                }
                else if (dividExpression[ind].ToString() == "/")
                {
                    afterMD[afterMD.Count - 1] = this.Division(afterMD[afterMD.Count - 1].ToString(), dividExpression[ind + 1].ToString()).ToString();
                    ind++;
                }
                else
                {
                    //double result = 0;
                    //double.TryParse( dividExpression[ind], out result);
                    afterMD.Add(dividExpression[ind]);
                }
                ind++;
            }
            double final = 0;
            ind = 0;
            //加减运算
            for (int i = 0; i < afterMD.Count; i++)
            {
                if (i == 0)
                {
                    final =D_GetDataByTimeIndex( afterMD[i].ToString(),_timeIndex );
                }
                else if (afterMD[i].ToString() == "+" && i > 0 && i < afterMD.Count)
                {
                    final = this.Add(final.ToString(), afterMD[++i].ToString());
                }
                else if (afterMD[i].ToString() == "-" && i > 0 && i < afterMD.Count)
                {
                    final = this.Minus(final.ToString(), afterMD[++i].ToString());
                }
            }
            return final;
        }
        private List<string>[] FindFuncV(List<string> source, int number)
        {
            List<string>[] final = new List<string>[number];
            for (int i = 0; i < number; i++)
            {
                final[i] = new List<string>();
            }
            int nowPara = 0;
            for (int i = 0; i < source.Count; i++)
            {
                if (this.IsFunction(source[i]))
                {
                    final[nowPara].Add(source[i]);
                    final[nowPara].Add("(");
                    int jumpStep = 0;
                    List<string> temp = this.FindBracketPair(source, i + 1);
                    jumpStep = temp.Count + 2;
                    for (int j = 0; j < temp.Count; j++)
                    {
                        final[nowPara].Add(temp[j]);
                    }
                    final[nowPara].Add(")");
                    i += jumpStep;
                }
                else if (source[i] == "(")
                {
                    final[nowPara].Add("(");
                    int jumpStep = 0;
                    List<string> temp = this.FindBracketPair(source, i);
                    jumpStep = temp.Count + 1;
                    for (int j = 0; j < temp.Count; j++)
                    {
                        final[nowPara].Add(temp[j]);
                    }
                    final[nowPara].Add(")");
                    i += jumpStep;
                }
                else if (source[i] == ",")
                {
                    nowPara++;
                }
                else
                {
                    final[nowPara].Add(source[i]);
                }
            }
            return final;
        }
        private List<string> FindBracketPair(List<string> source, int startNumber)
        {
            int number = 0;
            int startBracket = 0;
            int endBracket = 0;
            for (int i = startNumber; i < source.Count; i++)
            {
                if (source[i] == "(")
                {
                    startBracket++;
                }
                if (source[i] == ")")
                {
                    endBracket++;
                }
                if (startBracket == endBracket)
                {
                    number = i;
                    break;
                }
            }
            List<string> final = new List<string>();
            for (int i = startNumber + 1; i < number; i++)
            {
                final.Add(source[i]);
            }
            return final;
        }

        #endregion

        #region algorithm
        /// <summary>
        /// 加法V1+V2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Add(string v1, string v2)
        {
            return  D_GetDataByTimeIndex(v1, _timeIndex) + D_GetDataByTimeIndex(v2, _timeIndex);
        }
        /// <summary>
        /// 减法v1-v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Minus(string v1, string v2)
        {
            return  D_GetDataByTimeIndex(v1, _timeIndex) - D_GetDataByTimeIndex(v2, _timeIndex);
        }
        /// <summary>
        /// 乘法v1*v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Multiply(string v1, string v2)
        {
            return D_GetDataByTimeIndex(v1, _timeIndex) * D_GetDataByTimeIndex(v2, _timeIndex);
        }
        /// <summary>
        /// 除法V1/V2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Division(string v1, string v2)
        {
            double value1 = D_GetDataByTimeIndex(v1, _timeIndex);
            double value2 = D_GetDataByTimeIndex(v2, _timeIndex);
            return value2 == 0 ? 0 : value1 / value2;
        }
        /// <summary>
        /// 求时间索引
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double DelayIndex(string v1)
        {
            return _timeIndex;
            //return D_GetTimeIndexByIndex(v1, _index);
        }
        /// <summary>
        /// 取得指定时间轴索引的数据(尚未实现)
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double DelayIndexData(string v1, string v2)
        {
            
            return D_GetDataByTimeIndex(v1, _timeIndex);
            //int delayIndex;
            //InstantDataRecord dataRecord = this.GetRowValue(v2, _time, _index, 0);
            //int.TryParse(dataRecord.Value.ToString(), out delayIndex);
            //CalculateData data = _calculateDataManager.GetCalculateData(v1);
            //if (data != null)
            //{
            //    if (data.DataCollection.Count >= _time && _time > 0)
            //    {
            //        CalculateDataStore store = data.DataCollection[_time - 1];
            //        for (int i = 0; i < store.Datas.Length; i++)
            //        {
            //            if (store.Datas[i].DelayIndex == delayIndex)
            //            {
            //                value = store.Datas[i].Value;
            //                break;
            //            }
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 求时间索引
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double Index(string v1)
        {
            return (double)D_GetDataIndexByTimeIndex( v1,_timeIndex );
        }
        /// <summary>
        /// 按索引取得数据
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double IndexData(string v1, string v2)
        {
            int index;
            int.TryParse(v2, out index);
            return D_GetDataByDataIndex(v1,index) ;
        }
        /// <summary>
        /// 求时间戳
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double TimeStamp(string v1)
        {
            return D_GetTimeStampByTimeIndex( v1,_timeIndex );
        }
        /// <summary>
        /// 求实验次数
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double Time(string v1)
        {
            return (double)(_sectionIndex+1);

        }
        /// <summary>
        /// 求绝对值
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double Abs(string v1)
        {
            return Math.Abs(D_GetDataByTimeIndex(v1, _timeIndex));
        }
        /// <summary>
        /// 按照索引获取数据
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Get(string v1, string v2)
        {
            int index;
            int.TryParse(v2, out index);
            return D_GetDataByTimeIndex(v1, index);
        }
        /// <summary>
        /// 正弦（弧度）
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double Sin(string v1)
        {
            return Math.Sin(D_GetDataByTimeIndex(v1, _timeIndex) * Math.PI / 180);

        }
        /// <summary>
        /// 余弦（弧度）
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double Cos(string v1)
        {
            return Math.Cos( D_GetDataByTimeIndex(v1, _timeIndex) * Math.PI / 180);
        }
        /// <summary>
        /// 正切（弧度）
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double Tan(string v1)
        {
            return Math.Tan(D_GetDataByTimeIndex(v1, _timeIndex) * Math.PI / 180);
        }
        /// <summary>
        /// 余切（弧度）
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double Cot(string v1)
        {
            double result = Math.Cos(D_GetDataByTimeIndex(v1, _timeIndex) * Math.PI / 180);
            return result == 0 ? 0 : 1 / result;
        }
        /// <summary>
        /// e的指定次幂
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double Exp(string v1)
        {
            return Math.Exp(D_GetDataByTimeIndex(v1, _timeIndex));
        }
        /// <summary>
        /// 最小值
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Min(string v1, string v2)
        {
            return Math.Min(D_GetDataByTimeIndex(v1, _timeIndex), D_GetDataByTimeIndex(v2, _timeIndex));

        }
        /// <summary>
        /// 最大值
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Max(string v1, string v2)
        {
            return Math.Max(D_GetDataByTimeIndex(v1, _timeIndex), D_GetDataByTimeIndex(v2, _timeIndex));

        }
        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Avg(string v1, string v2)
        {
            return (D_GetDataByTimeIndex(v1, _timeIndex) + D_GetDataByTimeIndex(v2, _timeIndex)) / 2;


        }

        /// <summary>
        /// 两数相除之余v1/v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Rem(string v1, string v2)
        {
            return Math.IEEERemainder(D_GetDataByTimeIndex(v1, _timeIndex), D_GetDataByTimeIndex(v2, _timeIndex));

        }
        /// <summary>
        /// 任意底的对数v1计算数，v2底数
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Log(string v1, string v2)
        {
            return Math.Log(D_GetDataByTimeIndex(v1, _timeIndex), D_GetDataByTimeIndex(v2, _timeIndex));

        }
        /// <summary>
        /// 10底的对数v1计算数
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Log10(string v1)
        {
            return Math.Log10(D_GetDataByTimeIndex(v1, _timeIndex));
        }
        /// <summary>
        /// 任意数的指定次幂v1底数v2指定次
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Pow(string v1, string v2)
        {
            return Math.Pow(D_GetDataByTimeIndex(v1, _timeIndex), D_GetDataByTimeIndex(v2, _timeIndex));
        }
        /// <summary>
        /// 舍入v1计算数，v2小数位数
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Round(string v1, string v2)
        {
            int i = (int)Math.Round(D_GetDataByTimeIndex(v2, _timeIndex), 0);
            return Math.Round(D_GetDataByTimeIndex(v1, _timeIndex), i);
        }

        /// <summary>
        /// 求平方根
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double Sqrt(string v1)
        {
            return Math.Sqrt(D_GetDataByTimeIndex(v1, _timeIndex));
        }

        /// <summary>
        /// 积分
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Integral(string v1, string v2, string v3)
        {
            return D_GetDataByTimeIndex(v1, _timeIndex) + D_GetDataByTimeIndex(v2, _timeIndex) * D_GetDataByTimeIndex(v3, _timeIndex);
        }
        /// <summary>
        /// 求导，v1被除数，v2除数，v3点间隔数
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <returns></returns>
        private double Dif(string v1, string v2, string v3)
        {
            double value11 = 0, value12 = 0, value21 = 0, value22 = 0, value3;
            //double.TryParse(v3, out value3);
            //int jump = (int)Math.Round(value3 / 2, 0);
            //int prevIndex = (_timeIndex - jump) < 0 ? 0 : (_timeIndex - jump);
            int jump =Convert .ToInt32 ( v3);
            int prevIndex=(_timeIndex-jump)<0?0:(_timeIndex-jump ) ;
            int afterIndex = _timeIndex;

            double data11 = D_GetDataByTimeIndex(v1, afterIndex);
            double data12 = D_GetDataByTimeIndex(v1, prevIndex);

            double data21 = D_GetDataByTimeIndex(v2, afterIndex);
            double data22 = D_GetDataByTimeIndex(v2, prevIndex);
            value11 = data11;
            value12 = data12;
            value21 = data21;
            value22 = data22;
            double dt = value21 - value22;

            return (value11 - value12) / (dt == 0 ? 1 : dt);
        }
        /// <summary>
        /// 同一列上的两个数据之间的差额
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double TimeDif(string v1)
        {
            int dataIndex = D_GetDataIndexByTimeIndex(v1, _timeIndex);
            int prevIndex = dataIndex > 0 ? dataIndex - 1 : 0;

            return D_GetTimeStampByDataIndex(v1,dataIndex) - D_GetTimeStampByDataIndex(v1,prevIndex);
        }
        /// <summary>
        /// 同一列上的两个数据之和
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double TimeAdd(string v1)
        {
            int dataIndex = D_GetDataIndexByTimeIndex(v1,_timeIndex);
            int prevIndex = dataIndex > 0 ? dataIndex - 1 : 0;
            return D_GetDataByDataIndex(v1, prevIndex) + D_GetDataByDataIndex(v1, dataIndex);
        }
        /// <summary>
        /// 两个列同一行数据之间的差额（同一个Index）
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double Tsl2(string v1, string v2)
        {
            return D_GetDataByTimeIndex(v1, _timeIndex) - D_GetDataByTimeIndex(v2, _timeIndex);
        }
        private double HeartRate(string v1)
        {
            Fitting fitting = new Fitting();
            List<double> xList = new List<double>();
            List<double> yList = new List<double>();
            double data1 = D_GetTimeStampByTimeIndex(v1, _timeIndex);
            
            double startTime = data1;
            double endTime = (startTime >= 10) ? (startTime - 10) : 0;
            int index = _timeIndex;
            double timeMark = startTime;
            while (timeMark >= endTime)
            {
                double dataValue = D_GetDataByTimeIndex(v1, index);
                double data = D_GetTimeStampByTimeIndex(v1, index);
                xList.Add(data);
                yList.Add(dataValue);
                timeMark = data;
                index--;
                if (index < 0)
                {
                    break;
                }
            }
            double[] x = xList.ToArray();
            double[] y = yList.ToArray();
            double frequency = 0;
            fitting.CalCurveFreq(x, y, ref frequency);
            return frequency * 60;
        }
        private double Smooth(string v1, string v2)
        {
            return 0;
        }

        #endregion
    }
}
