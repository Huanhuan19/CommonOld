using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualInstrument.QuickView
{
    public partial class QuickView : UserControl
    {
        public QuickView()
        {
            InitializeComponent();
        }
        #region Props
        bool _editMode = false;
        public bool EditMode
        {
            get
            {
                return _editMode;
            }
            set
            {
                _editMode = value;
                foreach (QuickText quickText in flowLayoutPanel1.Controls)
                {
                    quickText.EditMode = _editMode;
                }
            }
        }
        public List<string> VariableNames
        {
            get
            {
                List<string> list = new List<string>();
                foreach (QuickText quickText in flowLayoutPanel1.Controls)
                {
                    list.Add(quickText.VariableName);
                }
                return list;
            }
        }
        
        #endregion

        #region Methods
        public void Clear()
        {
            flowLayoutPanel1.Controls.Clear();
        }
        public void Add(string name, string caption, string unit,int decimalCount)
        {
            QuickText quickText = new QuickText();
            quickText.VariableName = name;
            quickText.Caption = caption;
            quickText.Unit = unit;
            quickText.Decimal = decimalCount;
            flowLayoutPanel1.Controls.Add(quickText);
            

        }
        public void SetValue(string name, double value)
        {
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                if (string.Equals(((QuickText)flowLayoutPanel1.Controls[i]).VariableName, name))
                {
                    ((QuickText)flowLayoutPanel1.Controls[i]).Value = value;
                }
            }
        }
        public void Remove(string name)
        {
            for (int i = flowLayoutPanel1.Controls.Count - 1; i >= 0; i--)
            {
                if (string.Equals(((QuickText)flowLayoutPanel1.Controls[i]).VariableName, name))
                {
                    flowLayoutPanel1.Controls.RemoveAt(i);
                }
            }
        }
        public bool Contains(string name)
        {
            bool contains = false;
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                if (string.Equals(((QuickText)flowLayoutPanel1.Controls[i]).VariableName, name))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        #endregion

        #region Serialize
        string List2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                keyValue.Add(i.ToString(), ((QuickText)flowLayoutPanel1.Controls[i]).ToString());
            }
            return keyValue.ToString();
        }
        void Str2List(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                QuickText quickText = new QuickText();
                quickText.Parse(keyValue.GetValueByKey(i.ToString()));
                flowLayoutPanel1.Controls.Add(quickText);
            }
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Controls", List2Str());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Str2List(keyValue.GetValueByKey("Controls"));
        }
        #endregion
    }
}
