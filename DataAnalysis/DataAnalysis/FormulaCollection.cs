using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;

namespace DataAnalysis
{
    public class FormulaCollection
    {
        ResourceManager _resourceManager = new ResourceManager("DataAnalysis.FormulaCollection",
        Assembly.GetExecutingAssembly());

        List<FormulaDefine> _formulas = new List<FormulaDefine>();
        public List<FormulaDefine> Formulas
        {
            get
            {
                return _formulas;
            }
        }
        public List<string> Functions
        {
            get
            {
                List<string> functions = new List<string>();
                for (int i = 0; i < this._formulas.Count; i++)
                {
                    functions.Add(this._formulas[i].Name.ToLower());
                }
                return functions;
            }
        }
        public List<string> FunctionExpressions
        {
            get
            {
                List<string> functions = new List<string>();
                for (int i = 0; i < this._formulas.Count; i++)
                {
                    functions.Add(this._formulas[i].Expression.ToLower());
                }
                return functions;
            }
        }
        public FormulaCollection()
        {

            InitializeFormulas();

        }
        public FormulaDefine Get(string name)
        {
            for (int i = 0; i < this._formulas.Count; i++)
            {
                if (this._formulas[i].Name.ToLower() == name.Trim().ToLower())
                {
                    return this._formulas[i];
                }
            }
            return null;
        }
        public bool IsFormula(string name)
        {
            return Get(name) != null;
        }
        public void InitializeFormulas()
        {
            //_formulas.Add(new FormulaDefine("Time", "Time( )", "获得数据的实验次数", 1));
            //_formulas.Add(new FormulaDefine("Index", "Index( )", "获得数据索引", 1));
            //_formulas.Add(new FormulaDefine("DelayIndex", "DelayIndex( )", "获得数据的对应时间轴索引", 1));
            //_formulas.Add(new FormulaDefine("IndexData", "IndexData( , )", "按索引取得数据", 2));
            //_formulas.Add(new FormulaDefine("DelayIndexData", "DelayIndexData( , )", "按数据的对应时间轴索引获得数据", 2));
            //_formulas.Add(new FormulaDefine("Get", "Get( , )", "按数据的顺序索引获得数据", 2));
            //_formulas.Add(new FormulaDefine("TM", "TM( )", "获得数据的时间戳", 1));
            //_formulas.Add(new FormulaDefine("Abs", "Abs( )", "求绝对值", 1));
            //_formulas.Add(new FormulaDefine("Sin", "Sin( )", "求正弦值", 1));
            //_formulas.Add(new FormulaDefine("Cos", "Cos( )", "求余弦值", 1));
            //_formulas.Add(new FormulaDefine("Tan", "Tan( )", "求正切值", 1));
            //_formulas.Add(new FormulaDefine("Cot", "Cot( )", "求余切值", 1));
            //_formulas.Add(new FormulaDefine("Exp", "Exp( )", "e的指定次幂", 1));
            //_formulas.Add(new FormulaDefine("Rem", "Rem( , )", "两数相除之余", 2));
            //_formulas.Add(new FormulaDefine("Log", "Log( , )", "求指定底数的对数", 1));
            //_formulas.Add(new FormulaDefine("Log10", "Log10( )", "求10为底的对数", 1));
            //_formulas.Add(new FormulaDefine("Pow", "Pow( , )", "求指定数字的指定次幂", 2));
            //_formulas.Add(new FormulaDefine("Rnd", "Rnd( , )", "将小数舍人到指定精度", 2));
            //_formulas.Add(new FormulaDefine("Sqrt", "Sqrt( )", "求平方根", 1));
            //_formulas.Add(new FormulaDefine("Dif", "Dif( , , )", "求导数，第一个参数为dy，第二个参数为dx，第三个参数为总间隔点数", 3));
            //_formulas.Add(new FormulaDefine("Int", "Int( , , )", "求积分，第一个参数为被积数，第二个参数为dy，第三个参数为dx", 3));
            //_formulas.Add(new FormulaDefine("Min", "Min( , )", "求两数中小值", 2));
            //_formulas.Add(new FormulaDefine("Max", "Max( , )", "求两数中的大值", 2));
            //_formulas.Add(new FormulaDefine("Avg", "Avg( , )", "求两数的平均值", 2));
            //_formulas.Add(new FormulaDefine("Tsl", "Tsl( )", "获得脉冲型传感器的两次下沿时间差", 1));
            //_formulas.Add(new FormulaDefine("Tsl2", "Tsl2( , )", "获得两个脉冲型传感器的同次下沿时间差", 2));
            //_formulas.Add(new FormulaDefine("Tad", "Tad( )", "获得同一列连续两行数据之和", 1));
            //_formulas.Add(new FormulaDefine("HR", "HR( )", "获得心跳数据", 1));
            //_formulas.Add(new FormulaDefine("Smth", "Smth( )", "平滑算法", 1));
            _formulas.Add(new FormulaDefine("Time", "Time( )", _resourceManager.GetString("Time"), 1));
            _formulas.Add(new FormulaDefine("Index", "Index( )", _resourceManager.GetString("Index"), 1));
            _formulas.Add(new FormulaDefine("DelayIndex", "DelayIndex( )", _resourceManager.GetString("DelayIndex"), 1));
            _formulas.Add(new FormulaDefine("IndexData", "IndexData( , )", _resourceManager.GetString("IndexData"), 2));
            _formulas.Add(new FormulaDefine("DelayIndexData", "DelayIndexData( , )", _resourceManager.GetString("DelayIndexData"), 2));
            _formulas.Add(new FormulaDefine("Get", "Get( , )", _resourceManager.GetString("Get"), 2));
            _formulas.Add(new FormulaDefine("TM", "TM( )", _resourceManager.GetString("TM"), 1));
            _formulas.Add(new FormulaDefine("Abs", "Abs( )", _resourceManager.GetString("Abs"), 1));
            _formulas.Add(new FormulaDefine("Sin", "Sin( )", _resourceManager.GetString("Sin"), 1));
            _formulas.Add(new FormulaDefine("Cos", "Cos( )", _resourceManager.GetString("Cos"), 1));
            _formulas.Add(new FormulaDefine("Tan", "Tan( )", _resourceManager.GetString("Tan"), 1));
            _formulas.Add(new FormulaDefine("Cot", "Cot( )", _resourceManager.GetString("Cot"), 1));
            _formulas.Add(new FormulaDefine("Exp", "Exp( )", _resourceManager.GetString("Exp"), 1));
            _formulas.Add(new FormulaDefine("Rem", "Rem( , )", _resourceManager.GetString("Rem"), 2));
            _formulas.Add(new FormulaDefine("Log", "Log( , )", _resourceManager.GetString("Log"), 1));
            _formulas.Add(new FormulaDefine("Log10", "Log10( )", _resourceManager.GetString("Log10"), 1));
            _formulas.Add(new FormulaDefine("Pow", "Pow( , )", _resourceManager.GetString("Pow"), 2));
            _formulas.Add(new FormulaDefine("Rnd", "Rnd( , )", _resourceManager.GetString("Rnd"), 2));
            _formulas.Add(new FormulaDefine("Sqrt", "Sqrt( )", _resourceManager.GetString("Sqrt"), 1));
            _formulas.Add(new FormulaDefine("Dif", "Dif( , , )", _resourceManager.GetString("Dif"), 3));
            _formulas.Add(new FormulaDefine("Int", "Int( , , )", _resourceManager.GetString("Int"), 3));
            _formulas.Add(new FormulaDefine("Min", "Min( , )", _resourceManager.GetString("Min"), 2));
            _formulas.Add(new FormulaDefine("Max", "Max( , )", _resourceManager.GetString("Max"), 2));
            _formulas.Add(new FormulaDefine("Avg", "Avg( , )", _resourceManager.GetString("Avg"), 2));
            _formulas.Add(new FormulaDefine("Tsl", "Tsl( )", _resourceManager.GetString("Tsl"), 1));
            _formulas.Add(new FormulaDefine("Tsl2", "Tsl2( , )", _resourceManager.GetString("Tsl2"), 2));
            _formulas.Add(new FormulaDefine("Tad", "Tad( )", _resourceManager.GetString("Tad"), 1));
            _formulas.Add(new FormulaDefine("HR", "HR( )", _resourceManager.GetString("HR"), 1));
            _formulas.Add(new FormulaDefine("Smth", "Smth( )", _resourceManager.GetString("Smth"), 1));

        }


    }
}
