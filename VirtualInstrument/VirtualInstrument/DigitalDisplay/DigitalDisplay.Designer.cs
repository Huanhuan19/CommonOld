namespace VirtualInstrument.DigitalDisplay
{
    partial class DigitalDisplay
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
            this.components = new System.ComponentModel.Container();
            this.label_recordValue = new System.Windows.Forms.Label();
            this.label_recordValuePrev1 = new System.Windows.Forms.Label();
            this.label_recordValuePrev2 = new System.Windows.Forms.Label();
            this.label_currentValue = new System.Windows.Forms.Label();
            this.label_currentValuePrev1 = new System.Windows.Forms.Label();
            this.label_currentValuePrev2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_caption = new System.Windows.Forms.Label();
            this.label_unit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_recordValue
            // 
            this.label_recordValue.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_recordValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_recordValue.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.label_recordValue.Location = new System.Drawing.Point(313, 82);
            this.label_recordValue.Name = "label_recordValue";
            this.label_recordValue.Size = new System.Drawing.Size(200, 58);
            this.label_recordValue.TabIndex = 0;
            this.label_recordValue.Text = "label1";
            this.label_recordValue.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_recordValuePrev1
            // 
            this.label_recordValuePrev1.BackColor = System.Drawing.SystemColors.Control;
            this.label_recordValuePrev1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_recordValuePrev1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label_recordValuePrev1.Location = new System.Drawing.Point(145, 113);
            this.label_recordValuePrev1.Name = "label_recordValuePrev1";
            this.label_recordValuePrev1.Size = new System.Drawing.Size(160, 27);
            this.label_recordValuePrev1.TabIndex = 1;
            this.label_recordValuePrev1.Text = "label1";
            this.label_recordValuePrev1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_recordValuePrev2
            // 
            this.label_recordValuePrev2.BackColor = System.Drawing.SystemColors.Control;
            this.label_recordValuePrev2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_recordValuePrev2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label_recordValuePrev2.Location = new System.Drawing.Point(3, 113);
            this.label_recordValuePrev2.Name = "label_recordValuePrev2";
            this.label_recordValuePrev2.Size = new System.Drawing.Size(134, 27);
            this.label_recordValuePrev2.TabIndex = 2;
            this.label_recordValuePrev2.Text = "label1";
            this.label_recordValuePrev2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_currentValue
            // 
            this.label_currentValue.BackColor = System.Drawing.SystemColors.Control;
            this.label_currentValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_currentValue.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.label_currentValue.Location = new System.Drawing.Point(313, 13);
            this.label_currentValue.Name = "label_currentValue";
            this.label_currentValue.Size = new System.Drawing.Size(200, 58);
            this.label_currentValue.TabIndex = 3;
            this.label_currentValue.Text = "label1";
            this.label_currentValue.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_currentValuePrev1
            // 
            this.label_currentValuePrev1.BackColor = System.Drawing.SystemColors.Control;
            this.label_currentValuePrev1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_currentValuePrev1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label_currentValuePrev1.Location = new System.Drawing.Point(145, 45);
            this.label_currentValuePrev1.Name = "label_currentValuePrev1";
            this.label_currentValuePrev1.Size = new System.Drawing.Size(160, 27);
            this.label_currentValuePrev1.TabIndex = 4;
            this.label_currentValuePrev1.Text = "label1";
            this.label_currentValuePrev1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_currentValuePrev2
            // 
            this.label_currentValuePrev2.BackColor = System.Drawing.SystemColors.Control;
            this.label_currentValuePrev2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_currentValuePrev2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label_currentValuePrev2.Location = new System.Drawing.Point(3, 45);
            this.label_currentValuePrev2.Name = "label_currentValuePrev2";
            this.label_currentValuePrev2.Size = new System.Drawing.Size(134, 27);
            this.label_currentValuePrev2.TabIndex = 5;
            this.label_currentValuePrev2.Text = "label1";
            this.label_currentValuePrev2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_caption
            // 
            this.label_caption.AutoSize = true;
            this.label_caption.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_caption.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_caption.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label_caption.Location = new System.Drawing.Point(6, 13);
            this.label_caption.Name = "label_caption";
            this.label_caption.Size = new System.Drawing.Size(62, 16);
            this.label_caption.TabIndex = 6;
            this.label_caption.Text = "label1";
            // 
            // label_unit
            // 
            this.label_unit.AutoSize = true;
            this.label_unit.Location = new System.Drawing.Point(521, 123);
            this.label_unit.Name = "label_unit";
            this.label_unit.Size = new System.Drawing.Size(45, 14);
            this.label_unit.TabIndex = 7;
            this.label_unit.Text = "label1";
            // 
            // DigitalDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_unit);
            this.Controls.Add(this.label_caption);
            this.Controls.Add(this.label_currentValuePrev2);
            this.Controls.Add(this.label_currentValuePrev1);
            this.Controls.Add(this.label_currentValue);
            this.Controls.Add(this.label_recordValuePrev2);
            this.Controls.Add(this.label_recordValuePrev1);
            this.Controls.Add(this.label_recordValue);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DigitalDisplay";
            this.Size = new System.Drawing.Size(600, 159);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_recordValue;
        private System.Windows.Forms.Label label_recordValuePrev1;
        private System.Windows.Forms.Label label_recordValuePrev2;
        private System.Windows.Forms.Label label_currentValue;
        private System.Windows.Forms.Label label_currentValuePrev1;
        private System.Windows.Forms.Label label_currentValuePrev2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_caption;
        private System.Windows.Forms.Label label_unit;
    }
}
