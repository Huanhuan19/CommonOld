namespace WCYDisLab.Controls
{
    partial class ConstantControler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConstantControler));
            this.label_caption = new System.Windows.Forms.Label();
            this.textBox_current = new System.Windows.Forms.TextBox();
            this.textBox_manual = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button_modify = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label_caption
            // 
            this.label_caption.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_caption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_caption.Location = new System.Drawing.Point(0, 0);
            this.label_caption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_caption.Name = "label_caption";
            this.label_caption.Size = new System.Drawing.Size(161, 29);
            this.label_caption.TabIndex = 0;
            this.label_caption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_current
            // 
            this.textBox_current.Location = new System.Drawing.Point(4, 33);
            this.textBox_current.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_current.Name = "textBox_current";
            this.textBox_current.ReadOnly = true;
            this.textBox_current.Size = new System.Drawing.Size(151, 22);
            this.textBox_current.TabIndex = 1;
            // 
            // textBox_manual
            // 
            this.textBox_manual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_manual.Location = new System.Drawing.Point(4, 243);
            this.textBox_manual.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_manual.Name = "textBox_manual";
            this.textBox_manual.Size = new System.Drawing.Size(151, 22);
            this.textBox_manual.TabIndex = 3;
            this.textBox_manual.TextChanged += new System.EventHandler(this.textBox_manual_TextChanged);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(4, 66);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(151, 132);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "值";
            this.columnHeader1.Width = 90;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Unselected");
            this.imageList1.Images.SetKeyName(1, "Selected");
            // 
            // button_modify
            // 
            this.button_modify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_modify.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button_modify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_modify.Image = ((System.Drawing.Image)(resources.GetObject("button_modify.Image")));
            this.button_modify.Location = new System.Drawing.Point(64, 206);
            this.button_modify.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_modify.Name = "button_modify";
            this.button_modify.Size = new System.Drawing.Size(36, 29);
            this.button_modify.TabIndex = 5;
            this.button_modify.UseVisualStyleBackColor = true;
            this.button_modify.Click += new System.EventHandler(this.button_modify_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ConstantControler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button_modify);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox_manual);
            this.Controls.Add(this.textBox_current);
            this.Controls.Add(this.label_caption);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ConstantControler";
            this.Size = new System.Drawing.Size(161, 273);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_caption;
        private System.Windows.Forms.TextBox textBox_current;
        private System.Windows.Forms.TextBox textBox_manual;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button_modify;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timer1;

    }
}
