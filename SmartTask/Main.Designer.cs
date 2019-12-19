namespace SmartTask
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.taskbarIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RunStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.interval = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.interval)).BeginInit();
            this.SuspendLayout();
            // 
            // taskbarIcon
            // 
            this.taskbarIcon.ContextMenuStrip = this.contextMenuStrip;
            this.taskbarIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("taskbarIcon.Icon")));
            this.taskbarIcon.Text = "SmartTaskbar";
            this.taskbarIcon.Visible = true;
            this.taskbarIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.taskbarIcon_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunStart,
            this.toolStripSeparator1,
            this.Quit});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(125, 54);
            // 
            // RunStart
            // 
            this.RunStart.CheckOnClick = true;
            this.RunStart.Name = "RunStart";
            this.RunStart.Size = new System.Drawing.Size(124, 22);
            this.RunStart.Text = "开机启动";
            this.RunStart.CheckedChanged += new System.EventHandler(this.RunStart_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // Quit
            // 
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(124, 22);
            this.Quit.Text = "退出";
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // interval
            // 
            this.interval.Location = new System.Drawing.Point(87, 99);
            this.interval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.interval.Name = "interval";
            this.interval.Size = new System.Drawing.Size(120, 21);
            this.interval.TabIndex = 1;
            this.interval.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(110, 136);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 251);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.interval);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartTaskbar";
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.interval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon taskbarIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem RunStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Quit;
        private System.Windows.Forms.NumericUpDown interval;
        private System.Windows.Forms.Button btnOK;
    }
}

