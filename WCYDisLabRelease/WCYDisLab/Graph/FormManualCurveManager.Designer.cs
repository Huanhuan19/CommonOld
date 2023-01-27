﻿namespace WCYDisLab.Graph
{
    partial class FormManualCurveManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormManualCurveManager));
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_edit = new System.Windows.Forms.Button();
            this.button_open = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_remove = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_confirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // listView1
            // 
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // button_edit
            // 
            this.button_edit.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_edit, "button_edit");
            this.button_edit.Image = global::WCYDisLab.Properties.Resources.chinaz80;
            this.button_edit.Name = "button_edit";
            this.toolTip1.SetToolTip(this.button_edit, resources.GetString("button_edit.ToolTip"));
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // button_open
            // 
            this.button_open.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_open, "button_open");
            this.button_open.Image = global::WCYDisLab.Properties.Resources.chinaz86;
            this.button_open.Name = "button_open";
            this.toolTip1.SetToolTip(this.button_open, resources.GetString("button_open.ToolTip"));
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // button_clear
            // 
            this.button_clear.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_clear, "button_clear");
            this.button_clear.Image = global::WCYDisLab.Properties.Resources.chinaz73;
            this.button_clear.Name = "button_clear";
            this.toolTip1.SetToolTip(this.button_clear, resources.GetString("button_clear.ToolTip"));
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // button_remove
            // 
            this.button_remove.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_remove, "button_remove");
            this.button_remove.Image = global::WCYDisLab.Properties.Resources.chinaz25;
            this.button_remove.Name = "button_remove";
            this.toolTip1.SetToolTip(this.button_remove, resources.GetString("button_remove.ToolTip"));
            this.button_remove.UseVisualStyleBackColor = true;
            this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
            // 
            // button_add
            // 
            this.button_add.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_add, "button_add");
            this.button_add.Image = global::WCYDisLab.Properties.Resources.chinaz2;
            this.button_add.Name = "button_add";
            this.toolTip1.SetToolTip(this.button_add, resources.GetString("button_add.ToolTip"));
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xls";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
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
            // FormManualCurveManager
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_edit);
            this.Controls.Add(this.button_open);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.button_remove);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.Name = "FormManualCurveManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_remove;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_edit;
    }
}