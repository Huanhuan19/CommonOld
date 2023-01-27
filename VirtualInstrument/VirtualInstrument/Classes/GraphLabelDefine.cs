using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class GraphLabelDefine
    {
        public GraphLabelDefine()
        {
            LoadDefault();
        }
        public GraphLabelDefine(string labelName, string labelCaption, string labelText, double x, double y, double z)
        {
            Initialize(labelName, labelCaption, labelText, x, y, z);
        }
        public GraphLabelDefine(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        string _labelText;
        GraphPointDefine _position;
        string _labelName;
        string _labelCaption;
        ZedGraph.TextObj _label;
        public string LabelText
        {
            get { return _labelText; }
            set { _labelText = value; }
        }
        public GraphPointDefine Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public string LabelName
        {
            get { return _labelName; }
            set { _labelName = value; }
        }
        public string LabelCaption
        {
            get { return _labelCaption; }
            set { _labelCaption = value; }
        }
        public ZedGraph.TextObj Label
        {
            get { return _label; }
            set { _label = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _labelCaption = "";
            _labelName = "";
            _labelText = "";
            _position = new GraphPointDefine();
        }
        void Initialize(string labelName, string labelCaption, string labelText, double x, double y, double z)
        {
            _labelName = labelName;
            _labelCaption = labelCaption;
            _labelText = labelText;
            _position = new GraphPointDefine(x, y, z);
        }
        public ZedGraph.TextObj CreateLabel(string sectionName)
        {
            ZedGraph.TextObj label = new ZedGraph.TextObj(LabelText, Position.X, Position.Y, ZedGraph.CoordType.AxisXYScale, ZedGraph.AlignH.Left, ZedGraph.AlignV.Center);
            label.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.Label, sectionName, LabelName, this.ToString());
            label.Location = new ZedGraph.Location(_position.X, _position.Y,ZedGraph.CoordType.AxisXYScale);
            label.ZOrder = ZedGraph.ZOrder.A_InFront;
            _label = label;
            return label;
        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("LabelText", _labelText);
            keyValue.Add("Position", _position.ToString());
            keyValue.Add("LabelName", _labelName);
            keyValue.Add("LabelCaption", _labelCaption);
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _labelText = keyValue.GetValueByKey("LabelText");
            _labelName = keyValue.GetValueByKey("LabelName");
            _labelCaption = keyValue.GetValueByKey("LabelCaption");
            _position = new GraphPointDefine(keyValue.GetValueByKey("Position"));

        }
        #endregion
    }
}
