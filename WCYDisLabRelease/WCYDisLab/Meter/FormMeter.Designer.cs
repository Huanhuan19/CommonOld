namespace WCYDisLab.Meter
{
    partial class FormMeter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMeter));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_props = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_caption = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_scaleRange = new System.Windows.Forms.ToolStripButton();
            this.meter1 = new VirtualInstrument.UVMeter();
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
            this.toolStripButton_caption,
            this.toolStripButton_scaleRange});
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
            // toolStripButton_caption
            // 
            this.toolStripButton_caption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_caption.Image = global::WCYDisLab.Properties.Resources.title;
            resources.ApplyResources(this.toolStripButton_caption, "toolStripButton_caption");
            this.toolStripButton_caption.Name = "toolStripButton_caption";
            this.toolStripButton_caption.Click += new System.EventHandler(this.toolStripButton_caption_Click);
            // 
            // toolStripButton_scaleRange
            // 
            this.toolStripButton_scaleRange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_scaleRange.Image = global::WCYDisLab.Properties.Resources.setvalue;
            resources.ApplyResources(this.toolStripButton_scaleRange, "toolStripButton_scaleRange");
            this.toolStripButton_scaleRange.Name = "toolStripButton_scaleRange";
            this.toolStripButton_scaleRange.Click += new System.EventHandler(this.toolStripButton_scaleRange_Click);
            // 
            // meter1
            // 
            this.meter1.BackColor = System.Drawing.SystemColors.Window;
            this.meter1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.meter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.meter1.Caption = "";
            this.meter1.CaptionColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.meter1, "meter1");
            this.meter1.EndAngle = -120F;
            this.meter1.MainScaleCount = 4;
            this.meter1.MaxValue = 100;
            this.meter1.MeterBackColor = System.Drawing.SystemColors.Window;
            this.meter1.MinimumSize = new System.Drawing.Size(255, 317);
            this.meter1.MinorScaleCount = 5;
            this.meter1.MinValue = -100;
            this.meter1.Name = "meter1";
            this.meter1.NeedleColor = System.Drawing.Color.Red;
            this.meter1.OriginPointColor = System.Drawing.Color.Red;
            this.meter1.ScaleAuto = false;
            this.meter1.ScaleColor = System.Drawing.Color.Black;
            this.meter1.StartAngle = -240F;
            this.meter1.Unit = "";
            this.meter1.UnitColor = System.Drawing.Color.Black;
            this.meter1.Value = 0;
            // 
            // FormMeter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.meter1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormMeter";
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
        private System.Windows.Forms.ToolStripButton toolStripButton_caption;
        private System.Windows.Forms.ToolStripButton toolStripButton_scaleRange;
        private VirtualInstrument.UVMeter meter1;
    }
}