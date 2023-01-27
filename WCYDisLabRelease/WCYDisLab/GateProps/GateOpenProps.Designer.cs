namespace WCYDisLab.GateProps
{
    partial class GateOpenProps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GateOpenProps));
            this.panel1 = new System.Windows.Forms.Panel();
            this.valueSet1 = new WCYDisLab.GateProps.ValueSet();
            this.button_mode = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_serial = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_manual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_valueIncreaseStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_valueDecreaseStop = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.valueSet1);
            this.panel1.Name = "panel1";
            // 
            // valueSet1
            // 
            resources.ApplyResources(this.valueSet1, "valueSet1");
            this.valueSet1.Name = "valueSet1";
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
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_serial,
            this.toolStripMenuItem_manual,
            this.toolStripMenuItem_valueIncreaseStop,
            this.toolStripMenuItem_valueDecreaseStop});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // toolStripMenuItem_serial
            // 
            this.toolStripMenuItem_serial.Name = "toolStripMenuItem_serial";
            resources.ApplyResources(this.toolStripMenuItem_serial, "toolStripMenuItem_serial");
            this.toolStripMenuItem_serial.Click += new System.EventHandler(this.toolStripMenuItem_serial_Click);
            // 
            // toolStripMenuItem_manual
            // 
            this.toolStripMenuItem_manual.Name = "toolStripMenuItem_manual";
            resources.ApplyResources(this.toolStripMenuItem_manual, "toolStripMenuItem_manual");
            this.toolStripMenuItem_manual.Click += new System.EventHandler(this.toolStripMenuItem_manual_Click);
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
            // GateOpenProps
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_mode);
            this.Controls.Add(this.label1);
            this.Name = "GateOpenProps";
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_mode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_serial;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_valueIncreaseStop;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_valueDecreaseStop;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_manual;
        private ValueSet valueSet1;
    }
}
