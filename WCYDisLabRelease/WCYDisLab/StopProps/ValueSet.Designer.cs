namespace WCYDisLab.StopProps
{
    partial class ValueSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValueSet));
            this.variablePicker1 = new WCYDisLab.Controls.VariablePicker();
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_abs = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // variablePicker1
            // 
            resources.ApplyResources(this.variablePicker1, "variablePicker1");
            this.variablePicker1.Name = "variablePicker1";
            // 
            // textBox_value
            // 
            resources.ApplyResources(this.textBox_value, "textBox_value");
            this.textBox_value.Name = "textBox_value";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkBox_abs
            // 
            resources.ApplyResources(this.checkBox_abs, "checkBox_abs");
            this.checkBox_abs.Name = "checkBox_abs";
            this.checkBox_abs.UseVisualStyleBackColor = true;
            // 
            // ValueSet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_abs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.variablePicker1);
            this.Name = "ValueSet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WCYDisLab.Controls.VariablePicker variablePicker1;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_abs;
    }
}
