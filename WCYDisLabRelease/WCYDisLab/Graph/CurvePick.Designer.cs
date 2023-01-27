namespace WCYDisLab.Graph
{
    partial class CurvePick
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurvePick));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView_yAxis = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView_variables = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listView_y2Axis = new System.Windows.Forms.ListView();
            this.button_editYAxisCurve = new System.Windows.Forms.Button();
            this.button_editY2AxisCurve = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_removeFromY2Axis = new System.Windows.Forms.Button();
            this.button_add2Y2Axis = new System.Windows.Forms.Button();
            this.button_removeFromYAxis = new System.Windows.Forms.Button();
            this.button_add2YAxis = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.listView_yAxis);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // listView_yAxis
            // 
            resources.ApplyResources(this.listView_yAxis, "listView_yAxis");
            this.listView_yAxis.FullRowSelect = true;
            this.listView_yAxis.GridLines = true;
            this.listView_yAxis.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listView_yAxis.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listView_yAxis.Groups1")))});
            this.listView_yAxis.HideSelection = false;
            this.listView_yAxis.Name = "listView_yAxis";
            this.listView_yAxis.UseCompatibleStateImageBehavior = false;
            this.listView_yAxis.View = System.Windows.Forms.View.SmallIcon;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.listView_variables);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // listView_variables
            // 
            resources.ApplyResources(this.listView_variables, "listView_variables");
            this.listView_variables.FullRowSelect = true;
            this.listView_variables.GridLines = true;
            this.listView_variables.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listView_variables.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listView_variables.Groups1")))});
            this.listView_variables.HideSelection = false;
            this.listView_variables.Name = "listView_variables";
            this.listView_variables.UseCompatibleStateImageBehavior = false;
            this.listView_variables.View = System.Windows.Forms.View.SmallIcon;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.listView_y2Axis);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // listView_y2Axis
            // 
            resources.ApplyResources(this.listView_y2Axis, "listView_y2Axis");
            this.listView_y2Axis.FullRowSelect = true;
            this.listView_y2Axis.GridLines = true;
            this.listView_y2Axis.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listView_y2Axis.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listView_y2Axis.Groups1")))});
            this.listView_y2Axis.HideSelection = false;
            this.listView_y2Axis.Name = "listView_y2Axis";
            this.listView_y2Axis.UseCompatibleStateImageBehavior = false;
            this.listView_y2Axis.View = System.Windows.Forms.View.SmallIcon;
            // 
            // button_editYAxisCurve
            // 
            resources.ApplyResources(this.button_editYAxisCurve, "button_editYAxisCurve");
            this.button_editYAxisCurve.Name = "button_editYAxisCurve";
            this.toolTip1.SetToolTip(this.button_editYAxisCurve, resources.GetString("button_editYAxisCurve.ToolTip"));
            this.button_editYAxisCurve.UseVisualStyleBackColor = true;
            this.button_editYAxisCurve.Click += new System.EventHandler(this.button_editYAxisCurve_Click);
            // 
            // button_editY2AxisCurve
            // 
            resources.ApplyResources(this.button_editY2AxisCurve, "button_editY2AxisCurve");
            this.button_editY2AxisCurve.Name = "button_editY2AxisCurve";
            this.toolTip1.SetToolTip(this.button_editY2AxisCurve, resources.GetString("button_editY2AxisCurve.ToolTip"));
            this.button_editY2AxisCurve.UseVisualStyleBackColor = true;
            this.button_editY2AxisCurve.Click += new System.EventHandler(this.button_editY2AxisCurve_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "选择数据";
            // 
            // button_removeFromY2Axis
            // 
            this.button_removeFromY2Axis.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_removeFromY2Axis, "button_removeFromY2Axis");
            this.button_removeFromY2Axis.Image = global::WCYDisLab.Properties.Resources.chinaz55;
            this.button_removeFromY2Axis.Name = "button_removeFromY2Axis";
            this.toolTip1.SetToolTip(this.button_removeFromY2Axis, resources.GetString("button_removeFromY2Axis.ToolTip"));
            this.button_removeFromY2Axis.UseVisualStyleBackColor = true;
            this.button_removeFromY2Axis.Click += new System.EventHandler(this.button_removeFromY2Axis_Click);
            // 
            // button_add2Y2Axis
            // 
            this.button_add2Y2Axis.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_add2Y2Axis, "button_add2Y2Axis");
            this.button_add2Y2Axis.Image = global::WCYDisLab.Properties.Resources.chinaz56;
            this.button_add2Y2Axis.Name = "button_add2Y2Axis";
            this.toolTip1.SetToolTip(this.button_add2Y2Axis, resources.GetString("button_add2Y2Axis.ToolTip"));
            this.button_add2Y2Axis.UseVisualStyleBackColor = true;
            this.button_add2Y2Axis.Click += new System.EventHandler(this.button_add2Y2Axis_Click);
            // 
            // button_removeFromYAxis
            // 
            this.button_removeFromYAxis.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_removeFromYAxis, "button_removeFromYAxis");
            this.button_removeFromYAxis.Image = global::WCYDisLab.Properties.Resources.chinaz56;
            this.button_removeFromYAxis.Name = "button_removeFromYAxis";
            this.toolTip1.SetToolTip(this.button_removeFromYAxis, resources.GetString("button_removeFromYAxis.ToolTip"));
            this.button_removeFromYAxis.UseVisualStyleBackColor = true;
            this.button_removeFromYAxis.Click += new System.EventHandler(this.button_removeFromYAxis_Click);
            // 
            // button_add2YAxis
            // 
            this.button_add2YAxis.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_add2YAxis, "button_add2YAxis");
            this.button_add2YAxis.Image = global::WCYDisLab.Properties.Resources.chinaz55;
            this.button_add2YAxis.Name = "button_add2YAxis";
            this.toolTip1.SetToolTip(this.button_add2YAxis, resources.GetString("button_add2YAxis.ToolTip"));
            this.button_add2YAxis.UseVisualStyleBackColor = true;
            this.button_add2YAxis.Click += new System.EventHandler(this.button_add2YAxis_Click);
            // 
            // CurvePick
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_editY2AxisCurve);
            this.Controls.Add(this.button_editYAxisCurve);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button_removeFromY2Axis);
            this.Controls.Add(this.button_add2Y2Axis);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_removeFromYAxis);
            this.Controls.Add(this.button_add2YAxis);
            this.Controls.Add(this.groupBox1);
            this.Name = "CurvePick";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView_yAxis;
        private System.Windows.Forms.Button button_add2YAxis;
        private System.Windows.Forms.Button button_removeFromYAxis;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView_variables;
        private System.Windows.Forms.Button button_removeFromY2Axis;
        private System.Windows.Forms.Button button_add2Y2Axis;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView listView_y2Axis;
        private System.Windows.Forms.Button button_editYAxisCurve;
        private System.Windows.Forms.Button button_editY2AxisCurve;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
