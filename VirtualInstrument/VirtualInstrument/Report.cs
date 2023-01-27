using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualInstrument
{
    public partial class Report : UserControl
    {
        public Report()
        {
            InitializeComponent();
            LoadDefault();
        }

        #region Props
        string _filename;
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        public bool ReadOnly
        {
            get { return richTextBox1.ReadOnly; }
            set { richTextBox1.ReadOnly = value; }
        }
        public float ZoomFactor
        {
            get { return richTextBox1.ZoomFactor; }
            set { richTextBox1.ZoomFactor = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _filename = "";
            ReadOnly = false;
        }
        void Initalize(string filename,bool readOnly)
        {
            _filename = filename;
            ReadOnly = readOnly;
        }
        public void LoadFile(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                _filename = filename;
                richTextBox1.LoadFile(filename);
            }
        }
        public void SaveFile(string filename)
        {
            _filename = filename;
            richTextBox1.SaveFile(filename);
        }
        public void Cut()
        {
            richTextBox1.Cut();
        }
        public void Paste()
        {
            richTextBox1.Paste();

        }
        public void Copy()
        {
            richTextBox1.Copy();
        }
        public void Clear()
        {
            richTextBox1.Clear();
        }
        public void Undo()
        {
            richTextBox1.Undo();
        }
        public void Redo()
        {
            richTextBox1.Redo();
        }
        public void SelectAll()
        {
            richTextBox1.SelectAll();
        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Filename", _filename);
            keyValue.Add("ReadOnly", ReadOnly.ToString());
            keyValue.Add("ZoomFactor", ZoomFactor.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _filename = keyValue.GetValueByKey("Filename");
            bool readOnly;
            bool.TryParse(keyValue.GetValueByKey("ReadOnly"), out readOnly);
            ReadOnly = readOnly;
            float zoomFactor;
            float.TryParse(keyValue.GetValueByKey("ZoomFactor"), out zoomFactor);
            ZoomFactor = zoomFactor;
            LoadFile(_filename);
        }
        #endregion
    }
}
