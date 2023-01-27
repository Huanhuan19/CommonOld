namespace WCYDisLab.Table
{
    partial class FormTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTable));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_timeIndex = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_timeStamp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_props = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_caption = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_time = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_excel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_fontSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_merge = new System.Windows.Forms.ToolStripButton();
            this.table1 = new VirtualInstrument.Table();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_timeIndex,
            this.toolStripButton_timeStamp,
            this.toolStripButton_props,
            this.toolStripButton_caption,
            this.toolStripButton_time,
            this.toolStripSeparator1,
            this.toolStripButton_excel,
            this.toolStripSeparator2,
            this.toolStripButton_fontSize,
            this.toolStripButton_merge});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton_timeIndex
            // 
            this.toolStripButton_timeIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_timeIndex.Image = global::WCYDisLab.Properties.Resources._decimal;
            resources.ApplyResources(this.toolStripButton_timeIndex, "toolStripButton_timeIndex");
            this.toolStripButton_timeIndex.Name = "toolStripButton_timeIndex";
            this.toolStripButton_timeIndex.Click += new System.EventHandler(this.toolStripButton_timeIndex_Click);
            // 
            // toolStripButton_timeStamp
            // 
            this.toolStripButton_timeStamp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_timeStamp.Image = global::WCYDisLab.Properties.Resources.setvalue;
            resources.ApplyResources(this.toolStripButton_timeStamp, "toolStripButton_timeStamp");
            this.toolStripButton_timeStamp.Name = "toolStripButton_timeStamp";
            this.toolStripButton_timeStamp.Click += new System.EventHandler(this.toolStripButton_timeStamp_Click);
            // 
            // toolStripButton_props
            // 
            this.toolStripButton_props.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_props.Image = global::WCYDisLab.Properties.Resources.settingicon;
            resources.ApplyResources(this.toolStripButton_props, "toolStripButton_props");
            this.toolStripButton_props.Name = "toolStripButton_props";
            this.toolStripButton_props.Click += new System.EventHandler(this.toolStripButton_props_Click);
            // 
            // toolStripButton_caption
            // 
            this.toolStripButton_caption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_caption.Image = global::WCYDisLab.Properties.Resources.title;
            resources.ApplyResources(this.toolStripButton_caption, "toolStripButton_caption");
            this.toolStripButton_caption.Name = "toolStripButton_caption";
            this.toolStripButton_caption.Click += new System.EventHandler(this.toolStripButton_caption_Click);
            // 
            // toolStripButton_time
            // 
            this.toolStripButton_time.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_time.Image = global::WCYDisLab.Properties.Resources.samplingtimes;
            resources.ApplyResources(this.toolStripButton_time, "toolStripButton_time");
            this.toolStripButton_time.Name = "toolStripButton_time";
            this.toolStripButton_time.Click += new System.EventHandler(this.toolStripButton_time_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripButton_excel
            // 
            this.toolStripButton_excel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_excel.Image = global::WCYDisLab.Properties.Resources.exportEXL;
            resources.ApplyResources(this.toolStripButton_excel, "toolStripButton_excel");
            this.toolStripButton_excel.Name = "toolStripButton_excel";
            this.toolStripButton_excel.Click += new System.EventHandler(this.toolStripButton_excel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripButton_fontSize
            // 
            this.toolStripButton_fontSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_fontSize.Image = global::WCYDisLab.Properties.Resources.fontsize;
            resources.ApplyResources(this.toolStripButton_fontSize, "toolStripButton_fontSize");
            this.toolStripButton_fontSize.Name = "toolStripButton_fontSize";
            this.toolStripButton_fontSize.Click += new System.EventHandler(this.toolStripButton_fontSize_Click);
            // 
            // toolStripButton_merge
            // 
            this.toolStripButton_merge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_merge.Image = global::WCYDisLab.Properties.Resources.merge;
            resources.ApplyResources(this.toolStripButton_merge, "toolStripButton_merge");
            this.toolStripButton_merge.Name = "toolStripButton_merge";
            this.toolStripButton_merge.Click += new System.EventHandler(this.toolStripButton_merge_Click);
            // 
            // table1
            // 
            resources.ApplyResources(this.table1, "table1");
            this.table1.FontSizeF = 9F;
            this.table1.Merge = false;
            this.table1.Name = "table1";
            this.table1.SectionName = "";
            this.table1.TableCaption = "";
            this.table1.TableTitle = "";
            this.table1.ZoomFactor = 1F;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // FormTable
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormTable";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_caption;
        private System.Windows.Forms.ToolStripButton toolStripButton_props;
        private System.Windows.Forms.ToolStripButton toolStripButton_time;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_excel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_fontSize;
        private System.Windows.Forms.ToolStripButton toolStripButton_merge;
        private VirtualInstrument.Table table1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton toolStripButton_timeIndex;
        private System.Windows.Forms.ToolStripButton toolStripButton_timeStamp;

    }
}