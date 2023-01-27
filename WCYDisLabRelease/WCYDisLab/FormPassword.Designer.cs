namespace WCYDisLab
{
    partial class FormPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPassword));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_check = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_change = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_new2 = new System.Windows.Forms.TextBox();
            this.textBox_new1 = new System.Windows.Forms.TextBox();
            this.label_right = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_changePassword = new System.Windows.Forms.Button();
            this.button_go = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox_password
            // 
            resources.ApplyResources(this.textBox_password, "textBox_password");
            this.textBox_password.Name = "textBox_password";
            // 
            // button_check
            // 
            this.button_check.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_check, "button_check");
            this.button_check.Image = global::WCYDisLab.Properties.Resources.chinaz76;
            this.button_check.Name = "button_check";
            this.button_check.UseVisualStyleBackColor = true;
            this.button_check.Click += new System.EventHandler(this.button_check_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_change);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_new2);
            this.panel1.Controls.Add(this.textBox_new1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // button_change
            // 
            this.button_change.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_change, "button_change");
            this.button_change.Image = global::WCYDisLab.Properties.Resources.chinaz73;
            this.button_change.Name = "button_change";
            this.button_change.UseVisualStyleBackColor = true;
            this.button_change.Click += new System.EventHandler(this.button_change_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBox_new2
            // 
            resources.ApplyResources(this.textBox_new2, "textBox_new2");
            this.textBox_new2.Name = "textBox_new2";
            // 
            // textBox_new1
            // 
            resources.ApplyResources(this.textBox_new1, "textBox_new1");
            this.textBox_new1.Name = "textBox_new1";
            // 
            // label_right
            // 
            resources.ApplyResources(this.label_right, "label_right");
            this.label_right.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label_right.Name = "label_right";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_changePassword);
            this.panel2.Controls.Add(this.button_go);
            this.panel2.Controls.Add(this.label_right);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // button_changePassword
            // 
            resources.ApplyResources(this.button_changePassword, "button_changePassword");
            this.button_changePassword.Name = "button_changePassword";
            this.button_changePassword.UseVisualStyleBackColor = true;
            this.button_changePassword.Click += new System.EventHandler(this.button_changePassword_Click);
            // 
            // button_go
            // 
            resources.ApplyResources(this.button_go, "button_go");
            this.button_go.Name = "button_go";
            this.button_go.UseVisualStyleBackColor = true;
            this.button_go.Click += new System.EventHandler(this.button_go_Click);
            // 
            // FormPassword
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_check);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormPassword";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_check;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_right;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_new2;
        private System.Windows.Forms.TextBox textBox_new1;
        private System.Windows.Forms.Button button_change;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_changePassword;
        private System.Windows.Forms.Button button_go;
    }
}