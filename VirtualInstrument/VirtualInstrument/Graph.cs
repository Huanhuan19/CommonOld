    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
namespace VirtualInstrument
{
    public partial class Graph : UserControl
    {
        public Graph()
        {
            InitializeComponent();
            zedGraphControl1.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(zedGraphControl1_MouseDownEvent);
            zedGraphControl1.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(zedGraphControl1_MouseMoveEvent);
            zedGraphControl1.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(zedGraphControl1_MouseUpEvent);
            zedGraphControl1.MouseDoubleClick += new MouseEventHandler(zedGraphControl1_MouseDoubleClick);
            zedGraphControl1.Invalidated += new InvalidateEventHandler(zedGraphControl1_Invalidated);
            timer1.Start();
            InitializeGraphics();
            LoadDefault();
        }

        void zedGraphControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetHighLight();
            //int index;
            //if (this.zedGraphControl1.GraphPane.GraphObjList.FindPoint(new PointF(e.Location.X, e.Location.Y), this.zedGraphControl1.GraphPane, this.zedGraphControl1.CreateGraphics(), 1f, out index))
            //{
            //    if (index >= 0 && index < zedGraphControl1.GraphPane.GraphObjList.Count)
            //    {
            //        if (zedGraphControl1.GraphPane.GraphObjList[index].Tag != null && Classes.GraphObjCollection.GetObjTypeFromPack(zedGraphControl1.GraphPane.GraphObjList[index].Tag.ToString()) == VirtualInstrument.Classes.GraphObjType.Line)
            //        {
            //            ZedGraph.LineObj line = (ZedGraph.LineObj)zedGraphControl1.GraphPane.GraphObjList[index];
            //            foreach (Classes.GraphObjCollection section in _sections)
            //            {
            //                foreach (Classes.GraphLineDefine lineDefine in section.GraphLines)
            //                {
            //                    if (lineDefine.LineItem != null)
            //                    {
            //                        if (object.Equals(lineDefine.LineItem, line))
            //                        {
            //                            lineDefine.SetHighLight();
            //                            _needRefreshAll = true;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

        }

        #region Props
        public static float DEFAULT_LINE_WIDTH = 1f;
        public static float FIT_LINE_WIDTH = 4f;
        public static float EXTEND_LINE_WIDTH = 1f;
        public static float STANDARD_CURVE_WIDTH = 2f;
        public static float POINT_SIZE = 3f;
        public static float POINTIMAGE_SIZE = 11f;
        public static float CURRENT_LINE_WIDTH = 3f;
        public static float HIGHLIGHTER_LINE_WIDTH = 10f;
        public static float PROMPT_POINT_SIZE = 8f;
        public static float PROMPT_LINE_WIDTH = 4f;

        public static Color FIT_LINE_COLOR = Color.FromArgb( 255,0,0,0);
        public static Color EXTEND_LINE_COLOR = Color.FromArgb(20, 0, 0, 0);
        public static Color STANDARD_CURVE_COLOR = Color.FromArgb(100, 0, 0, 0);

        Classes.StandardCurveGetValue _getValue = null;
        string _graphName;
        List<Classes.GraphObjCollection> _sections = new List<VirtualInstrument.Classes.GraphObjCollection>();
        Classes.GraphObjCollection _sectionTemplete;
        //List<string> _visibleSectionNames = new List<string>();
        Classes.GraphAxisDefine _xAxis;
        Classes.GraphAxisDefine _yAxis;
        Classes.GraphAxisDefine _y2Axis;
        float _smoothTension = 1f;
        bool _isSmooth = false;
        bool _lockYAxis = false, _lockXAxis = false, _lockY2Axis = false;
        //Classes.GraphObjCollection _currentSection;
        
        List<Classes.GraphLine> _selectedCurves = new List<VirtualInstrument.Classes.GraphLine>();
        Classes.GraphicsSelectType _graphicsSelectType = Classes.GraphicsSelectType.None;
        Color _highLighterColor = Classes.PublicMethods.GetNewHighLighterColor();
        string _defaultFittingMethodName = "";
        public string DefaultFittingMethodName
        {
            get { return _defaultFittingMethodName; }
            set { _defaultFittingMethodName = value; }
        }
        public bool LockYAxis
        {
            get { return _lockYAxis; }
            set { _lockYAxis = value; }
        }
        public bool LockY2Axis
        {
            get { return _lockY2Axis; }
            set { _lockY2Axis = value; }
        }
        public bool LockXAxis
        {
            get { return _lockXAxis; }
            set { _lockXAxis = value; }
        }
        public void LockY(bool IsEnableHZoom)
        {
            zedGraphControl1.IsEnableHZoom = IsEnableHZoom;
        }
        public void LockX(bool IsEnableVZoom)
        {
            zedGraphControl1.IsEnableVZoom = IsEnableVZoom;
        }

        public bool GraphCrossAuto
        {
            get
            {
                return this.zedGraphControl1.GraphPane.XAxis.CrossAuto;
            }
            set
            {
                this.zedGraphControl1.GraphPane.XAxis.CrossAuto = value;
                this.zedGraphControl1.GraphPane.XAxis.Title.IsTitleAtCross = !value;
                this.zedGraphControl1.GraphPane.YAxis.CrossAuto = value;
                this.zedGraphControl1.GraphPane.YAxis.Title.IsTitleAtCross = !value;
                this.zedGraphControl1.GraphPane.Y2Axis.CrossAuto = value;
                this.zedGraphControl1.GraphPane.Y2Axis.Title.IsTitleAtCross = !value;

            }
        }
        public Color HighLighterColor
        {
            get { return _highLighterColor; }
            set { _highLighterColor = value; }
        }
        public Classes.GraphicsSelectType GraphicsSelectType
        {
            get { return _graphicsSelectType; }
            set
            {
                _graphicsSelectType = value;
                //switch (_graphicsSelectType)
                //{
                //    case VirtualInstrument.Classes.GraphicsSelectType.SelectArea:
                //    case VirtualInstrument.Classes.GraphicsSelectType.SelectData:
                //    case VirtualInstrument.Classes.GraphicsSelectType.SelectPoint:
                //        zedGraphControl1.IsEnableHPan = false;
                //        break;
                //    default:
                //        zedGraphControl1.IsEnableHPan = true;
                //        break;
                //}
            }
        }

        public List<Classes.GraphLine> SelectedCurves
        {
            get { return _selectedCurves; }
        }
        public string GraphCaption
        {
            get { return zedGraphControl1.GraphPane.Title.Text; }
            set { zedGraphControl1.GraphPane.Title.Text = value; _needRefreshAll = true; }
        }
        public string GraphName
        {
            get { return _graphName; }
            set { _graphName = value; }
        }
        public Classes.StandardCurveGetValue Method_GetValue
        {
            get { return _getValue; }
            set 
            { 
                _getValue = value;
                _sectionTemplete.GetValueMethod = value;
                for (int i = 0; i < _sections.Count; i++)
                {
                    _sections[i].GetValueMethod = value;
                }
                    
            }
        }
        //public List<Classes.GraphObjCollection> Sections
        //{
        //    get { return _sections; }
        //}
        public int VisibleSectionCount
        {
            get
            {
                return VisibleSectionNames.Count;
            }
        }
        public Classes.GraphObjCollection CurrentSection
        {
            get
            {
                Classes.GraphObjCollection currentSection = _sectionTemplete;
                for (int i = _sections.Count - 1; i >= 0; i--)
                {
                    if (_sections[i].Visible)
                    {
                        currentSection = _sections[i];
                        break;
                    }
                }
                return currentSection;
            }
        }
        public int Count
        {
            get { return _sections.Count; }
        }
        public Classes.GraphObjCollection SectionTemplete
        {
            get { return _sectionTemplete; }
        }
        public List<string> VisibleSectionNames
        {
            get 
            {
                List<string> sectionNames = new List<string>();
                for (int i = 0; i < _sections.Count; i++)
                {
                    if (_sections[i].Visible)
                    {
                        sectionNames.Add(_sections[i].Name);
                    }
                }
                return sectionNames;
            }
        }
        public Classes.GraphAxisDefine XAxis
        {
            get { return _xAxis; }
            set 
            { 
                _xAxis = value;
                SyncXAxis();
                _needRefreshAll = true; 
            }
        }
        public Classes.GraphAxisDefine YAxis
        {
            get { return _yAxis; }
            set 
            { 
                _yAxis = value;
                SyncYAxis();
                _needRefreshAll = true; 
            }
        }
        public Classes.GraphAxisDefine Y2Axis
        {
            get { return _y2Axis; }
            set 
            { 
                _y2Axis = value;
                SyncY2Axis();
                _needRefreshAll = true; 
            }
        }
        public bool IsSmooth
        {
            get { return _isSmooth; }
            set 
            { 
                _isSmooth = value; 
                
                _needRefreshAll = true; 
            }
        }
        public float SmoothTension
        {
            get { return _smoothTension; }
            set { _smoothTension = value; _needRefreshAll = true; }
        }
        public Classes.GraphPointDefine YAxisCenter
        {
            get
            {
                Classes.GraphPointDefine point = new VirtualInstrument.Classes.GraphPointDefine();
                point.X = _xAxis.Minimum + _xAxis.ScaleRange / 2;
                point.Y = _yAxis.Minimum + _yAxis.ScaleRange / 2;
                point.Z = point.Y;
                return point;
            }
        }
        public Classes.GraphPointDefine Y2AxisCenter
        {
            get
            {
                Classes.GraphPointDefine point = new VirtualInstrument.Classes.GraphPointDefine();
                point.X = _xAxis.Minimum + _xAxis.ScaleRange / 2;
                point.Y = _y2Axis.Minimum + _y2Axis.ScaleRange / 2;
                point.Z = point.Y;
                return point;
            }
        }
        public int XCount
        {
            get
            {
                return (int)Math.Round(zedGraphControl1.GraphPane.Chart.Rect.Width, 0);
               
            }
        }
        /// <summary>
        /// 耗费资源，少用此接口
        /// </summary>
        public double YAxisValueMax
        {
            get
            {

                double value = 0;
                int index = -1;
                for (int i = 0; i < _sections.Count; i++)
                {
                    for (int j = 0; j < _sections[i].GraphLines.Count; j++)
                    {
                        Classes.GraphLineDefine line = _sections[i].GraphLines[j];
                        if (line.LineItem != null && !line.LineItem.IsY2Axis)
                        {
                            for (int k = 0; k < line.LineItem.Points.Count; k++)
                            {
                                ZedGraph.PointPair point = line.LineItem.Points[k];
                                index++;
                                if (index == 0)
                                {
                                    value = point.Y;
                                }
                                else
                                {
                                    value = Math.Max(value, point.Y);
                                }
                            }
                        }
                    }
                }
                return value;
            }
        }
        /// <summary>
        /// 耗费资源，少用此接口
        /// </summary>
        public double YAxisValueMin
        {
            get
            {

                double value = 0;
                int index = -1;
                for (int i = 0; i < _sections.Count; i++)
                {
                    for (int j = 0; j < _sections[i].GraphLines.Count; j++)
                    {
                        Classes.GraphLineDefine line = _sections[i].GraphLines[j];
                        if (line.LineItem != null && !line.LineItem.IsY2Axis)
                        {
                            for (int k = 0; k < line.LineItem.Points.Count; k++)
                            {
                                ZedGraph.PointPair point = line.LineItem.Points[k];
                                index++;
                                if (index == 0)
                                {
                                    value = point.Y;
                                }
                                else
                                {
                                    value = Math.Min(value, point.Y);
                                }
                            }
                        }
                    }
                }
                return value;
            }
        }
        /// <summary>
        /// 耗费资源，少用此接口
        /// </summary>
        public double Y2AxisValueMax
        {
            get
            {

                double value = 0;
                int index = -1;
                for (int i = 0; i < _sections.Count; i++)
                {
                    for (int j = 0; j < _sections[i].GraphLines.Count; j++)
                    {
                        Classes.GraphLineDefine line = _sections[i].GraphLines[j];
                        if (line.LineItem != null && line.LineItem.IsY2Axis)
                        {
                            for (int k = 0; k < line.LineItem.Points.Count; k++)
                            {
                                ZedGraph.PointPair point = line.LineItem.Points[k];
                                index++;
                                if (index == 0)
                                {
                                    value = point.Y;
                                }
                                else
                                {
                                    value = Math.Max(value, point.Y);
                                }
                            }
                        }
                    }
                }
                return value;
            }
        }
        /// <summary>
        /// 耗费资源，少用此接口
        /// </summary>
        public double Y2AxisValueMin
        {
            get
            {

                double value = 0;
                int index = -1;
                for (int i = 0; i < _sections.Count; i++)
                {
                    for (int j = 0; j < _sections[i].GraphLines.Count; j++)
                    {
                        Classes.GraphLineDefine line = _sections[i].GraphLines[j];
                        if (line.LineItem != null && line.LineItem.IsY2Axis)
                        {
                            for (int k = 0; k < line.LineItem.Points.Count; k++)
                            {
                                ZedGraph.PointPair point = line.LineItem.Points[k];
                                index++;
                                if (index == 0)
                                {
                                    value = point.Y;
                                }
                                else
                                {
                                    value = Math.Min(value, point.Y);
                                }
                            }
                        }
                    }
                }
                return value;
            }
        }
        /// <summary>
        /// 耗费资源，少用此接口
        /// </summary>
        public double XAxisValueMax
        {
            get
            {

                double value = 0;
                int index = -1;
                for (int i = 0; i < _sections.Count; i++)
                {
                    for (int j = 0; j < _sections[i].GraphLines.Count; j++)
                    {
                        Classes.GraphLineDefine line = _sections[i].GraphLines[j];
                        if (line.LineItem != null)
                        {
                            for (int k = 0; k < line.LineItem.Points.Count; k++)
                            {
                                ZedGraph.PointPair point = line.LineItem.Points[k];
                                index++;
                                if (index == 0)
                                {
                                    value = point.X;
                                }
                                else
                                {
                                    value = Math.Max(value, point.X);
                                }
                            }
                        }
                    }
                }
                return value;
            }
        }
        /// <summary>
        /// 耗费资源，少用此接口
        /// </summary>
        public double XAxisValueMin
        {
            get
            {
                
                double value = 0;
                int index = -1;
                for (int i = 0; i < _sections.Count; i++)
                {
                    for (int j = 0; j < _sections[i].GraphLines.Count; j++)
                    {
                        Classes.GraphLineDefine line = _sections[i].GraphLines[j];
                        if (line.LineItem != null )
                        {
                            for (int k = 0; k < line.LineItem.Points.Count; k++)
                            {
                                ZedGraph.PointPair point = line.LineItem.Points[k];
                                index++;
                                if (index == 0)
                                {
                                    value = point.X;
                                }
                                else
                                {
                                    value = Math.Min(value, point.X);
                                }
                            }
                        }
                    }
                }
                return value;
            }
        }
        #endregion

