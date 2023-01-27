namespace WCYDisLab.SensorSet
{
    partial class WaveCreat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveCreat));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_waveType = new System.Windows.Forms.Button();
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.textBox_fre = new System.Windows.Forms.TextBox();
            this.button_IsOk = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_square = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_sin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_triangle = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // button_waveType
            // 
            resources.ApplyResources(this.button_waveType, "button_waveType");
            this.button_waveType.Name = "button_waveType";
            this.button_waveType.UseVisualStyleBackColor = true;
            this.button_waveType.Click += new System.EventHandler(this.button_waveType_Click);
            // 
            // textBox_value
            // 
            resources.ApplyResources(this.textBox_value, "textBox_value");
            this.textBox_value.Name = "textBox_value";
            // 
            // textBox_fre
            // 
            resources.ApplyResources(this.textBox_fre, "textBox_fre");
            this.textBox_fre.Name = "textBox_fre";
            // 
            // button_IsOk
            // 
            resources.ApplyResources(this.button_IsOk, "button_IsOk");
            this.button_IsOk.Name = "button_IsOk";
            this.button_IsOk.UseVisualStyleBackColor = true;
            this.button_IsOk.Click += new System.EventHandler(this.button_IsOk_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_square,
            this.toolStripMenuItem_sin,
            this.toolStripMenuItem_triangle});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // toolStripMenuItem_square
            // 
            this.toolStripMenuItem_square.Name = "toolStripMenuItem_square";
            resources.ApplyResources(this.toolStripMenuItem_square, "toolStripMenuItem_square");
            this.toolStripMenuItem_square.Click += new System.EventHandler(this.toolStripMenuItem_square_Click);
            // 
            // toolStripMenuItem_sin
            // 
            this.toolStripMenuItem_sin.Name = "toolStripMenuItem_sin";
            resources.ApplyResources(this.toolStripMenuItem_sin, "toolStripMenuItem_sin");
            this.toolStripMenuItem_sin.Click += new System.EventHandler(this.toolStripMenuItem_sin_Click);
            // 
            // toolStripMenuItem_triangle
            // 
            this.toolStripMenuItem_triangle.Name = "toolStripMenuItem_triangle";
            resources.ApplyResources(this.toolStripMenuItem_triangle, "toolStripMenuItem_triangle");
            this.toolStripMenuItem_triangle.Click += new System.EventHandler(this.toolStripMenuItem_triangle_Click);
            // 
            // WaveCreat
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_IsOk);
            this.Controls.Add(this.textBox_fre);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.button_waveType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "WaveCreat";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_waveType;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.TextBox textBox_fre;
        private System.Windows.Forms.Button button_IsOk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_square;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_sin;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_triangle;
    }
}
