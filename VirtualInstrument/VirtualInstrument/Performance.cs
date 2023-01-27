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
    public partial class Performance : UserControl
    {
        public Performance()
        {
            InitializeComponent();
            timer1.Start();
            LoadDefault();
            InitializeGraphics();
            Clear();
            this.Click += new EventHandler(Performance_Click);
            this.label_caption.Click += new EventHandler(label_caption_Click);
            this.uvMeter1.Click += new EventHandler(uvMeter1_Click);
            this.zedGraphControl1.Click += new EventHandler(zedGraphControl1_Click);
            this.label_unit.Click += new EventHandler(label_unit_Click);
            this.label_value.Click += new EventHandler(label_value_Click);
            this.DoubleClick += new EventHandler(Performance_DoubleClick);
            this.label_caption.DoubleClick += new EventHandler(label_caption_DoubleClick);
            this.uvMeter1.DoubleClick += new EventHandler(uvMeter1_DoubleClick);
            this.zedGraphControl1.DoubleClickEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(zedGraphControl1_DoubleClickEvent);
            this.label_unit.DoubleClick += new EventHandler(label_unit_DoubleClick);
            this.label_value.DoubleClick += new EventHandler(label_value_DoubleClick);
            this.label_scope.DoubleClick += new EventHandler(label_scope_DoubleClick);
        }

        #region Props
        string _name;
        Color _basicColor,_selectedColor;
        bool _selected;
        double _value;
            
        public string PerformanceName
        {
            get { return _name; }
            set { _name = value; }
        }
        public double MaxValue
        {
            get { return uvMeter1.MaxValue; }
            set 
            { 
                uvMeter1.MaxValue = (float)value;
                zedGraphControl1.GraphPane.YAxis.Scale.Max = value;
                
            }
        }
        public double MinValue
        {
            get { return uvMeter1.MinValue; }
            set 
            { 
                uvMeter1.MinValue =(float) value;
                zedGraphControl1.GraphPane.YAxis.Scale.Min = value;
                
            }
        }
        public double Value
        {
            get { return _value; }
            set { _value = Math.Round(value,4); _needRefreshValue = true; }
        }
        public Color BasicColor
        {
            get { return _basicColor; }
            set { _basicColor = value; }
        }
        public string PerformenceCaption
        {
            get { return this.label_caption.Text; }
            set { this.label_caption.Text = value; }
        }
        public string Unit
        {
            get { return this.label_unit.Text; }
            set { this.label_unit.Text = value; }
        }
        public string Scope
        {
            get { return label_scope.Text; }
            set { label_scope.Text = value; }
        }
        public Color SelectedColor
        {
            get { return _selectedColor; }
        }
        public bool Selected
        {
            get { return _selected; }
        }
        #endregion

        #region Variables
        bool _needRefreshAll = true, _needRefreshValue = false;
        static int GRAPHICS_POINT_COUNT = 200;
        #endregion

        #region Events
        public event Classes.ControlSelectedHandler ControlSelected = null;
        protected void OnControlSelected(Classes.ControlSelectedEventArgs e)
        {
            if (ControlSelected != null)
            {
                ControlSelected(this, e);
            }
        }
        public event Classes.ControlSelectedHandler ControlDoubleClicked = null;
        protected void OnControlDoubleClicked(Classes.ControlSelectedEventArgs e)
        {
            if (ControlDoubleClicked != null)
            {
                ControlDoubleClicked(this, e);
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _value = 0;
            _basicColor = Color.Empty;
            _selectedColor = System.Drawing.SystemColors.ActiveCaption;
            _selected = false;
        }
        public void Initialize(string name, string caption, double min, double max, Color basicColor, string unit)
        {
            _name = name;
            PerformenceCaption = caption;
            MinValue = min;
            MaxValue = max;
            Unit = unit;
            Scope= min + unit + " ~ "+max+unit;
            _basicColor = basicColor;
            _value = 0;
            _selected = false;
            _needRefreshAll = true;
        }
        void InitializeGraphics()
        {
            this.zedGraphControl1.GraphPane.TitleGap = 0.1f;

            this.zedGraphControl1.GraphPane.Fill = new  ZedGraph.Fill(Color.Black);
            this.zedGraphControl1.GraphPane.Chart.Fill = new  ZedGraph.Fill(Color.Black);
            this.zedGraphControl1.GraphPane.Title.IsVisible = false;
            this.zedGraphControl1.GraphPane.YAxis.IsVisible = true;
            this.zedGraphControl1.GraphPane.Y2Axis.IsVisible = false;
            this.zedGraphControl1.GraphPane.XAxis.IsVisible = true;
            this.zedGraphControl1.GraphPane.YAxis.Scale.IsVisible = false;
            this.zedGraphControl1.GraphPane.XAxis.Scale.IsVisible = false;

            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.DashOff = 0f;
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.PenWidth = 1f;
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = true;
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.Color = Color.FromArgb(100, Color.Green.R, Color.Green.G, Color.Green.B);
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.IsZeroLine = true;

            this.zedGraphControl1.GraphPane.YAxis.Title.IsVisible = false;
            this.zedGraphControl1.GraphPane.YAxis.Title.IsTitleAtCross = false;

            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.DashOff = 0f;
            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.PenWidth = 1f;
            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.IsVisible = false;
            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.Color = Color.FromArgb(50, 0, 0, 255);

            this.zedGraphControl1.GraphPane.YAxis.Scale.MaxAuto = false;
            this.zedGraphControl1.GraphPane.YAxis.Scale.MinAuto = false;


            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.IsVisible = true;

            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.DashOff = 0f;
            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.PenWidth = 1f;
            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.Color = Color.FromArgb(100, Color.Green.R, Color.Green.G, Color.Green.B);
            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.IsZeroLine = true;

            this.zedGraphControl1.GraphPane.XAxis.Title.IsVisible = false;
            this.zedGraphControl1.GraphPane.XAxis.Title.IsTitleAtCross = false;

            this.zedGraphControl1.GraphPane.XAxis.MinorGrid.DashOff = 0f;
            this.zedGraphControl1.GraphPane.XAxis.MinorGrid.PenWidth = 1f;
            this.zedGraphControl1.GraphPane.XAxis.MinorGrid.IsVisible = false;
            this.zedGraphControl1.GraphPane.XAxis.MinorGrid.Color = Color.FromArgb(50, 0, 255, 0);

            this.zedGraphControl1.GraphPane.XAxis.Scale.MaxAuto = false;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MinAuto = false;
            this.zedGraphControl1.GraphPane.XAxis.Scale.Min = 0;
            this.zedGraphControl1.GraphPane.XAxis.Scale.Max = GRAPHICS_POINT_COUNT;

            this.zedGraphControl1.GraphPane.Legend.IsVisible = false;
            this.zedGraphControl1.GraphPane.Legend.Gap = 0.1f;
            this.zedGraphControl1.GraphPane.Legend.Position = ZedGraph.LegendPos.TopCenter;
            this.zedGraphControl1.GraphPane.Legend.FontSpec.Size = 10f;

            this.zedGraphControl1.GraphPane.XAxis.Title.Gap = 0.1f;
            this.zedGraphControl1.GraphPane.YAxis.Title.Gap = 0.1f;
            this.zedGraphControl1.GraphPane.Y2Axis.Title.Gap = 0.1f;

            this.zedGraphControl1.GraphPane.XAxis.Title.FontSpec.Size = 20f;
            this.zedGraphControl1.GraphPane.YAxis.Title.FontSpec.Size = 20f;
            this.zedGraphControl1.GraphPane.Y2Axis.Title.FontSpec.Size = 20f;
            this.zedGraphControl1.GraphPane.Y2Axis.Title.IsTitleAtCross = false;

            this.zedGraphControl1.GraphPane.XAxis.AxisGap = 0.1f;
            this.zedGraphControl1.GraphPane.Y2Axis.AxisGap = 0.1f;
            this.zedGraphControl1.GraphPane.YAxis.AxisGap = 0.1f;

            this.zedGraphControl1.GraphPane.Y2Axis.Scale.MaxAuto = false;
            this.zedGraphControl1.GraphPane.Y2Axis.Scale.MinAuto = false;
        }

        void InitializeCurve()
        {
            Color lineColor = Color.Lime;
            this.zedGraphControl1.GraphPane.CurveList.Clear();
            ZedGraph.LineItem lineItem = new ZedGraph.LineItem("Sensor");
            lineItem.Color = lineColor;
            lineItem.Line.IsVisible = true;
            lineItem.Line.IsSmooth = true;
            lineItem.Line.SmoothTension = 1f;
            lineItem.Line.Width = 1f;
            //lineItem.Symbol = new Symbol(SymbolType.Circle, lineColor);
            lineItem.Symbol.IsVisible = false;
            //lineItem.Symbol.Size = global::BoxLab.Properties.Settings.Default.Graphics_Point_Size;
            //lineItem.Symbol.Fill = new Fill(lineColor);
            lineItem.Line.Fill = new ZedGraph.Fill(Color.FromArgb(128, lineColor.R, lineColor.G, lineColor.B));
            lineItem.IsY2Axis = false;
            this.zedGraphControl1.GraphPane.CurveList.Add(lineItem);

        }
        void InitializePoints()
        {
            if (zedGraphControl1.GraphPane.CurveList.Count != 1)
            {
                InitializeCurve();
            }
            ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[0];
            lineItem.Clear();
            for (int i = 0; i < GRAPHICS_POINT_COUNT; i++)
            {
                lineItem.AddPoint(new ZedGraph.PointPair(i,0));
            }
        }

        public void Clear()
        {
            InitializePoints();
        }

        public void SetValue(double value)
        {
            if (zedGraphControl1.GraphPane.CurveList.Count != 1)
            {
                InitializePoints();
            }
            ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[0];
            for (int i = 0; i < lineItem.Points.Count-1; i++)
            {
                lineItem.Points[i].Y = lineItem.Points[i+1].Y;
            }
            lineItem.Points[lineItem.Points.Count - 1].Y = value;
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
        }

        void DrawBackGround(Graphics g)
        {
            if (_selected)
            {
                this.label_caption.BackColor = System.Drawing.SystemColors.ActiveCaption;
            }
            else
            {
                this.label_caption.BackColor = System.Drawing.SystemColors.Control;
            }

        }
        void DrawValue(Graphics g)
        {
            uvMeter1.Value = (float)_value;
            this.label_value.Text = _value.ToString();
            SetValue(_value);
        }
        void DrawAll(Graphics g)
        {
            DrawBackGround(g);
            DrawValue(g);
        }
        void label_value_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClickEvent();
        }

        void label_unit_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClickEvent();
        }
        void label_scope_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClickEvent();
        }

        bool zedGraphControl1_DoubleClickEvent(ZedGraph.ZedGraphControl sender, MouseEventArgs e)
        {
            OnDoubleClickEvent();
            return true;
        }

        void uvMeter1_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClickEvent();
        }

        void label_caption_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClickEvent();
        }

        void Performance_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClickEvent();
        }

        void Performance_Click(object sender, EventArgs e)
        {
            OnClickEvent();
        }

        void label_caption_Click(object sender, EventArgs e)
        {
            OnClickEvent();
        }
        void zedGraphControl1_Click(object sender, EventArgs e)
        {
            SwitchToMeter();
            OnClickEvent();
        }

        void uvMeter1_Click(object sender, EventArgs e)
        {
            SwitchToDigital();
            OnClickEvent();
        }
        void label_value_Click(object sender, EventArgs e)
        {
            SwitchToGraph();
            OnClickEvent();
        }

        void label_unit_Click(object sender, EventArgs e)
        {
            OnClickEvent();
        }

        void OnDoubleClickEvent()
        {
            Classes.ControlSelectedEventArgs a = new VirtualInstrument.Classes.ControlSelectedEventArgs(VirtualInstrument.Classes.ControlType.Performance, this, true);
            OnControlDoubleClicked(a);
        }
        void OnClickEvent()
        {
            Classes.ControlSelectedEventArgs a = new VirtualInstrument.Classes.ControlSelectedEventArgs(VirtualInstrument.Classes.ControlType.Performance, this, true);
            OnControlSelected(a);
        }
        void SwitchToDigital()
        {
            label_value.Visible = true;
            zedGraphControl1.Visible = false;
            uvMeter1.Visible = false;
        }

        void SwitchToMeter()
        {
            label_value.Visible = false;
            zedGraphControl1.Visible = false;
            uvMeter1.Visible = true;
        }

        void SwitchToGraph()
        {
            label_value.Visible = false;
            zedGraphControl1.Visible = true;
            uvMeter1.Visible = false;

        }
        public void SetSelectedStatus(bool selected)
        {
            if (selected != _selected)
            {
                _needRefreshAll = true;
            }
            _selected = selected;
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
                DrawValue(this.CreateGraphics());
            }
        }

        #endregion

    }

    
}
