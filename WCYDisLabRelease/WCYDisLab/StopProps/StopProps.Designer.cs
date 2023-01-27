﻿namespace WCYDisLab.StopProps
{
    partial class StopProps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StopProps));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_manualStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_valueIncreaseStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_valueDecreaseStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_timeIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_timeStamp = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_mode = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_manualStop,
            this.toolStripMenuItem_valueIncreaseStop,
            this.toolStripMenuItem_valueDecreaseStop,
            this.toolStripSeparator1,
            this.toolStripMenuItem_timeIndex,
            this.toolStripMenuItem_timeStamp});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // toolStripMenuItem_manualStop
            // 
            this.toolStripMenuItem_manualStop.Name = "toolStripMenuItem_manualStop";
            resources.ApplyResources(this.toolStripMenuItem_manualStop, "toolStripMenuItem_manualStop");
            this.toolStripMenuItem_manualStop.Click += new System.EventHandler(this.toolStripMenuItem_manualStop_Click);
            // 
            // toolStripMenuItem_valueIncreaseStop
            // 
            this.toolStripMenuItem_valueIncreaseStop.Name = "toolStripMenuItem_valueIncreaseStop";
            resources.ApplyResources(this.toolStripMenuItem_valueIncreaseStop, "toolStripMenuItem_valueIncreaseStop");
            this.toolStripMenuItem_valueIncreaseStop.Click += new System.EventHandler(this.toolStripMenuItem_valueIncreaseStop_Click);
            // 
            // toolStripMenuItem_valueDecreaseStop
            // 
            this.toolStripMenuItem_valueDecreaseStop.Name = "toolStripMenuItem_valueDecreaseStop";
            resources.ApplyResources(this.toolStripMenuItem_valueDecreaseStop, "toolStripMenuItem_valueDecreaseStop");
            this.toolStripMenuItem_valueDecreaseStop.Click += new System.EventHandler(this.toolStripMenuItem_valueDecreaseStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItem_timeIndex
            // 
            this.toolStripMenuItem_timeIndex.Name = "toolStripMenuItem_timeIndex";
            resources.ApplyResources(this.toolStripMenuItem_timeIndex, "toolStripMenuItem_timeIndex");
            this.toolStripMenuItem_timeIndex.Click += new System.EventHandler(this.toolStripMenuItem_timeIndex_Click);
            // 
            // toolStripMenuItem_timeStamp
            // 
            this.toolStripMenuItem_timeStamp.Name = "toolStripMenuItem_timeStamp";
            resources.ApplyResources(this.toolStripMenuItem_timeStamp, "toolStripMenuItem_timeStamp");
            this.toolStripMenuItem_timeStamp.Click += new System.EventHandler(this.toolStripMenuItem_timeStamp_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // button_mode
            // 
            resources.ApplyResources(this.button_mode, "button_mode");
            this.button_mode.Name = "button_mode";
            this.button_mode.UseVisualStyleBackColor = true;
            this.button_mode.Click += new System.EventHandler(this.button_mode_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // StopProps
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_mode);
            this.Controls.Add(this.label1);
            this.Name = "StopProps";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_manualStop;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_valueIncreaseStop;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_valueDecreaseStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_timeIndex;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_timeStamp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_mode;
        private System.Windows.Forms.Label label1;
    }
}