        #region Variables
        bool _needRefreshAll = false, _needSyncAll = false, _needSyncCurves = false, _needSyncAxis = false, _needSyncObjs = false,
            _needResumeLineWidth = false, _needSetCurrentLineWidth = false, _needSyncVisible = false, _needZoomAuto = false;
        /// <summary>
        /// 画选择线的起始点和结束点
        /// </summary>
        Point _selectStartPoint, _selectEndPoint;
        /// <summary>
        /// <summary>
        /// 是否在选择区域或选中了标签
        /// </summary>
        bool _selecting = false;
        /// <summary>
        /// 用来画选择区域的Box
        /// </summary>
        ZedGraph.BoxObj _boxObj = null;
        /// <summary>
        /// 用于移动标签的Obj
        /// </summary>
        ZedGraph.GraphObj _movingObj = null;
        /// <summary>
        /// 用于移动标签的坐标
        /// </summary>
        double _movingX = 0, _movingY = 0;
        Classes.GraphHighLighterDefine _currentHighLighter;
        ZedGraph.TextObj _xAxisUnit, _yAxisUnit, _y2AxisUnit;
        ZedGraph.ArrowObj _xArrow, _yArrow;
        //bool _isShowAll = true;
        double _lastXMin, _lastXMax, _lastYMin, _lastYMax, _lastY2Min, _lastY2Max;
        bool _canZoomBack = false;
        #endregion

        #region Methods
        void LoadDefault()
        {
            GraphCaption = "";
            _graphName = "";
            _sections.Clear();
            _sectionTemplete = new VirtualInstrument.Classes.GraphObjCollection();
            _sectionTemplete.Name = "SectionTemplete";
            _sectionTemplete.GetValueMethod = _getValue;
            _xAxis = new VirtualInstrument.Classes.GraphAxisDefine();
            _y2Axis = new VirtualInstrument.Classes.GraphAxisDefine();
            _yAxis = new VirtualInstrument.Classes.GraphAxisDefine();
            _y2Axis.Visible = false;
            _smoothTension = 1f;
            _isSmooth = false;
            _lockXAxis = false;
            _lockY2Axis = false;
            _lockYAxis = false;
            _needSyncAll = true;
        }
        public void Initialize( string graphName,string graphCaption )
        {
            //LoadDefault();
            GraphCaption = graphCaption;
            _graphName = graphName;
        }
        public void Initialize(string graphName, string graphCaption, string yAxisCaption, double yMax, double yMin,string xAxisName, string xAxisCaption, double xMax, double xMin, string columnName,string curveCaption,int decimalCount, Color lineColor)
        {
            //LoadDefault();
            _graphName = graphName;
            GraphCaption = graphCaption;
            Y2Axis.Visible = false;
            YAxis.AutoScale = false;
            YAxis.Caption = yAxisCaption;
            YAxis.Visible = true;
            YAxis.Maximum = yMax;
            YAxis.Minimum = yMin;
            XAxis.Visible = true;
            XAxis.Name = xAxisName;
            XAxis.Caption = xAxisCaption;
            XAxis.Maximum = xMax;
            XAxis.Minimum = xMin;
            _lockXAxis = false;
            _lockY2Axis = false;
            _lockYAxis = false;
            AddCurve(columnName, curveCaption, decimalCount, lineColor, false, true, DEFAULT_LINE_WIDTH, false, false, 1f, true);

            _needRefreshAll = true;
        }
        void InitializeGraphics()
        {
            this.zedGraphControl1.GraphPane.TitleGap = 1f;
            //XAxis
            this.zedGraphControl1.GraphPane.XAxis.IsVisible = true;
            this.zedGraphControl1.GraphPane.XAxis.Title.Gap = 0.5f;
            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.IsVisible = true;
            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.DashOff = 0f;
            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.PenWidth = 1f;
            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.Color = Color.FromArgb(100, 255, 0, 0);
            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.IsZeroLine = true;
            this.zedGraphControl1.GraphPane.XAxis.Title.IsTitleAtCross = false;
            this.zedGraphControl1.GraphPane.XAxis.MinorGrid.DashOff = 0f;
            this.zedGraphControl1.GraphPane.XAxis.MinorGrid.PenWidth = 1f;
            this.zedGraphControl1.GraphPane.XAxis.MinorGrid.IsVisible = true;
            this.zedGraphControl1.GraphPane.XAxis.MinorGrid.Color = Color.FromArgb(50, 0, 255, 255);
            this.zedGraphControl1.GraphPane.XAxis.Scale.MaxAuto = false;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MinAuto = false;
            this.zedGraphControl1.GraphPane.XAxis.AxisGap = 0.1f;
            this.zedGraphControl1.GraphPane.XAxis.CrossAuto = true;
            this.zedGraphControl1.GraphPane.XAxis.Title.IsTitleAtCross = false;
            
            //YAxis
            this.zedGraphControl1.GraphPane.YAxis.IsVisible = true;
            this.zedGraphControl1.GraphPane.YAxis.Title.Gap = 0.8f;
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.DashOff = 0f;
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.PenWidth = 1f;
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = true;
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.Color = Color.FromArgb(100, 255, 0, 0);
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.IsZeroLine = true;
            this.zedGraphControl1.GraphPane.YAxis.Title.IsTitleAtCross = false;
            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.DashOff = 0f;
            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.PenWidth = 1f;
            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.IsVisible = true;
            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.Color = Color.FromArgb(50, 0, 255, 255);
            this.zedGraphControl1.GraphPane.YAxis.Scale.MaxAuto = false;
            this.zedGraphControl1.GraphPane.YAxis.Scale.MinAuto = false;
            this.zedGraphControl1.GraphPane.YAxis.AxisGap = 0.1f;
            this.zedGraphControl1.GraphPane.YAxis.CrossAuto = true;
            this.zedGraphControl1.GraphPane.YAxis.Title.IsTitleAtCross = false;
            
            //Y2Axis
            this.zedGraphControl1.GraphPane.Y2Axis.IsVisible = false;
            this.zedGraphControl1.GraphPane.Y2Axis.Title.Gap = 0.1f;
            this.zedGraphControl1.GraphPane.XAxis.Title.FontSpec.Size = 20f;
            this.zedGraphControl1.GraphPane.YAxis.Title.FontSpec.Size = 20f;
            this.zedGraphControl1.GraphPane.Y2Axis.Title.FontSpec.Size = 20f;
            this.zedGraphControl1.GraphPane.Y2Axis.Title.IsTitleAtCross = false;
            this.zedGraphControl1.GraphPane.Y2Axis.Scale.MaxAuto = false;
            this.zedGraphControl1.GraphPane.Y2Axis.Scale.MinAuto = false;
            this.zedGraphControl1.GraphPane.Y2Axis.AxisGap = 0.1f;
            this.zedGraphControl1.GraphPane.Y2Axis.CrossAuto = true;
            this.zedGraphControl1.GraphPane.Y2Axis.Title.IsTitleAtCross = false;
            //Legend
            this.zedGraphControl1.GraphPane.Legend.IsVisible = false;
            this.zedGraphControl1.GraphPane.Legend.Gap = 0.1f;
            this.zedGraphControl1.GraphPane.Legend.Position = ZedGraph.LegendPos.TopCenter;
            this.zedGraphControl1.GraphPane.Legend.FontSpec.Size = 10f;

            _needRefreshAll = true;

        }
        public void ZoomOut()
        {
            this.zedGraphControl1.ZoomOut(zedGraphControl1.GraphPane);
            _needRefreshAll = true;
        }
        public void CopyToClipboard()
        {
            this.zedGraphControl1.Copy(false);
        }
        void zedGraphControl1_Invalidated(object sender, InvalidateEventArgs e)
        {
            DrawUnit();
            FillAssistLines();
        }
        void DrawUnit()
        {
            if (_xAxisUnit != null)
            {
                zedGraphControl1.GraphPane.GraphObjList.Remove(_xAxisUnit);
            }
            if (_yAxisUnit != null)
            {
                zedGraphControl1.GraphPane.GraphObjList.Remove(_yAxisUnit);
            }
            if (_y2AxisUnit != null)
            {
                zedGraphControl1.GraphPane.GraphObjList.Remove(_y2AxisUnit);
            }
            if (_xArrow != null)
            {
                zedGraphControl1.GraphPane.GraphObjList.Remove(_xArrow);
            }
            if (_yArrow != null)
            {
                zedGraphControl1.GraphPane.GraphObjList.Remove(_yArrow);
            }
            double x_x = zedGraphControl1.GraphPane.XAxis.Scale.Max;
            double x_y;
            if( zedGraphControl1.GraphPane.YAxis.Scale.Max < 0  )
            {
                x_y = zedGraphControl1.GraphPane.YAxis.Scale.Max;
            }
            else if (zedGraphControl1.GraphPane.YAxis.Scale.Min > 0)
            {
                x_y = zedGraphControl1.GraphPane.YAxis.Scale.Min;
            }
            else
            {
                x_y = 0;
            }
            _xAxisUnit = new ZedGraph.TextObj(_xAxis.Unit, x_x,x_y , ZedGraph.CoordType.AxisXYScale, ZedGraph.AlignH.Right, ZedGraph.AlignV.Top);
            zedGraphControl1.GraphPane.GraphObjList.Add(_xAxisUnit);
            _xArrow = new ZedGraph.ArrowObj(_xAxis.MainMarginColor, 10f, x_x - (zedGraphControl1.GraphPane.XAxis.Scale.Max - zedGraphControl1.GraphPane.XAxis.Scale.Min)*0.1, x_y, x_x, x_y);
            zedGraphControl1.GraphPane.GraphObjList.Add(_xArrow);            
            if (_yAxis.Visible)
            {
                double y_x;
                double y_y = zedGraphControl1.GraphPane.YAxis.Scale.Max;
                if (zedGraphControl1.GraphPane.XAxis.Scale.Max < 0)
                {
                    y_x = zedGraphControl1.GraphPane.XAxis.Scale.Max;
                }
                else if (zedGraphControl1.GraphPane.XAxis.Scale.Min > 0)
                {
                    y_x = zedGraphControl1.GraphPane.XAxis.Scale.Min;
                }
                else
                {
                    y_x = 0;
                }
                _yAxisUnit = new ZedGraph.TextObj(_yAxis.Unit, y_x, y_y, ZedGraph.CoordType.AxisXYScale, ZedGraph.AlignH.Right, ZedGraph.AlignV.Center);
                zedGraphControl1.GraphPane.GraphObjList.Add(_yAxisUnit);
                _yArrow = new ZedGraph.ArrowObj(_yAxis.MainMarginColor, 10f, y_x, y_y - (zedGraphControl1.GraphPane.YAxis.Scale.Max - zedGraphControl1.GraphPane.YAxis.Scale.Min) * 0.1, y_x, y_y);
                zedGraphControl1.GraphPane.GraphObjList.Add(_yArrow);
            }
            if (_y2Axis.Visible)
            {
                double y_x;
                double y_y = zedGraphControl1.GraphPane.Y2Axis.Scale.Max;
                if (zedGraphControl1.GraphPane.XAxis.Scale.Max < 0)
                {
                    y_x = zedGraphControl1.GraphPane.XAxis.Scale.Max;
                }
                else if (zedGraphControl1.GraphPane.XAxis.Scale.Min > 0)
                {
                    y_x = zedGraphControl1.GraphPane.XAxis.Scale.Min;
                }
                else
                {
                    y_x = 0;
                }
                _y2AxisUnit = new ZedGraph.TextObj(_y2Axis.Unit, y_x, y_y, ZedGraph.CoordType.AxisXY2Scale, ZedGraph.AlignH.Left, ZedGraph.AlignV.Center);
                zedGraphControl1.GraphPane.GraphObjList.Add(_y2AxisUnit);
                if (_yArrow == null)
                {
                    _yArrow = new ZedGraph.ArrowObj(_y2Axis.MainMarginColor, 10f, y_x, y_y - (zedGraphControl1.GraphPane.Y2Axis.Scale.Max - zedGraphControl1.GraphPane.Y2Axis.Scale.Min) * 0.1, y_x, y_y);
                    zedGraphControl1.GraphPane.GraphObjList.Add(_yArrow);
                }
            }

        }
        void FillAssistLines()
        {
            ZedGraph.GraphPane pane = zedGraphControl1.GraphPane;
            _xAxis.Maximum = pane.XAxis.Scale.Max;
            _xAxis.Minimum = pane.XAxis.Scale.Min;
            _y2Axis.Maximum = pane.Y2Axis.Scale.Max;
            _y2Axis.Minimum = pane.Y2Axis.Scale.Min;
            _yAxis.Maximum = pane.YAxis.Scale.Max;
            _yAxis.Minimum = pane.YAxis.Scale.Min;
            for (int i = 0; i < _sectionTemplete.StandardCurves.Count; i++)
            {
                _sectionTemplete.StandardCurves[i].Fill(_xAxis.Minimum, _xAxis.Maximum, XCount);
            }
            for (int i = 0; i < _sectionTemplete.FitLineExtands.Count; i++)
            {
                _sectionTemplete.FitLineExtands[i].Fill(_xAxis.Minimum, _xAxis.Maximum, XCount);
            }
            foreach (Classes.GraphObjCollection section in _sections)
            {
                for (int i = 0; i < section.StandardCurves.Count; i++)
                {
                    section.StandardCurves[i].Fill(_xAxis.Minimum, _xAxis.Maximum, XCount);
                }
                for (int i = 0; i < section.FitLineExtands.Count; i++)
                {
                    section.FitLineExtands[i].Fill(_xAxis.Minimum, _xAxis.Maximum, XCount);
                }
            }
        }
        void RecordZoomBackInfo()
        {
            _canZoomBack = true;
            _lastXMax = zedGraphControl1.GraphPane.XAxis.Scale.Max;
            _lastXMin = zedGraphControl1.GraphPane.XAxis.Scale.Min;
            _lastY2Max = zedGraphControl1.GraphPane.Y2Axis.Scale.Max;
            _lastY2Min = zedGraphControl1.GraphPane.Y2Axis.Scale.Min;
            _lastYMax = zedGraphControl1.GraphPane.YAxis.Scale.Max;
            _lastYMin = zedGraphControl1.GraphPane.YAxis.Scale.Min;

        }
        void ZoomAuto()
        {
            ClearAssistLines();
            RecordZoomBackInfo();
            zedGraphControl1.GraphPane.Y2Axis.Scale.MaxAuto = true;
            zedGraphControl1.GraphPane.Y2Axis.Scale.MinAuto = true;

            zedGraphControl1.GraphPane.YAxis.Scale.MaxAuto = true;
            zedGraphControl1.GraphPane.YAxis.Scale.MinAuto = true;

            zedGraphControl1.GraphPane.XAxis.Scale.MaxAuto = true;
            zedGraphControl1.GraphPane.XAxis.Scale.MinAuto = true;

            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();

            FillAssistLines();

            _needSyncAxis = true;
        }
        void ZoomToSelectedArea()
        {
            double startPx, startPy, endPx, endPy, startPx2, startPy2, endPx2, endPy2;
            this.zedGraphControl1.GraphPane.ReverseTransform(new PointF(_selectStartPoint.X, _selectStartPoint.Y), out startPx, out startPx2, out startPy, out startPy2);
            this.zedGraphControl1.GraphPane.ReverseTransform(new PointF(_selectEndPoint.X, _selectEndPoint.Y), out endPx, out endPx2, out endPy, out endPy2);
            RecordZoomBackInfo();
            if (!_lockXAxis)
            {
                _xAxis.Minimum = Math.Min(startPx, endPx);
                _xAxis.Maximum = Math.Max(startPx, endPx);
            }
            if (!_lockYAxis)
            {
                _yAxis.Minimum = Math.Min(startPy, endPy);
                _yAxis.Maximum = Math.Max(startPy, endPy);
            }
            if (!_lockY2Axis)
            {
                _y2Axis.Minimum = Math.Min(startPy2, endPy2);
                _y2Axis.Maximum = Math.Max(startPy2, endPy2);
            }
            //_isShowAll = false;
            _needSyncAxis = true;
        }
        void ZoomBack()
        {
            if (_canZoomBack)
            {
                _canZoomBack = false;
                _xAxis.Minimum = _lastXMin;
                _xAxis.Maximum = _lastXMax;
                _y2Axis.Maximum = _lastY2Max;
                _y2Axis.Minimum = _lastY2Min;
                _yAxis.Maximum = _lastYMax;
                _yAxis.Minimum = _lastYMin;
                _needSyncAxis = true;
            }
        }
        void ClearAssistLines()
        {
            for (int j = 0; j < this._sectionTemplete.FitLineExtands.Count; j++)
            {
                if (_sectionTemplete.FitLineExtands[j].LineItem != null)
                {
                    _sectionTemplete.FitLineExtands[j].LineItem.Clear();
                }

            }
            //for (int j = 0; j < _sectionTemplete.FitLines.Count; j++)
            //{
            //    if (_sectionTemplete.FitLines[j].LineItem != null)
            //    {
            //        _sectionTemplete.FitLines[j].LineItem.Clear();
            //    }

            //}
            for (int j = 0; j < _sectionTemplete.StandardCurves.Count; j++)
            {
                if (_sectionTemplete.StandardCurves[j].LineItem != null)
                {
                    _sectionTemplete.StandardCurves[j].LineItem.Clear();
                }

            }
            for (int i = 0; i < _sections.Count; i++)
            {
                for (int j = 0; j < _sections[i].FitLineExtands.Count; j++)
                {
                    if (_sections[i].FitLineExtands[j].LineItem != null)
                    {
                        _sections[i].FitLineExtands[j].LineItem.Clear();
                    }

                }
                //for (int j = 0; j < _sections[i].FitLines.Count; j++)
                //{
                //    if (_sections[i].FitLines[j].LineItem != null)
                //    {
                //        _sections[i].FitLines[j].LineItem.Clear();
                //    }

                //}
                for (int j = 0; j < _sections[i].StandardCurves.Count; j++)
                {
                    if (_sections[i].StandardCurves[j].LineItem != null)
                    {
                        _sections[i].StandardCurves[j].LineItem.Clear();
                    }

                }
            }
        }
        public void NeedZoomAuto()
        {
            _needZoomAuto = true;
        }
        public bool ContainsLine(string columnName)
        {
            return _sectionTemplete.LineContains(columnName);
        }
        public void SetHighLight(bool highLight)
        {
            for (int i = 0; i < _sections.Count; i++)
            {
                foreach (Classes.GraphLineDefine graphLine in _sections[i].GraphLines)
                {
                    graphLine.SetHighLight(highLight);
                }
            }
            _needRefreshAll = true;
        }
        public void SetHighLight()
        {
            for (int i = 0; i < _sections.Count; i++)
            {
                foreach (Classes.GraphLineDefine graphLine in _sections[i].GraphLines)
                {
                    graphLine.SetHighLight();
                }
            }
            _needRefreshAll = true;
        }
        #region Section Methods
        public Classes.GraphObjCollection GetSectionByName(string name)
        {
            Classes.GraphObjCollection section = null;
            if (string.Equals(_sectionTemplete.Name, name))
            {
                section = _sectionTemplete;
            }
            else
            {
                for (int i = 0; i < _sections.Count; i++)
                {
                    if (string.Equals(_sections[i].Name, name))
                    {
                        section = _sections[i];
                        break;
                    }
                }
            }
            return section;
        }

