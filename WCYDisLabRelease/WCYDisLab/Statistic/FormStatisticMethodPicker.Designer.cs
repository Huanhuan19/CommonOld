namespace WCYDisLab.Statistic
{
    partial class FormStatisticMethodPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStatisticMethodPicker));
            this.radioButton_sum = new System.Windows.Forms.RadioButton();
            this.radioButton_standardError = new System.Windows.Forms.RadioButton();
            this.radioButton_median = new System.Windows.Forms.RadioButton();
            this.radioButton_minimum = new System.Windows.Forms.RadioButton();
            this.radioButton_maximum = new System.Windows.Forms.RadioButton();
            this.radioButton_average = new System.Windows.Forms.RadioButton();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_confirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButton_sum
            // 
            resources.ApplyResources(this.radioButton_sum, "radioButton_sum");
            this.radioButton_sum.Name = "radioButton_sum";
            this.radioButton_sum.TabStop = true;
            this.radioButton_sum.UseVisualStyleBackColor = true;
            // 
            // radioButton_standardError
            // 
            resources.ApplyResources(this.radioButton_standardError, "radioButton_standardError");
            this.radioButton_standardError.Name = "radioButton_standardError";
            this.radioButton_standardError.TabStop = true;
            this.radioButton_standardError.UseVisualStyleBackColor = true;
            // 
            // radioButton_median
            // 
            resources.ApplyResources(this.radioButton_median, "radioButton_median");
            this.radioButton_median.Name = "radioButton_median";
            this.radioButton_median.TabStop = true;
            this.radioButton_median.UseVisualStyleBackColor = true;
            // 
            // radioButton_minimum
            // 
            resources.ApplyResources(this.radioButton_minimum, "radioButton_minimum");
            this.radioButton_minimum.Name = "radioButton_minimum";
            this.radioButton_minimum.TabStop = true;
            this.radioButton_minimum.UseVisualStyleBackColor = true;
            // 
            // radioButton_maximum
            // 
            resources.ApplyResources(this.radioButton_maximum, "radioButton_maximum");
            this.radioButton_maximum.Name = "radioButton_maximum";
            this.radioButton_maximum.TabStop = true;
            this.radioButton_maximum.UseVisualStyleBackColor = true;
            // 
            // radioButton_average
            // 
            this.radioButton_average.Checked = true;
            this.radioButton_average.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.radioButton_average, "radioButton_average");
            this.radioButton_average.Name = "radioButton_average";
            this.radioButton_average.TabStop = true;
            this.radioButton_average.UseVisualStyleBackColor = true;
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
            // FormStatisticMethodPicker
            // 
            this.AcceptButton = this.button_confirm;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.Controls.Add(this.radioButton_sum);
            this.Controls.Add(this.radioButton_standardError);
            this.Controls.Add(this.radioButton_median);
            this.Controls.Add(this.radioButton_minimum);
            this.Controls.Add(this.radioButton_maximum);
            this.Controls.Add(this.radioButton_average);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormStatisticMethodPicker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton_sum;
        private System.Windows.Forms.RadioButton radioButton_standardError;
        private System.Windows.Forms.RadioButton radioButton_median;
        private System.Windows.Forms.RadioButton radioButton_minimum;
        private System.Windows.Forms.RadioButton radioButton_maximum;
        private System.Windows.Forms.RadioButton radioButton_average;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_confirm;
    }
}