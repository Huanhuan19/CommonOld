namespace WCYDisLab
{
    partial class FormSensorEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSensorEditor));
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_confirm = new System.Windows.Forms.Button();
            this.textBox_caption = new System.Windows.Forms.TextBox();
            this.textBox_sensorID = new System.Windows.Forms.TextBox();
            this.button_typeID = new System.Windows.Forms.Button();
            this.numericUpDown_decimal = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_max = new System.Windows.Forms.TextBox();
            this.textBox_min = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_unit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip_type = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_narmal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_photoGate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_heartRate = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_k = new System.Windows.Forms.TextBox();
            this.textBox_b = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_decimal)).BeginInit();
            this.contextMenuStrip_type.SuspendLayout();
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
            // textBox_caption
            // 
            resources.ApplyResources(this.textBox_caption, "textBox_caption");
            this.textBox_caption.Name = "textBox_caption";
            // 
            // textBox_sensorID
            // 
            resources.ApplyResources(this.textBox_sensorID, "textBox_sensorID");
            this.textBox_sensorID.Name = "textBox_sensorID";
            // 
            // button_typeID
            // 
            resources.ApplyResources(this.button_typeID, "button_typeID");
            this.button_typeID.Name = "button_typeID";
            this.button_typeID.UseVisualStyleBackColor = true;
            this.button_typeID.Click += new System.EventHandler(this.button_typeID_Click);
            // 
            // numericUpDown_decimal
            // 
            resources.ApplyResources(this.numericUpDown_decimal, "numericUpDown_decimal");
            this.numericUpDown_decimal.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_decimal.Name = "numericUpDown_decimal";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // textBox_max
            // 
            resources.ApplyResources(this.textBox_max, "textBox_max");
            this.textBox_max.Name = "textBox_max";
            // 
            // textBox_min
            // 
            resources.ApplyResources(this.textBox_min, "textBox_min");
            this.textBox_min.Name = "textBox_min";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBox_unit
            // 
            resources.ApplyResources(this.textBox_unit, "textBox_unit");
            this.textBox_unit.Name = "textBox_unit";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // contextMenuStrip_type
            // 
            resources.ApplyResources(this.contextMenuStrip_type, "contextMenuStrip_type");
            this.contextMenuStrip_type.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_narmal,
            this.toolStripMenuItem_photoGate,
            this.toolStripMenuItem_heartRate});
            this.contextMenuStrip_type.Name = "contextMenuStrip_type";
            // 
            // toolStripMenuItem_narmal
            // 
            this.toolStripMenuItem_narmal.Name = "toolStripMenuItem_narmal";
            resources.ApplyResources(this.toolStripMenuItem_narmal, "toolStripMenuItem_narmal");
            this.toolStripMenuItem_narmal.Click += new System.EventHandler(this.toolStripMenuItem_narmal_Click);
            // 
            // toolStripMenuItem_photoGate
            // 
            this.toolStripMenuItem_photoGate.Name = "toolStripMenuItem_photoGate";
            resources.ApplyResources(this.toolStripMenuItem_photoGate, "toolStripMenuItem_photoGate");
            this.toolStripMenuItem_photoGate.Click += new System.EventHandler(this.toolStripMenuItem_photoGate_Click);
            // 
            // toolStripMenuItem_heartRate
            // 
            this.toolStripMenuItem_heartRate.Name = "toolStripMenuItem_heartRate";
            resources.ApplyResources(this.toolStripMenuItem_heartRate, "toolStripMenuItem_heartRate");
            this.toolStripMenuItem_heartRate.Click += new System.EventHandler(this.toolStripMenuItem_heartRate_Click);
            // 
            // textBox_k
            // 
            resources.ApplyResources(this.textBox_k, "textBox_k");
            this.textBox_k.Name = "textBox_k";
            // 
            // textBox_b
            // 
            resources.ApplyResources(this.textBox_b, "textBox_b");
            this.textBox_b.Name = "textBox_b";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // FormSensorEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_b);
            this.Controls.Add(this.textBox_k);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_unit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_min);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_max);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_typeID);
            this.Controls.Add(this.numericUpDown_decimal);
            this.Controls.Add(this.textBox_sensorID);
            this.Controls.Add(this.textBox_caption);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.Name = "FormSensorEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_decimal)).EndInit();
            this.contextMenuStrip_type.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.TextBox textBox_caption;
        private System.Windows.Forms.TextBox textBox_sensorID;
        private System.Windows.Forms.Button button_typeID;
        private System.Windows.Forms.NumericUpDown numericUpDown_decimal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_max;
        private System.Windows.Forms.TextBox textBox_min;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_unit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_type;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_narmal;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_photoGate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_heartRate;
        private System.Windows.Forms.TextBox textBox_k;
        private System.Windows.Forms.TextBox textBox_b;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
    }
}