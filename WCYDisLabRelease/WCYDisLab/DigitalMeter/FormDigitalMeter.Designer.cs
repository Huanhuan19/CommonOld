namespace WCYDisLab.DigitalMeter
{
    partial class FormDigitalMeter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDigitalMeter));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_props = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_backColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_foreColor = new System.Windows.Forms.ToolStripButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.digitalMeter1 = new VirtualInstrument.DigitalMeter();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_props,
            this.toolStripSeparator1,
            this.toolStripButton_backColor,
            this.toolStripButton_foreColor});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton_props
            // 
            this.toolStripButton_props.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_props.Image = global::WCYDisLab.Properties.Resources.settingicon;
            resources.ApplyResources(this.toolStripButton_props, "toolStripButton_props");
            this.toolStripButton_props.Name = "toolStripButton_props";
            this.toolStripButton_props.Click += new System.EventHandler(this.toolStripButton_props_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripButton_backColor
            // 
            this.toolStripButton_backColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_backColor.Image = global::WCYDisLab.Properties.Resources.backgroundcolor;
            resources.ApplyResources(this.toolStripButton_backColor, "toolStripButton_backColor");
            this.toolStripButton_backColor.Name = "toolStripButton_backColor";
            this.toolStripButton_backColor.Click += new System.EventHandler(this.toolStripButton_backColor_Click);
            // 
            // toolStripButton_foreColor
            // 
            this.toolStripButton_foreColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_foreColor.Image = global::WCYDisLab.Properties.Resources.foreground;
            resources.ApplyResources(this.toolStripButton_foreColor, "toolStripButton_foreColor");
            this.toolStripButton_foreColor.Name = "toolStripButton_foreColor";
            this.toolStripButton_foreColor.Click += new System.EventHandler(this.toolStripButton_foreColor_Click);
            // 
            // digitalMeter1
            // 
            this.digitalMeter1.BackColor = System.Drawing.SystemColors.Control;
            this.digitalMeter1.Caption = "";
            this.digitalMeter1.DecimalCount = 4;
            this.digitalMeter1.DigitalColor = System.Drawing.SystemColors.ControlText;
            this.digitalMeter1.DigitalFontSize = 31.11429F;
            resources.ApplyResources(this.digitalMeter1, "digitalMeter1");
            this.digitalMeter1.MeterBackColor = System.Drawing.SystemColors.Control;
            this.digitalMeter1.Name = "digitalMeter1";
            this.digitalMeter1.Unit = "";
            this.digitalMeter1.Value = 0;
            // 
            // FormDigitalMeter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.digitalMeter1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormDigitalMeter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_props;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_backColor;
        private System.Windows.Forms.ToolStripButton toolStripButton_foreColor;
        private VirtualInstrument.DigitalMeter digitalMeter1;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}