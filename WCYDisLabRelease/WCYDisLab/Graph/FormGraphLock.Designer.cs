namespace WCYDisLab.Graph
{
    partial class FormGraphLock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGraphLock));
            this.checkBox_y = new System.Windows.Forms.CheckBox();
            this.checkBox_y2 = new System.Windows.Forms.CheckBox();
            this.checkBox_x = new System.Windows.Forms.CheckBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_confirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox_y
            // 
            resources.ApplyResources(this.checkBox_y, "checkBox_y");
            this.checkBox_y.Name = "checkBox_y";
            this.checkBox_y.UseVisualStyleBackColor = true;
            // 
            // checkBox_y2
            // 
            resources.ApplyResources(this.checkBox_y2, "checkBox_y2");
            this.checkBox_y2.Name = "checkBox_y2";
            this.checkBox_y2.UseVisualStyleBackColor = true;
            // 
            // checkBox_x
            // 
            resources.ApplyResources(this.checkBox_x, "checkBox_x");
            this.checkBox_x.Name = "checkBox_x";
            this.checkBox_x.UseVisualStyleBackColor = true;
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
            // FormGraphLock
            // 
            this.AcceptButton = this.button_confirm;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ControlBox = false;
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.Controls.Add(this.checkBox_x);
            this.Controls.Add(this.checkBox_y2);
            this.Controls.Add(this.checkBox_y);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormGraphLock";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_y;
        private System.Windows.Forms.CheckBox checkBox_y2;
        private System.Windows.Forms.CheckBox checkBox_x;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_confirm;

    }
}