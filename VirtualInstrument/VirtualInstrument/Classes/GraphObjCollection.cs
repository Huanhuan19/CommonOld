using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.Classes
{
    public class GraphObjCollection
    {
        public GraphObjCollection()
        {
            LoadDefault();
        }
        public GraphObjCollection(string name, string caption, List<GraphLineDefine> lines, List<GraphLabelDefine> labels, List<StandardCurveDefine> standardCurves,List<GraphFitLineDefine> fitLines,List<StandardCurveDefine> fitLineExtands,List<GraphHighLighterDefine> highLines,List<GraphManualLine> manualLines, StandardCurveGetValue getValue)
        {
            LoadDefault();
            Initialize(name, caption, lines,labels,standardCurves,fitLines,fitLineExtands,highLines,manualLines, getValue);

        }
        public GraphObjCollection(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        //public static int OBJTYPE_LINE = 1;
        //public static int OBJTYPE_LABEL = 2;
        //public static int OBJTYPE_STANDARDCURVE = 3;
        //public static int OBJTYPE_FITLINE = 4;
        //public static int OBJTYPE_FITEXTANDLINE = 5;

        string _name;
        string _caption;
        List<GraphLineDefine> _graphLines = new List<GraphLineDefine>();
        List<GraphLabelDefine> _graphLabels = new List<GraphLabelDefine>();
        List<StandardCurveDefine> _standardCurves = new List<StandardCurveDefine>();
        List<StandardCurveDefine> _fitLineExtands = new List<StandardCurveDefine>();
        List<GraphFitLineDefine> _fitLines = new List<GraphFitLineDefine>();
        List<GraphHighLighterDefine> _highLighters = new List<GraphHighLighterDefine>();
        List<GraphManualLine> _manualLines = new List<GraphManualLine>();
        StandardCurveGetValue _getValue = null;
        bool _visible = true;
        public StandardCurveGetValue GetValueMethod
        {
            get { return _getValue; }
            set 
            { 
                _getValue = value;
                for (int i = 0; i < _fitLineExtands.Count; i++)
                {
                    _fitLineExtands[i].GetValueMethod = value;
                }
                for (int i = 0; i < _standardCurves.Count; i++)
                {
                    _standardCurves[i].GetValueMethod = value;
                }
            }
                
        }
        public bool Visible
        {
            get { return _visible; }
            set 
            { 
                _visible = value;
            }
        }
        public bool SyncVisible
        {
            set
            {
                for (int i = 0; i < _graphLabels.Count; i++)
                {
                    if (_graphLabels[i].Label != null)
                    {
                        _graphLabels[i].Label.IsVisible = value;
                    }
                }
                for (int i = 0; i < _graphLines.Count; i++)
                {
                    if (_graphLines[i].LineItem != null)
                    {
                        _graphLines[i].LineItem.IsVisible = value;
                    }
                }
                for (int i = 0; i < _fitLines.Count; i++)
                {
                    if (_fitLines[i].LineItem != null)
                    {
                        _fitLines[i].LineItem.IsVisible = value;
                    }
                }
                for (int i = 0; i < _fitLineExtands.Count; i++)
                {
                    if (_fitLineExtands[i].LineItem != null)
                    {
                        _fitLineExtands[i].LineItem.IsVisible = value;
                    }
                }
                for (int i = 0; i < this._standardCurves.Count; i++)
                {
                    if (_standardCurves[i].LineItem != null)
                    {
                        _standardCurves[i].LineItem.IsVisible = value;
                    }
                }
                for (int i = 0; i < this._highLighters.Count; i++)
                {
                    if (_highLighters[i].LineItem != null)
                    {
                        _highLighters[i].LineItem.IsVisible = value;
                    }
                }
                for (int i = 0; i < this._manualLines.Count; i++)
                {
                    if (_manualLines[i].LineItem != null)
                    {
                        _manualLines[i].LineItem.IsVisible = value;
                    }
                }

            }
        }
        
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
        public List<GraphLineDefine> GraphLines
        {
            get { return _graphLines; }
        }
        public List<GraphLabelDefine> GraphLabels
        {
            get { return _graphLabels; }
        }
        public List<StandardCurveDefine> StandardCurves
        {
            get { return _standardCurves; }
        }
        public List<StandardCurveDefine> FitLineExtands
        {
            get { return _fitLineExtands; }
        }
        public List<GraphFitLineDefine> FitLines
        {
            get { return _fitLines; }
        }
        public List<GraphHighLighterDefine> HighLighters
        {
            get { return _highLighters; }
        }
        public List<GraphManualLine> ManualLines
        {
            get { return _manualLines; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _caption = "";
            _graphLines.Clear();
            _graphLabels.Clear();
            _standardCurves.Clear();
            _fitLines.Clear();
            _fitLineExtands.Clear();
            _highLighters.Clear();
            _manualLines.Clear();
            _getValue = null;
        }
        public void Initialize(string name, string caption, List<GraphLineDefine> lines, List<GraphLabelDefine> labels, List<StandardCurveDefine> stanardCurves, List<GraphFitLineDefine> fitLines, List<StandardCurveDefine> fitLineExtands,List<GraphHighLighterDefine> highLines, List<GraphManualLine> manualLines, StandardCurveGetValue getValue)
        {
            _name = name;
            _caption = caption;
            _graphLines.Clear();
            _graphLines.AddRange(lines);
            _graphLabels.Clear();
            _graphLabels.AddRange(labels);
            _standardCurves.Clear();
            _standardCurves.AddRange(stanardCurves);
            _fitLineExtands.Clear();
            _fitLineExtands.AddRange(fitLineExtands);
            _fitLines.Clear();
            _fitLines.AddRange(fitLines);
            _highLighters.Clear();
            _highLighters.AddRange(highLines);
            _manualLines.Clear();
            _manualLines.AddRange(manualLines);
            _getValue = getValue;

        }
        public GraphFitLineDefine AddFitLine(double start, double end, string caption, string methodName, double a, double b, double c, double d, bool isY2Axis)
        {
            GraphFitLineDefine line = new GraphFitLineDefine(_getValue, start, end, new StandardCurveDefine(GetNewFitLineName(), caption, methodName, a, b, c, d, isY2Axis, _getValue));

            _fitLines.Add( line);
            return line;
        }
        public GraphFitLineDefine AddFitLine(string caption, string methodName,List<GraphPointDefine> points, bool isY2Axis)
        {
            GraphFitLineDefine line = new GraphFitLineDefine(points, new StandardCurveDefine(GetNewFitLineName(), caption, methodName, 0,0,0,0, isY2Axis, _getValue));

            _fitLines.Add(line);
            return line;
        }
        public StandardCurveDefine AddFitLineExtand(string caption, string methodName, double a, double b, double c, double d, bool isY2Axis)
        {
            StandardCurveDefine line = new StandardCurveDefine(GetNewFitLineExtandName(), caption, methodName, a, b, c, d, isY2Axis, _getValue);
            _fitLineExtands.Add(line);
            return line;
        }
        public StandardCurveDefine AddStandardCurve(string caption, string methodName, double a, double b, double c, double d,bool isY2Axis)
        {
            StandardCurveDefine line = new StandardCurveDefine(GetNewStandardCurveName(), caption, methodName, a, b, c, d, isY2Axis, _getValue);
            _standardCurves.Add(line );
            return line;
        }
        public GraphLabelDefine AddLabel(string content)
        {
            GraphLabelDefine label = new GraphLabelDefine(GetNewLabelName(), "Label", content, 0, 0, 0);
            _graphLabels.Add(label);
            return label;
        }
        public GraphLabelDefine AddLabel(string caption, string text, double x, double y, double z)
        {
            GraphLabelDefine label = new GraphLabelDefine(GetNewLabelName(), caption, text, x, y, z);
            _graphLabels.Add(label);
            return label;
        }
        public GraphHighLighterDefine AddHighLighter(Color color)
        {
            GraphHighLighterDefine highLighter = new GraphHighLighterDefine(GetNewHighLighterName(),color);
            _highLighters.Add(highLighter);
            return highLighter;
        }
        public GraphManualLine AddManualLine(string xCaption, string yCaption, Color color)
        {
            GraphManualLine line = new GraphManualLine(GetNewManualLineName() , xCaption,yCaption,color );
            _manualLines.Add( line );
            return line;
        }
        public GraphManualLine AddLine_V(string xCaption, string yCaption, Color color)
        {
            GraphManualLine line = new GraphManualLine(GetNewManualLineName(), xCaption, yCaption, color);
            _manualLines.Add(line);
            return line;
        }
        public GraphLineDefine AddLine(string columnName, string caption, int decimalCount, Color lineColor, bool pointVisible, bool lineVisible, float lineWidth, bool isY2Axis, bool isSmooth, float smoothTention)
        {
            return AddLine(columnName, caption, decimalCount, lineColor, pointVisible, lineVisible, lineWidth, isY2Axis, isSmooth,smoothTention, false);
        }
        public GraphLineDefine AddLine(string columnName, string caption, int decimalCount, Color lineColor, bool pointVisible, bool lineVisible, float lineWidth, bool isY2Axis, bool isSmooth, float smoothTention, bool draw3DPoint)
        {
            GraphLineDefine line = new GraphLineDefine(columnName, caption, decimalCount, lineColor, pointVisible, lineVisible, lineWidth, isY2Axis, isSmooth, smoothTention,draw3DPoint);
            _graphLines.Add(line);
            return line;
        }
        public ZedGraph.LineItem GetLineItemByName(string columnName)
        {
            ZedGraph.LineItem lineItem = null;
            for (int i = 0; i < _graphLines.Count; i++)
            {
                if (string.Equals(_graphLines[i].ColumnDefine.ColumnName, columnName))
                {
                    lineItem = _graphLines[i].LineItem;
                }
            }
            return lineItem;
        }
        public Classes.GraphLineDefine GetLineDefineByName(string columnName)
        {
            Classes.GraphLineDefine line = null;
            for (int i = 0; i < _graphLines.Count; i++)
            {
                if (string.Equals(_graphLines[i].ColumnDefine.ColumnName, columnName))
                {
                    line = _graphLines[i];
                    break;
                }
            }
            return line;

        }
        string GetNewManualLineName()
        {
            string seed = "ManualLine_";
            int index = 0;
            string name = seed + index.ToString();
            while (ManualLineContains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;
        }
        bool ManualLineContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _manualLines.Count; i++)
            {
                if (string.Equals(_manualLines[i].Name, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        string GetNewHighLighterName()
        {
            string seed = "HighLighter_";
            int index = 0;
            string name = seed + index.ToString();
            while (HighLighterContains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;
        }
        bool HighLighterContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _highLighters.Count; i++)
            {
                if (string.Equals(_highLighters[i].Name, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        string GetNewFitLineExtandName()
        {
            string seed = "FitLineExtand_";
            int index = 0;
            string name = seed + index.ToString();
            while (FitLineExtandsContains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;
        }
        public bool FitLineExtandsContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _fitLineExtands.Count; i++)
            {
                if (string.Equals(_fitLineExtands[i].Name, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        string GetNewFitLineName()
        {
            string seed = "FitLine_";
            int index = 0;
            string name = seed + index.ToString();
            while (FitLinesContains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;
        }
        public bool FitLinesContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _fitLines.Count; i++)
            {
                if (string.Equals(_fitLines[i].CurveDefine.Name, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        string GetNewStandardCurveName()
        {
            string seed = "StandardCurve_";
            int index = 0;
            string name = seed + index.ToString();
            while (StandardCurvesContains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;
        }
        public bool StandardCurvesContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _standardCurves.Count; i++)
            {
                if (string.Equals(_standardCurves[i].Name, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        string GetNewLabelName()
        {
            string seed = "Label_";
            int index = 0;
            string name = seed + index.ToString();
            while (LabelContains(name))
            {
                index++;
                name = seed + index.ToString();
            }
            return name;
        }
        public bool LabelContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _graphLabels.Count; i++)
            {
                if (string.Equals(_graphLabels[i].LabelName, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        public bool LineContains(string name)
        {
            bool contains = false;
            for (int i = 0; i < _graphLines.Count; i++)
            {
                if (string.Equals(_graphLines[i].ColumnDefine.ColumnName, name))
                {
                    contains = true;
                    break;
                }

            }
            return contains;
        }
        public void ClearPoint()
        {
            for (int i = 0; i < _graphLines.Count; i++)
            {
                if( _graphLines[i].LineItem!= null )
                {
                    _graphLines[i].LineItem.Clear();
                }
            }
        }
        #region PackObj Methods
        public static string Pack(Classes.GraphObjType objType, string sectionName, string objName, string info)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("ObjType", ((int)objType).ToString());
            keyValue.Add("SectionName", sectionName);
            keyValue.Add("ObjName", objName);
            keyValue.Add("Value", info);
            return keyValue.ToString();
        }
        public static Classes.GraphObjType GetObjTypeFromPack(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            int type;
            int.TryParse(keyValue.GetValueByKey("ObjType"), out type);
            return (Classes.GraphObjType)type;
        }
        public static string GetSectionNameFromPack(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            return keyValue.GetValueByKey("SectionName");
        }
        public static string GetObjNameFromPack(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            return keyValue.GetValueByKey("ObjName");
        }
        public static string GetInfoFromPack(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            return keyValue.GetValueByKey("Value");
        }
        #endregion


        #endregion

        #region Serialize

        string ManualLines2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < this._manualLines.Count; i++)
            {
                keyValue.Add(i.ToString(), _manualLines[i].ToString());
            }
            return keyValue.ToString();
        }
        void ManualLinesParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _manualLines.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _manualLines.Add(new GraphManualLine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        string HighLighters2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < this._highLighters.Count; i++)
            {
                keyValue.Add(i.ToString(), _highLighters[i].ToString());
            }
            return keyValue.ToString();
        }
        void HighLightersParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _highLighters.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _highLighters.Add(new GraphHighLighterDefine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        string FitLines2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _fitLines.Count; i++)
            {
                keyValue.Add(i.ToString(), _fitLines[i].ToString());
            }
            return keyValue.ToString();
        }
        void FitLinesParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _fitLines.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _fitLines.Add(new GraphFitLineDefine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        string FitLineExtands2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _fitLineExtands.Count; i++)
            {
                keyValue.Add(i.ToString(), _fitLineExtands[i].ToString());
            }
            return keyValue.ToString();
        }
        void FitLineExtandsParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _fitLineExtands.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _fitLineExtands.Add(new StandardCurveDefine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        string Lines2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _graphLines.Count; i++)
            {
                keyValue.Add(i.ToString(), _graphLines[i].ToString());
            }
            return keyValue.ToString();
        }
        void LinesParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _graphLines.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _graphLines.Add(new GraphLineDefine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        string Labels2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _graphLabels.Count; i++)
            {
                keyValue.Add(i.ToString(), _graphLabels[i].ToString());
            }
            return keyValue.ToString();
        }
        void LabelsParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _graphLabels.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _graphLabels.Add(new GraphLabelDefine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        string StandardCurves2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < this._standardCurves.Count; i++)
            {
                keyValue.Add(i.ToString(), _standardCurves[i].ToString());
            }
            return keyValue.ToString();
        }
        void StandardCurvesParse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            this._standardCurves.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _standardCurves.Add(new StandardCurveDefine(keyValue.GetValueByKey(i.ToString()),_getValue));
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Caption", _caption);
            keyValue.Add("GraphLines", Lines2Str());
            keyValue.Add("GraphLabels", Labels2Str());
            keyValue.Add("StandardCurves", StandardCurves2Str());
            keyValue.Add("FitLines", FitLines2Str());
            keyValue.Add("FitLineExtands", FitLineExtands2Str());
            keyValue.Add("Visible", _visible.ToString());
            keyValue.Add("HighLighters", HighLighters2Str());
            keyValue.Add("ManualLines", ManualLines2Str());
            return keyValue.ToString();

        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _caption = keyValue.GetValueByKey("Caption");
            LinesParse(keyValue.GetValueByKey("GraphLines"));
            LabelsParse(keyValue.GetValueByKey("GraphLabels"));
            StandardCurvesParse(keyValue.GetValueByKey("StandardCurves"));
            FitLineExtandsParse(keyValue.GetValueByKey("FitLineExtands"));
            FitLinesParse(keyValue.GetValueByKey("FitLines"));
            HighLightersParse(keyValue.GetValueByKey("HighLighters"));
            ManualLinesParse(keyValue.GetValueByKey("ManualLines"));
            bool.TryParse(keyValue.GetValueByKey("Visible"), out _visible);
        }
        #endregion
    }
}
