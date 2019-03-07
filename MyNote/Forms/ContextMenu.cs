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
                CreateMenuItem(this.LineToolStripMenuItem_Click, "LineToolStripMenuItem", "ラインの削除"),
                CreateMenuItem(this.WordToolStripMenuItem_Click, "WordToolStripMenuItem", "ワード編集"),
                CreateMenuItem(this.WordPadToolStripMenuItem_Click, "WordPadToolStripMenuItem", "ワードパッド編集") });
            //　workingpanel
            //contextWorkPanelMenuStrip = createContext("contextMenuStrip2", ContextMenuStrip_Click, new ToolStripItem[] {
            //    CreateMenuItem(this.MovePageToolStripMenuItem_Click, "MovePageToolStripMenuItem", "移動")});
            //　PictureBoxのメニュー
            contextPictureMenuStrip = createContext("contextMenuStrip3", ContextMenuStrip_Click, new ToolStripItem[] {
                CreateMenuItem(this.DeleteToolStripMenuItem_Click, "DeleteToolStripMenuItem", "削除"),
                CreateMenuItem(this.FrontToolStripMenuItem_Click, "FrontToolStripMenuItem", "前面"),
                CreateMenuItem(this.LineToolStripMenuItem_Click, "LineToolStripMenuItem", "ラインの削除")});
            //　HTMLBoxのメニュー
            contextHTMLMenuStrip = createContext("contextMenuStrip4", ContextMenuStrip_Click, new ToolStripItem[] {
                CreateMenuItem(this.DeleteToolStripMenuItem_Click, "DeleteToolStripMenuItem", "削除"),
                CreateMenuItem(this.FrontToolStripMenuItem_Click, "FrontToolStripMenuItem", "前面"),
                CreateMenuItem(this.LineToolStripMenuItem_Click, "LineToolStripMenuItem", "ラインの削除"),
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
        private void ToolStripSize_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            setPage(menu.Text);
        }

        private void ToolStripBoxClear_Click(object sender, EventArgs e)
        {
            string subdir = getWorkSubDir();
            string[] files = Directory.GetFiles(subdir);
            List<string> boxes = new List<string>();
            foreach (string key in mediaBoxInfos.Keys)
            {
                boxes.Add(mediaBoxInfos[key].boxinfo.filename);
            }
            foreach (string path in files)
            {
                string filename = Path.GetFileName(path);
                if (!boxes.Contains(filename))
                {
                    File.Delete(path);
                }
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeControl(mediaBoxInfos[currentControlTag].controlbox);
        }

        private void FrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tofrontControl(mediaBoxInfos[currentControlTag].controlbox);
        }

        private void LineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tofrontControl(mediaBoxInfos[currentControlTag].controlbox);
            deleteLine(currentControlTag);
        }

        private void deleteLine(string controltag)
        {
            List<string> linktags = mediaBoxInfos[currentControlTag].boxinfo.linkboxes;
            // パートナーのリンク情報からこのBoxの情報を消す
            foreach (string tag in linktags)
            {
                mediaBoxInfos[tag].boxinfo.linkboxes.Remove(currentControlTag);
            }
            //　自身のリンク情報を消す
            mediaBoxInfos[currentControlTag].boxinfo.linkboxes.Clear();
            // 再描画
            workingpanel.Refresh();
        }

        private void WebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // HTMLファイルを読み現行のURLを調べる
            string path = Path.Combine(getWorkSubDir(), mediaBoxInfos[currentControlTag].boxinfo.filename);
            string[] htmls = File.ReadAllLines(path);
            
            for (int i=0;i<10;i++)
            {
                if (htmls[i].StartsWith("SourceURL:"))
                {
                    Process.Start(htmls[i].Replace("SourceURL:", ""));
                    return;
                }
            }
        }

        private void MovePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //moveControl(richBoxInfos[currentControlTag].controlbox);
        }

        private void WordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentControlTag.Length < 1) invokeApp("WINWORD.EXE");
            else invokeApp("WINWORD.EXE", Path.Combine(getWorkSubDir(), mediaBoxInfos[currentControlTag].boxinfo.filename));
        }

        private void WordPadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentControlTag.Length < 1) invokeApp("WORDPAD.EXE");
            else invokeApp("WORDPAD.EXE", Path.Combine(getWorkSubDir(), mediaBoxInfos[currentControlTag].boxinfo.filename));
        }

        private void NotePadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invokeApp("NOTEPAD.EXE");
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
            movef.setrichBoxInfo(mediaBoxInfos, toolStripPagesBox.DropDownItems.Count);
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
            PopupHelpForm help = new PopupHelpForm();
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

            toolStripHelpButton1.Visible = true; // ヘルプは常に表示
            toolStripCreate.Visible = !flag;
            toolStripLoad.Visible = !flag;

            toolStripText.Visible = flag;
            toolStripScreen.Visible = flag;
            toolStripPagesBox.Visible = flag;
            toolStripPage.Visible = flag;
            toolStripBackground.Visible = flag;
            toolStripPageSize.Visible = flag;
            toolStripOthers.Visible = flag;
        }

        private void setPage(string size)
        {
            // Pageサイズの変更
            workingpanel.Size = setPageSize(size);
            toolStripPageSize.Text = size;
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
            foreach (string fkey in mediaBoxInfos.Keys)
            {
                if (mediaBoxInfos[fkey].boxinfo.page == page && mediaBoxInfos[fkey].boxinfo.visible)
                {
                    workingpanel.Controls.Add(mediaBoxInfos[fkey].controlbox);
                }
            }
            this.Text = String.Format("{0}  {1} ページ", getWorkfile(), page);
            workingpanel.Refresh();
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
            f1.displayPage(CaptureControl(workingpanel), getCurrentPage(), this.Text);
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
            foreach (string key in mediaBoxInfos.Keys)
            {
                if (mediaBoxInfos[key].boxinfo.text.Contains(searchstring))
                {
                    radios[mediaBoxInfos[key].boxinfo.page].Checked = true;
                    pagedraw(mediaBoxInfos[key].boxinfo.page);
                    break;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (string key in mediaBoxInfos.Keys)
            {
                if (mediaBoxInfos[key].boxinfo.text.Contains(searchstring))
                {
                    sb.Append((mediaBoxInfos[key].boxinfo.page + 1).ToString() + ", ");
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
