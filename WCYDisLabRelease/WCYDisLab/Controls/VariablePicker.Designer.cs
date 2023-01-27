namespace WCYDisLab.Controls
{
    partial class VariablePicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariablePicker));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_timeIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_timeStamp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_sensor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_expr = new System.Windows.Forms.ToolStripMenuItem();
            this.button_pick = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_timeIndex,
            this.toolStripMenuItem_timeStamp,
            this.toolStripSeparator1,
            this.toolStripMenuItem_sensor,
            this.toolStripMenuItem_expr});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItem_sensor
            // 
            this.toolStripMenuItem_sensor.Name = "toolStripMenuItem_sensor";
            resources.ApplyResources(this.toolStripMenuItem_sensor, "toolStripMenuItem_sensor");
            this.toolStripMenuItem_sensor.Click += new System.EventHandler(this.toolStripMenuItem_sensor_Click);
            // 
            // toolStripMenuItem_expr
            // 
            this.toolStripMenuItem_expr.Name = "toolStripMenuItem_expr";
            resources.ApplyResources(this.toolStripMenuItem_expr, "toolStripMenuItem_expr");
            this.toolStripMenuItem_expr.Click += new System.EventHandler(this.toolStripMenuItem_expr_Click);
            // 
            // button_pick
            // 
            resources.ApplyResources(this.button_pick, "button_pick");
            this.button_pick.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button_pick.Name = "button_pick";
            this.button_pick.UseVisualStyleBackColor = true;
            this.button_pick.Click += new System.EventHandler(this.button_pick_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.button_pick, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // VariablePicker
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VariablePicker";
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_pick;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_timeIndex;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_timeStamp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_sensor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_expr;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}
