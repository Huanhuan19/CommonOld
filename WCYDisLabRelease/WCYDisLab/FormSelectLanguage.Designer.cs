namespace WCYDisLab
{
    partial class FormSelectLanguage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectLanguage));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_title = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.label_factoryName = new System.Windows.Forms.Label();
            this.label_copyRightDate = new System.Windows.Forms.Label();
            this.button_in = new System.Windows.Forms.Button();
            this.label_copyRight = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lb_Language = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button_out = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.buttonUSB_in = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_title
            // 
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label_title, "label_title");
            this.label_title.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label_title.Name = "label_title";
            // 
            // label_version
            // 
            this.label_version.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label_version, "label_version");
            this.label_version.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label_version.Name = "label_version";
            // 
            // label_factoryName
            // 
            resources.ApplyResources(this.label_factoryName, "label_factoryName");
            this.label_factoryName.Name = "label_factoryName";
            // 
            // label_copyRightDate
            // 
            resources.ApplyResources(this.label_copyRightDate, "label_copyRightDate");
            this.label_copyRightDate.Name = "label_copyRightDate";
            // 
            // button_in
            // 
            resources.ApplyResources(this.button_in, "button_in");
            this.button_in.Name = "button_in";
            this.button_in.UseVisualStyleBackColor = true;
            this.button_in.Click += new System.EventHandler(this.button_skip_Click);
            // 
            // label_copyRight
            // 
            resources.ApplyResources(this.label_copyRight, "label_copyRight");
            this.label_copyRight.Name = "label_copyRight";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1"),
            resources.GetString("comboBox1.Items2"),
            resources.GetString("comboBox1.Items3"),
            resources.GetString("comboBox1.Items4")});
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lb_Language
            // 
            resources.ApplyResources(this.lb_Language, "lb_Language");
            this.lb_Language.Name = "lb_Language";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // button_out
            // 
            resources.ApplyResources(this.button_out, "button_out");
            this.button_out.Name = "button_out";
            this.button_out.UseVisualStyleBackColor = true;
            this.button_out.Click += new System.EventHandler(this.button_out_Click);
            // 
            // comboBox2
            // 
            resources.ApplyResources(this.comboBox2, "comboBox2");
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            resources.GetString("comboBox2.Items"),
            resources.GetString("comboBox2.Items1")});
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // buttonUSB_in
            // 
            resources.ApplyResources(this.buttonUSB_in, "buttonUSB_in");
            this.buttonUSB_in.Name = "buttonUSB_in";
            this.buttonUSB_in.UseVisualStyleBackColor = true;
            this.buttonUSB_in.Click += new System.EventHandler(this.buttonUSB_in_Click);
            // 
            // FormSelectLanguage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ControlBox = false;
            this.Controls.Add(this.buttonUSB_in);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.button_out);
            this.Controls.Add(this.lb_Language);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button_in);
            this.Controls.Add(this.label_copyRightDate);
            this.Controls.Add(this.label_factoryName);
            this.Controls.Add(this.label_copyRight);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSelectLanguage";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label_factoryName;
        private System.Windows.Forms.Label label_copyRightDate;
        private System.Windows.Forms.Button button_in;
        private System.Windows.Forms.Label label_copyRight;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lb_Language;
        private System.Windows.Forms.Button button_out;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button buttonUSB_in;
    }
}