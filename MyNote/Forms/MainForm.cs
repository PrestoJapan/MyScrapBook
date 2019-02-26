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

        private enum astatus { 移動, サイズ , NOP, 削除};
        private astatus status = astatus.NOP;
        private Point delta = new Point();
        // ページ機能
        public Dictionary<string, RichBoxsInfo> richBoxInfos = new Dictionary<string, RichBoxsInfo>();
        public string currentControlTag = "";
        public int maxpage = 1;
        public FlowLayoutPanel pagePanel = new FlowLayoutPanel();
        public int movetargetpage = 0;
        //　保存場所
        public List<BoxInfo> saveboxinfos;
        // Popup  
        PopupForm fm = new PopupForm();
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem EditToolStripMenuItem;
        private ToolStripMenuItem DeleteToolStripMenuItem;
        private ToolStripMenuItem FrontToolStripMenuItem;
        private ToolStripMenuItem ExpandToolStripMenuItem;
        private ToolStripMenuItem WebToolStripMenuItem;
        // Pageのポップアップ
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem MovePageToolStripMenuItem;
        private ToolStripMenuItem WordToolStripMenuItem;
        //　アプリの状態
        private bool editstart = false;
        private string printtmppath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\cpage.png";

        public MainForm()
        {
            InitializeComponent();
            this.MaximumSize = Screen.AllScreens[0].Bounds.Size;
            createContextMenu();
            BackColor = Color.Black;
            //addPageRadio(1);
            // event
            workingpanel.MouseDoubleClick += Workingpanel_MouseDoubleClick;
            //            workingpanel.MouseUp .MouseDoubleClick += Workingpanel_MouseDoubleClick;
            toolStripPagesBox.DropDownItems.Add("ページ1");
            toolStripPagesBox.Text = toolStripPagesBox.DropDownItems[0].Text;
            onofftoolStrip(false);
            workingpanel.AllowDrop = true;
            workingpanel.DragDrop += Workingpanel_DragDrop;
            workingpanel.DragEnter += Workingpanel_DragEnter;
            workingpanel.Size = setPageSize(toolStripPageSize.Text);

            toolStripSize.DropDownItems.Add("1920 x 1080");
            toolStripSize.DropDownItems.Add("3840 x 2160");
            toolStripSize.DropDownItems.Add("7680 x 4320");
        }

        private Size setPageSize(string w_h_pix)
        {
            string[] wh = w_h_pix.Split(new char[] {'x', ' '}, StringSplitOptions.RemoveEmptyEntries);
            return new Size(){Width = int.Parse(wh[0]), Height = int.Parse(wh[1])};
        }

        private void Workingpanel_DragEnter(object sender, DragEventArgs e)
        {
            if (editstart)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.All;
                else e.Effect = DragDropEffects.None;
            }
        }

        private void Workingpanel_DragDrop(object sender, DragEventArgs e)
        {
            if (editstart)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (files.Length != 1 ) return;
                // 拡張子を確認する
                string fileName = files[0];
                switch (Path.GetExtension(fileName))
                {
                    case ".php":
                    case ".txt":
                    case ".cpp":
                    case ".c":
                    case ".hpp":
                    case ".java":
                    case ".cs":
                    case ".js":
                    case ".css":
                    case ".html":
                        Clipboard.SetText(File.ReadAllText(fileName));
                        addControlToWorkingpanel();
                        break;
                    case ".jpg":
                    case ".png":
                        Clipboard.SetImage(Image.FromFile(fileName));
                        addImage(workingpanel.PointToClient(Cursor.Position));
                        break;
                }
                Debug.WriteLine(fileName);
            }
        }

        private void Workingpanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            addControlToWorkingpanel();
        }

        public void closemainform()
        {
            this.Close();
        }

        private void createContextMenu()
        {
            // ToolStripMenuItem
            EditToolStripMenuItem = CreateMenuItem(this.EditToolStripMenuItem_Click, "EditToolStripMenuItem", "編集");
            DeleteToolStripMenuItem = CreateMenuItem(this.DeleteToolStripMenuItem_Click, "DeleteToolStripMenuItem", "削除");
            FrontToolStripMenuItem = CreateMenuItem(this.FrontToolStripMenuItem_Click, "FrontToolStripMenuItem", "前面");
            ExpandToolStripMenuItem = CreateMenuItem(this.ExpandToolStripMenuItem_Click, "ExpandToolStripMenuItem", "拡大");
            WebToolStripMenuItem = CreateMenuItem(this.WebToolStripMenuItem_Click, "WebToolStripMenuItem", "Webの表示");
            MovePageToolStripMenuItem = CreateMenuItem(this.MovePageToolStripMenuItem_Click, "MovePageToolStripMenuItem", "移動");
            WordToolStripMenuItem = CreateMenuItem(this.WordToolStripMenuItem_Click, "WordToolStripMenuItem", "ワード編集");

            //　RichTextBoxとPictureBoxのメニュー
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            contextMenuStrip1.Click += ContextMenuStrip1_Click;

            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                EditToolStripMenuItem, DeleteToolStripMenuItem, FrontToolStripMenuItem,
                WebToolStripMenuItem, WordToolStripMenuItem});

            // workingPanelのメニュー
            contextMenuStrip2 = new ContextMenuStrip();
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new System.Drawing.Size(181, 48);
            contextMenuStrip2.Click += ContextMenuStrip2_Click;

            contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                MovePageToolStripMenuItem });
            // Panel event
            workingpanel.ContextMenuStrip = contextMenuStrip2;
            workingpanel.MouseUp += Workingpanel_MouseUp;
        }

        private void Workingpanel_MouseUp(object sender, MouseEventArgs e)
        {
            statusclear();
        }

        private ToolStripMenuItem CreateMenuItem( EventHandler handler, string name, string text)
        {
            ToolStripMenuItem menuitem = new System.Windows.Forms.ToolStripMenuItem();
            menuitem.Name = name;
            menuitem.Size = new System.Drawing.Size(180, 22);
            menuitem.Text = text;
            menuitem.Click += handler; // new System.EventHandler(this.ExpandToolStripMenuItem_Click);
            return menuitem;
        }

        private void ContextMenuStrip1_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = sender as ContextMenuStrip;
            currentControlTag = menu.SourceControl.Tag as string;
        }

        private void ContextMenuStrip2_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = sender as ContextMenuStrip;
            currentControlTag = menu.SourceControl.Tag as string;
        }

        // フォームに書き込む
        private void addControlToWorkingpanel()
        {
            Debug.WriteLine("ダブルクリック");
            // shiftキーをチェック
            if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
            {
                // クリップボードの内容をチェック
                IDataObject data = Clipboard.GetDataObject();
                if (data != null)
                {
                    //関連付けられているすべての形式を列挙する
                    foreach (string fmt in data.GetFormats())
                    {
                        if (fmt.Contains("Rich") || fmt.Contains("HTML") || fmt.Contains("Text") || fmt.Contains("String"))
                        {
                            // RichTextBoxの作成　tagはBoxInfoと同じ内容
                            RichTextBox rbox = createRichBox();
                            BoxInfo info = new BoxInfo()
                            {
                                filename = savefilename("rtf"),
                                size = new Size(200, 200),
                                visible = true,
                                location = workingpanel.PointToClient(Cursor.Position),
                                page = getCurrentPage()
                            };
                            rbox.Size = info.size;
                            rbox.Location = info.location;
                            rbox.Tag = info.filename;
                            rbox.Paste();
                            info.text = rbox.Text;
                            rbox.ReadOnly = true; // 必ずPaste後に行うこと

                            // １秒以内にダブルクリックされていないことを確認し、ファイル名が重複しないようにする
                            if (!richBoxInfos.ContainsKey(info.filename))
                            {
                                richBoxInfos.Add(info.filename, new RichBoxsInfo { boxinfo = info, controlbox = rbox });
                                workingpanel.Controls.Add(rbox);
                                rbox.SaveFile(Path.Combine(getWorkSubDir(),info.filename));
                            }
                            break;
                        }
                        else if (fmt.Contains("Bitmap"))
                        {
                            // Imageの追加
                            addImage(workingpanel.PointToClient(Cursor.Position));
                            break;
                        }
                        Console.WriteLine(fmt);
                    }
                }
            }
        }

        private void addImage(Point place)
        {
            PictureBox pic = createPictureBox();
            pic.Location = place;
            pic.Image = Clipboard.GetImage();
            pic.Size = pic.Image.Size;
            // １秒以内にダブルクリックされていないことを確認し、ファイル名が重複しないようにする
            BoxInfo info = new BoxInfo()
            {
                filename = savefilename("png"),
                size = pic.Size,
                visible = true,
                location = pic.Location,
                page = getCurrentPage()
            };
            pic.Tag = info.filename;
            if (!richBoxInfos.ContainsKey(info.filename))
            {
                richBoxInfos.Add(info.filename, new RichBoxsInfo { boxinfo = info, controlbox = pic });
                workingpanel.Controls.Add(pic);
            }
            pic.Image.Save(Path.Combine(getWorkSubDir(), info.filename));
            return;
        }


        //
        // 移動・サイズ変更処理作業変数のクリア
        //
        private void statusclear()
        {
            status = astatus.NOP;
            delta = new Point();
            Cursor.Current = Cursors.Default;
        }
            
        public void savedata()
        {
            if (editstart)
            {
                set_zOrderToInfo(getCurrentPage()); // 枠の前後
                saveNote();
            }
        }

        public bool readNote0()
        {
            bool flag = readNote();
            if (!flag) return false;
            // toolstripのページ修正
            int max = 1;
            foreach (RichBoxsInfo info in richBoxInfos.Values) {
                if (info.boxinfo.page > max) max = info.boxinfo.page;
            }
            toolStripPagesBox.DropDownItems.Clear();
            for (int i=1;i<=max;i++) { toolStripPagesBox.DropDownItems.Add(string.Format("ページ{0}",i)); }
            // 描画
            pagedraw(1);
            set_zOrderToControl(1);
            return true;
        }

        private void set_zOrderToInfo(int page)
        {
            // ページ内のオブジェのZorderをセットする
            foreach (string fkey in richBoxInfos.Keys)
            {
                if (richBoxInfos[fkey].boxinfo.page == getCurrentPage())
                {
                    richBoxInfos[fkey].boxinfo.zorder = workingpanel.Controls.GetChildIndex(richBoxInfos[fkey].controlbox);
                }
            }
        }

        private void set_zOrderToControl(int page)
        {
            // ページ内のオブジェのZorderをセットする
            foreach (string fkey in richBoxInfos.Keys)
            {
                if (richBoxInfos[fkey].boxinfo.page == getCurrentPage())
                {
                    workingpanel.Controls.SetChildIndex(richBoxInfos[fkey].controlbox, richBoxInfos[fkey].boxinfo.zorder);
                }
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
            this.Text = String.Format("{0}  {1} ページ",getWorkfile(), page);
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

        public void explorer()
        {
            Process proc = Process.Start(getWorkDir());
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

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (typeof(RichTextBox) == richBoxInfos[currentControlTag].controlbox.GetType())
            {
                (richBoxInfos[currentControlTag].controlbox as RichTextBox).ReadOnly = false;
            }
            else if (typeof(PictureBox) == richBoxInfos[currentControlTag].controlbox.GetType())
            {
            }
        }

        private void invokeApp(string exename, string path)
        {
            Process p = new Process();
            p.StartInfo.FileName = exename;
            p.StartInfo.Arguments = path;
            p.SynchronizingObject = this;
            p.Exited += app_Exited;
            p.EnableRaisingEvents = true;
            p.Start();
        }

        private void app_Exited(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            Process p = sender as Process;
            string fullpath = p.StartInfo.Arguments;
            string tagname = Path.GetFileName(fullpath);
            if (tagname.Contains(".png")) return;

            if (typeof(RichTextBox) == richBoxInfos[tagname].controlbox.GetType())
            {
                MessageBox.Show("編集が終了しました。");
                (richBoxInfos[tagname].controlbox as RichTextBox).LoadFile(fullpath);
                pagedraw(getCurrentPage());
            }
            else
            {
                MessageBox.Show("画像はMyNote実行中には編集できません。");
            }
        }

        /// <summary>
        /// ToolStripMenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void MoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
//            moveControl(richBoxInfos[currentControlTag].controlbox);
        }

        private void WebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //            WebForm web = new WebForm();
            WebCameraForm web = new WebCameraForm();
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
            //Process p = new Process();
            //p.StartInfo.FileName = "WINWORD.EXE";
            //p.Start();
            invokeApp("WINWORD.EXE", Path.Combine(getWorkSubDir(),richBoxInfos[currentControlTag].boxinfo.filename));
        }

        //
        // 削除
        //
        private void removeControl(Control rbox)
        {
            string infokey = rbox.Tag as string;
            workingpanel.Controls.Remove(rbox);
            richBoxInfos.Remove(infokey);
        }

        //
        // 全面
        //
        private static void tofrontControl(Control rbox)
        {
            rbox.BringToFront();
            int zIndex = rbox.Parent.Controls.GetChildIndex(rbox);
        }

        //
        // 拡大
        //
        private static void expandControl(Control rbox)
        {
            rbox.Size = rbox.Size + new Size(100, 100);
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

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            savedata();
            //onofftoolStrip(false);
            //this.Text = "MyNote";
            //workingpanel.Controls.Clear();
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

        public int getCurrentPage()
        {
            return int.Parse(toolStripPagesBox.Text.Replace("ページ", ""));
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
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSize_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripScreen.Text = e.ClickedItem.Text;
            // Pageサイズの変更
            workingpanel.Size = setPageSize(toolStripScreen.Text);
            toolStripPageSize.Text = toolStripScreen.Text;
        }
    }

}
