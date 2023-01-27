namespace WCYDisLab.Graph
{
    partial class XAxisProps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XAxisProps));
            this.checkBox_auto = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_max = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_min = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_caption = new System.Windows.Forms.TextBox();
            this.textBox_unit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.variablePicker1 = new WCYDisLab.Controls.VariablePicker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox_auto
            // 
            resources.ApplyResources(this.checkBox_auto, "checkBox_auto");
            this.checkBox_auto.Name = "checkBox_auto";
            this.checkBox_auto.UseVisualStyleBackColor = true;
            this.checkBox_auto.CheckedChanged += new System.EventHandler(this.checkBox_auto_CheckedChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.textBox_max);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox_min);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Name = "panel1";
            // 
            // textBox_max
            // 
            resources.ApplyResources(this.textBox_max, "textBox_max");
            this.textBox_max.Name = "textBox_max";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBox_min
            // 
            resources.ApplyResources(this.textBox_min, "textBox_min");
            this.textBox_min.Name = "textBox_min";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBox_caption
            // 
            resources.ApplyResources(this.textBox_caption, "textBox_caption");
            this.textBox_caption.Name = "textBox_caption";
            // 
            // textBox_unit
            // 
            resources.ApplyResources(this.textBox_unit, "textBox_unit");
            this.textBox_unit.Name = "textBox_unit";
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
            // variablePicker1
            // 
            resources.ApplyResources(this.variablePicker1, "variablePicker1");
            this.variablePicker1.Name = "variablePicker1";
            // 
            // XAxisProps
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.variablePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_unit);
            this.Controls.Add(this.textBox_caption);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox_auto);
            this.Name = "XAxisProps";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_auto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_caption;
        private System.Windows.Forms.TextBox textBox_unit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private WCYDisLab.Controls.VariablePicker variablePicker1;
        private System.Windows.Forms.TextBox textBox_max;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_min;
        private System.Windows.Forms.Label label3;
    }
}
