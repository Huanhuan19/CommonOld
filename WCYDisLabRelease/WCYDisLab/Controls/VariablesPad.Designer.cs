namespace WCYDisLab.Controls
{
    partial class VariablesPad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariablesPad));
            this.contextMenuStrip_add = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_addSensor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_addConstant = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_addExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip_sensor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_removeSensor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_shiftSetSensor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_popGraphSensor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_popTableSensor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_popDigitalMeterSensor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_popMeterSensor = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_constant = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_editConstant = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_removeConstant = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_monitorConstant = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_expr = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_editExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_removeExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_moveUpExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_moveDownExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_popGraphExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_popTableExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_popDigitalMeterExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_popMeterExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip_add.SuspendLayout();
            this.contextMenuStrip_sensor.SuspendLayout();
            this.contextMenuStrip_constant.SuspendLayout();
            this.contextMenuStrip_expr.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip_add
            // 
            resources.ApplyResources(this.contextMenuStrip_add, "contextMenuStrip_add");
            this.contextMenuStrip_add.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_addSensor,
            this.toolStripMenuItem_addConstant,
            this.toolStripMenuItem_addExpr});
            this.contextMenuStrip_add.Name = "contextMenuStrip_add";
            // 
            // toolStripMenuItem_addSensor
            // 
            this.toolStripMenuItem_addSensor.Name = "toolStripMenuItem_addSensor";
            resources.ApplyResources(this.toolStripMenuItem_addSensor, "toolStripMenuItem_addSensor");
            this.toolStripMenuItem_addSensor.Click += new System.EventHandler(this.toolStripMenuItem_addSensor_Click);
            // 
            // toolStripMenuItem_addConstant
            // 
            this.toolStripMenuItem_addConstant.Name = "toolStripMenuItem_addConstant";
            resources.ApplyResources(this.toolStripMenuItem_addConstant, "toolStripMenuItem_addConstant");
            this.toolStripMenuItem_addConstant.Click += new System.EventHandler(this.toolStripMenuItem_addConstant_Click);
            // 
            // toolStripMenuItem_addExpr
            // 
            this.toolStripMenuItem_addExpr.Name = "toolStripMenuItem_addExpr";
            resources.ApplyResources(this.toolStripMenuItem_addExpr, "toolStripMenuItem_addExpr");
            this.toolStripMenuItem_addExpr.Click += new System.EventHandler(this.toolStripMenuItem_addExpr_Click);
            // 
            // listView1
            // 
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.FullRowSelect = true;
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listView1.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listView1.Groups1"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listView1.Groups2")))});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.SmallImageList = this.imageList1;
            this.toolTip1.SetToolTip(this.listView1, resources.GetString("listView1.ToolTip"));
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "sensor");
            this.imageList1.Images.SetKeyName(1, "constant");
            this.imageList1.Images.SetKeyName(2, "expr");
            this.imageList1.Images.SetKeyName(3, "warning");
            // 
            // contextMenuStrip_sensor
            // 
            resources.ApplyResources(this.contextMenuStrip_sensor, "contextMenuStrip_sensor");
            this.contextMenuStrip_sensor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_removeSensor,
            this.toolStripSeparator2,
            this.toolStripMenuItem_shiftSetSensor,
            this.toolStripSeparator4,
            this.toolStripMenuItem_popGraphSensor,
            this.toolStripMenuItem_popTableSensor,
            this.toolStripMenuItem_popDigitalMeterSensor,
            this.toolStripMenuItem_popMeterSensor});
            this.contextMenuStrip_sensor.Name = "contextMenuStrip_sensor";
            // 
            // toolStripMenuItem_removeSensor
            // 
            this.toolStripMenuItem_removeSensor.Name = "toolStripMenuItem_removeSensor";
            resources.ApplyResources(this.toolStripMenuItem_removeSensor, "toolStripMenuItem_removeSensor");
            this.toolStripMenuItem_removeSensor.Click += new System.EventHandler(this.toolStripMenuItem_removeSensor_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripMenuItem_shiftSetSensor
            // 
            this.toolStripMenuItem_shiftSetSensor.Name = "toolStripMenuItem_shiftSetSensor";
            resources.ApplyResources(this.toolStripMenuItem_shiftSetSensor, "toolStripMenuItem_shiftSetSensor");
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripMenuItem_popGraphSensor
            // 
            this.toolStripMenuItem_popGraphSensor.Name = "toolStripMenuItem_popGraphSensor";
            resources.ApplyResources(this.toolStripMenuItem_popGraphSensor, "toolStripMenuItem_popGraphSensor");
            this.toolStripMenuItem_popGraphSensor.Click += new System.EventHandler(this.toolStripMenuItem_popGraphSensor_Click);
            // 
            // toolStripMenuItem_popTableSensor
            // 
            this.toolStripMenuItem_popTableSensor.Name = "toolStripMenuItem_popTableSensor";
            resources.ApplyResources(this.toolStripMenuItem_popTableSensor, "toolStripMenuItem_popTableSensor");
            this.toolStripMenuItem_popTableSensor.Click += new System.EventHandler(this.toolStripMenuItem_popTableSensor_Click);
            // 
            // toolStripMenuItem_popDigitalMeterSensor
            // 
            this.toolStripMenuItem_popDigitalMeterSensor.Name = "toolStripMenuItem_popDigitalMeterSensor";
            resources.ApplyResources(this.toolStripMenuItem_popDigitalMeterSensor, "toolStripMenuItem_popDigitalMeterSensor");
            this.toolStripMenuItem_popDigitalMeterSensor.Click += new System.EventHandler(this.toolStripMenuItem_popDigitalMeterSensor_Click);
            // 
            // toolStripMenuItem_popMeterSensor
            // 
            this.toolStripMenuItem_popMeterSensor.Name = "toolStripMenuItem_popMeterSensor";
            resources.ApplyResources(this.toolStripMenuItem_popMeterSensor, "toolStripMenuItem_popMeterSensor");
            this.toolStripMenuItem_popMeterSensor.Click += new System.EventHandler(this.toolStripMenuItem_popMeterSensor_Click);
            // 
            // contextMenuStrip_constant
            // 
            resources.ApplyResources(this.contextMenuStrip_constant, "contextMenuStrip_constant");
            this.contextMenuStrip_constant.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_editConstant,
            this.toolStripMenuItem_removeConstant,
            this.toolStripSeparator5,
            this.toolStripMenuItem_monitorConstant});
            this.contextMenuStrip_constant.Name = "contextMenuStrip_sensor";
            // 
            // toolStripMenuItem_editConstant
            // 
            this.toolStripMenuItem_editConstant.Name = "toolStripMenuItem_editConstant";
            resources.ApplyResources(this.toolStripMenuItem_editConstant, "toolStripMenuItem_editConstant");
            this.toolStripMenuItem_editConstant.Click += new System.EventHandler(this.toolStripMenuItem_editConstant_Click);
            // 
            // toolStripMenuItem_removeConstant
            // 
            this.toolStripMenuItem_removeConstant.Name = "toolStripMenuItem_removeConstant";
            resources.ApplyResources(this.toolStripMenuItem_removeConstant, "toolStripMenuItem_removeConstant");
            this.toolStripMenuItem_removeConstant.Click += new System.EventHandler(this.toolStripMenuItem_removeConstant_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripMenuItem_monitorConstant
            // 
            this.toolStripMenuItem_monitorConstant.Name = "toolStripMenuItem_monitorConstant";
            resources.ApplyResources(this.toolStripMenuItem_monitorConstant, "toolStripMenuItem_monitorConstant");
            this.toolStripMenuItem_monitorConstant.Click += new System.EventHandler(this.toolStripMenuItem_monitorConstant_Click);
            // 
            // contextMenuStrip_expr
            // 
            resources.ApplyResources(this.contextMenuStrip_expr, "contextMenuStrip_expr");
            this.contextMenuStrip_expr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_editExpr,
            this.toolStripMenuItem_removeExpr,
            this.toolStripSeparator1,
            this.toolStripMenuItem_moveUpExpr,
            this.toolStripMenuItem_moveDownExpr,
            this.toolStripSeparator3,
            this.toolStripMenuItem_popGraphExpr,
            this.toolStripMenuItem_popTableExpr,
            this.toolStripMenuItem_popDigitalMeterExpr,
            this.toolStripMenuItem_popMeterExpr});
            this.contextMenuStrip_expr.Name = "contextMenuStrip_sensor";
            // 
            // toolStripMenuItem_editExpr
            // 
            this.toolStripMenuItem_editExpr.Name = "toolStripMenuItem_editExpr";
            resources.ApplyResources(this.toolStripMenuItem_editExpr, "toolStripMenuItem_editExpr");
            this.toolStripMenuItem_editExpr.Click += new System.EventHandler(this.toolStripMenuItem_editExpr_Click);
            // 
            // toolStripMenuItem_removeExpr
            // 
            this.toolStripMenuItem_removeExpr.Name = "toolStripMenuItem_removeExpr";
            resources.ApplyResources(this.toolStripMenuItem_removeExpr, "toolStripMenuItem_removeExpr");
            this.toolStripMenuItem_removeExpr.Click += new System.EventHandler(this.toolStripMenuItem_removeExpr_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItem_moveUpExpr
            // 
            this.toolStripMenuItem_moveUpExpr.Name = "toolStripMenuItem_moveUpExpr";
            resources.ApplyResources(this.toolStripMenuItem_moveUpExpr, "toolStripMenuItem_moveUpExpr");
            this.toolStripMenuItem_moveUpExpr.Click += new System.EventHandler(this.toolStripMenuItem_moveUpExpr_Click);
            // 
            // toolStripMenuItem_moveDownExpr
            // 
            this.toolStripMenuItem_moveDownExpr.Name = "toolStripMenuItem_moveDownExpr";
            resources.ApplyResources(this.toolStripMenuItem_moveDownExpr, "toolStripMenuItem_moveDownExpr");
            this.toolStripMenuItem_moveDownExpr.Click += new System.EventHandler(this.toolStripMenuItem_moveDownExpr_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripMenuItem_popGraphExpr
            // 
            this.toolStripMenuItem_popGraphExpr.Name = "toolStripMenuItem_popGraphExpr";
            resources.ApplyResources(this.toolStripMenuItem_popGraphExpr, "toolStripMenuItem_popGraphExpr");
            this.toolStripMenuItem_popGraphExpr.Click += new System.EventHandler(this.toolStripMenuItem_popGraphExpr_Click);
            // 
            // toolStripMenuItem_popTableExpr
            // 
            this.toolStripMenuItem_popTableExpr.Name = "toolStripMenuItem_popTableExpr";
            resources.ApplyResources(this.toolStripMenuItem_popTableExpr, "toolStripMenuItem_popTableExpr");
            this.toolStripMenuItem_popTableExpr.Click += new System.EventHandler(this.toolStripMenuItem_popTableExpr_Click);
            // 
            // toolStripMenuItem_popDigitalMeterExpr
            // 
            this.toolStripMenuItem_popDigitalMeterExpr.Name = "toolStripMenuItem_popDigitalMeterExpr";
            resources.ApplyResources(this.toolStripMenuItem_popDigitalMeterExpr, "toolStripMenuItem_popDigitalMeterExpr");
            this.toolStripMenuItem_popDigitalMeterExpr.Click += new System.EventHandler(this.toolStripMenuItem_popDigitalMeterExpr_Click);
            // 
            // toolStripMenuItem_popMeterExpr
            // 
            this.toolStripMenuItem_popMeterExpr.Name = "toolStripMenuItem_popMeterExpr";
            resources.ApplyResources(this.toolStripMenuItem_popMeterExpr, "toolStripMenuItem_popMeterExpr");
            this.toolStripMenuItem_popMeterExpr.Click += new System.EventHandler(this.toolStripMenuItem_popMeterExpr_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VariablesPad
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Name = "VariablesPad";
            this.contextMenuStrip_add.ResumeLayout(false);
            this.contextMenuStrip_sensor.ResumeLayout(false);
            this.contextMenuStrip_constant.ResumeLayout(false);
            this.contextMenuStrip_expr.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_add;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addSensor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addConstant;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addExpr;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_sensor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_removeSensor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_constant;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_editConstant;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_expr;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_editExpr;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_removeExpr;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_moveUpExpr;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_moveDownExpr;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_popGraphSensor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_popTableSensor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_popDigitalMeterSensor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_popMeterSensor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_popGraphExpr;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_popTableExpr;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_popDigitalMeterExpr;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_popMeterExpr;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_shiftSetSensor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_monitorConstant;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_removeConstant;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
