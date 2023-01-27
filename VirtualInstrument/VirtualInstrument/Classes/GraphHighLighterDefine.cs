using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class GraphHighLighterDefine
    {
        public GraphHighLighterDefine()
        {
            _name = "HighLighter";
            _color = PublicMethods.GetNewHighLighterColor();
        }
        public GraphHighLighterDefine(string name, System.Drawing.Color color)
        {
            _color = System.Drawing.Color.FromArgb( 50,color.R,color.G,color.B );
            _name = name;
        }
        public GraphHighLighterDefine(string value)
        {
            Parse(value);
        }
        #region Props
        string _name;
        System.Drawing.Color _color;
        ZedGraph.LineItem _lineItem;
        public string Name
        {
            get { return _name; }
        }
        public System.Drawing.Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public ZedGraph.LineItem LineItem
        {
            get { return _lineItem; }
            set { _lineItem = value; }
        }

        #endregion

        #region Methods
        public ZedGraph.LineItem CreateLineItem(string sectionName)
        {
            ZedGraph.LineItem lineItem = new ZedGraph.LineItem("HighLight");
            lineItem.Color = _color;
            lineItem.IsY2Axis = false;
            lineItem.Label.IsVisible = false;
            lineItem.Symbol.IsVisible = false;
            lineItem.Line.Color = _color;
            lineItem.Line.IsAntiAlias = true;
            lineItem.Line.Width = Graph.HIGHLIGHTER_LINE_WIDTH;
            lineItem.Line.IsOptimizedDraw = true;
           //lineItem .Line .
            //lineItem.Line.GradientFill = new ZedGraph.Fill(System.Drawing.Color.Black, _color);
            lineItem.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.HighLight, sectionName, _name, this.ToString());
            
            _lineItem = lineItem;
            return _lineItem;
        }
        public void AddPoint(double x, double y)
        {
            if (_lineItem != null)
            {
                _lineItem.AddPoint(x, y);
            }
        }

        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name", _name);
            keyValue.Add("Color", KeyValue.KeyValue.ColorToString(_color));
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _name = keyValue.GetValueByKey("Name");
            _color = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("Color"));
        }
        #endregion
    }
}
