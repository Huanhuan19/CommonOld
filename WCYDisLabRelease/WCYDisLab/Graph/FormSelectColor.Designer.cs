namespace WCYDisLab.Graph
{
    partial class FormSelectColor
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
            this.button_red = new System.Windows.Forms.Button();
            this.button_green = new System.Windows.Forms.Button();
            this.button_blue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_red
            // 
            this.button_red.BackColor = System.Drawing.Color.Red;
            this.button_red.Location = new System.Drawing.Point(81, 15);
            this.button_red.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_red.Name = "button_red";
            this.button_red.Size = new System.Drawing.Size(114, 79);
            this.button_red.TabIndex = 0;
            this.button_red.UseVisualStyleBackColor = false;
            this.button_red.Click += new System.EventHandler(this.button_red_Click);
            // 
            // button_green
            // 
            this.button_green.BackColor = System.Drawing.Color.Lime;
            this.button_green.Location = new System.Drawing.Point(18, 102);
            this.button_green.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_green.Name = "button_green";
            this.button_green.Size = new System.Drawing.Size(114, 79);
            this.button_green.TabIndex = 1;
            this.button_green.UseVisualStyleBackColor = false;
            this.button_green.Click += new System.EventHandler(this.button_green_Click);
            // 
            // button_blue
            // 
            this.button_blue.BackColor = System.Drawing.Color.Blue;
            this.button_blue.Location = new System.Drawing.Point(142, 102);
            this.button_blue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_blue.Name = "button_blue";
            this.button_blue.Size = new System.Drawing.Size(114, 79);
            this.button_blue.TabIndex = 2;
            this.button_blue.UseVisualStyleBackColor = false;
            this.button_blue.Click += new System.EventHandler(this.button_blue_Click);
            // 
            // FormSelectColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 181);
            this.ControlBox = false;
            this.Controls.Add(this.button_blue);
            this.Controls.Add(this.button_green);
            this.Controls.Add(this.button_red);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormSelectColor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_red;
        private System.Windows.Forms.Button button_green;
        private System.Windows.Forms.Button button_blue;
    }
}