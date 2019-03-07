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
            this.toolStripText = new System.Windows.Forms.ToolStripButton();
            this.toolStripScreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripPagesBox = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripBackground = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripOthers = new System.Windows.Forms.ToolStripDropDownButton();
            this.上書き保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.印刷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ページサイズ変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x1080ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x2160ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x4320ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ページの並べ替えToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ワードパッドToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ワードの起動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.メモ帳の起動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.データフォルダToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.未使用のBoxデータを削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.このページの参照ウインドウToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPageSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripHelpButton1 = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1417, 383);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1417, 408);
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
            this.scrollpanel.Size = new System.Drawing.Size(1417, 383);
            this.scrollpanel.TabIndex = 1;
            // 
            // workingpanel
            // 
            this.workingpanel.AutoScroll = true;
            this.workingpanel.Location = new System.Drawing.Point(12, 11);
            this.workingpanel.Name = "workingpanel";
            this.workingpanel.Size = new System.Drawing.Size(4000, 4000);
            this.workingpanel.TabIndex = 0;
            this.workingpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCreate,
            this.toolStripLoad,
            this.toolStripText,
            this.toolStripScreen,
            this.toolStripPage,
            this.toolStripPagesBox,
            this.toolStripBackground,
            this.toolStripLabel1,
            this.toolStripOthers,
            this.toolStripPageSize,
            this.toolStripHelpButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(759, 25);
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
            // toolStripText
            // 
            this.toolStripText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripText.Image")));
            this.toolStripText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripText.Name = "toolStripText";
            this.toolStripText.Size = new System.Drawing.Size(98, 22);
            this.toolStripText.Text = "テキスト枠追加";
            this.toolStripText.Click += new System.EventHandler(this.toolStripText_Click);
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
            // toolStripPage
            // 
            this.toolStripPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPage.Image")));
            this.toolStripPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPage.Name = "toolStripPage";
            this.toolStripPage.Size = new System.Drawing.Size(79, 22);
            this.toolStripPage.Text = "ページ追加";
            this.toolStripPage.Click += new System.EventHandler(this.toolStripPage_Click);
            // 
            // toolStripPagesBox
            // 
            this.toolStripPagesBox.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPagesBox.Image")));
            this.toolStripPagesBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPagesBox.Name = "toolStripPagesBox";
            this.toolStripPagesBox.Size = new System.Drawing.Size(29, 22);
            this.toolStripPagesBox.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripPagesBox_DropDownItemClicked);
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripOthers
            // 
            this.toolStripOthers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上書き保存ToolStripMenuItem,
            this.終了ToolStripMenuItem,
            this.印刷ToolStripMenuItem,
            this.toolStripSeparator1,
            this.ページサイズ変更ToolStripMenuItem,
            this.ページの並べ替えToolStripMenuItem,
            this.toolStripSeparator2,
            this.ワードパッドToolStripMenuItem,
            this.ワードの起動ToolStripMenuItem,
            this.メモ帳の起動ToolStripMenuItem,
            this.toolStripSeparator3,
            this.データフォルダToolStripMenuItem,
            this.未使用のBoxデータを削除ToolStripMenuItem,
            this.このページの参照ウインドウToolStripMenuItem});
            this.toolStripOthers.Image = ((System.Drawing.Image)(resources.GetObject("toolStripOthers.Image")));
            this.toolStripOthers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripOthers.Name = "toolStripOthers";
            this.toolStripOthers.Size = new System.Drawing.Size(67, 22);
            this.toolStripOthers.Text = "その他";
            // 
            // 上書き保存ToolStripMenuItem
            // 
            this.上書き保存ToolStripMenuItem.Name = "上書き保存ToolStripMenuItem";
            this.上書き保存ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.上書き保存ToolStripMenuItem.Text = "上書き保存";
            this.上書き保存ToolStripMenuItem.Click += new System.EventHandler(this.toolStripSave_Click);
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.toolStripEnd_Click);
            // 
            // 印刷ToolStripMenuItem
            // 
            this.印刷ToolStripMenuItem.Name = "印刷ToolStripMenuItem";
            this.印刷ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.印刷ToolStripMenuItem.Text = "印刷";
            this.印刷ToolStripMenuItem.Click += new System.EventHandler(this.toolStripPrint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // ページサイズ変更ToolStripMenuItem
            // 
            this.ページサイズ変更ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x1080ToolStripMenuItem,
            this.x2160ToolStripMenuItem,
            this.x4320ToolStripMenuItem});
            this.ページサイズ変更ToolStripMenuItem.Name = "ページサイズ変更ToolStripMenuItem";
            this.ページサイズ変更ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.ページサイズ変更ToolStripMenuItem.Text = "ページサイズ変更";
            // 
            // x1080ToolStripMenuItem
            // 
            this.x1080ToolStripMenuItem.Name = "x1080ToolStripMenuItem";
            this.x1080ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.x1080ToolStripMenuItem.Text = "1920 x 1080";
            this.x1080ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripSize_Click);
            // 
            // x2160ToolStripMenuItem
            // 
            this.x2160ToolStripMenuItem.Name = "x2160ToolStripMenuItem";
            this.x2160ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.x2160ToolStripMenuItem.Text = "3840 x 2160";
            this.x2160ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripSize_Click);
            // 
            // x4320ToolStripMenuItem
            // 
            this.x4320ToolStripMenuItem.Name = "x4320ToolStripMenuItem";
            this.x4320ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.x4320ToolStripMenuItem.Text = "7680 x 4320";
            this.x4320ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripSize_Click);
            // 
            // ページの並べ替えToolStripMenuItem
            // 
            this.ページの並べ替えToolStripMenuItem.Name = "ページの並べ替えToolStripMenuItem";
            this.ページの並べ替えToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.ページの並べ替えToolStripMenuItem.Text = "ページの並べ替え";
            this.ページの並べ替えToolStripMenuItem.Click += new System.EventHandler(this.toolStripPageMove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(196, 6);
            // 
            // ワードパッドToolStripMenuItem
            // 
            this.ワードパッドToolStripMenuItem.Name = "ワードパッドToolStripMenuItem";
            this.ワードパッドToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.ワードパッドToolStripMenuItem.Text = "ワードパッドの起動";
            this.ワードパッドToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // ワードの起動ToolStripMenuItem
            // 
            this.ワードの起動ToolStripMenuItem.Name = "ワードの起動ToolStripMenuItem";
            this.ワードの起動ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.ワードの起動ToolStripMenuItem.Text = "ワードの起動";
            this.ワードの起動ToolStripMenuItem.Click += new System.EventHandler(this.WordToolStripMenuItem_Click);
            // 
            // メモ帳の起動ToolStripMenuItem
            // 
            this.メモ帳の起動ToolStripMenuItem.Name = "メモ帳の起動ToolStripMenuItem";
            this.メモ帳の起動ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.メモ帳の起動ToolStripMenuItem.Text = "メモ帳の起動";
            this.メモ帳の起動ToolStripMenuItem.Click += new System.EventHandler(this.NotePadToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(196, 6);
            // 
            // データフォルダToolStripMenuItem
            // 
            this.データフォルダToolStripMenuItem.Name = "データフォルダToolStripMenuItem";
            this.データフォルダToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.データフォルダToolStripMenuItem.Text = "データフォルダを開く";
            this.データフォルダToolStripMenuItem.Click += new System.EventHandler(this.toolStripFolder_Click);
            // 
            // 未使用のBoxデータを削除ToolStripMenuItem
            // 
            this.未使用のBoxデータを削除ToolStripMenuItem.Name = "未使用のBoxデータを削除ToolStripMenuItem";
            this.未使用のBoxデータを削除ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.未使用のBoxデータを削除ToolStripMenuItem.Text = "未使用のBoxデータを削除";
            this.未使用のBoxデータを削除ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripBoxClear_Click);
            // 
            // このページの参照ウインドウToolStripMenuItem
            // 
            this.このページの参照ウインドウToolStripMenuItem.Name = "このページの参照ウインドウToolStripMenuItem";
            this.このページの参照ウインドウToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.このページの参照ウインドウToolStripMenuItem.Text = "このページの参照ウインドウ";
            this.このページの参照ウインドウToolStripMenuItem.Click += new System.EventHandler(this.toolStripDisplay_Click);
            // 
            // toolStripPageSize
            // 
            this.toolStripPageSize.Name = "toolStripPageSize";
            this.toolStripPageSize.ReadOnly = true;
            this.toolStripPageSize.Size = new System.Drawing.Size(80, 25);
            this.toolStripPageSize.Text = "1920 x 1080";
            // 
            // toolStripHelpButton1
            // 
            this.toolStripHelpButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripHelpButton1.Image")));
            this.toolStripHelpButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripHelpButton1.Name = "toolStripHelpButton1";
            this.toolStripHelpButton1.Size = new System.Drawing.Size(56, 22);
            this.toolStripHelpButton1.Text = "ヘルプ";
            this.toolStripHelpButton1.Click += new System.EventHandler(this.toolStripHelp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1417, 408);
            this.Controls.Add(this.toolStripContainer1);
            this.MinimumSize = new System.Drawing.Size(200, 150);
            this.Name = "MainForm";
            this.Text = "スクラップブック";
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
        private System.Windows.Forms.ToolStripButton toolStripLoad;
        private System.Windows.Forms.ToolStripButton toolStripScreen;
        private System.Windows.Forms.ToolStripButton toolStripPage;
        private System.Windows.Forms.ToolStripButton toolStripBackground;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel scrollpanel;
        private System.Windows.Forms.ToolStripButton toolStripText;
        private System.Windows.Forms.ToolStripButton toolStripCreate;
        private System.Windows.Forms.ToolStripTextBox toolStripPageSize;
        private System.Windows.Forms.ToolStripDropDownButton toolStripOthers;
        private System.Windows.Forms.ToolStripMenuItem 上書き保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 印刷ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ページの並べ替えToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ワードパッドToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ワードの起動ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem メモ帳の起動ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem データフォルダToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 未使用のBoxデータを削除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem このページの参照ウインドウToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ページサイズ変更ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x1080ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x2160ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x4320ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripPagesBox;
        private System.Windows.Forms.ToolStripButton toolStripHelpButton1;
    }
}

