﻿namespace WCYDisLab
{
    partial class FormInputPosition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInputPosition));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_confirm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_x = new System.Windows.Forms.TextBox();
            this.textBox_y = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_confirm,
            this.toolStripMenuItem_cancel});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // toolStripMenuItem_confirm
            // 
            resources.ApplyResources(this.toolStripMenuItem_confirm, "toolStripMenuItem_confirm");
            this.toolStripMenuItem_confirm.Name = "toolStripMenuItem_confirm";
            this.toolStripMenuItem_confirm.Click += new System.EventHandler(this.toolStripMenuItem_confirm_Click);
            // 
            // toolStripMenuItem_cancel
            // 
            resources.ApplyResources(this.toolStripMenuItem_cancel, "toolStripMenuItem_cancel");
            this.toolStripMenuItem_cancel.Name = "toolStripMenuItem_cancel";
            this.toolStripMenuItem_cancel.Click += new System.EventHandler(this.toolStripMenuItem_cancel_Click);
            // 
            // textBox_x
            // 
            resources.ApplyResources(this.textBox_x, "textBox_x");
            this.textBox_x.Name = "textBox_x";
            // 
            // textBox_y
            // 
            resources.ApplyResources(this.textBox_y, "textBox_y");
            this.textBox_y.Name = "textBox_y";
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
            // FormInputPosition
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_y);
            this.Controls.Add(this.textBox_x);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormInputPosition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_confirm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_cancel;
        private System.Windows.Forms.TextBox textBox_x;
        private System.Windows.Forms.TextBox textBox_y;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}