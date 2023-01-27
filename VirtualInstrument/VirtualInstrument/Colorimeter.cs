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
    public partial class Colorimeter : UserControl
    {
        public Colorimeter()
        {
            InitializeComponent();
            timer1.Start();
            LoadDefault();
        }

        #region Props
        string _caption;
        double _maxValue, _minValue, _value;
        Color _basicColor;
        
        string _unit;

        public double MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }
        public double MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
        public double Value
        {
            get { return _value; }
            set { _value = value; _needRefreshValue = true; }
        }
        public Color BasicColor
        {
            get { return _basicColor; }
            set { _basicColor = value; }
        }
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        #endregion

        #region Varialbles
        bool _needRefreshAll = false, _needRefreshValue = false;
        #endregion

        #region Methods
        void DrawBackGround(Graphics g)
        {
        }
        void DrawColorBar( Graphics g )
        {
        }
        void DrawAll(Graphics g)
        {
            DrawBackGround(g );
            DrawColorBar(g );
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshAll)
            {
                _needRefreshAll = false;
                DrawAll(this.CreateGraphics());
            }
            if (_needRefreshValue)
            {
                _needRefreshValue = false;
                DrawColorBar(this.CreateGraphics());
            }
        }

        #endregion

        #region Serialize
        void LoadDefault()
        {
            _caption = "";
            _basicColor = Color.Empty;
            _maxValue = 0; 
            _minValue = 0; 
            _value = 0;
            _unit = "";

        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Caption", _caption);
            keyValue.Add("BasicColor", KeyValue.KeyValue.ColorToString(_basicColor));
            keyValue.Add("MaxValue", _maxValue.ToString());
            keyValue.Add("MinValue", _minValue.ToString());
            keyValue.Add("Unit", _unit);
            return keyValue.ToString();

        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _caption = keyValue.GetValueByKey("Caption");
            _basicColor = KeyValue.KeyValue.ParseColor(keyValue.GetValueByKey("BasicColor"));
            double.TryParse(keyValue.GetValueByKey("MaxValue"), out _maxValue);
            double.TryParse(keyValue.GetValueByKey("MinValue"), out _minValue);
            _unit = keyValue.GetValueByKey("Unit");
        }
        #endregion
    }
}
