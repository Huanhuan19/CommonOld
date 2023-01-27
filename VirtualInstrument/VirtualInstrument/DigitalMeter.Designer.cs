namespace VirtualInstrument
{
    partial class DigitalMeter
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_value = new System.Windows.Forms.Label();
            this.label_caption = new System.Windows.Forms.Label();
            this.label_unit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_value
            // 
            this.label_value.AutoEllipsis = true;
            this.label_value.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_value.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_value.Location = new System.Drawing.Point(0, 0);
            this.label_value.Name = "label_value";
            this.label_value.Size = new System.Drawing.Size(251, 98);
            this.label_value.TabIndex = 0;
            this.label_value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_caption
            // 
            this.label_caption.AutoSize = true;
            this.label_caption.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_caption.Location = new System.Drawing.Point(3, 0);
            this.label_caption.Name = "label_caption";
            this.label_caption.Size = new System.Drawing.Size(0, 14);
            this.label_caption.TabIndex = 1;
            this.label_caption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_unit
            // 
            this.label_unit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_unit.AutoSize = true;
            this.label_unit.Location = new System.Drawing.Point(248, 0);
            this.label_unit.Name = "label_unit";
            this.label_unit.Size = new System.Drawing.Size(0, 14);
            this.label_unit.TabIndex = 2;
            this.label_unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DigitalMeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_unit);
            this.Controls.Add(this.label_caption);
            this.Controls.Add(this.label_value);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DigitalMeter";
            this.Size = new System.Drawing.Size(251, 98);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_value;
        private System.Windows.Forms.Label label_caption;
        private System.Windows.Forms.Label label_unit;
    }
}