        public void RemoveSection(string name)
        {
            for (int i = _sections.Count - 1; i >= 0; i--)
            {
                if (string.Compare(_sections[i].Name, name) == 0)
                {
                    _sections.RemoveAt(i);
                    _needSyncAll = true;
                }
            }
        }
        public bool ContainsSection(string name)
        {
            return GetSectionByName(name) != null;
        }
        public void CloneSectionFromTemplete(string name, string caption, bool syncMode)
        {
            Classes.GraphObjCollection section;
            if (ContainsSection(name))
            {
                section = GetSectionByName(name);
            }
            else
            {
                section = new VirtualInstrument.Classes.GraphObjCollection();
            }
            section.Name = name;
            section.Caption = caption;
            section.GetValueMethod = _getValue;
            section.GraphLines.Clear();
            foreach (Classes.GraphLineDefine line in _sectionTemplete.GraphLines)
            {
                Classes.GraphLineDefine newLine = new VirtualInstrument.Classes.GraphLineDefine(line.ToString());
                newLine.LineColor = Classes.PublicMethods.GetNewColor(line.LineColor,_sections.Count);
                section.GraphLines.Add(newLine);
            }
            _sections.Add(section);
            if (!syncMode)
            {
                _needSyncCurves = true;
            }
            else
            {
                SyncLines(name);
            }
        }

        public void RefreshSecions(bool syncMode)
        {
            for (int i = 0; i < _sections.Count; i++)
            {
                CloneSectionFromTemplete(_sections[i].Name, _sections[i].Caption,syncMode);
            }
        }
        
        public void SectionsRemoveAt(int index)
        {
            if (index >= 0 && index < _sections.Count)
            {
                _sections.RemoveAt(index);
                _needSyncAll = true;
            }
        }
        public void SectionClear()
        {
            _sections.Clear();
            _needSyncAll = true;
        }
        public void SetVisibleSections(List<string> visibleSectionNames)
        {
            for (int i = _sections.Count - 1; i >= 0; i--)
            {
                _sections[i].Visible = visibleSectionNames.Contains(_sections[i].Name);

            }
            _needSyncVisible = true;
            
        }
        public void ResumeLineWidth()
        {
            for (int i = 0; i < _sections.Count; i++)
            {
                for (int j = 0; j < _sections[i].GraphLines.Count; j++)
                {
                    Classes.GraphLineDefine line = _sections[i].GraphLines[j];
                    if (line.LineItem != null)
                    {
                        line.LineItem.Line.Width = line.LineWidthF;
                    }
                }
            }
            _needRefreshAll = true;
        }
        public void SetLastSectionWidth()
        {
            ResumeLineWidth();
            if (_sections.Count > 0)
            {
                int index = _sections.Count - 1;
                foreach (Classes.GraphLineDefine line in _sections[index].GraphLines)
                {
                    if (line.LineItem != null)
                    {
                        line.LineItem.Line.Width = CURRENT_LINE_WIDTH;
                    }
                }
            }
            _needRefreshAll = true;
        }
        #endregion

