namespace WCYDisLab.Graph
{
    partial class FormGraph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGraph));
            VirtualInstrument.Classes.GraphAxisDefine graphAxisDefine1 = new VirtualInstrument.Classes.GraphAxisDefine();
            VirtualInstrument.Classes.GraphAxisDefine graphAxisDefine2 = new VirtualInstrument.Classes.GraphAxisDefine();
            VirtualInstrument.Classes.GraphAxisDefine graphAxisDefine3 = new VirtualInstrument.Classes.GraphAxisDefine();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_props = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_lineAndAxis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_times = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_manager = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_label = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_manualCurve = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_standardCurve = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_clear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_operat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_mouse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_move = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_analysis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_zoom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_pen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_moveLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_bandPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_lock = new System.Windows.Forms.ToolStripMenuItem();
            this.graph1 = new VirtualInstrument.Graph();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_move = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_analysis = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_zoom = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_selectLabel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_selectBandPoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_pen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_lock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_showAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_photo = new System.Windows.Forms.ToolStripButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_props,
            this.toolStripMenuItem_manager,
            this.toolStripMenuItem_operat});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // toolStripMenuItem_props
            // 
            this.toolStripMenuItem_props.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_lineAndAxis,
            this.toolStripMenuItem_times});
            this.toolStripMenuItem_props.Name = "toolStripMenuItem_props";
            resources.ApplyResources(this.toolStripMenuItem_props, "toolStripMenuItem_props");
            // 
            // toolStripMenuItem_lineAndAxis
            // 
            this.toolStripMenuItem_lineAndAxis.Name = "toolStripMenuItem_lineAndAxis";
            resources.ApplyResources(this.toolStripMenuItem_lineAndAxis, "toolStripMenuItem_lineAndAxis");
            this.toolStripMenuItem_lineAndAxis.Click += new System.EventHandler(this.toolStripMenuItem_lineAndAxis_Click);
            // 
            // toolStripMenuItem_times
            // 
            this.toolStripMenuItem_times.Name = "toolStripMenuItem_times";
            resources.ApplyResources(this.toolStripMenuItem_times, "toolStripMenuItem_times");
            this.toolStripMenuItem_times.Click += new System.EventHandler(this.toolStripMenuItem_times_Click);
            // 
            // toolStripMenuItem_manager
            // 
            this.toolStripMenuItem_manager.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_label,
            this.toolStripMenuItem_manualCurve,
            this.toolStripMenuItem_standardCurve,
            this.toolStripSeparator1,
            this.toolStripMenuItem_clear});
            this.toolStripMenuItem_manager.Name = "toolStripMenuItem_manager";
            resources.ApplyResources(this.toolStripMenuItem_manager, "toolStripMenuItem_manager");
            // 
            // toolStripMenuItem_label
            // 
            this.toolStripMenuItem_label.Name = "toolStripMenuItem_label";
            resources.ApplyResources(this.toolStripMenuItem_label, "toolStripMenuItem_label");
            this.toolStripMenuItem_label.Click += new System.EventHandler(this.toolStripMenuItem_label_Click);
            // 
            // toolStripMenuItem_manualCurve
            // 
            this.toolStripMenuItem_manualCurve.Name = "toolStripMenuItem_manualCurve";
            resources.ApplyResources(this.toolStripMenuItem_manualCurve, "toolStripMenuItem_manualCurve");
            this.toolStripMenuItem_manualCurve.Click += new System.EventHandler(this.toolStripMenuItem_manualCurve_Click);
            // 
            // toolStripMenuItem_standardCurve
            // 
            this.toolStripMenuItem_standardCurve.Name = "toolStripMenuItem_standardCurve";
            resources.ApplyResources(this.toolStripMenuItem_standardCurve, "toolStripMenuItem_standardCurve");
            this.toolStripMenuItem_standardCurve.Click += new System.EventHandler(this.toolStripMenuItem_standardCurve_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItem_clear
            // 
            this.toolStripMenuItem_clear.Name = "toolStripMenuItem_clear";
            resources.ApplyResources(this.toolStripMenuItem_clear, "toolStripMenuItem_clear");
            this.toolStripMenuItem_clear.Click += new System.EventHandler(this.toolStripMenuItem_clear_Click);
            // 
            // toolStripMenuItem_operat
            // 
            this.toolStripMenuItem_operat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_mouse,
            this.toolStripMenuItem_lock});
            this.toolStripMenuItem_operat.Name = "toolStripMenuItem_operat";
            resources.ApplyResources(this.toolStripMenuItem_operat, "toolStripMenuItem_operat");
            // 
            // toolStripMenuItem_mouse
            // 
            this.toolStripMenuItem_mouse.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_move,
            this.toolStripMenuItem_analysis,
            this.toolStripMenuItem_zoom,
            this.toolStripMenuItem_pen,
            this.toolStripMenuItem_moveLabel,
            this.toolStripMenuItem_bandPoint});
            this.toolStripMenuItem_mouse.Name = "toolStripMenuItem_mouse";
            resources.ApplyResources(this.toolStripMenuItem_mouse, "toolStripMenuItem_mouse");
            // 
            // toolStripMenuItem_move
            // 
            this.toolStripMenuItem_move.Name = "toolStripMenuItem_move";
            resources.ApplyResources(this.toolStripMenuItem_move, "toolStripMenuItem_move");
            this.toolStripMenuItem_move.Click += new System.EventHandler(this.toolStripMenuItem_move_Click);
            // 
            // toolStripMenuItem_analysis
            // 
            this.toolStripMenuItem_analysis.Name = "toolStripMenuItem_analysis";
            resources.ApplyResources(this.toolStripMenuItem_analysis, "toolStripMenuItem_analysis");
            this.toolStripMenuItem_analysis.Click += new System.EventHandler(this.toolStripMenuItem_analysis_Click);
            // 
            // toolStripMenuItem_zoom
            // 
            this.toolStripMenuItem_zoom.Name = "toolStripMenuItem_zoom";
            resources.ApplyResources(this.toolStripMenuItem_zoom, "toolStripMenuItem_zoom");
            this.toolStripMenuItem_zoom.Click += new System.EventHandler(this.toolStripMenuItem_zoom_Click);
            // 
            // toolStripMenuItem_pen
            // 
            this.toolStripMenuItem_pen.Name = "toolStripMenuItem_pen";
            resources.ApplyResources(this.toolStripMenuItem_pen, "toolStripMenuItem_pen");
            this.toolStripMenuItem_pen.Click += new System.EventHandler(this.toolStripMenuItem_pen_Click);
            // 
            // toolStripMenuItem_moveLabel
            // 
            this.toolStripMenuItem_moveLabel.Name = "toolStripMenuItem_moveLabel";
            resources.ApplyResources(this.toolStripMenuItem_moveLabel, "toolStripMenuItem_moveLabel");
            this.toolStripMenuItem_moveLabel.Click += new System.EventHandler(this.toolStripMenuItem_moveLabel_Click);
            // 
            // toolStripMenuItem_bandPoint
            // 
            this.toolStripMenuItem_bandPoint.Name = "toolStripMenuItem_bandPoint";
            resources.ApplyResources(this.toolStripMenuItem_bandPoint, "toolStripMenuItem_bandPoint");
            this.toolStripMenuItem_bandPoint.Click += new System.EventHandler(this.toolStripMenuItem_bandPoint_Click);
            // 
            // toolStripMenuItem_lock
            // 
            this.toolStripMenuItem_lock.Name = "toolStripMenuItem_lock";
            resources.ApplyResources(this.toolStripMenuItem_lock, "toolStripMenuItem_lock");
            this.toolStripMenuItem_lock.Click += new System.EventHandler(this.toolStripMenuItem_lock_Click);
            // 
            // graph1
            // 
            this.graph1.DefaultFittingMethodName = "";
            resources.ApplyResources(this.graph1, "graph1");
            this.graph1.GraphCaption = "";
            this.graph1.GraphCrossAuto = true;
            this.graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.None;
            this.graph1.GraphName = "";
            this.graph1.HighLighterColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.graph1.IsSmooth = false;
            this.graph1.LockXAxis = false;
            this.graph1.LockY2Axis = false;
            this.graph1.LockYAxis = false;
            this.graph1.Method_GetValue = null;
            this.graph1.Name = "graph1";
            this.graph1.SmoothTension = 1F;
            graphAxisDefine1.AutoScale = true;
            graphAxisDefine1.AxisColor = System.Drawing.SystemColors.ActiveCaption;
            graphAxisDefine1.Caption = "";
            graphAxisDefine1.LockRange = false;
            graphAxisDefine1.MainMarginColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            graphAxisDefine1.Maximum = 1.2;
            graphAxisDefine1.Minimum = 0;
            graphAxisDefine1.MinorMarginColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            graphAxisDefine1.Name = "";
            graphAxisDefine1.Unit = "";
            graphAxisDefine1.Visible = true;
            this.graph1.XAxis = graphAxisDefine1;
            graphAxisDefine2.AutoScale = true;
            graphAxisDefine2.AxisColor = System.Drawing.SystemColors.ActiveCaption;
            graphAxisDefine2.Caption = "";
            graphAxisDefine2.LockRange = false;
            graphAxisDefine2.MainMarginColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            graphAxisDefine2.Maximum = 1.2;
            graphAxisDefine2.Minimum = 0;
            graphAxisDefine2.MinorMarginColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            graphAxisDefine2.Name = "";
            graphAxisDefine2.Unit = "";
            graphAxisDefine2.Visible = false;
            this.graph1.Y2Axis = graphAxisDefine2;
            graphAxisDefine3.AutoScale = true;
            graphAxisDefine3.AxisColor = System.Drawing.SystemColors.ActiveCaption;
            graphAxisDefine3.Caption = "";
            graphAxisDefine3.LockRange = false;
            graphAxisDefine3.MainMarginColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            graphAxisDefine3.Maximum = 1.2;
            graphAxisDefine3.Minimum = 0;
            graphAxisDefine3.MinorMarginColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            graphAxisDefine3.Name = "";
            graphAxisDefine3.Unit = "";
            graphAxisDefine3.Visible = true;
            this.graph1.YAxis = graphAxisDefine3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_move,
            this.toolStripButton_analysis,
            this.toolStripButton_zoom,
            this.toolStripButton_selectLabel,
            this.toolStripButton_selectBandPoint,
            this.toolStripSeparator2,
            this.toolStripButton_pen,
            this.toolStripSeparator3,
            this.toolStripButton_lock,
            this.toolStripSeparator4,
            this.toolStripButton_showAll,
            this.toolStripSeparator5,
            this.toolStripButton_photo});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton_move
            // 
            this.toolStripButton_move.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_move.Image = global::WCYDisLab.Properties.Resources.move;
            resources.ApplyResources(this.toolStripButton_move, "toolStripButton_move");
            this.toolStripButton_move.Name = "toolStripButton_move";
            this.toolStripButton_move.Click += new System.EventHandler(this.toolStripButton_move_Click);
            // 
            // toolStripButton_analysis
            // 
            this.toolStripButton_analysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_analysis.Image = global::WCYDisLab.Properties.Resources.curve;
            resources.ApplyResources(this.toolStripButton_analysis, "toolStripButton_analysis");
            this.toolStripButton_analysis.Name = "toolStripButton_analysis";
            this.toolStripButton_analysis.Click += new System.EventHandler(this.toolStripButton_analysis_Click);
            // 
            // toolStripButton_zoom
            // 
            this.toolStripButton_zoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_zoom.Image = global::WCYDisLab.Properties.Resources.zoomselection;
            resources.ApplyResources(this.toolStripButton_zoom, "toolStripButton_zoom");
            this.toolStripButton_zoom.Name = "toolStripButton_zoom";
            this.toolStripButton_zoom.Click += new System.EventHandler(this.toolStripButton_zoom_Click);
            // 
            // toolStripButton_selectLabel
            // 
            this.toolStripButton_selectLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_selectLabel.Image = global::WCYDisLab.Properties.Resources.movetags;
            resources.ApplyResources(this.toolStripButton_selectLabel, "toolStripButton_selectLabel");
            this.toolStripButton_selectLabel.Name = "toolStripButton_selectLabel";
            this.toolStripButton_selectLabel.Click += new System.EventHandler(this.toolStripButton_selectLabel_Click);
            // 
            // toolStripButton_selectBandPoint
            // 
            this.toolStripButton_selectBandPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_selectBandPoint.Image = global::WCYDisLab.Properties.Resources.cut;
            resources.ApplyResources(this.toolStripButton_selectBandPoint, "toolStripButton_selectBandPoint");
            this.toolStripButton_selectBandPoint.Name = "toolStripButton_selectBandPoint";
            this.toolStripButton_selectBandPoint.Click += new System.EventHandler(this.toolStripButton_selectBandPoint_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripButton_pen
            // 
            this.toolStripButton_pen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_pen.Image = global::WCYDisLab.Properties.Resources.highlighter;
            resources.ApplyResources(this.toolStripButton_pen, "toolStripButton_pen");
            this.toolStripButton_pen.Name = "toolStripButton_pen";
            this.toolStripButton_pen.Click += new System.EventHandler(this.toolStripButton_pen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripButton_lock
            // 
            this.toolStripButton_lock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_lock.Image = global::WCYDisLab.Properties.Resources.lockX;
            resources.ApplyResources(this.toolStripButton_lock, "toolStripButton_lock");
            this.toolStripButton_lock.Name = "toolStripButton_lock";
            this.toolStripButton_lock.Click += new System.EventHandler(this.toolStripButton_lock_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripButton_showAll
            // 
            this.toolStripButton_showAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_showAll.Image = global::WCYDisLab.Properties.Resources.showall;
            resources.ApplyResources(this.toolStripButton_showAll, "toolStripButton_showAll");
            this.toolStripButton_showAll.Name = "toolStripButton_showAll";
            this.toolStripButton_showAll.Click += new System.EventHandler(this.toolStripButton_showAll_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripButton_photo
            // 
            this.toolStripButton_photo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_photo.Image = global::WCYDisLab.Properties.Resources.photograph;
            resources.ApplyResources(this.toolStripButton_photo, "toolStripButton_photo");
            this.toolStripButton_photo.Name = "toolStripButton_photo";
            this.toolStripButton_photo.Click += new System.EventHandler(this.toolStripButton_photo_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.ShowHelp = true;
            // 
            // FormGraph
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.graph1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormGraph";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_props;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_manager;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_operat;
        private VirtualInstrument.Graph graph1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_lineAndAxis;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_times;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_label;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_manualCurve;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_standardCurve;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_clear;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_mouse;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_lock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_move;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_analysis;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_zoom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_pen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_moveLabel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_bandPoint;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_move;
        private System.Windows.Forms.ToolStripButton toolStripButton_analysis;
        private System.Windows.Forms.ToolStripButton toolStripButton_zoom;
        private System.Windows.Forms.ToolStripButton toolStripButton_selectLabel;
        private System.Windows.Forms.ToolStripButton toolStripButton_selectBandPoint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_pen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_lock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton_showAll;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton_photo;
    }
}