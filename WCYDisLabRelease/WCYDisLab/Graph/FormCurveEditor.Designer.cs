namespace WCYDisLab.Graph
{
    partial class FormCurveEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCurveEditor));
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_confirm = new System.Windows.Forms.Button();
            this.checkBox_pointVisible = new System.Windows.Forms.CheckBox();
            this.button_color = new System.Windows.Forms.Button();
            this.checkBox_isSmooth = new System.Windows.Forms.CheckBox();
            this.checkBox_lineVisible = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_width = new System.Windows.Forms.NumericUpDown();
            this.textBox_caption = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_smoothTention = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_width)).BeginInit();
            this.SuspendLayout();
            // 
            // button_cancel
            // 
            resources.ApplyResources(this.button_cancel, "button_cancel");
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Image = global::WCYDisLab.Properties.Resources.chinaz29;
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // button_confirm
            // 
            resources.ApplyResources(this.button_confirm, "button_confirm");
            this.button_confirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_confirm.Image = global::WCYDisLab.Properties.Resources.chinaz65;
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.UseVisualStyleBackColor = true;
            // 
            // checkBox_pointVisible
            // 
            resources.ApplyResources(this.checkBox_pointVisible, "checkBox_pointVisible");
            this.checkBox_pointVisible.Name = "checkBox_pointVisible";
            this.checkBox_pointVisible.UseVisualStyleBackColor = true;
            // 
            // button_color
            // 
            resources.ApplyResources(this.button_color, "button_color");
            this.button_color.Name = "button_color";
            this.button_color.UseVisualStyleBackColor = true;
            this.button_color.Click += new System.EventHandler(this.button_color_Click);
            // 
            // checkBox_isSmooth
            // 
            resources.ApplyResources(this.checkBox_isSmooth, "checkBox_isSmooth");
            this.checkBox_isSmooth.Name = "checkBox_isSmooth";
            this.checkBox_isSmooth.UseVisualStyleBackColor = true;
            this.checkBox_isSmooth.CheckedChanged += new System.EventHandler(this.checkBox_isSmooth_CheckedChanged);
            // 
            // checkBox_lineVisible
            // 
            resources.ApplyResources(this.checkBox_lineVisible, "checkBox_lineVisible");
            this.checkBox_lineVisible.Name = "checkBox_lineVisible";
            this.checkBox_lineVisible.UseVisualStyleBackColor = true;
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
            // numericUpDown_width
            // 
            resources.ApplyResources(this.numericUpDown_width, "numericUpDown_width");
            this.numericUpDown_width.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_width.Name = "numericUpDown_width";
            this.numericUpDown_width.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox_caption
            // 
            resources.ApplyResources(this.textBox_caption, "textBox_caption");
            this.textBox_caption.Name = "textBox_caption";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox_smoothTention
            // 
            resources.ApplyResources(this.textBox_smoothTention, "textBox_smoothTention");
            this.textBox_smoothTention.Name = "textBox_smoothTention";
            // 
            // FormCurveEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_pointVisible);
            this.Controls.Add(this.button_color);
            this.Controls.Add(this.textBox_smoothTention);
            this.Controls.Add(this.checkBox_isSmooth);
            this.Controls.Add(this.checkBox_lineVisible);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown_width);
            this.Controls.Add(this.textBox_caption);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormCurveEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_width)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.CheckBox checkBox_pointVisible;
        private System.Windows.Forms.Button button_color;
        private System.Windows.Forms.CheckBox checkBox_isSmooth;
        private System.Windows.Forms.CheckBox checkBox_lineVisible;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_width;
        private System.Windows.Forms.TextBox textBox_caption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_smoothTention;
    }
}