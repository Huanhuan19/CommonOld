namespace WCYDisLab.SensorSet
{
    partial class Switch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Switch));
            this.button_voice = new System.Windows.Forms.Button();
            this.button_light = new System.Windows.Forms.Button();
            this.button_control = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_voiceOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_voiceClose = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_lightOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_lightClose = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_controlOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_controlClose = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_voice
            // 
            resources.ApplyResources(this.button_voice, "button_voice");
            this.button_voice.Name = "button_voice";
            this.button_voice.UseVisualStyleBackColor = true;
            this.button_voice.Click += new System.EventHandler(this.button_voice_Click);
            // 
            // button_light
            // 
            resources.ApplyResources(this.button_light, "button_light");
            this.button_light.Name = "button_light";
            this.button_light.UseVisualStyleBackColor = true;
            this.button_light.Click += new System.EventHandler(this.button_lignt_Click);
            // 
            // button_control
            // 
            resources.ApplyResources(this.button_control, "button_control");
            this.button_control.Name = "button_control";
            this.button_control.UseVisualStyleBackColor = true;
            this.button_control.Click += new System.EventHandler(this.button_control_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_voiceOpen,
            this.toolStripMenuItem_voiceClose});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // toolStripMenuItem_voiceOpen
            // 
            this.toolStripMenuItem_voiceOpen.Name = "toolStripMenuItem_voiceOpen";
            resources.ApplyResources(this.toolStripMenuItem_voiceOpen, "toolStripMenuItem_voiceOpen");
            this.toolStripMenuItem_voiceOpen.Click += new System.EventHandler(this.toolStripMenuItem_voiceOpen_Click);
            // 
            // toolStripMenuItem_voiceClose
            // 
            this.toolStripMenuItem_voiceClose.Name = "toolStripMenuItem_voiceClose";
            resources.ApplyResources(this.toolStripMenuItem_voiceClose, "toolStripMenuItem_voiceClose");
            this.toolStripMenuItem_voiceClose.Click += new System.EventHandler(this.toolStripMenuItem_voiceClose_Click);
            // 
            // contextMenuStrip2
            // 
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_lightOpen,
            this.toolStripMenuItem_lightClose});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            // 
            // toolStripMenuItem_lightOpen
            // 
            this.toolStripMenuItem_lightOpen.Name = "toolStripMenuItem_lightOpen";
            resources.ApplyResources(this.toolStripMenuItem_lightOpen, "toolStripMenuItem_lightOpen");
            this.toolStripMenuItem_lightOpen.Click += new System.EventHandler(this.toolStripMenuItem_lightOpen_Click);
            // 
            // toolStripMenuItem_lightClose
            // 
            this.toolStripMenuItem_lightClose.Name = "toolStripMenuItem_lightClose";
            resources.ApplyResources(this.toolStripMenuItem_lightClose, "toolStripMenuItem_lightClose");
            this.toolStripMenuItem_lightClose.Click += new System.EventHandler(this.toolStripMenuItem_lightClose_Click);
            // 
            // contextMenuStrip3
            // 
            resources.ApplyResources(this.contextMenuStrip3, "contextMenuStrip3");
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_controlOpen,
            this.toolStripMenuItem_controlClose});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            // 
            // toolStripMenuItem_controlOpen
            // 
            this.toolStripMenuItem_controlOpen.Name = "toolStripMenuItem_controlOpen";
            resources.ApplyResources(this.toolStripMenuItem_controlOpen, "toolStripMenuItem_controlOpen");
            this.toolStripMenuItem_controlOpen.Click += new System.EventHandler(this.toolStripMenuItem_controlOpen_Click);
            // 
            // toolStripMenuItem_controlClose
            // 
            this.toolStripMenuItem_controlClose.Name = "toolStripMenuItem_controlClose";
            resources.ApplyResources(this.toolStripMenuItem_controlClose, "toolStripMenuItem_controlClose");
            this.toolStripMenuItem_controlClose.Click += new System.EventHandler(this.toolStripMenuItem_controlClose_Click);
            // 
            // Switch
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button_control);
            this.Controls.Add(this.button_light);
            this.Controls.Add(this.button_voice);
            this.Name = "Switch";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_voice;
        private System.Windows.Forms.Button button_light;
        private System.Windows.Forms.Button button_control;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_voiceOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_voiceClose;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_lightOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_lightClose;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_controlOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_controlClose;
    }
}
