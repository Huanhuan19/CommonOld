﻿namespace WCYDisLab
{
    partial class FormInputText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInputText));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_confirm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.toolStripMenuItem_confirm.Name = "toolStripMenuItem_confirm";
            resources.ApplyResources(this.toolStripMenuItem_confirm, "toolStripMenuItem_confirm");
            this.toolStripMenuItem_confirm.Click += new System.EventHandler(this.toolStripMenuItem_confirm_Click);
            // 
            // toolStripMenuItem_cancel
            // 
            this.toolStripMenuItem_cancel.Name = "toolStripMenuItem_cancel";
            resources.ApplyResources(this.toolStripMenuItem_cancel, "toolStripMenuItem_cancel");
            this.toolStripMenuItem_cancel.Click += new System.EventHandler(this.toolStripMenuItem_cancel_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // FormInputText
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormInputText";
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
        private System.Windows.Forms.TextBox textBox1;
    }
}