        #region Obj Methods
        ZedGraph.TextObj GetGLabel(string sectionName, string labelName)
        {
            ZedGraph.TextObj textObj = null;
            for (int i = 0; i < zedGraphControl1.GraphPane.GraphObjList.Count; i++)
            {
                ZedGraph.GraphObj obj = zedGraphControl1.GraphPane.GraphObjList[i];
                if (obj.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(obj.Tag.ToString()) == Classes.GraphObjType.Label)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(obj.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(obj.Tag.ToString()) == labelName)
                        {
                            textObj = (ZedGraph.TextObj)obj;
                            break;
                        }
                    }
                }
            }
            return textObj;
        }
        ZedGraph.LineItem GetGLine(string sectionName, string lineName)
        {
            ZedGraph.LineItem line = null;
            for (int i = 0; i < zedGraphControl1.GraphPane.CurveList.Count; i++)
            {
                ZedGraph.LineItem obj = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (obj.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(obj.Tag.ToString()) == Classes.GraphObjType.Line)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(obj.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(obj.Tag.ToString()) == lineName)
                        {
                            line = (ZedGraph.LineItem)obj;
                            break;
                        }
                    }
                }
            }

            return line;
        }
        ZedGraph.LineItem GetGFitLine(string sectionName, string lineName)
        {
            ZedGraph.LineItem line = null;
            for (int i = 0; i < zedGraphControl1.GraphPane.CurveList.Count; i++)
            {
                ZedGraph.LineItem obj = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (obj.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(obj.Tag.ToString()) == Classes.GraphObjType.FitLine)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(obj.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(obj.Tag.ToString()) == lineName)
                        {
                            line = (ZedGraph.LineItem)obj;
                            break;
                        }
                    }
                }
            }
            return line;
        }
        ZedGraph.LineItem GetGFitLineExtand(string sectionName, string lineName)
        {
            ZedGraph.LineItem line = null;
            for (int i = 0; i < zedGraphControl1.GraphPane.CurveList.Count; i++)
            {
                ZedGraph.LineItem obj = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (obj.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(obj.Tag.ToString()) == Classes.GraphObjType.FitLineExtand)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(obj.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(obj.Tag.ToString()) == lineName)
                        {
                            line = (ZedGraph.LineItem)obj;
                            break;
                        }
                    }
                }
            }
            return line;
        }
        ZedGraph.LineItem GetGStandardCurve(string sectionName, string lineName)
        {
            ZedGraph.LineItem line = null;
            for (int i = 0; i < zedGraphControl1.GraphPane.CurveList.Count; i++)
            {
                ZedGraph.LineItem obj = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (obj.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(obj.Tag.ToString()) == Classes.GraphObjType.StandardCurve)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(obj.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(obj.Tag.ToString()) == lineName)
                        {
                            line = (ZedGraph.LineItem)obj;
                            break;
                        }
                    }
                }
            }
            return line;
        }
        #endregion

        #region Get Define Methods
        public Classes.GraphLineDefine DGetLineDefine(string sectionName, string columnName)
        {
            Classes.GraphLineDefine lineDefine = null;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i <= section.GraphLines.Count; i++)
                {
                    if (string.Compare(section.GraphLines[i].ColumnDefine.ColumnName, columnName) == 0)
                    {
                        lineDefine = section.GraphLines[i];
                        break;
                    }
                }
            }
            return lineDefine;
        }
        public Classes.GraphLabelDefine DGetLabelDefine(string sectionName, string labelName)
        {
            Classes.GraphLabelDefine labelDefine = null;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.GraphLabels.Count; i++)
                {
                    Classes.GraphLabelDefine label = section.GraphLabels[i];
                    if (string.Compare(label.LabelName, labelName) == 0)
                    {
                        labelDefine = label;
                        break;
                    }
                }
            }
            return labelDefine;
        }
        public Classes.StandardCurveDefine DGeStandardCurveDefine(string sectionName, string labelName)
        {
            Classes.StandardCurveDefine lineDefine = null;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.StandardCurves.Count; i++)
                {
                    Classes.StandardCurveDefine line = section.StandardCurves[i];
                    if (string.Compare(line.Name, labelName) == 0)
                    {
                        lineDefine = line;
                        break;
                    }
                }
            }
            return lineDefine;
        }
        public Classes.StandardCurveDefine DGeFitLineExtandDefine(string sectionName, string labelName)
        {
            Classes.StandardCurveDefine lineDefine = null;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.FitLineExtands.Count; i++)
                {
                    Classes.StandardCurveDefine line = section.FitLineExtands[i];
                    if (string.Compare(line.Name, labelName) == 0)
                    {
                        lineDefine = line;
                        break;
                    }
                }
            }
            return lineDefine;
        }
        public Classes.GraphFitLineDefine DGeFitLineDefine(string sectionName, string lineName)
        {
            Classes.GraphFitLineDefine lineDefine = null;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.FitLines.Count; i++)
                {
                    Classes.GraphFitLineDefine line = section.FitLines[i];
                    if (string.Compare(line.CurveDefine.Name, lineName) == 0)
                    {
                        lineDefine = line;
                        break;
                    }
                }
            }
            return lineDefine;
        }
        #endregion

        #region Add Methods
        public void AddLabel(string caption,string text, double x, double y,double z,bool syncMode)
        {
            Classes.GraphObjCollection section = _sectionTemplete;
            Classes.GraphLabelDefine label = section.AddLabel(caption, text, x, y, z);
            if (syncMode)
            {
                ZedGraph.TextObj textObj = label.CreateLabel(section.Name);
                zedGraphControl1.GraphPane.GraphObjList.Add(textObj);
                textObj.ZOrder = ZedGraph.ZOrder.A_InFront;
                
            }
            else
            {
                _needSyncObjs = true;
            }
        }
        public void AddCurve(string columnName, string caption, int decimalCount, Color lineColor, bool pointVisible, bool lineVisible, float lineWidth, bool isY2Axis, bool isSmooth, float smoothTention, bool syncMode)
        {
            AddCurve(columnName, caption, decimalCount, lineColor, pointVisible, lineVisible, lineWidth, isY2Axis, isSmooth, smoothTention, syncMode, false);
        }
        public void AddCurve(string columnName, string caption, int decimalCount, Color lineColor, bool pointVisible, bool lineVisible, float lineWidth, bool isY2Axis, bool isSmooth, float smoothTention, bool syncMode, bool draw3DPoint)
        {
            if (!_sectionTemplete.LineContains(columnName))
            {

                _sectionTemplete.AddLine(columnName, caption, decimalCount, lineColor, pointVisible, lineVisible, lineWidth, isY2Axis, isSmooth, smoothTention,draw3DPoint);
                for (int i = 0; i < _sections.Count; i++)
                {
                    if (!_sections[i].LineContains(columnName))
                    {
                        Color newColor =  Classes.PublicMethods.GetNewColor(lineColor,i);
                        //if (_sectionTemplete.GraphLines.Count == 1)
                        //{
                        //    newColor = Classes.PublicMethods.GetNewColor(i);
                        //}
                        Classes.GraphLineDefine line = _sections[i].AddLine(columnName, caption, decimalCount, newColor, pointVisible, lineVisible, lineWidth, isY2Axis, isSmooth, smoothTention,draw3DPoint);
                        if (syncMode)
                        {
                            zedGraphControl1.GraphPane.CurveList.Add(line.CreateLineItem(_sections[i].Name));
                            int index = zedGraphControl1.GraphPane.CurveList.Count - 1;
                            //zedGraphControl1.GraphPane.CurveList[index].MakeUnique(new ZedGraph.ColorSymbolRotator());
                        }
                        else
                        {
                            _needSyncCurves = true;
                        }
                    }
                }
            }
        }
        public void AddFitLine(string sectionName, string caption, string methodName, double a, double b, double c, double d, bool isY2Axis, double start, double end, bool extended)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                Classes.GraphFitLineDefine line = section.AddFitLine(start, end, caption, methodName, a, b, c, d, isY2Axis);
                zedGraphControl1.GraphPane.CurveList.Add(line.CreateLineItem(section.Name));

                if (extended)
                {
                    Classes.StandardCurveDefine exLine = section.AddFitLineExtand(caption, methodName, a, b, c, d, isY2Axis);
                    zedGraphControl1.GraphPane.CurveList.Add(exLine.CreateLineItem(section.Name));
                }
                _needRefreshAll = true;
            }
        }
        public void AddFitLine(string sectionName, string caption, string methodName,List<Classes.GraphPointDefine> points ,bool isY2Axis)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                Classes.GraphFitLineDefine line = section.AddFitLine(caption,methodName,points,isY2Axis);
                zedGraphControl1.GraphPane.CurveList.Add(line.CreateLineItem(section.Name));

                _needRefreshAll = true;
            }
        }
        public Classes.StandardCurveDefine AddStandardCurve(string sectionName, string caption, string methodName, double a, double b, double c, double d, bool isY2Axis)
        {
            Classes.StandardCurveDefine line = null ;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                line = section.AddStandardCurve(caption, methodName, a, b, c, d, isY2Axis);
                zedGraphControl1.GraphPane.CurveList.Add(line.CreateLineItem(section.Name));
                line.Fill(_xAxis.Minimum, _xAxis.Maximum, XCount);
                _needRefreshAll = true;
            }
            return line;
        }
        public Classes.GraphHighLighterDefine AddHighLighter(string sectionName,Color color)
        {
            Classes.GraphHighLighterDefine highLighter = null;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                highLighter = section.AddHighLighter(color);
                zedGraphControl1.GraphPane.CurveList.Add(highLighter.CreateLineItem(section.Name));
                _needRefreshAll = true;
            }
            return highLighter;
        }
        public Classes.GraphManualLine AddManualLine(string sectionName,string xCaption,string yCaption, Color color,List<Classes.GraphPointDefine> points)
        {
            Classes.GraphManualLine line = null;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                line = section.AddManualLine(xCaption,yCaption,color);
                for (int i = 0; i < points.Count; i++)
                {
                    line.Add(points[i].X, points[i].Y);
                }
                zedGraphControl1.GraphPane.CurveList.Add(line.CreateLineItem(section.Name));
                _needRefreshAll = true;
            }
            return line;
        }
        public Classes.GraphManualLine AddLine(string sectionName, string xCaption, string yCaption, Color color, List<Classes.GraphPointDefine> points)//求导
        {
            Classes.GraphManualLine line = null;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                line = section.AddLine_V(xCaption, yCaption, color);
                for (int i = 0; i < points.Count; i++)
                {
                    line.Add(points[i].X, points[i].Y);
                }
                zedGraphControl1.GraphPane.CurveList.Add(line.CreateLineItem(section.Name));
                _needRefreshAll = true;
            }
            return line;
        }
        public void AddPoint(string sectionName, string columnName, double x, double y, string sectionCaption)
        {
            Classes.GraphObjCollection section= GetSectionByName(sectionName);
            if (section != null)
            {
                Classes.GraphLineDefine line = section.GetLineDefineByName(columnName);
                if (line != null)
                {
                    line.AddPoint(sectionCaption,x, y);
                    if (line.IsY2Axis)
                    {
                        ResetY2Scale(y);
                    }
                    else
                    {
                        ResetYScale(y);
                    }
                    ResetXScale(x);

                    _needRefreshAll = true;
                }
            }
            //GAddPoint(sectionName, columnName, x, y);
        }
        #endregion

        #region GAdd Methods
        void GAddFitLine(string sectionName, Classes.GraphFitLineDefine lineDefine)
        {
            ZedGraph.LineItem lineItem = new ZedGraph.LineItem(lineDefine.CurveDefine.Caption);
            lineItem.Color = FIT_LINE_COLOR;
            lineItem.Line.IsVisible = true;
            lineItem.Line.IsSmooth = true;
            lineItem.Line.SmoothTension = 1f;
            lineItem.Line.Width = FIT_LINE_WIDTH;
            lineItem.Symbol = new ZedGraph.Symbol(ZedGraph.SymbolType.Circle, FIT_LINE_COLOR);
            lineItem.Symbol.IsVisible = false;
            lineItem.Symbol.Size = POINT_SIZE;
            lineItem.Symbol.Fill = new ZedGraph.Fill(FIT_LINE_COLOR);
            lineItem.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.FitLine, sectionName,lineDefine.CurveDefine.Name, lineDefine.ToString());
            lineItem.IsY2Axis = lineDefine.CurveDefine.IsY2Axis;
            lineDefine.LineItem = lineItem;
            this.zedGraphControl1.GraphPane.CurveList.Add(lineItem);
            //_needRefreshAll = true;
            GFillFitLine(sectionName, lineDefine.CurveDefine.Name);
        }
        void GAddExtandLine(string sectionName, Classes.StandardCurveDefine lineDefine)
        {
            ZedGraph.LineItem lineItem = new ZedGraph.LineItem(lineDefine.Caption);
            lineItem.Color = EXTEND_LINE_COLOR;
            lineItem.Line.IsVisible = true;
            lineItem.Line.IsSmooth = true;
            lineItem.Line.SmoothTension = 1f;
            lineItem.Line.Width = EXTEND_LINE_WIDTH;
            lineItem.Symbol = new ZedGraph.Symbol(ZedGraph.SymbolType.Circle, EXTEND_LINE_COLOR);
            lineItem.Symbol.IsVisible = false;
            lineItem.Symbol.Size = POINT_SIZE;
            lineItem.Symbol.Fill = new ZedGraph.Fill(EXTEND_LINE_COLOR);
            lineItem.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.FitLineExtand, sectionName,lineDefine.Name, lineDefine.ToString());
            lineItem.IsY2Axis = lineDefine.IsY2Axis;
            lineDefine.LineItem = lineItem;
            this.zedGraphControl1.GraphPane.CurveList.Add(lineItem);
            //_needRefreshAll = true;
            GFillFitExtandLine(sectionName, lineDefine.Name);
        }
        void GAddStandardCurve(string sectionName,Classes.StandardCurveDefine lineDefine)
        {
            ZedGraph.LineItem lineItem = new ZedGraph.LineItem(lineDefine.Caption);
            lineItem.Color = STANDARD_CURVE_COLOR;
            lineItem.Line.IsVisible = true;
            lineItem.Line.IsSmooth = true;
            lineItem.Line.SmoothTension = 1f;
            lineItem.Line.Width = STANDARD_CURVE_WIDTH;
            lineItem.Symbol = new ZedGraph.Symbol(ZedGraph.SymbolType.Circle, STANDARD_CURVE_COLOR);
            lineItem.Symbol.IsVisible = false;
            lineItem.Symbol.Size = POINT_SIZE;
            lineItem.Symbol.Fill = new ZedGraph.Fill(STANDARD_CURVE_COLOR);
            lineItem.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.StandardCurve, sectionName,lineDefine.Name, lineDefine.ToString());
            lineItem.IsY2Axis = lineDefine.IsY2Axis;

            this.zedGraphControl1.GraphPane.CurveList.Add(lineItem);
            lineDefine.LineItem = lineItem;
            //_needRefreshAll = true;
            GFillStandardCurve(sectionName, lineDefine.Name);
        }
        void GAddLabel(string sectionName,Classes.GraphLabelDefine labelDefine)
        {
            ZedGraph.TextObj label = new ZedGraph.TextObj(labelDefine.LabelText, labelDefine.Position.X, labelDefine.Position.Y,  ZedGraph.CoordType.AxisXYScale,ZedGraph.AlignH .Left,ZedGraph.AlignV.Center);
            label.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.Label,sectionName,labelDefine.LabelName, labelDefine.ToString());
            labelDefine.Label = label;
            zedGraphControl1.GraphPane.GraphObjList.Add(label);
            _needRefreshAll = true;

        }
        void GAddCurve(string sectionName,Classes.GraphLineDefine line)
        {
            ZedGraph.LineItem lineItem = new ZedGraph.LineItem(line.ColumnDefine.ColumnCaption);
            lineItem.Color = line.LineColor;
            lineItem.Line.IsVisible = line.LineVisible;
            lineItem.Line.IsSmooth = line.IsSmooth;
            lineItem.Line.SmoothTension = line.SmoothTention;
            lineItem.Line.Width = DEFAULT_LINE_WIDTH;
            lineItem.Symbol = new ZedGraph.Symbol(ZedGraph.SymbolType.Circle, line.LineColor);
            lineItem.Symbol.IsVisible = line.PointVisible;
            lineItem.Symbol.Size = POINT_SIZE;
            lineItem.Symbol.Fill = new ZedGraph.Fill(line.LineColor);
            lineItem.Tag = Classes.GraphObjCollection.Pack(Classes.GraphObjType.Line, sectionName,line.ColumnDefine.ColumnName, line.ToString());
            lineItem.IsY2Axis = line.IsY2Axis;
            line.LineItem = lineItem;
            this.zedGraphControl1.GraphPane.CurveList.Add(lineItem);
            //_needRefreshAll = true;
        }
        void GAddPoint(string sectionName, string columnName, double x, double y)
        {
            ZedGraph.LineItem lineItem = GetGLine(sectionName, columnName);
            if (lineItem != null)
            {
                lineItem.AddPoint(new ZedGraph.PointPair(x, y,PointString( x, y)));
                if (lineItem.IsY2Axis)
                {
                    ResetY2Scale(y);
                }
                else
                {
                    ResetYScale(y);
                }
                ResetXScale(x);
                _needRefreshAll = true;
            }
        }
        void GFillFitLine(string sectionName, string lineName)
        {
            ZedGraph.LineItem lineItem = GetGFitLine(sectionName, lineName);
            Classes.GraphFitLineDefine fitLine = DGeFitLineDefine(sectionName, lineName);
            if (lineItem != null && fitLine != null && _getValue != null)
            {
                lineItem.Clear();
                int count = CalcChartPoint(fitLine.StartPosition, fitLine.EndPosition);
                if (count > 0)
                {
                    double interval = (Math.Max(fitLine.StartPosition, fitLine.EndPosition) - Math.Min(fitLine.StartPosition, fitLine.EndPosition)) / count;
                    for (int i = 0; i < count; i++)
                    {
                        double x = fitLine.StartPosition + interval * i;
                        double y = _getValue(fitLine.CurveDefine.MethodName, x,fitLine.CurveDefine.A,fitLine.CurveDefine.B,fitLine.CurveDefine.C,fitLine.CurveDefine.D);
                        lineItem.AddPoint(new ZedGraph.PointPair(x, y,PointString(x,y)));
                        if (lineItem.IsY2Axis)
                        {
                            ResetY2Scale(y);
                        }
                        else
                        {
                            ResetYScale(y);
                        }
                        ResetXScale(x);
                        
                    }
                    _needRefreshAll = true;
                }
            }
        }
        void GFillFitExtandLine(string sectionName, string lineName)
        {
            ZedGraph.LineItem lineItem = GetGFitLineExtand(sectionName, lineName);
            Classes.StandardCurveDefine fitLine = DGeFitLineExtandDefine(sectionName, lineName);
            if (lineItem != null && fitLine != null && _getValue != null)
            {
                lineItem.Clear();
                int count = (int)Math.Round(zedGraphControl1.GraphPane.Chart.Rect.Width,0);
                double startPosition = zedGraphControl1.GraphPane.XAxis.Scale.Min;
                if (count > 0)
                {
                    double interval = (zedGraphControl1.GraphPane.XAxis.Scale.Max- zedGraphControl1.GraphPane.XAxis.Scale.Min) / count;
                    for (int i = 0; i < count; i++)
                    {
                        double x = startPosition + interval * i;
                        double y = _getValue(fitLine.MethodName, x,fitLine.A,fitLine.B,fitLine.C,fitLine.D);
                        lineItem.AddPoint(new ZedGraph.PointPair(x, y, PointString(x, y)));
                        if (lineItem.IsY2Axis)
                        {
                            ResetY2Scale(y);
                        }
                        else
                        {
                            ResetYScale(y);
                        }
                        ResetXScale(x);
                    }
                    _needRefreshAll = true;
                }
            }
        }
        void GFillStandardCurve(string sectionName, string lineName)
        {
            ZedGraph.LineItem lineItem = GetGStandardCurve(sectionName, lineName);
            Classes.StandardCurveDefine line = DGeStandardCurveDefine(sectionName, lineName);
            if (lineItem != null && line != null && _getValue != null)
            {
                lineItem.Clear();
                int count = (int)Math.Round(zedGraphControl1.GraphPane.Chart.Rect.Width, 0);
                double startPosition = zedGraphControl1.GraphPane.XAxis.Scale.Min;
                if (count > 0)
                {
                    double interval = (zedGraphControl1.GraphPane.XAxis.Scale.Max - zedGraphControl1.GraphPane.XAxis.Scale.Min) / count;
                    for (int i = 0; i < count; i++)
                    {
                        double x = startPosition + interval * i;
                        double y = _getValue(line.MethodName, x,line.A,line.B,line.C,line.D);
                        lineItem.AddPoint(new ZedGraph.PointPair(x, y, PointString(x, y)));
                        if (lineItem.IsY2Axis)
                        {
                            ResetY2Scale(y);
                        }
                        else
                        {
                            ResetYScale(y);
                        }
                        ResetXScale(x);
                    }
                    _needRefreshAll = true;
                }
            }
        }
        public void ResetScale(double x, double y, bool isY2Axis)
        {
            if (isY2Axis)
            {
                ResetY2Scale(y);
            }
            else
            {
                ResetYScale(y);
            }
            ResetXScale(x);
            _needRefreshAll = true;
        }

        void ResetXScale(double x)
        {
            if (_xAxis.AutoScale)
            {
                double range = _xAxis.ScaleRange;
                if (x >= _xAxis.Minimum + range*0.8)
                {
                    double delta = x - _xAxis.Minimum - range *0.8;
                    if (_xAxis.LockRange)
                    {
                        _xAxis.Minimum += delta;
                    }
                    _xAxis.Maximum += delta;
                    _needSyncAxis = true;
                }
                if (x <= _xAxis.Maximum - range*0.8)
                {
                    double delta = _xAxis.Maximum - range *0.8 - x;
                    if (_xAxis.LockRange)
                    {
                        _xAxis.Maximum -= delta;
                    }
                    _xAxis.Minimum -= delta;
                    _needSyncAxis = true;
                }
            }
        }
        void ResetYScale(double y)
        {
            if (_yAxis.AutoScale)
            {
                double range = _yAxis.ScaleRange;
                if (y >= _yAxis.Minimum + _yAxis.ScaleRange*0.8)
                {
                    double delta = y - _yAxis.Minimum - range *0.8;
                    if (_yAxis.LockRange)
                    {
                        _yAxis.Minimum += delta;
                    }

                    _yAxis.Maximum += delta;
                    _needSyncAxis = true;
                }
                if (y <= _yAxis.Maximum - _yAxis.ScaleRange )
                {
                    double delta = _yAxis.Maximum - range - y;
                    if (_yAxis.LockRange)
                    {
                        _yAxis.Maximum -= delta;
                    }
                    _yAxis.Minimum -= delta;
                    _needSyncAxis = true;
                }
            }
        }
        void ResetY2Scale(double y)
        {
            if (_y2Axis.AutoScale)
            {
                double range = _y2Axis.ScaleRange;
                if (y >= _y2Axis.Minimum + _y2Axis.ScaleRange *0.8)
                {
                    double delta = y - _y2Axis.Minimum - range *0.8;
                    if (_y2Axis.LockRange)
                    {
                        _y2Axis.Minimum += delta;
                    }

                    _yAxis.Maximum += delta;
                    _needSyncAxis = true;
                }
                if (y <= _y2Axis.Maximum - _y2Axis.ScaleRange*0.8 )
                {
                    double delta = _y2Axis.Maximum - range * 0.8 - y;
                    if (_y2Axis.LockRange)
                    {
                        _y2Axis.Maximum -= delta;
                    }
                    _y2Axis.Minimum -= delta;
                    _needSyncAxis = true;
                }
            }
        }
        public void ResetAxis()
        {
            ResetXAxis();
            ResetY2Axis();
            ResetYAxis();
        }
        public void ResetYAxis()
        {
            double max = YAxisValueMax;
            double min = YAxisValueMin;
            if (max > min)
            {
                _yAxis.Maximum = max;
                _yAxis.Minimum = min;
                _needSyncAxis = true;
            }
        }
        public void ResetY2Axis()
        {
            double max = Y2AxisValueMax;
            double min = Y2AxisValueMin;
            if (max > min)
            {
                _y2Axis.Maximum = max;
                _y2Axis.Minimum = min;
                _needSyncAxis = true;
            }
        }
        public void ResetXAxis()
        {
            double max = XAxisValueMax;
            double min = XAxisValueMin;
            if (max > min)
            {
                _xAxis.Maximum = max;
                _xAxis.Minimum = min;
                _needSyncAxis = true;
            }
        }

        #endregion

        #region clear methods
        public void ClearManualLines(bool syncMode)
        {
            for (int j = this._sectionTemplete.ManualLines.Count - 1; j >= 0; j--)
            {
                Classes.GraphManualLine line = _sectionTemplete.ManualLines[j];
                if (line.LineItem != null)
                {
                    if (syncMode)
                    {
                        zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                    }
                    else
                    {
                        _needSyncObjs = true;
                    }
                }
                _sectionTemplete.ManualLines.RemoveAt(j);
            }
            for (int i = _sections.Count - 1; i >= 0; i--)
            {
                for (int j = _sections[i].ManualLines.Count - 1; j >= 0; j--)
                {
                    Classes.GraphManualLine line = _sections[i].ManualLines[j];
                    if (line.LineItem != null)
                    {
                        if (syncMode)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                        }
                        else
                        {
                            _needSyncObjs = true;
                        }
                    }
                    _sections[i].ManualLines.RemoveAt(j);
                }
            }
            _needRefreshAll = true;
        }
        public void ClearHighLighters(bool syncMode)
        {
            for (int j = this._sectionTemplete.HighLighters.Count - 1; j >= 0; j--)
            {
                Classes.GraphHighLighterDefine line = _sectionTemplete.HighLighters[j];
                if (line.LineItem != null)
                {
                    if (syncMode)
                    {
                        zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                    }
                    else
                    {
                        _needSyncObjs = true;
                    }
                }
                _sectionTemplete.HighLighters.RemoveAt(j);
            }
            for (int i = _sections.Count - 1; i >= 0; i--)
            {
                for (int j = _sections[i].HighLighters.Count - 1; j >= 0; j--)
                {
                    Classes.GraphHighLighterDefine line = _sections[i].HighLighters[j];
                    if (line.LineItem != null)
                    {
                        if (syncMode)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                        }
                        else
                        {
                            _needSyncObjs = true;
                        }
                    }
                    _sections[i].HighLighters.RemoveAt(j);
                }
            }
            _needRefreshAll = true;
        }
        public void ClearFitLines(bool syncMode)
        {
            for (int j =this._sectionTemplete.FitLines.Count - 1; j >= 0; j--)
            {
                Classes.GraphFitLineDefine line = _sectionTemplete.FitLines[j];
                if (line.LineItem != null)
                {
                    if (syncMode)
                    {
                        zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                    }
                    else
                    {
                        _needSyncObjs = true;
                    }
                }
                _sectionTemplete.FitLines.RemoveAt(j);
            }
            for (int i = _sections.Count - 1; i >= 0; i--)
            {
                for (int j = _sections[i].FitLines.Count - 1; j >= 0; j--)
                {
                    Classes.GraphFitLineDefine line = _sections[i].FitLines[j];
                    if (line.LineItem != null)
                    {
                        if (syncMode)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                        }
                        else
                        {
                            _needSyncObjs = true;
                        }
                    }
                    _sections[i].FitLines.RemoveAt(j);
                }
            }
            ClearFitLineExtands(syncMode);
            _needRefreshAll = true;
        }
        public void ClearFitLineExtands(bool syncMode)
        {
            for (int j = this._sectionTemplete.FitLineExtands.Count - 1; j >= 0; j--)
            {
                Classes.StandardCurveDefine line = _sectionTemplete.FitLineExtands[j];
                if (line.LineItem != null)
                {
                    if (syncMode)
                    {
                        zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                    }
                    else
                    {
                        _needSyncObjs = true;
                    }
                }
                _sectionTemplete.FitLineExtands.RemoveAt(j);
            }
            for (int i = _sections.Count - 1; i >= 0; i--)
            {
                for (int j = _sections[i].FitLineExtands.Count - 1; j >= 0; j--)
                {
                    Classes.StandardCurveDefine line = _sections[i].FitLineExtands[j];
                    if (line.LineItem != null)
                    {
                        if (syncMode)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                        }
                        else
                        {
                            _needSyncObjs = true;
                        }
                    }
                    _sections[i].FitLineExtands.RemoveAt(j);
                }
            } 
            _needRefreshAll = true;

        }

        public void ClearStandardCurves(bool syncMode)
        {
            ClearStandardCurves(_sectionTemplete.Name, syncMode);
            for (int i = 0; i < _sections.Count; i++)
            {
                ClearStandardCurves(_sections[i].Name, syncMode);
            }
        }
        public void ClearStandardCurves(string sectionName, bool syncMode)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = section.StandardCurves.Count - 1; i >= 0; i--)
                {
                    Classes.StandardCurveDefine line = section.StandardCurves[i];
                    if (line.LineItem != null)
                    {
                        if (syncMode)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                        }
                        else
                        {
                            _needSyncObjs = true;
                        }
                    }
                    section.StandardCurves.RemoveAt(i);
                    _needRefreshAll = true;
                }
            }
        }

        public void ClearLabels(bool syncMode)
        {
            for (int i = this._sectionTemplete.GraphLabels.Count - 1; i >= 0; i--)
            {
                Classes.GraphLabelDefine label = _sectionTemplete.GraphLabels[i];
                if (label.Label != null)
                {
                    if (syncMode)
                    {
                        zedGraphControl1.GraphPane.GraphObjList.Remove(label.Label);
                    }
                    else
                    {
                        _needSyncObjs = true;
                    }
                }
                _sectionTemplete.GraphLabels.RemoveAt(i);
            }
            for (int i = _sections.Count - 1; i >= 0; i--)
            {
                for (int j = _sections[i].GraphLabels.Count - 1; j >= 0; j--)
                {
                    Classes.GraphLabelDefine label = _sections[i].GraphLabels[j];
                    if (label.Label != null)
                    {
                        if (syncMode)
                        {
                            zedGraphControl1.GraphPane.GraphObjList.Remove(label.Label);
                        }
                        else
                        {
                            _needSyncObjs = true;
                        }
                    }
                    _sections[i].GraphLabels.RemoveAt(j);
                }
            }
            _needRefreshAll = true;
        }
        public void ClearLines(bool syncMode)
        {
            for (int i = _sectionTemplete.GraphLines.Count - 1; i >= 0; i--)
            {
                Classes.GraphLineDefine line = _sectionTemplete.GraphLines[i];
                if (line.LineItem != null)
                {
                    if (syncMode)
                    {
                        zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                    }
                    else
                    {
                        _needSyncCurves = true;
                    }
                }
                _sectionTemplete.GraphLines.RemoveAt(i);
            }
            for (int i = _sections.Count - 1; i >= 0; i--)
            {
                for (int j = _sections[i].GraphLines.Count - 1; j >= 0; j--)
                {
                    Classes.GraphLineDefine line = _sections[i].GraphLines[j];
                    if (line.LineItem != null)
                    {
                        if (syncMode)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(line.LineItem);
                        }
                        else
                        {
                            _needSyncCurves = true;
                        }
                    }
                    _sections[i].GraphLines.RemoveAt(j);
                }
            }
            _needRefreshAll = true;

        }
        public void ClearPoints(string sectionName, string columnName)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.GraphLines.Count; i++)
                {
                    if (string.Equals(section.GraphLines[i].ColumnDefine.ColumnName, columnName))
                    {
                        if (section.GraphLines[i].LineItem != null)
                        {
                            section.GraphLines[i].LineItem.Clear();
                            _needRefreshAll = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region Remove Methods
        public void RemoveManualLine(string sectionName, string name)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = section.ManualLines.Count - 1; i >= 0; i--)
                {
                    if (string.Equals(section.ManualLines[i].Name, name))
                    {
                        if (section.ManualLines[i].LineItem != null)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(section.ManualLines[i].LineItem);
                        }
                        section.ManualLines.RemoveAt(i);
                        _needRefreshAll = true;
                    }
                }
            }
        }
        public void RemoveHighLighter(string sectionName, string name)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = section.HighLighters.Count - 1; i >= 0; i--)
                {
                    if (string.Equals(section.HighLighters[i].Name, name))
                    {
                        if (section.HighLighters[i].LineItem != null)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(section.HighLighters[i].LineItem);
                        }
                        section.HighLighters.RemoveAt(i);
                        _needRefreshAll = true;
                    }
                }
            }
        }
        public void RemoveFitLine(string sectionName, string name)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = section.FitLines.Count - 1; i >= 0; i--)
                {
                    if (string.Equals(section.FitLines[i].CurveDefine.Name, name))
                    {
                        if (section.FitLines[i].LineItem != null)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(section.FitLines[i].LineItem);
                        }
                        section.FitLines.RemoveAt(i);
                        _needRefreshAll = true;
                    }
                }
                for (int i = section.FitLineExtands.Count - 1; i >= 0; i--)
                {
                    if (string.Equals(section.FitLineExtands[i].Name, name))
                    {
                        if (section.FitLineExtands[i].LineItem != null)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(section.FitLineExtands[i].LineItem);
                        }
                        section.FitLineExtands.RemoveAt(i);
                        _needRefreshAll = true;
                    }
                }
            }
        }
        public void RemoveStandardCurve(string sectionName, string name)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = section.StandardCurves.Count - 1; i >= 0; i--)
                {
                    if (string.Equals(section.StandardCurves[i].Name, name))
                    {
                        if (section.StandardCurves[i].LineItem != null)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(section.StandardCurves[i].LineItem);
                        }
                        section.StandardCurves.RemoveAt(i);
                        _needRefreshAll = true;
                    }
                }
            }
        }
        public void RemoveLabel(string sectionName, string name)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = section.GraphLabels.Count - 1; i >= 0; i--)
                {
                    if (string.Equals(section.GraphLabels[i].LabelName, name))
                    {
                        if (section.GraphLabels[i].Label != null)
                        {
                            zedGraphControl1.GraphPane.GraphObjList.Remove(section.GraphLabels[i].Label);
                        }
                        section.GraphLabels.RemoveAt(i);
                        _needRefreshAll = true;
                    }
                }
            }
        }
        public void RemoveLine(string sectionName, string name)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = section.GraphLines.Count - 1; i >= 0; i--)
                {
                    if (string.Equals(section.GraphLines[i].ColumnDefine.ColumnName, name))
                    {
                        if (section.GraphLines[i].LineItem != null)
                        {
                            zedGraphControl1.GraphPane.CurveList.Remove(section.GraphLines[i].LineItem);
                        }
                        section.GraphLines.RemoveAt(i);
                        _needRefreshAll = true;
                    }
                }
            }

        }
        #endregion

        #region chart methods
        public static string PointString(double x, double y)
        {
            return Math.Round(x,4).ToString() + "," + Math.Round(y,4).ToString();
        }
        public static string PointString(string name, double x , double y)
        {
            return name + ":" + Math.Round(x, 4).ToString() + "," + Math.Round(y, 4).ToString();
        }
        int CalcChartPoint(double start, double end)
        {
            double scale = zedGraphControl1.GraphPane.XAxis.Scale.Max - zedGraphControl1.GraphPane.XAxis.Scale.Min;
            int length = (int)Math.Round(zedGraphControl1.GraphPane.Chart.Rect.Width,0);
            return scale > 0 ? (int)Math.Round((Math.Max(start, end) - Math.Min(start, end)) / scale * length, 0) : length;
        }
        public void ClearBoxObj()
        {
            if (_boxObj != null && zedGraphControl1.GraphPane.GraphObjList.Contains(_boxObj))
            {
                zedGraphControl1.GraphPane.GraphObjList.Remove(_boxObj);
                _boxObj = null;
                _needRefreshAll = true;
            }

        }
        void DrawSelectBox(Point startPoint, Point endPoint, Color borderColor, Color areaColor)
        {
            ClearBoxObj();
            Color drawBorderColor = Color.FromArgb(200, borderColor.R, borderColor.G, borderColor.B);
            Color drawAreaColor = Color.FromArgb(100, areaColor.R, areaColor.G, areaColor.B);
            //double x1, x2, y1, y2;
            double x1, x2, y1, y2, minY = zedGraphControl1.GraphPane.YAxis.Scale.Min, maxY = zedGraphControl1.GraphPane.YAxis.Scale.Max;
            this.zedGraphControl1.GraphPane.ReverseTransform(new PointF(startPoint.X, startPoint.Y), out x1, out y1);
            this.zedGraphControl1.GraphPane.ReverseTransform(new PointF(endPoint.X, endPoint.Y), out x2, out y2);
            ZedGraph.BoxObj box;
            if (_graphicsSelectType == VirtualInstrument.Classes.GraphicsSelectType.SelectArea)
            {
                box = new ZedGraph.BoxObj(Math.Min(x1, x2), Math.Max(y1, y2), Math.Abs(x2 - x1), Math.Abs(y2 - y1), borderColor, drawAreaColor);
            }
            else
            {
                box = new ZedGraph.BoxObj(Math.Min(x1, x2), Math.Max(minY, maxY), Math.Abs(x2 - x1), Math.Abs(maxY - minY), borderColor, drawAreaColor);
            }
            box.Border.Width = 2f;

            this.zedGraphControl1.GraphPane.GraphObjList.Add(box);
            _boxObj = box;
            _needRefreshAll = true;

        }
                
        bool RefreshSelect()
        {
            PointF startPtF = new PointF((float)_selectStartPoint.X, (float)_selectStartPoint.Y);
            PointF endPtF = new PointF((float)_selectEndPoint.X, (float)_selectEndPoint.Y);
            double startX, startY, endX, endY, startX2, startY2, endX2, endY2;
            this.zedGraphControl1.GraphPane.ReverseTransform(startPtF, out startX, out startX2, out startY, out startY2);
            this.zedGraphControl1.GraphPane.ReverseTransform(endPtF, out endX, out endX2, out endY, out endY2);
            PointF newStartPtF = new PointF((float)Math.Min(startX, endX), (float)Math.Min(startY, endY));
            PointF newEndPtF = new PointF((float)Math.Max(startX, endX), (float)Math.Max(startY, endY));
            PointF newStartPtF2 = new PointF((float)Math.Min(startX2, endX2), (float)Math.Min(startY2, endY2));
            PointF newEndPtF2 = new PointF((float)Math.Max(startX2, endX2), (float)Math.Max(startY2, endY2));
            this._selectedCurves.Clear();
            foreach (ZedGraph.LineItem line in zedGraphControl1.GraphPane.CurveList)
            {

                if (line.IsVisible)
                {
                    if (line.Tag != null)
                    {
                        //曲线数据
                        if (Classes.GraphObjCollection.GetObjTypeFromPack(line.Tag.ToString()) == Classes.GraphObjType.Line)
                        {
                            Classes.GraphLineDefine lineDefine = new VirtualInstrument.Classes.GraphLineDefine(Classes.GraphObjCollection.GetInfoFromPack(line.Tag.ToString()));
                            Classes.GraphLine graphLine = new VirtualInstrument.Classes.GraphLine();
                            graphLine.LineDefine.Parse(lineDefine.ToString());
                            graphLine.SectionName = Classes.GraphObjCollection.GetSectionNameFromPack(line.Tag.ToString());
                            for (int i = 0; i < line.Points.Count; i++)
                            {
                                ZedGraph.PointPair p = line.Points[i];
                                if (p.X >= newStartPtF.X && p.X <= newEndPtF.X)
                                {
                                    graphLine.Add(new VirtualInstrument.Classes.GraphPointDefine(p.X, p.Y, p.Z),i);
                                }
                            }
                            if (graphLine.Count > 0)
                            {
                                _selectedCurves.Add(graphLine);
                            }
                        }
                            //手工数据
                        else if (Classes.GraphObjCollection.GetObjTypeFromPack(line.Tag.ToString()) == Classes.GraphObjType.ManualLine)
                        {
                            Classes.GraphManualLine lineDefine = new VirtualInstrument.Classes.GraphManualLine(Classes.GraphObjCollection.GetInfoFromPack(line.Tag.ToString()));
                            Classes.GraphLine graphLine = new VirtualInstrument.Classes.GraphLine();
                            graphLine.SectionName = Classes.GraphObjCollection.GetSectionNameFromPack(line.Tag.ToString());
                            graphLine.LineDefine.ColumnDefine.ColumnName = lineDefine.Name;
                            graphLine.LineDefine.ColumnDefine.ColumnCaption = lineDefine.Caption;
                            graphLine.LineDefine.IsY2Axis = false;
                            graphLine.LineDefine.LineColor = lineDefine.Color;
                            graphLine.LineDefine.LineVisible = true;
                            graphLine.LineDefine.LineWidthF = 1f;
                            graphLine.LineDefine.PointVisible = true;
                            for (int i = 0; i < lineDefine.Points.Count; i++)
                            {
                                ZedGraph.PointPair p = line.Points[i];
                                if (p.X >= newStartPtF.X && p.X <= newEndPtF.X)
                                {
                                    graphLine.Add(new VirtualInstrument.Classes.GraphPointDefine(p.X, p.Y, p.Z), i);
                                }

                            }
                            if (graphLine.Count > 0)
                            {
                                _selectedCurves.Add(graphLine);
                            }
                        }
                    }
                }
            }
            return this._selectedCurves.Count > 0;
        }

        public void RemoveCurveSelectedPoints()
        {
            foreach (Classes.GraphLine line in _selectedCurves)
            {
                foreach (ZedGraph.LineItem linItem in zedGraphControl1.GraphPane.CurveList)
                {
                    if (linItem.Tag != null)
                    {
                        if (Classes.GraphObjCollection.GetObjTypeFromPack(linItem.Tag.ToString()) == Classes.GraphObjType.Line)
                        {
                            if (Classes.GraphObjCollection.GetSectionNameFromPack(linItem.Tag.ToString())== line.SectionName && Classes.GraphObjCollection.GetObjNameFromPack( linItem.Tag.ToString() ) == line.LineDefine.ColumnDefine.ColumnName )
                            {
                                for (int i = linItem.Points.Count - 1; i >= 0; i--)
                                {
                                    if (line.Indexes.Contains(i))
                                    {
                                        linItem.RemovePoint(i);
                                    }
                                }
                            }
                        }
                        _needRefreshAll = true;
                    }
                }
                
            }
        }
        public void RemoveCurveSelectedPoints(string curveLabel, int startIndex, int endIndex)
        {
            for (int curveIndex = 0; curveIndex < zedGraphControl1.GraphPane.CurveList.Count; curveIndex++)
            {
                if (zedGraphControl1.GraphPane.CurveList[curveIndex].Tag != null && zedGraphControl1.GraphPane.CurveList[curveIndex].Label.Text == curveLabel)
                {
                    ZedGraph.LineItem l = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[curveIndex];
                    if (l.NPts > endIndex && startIndex >= 0)
                    {
                        for (int i = endIndex; i >= startIndex; i--)
                        {
                            l.RemovePoint(i);
                        }
                    }
                }
            }
        }


        #endregion

        #region Sync Methods
        public void NeedSyncAll()
        {
            _needSyncAll = true;
        }
        public void NeedSyncCurves()
        {
            _needSyncCurves = true;
        }
        public void NeedSyncAxis()
        {
            _needSyncAxis = true;
        }
        public void NeedSyncObjs()
        {
            _needSyncObjs = true;
        }

        void SyncManualLines()
        {
            SyncManualLines(_sectionTemplete.Name);
            for (int i = 0; i < _sections.Count; i++)
            {
                SyncManualLines(_sections[i].Name);
            }
        }
        void SyncFitLines()
        {
            SyncFitLines(_sectionTemplete.Name);
            for (int i = 0; i < _sections.Count; i++)
            {
                SyncFitLines(_sections[i].Name);
            }
        }
        void SyncHighLighter()
        {
            SyncHighLighter(_sectionTemplete.Name);
            for (int i = 0; i < _sections.Count; i++)
            {
                SyncHighLighter(_sections[i].Name);
            }
        }
        void SyncFitLineExtand()
        {
            SyncFitLineExtand(_sectionTemplete.Name);
            for (int i = 0; i < _sections.Count; i++)
            {
                SyncFitLineExtand(_sections[i].Name);
            }
        }
        void SyncLabels()
        {
            SyncLabels(_sectionTemplete.Name);
            for (int i = 0; i < _sections.Count; i++)
            {
                SyncLabels(_sections[i].Name);
            }
        }
        void SyncStandardCurves()
        {
            SyncStandardCurves(_sectionTemplete.Name);
            for (int i = 0; i < _sections.Count; i++)
            {
                SyncStandardCurves(_sections[i].Name);
            }

        }
        void SyncLines()
        {
            //SyncLines(_sectionTemplete.Name);
            for (int i = 0; i < _sections.Count; i++)
            {
                SyncLines(_sections[i].Name);
            }
            RemoveAloneLines();
        }
        void RemoveAloneLines()
        {
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == Classes.GraphObjType.Line)
                    {
                        string thisSectionName = Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString());
                        string lineName = Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString());
                        if (!CurveContains(thisSectionName, lineName))
                        {
                            zedGraphControl1.GraphPane.CurveList.RemoveAt(i);
                        }
                    }
                }
            }
            _needRefreshAll = true;

        }
        public void SyncAxis()
        {
            SyncXAxis();
            SyncYAxis();
            SyncY2Axis();
        }
        void SyncXAxis()
        {
            zedGraphControl1.GraphPane.XAxis.Scale.MaxAuto = false;
            zedGraphControl1.GraphPane.XAxis.Scale.MinAuto = false;
            zedGraphControl1.GraphPane.XAxis.Color = _xAxis.AxisColor;
            zedGraphControl1.GraphPane.XAxis.Title.Text = _xAxis.Caption;
            zedGraphControl1.GraphPane.XAxis.MajorGrid.Color =_xAxis.MainMarginColor;
            zedGraphControl1.GraphPane.XAxis.MinorGrid.Color = _xAxis.MinorMarginColor;
            zedGraphControl1.GraphPane.XAxis.Scale.Max = _xAxis.Maximum;
            zedGraphControl1.GraphPane.XAxis.Scale.Min = _xAxis.Minimum;
            _needRefreshAll = true;
        }
        void SyncYAxis()
        {
            zedGraphControl1.GraphPane.YAxis.Scale.MaxAuto = false;
            zedGraphControl1.GraphPane.YAxis.Scale.MinAuto = false;
            zedGraphControl1.GraphPane.YAxis.Color = _yAxis.AxisColor;
            zedGraphControl1.GraphPane.YAxis.Title.Text = _yAxis.Caption;
            zedGraphControl1.GraphPane.YAxis.MajorGrid.Color = _yAxis.MainMarginColor;
            zedGraphControl1.GraphPane.YAxis.MinorGrid.Color = _yAxis.MinorMarginColor;
            zedGraphControl1.GraphPane.YAxis.Scale.Max = _yAxis.Maximum;
            zedGraphControl1.GraphPane.YAxis.Scale.Min = _yAxis.Minimum;
            zedGraphControl1.GraphPane.YAxis.IsVisible = _yAxis.Visible;
            _needRefreshAll = true;
        }
        void SyncY2Axis()
        {
            zedGraphControl1.GraphPane.Y2Axis.Scale.MaxAuto = false;
            zedGraphControl1.GraphPane.Y2Axis.Scale.MinAuto = false;
            zedGraphControl1.GraphPane.Y2Axis.Color = _y2Axis.AxisColor;
            zedGraphControl1.GraphPane.Y2Axis.Title.Text = _y2Axis.Caption;
            zedGraphControl1.GraphPane.Y2Axis.MajorGrid.Color = _y2Axis.MainMarginColor;
            zedGraphControl1.GraphPane.Y2Axis.MinorGrid.Color = _y2Axis.MinorMarginColor;
            zedGraphControl1.GraphPane.Y2Axis.Scale.Max = _y2Axis.Maximum;
            zedGraphControl1.GraphPane.Y2Axis.Scale.Min = _y2Axis.Minimum;
            zedGraphControl1.GraphPane.Y2Axis.IsVisible = _y2Axis.Visible;
            _needRefreshAll = true;
        }
        void SyncVisible()
        {
            for (int i = 0; i < _sections.Count; i++)
            {
                _sections[i].SyncVisible = _sections[i].Visible;
            }
            _needRefreshAll = true;
        }
        void SyncAllObjs()
        {
            SyncManualLines();
            SyncStandardCurves();
            SyncLabels();
            SyncFitLines();
            SyncFitLineExtand();
            SyncHighLighter();
            SyncLines();
        }
        void SyncManualLines(string sectionName)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.ManualLines.Count; i++)
                {
                    Classes.GraphManualLine lineDefine = section.ManualLines[i];
                    if (lineDefine.LineItem == null)
                    {
                        zedGraphControl1.GraphPane.CurveList.Add(lineDefine.CreateLineItem(section.Name));
                    }
                }
            }
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == Classes.GraphObjType.ManualLine)
                    {
                        string thisName = Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString());

                        string lineName = Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString());
                        if (!GManualLineContains(thisName, lineName))
                        {
                            zedGraphControl1.GraphPane.CurveList.RemoveAt(i);
                        }
                    }
                }
            }
            _needRefreshAll = true;

        }
        /// <summary>
        /// 实际是否包含
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="lineName"></param>
        /// <returns></returns>
        bool GManualLineContains(string sectionName, string lineName)
        {
            bool contains = false;
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == VirtualInstrument.Classes.GraphObjType.ManualLine)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString()) == lineName)
                        {
                            contains = true;
                            break;
                        }
                    }
                }
            }
            return contains;
        }

        void SyncHighLighter(string sectionName)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.HighLighters.Count; i++)
                {
                    Classes.GraphHighLighterDefine lineDefine = section.HighLighters[i];
                    if (lineDefine.LineItem == null)
                    {
                        zedGraphControl1.GraphPane.CurveList.Add(lineDefine.CreateLineItem(section.Name));
                    }
                }
            }
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == Classes.GraphObjType.HighLight)
                    {
                        string thisName = Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString());

                        string lineName = Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString());
                        if (!HighLighterContains(thisName, lineName))
                        {
                            zedGraphControl1.GraphPane.CurveList.RemoveAt(i);
                        }
                    }
                }
            }
            _needRefreshAll = true;

        }
        /// <summary>
        /// 实际是否包含
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="lineName"></param>
        /// <returns></returns>
        bool GHighLighterContains(string sectionName, string lineName)
        {
            bool contains = false;
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == VirtualInstrument.Classes.GraphObjType.HighLight)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString()) == lineName)
                        {
                            contains = true;
                            break;
                        }
                    }
                }
            }
            return contains;
        }
        /// <summary>
        /// 定义是否包含
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="lineName"></param>
        /// <returns></returns>
        bool HighLighterContains(string sectionName, string lineName)
        {
            bool contains = false;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.HighLighters.Count; i++)
                {
                    if (string.Compare(lineName, section.HighLighters[i].Name) == 0)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }

        void SyncFitLineExtand(string sectionName)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.FitLineExtands.Count; i++)
                {
                    Classes.StandardCurveDefine lineDefine = section.FitLineExtands[i];
                    if (lineDefine.LineItem == null)
                    {
                        zedGraphControl1.GraphPane.CurveList.Add(lineDefine.CreateLineItem(section.Name));
                        lineDefine.Fill(_xAxis.Minimum,_xAxis.Maximum,XCount);
                    }
                }
            }
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == Classes.GraphObjType.FitLineExtand)
                    {
                        string thisName = Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString());

                        string lineName = Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString());
                        if (!FitLineExtandContains(thisName, lineName))
                        {
                            zedGraphControl1.GraphPane.CurveList.RemoveAt(i);
                        }
                    }
                }
            }
            _needRefreshAll = true;

        }
        /// <summary>
        /// 实际是否包含
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="lineName"></param>
        /// <returns></returns>
        bool GFitLineExtandContains(string sectionName, string lineName)
        {
            bool contains = false;
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == VirtualInstrument.Classes.GraphObjType.FitLineExtand)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString()) == lineName)
                        {
                            contains = true;
                            break;
                        }
                    }
                }
            }
            return contains;
        }
        /// <summary>
        /// 定义是否包含
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="lineName"></param>
        /// <returns></returns>
        bool FitLineExtandContains(string sectionName, string lineName)
        {
            bool contains = false;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.FitLineExtands.Count; i++)
                {
                    if (string.Compare(lineName, section.FitLineExtands[i].Name) == 0)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }
        void SyncFitLines(string sectionName)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.FitLines.Count; i++)
                {
                    Classes.GraphFitLineDefine lineDefine = section.FitLines[i];
                    if (lineDefine.LineItem == null)
                    {
                        zedGraphControl1.GraphPane.CurveList.Add(lineDefine.CreateLineItem(section.Name));
                        lineDefine.Fill(CalcChartPoint(lineDefine.StartPosition, lineDefine.EndPosition));
                    }
                }
            }
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == Classes.GraphObjType.FitLine)
                    {
                        string thisName = Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString());

                        string lineName = Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString());

                        if (!FitLineContains(thisName, lineName))
                        {
                            zedGraphControl1.GraphPane.CurveList.RemoveAt(i);
                        }
                    }
                }
            }
            _needRefreshAll = true;

        }
        /// <summary>
        /// 判断实际拟合线里面有没有
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="lineName"></param>
        /// <returns></returns>
        bool GFitLineContains(string sectionName, string lineName)
        {
            bool contains = false;
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == VirtualInstrument.Classes.GraphObjType.FitLine)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString()) == lineName)
                        {
                            contains = true;
                            break;
                        }
                    }
                }
            }
            return contains;
        }
        /// <summary>
        /// 判断拟合线定义里面有没有
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="lineName"></param>
        /// <returns></returns>
        bool FitLineContains(string sectionName, string lineName)
        {
            bool contains = false;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.FitLines.Count; i++)
                {
                    Classes.GraphFitLineDefine lineDefine = section.FitLines[i];
                    if (string.Compare(lineName, lineDefine.CurveDefine.Name) == 0)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }
        /// <summary>
        /// 同步标准曲线
        /// </summary>
        /// <param name="sectionName"></param>
        void SyncStandardCurves(string sectionName)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.StandardCurves.Count; i++)
                {
                    Classes.StandardCurveDefine lineDefine = section.StandardCurves[i];
                    if (lineDefine.LineItem == null)
                    {
                        zedGraphControl1.GraphPane.CurveList.Add(lineDefine.CreateLineItem(section.Name));
                        lineDefine.Fill(_xAxis.Minimum, _xAxis.Maximum, XCount);
                    }
                }
            }
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == Classes.GraphObjType.StandardCurve)
                    {
                        string thisName = Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString());
                        string lineName = Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString());

                        if (!StandardCurveContains(thisName, lineName))
                        {
                            zedGraphControl1.GraphPane.CurveList.RemoveAt(i);
                        }
                    }
                }
            }
            _needRefreshAll = true;

        }
        /// <summary>
        /// 实际曲线中是否包含标准曲线定义
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="lineName"></param>
        /// <returns></returns>
        bool GStandardCurveContains(string sectionName, string lineName)
        {
            bool contains = false;
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == VirtualInstrument.Classes.GraphObjType.StandardCurve)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString()) == lineName)
                        {
                            contains = true;
                            break;
                        }
                    }
                }
            }
            return contains;
        }
        /// <summary>
        /// 标准曲线定义是否包含
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="lineName"></param>
        /// <returns></returns>
        bool StandardCurveContains(string sectionName, string lineName)
        {
            bool contains = false;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.StandardCurves.Count; i++)
                {
                    Classes.StandardCurveDefine lineDefine = section.StandardCurves[i];
                    if (string.Compare(lineName, lineDefine.Name) == 0)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }
        /// <summary>
        /// 同步某次采样的标签
        /// </summary>
        /// <param name="sectionName"></param>
        void SyncLabels(string sectionName)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.GraphLabels.Count; i++)
                {
                    Classes.GraphLabelDefine labelDefine = section.GraphLabels[i];
                    if (!GLabelDefineContains(sectionName, labelDefine.LabelName))
                    {
                        GAddLabel(section.Name, labelDefine);
                    }
                }
            }
            //for (int i = zedGraphControl1.GraphPane.GraphObjList.Count - 1; i >= 0; i--)
            //{
            //    ZedGraph.TextObj textObj = (ZedGraph.TextObj)zedGraphControl1.GraphPane.GraphObjList[i];
            //    if (textObj.Tag != null)
            //    {
            //        if (Classes.GraphObjCollection.GetObjTypeFromPack(textObj.Tag.ToString()) == Classes.GraphObjType.Label)
            //        {
            //            string thisName = Classes.GraphObjCollection.GetSectionNameFromPack(textObj.Tag.ToString());
            //            string lineName = Classes.GraphObjCollection.GetObjNameFromPack(textObj.Tag.ToString());

            //            if (!LabelDefineContains(thisName, lineName))
            //            {
            //                zedGraphControl1.GraphPane.GraphObjList.RemoveAt(i);
            //            }
            //        }
            //    }
            //}
            _needRefreshAll = true;
        }
        /// <summary>
        /// 判断标签定义在实际的标签里面有没有
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
        bool GLabelDefineContains(string sectionName, string labelName)
        {
            bool contains = false;
            for (int i = zedGraphControl1.GraphPane.GraphObjList.Count - 1; i >= 0; i--)
            {
                if (zedGraphControl1.GraphPane.GraphObjList[i].Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(zedGraphControl1.GraphPane.GraphObjList[i].Tag.ToString()) == VirtualInstrument.Classes.GraphObjType.Label)
                    {
                        ZedGraph.TextObj textObj = (ZedGraph.TextObj)zedGraphControl1.GraphPane.GraphObjList[i];
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(textObj.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(textObj.Tag.ToString()) == labelName)
                        {
                            contains = true;
                            break;
                        }
                    }
                }
            }
            return contains;
        }
        /// <summary>
        /// 判断实际的标签在标签定义里面有没有
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
        bool LabelDefineContains(string sectionName, string labelName)
        {
            bool contains = false;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.GraphLabels.Count; i++)
                {
                    Classes.GraphLabelDefine labelDefine = section.GraphLabels[i];
                    if (string.Compare(labelName, labelDefine.LabelName) == 0)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }

        /// <summary>
        /// 将某次采样数据的曲线同步
        /// </summary>
        /// <param name="sectionName"></param>
        void SyncLines(string sectionName)
        {
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.GraphLines.Count; i++)
                {
                    Classes.GraphLineDefine lineDefine = section.GraphLines[i];
                    if (lineDefine.LineItem == null)
                    {
                        zedGraphControl1.GraphPane.CurveList.Add(lineDefine.CreateLineItem(sectionName));
                    }
                }
            }
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == Classes.GraphObjType.Line)
                    {
                        string thisSectionName = Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString());
                        string lineName = Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString());
                        if (!CurveContains(thisSectionName, lineName))
                        {
                            zedGraphControl1.GraphPane.CurveList.RemoveAt(i);
                        }
                    }
                }
            }
            _needRefreshAll = true;
        }
        //void SyncLine(string sectionName, string columnName)
        //{
        //    if (!GCurveContains(sectionName, columnName))
        //    {
        //        Classes.GraphLineDefine lineDefine = DGetLineDefine(sectionName, columnName);
        //        GAddCurve(sectionName, lineDefine);
        //    }
        //}
        /// <summary>
        /// 判断实际曲线中是否包含
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        bool GCurveContains(string sectionName, string columnName)
        {
            bool contains = false;
            for (int i = zedGraphControl1.GraphPane.CurveList.Count - 1; i >= 0; i--)
            {
                ZedGraph.LineItem lineItem = (ZedGraph.LineItem)zedGraphControl1.GraphPane.CurveList[i];
                if (lineItem.Tag != null)
                {
                    if (Classes.GraphObjCollection.GetObjTypeFromPack(lineItem.Tag.ToString()) == VirtualInstrument.Classes.GraphObjType.Line)
                    {
                        if (Classes.GraphObjCollection.GetSectionNameFromPack(lineItem.Tag.ToString()) == sectionName && Classes.GraphObjCollection.GetObjNameFromPack(lineItem.Tag.ToString()) == columnName)
                        {
                            contains = true;
                            break;
                        }
                    }
                }
            }
            return contains;
        }
        /// <summary>
        /// 判断曲线定义中是否包含
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        bool CurveContains(string sectionName, string columnName)
        {
            bool contains = false;
            Classes.GraphObjCollection section = GetSectionByName(sectionName);
            if (section != null)
            {
                for (int i = 0; i < section.GraphLines.Count; i++)
                {
                    Classes.GraphLineDefine lineDefine = section.GraphLines[i];
                    if (string.Compare(columnName, lineDefine.ColumnDefine.ColumnName) == 0)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }
        void SyncAll()
        {
            _needSyncObjs = true;
            _needSyncCurves = true;
            _needSyncAxis = true;
        }
        public void RefreshAll()
        {
            _needRefreshAll = true;
        }
        #endregion

        #region Mouse Event
        bool zedGraphControl1_MouseUpEvent(ZedGraph.ZedGraphControl sender, MouseEventArgs e)
        {
            bool returnValue = false;
            if (e.Button == MouseButtons.Left)//左键的操作
            {
                if (_selecting)//
                {
                    _selecting = false;
                    switch (_graphicsSelectType)
                    {
                        case Classes.GraphicsSelectType.SelectLabel:
                            _movingObj = null;
                            returnValue = true;
                            break;
                        default:
                        case Classes.GraphicsSelectType.SelectCurve:
                        case Classes.GraphicsSelectType.None:
                            returnValue = false;
                            break;
                        case VirtualInstrument.Classes.GraphicsSelectType.HighLighter:
                            if (_currentHighLighter != null)
                            {
                                _currentHighLighter = null;
                                returnValue = true;
                            }
                            break;
                        case Classes.GraphicsSelectType.SelectArea:
                            _selectEndPoint = e.Location;
                            if (Math.Abs(_selectEndPoint.X - _selectStartPoint.X) > 5 && Math.Abs(_selectStartPoint.Y - _selectEndPoint.Y) > 5)
                            {
                                DrawSelectBox(_selectStartPoint, _selectEndPoint, Classes.GraphicsSelectColorDefine.SelectedBorderColor(Classes.GraphicsSelectType.SelectArea), Classes.GraphicsSelectColorDefine.SelectedAreaColor(Classes.GraphicsSelectType.SelectArea));
                                ZoomToSelectedArea();
                                ClearBoxObj();
                                returnValue = true;
                            }
                            break;
                        case Classes.GraphicsSelectType.SelectData:
                            _selectEndPoint = e.Location;
                            DrawSelectBox(_selectStartPoint, _selectEndPoint, Classes.GraphicsSelectColorDefine.SelectedBorderColor(Classes.GraphicsSelectType.SelectData), Classes.GraphicsSelectColorDefine.SelectedAreaColor(Classes.GraphicsSelectType.SelectData));
                            if (Math.Abs(_selectEndPoint.X - _selectStartPoint.X) > 5)
                            {
                                if (RefreshSelect())
                                {
                                    ClearBoxObj();
                                    _needRefreshAll = true;
                                    Classes.GraphMouseActionEventArgs a = new VirtualInstrument.Classes.GraphMouseActionEventArgs(VirtualInstrument.Classes.GraphicsSelectType.SelectData, _selectedCurves);
                                    OnMouseEvent(a);
                                }
                            }
                            returnValue = true;
                            break;
                        case Classes.GraphicsSelectType.SelectPoint:
                            _selectEndPoint = e.Location;
                            DrawSelectBox(_selectStartPoint, _selectEndPoint, Classes.GraphicsSelectColorDefine.SelectedBorderColor(Classes.GraphicsSelectType.SelectPoint), Classes.GraphicsSelectColorDefine.SelectedAreaColor(Classes.GraphicsSelectType.SelectPoint));
                            if (Math.Abs(_selectEndPoint.X - _selectStartPoint.X) > 5)
                            {
                                if (RefreshSelect())
                                {
                                    ClearBoxObj();
                                    _needRefreshAll = true;
                                    RemoveCurveSelectedPoints();
                                }
                            }
                            returnValue = true;
                            break;
                    }

                }
            }
            return returnValue;
        }

        bool zedGraphControl1_MouseMoveEvent(ZedGraph.ZedGraphControl sender, MouseEventArgs e)
        {
            bool returnValue = false;
            if (e.Button == MouseButtons.Left)
            {
                if (_selecting)//已经按下左键，正在选择
                {
                    switch (_graphicsSelectType)
                    {
                        case Classes.GraphicsSelectType.SelectLabel:
                            double labelX, labelY;
                            this.zedGraphControl1.GraphPane.ReverseTransform(new PointF(e.Location.X, e.Location.Y), out labelX, out labelY);
                            if (_movingObj != null)
                            {
                                _movingObj.Location = new ZedGraph.Location(_movingObj.Location.X + (labelX - _movingX), _movingObj.Location.Y + (labelY - _movingY),ZedGraph. CoordType.AxisXYScale);
                                _movingX = labelX;
                                _movingY = labelY;
                                //zedGraphControl1.AxisChange();
                                //zedGraphControl1.Invalidate();
                                for (int i = 0; i < _sectionTemplete.GraphLabels.Count; i++)
                                {
                                    Classes.GraphLabelDefine label = _sectionTemplete.GraphLabels[i];
                                    if (label.Label != null)
                                    {
                                        if (object.Equals(label.Label, _movingObj))
                                        {
                                            label.Position.X = _movingObj.Location.X;
                                            label.Position.Y = _movingObj.Location.Y;
                                        }
                                    }
                                }
                                foreach (Classes.GraphObjCollection section in _sections)
                                {
                                    for (int i = 0; i < section.GraphLabels.Count; i++)
                                    {
                                        Classes.GraphLabelDefine label = section.GraphLabels[i];
                                        if (label.Label != null)
                                        {
                                            if (object.Equals(label.Label, _movingObj))
                                            {
                                                label.Position.X = _movingObj.Location.X;
                                                label.Position.Y = _movingObj.Location.Y;
                                            }
                                        }
                                    }
                                }
                                _needRefreshAll = true;
                            }
                            returnValue = true;
                            break;
                        default:
                        case Classes.GraphicsSelectType.SelectCurve:
                        case Classes.GraphicsSelectType.None:
                            returnValue = false;
                            break;
                        case VirtualInstrument.Classes.GraphicsSelectType.HighLighter:
                            if (_currentHighLighter != null)
                            {
                                double highLighterX, highLighterY;
                                this.zedGraphControl1.GraphPane.ReverseTransform(new PointF(e.Location.X, e.Location.Y), out highLighterX, out highLighterY);
                                _currentHighLighter.AddPoint(highLighterX, highLighterY);
                                _needRefreshAll = true;
                                returnValue = true;
                            }
                            
                            break;

                        case Classes.GraphicsSelectType.SelectArea:
                            _selectEndPoint = e.Location;
                            DrawSelectBox(_selectStartPoint, _selectEndPoint, Classes.GraphicsSelectColorDefine.SelectedBorderColor(Classes.GraphicsSelectType.SelectArea), Classes.GraphicsSelectColorDefine.SelectedAreaColor(Classes.GraphicsSelectType.SelectArea));
                            returnValue = true;
                            break;
                        case Classes.GraphicsSelectType.SelectData:
                            _selectEndPoint = e.Location;
                            DrawSelectBox(_selectStartPoint, _selectEndPoint, Classes.GraphicsSelectColorDefine.SelectedBorderColor(Classes.GraphicsSelectType.SelectData), Classes.GraphicsSelectColorDefine.SelectedAreaColor(Classes.GraphicsSelectType.SelectData));
                            returnValue = true;
                            break;
                        case Classes.GraphicsSelectType.SelectPoint:
                            _selectEndPoint = e.Location;
                            DrawSelectBox(_selectStartPoint, _selectEndPoint, Classes.GraphicsSelectColorDefine.SelectedBorderColor(Classes.GraphicsSelectType.SelectPoint), Classes.GraphicsSelectColorDefine.SelectedAreaColor(Classes.GraphicsSelectType.SelectPoint));
                            returnValue = true;
                            break;
                    }
                }
            }
            return returnValue;
        }

        bool zedGraphControl1_MouseDownEvent(ZedGraph.ZedGraphControl sender, MouseEventArgs e)
        {
            bool returnValue = false;
            if (e.Button == MouseButtons.Left)//左键有效
            {
                int index = -1;
                switch (_graphicsSelectType)
                {
                    default:
                    case Classes.GraphicsSelectType.None:
                        returnValue = false;
                        break;
                    case Classes.GraphicsSelectType.SelectArea:
                    case Classes.GraphicsSelectType.SelectData:
                        //ClearBoxObj();
                        _selecting = true;
                        _selectStartPoint = e.Location;
                        _selectEndPoint = e.Location;
                        returnValue = true;
                        break;
                    case VirtualInstrument.Classes.GraphicsSelectType.HighLighter:
                        ClearBoxObj();
                        _selecting = true;
                        _currentHighLighter = AddHighLighter(CurrentSection.Name, _highLighterColor);
                        double highLighterX, highLighterY;
                        this.zedGraphControl1.GraphPane.ReverseTransform(new PointF(e.Location.X, e.Location.Y), out highLighterX, out highLighterY);
                        _currentHighLighter.AddPoint(highLighterX, highLighterY);
                        returnValue = true;
                        break;

                    case Classes.GraphicsSelectType.SelectLabel:
                        ClearBoxObj();
                        index = -1;
                        if (this.zedGraphControl1.GraphPane.GraphObjList.FindPoint(new PointF(e.Location.X, e.Location.Y), this.zedGraphControl1.GraphPane, this.zedGraphControl1.CreateGraphics(), 1f, out index))
                        {
                            if (index >= 0 && index < zedGraphControl1.GraphPane.GraphObjList.Count)
                            {
                                if (zedGraphControl1.GraphPane.GraphObjList[index].Tag != null && Classes.GraphObjCollection.GetObjTypeFromPack(zedGraphControl1.GraphPane.GraphObjList[index].Tag.ToString()) == VirtualInstrument.Classes.GraphObjType.Label)
                                {
                                    _selecting = true;
                                    _movingObj = zedGraphControl1.GraphPane.GraphObjList[index];
                                    double labelX, labelY;
                                    this.zedGraphControl1.GraphPane.ReverseTransform(new PointF(e.Location.X, e.Location.Y), out labelX, out labelY);

                                    _movingX = labelX;
                                    _movingY = labelY;
                                    _selectStartPoint = e.Location;
                                    _selectEndPoint = e.Location;
                                }
                            }
                        }
                        returnValue = true;
                        break;
                    case Classes.GraphicsSelectType.SelectCurve:
                        returnValue = true;
                        break;
                    case Classes.GraphicsSelectType.SelectPoint:
                        ClearBoxObj();
                        _selecting = true;
                        _selectStartPoint = e.Location;
                        _selectEndPoint = e.Location;
                        returnValue = true;
                        break;
                }
            }
            else if( e.Button == MouseButtons.Right )
            {
                ZoomOut();
                //if (_isShowAll)
                //{
                //    ZoomAuto();
                //}
                //else
                //{
                //    ZoomBack();
                //}
                //_isShowAll = !_isShowAll;
                returnValue = true;
            }
            return returnValue;
        }

        public event Classes.GraphMouseActionHandler MouseEvent = null;
        protected void OnMouseEvent(Classes.GraphMouseActionEventArgs e)
        {
            if (MouseEvent != null)
            {
                MouseEvent(this, e);
            }
        }
        #endregion

        #region Replay Methods
        public void MaskReplaySections(List< string > sectionNames)
        {
            for (int i = 0; i < _sections.Count; i++)
            {
                if (_sections[i].Visible)
                {
                    if (!sectionNames.Contains(_sections[i].Name))
                    {
                        _sections[i].Visible = false;
                    }
                }
            }
            _needSyncVisible = true;
        }
        public void ClearLinePoints(List<string> sectionNames)
        {
            for (int i = 0; i < _sections.Count; i++)
            {
                if (_sections[i].Visible)
                {
                    if (sectionNames.Contains(_sections[i].Name))
                    {
                        _sections[i].ClearPoint();
                    }
                }
            }
            _needRefreshAll = true;
        }
        #endregion

        public void NeedResumeLineWidth()
        {
            _needResumeLineWidth = true;
        }
        public void NeedSetLastSectionLineWidth()
        {
            _needSetCurrentLineWidth = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needSyncAll)
            {
                _needSyncAll = false;
                SyncAll();
                //_needRefreshAll = true;
            }
            if (_needZoomAuto)
            {
                _needZoomAuto = false;
                ZoomAuto();
            }
            if (_needSyncAxis)
            {
                _needSyncAxis = false;
                SyncXAxis();
                SyncY2Axis();
                SyncYAxis();
            }
            if (_needSyncCurves)
            {
                _needSyncCurves = false;
                SyncLines();
            }
            if (_needSyncObjs)
            {
                _needSyncObjs = false;
                SyncLabels();
                SyncStandardCurves();
                SyncFitLines();
                SyncFitLineExtand();
            }
            if (_needResumeLineWidth)
            {
                _needResumeLineWidth = false;
                ResumeLineWidth();
            }
            if (_needSetCurrentLineWidth)
            {
                _needSetCurrentLineWidth = false;
                SetLastSectionWidth();
            }
            if (_needRefreshAll)
            {
                _needRefreshAll = false;
                zedGraphControl1.GraphPane.AxisChange();
                zedGraphControl1.Invalidate();
            }
            if (_needSyncVisible)
            {
                _needSyncVisible = false;
                SyncVisible();
            }

        }
        #endregion

        #region Serialize
        string Sections2Str()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for( int i = 0 ; i < _sections.Count ; i ++ )
            {
                keyValue.Add(i.ToString(), _sections[i].ToString());
            }
            return keyValue.ToString();
        }
        void SectionsParse(string value )
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse( value );
            _sections.Clear();
            for( int i = 0 ; i < keyValue.Count ; i ++ )
            {
                _sections.Add(new Classes.GraphObjCollection(keyValue.GetValueByKey(i.ToString())));
            }
            SyncLines();
        }
        public override string  ToString()
        {
 	        KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Name",_graphName );
            keyValue.Add("Caption",GraphCaption );
            keyValue.Add("SmoothTension", _smoothTension.ToString());
            keyValue.Add("IsSmooth", _isSmooth.ToString());
            keyValue.Add("XAxis",_xAxis.ToString() );
            keyValue.Add("YAxis",_yAxis.ToString() );
            keyValue.Add("Y2Axis",_y2Axis.ToString() );
            keyValue.Add( "Sections",Sections2Str() );
            keyValue.Add("SectionTemplete",_sectionTemplete.ToString() );
            keyValue.Add("LockYAxis", _lockYAxis.ToString());
            keyValue.Add("LockY2Axis", _lockY2Axis.ToString());
            keyValue.Add("LockXAxis", _lockXAxis.ToString());
            return keyValue.ToString();

        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _graphName = keyValue.GetValueByKey("Name");
            GraphCaption = keyValue.GetValueByKey("Caption");
            float.TryParse(keyValue.GetValueByKey("SmoothTension"), out _smoothTension);
            bool.TryParse(keyValue.GetValueByKey("IsSmooth"), out _isSmooth);
            _xAxis.Parse(keyValue.GetValueByKey("XAxis"));
            _yAxis.Parse(keyValue.GetValueByKey("YAxis"));
            _y2Axis.Parse(keyValue.GetValueByKey("Y2Axis"));
            _sectionTemplete.Parse(keyValue.GetValueByKey("SectionTemplete"));
            bool.TryParse(keyValue.GetValueByKey("LockXAxis"), out _lockXAxis);
            bool.TryParse(keyValue.GetValueByKey("LockYAxis"), out _lockYAxis);
            bool.TryParse(keyValue.GetValueByKey("LockY2Axis"), out _lockY2Axis);
            SectionsParse(keyValue.GetValueByKey("Sections"));

            //for (int i = 0; i < _sections.Count; i++)
            //{
            //    Classes.GraphObjCollection section = _sections[i];
            //    section.GraphLines.Clear();
            //    foreach (Classes.GraphLineDefine line in _sectionTemplete.GraphLines)
            //    {
            //        Classes.GraphLineDefine newLine = new VirtualInstrument.Classes.GraphLineDefine(line.ToString());
            //        newLine.LineColor = Classes.PublicMethods.GetNewColor(line.LineColor);
            //        section.GraphLines.Add(newLine);
            //    }
            //}
            //SyncLines();
        }
        #endregion
    }
}
