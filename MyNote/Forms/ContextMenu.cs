using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyScrapBook
{
    public partial class MainForm : Form
    {

        #region ContextMenu
        private void createContextMenu()
        {
            //　RichTextBoxのメニュー
            contextRichMenuStrip = createContext("contextMenuStrip1", ContextMenuStrip_Click, new ToolStripItem[] {
                CreateMenuItem(this.DeleteToolStripMenuItem_Click, "DeleteToolStripMenuItem", "削除"),
                CreateMenuItem(this.FrontToolStripMenuItem_Click, "FrontToolStripMenuItem", "前面"),
                CreateMenuItem(this.WordToolStripMenuItem_Click, "WordToolStripMenuItem", "ワード編集"),
                CreateMenuItem(this.WordPadToolStripMenuItem_Click, "WordPadToolStripMenuItem", "ワードパッド編集") });
            //　workingpanel
            contextWorkPanelMenuStrip = createContext("contextMenuStrip2", ContextMenuStrip_Click, new ToolStripItem[] {
                CreateMenuItem(this.MovePageToolStripMenuItem_Click, "MovePageToolStripMenuItem", "移動")});
            //　PictureBoxのメニュー
            contextPictureMenuStrip = createContext("contextMenuStrip3", ContextMenuStrip_Click, new ToolStripItem[] {
                CreateMenuItem(this.DeleteToolStripMenuItem_Click, "DeleteToolStripMenuItem", "削除"),
                CreateMenuItem(this.FrontToolStripMenuItem_Click, "FrontToolStripMenuItem", "前面")});
            //　HTMLBoxのメニュー
            contextHTMLMenuStrip = createContext("contextMenuStrip4", ContextMenuStrip_Click, new ToolStripItem[] {
                CreateMenuItem(this.DeleteToolStripMenuItem_Click, "DeleteToolStripMenuItem", "削除"),
                CreateMenuItem(this.FrontToolStripMenuItem_Click, "FrontToolStripMenuItem", "前面"),
                CreateMenuItem(this.WebToolStripMenuItem_Click, "WebToolStripMenuItem", "Webの表示") });

            // workingpanelにメニューを割り当てる
            workingpanel.ContextMenuStrip = contextWorkPanelMenuStrip;
            workingpanel.MouseUp += Workingpanel_MouseUp;
        }

        private ContextMenuStrip createContext(string name, EventHandler handler, ToolStripItem[] items)
        {
            ContextMenuStrip　contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            contextMenuStrip1.Name = name;
            contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            contextMenuStrip1.Click += handler;
            contextMenuStrip1.Items.AddRange(items);
            return contextMenuStrip1;
        }

        private ToolStripMenuItem CreateMenuItem(EventHandler handler, string name, string text)
        {
            ToolStripMenuItem menuitem = new System.Windows.Forms.ToolStripMenuItem();
            menuitem.Name = name;
            menuitem.Size = new System.Drawing.Size(180, 22);
            menuitem.Text = text;
            menuitem.Click += handler; // new System.EventHandler(this.ExpandToolStripMenuItem_Click);
            return menuitem;
        }

        private void ContextMenuStrip_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = sender as ContextMenuStrip;
            currentControlTag = menu.SourceControl.Tag as string;
        }

        #endregion ContextMenu

        #region toolStripクリック
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (typeof(RichTextBox) == richBoxInfos[currentControlTag].controlbox.GetType())
            {
                (richBoxInfos[currentControlTag].controlbox as RichTextBox).ReadOnly = false;
            }
            //else if (typeof(PictureBox) == richBoxInfos[currentControlTag].controlbox.GetType())
            //{

            //}
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeControl(richBoxInfos[currentControlTag].controlbox);
        }

        private void FrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tofrontControl(richBoxInfos[currentControlTag].controlbox);
        }

        private void ExpandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            expandControl(richBoxInfos[currentControlTag].controlbox);
        }

        private void WebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //            WebForm web = new WebForm();
            WebCameraForm web = new WebCameraForm();
            if (sender.GetType() == typeof(Panel))
            {

            } else
            {

            }
            web.gotoweb(richBoxInfos[currentControlTag].controlbox.Text, this);
            web.TopMost = true;
            web.Show();
        }

        private void MovePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //moveControl(richBoxInfos[currentControlTag].controlbox);
        }

        private void WordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invokeApp("WINWORD.EXE", Path.Combine(getWorkSubDir(), richBoxInfos[currentControlTag].boxinfo.filename));
        }

        private void WordPadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invokeApp("WORDPAD.EXE", Path.Combine(getWorkSubDir(), richBoxInfos[currentControlTag].boxinfo.filename));
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            savedata();
        }

        private void toolStripLoad_Click(object sender, EventArgs e)
        {
            bool flag = readNote0();
            if (flag) onofftoolStrip(true);
        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            print(workingpanel);
        }

        private void toolStripScreen_Click(object sender, EventArgs e)
        {
            screenCopy();
        }

        private void toolStripPage_Click(object sender, EventArgs e)
        {
            pageplus();
        }

        private void toolStripDisplay_Click(object sender, EventArgs e)
        {
            anotherform();
        }

        private void toolStripFolder_Click(object sender, EventArgs e)
        {
            explorer();
        }

        private void toolStripBackground_Click(object sender, EventArgs e)
        {
            backgroundcolor();
        }

        private void toolStripPageMove_Click(object sender, EventArgs e)
        {
            PageMoveForm movef = new PageMoveForm();
            movef.TopMost = true;
            movef.setrichBoxInfo(richBoxInfos, toolStripPagesBox.DropDownItems.Count);
            movef.ShowDialog();
            pagedraw(getCurrentPage());
        }

        private void toolStripEnd_Click(object sender, EventArgs e)
        {
            closemainform();
        }

        private void toolStripPagesBox_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // ページの変更　パネルの再描画とカレントページの修正
            string cpage = e.ClickedItem.Text;
            toolStripPagesBox.Text = cpage;
            // ページ描画
            pagedraw(int.Parse(cpage.Replace("ページ", "")));
        }

        private void toolStripHelp_Click(object sender, EventArgs e)
        {
            PopupForm help = new PopupForm();
            help.TopMost = true;
            help.Show();
        }

        private void toolStripText_Click(object sender, EventArgs e)
        {
            // クリップボードにテキストを設定して、RickBoxを作成する
            Clipboard.SetText("修正してください。");
            addControlToWorkingpanel();
        }

        // Bookの作成
        private void toolStripBook_Click(object sender, EventArgs e)
        {
            //　Bookの作成
            bool flag = newBook();
            if (flag)
            {
                onofftoolStrip(true);
                yes("ファイル", "新しいノートを作成しました。");
            }
        }

        private void onofftoolStrip(bool flag)
        {
            toolStripPagesBox.Text = "ページ1";
            editstart = flag;

            toolStripCreate.Visible = !flag;
            toolStripLoad.Visible = !flag;

            toolStripSave.Visible = flag;
            toolStripText.Visible = flag;
            toolStripDisplay.Visible = flag;
            toolStripScreen.Visible = flag;
            toolStripFolder.Visible = flag;
            toolStripPagesBox.Visible = flag;
            toolStripPage.Visible = flag;
            toolStripBackground.Visible = flag;
            toolStripPrint.Visible = flag;
            toolStripPageMove.Visible = flag;
            toolStripEnd.Visible = flag;
            toolStripHelp.Visible = flag;
            toolStripSize.Visible = flag;
            toolStripPageSize.Visible = flag;
            toolStripWordPad.Visible = flag;
        }

        private void toolStripSize_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripScreen.Text = e.ClickedItem.Text;
            // Pageサイズの変更
            workingpanel.Size = setPageSize(toolStripScreen.Text);
            toolStripPageSize.Text = toolStripScreen.Text;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "wordpad.exe";
            p.Start();
        }
        #endregion toolStripクリック

        #region メニューの実行

        public void closemainform()
        {
            this.Close();
        }

        public void backgroundcolor()
        {
            if (workingpanel.BackColor == Color.Black)
            {
                workingpanel.BackColor = Color.White;
            }
            else
            {
                workingpanel.BackColor = Color.Black;
            }
        }

        private void pagedraw(int page)
        {
            // ページをクリアする
            workingpanel.Controls.Clear();
            // ページの描画
            foreach (string fkey in richBoxInfos.Keys)
            {
                if (richBoxInfos[fkey].boxinfo.page == page && richBoxInfos[fkey].boxinfo.visible)
                {
                    workingpanel.Controls.Add(richBoxInfos[fkey].controlbox);
                }
            }
            this.Text = String.Format("{0}  {1} ページ", getWorkfile(), page);
        }

        //Button1のClickイベントハンドラ
        public void print(Control panel)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\cpage.png";
            //コントロールの外観を描画するBitmapの作成
            Bitmap bmp = CaptureControl(panel);
            //キャプチャする
            bmp.Save(path);
            //後始末
            bmp.Dispose();
            invokeApp("mspaint.exe", path);
        }

        public void pageplus()
        {
            int pagecount = toolStripPagesBox.DropDownItems.Count;
            toolStripPagesBox.DropDownItems.Add(string.Format("ページ{0}", pagecount + 1));
            toolStripPagesBox.Text = string.Format("ページ{0}", pagecount + 1);
            pagedraw(pagecount + 1);
        }

        public void anotherform()
        {
            ReferenceForm f1 = new ReferenceForm();
            f1.displayPage(CaptureControl(workingpanel), getCurrentPage());
            f1.Show();
        }

        public void screenCopy()
        {
            ScreenCopyForm fm = new ScreenCopyForm();
            this.Visible = false;
            fm.ShowDialog();
            this.Visible = true;
        }

        public void search(string searchstring, ToolStripLabel result)
        {
            foreach (string key in richBoxInfos.Keys)
            {
                if (richBoxInfos[key].boxinfo.text.Contains(searchstring))
                {
                    radios[richBoxInfos[key].boxinfo.page].Checked = true;
                    pagedraw(richBoxInfos[key].boxinfo.page);
                    break;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (string key in richBoxInfos.Keys)
            {
                if (richBoxInfos[key].boxinfo.text.Contains(searchstring))
                {
                    sb.Append((richBoxInfos[key].boxinfo.page + 1).ToString() + ", ");
                }
            }
            result.Text = sb.ToString();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (editstart)
            {
                if (yesno("保存", "保存しますか？") == DialogResult.Yes) savedata();
            }
            Properties.Settings.Default.Save();
            if (File.Exists(printtmppath)) File.Delete(printtmppath);
        }

        #endregion メニューの実行


    }
}
