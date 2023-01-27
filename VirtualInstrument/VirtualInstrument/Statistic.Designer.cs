namespace VirtualInstrument
{
    partial class Statistic
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statistic));
            VirtualInstrument.Classes.GraphAxisDefine graphAxisDefine1 = new VirtualInstrument.Classes.GraphAxisDefine();
            VirtualInstrument.Classes.GraphAxisDefine graphAxisDefine2 = new VirtualInstrument.Classes.GraphAxisDefine();
            VirtualInstrument.Classes.GraphAxisDefine graphAxisDefine3 = new VirtualInstrument.Classes.GraphAxisDefine();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView_x = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView_y = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.graph1 = new VirtualInstrument.Graph();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_x)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_y)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView_x);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // dataGridView_x
            // 
            this.dataGridView_x.AllowUserToAddRows = false;
            this.dataGridView_x.AllowUserToDeleteRows = false;
            this.dataGridView_x.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_x.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_x.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_x.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView_x.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView_x, "dataGridView_x");
            this.dataGridView_x.MultiSelect = false;
            this.dataGridView_x.Name = "dataGridView_x";
            this.dataGridView_x.ReadOnly = true;
            this.dataGridView_x.RowHeadersVisible = false;
            this.dataGridView_x.RowTemplate.Height = 23;
            this.dataGridView_x.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView_y);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // dataGridView_y
            // 
            this.dataGridView_y.AllowUserToAddRows = false;
            this.dataGridView_y.AllowUserToDeleteRows = false;
            this.dataGridView_y.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_y.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_y.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView_y.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView_y, "dataGridView_y");
            this.dataGridView_y.MultiSelect = false;
            this.dataGridView_y.Name = "dataGridView_y";
            this.dataGridView_y.ReadOnly = true;
            this.dataGridView_y.RowHeadersVisible = false;
            this.dataGridView_y.RowTemplate.Height = 23;
            this.dataGridView_y.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.graph1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // graph1
            // 
            this.graph1.DefaultFittingMethodName = "";
            resources.ApplyResources(this.graph1, "graph1");
            this.graph1.GraphCaption = "";
            this.graph1.GraphCrossAuto = true;
            this.graph1.GraphicsSelectType = VirtualInstrument.Classes.GraphicsSelectType.None;
            this.graph1.GraphName = "";
            this.graph1.HighLighterColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
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
            // Statistic
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Statistic";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_x)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_y)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView_x;
        private System.Windows.Forms.DataGridView dataGridView_y;
        private Graph graph1;
    }
}
