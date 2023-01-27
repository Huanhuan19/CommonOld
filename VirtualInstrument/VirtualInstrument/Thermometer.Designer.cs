namespace VirtualInstrument
{
    partial class Thermometer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Thermometer));
            this.label_Caption = new System.Windows.Forms.Label();
            this.pictureBox_Needle = new System.Windows.Forms.PictureBox();
            this.label_r4 = new System.Windows.Forms.Label();
            this.label_r3 = new System.Windows.Forms.Label();
            this.label_r2 = new System.Windows.Forms.Label();
            this.label_r1 = new System.Windows.Forms.Label();
            this.label_l4 = new System.Windows.Forms.Label();
            this.label_l3 = new System.Windows.Forms.Label();
            this.label_r0 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_l2 = new System.Windows.Forms.Label();
            this.label_l1 = new System.Windows.Forms.Label();
            this.label_l0 = new System.Windows.Forms.Label();
            this.label_UnitName2 = new System.Windows.Forms.Label();
            this.label_UnitName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Needle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Caption
            // 
            this.label_Caption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_Caption.Location = new System.Drawing.Point(8, 478);
            this.label_Caption.Name = "label_Caption";
            this.label_Caption.Size = new System.Drawing.Size(176, 23);
            this.label_Caption.TabIndex = 29;
            this.label_Caption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox_Needle
            // 
            this.pictureBox_Needle.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Needle.Image")));
            this.pictureBox_Needle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox_Needle.Location = new System.Drawing.Point(87, 372);
            this.pictureBox_Needle.Name = "pictureBox_Needle";
            this.pictureBox_Needle.Size = new System.Drawing.Size(19, 50);
            this.pictureBox_Needle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Needle.TabIndex = 28;
            this.pictureBox_Needle.TabStop = false;
            // 
            // label_r4
            // 
            this.label_r4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_r4.Location = new System.Drawing.Point(140, 30);
            this.label_r4.Name = "label_r4";
            this.label_r4.Size = new System.Drawing.Size(44, 23);
            this.label_r4.TabIndex = 27;
            this.label_r4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_r3
            // 
            this.label_r3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_r3.Location = new System.Drawing.Point(140, 125);
            this.label_r3.Name = "label_r3";
            this.label_r3.Size = new System.Drawing.Size(44, 23);
            this.label_r3.TabIndex = 26;
            this.label_r3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_r2
            // 
            this.label_r2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_r2.Location = new System.Drawing.Point(140, 219);
            this.label_r2.Name = "label_r2";
            this.label_r2.Size = new System.Drawing.Size(44, 23);
            this.label_r2.TabIndex = 25;
            this.label_r2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_r1
            // 
            this.label_r1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_r1.Location = new System.Drawing.Point(140, 314);
            this.label_r1.Name = "label_r1";
            this.label_r1.Size = new System.Drawing.Size(44, 23);
            this.label_r1.TabIndex = 24;
            this.label_r1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_l4
            // 
            this.label_l4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_l4.Location = new System.Drawing.Point(8, 30);
            this.label_l4.Name = "label_l4";
            this.label_l4.Size = new System.Drawing.Size(44, 23);
            this.label_l4.TabIndex = 22;
            this.label_l4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_l3
            // 
            this.label_l3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_l3.Location = new System.Drawing.Point(8, 125);
            this.label_l3.Name = "label_l3";
            this.label_l3.Size = new System.Drawing.Size(44, 23);
            this.label_l3.TabIndex = 21;
            this.label_l3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_r0
            // 
            this.label_r0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_r0.Location = new System.Drawing.Point(140, 409);
            this.label_r0.Name = "label_r0";
            this.label_r0.Size = new System.Drawing.Size(44, 23);
            this.label_r0.TabIndex = 23;
            this.label_r0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_l2
            // 
            this.label_l2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_l2.Location = new System.Drawing.Point(8, 219);
            this.label_l2.Name = "label_l2";
            this.label_l2.Size = new System.Drawing.Size(44, 23);
            this.label_l2.TabIndex = 20;
            this.label_l2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_l1
            // 
            this.label_l1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_l1.Location = new System.Drawing.Point(8, 314);
            this.label_l1.Name = "label_l1";
            this.label_l1.Size = new System.Drawing.Size(44, 23);
            this.label_l1.TabIndex = 19;
            this.label_l1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_l0
            // 
            this.label_l0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_l0.Location = new System.Drawing.Point(8, 409);
            this.label_l0.Name = "label_l0";
            this.label_l0.Size = new System.Drawing.Size(44, 23);
            this.label_l0.TabIndex = 18;
            this.label_l0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_UnitName2
            // 
            this.label_UnitName2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label_UnitName2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_UnitName2.Location = new System.Drawing.Point(142, 4);
            this.label_UnitName2.Name = "label_UnitName2";
            this.label_UnitName2.Size = new System.Drawing.Size(46, 26);
            this.label_UnitName2.TabIndex = 17;
            this.label_UnitName2.Text = "℉";
            this.label_UnitName2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_UnitName
            // 
            this.label_UnitName.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label_UnitName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_UnitName.Location = new System.Drawing.Point(4, 4);
            this.label_UnitName.Name = "label_UnitName";
            this.label_UnitName.Size = new System.Drawing.Size(48, 26);
            this.label_UnitName.TabIndex = 16;
            this.label_UnitName.Text = "℃";
            this.label_UnitName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(58, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 435);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // Thermometer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Caption);
            this.Controls.Add(this.pictureBox_Needle);
            this.Controls.Add(this.label_r4);
            this.Controls.Add(this.label_r3);
            this.Controls.Add(this.label_r2);
            this.Controls.Add(this.label_r1);
            this.Controls.Add(this.label_l4);
            this.Controls.Add(this.label_l3);
            this.Controls.Add(this.label_r0);
            this.Controls.Add(this.label_l2);
            this.Controls.Add(this.label_l1);
            this.Controls.Add(this.label_l0);
            this.Controls.Add(this.label_UnitName2);
            this.Controls.Add(this.label_UnitName);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Thermometer";
            this.Size = new System.Drawing.Size(192, 505);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Needle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Caption;
        private System.Windows.Forms.PictureBox pictureBox_Needle;
        private System.Windows.Forms.Label label_r4;
        private System.Windows.Forms.Label label_r3;
        private System.Windows.Forms.Label label_r2;
        private System.Windows.Forms.Label label_r1;
        private System.Windows.Forms.Label label_l4;
        private System.Windows.Forms.Label label_l3;
        private System.Windows.Forms.Label label_r0;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_l2;
        private System.Windows.Forms.Label label_l1;
        private System.Windows.Forms.Label label_l0;
        private System.Windows.Forms.Label label_UnitName2;
        private System.Windows.Forms.Label label_UnitName;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}
