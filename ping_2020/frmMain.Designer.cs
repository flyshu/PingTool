namespace ping_2020 {
	partial class frmMain {
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuMethods = new System.Windows.Forms.ToolStripMenuItem();
			this.menuMethodsStart = new System.Windows.Forms.ToolStripMenuItem();
			this.menuMethodsStop = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripText = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItemStart = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemStop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemColor = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStripText.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMethods});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1421, 25);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "操作";
			// 
			// menuMethods
			// 
			this.menuMethods.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMethodsStart,
            this.menuMethodsStop});
			this.menuMethods.Name = "menuMethods";
			this.menuMethods.Size = new System.Drawing.Size(44, 21);
			this.menuMethods.Text = "操作";
			// 
			// menuMethodsStart
			// 
			this.menuMethodsStart.Name = "menuMethodsStart";
			this.menuMethodsStart.Size = new System.Drawing.Size(180, 22);
			this.menuMethodsStart.Text = "全部启动";
			this.menuMethodsStart.Click += new System.EventHandler(this.menuMethodsStart_Click);
			// 
			// menuMethodsStop
			// 
			this.menuMethodsStop.Name = "menuMethodsStop";
			this.menuMethodsStop.Size = new System.Drawing.Size(180, 22);
			this.menuMethodsStop.Text = "全部停止";
			this.menuMethodsStop.Click += new System.EventHandler(this.menuMethodsStop_Click);
			// 
			// contextMenuStripText
			// 
			this.contextMenuStripText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemStart,
            this.toolStripMenuItemStop,
            this.toolStripMenuItemColor});
			this.contextMenuStripText.Name = "contextMenuStripText";
			this.contextMenuStripText.Size = new System.Drawing.Size(181, 92);
			// 
			// ToolStripMenuItemStart
			// 
			this.ToolStripMenuItemStart.Enabled = false;
			this.ToolStripMenuItemStart.Name = "ToolStripMenuItemStart";
			this.ToolStripMenuItemStart.Size = new System.Drawing.Size(180, 22);
			this.ToolStripMenuItemStart.Text = "开始";
			this.ToolStripMenuItemStart.Click += new System.EventHandler(this.ToolStripMenuItemStart_Click);
			// 
			// toolStripMenuItemStop
			// 
			this.toolStripMenuItemStop.Name = "toolStripMenuItemStop";
			this.toolStripMenuItemStop.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItemStop.Text = "停止";
			this.toolStripMenuItemStop.Click += new System.EventHandler(this.toolStripMenuItemStop_Click);
			// 
			// toolStripMenuItemColor
			// 
			this.toolStripMenuItemColor.Name = "toolStripMenuItemColor";
			this.toolStripMenuItemColor.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItemColor.Text = "改变状态";
			this.toolStripMenuItemColor.Click += new System.EventHandler(this.toolStripMenuItemColor_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1421, 668);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmMain";
			this.Text = "服务器实时网络状态监控程序";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStripText.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menuMethods;
		private System.Windows.Forms.ToolStripMenuItem menuMethodsStart;
		private System.Windows.Forms.ToolStripMenuItem menuMethodsStop;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripText;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStart;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStop;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemColor;
	}
}

