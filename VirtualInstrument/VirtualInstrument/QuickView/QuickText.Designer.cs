namespace VirtualInstrument.QuickView
{
    partial class QuickText
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
            this.textBox_caption = new System.Windows.Forms.TextBox();
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.textBox_unit = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // textBox_caption
            // 
            this.textBox_caption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_caption.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_caption.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_caption.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_caption.Location = new System.Drawing.Point(3, 4);
            this.textBox_caption.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_caption.Name = "textBox_caption";
            this.textBox_caption.ReadOnly = true;
            this.textBox_caption.Size = new System.Drawing.Size(223, 28);
            this.textBox_caption.TabIndex = 0;
            this.textBox_caption.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_value
            // 
            this.textBox_value.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_value.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_value.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_value.Location = new System.Drawing.Point(234, 0);
            this.textBox_value.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.ReadOnly = true;
            this.textBox_value.Size = new System.Drawing.Size(212, 35);
            this.textBox_value.TabIndex = 1;
            this.textBox_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_unit
            // 
            this.textBox_unit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_unit.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_unit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_unit.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_unit.Location = new System.Drawing.Point(456, 4);
            this.textBox_unit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_unit.Name = "textBox_unit";
            this.textBox_unit.ReadOnly = true;
            this.textBox_unit.Size = new System.Drawing.Size(118, 28);
            this.textBox_unit.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // QuickText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox_unit);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.textBox_caption);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "QuickText";
            this.Size = new System.Drawing.Size(577, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_caption;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.TextBox textBox_unit;
        private System.Windows.Forms.Timer timer1;
    }
}
