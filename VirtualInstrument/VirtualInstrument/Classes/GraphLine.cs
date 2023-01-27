using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.Classes
{
    public class GraphLine
    {
        public GraphLine()
        {
            LoadDefault();
        }
        public GraphLine(string sectionName,string columnName, string columnCaption, int decimalCount, Color lineColor, bool pointVisible, bool lineVisible, float lineWidthF,bool isY2Axis,bool isSmooth,float smoothTention, int count)
        {
            Initialize( sectionName,columnName,columnCaption,decimalCount,lineColor,pointVisible,lineVisible,lineWidthF ,isY2Axis,isSmooth,smoothTention,count);
        }
        public GraphLine(string value)
        {
            LoadDefault();
            Parse(value);
        }
        public static int DEFAULT_POINTS_COUNT = 1000;
        #region Props
        GraphLineDefine _lineDefine;
        List<GraphPointDefine> _points;
        List<int> _indexes;
        string _sectionName;
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        public GraphLineDefine LineDefine
        {
            get { return _lineDefine; }
            set { _lineDefine = value; }
        }
        public List<GraphPointDefine> Points
        {
            get { return _points; }
        }
        public List<int> Indexes
        {
            get { return _indexes; }
        }
        public int Count
        {
            get { return _points.Count; }
        }
        public double StartX
        {
            get
            {
                double value = 0;
                for (int i = 0; i < _points.Count; i++)
                {
                    if (i == 0)
                    {
                        value = _points[i].X;
                    }
                    else
                    {
                        value = Math.Min(_points[i].X, value);
                    }
                }
                return value;
            }
        }
        public double EndX
        {
            get
            {
                double value = 0;
                for (int i = 0; i < _points.Count; i++)
                {
                    if (i == 0)
                    {
                        value = _points[i].X;
                    }
                    else
                    {
                        value = Math.Max(_points[i].X, value);
                    }
                }
                return value;
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _lineDefine = new GraphLineDefine();
             _points = new List<GraphPointDefine>(DEFAULT_POINTS_COUNT);
             _indexes = new List<int>(DEFAULT_POINTS_COUNT);
        }
        void Initialize(string sectionName,string columnName, string columnCaption, int decimalCount, Color lineColor, bool pointVisible, bool lineVisible, float lineWidthF,bool isY2Axis,bool isSmooth,float smoothTention, int count)
        {
            _sectionName = sectionName;
            _lineDefine = new GraphLineDefine(columnName, columnCaption, decimalCount, lineColor, pointVisible, lineVisible, lineWidthF,isY2Axis,isSmooth,smoothTention);
            _points = new List<GraphPointDefine>(count);
            _indexes = new List<int>(count);
        }
        public void Clear()
        {
            _points.Clear();
            _indexes.Clear();
        }
        /// <summary>
        /// 原有数据不清除
        /// </summary>
        /// <param name="count"></param>
        public void SetCapacity(int count)
        {
            _points.Capacity = count;
            _indexes.Capacity = count;
        }
        /// <summary>
        /// 返回Points.Count
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public int Add(GraphPointDefine point)
        {
            _points.Add(point);
            return _points.Count;
        }
        public int Add(int index)
        {
            _indexes.Add(index);
            return _indexes.Count;
        }
        public int Add(GraphPointDefine point, int index)
        {
            Add(point);
            return Add(index);
        }
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < _points.Count)
            {
                _points.RemoveAt(index);
            }
            if (index >= 0 && index < _indexes.Count)
            {
                _indexes.RemoveAt(index);
            }
        }
        #endregion

        #region Serialize
        string Points2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _points.Count; i++)
            {
                keyValue.Add(i.ToString(), _points[i].ToString());
            }
            return keyValue.ToString();
        }
        void Str2Points(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _points.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _points.Add(new GraphPointDefine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        string Indexes2Str()
        {
            string[] strList = new string[_indexes.Count];
            for (int i = 0; i < _indexes.Count; i++)
            {
                strList[i] = _indexes[i].ToString();
            }
            return string.Join("|", strList);

        }
        void Str2Indexes(string value)
        {
            string[] strList = value.Split('|');
            _indexes.Clear();
            for (int i = 0; i < strList.Length; i++)
            {
                int index;
                int.TryParse(strList[i], out index);
                _indexes.Add(index);
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("SectionName", _sectionName);
            keyValue.Add("LineDefine", _lineDefine.ToString());
            keyValue.Add("Points", Points2Str());
            keyValue.Add("Indexes", Indexes2Str());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _sectionName = keyValue.GetValueByKey("SectionName");
            _lineDefine = new GraphLineDefine(keyValue.GetValueByKey("LineDefine"));
            Str2Points(keyValue.GetValueByKey("Points"));
            Str2Indexes(keyValue.GetValueByKey("Indexes"));
        }
        #endregion
    }
}
