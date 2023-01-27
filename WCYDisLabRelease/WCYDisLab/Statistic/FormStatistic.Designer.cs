namespace WCYDisLab.Statistic
{
    partial class FormStatistic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStatistic));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_props = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_xAxis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_yAxis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_lineName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_method = new System.Windows.Forms.ToolStripMenuItem();
            this.statistic1 = new VirtualInstrument.Statistic();
            this.toolStripButton_move = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_analysis = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_zoom = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_selectLabel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_selectBandPoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_pen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_lock = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_showAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_props,
            this.toolStripMenuItem_method});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // toolStripMenuItem_props
            // 
            this.toolStripMenuItem_props.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_xAxis,
            this.toolStripMenuItem_yAxis,
            this.toolStripMenuItem_lineName});
            this.toolStripMenuItem_props.Name = "toolStripMenuItem_props";
            resources.ApplyResources(this.toolStripMenuItem_props, "toolStripMenuItem_props");
            // 
            // toolStripMenuItem_xAxis
            // 
            this.toolStripMenuItem_xAxis.Name = "toolStripMenuItem_xAxis";
            resources.ApplyResources(this.toolStripMenuItem_xAxis, "toolStripMenuItem_xAxis");
            this.toolStripMenuItem_xAxis.Click += new System.EventHandler(this.toolStripMenuItem_xAxis_Click);
            // 
            // toolStripMenuItem_yAxis
            // 
            this.toolStripMenuItem_yAxis.Name = "toolStripMenuItem_yAxis";
            resources.ApplyResources(this.toolStripMenuItem_yAxis, "toolStripMenuItem_yAxis");
            this.toolStripMenuItem_yAxis.Click += new System.EventHandler(this.toolStripMenuItem_yAxis_Click);
            // 
            // toolStripMenuItem_lineName
            // 
            this.toolStripMenuItem_lineName.Name = "toolStripMenuItem_lineName";
            resources.ApplyResources(this.toolStripMenuItem_lineName, "toolStripMenuItem_lineName");
            this.toolStripMenuItem_lineName.Click += new System.EventHandler(this.toolStripMenuItem_lineName_Click);
            // 
            // toolStripMenuItem_method
            // 
            this.toolStripMenuItem_method.Name = "toolStripMenuItem_method";
            resources.ApplyResources(this.toolStripMenuItem_method, "toolStripMenuItem_method");
            this.toolStripMenuItem_method.Click += new System.EventHandler(this.toolStripMenuItem_method_Click);
            // 
            // statistic1
            // 
            this.statistic1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.statistic1, "statistic1");
            this.statistic1.FontSizeF = 9F;
            this.statistic1.Name = "statistic1";
            this.statistic1.XColumnCurrentColumnName = "";
            this.statistic1.XTable = null;
            this.statistic1.YColumnCurrentColumnName = "";
            this.statistic1.YTable = null;
            this.statistic1.ZoomFactor = 0F;
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
            // toolStripButton_pen
            // 
            this.toolStripButton_pen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_pen.Image = global::WCYDisLab.Properties.Resources.highlighter;
            resources.ApplyResources(this.toolStripButton_pen, "toolStripButton_pen");
            this.toolStripButton_pen.Name = "toolStripButton_pen";
            this.toolStripButton_pen.Click += new System.EventHandler(this.toolStripButton_pen_Click);
            // 
            // toolStripButton_lock
            // 
            this.toolStripButton_lock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_lock.Image = global::WCYDisLab.Properties.Resources.lockX;
            resources.ApplyResources(this.toolStripButton_lock, "toolStripButton_lock");
            this.toolStripButton_lock.Name = "toolStripButton_lock";
            this.toolStripButton_lock.Click += new System.EventHandler(this.toolStripButton_lock_Click);
            // 
            // toolStripButton_showAll
            // 
            this.toolStripButton_showAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_showAll.Image = global::WCYDisLab.Properties.Resources.showall;
            resources.ApplyResources(this.toolStripButton_showAll, "toolStripButton_showAll");
            this.toolStripButton_showAll.Name = "toolStripButton_showAll";
            this.toolStripButton_showAll.Click += new System.EventHandler(this.toolStripButton_showAll_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::WCYDisLab.Properties.Resources.photograph;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // FormStatistic
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statistic1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormStatistic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_props;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_xAxis;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_yAxis;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_method;
        private VirtualInstrument.Statistic statistic1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_lineName;
    }
}