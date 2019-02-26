namespace MyScrapBook
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.scrollpanel = new System.Windows.Forms.Panel();
            this.workingpanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripText = new System.Windows.Forms.ToolStripButton();
            this.toolStripDisplay = new System.Windows.Forms.ToolStripButton();
            this.toolStripScreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripPagesBox = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripBackground = new System.Windows.Forms.ToolStripButton();
            this.toolStripPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripPageMove = new System.Windows.Forms.ToolStripButton();
            this.toolStripEnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSize = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripPageSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.scrollpanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.scrollpanel);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1305, 383);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1305, 408);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // scrollpanel
            // 
            this.scrollpanel.AutoScroll = true;
            this.scrollpanel.Controls.Add(this.workingpanel);
            this.scrollpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollpanel.Location = new System.Drawing.Point(0, 0);
            this.scrollpanel.Name = "scrollpanel";
            this.scrollpanel.Size = new System.Drawing.Size(1305, 383);
            this.scrollpanel.TabIndex = 1;
            // 
            // workingpanel
            // 
            this.workingpanel.AutoScroll = true;
            this.workingpanel.Location = new System.Drawing.Point(12, 14);
            this.workingpanel.Name = "workingpanel";
            this.workingpanel.Size = new System.Drawing.Size(4000, 4000);
            this.workingpanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCreate,
            this.toolStripLoad,
            this.toolStripSave,
            this.toolStripSize,
            this.toolStripPageSize,
            this.toolStripText,
            this.toolStripDisplay,
            this.toolStripScreen,
            this.toolStripFolder,
            this.toolStripPagesBox,
            this.toolStripPage,
            this.toolStripBackground,
            this.toolStripPrint,
            this.toolStripLabel1,
            this.toolStripPageMove,
            this.toolStripEnd,
            this.toolStripHelp});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1195, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripCreate
            // 
            this.toolStripCreate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCreate.Image")));
            this.toolStripCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCreate.Name = "toolStripCreate";
            this.toolStripCreate.Size = new System.Drawing.Size(88, 22);
            this.toolStripCreate.Text = "Bookの作成";
            this.toolStripCreate.Click += new System.EventHandler(this.toolStripBook_Click);
            // 
            // toolStripLoad
            // 
            this.toolStripLoad.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLoad.Image")));
            this.toolStripLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLoad.Name = "toolStripLoad";
            this.toolStripLoad.Size = new System.Drawing.Size(82, 22);
            this.toolStripLoad.Text = "Bookを開く";
            this.toolStripLoad.Click += new System.EventHandler(this.toolStripLoad_Click);
            // 
            // toolStripSave
            // 
            this.toolStripSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSave.Image")));
            this.toolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSave.Name = "toolStripSave";
            this.toolStripSave.Size = new System.Drawing.Size(51, 22);
            this.toolStripSave.Text = "保存";
            this.toolStripSave.Click += new System.EventHandler(this.toolStripSave_Click);
            // 
            // toolStripText
            // 
            this.toolStripText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripText.Image")));
            this.toolStripText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripText.Name = "toolStripText";
            this.toolStripText.Size = new System.Drawing.Size(74, 22);
            this.toolStripText.Text = "テキスト枠";
            this.toolStripText.Click += new System.EventHandler(this.toolStripText_Click);
            // 
            // toolStripDisplay
            // 
            this.toolStripDisplay.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDisplay.Image")));
            this.toolStripDisplay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDisplay.Name = "toolStripDisplay";
            this.toolStripDisplay.Size = new System.Drawing.Size(83, 22);
            this.toolStripDisplay.Text = "別ウインドウ";
            this.toolStripDisplay.Click += new System.EventHandler(this.toolStripDisplay_Click);
            // 
            // toolStripScreen
            // 
            this.toolStripScreen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripScreen.Image")));
            this.toolStripScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripScreen.Name = "toolStripScreen";
            this.toolStripScreen.Size = new System.Drawing.Size(103, 22);
            this.toolStripScreen.Text = "スクリーンショット";
            this.toolStripScreen.Click += new System.EventHandler(this.toolStripScreen_Click);
            // 
            // toolStripFolder
            // 
            this.toolStripFolder.Image = ((System.Drawing.Image)(resources.GetObject("toolStripFolder.Image")));
            this.toolStripFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripFolder.Name = "toolStripFolder";
            this.toolStripFolder.Size = new System.Drawing.Size(88, 22);
            this.toolStripFolder.Text = "データフォルダ";
            this.toolStripFolder.Click += new System.EventHandler(this.toolStripFolder_Click);
            // 
            // toolStripPagesBox
            // 
            this.toolStripPagesBox.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPagesBox.Image")));
            this.toolStripPagesBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPagesBox.Name = "toolStripPagesBox";
            this.toolStripPagesBox.Size = new System.Drawing.Size(29, 22);
            this.toolStripPagesBox.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripPagesBox_DropDownItemClicked);
            // 
            // toolStripPage
            // 
            this.toolStripPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPage.Image")));
            this.toolStripPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPage.Name = "toolStripPage";
            this.toolStripPage.Size = new System.Drawing.Size(79, 22);
            this.toolStripPage.Text = "ページ追加";
            this.toolStripPage.Click += new System.EventHandler(this.toolStripPage_Click);
            // 
            // toolStripBackground
            // 
            this.toolStripBackground.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBackground.Image")));
            this.toolStripBackground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBackground.Name = "toolStripBackground";
            this.toolStripBackground.Size = new System.Drawing.Size(63, 22);
            this.toolStripBackground.Text = "背景色";
            this.toolStripBackground.Click += new System.EventHandler(this.toolStripBackground_Click);
            // 
            // toolStripPrint
            // 
            this.toolStripPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPrint.Image")));
            this.toolStripPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPrint.Name = "toolStripPrint";
            this.toolStripPrint.Size = new System.Drawing.Size(51, 22);
            this.toolStripPrint.Text = "印刷";
            this.toolStripPrint.Click += new System.EventHandler(this.toolStripPrint_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripPageMove
            // 
            this.toolStripPageMove.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPageMove.Image")));
            this.toolStripPageMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPageMove.Name = "toolStripPageMove";
            this.toolStripPageMove.Size = new System.Drawing.Size(91, 22);
            this.toolStripPageMove.Text = "ページ再配置";
            this.toolStripPageMove.Click += new System.EventHandler(this.toolStripPageMove_Click);
            // 
            // toolStripEnd
            // 
            this.toolStripEnd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEnd.Image")));
            this.toolStripEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEnd.Name = "toolStripEnd";
            this.toolStripEnd.Size = new System.Drawing.Size(51, 22);
            this.toolStripEnd.Text = "終了";
            this.toolStripEnd.Click += new System.EventHandler(this.toolStripEnd_Click);
            // 
            // toolStripHelp
            // 
            this.toolStripHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripHelp.Image")));
            this.toolStripHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripHelp.Name = "toolStripHelp";
            this.toolStripHelp.Size = new System.Drawing.Size(56, 22);
            this.toolStripHelp.Text = "ヘルプ";
            this.toolStripHelp.Click += new System.EventHandler(this.toolStripHelp_Click);
            // 
            // toolStripSize
            // 
            this.toolStripSize.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSize.Image")));
            this.toolStripSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSize.Name = "toolStripSize";
            this.toolStripSize.Size = new System.Drawing.Size(92, 22);
            this.toolStripSize.Text = "ページサイズ";
            this.toolStripSize.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripSize_DropDownItemClicked);
            // 
            // toolStripPageSize
            // 
            this.toolStripPageSize.Name = "toolStripPageSize";
            this.toolStripPageSize.ReadOnly = true;
            this.toolStripPageSize.Size = new System.Drawing.Size(100, 25);
            this.toolStripPageSize.Text = "1920 x 1080";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1305, 408);
            this.Controls.Add(this.toolStripContainer1);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(200, 150);
            this.Name = "MainForm";
            this.Text = "MyNote";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.scrollpanel.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel workingpanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripSave;
        private System.Windows.Forms.ToolStripButton toolStripLoad;
        private System.Windows.Forms.ToolStripButton toolStripPrint;
        private System.Windows.Forms.ToolStripButton toolStripScreen;
        private System.Windows.Forms.ToolStripButton toolStripPage;
        private System.Windows.Forms.ToolStripButton toolStripDisplay;
        private System.Windows.Forms.ToolStripButton toolStripFolder;
        private System.Windows.Forms.ToolStripButton toolStripBackground;
        private System.Windows.Forms.ToolStripButton toolStripPageMove;
        private System.Windows.Forms.ToolStripButton toolStripEnd;
        private System.Windows.Forms.ToolStripButton toolStripHelp;
        private System.Windows.Forms.ToolStripDropDownButton toolStripPagesBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel scrollpanel;
        private System.Windows.Forms.ToolStripButton toolStripText;
        private System.Windows.Forms.ToolStripButton toolStripCreate;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSize;
        private System.Windows.Forms.ToolStripTextBox toolStripPageSize;
    }
}

