using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class PresetExpr
    {
        public PresetExpr()//无参构造；
        {
            LoadDefault();
        }
        public PresetExpr(string name, string caption, string description, string methodName)//含有四个参数的构造函数；
        {
            LoadDefault();
            Initialize(name, caption, description, methodName);
        }
        public PresetExpr(string value)//含有一个参数的构造函数；是连接的结果；
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        string _name,_caption,_description,_methodName;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _caption = "";
            _description = "";
            _methodName = "";
        }
        public void Initialize( string name,string caption,string description,string methodName )//传递四个参数；
        {
            _name = name;
            _caption = caption;
            _description = description;
            _methodName = methodName;
        }

        #region Preset Expressions
        /// <summary>
        /// 根据位移差值和时间差值算速度
        /// </summary>
        /// <param name="s0"></param>
        /// <param name="s1"></param>
        /// <param name="t0"></param>
        /// <param name="t1"></param>
        /// <param name="dt"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        bool  VbyS(double s0, double s1, double t0, double t1,ref double dt,ref double v)
        {
            bool success = false;
            if (t1 - t0 > 0)
            {
                dt = t1 - t0;
                v = (s1 - s0) / dt;
                success = true;
            }
            return success;
        }
        /// <summary>
        /// 用光电门测量瞬时速度
        /// </summary>
        /// <param name="width"></param>
        /// <param name="dt"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        bool VbyGate(double width, double dt, ref double v)
        {
            bool success = false;
            if (dt > 0)
            {
                v = width / dt;
                success = true;
            }
            return success;
        }
        /// <summary>
        /// 从位移队列求出速度队列和加速度队列
        /// </summary>
        /// <param name="sList"></param>
        /// <param name="frequency"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        bool V_AbySList(double[] sList, double frequency, ref double[] v, ref double[] a)
        {
            bool success = false;
            if (frequency > 0 && sList.Length > 0)
            {
                double dt = 1 / frequency;
                v = new double[sList.Length];
                a = new double[sList.Length];
                for (int i = 0; i < sList.Length; i++)
                {
                    if (i == 0)
                    {
                        v[i] = sList[i] / dt;
                        a[i] = v[i] / dt;
                    }
                    else
                    {
                        v[i] = (sList[i] - sList[i - 1]) / dt;
                        a[i] = (v[i] - v[i - 1]) / dt;
                    }
                }
                success = true;
            }
            return success;
        }
        /// <summary>
        /// 牛顿第三定律，根据挡光片通过光电门的时间，计算速度、势能、动能，总能量
        /// </summary>
        /// <param name="width"></param>
        /// <param name="h"></param>
        /// <param name="m"></param>
        /// <param name="g"></param>
        /// <param name="dt"></param>
        /// <param name="v"></param>
        /// <param name="ep"></param>
        /// <param name="ea"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        bool V_Ep_Ea_EbyGate(double width, double h, double m, double g, double dt, ref double v, ref double ep, ref double ea, ref double e)
        {
            bool success = false;
            if (width > 0 && dt > 0)
            {
                v = width / dt;
                ep = m * g * h;
                ea = m * v * v;
                e = ep + ea;
                success = true;
            }
            return success;
        }
        /// <summary>
        /// 用光电门测动量
        /// </summary>
        /// <param name="width"></param>
        /// <param name="dt"></param>
        /// <param name="m"></param>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        bool PbyGate(double width, double dt, double m, ref double p,ref double v)
        {
            bool success = false;
            if (dt > 0)
            {
                v = width / dt;
                p = m * v;
                success = true;
            }
            return success;
        }
        /// <summary>
        /// 根据列表和采样频率积分
        /// </summary>
        /// <param name="list"></param>
        /// <param name="frequency"></param>
        /// <param name="integral"></param>
        /// <returns></returns>
        bool IntegralbyList(double[] list, double frequency, ref double integral)
        {
            bool success = false;
            if (frequency > 0)
            {
                double dt = 1/frequency;
                integral = list.Sum() * dt;
                success = true;
            }
            return success;
        }
        /// <summary>
        /// 根据电压和电流计算电阻
        /// </summary>
        /// <param name="u"></param>
        /// <param name="i"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        bool  RbyU_I(double u, double i, ref double r)
        {
            bool success = false;
            if (i != 0)
            {
                r = u / i;
                success = true;
            }
            return success;
        }
        /// <summary>
        /// 计算数列的平均值
        /// </summary>
        /// <param name="list"></param>
        /// <param name="avg"></param>
        /// <returns></returns>
        bool AvgByList(double[] list, ref double avg)
        {
            bool success = false;
            if (list.Length > 0)
            {
                avg = list.Average();
                success = true;
            }
            return success;
        }
        #endregion

        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("Description", _description);
            keyValue.Add("MethodName", _methodName);
            return keyValue.ToString();
        }
        public void Parse(string value)//传递值；
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            _description = keyValue.GetValueByKey("Description");
            _methodName = keyValue.GetValueByKey("MethodName");
        }
        #endregion

    }
}
