namespace VirtualInstrument
{
    partial class Performance
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
            this.label_caption = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.label_value = new System.Windows.Forms.Label();
            this.label_unit = new System.Windows.Forms.Label();
            this.label_scope = new System.Windows.Forms.Label();
            this.uvMeter1 = new VirtualInstrument.UVMeter();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_caption
            // 
            this.label_caption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_caption.BackColor = System.Drawing.SystemColors.Control;
            this.label_caption.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_caption.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_caption.Location = new System.Drawing.Point(0, 0);
            this.label_caption.Name = "label_caption";
            this.label_caption.Size = new System.Drawing.Size(139, 33);
            this.label_caption.TabIndex = 18;
            this.label_caption.Text = "test";
            this.label_caption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.IsEnableHPan = false;
            this.zedGraphControl1.IsEnableHZoom = false;
            this.zedGraphControl1.IsEnableVPan = false;
            this.zedGraphControl1.IsEnableVZoom = false;
            this.zedGraphControl1.IsShowContextMenu = false;
            this.zedGraphControl1.IsShowCopyMessage = false;
            this.zedGraphControl1.Location = new System.Drawing.Point(0, 73);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            this.zedGraphControl1.Size = new System.Drawing.Size(191, 103);
            this.zedGraphControl1.TabIndex = 29;
            this.zedGraphControl1.Visible = false;
            // 
            // label_value
            // 
            this.label_value.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_value.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_value.Location = new System.Drawing.Point(0, 73);
            this.label_value.Name = "label_value";
            this.label_value.Size = new System.Drawing.Size(191, 103);
            this.label_value.TabIndex = 30;
            this.label_value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_unit
            // 
            this.label_unit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label_unit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_unit.Location = new System.Drawing.Point(135, 0);
            this.label_unit.Name = "label_unit";
            this.label_unit.Size = new System.Drawing.Size(58, 33);
            this.label_unit.TabIndex = 33;
            this.label_unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_scope
            // 
            this.label_scope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_scope.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_scope.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_scope.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_scope.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_scope.Location = new System.Drawing.Point(-1, 33);
            this.label_scope.Name = "label_scope";
            this.label_scope.Size = new System.Drawing.Size(194, 36);
            this.label_scope.TabIndex = 34;
            this.label_scope.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uvMeter1
            // 
            this.uvMeter1.BackColor = System.Drawing.SystemColors.Control;
            this.uvMeter1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.uvMeter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.uvMeter1.Caption = "";
            this.uvMeter1.CaptionColor = System.Drawing.Color.Black;
            this.uvMeter1.EndAngle = -120F;
            this.uvMeter1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uvMeter1.Location = new System.Drawing.Point(-1, 73);
            this.uvMeter1.MainScaleCount = 4;
            this.uvMeter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uvMeter1.MaxValue = 10;
            this.uvMeter1.MeterBackColor = System.Drawing.SystemColors.Control;
            this.uvMeter1.MinorScaleCount = 5;
            this.uvMeter1.MinValue = -10;
            this.uvMeter1.Name = "uvMeter1";
            this.uvMeter1.NeedleColor = System.Drawing.Color.Red;
            this.uvMeter1.OriginPointColor = System.Drawing.Color.Red;
            this.uvMeter1.ScaleAuto = false;
            this.uvMeter1.ScaleColor = System.Drawing.Color.Black;
            this.uvMeter1.Size = new System.Drawing.Size(190, 102);
            this.uvMeter1.StartAngle = -240F;
            this.uvMeter1.TabIndex = 35;
            this.uvMeter1.Unit = "";
            this.uvMeter1.UnitColor = System.Drawing.Color.Black;
            this.uvMeter1.Value = 0;
            this.uvMeter1.Visible = false;
            // 
            // Performance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.uvMeter1);
            this.Controls.Add(this.label_scope);
            this.Controls.Add(this.label_unit);
            this.Controls.Add(this.label_value);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.label_caption);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Performance";
            this.Size = new System.Drawing.Size(198, 178);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_caption;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Label label_value;
        private System.Windows.Forms.Label label_unit;
        private System.Windows.Forms.Label label_scope;
        private UVMeter uvMeter1;
    }
}
