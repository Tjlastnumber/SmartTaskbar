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
            this.SmartTaskEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.RunStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.Nud_Interval = new System.Windows.Forms.NumericUpDown();
            this.BtnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Interval)).BeginInit();
            this.SuspendLayout();
            // 
            // taskbarIcon
            // 
            this.taskbarIcon.ContextMenuStrip = this.contextMenuStrip;
            this.taskbarIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("taskbarIcon.Icon")));
            this.taskbarIcon.Text = "SmartTaskbar";
            this.taskbarIcon.Visible = true;
            this.taskbarIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.taskbarIcon_MouseDoubleClick_1);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Setting,
            this.SmartTaskEnable,
            this.RunStart,
            this.toolStripSeparator1,
            this.Quit});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 120);
            // 
            // SmartTaskEnable
            // 
            this.SmartTaskEnable.Name = "SmartTaskEnable";
            this.SmartTaskEnable.Size = new System.Drawing.Size(180, 22);
            this.SmartTaskEnable.Text = "开启";
            this.SmartTaskEnable.Click += new System.EventHandler(this.SmartTaskEnable_Click);
            // 
            // RunStart
            // 
            this.RunStart.CheckOnClick = true;
            this.RunStart.Name = "RunStart";
            this.RunStart.Size = new System.Drawing.Size(180, 22);
            this.RunStart.Text = "开机启动";
            this.RunStart.CheckedChanged += new System.EventHandler(this.RunStart_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // Quit
            // 
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(180, 22);
            this.Quit.Text = "退出";
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // Nud_Interval
            // 
            this.Nud_Interval.Location = new System.Drawing.Point(129, 29);
            this.Nud_Interval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Nud_Interval.Name = "Nud_Interval";
            this.Nud_Interval.Size = new System.Drawing.Size(60, 21);
            this.Nud_Interval.TabIndex = 1;
            this.Nud_Interval.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.Location = new System.Drawing.Point(226, 70);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 2;
            this.BtnOK.Text = "确定";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "停止活动后";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "秒隐藏";
            // 
            // Setting
            // 
            this.Setting.Name = "Setting";
            this.Setting.Size = new System.Drawing.Size(180, 22);
            this.Setting.Text = "设置";
            this.Setting.Click += new System.EventHandler(this.Settings_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 105);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.Nud_Interval);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartTaskbar";
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Interval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon taskbarIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem RunStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Quit;
        private System.Windows.Forms.NumericUpDown Nud_Interval;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem SmartTaskEnable;
        private System.Windows.Forms.ToolStripMenuItem Setting;
    }
}

