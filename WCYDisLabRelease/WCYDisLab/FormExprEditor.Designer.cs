namespace WCYDisLab
{
    partial class FormExprEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExprEditor));
            this.textBox_ex = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_formula = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_timeIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_timeStamp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_sensor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_expr = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.textBox_caption = new System.Windows.Forms.TextBox();
            this.numericUpDown_decimal = new System.Windows.Forms.NumericUpDown();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_confirm = new System.Windows.Forms.Button();
            this.textBox_min = new System.Windows.Forms.TextBox();
            this.textBox_max = new System.Windows.Forms.TextBox();
            this.textBox_unit = new System.Windows.Forms.TextBox();
            this.numericUpDown_step = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.variablePicker1 = new WCYDisLab.Controls.VariablePicker();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_decimal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_step)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_ex
            // 
            this.textBox_ex.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.textBox_ex, "textBox_ex");
            this.textBox_ex.Name = "textBox_ex";
            this.toolTip1.SetToolTip(this.textBox_ex, resources.GetString("textBox_ex.ToolTip"));
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_formula,
            this.toolStripSeparator1,
            this.toolStripMenuItem_timeIndex,
            this.toolStripMenuItem_timeStamp,
            this.toolStripSeparator2,
            this.toolStripMenuItem_sensor,
            this.toolStripMenuItem_expr});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // toolStripMenuItem_formula
            // 
            this.toolStripMenuItem_formula.Name = "toolStripMenuItem_formula";
            resources.ApplyResources(this.toolStripMenuItem_formula, "toolStripMenuItem_formula");
            this.toolStripMenuItem_formula.Click += new System.EventHandler(this.toolStripMenuItem_formula_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItem_timeIndex
            // 
            this.toolStripMenuItem_timeIndex.Name = "toolStripMenuItem_timeIndex";
            resources.ApplyResources(this.toolStripMenuItem_timeIndex, "toolStripMenuItem_timeIndex");
            this.toolStripMenuItem_timeIndex.Click += new System.EventHandler(this.toolStripMenuItem_timeIndex_Click);
            // 
            // toolStripMenuItem_timeStamp
            // 
            this.toolStripMenuItem_timeStamp.Name = "toolStripMenuItem_timeStamp";
            resources.ApplyResources(this.toolStripMenuItem_timeStamp, "toolStripMenuItem_timeStamp");
            this.toolStripMenuItem_timeStamp.Click += new System.EventHandler(this.toolStripMenuItem_timeStamp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripMenuItem_sensor
            // 
            this.toolStripMenuItem_sensor.Name = "toolStripMenuItem_sensor";
            resources.ApplyResources(this.toolStripMenuItem_sensor, "toolStripMenuItem_sensor");
            this.toolStripMenuItem_sensor.Click += new System.EventHandler(this.toolStripMenuItem_sensor_Click);
            // 
            // toolStripMenuItem_expr
            // 
            this.toolStripMenuItem_expr.Name = "toolStripMenuItem_expr";
            resources.ApplyResources(this.toolStripMenuItem_expr, "toolStripMenuItem_expr");
            this.toolStripMenuItem_expr.Click += new System.EventHandler(this.toolStripMenuItem_expr_Click);
            // 
            // textBox_description
            // 
            resources.ApplyResources(this.textBox_description, "textBox_description");
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.ReadOnly = true;
            // 
            // textBox_caption
            // 
            resources.ApplyResources(this.textBox_caption, "textBox_caption");
            this.textBox_caption.Name = "textBox_caption";
            // 
            // numericUpDown_decimal
            // 
            resources.ApplyResources(this.numericUpDown_decimal, "numericUpDown_decimal");
            this.numericUpDown_decimal.Name = "numericUpDown_decimal";
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
            // textBox_min
            // 
            resources.ApplyResources(this.textBox_min, "textBox_min");
            this.textBox_min.Name = "textBox_min";
            // 
            // textBox_max
            // 
            resources.ApplyResources(this.textBox_max, "textBox_max");
            this.textBox_max.Name = "textBox_max";
            // 
            // textBox_unit
            // 
            resources.ApplyResources(this.textBox_unit, "textBox_unit");
            this.textBox_unit.Name = "textBox_unit";
            // 
            // numericUpDown_step
            // 
            resources.ApplyResources(this.numericUpDown_step, "numericUpDown_step");
            this.numericUpDown_step.Name = "numericUpDown_step";
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "操作提示";
            // 
            // variablePicker1
            // 
            resources.ApplyResources(this.variablePicker1, "variablePicker1");
            this.variablePicker1.Name = "variablePicker1";
            // 
            // FormExprEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_step);
            this.Controls.Add(this.textBox_unit);
            this.Controls.Add(this.textBox_max);
            this.Controls.Add(this.textBox_min);
            this.Controls.Add(this.variablePicker1);
            this.Controls.Add(this.numericUpDown_decimal);
            this.Controls.Add(this.textBox_caption);
            this.Controls.Add(this.textBox_description);
            this.Controls.Add(this.textBox_ex);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.Name = "FormExprEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_decimal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_step)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.TextBox textBox_ex;
        private System.Windows.Forms.TextBox textBox_description;
        private System.Windows.Forms.TextBox textBox_caption;
        private System.Windows.Forms.NumericUpDown numericUpDown_decimal;
        private WCYDisLab.Controls.VariablePicker variablePicker1;
        private System.Windows.Forms.TextBox textBox_min;
        private System.Windows.Forms.TextBox textBox_max;
        private System.Windows.Forms.TextBox textBox_unit;
        private System.Windows.Forms.NumericUpDown numericUpDown_step;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_formula;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_timeIndex;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_timeStamp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_sensor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_expr;
    }
}