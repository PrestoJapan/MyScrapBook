using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MyScrapBook
{
    public partial class MainForm : Form
    {
        // Workingpanelにコントロールを追加
        #region 独自コントロールの追加
        // Workingpanelのダブルクリックで呼ばれる
        private void addControlToWorkingpanel()
        {
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
                        if (fmt.Contains("HTML"))
                        {
                            //invokeApp("wordpad.exe");
                            var meta = data.GetData("HTML Format", true);
                            string html = Clipboard.GetData("HTML Format") as string;
                            addHTML(workingpanel.PointToClient(Cursor.Position), html);
                            return;
                        }

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
                                rbox.SaveFile(Path.Combine(getWorkSubDir(), info.filename));
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

        public RichTextBox createRichBox()
        {
            RichTextBox rbox = new RichTextBox();
            rbox.BorderStyle = BorderStyle.None;
            // マウスを使った移動
            rbox.MouseDown += Box_MouseDown;
            rbox.MouseMove += Box_MouseMove;
            rbox.MouseUp += Box_MouseUp;
            rbox.MouseDoubleClick += Box_MouseDoubleClick;
            rbox.ScrollBars = RichTextBoxScrollBars.None;
            rbox.ContextMenuStrip = this.contextRichMenuStrip; // 右ボタンクリックで表示されるポップアップメニュー
            rbox.BackColor = Color.White;
            rbox.LostFocus += Rbox_LostFocus;
            return rbox;
        }

        private void addImage(Point place)
        {
            PictureBox pic = createPictureBox();
            pic.Location = place;
            pic.Image = Clipboard.GetImage();
            pic.Size = pic.Image.Size;
            BoxInfo info = new BoxInfo()
            {
                filename = savefilename("png"),
                size = pic.Size,
                visible = true,
                location = pic.Location,
                page = getCurrentPage()
            };
            pic.Tag = info.filename;
            richBoxInfos.Add(info.filename, new RichBoxsInfo { boxinfo = info, controlbox = pic });
            workingpanel.Controls.Add(pic);
            pic.Image.Save(Path.Combine(getWorkSubDir(), info.filename));
            // クリップボードをクリア
            Clipboard.Clear();
            return;
        }

        private void addHTML(Point place, string html)
        {
            BoxInfo info = new BoxInfo()
            {
                filename = savefilename("html"),
                size = new Size(400, 400),
                visible = true,
                location = place,
                page = getCurrentPage()
            };
            Panel web = createWebBrowser(html, info);
            richBoxInfos.Add(info.filename, new RichBoxsInfo { boxinfo = info, controlbox = web });
            workingpanel.Controls.Add(web);
            File.WriteAllText(Path.Combine(getWorkSubDir(), info.filename), html);
            // クリップボードをクリア
            Clipboard.Clear();
            return;
        }

        private PictureBox createPictureBox()
        {
            PictureBox pic = new PictureBox();
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.MouseDown += Box_MouseDown;
            pic.MouseMove += Box_MouseMove;
            pic.MouseUp += Box_MouseUp;
            pic.MouseDoubleClick += Box_MouseDoubleClick;
            pic.ContextMenuStrip = this.contextPictureMenuStrip; // 右ボタンクリックで表示されるポップアップメニュー
            return pic;
        }

        private Panel createWebBrowser(string html, BoxInfo boxinfo)
        {
            Panel pan = new Panel() { BackColor = Color.White, Width = boxinfo.size.Width, Height = boxinfo.size.Height };
            WebBrowser web = new WebBrowser();

            pan.MouseDown += Box_MouseDown;
            pan.MouseMove += Box_MouseMove;
            pan.MouseUp += Box_MouseUp;
            pan.MouseDoubleClick += Box_MouseDoubleClick;
            pan.ContextMenuStrip = this.contextHTMLMenuStrip; // 右ボタンクリックで表示されるポップアップメニュー
            pan.Location = boxinfo.location;
            pan.Tag = boxinfo.filename;
            pan.Size = boxinfo.size;

            web.Location = new Point(30, 30);
            web.Size = new Size(pan.Width - 60, pan.Height - 60);
            web.AllowNavigation = false;
            web.ScrollBarsEnabled = false;
            web.IsWebBrowserContextMenuEnabled = false;
            web.ContextMenuStrip = this.contextHTMLMenuStrip;
            web.DocumentText = html.Substring(html.IndexOf("<html>"));
            web.Tag = boxinfo.filename;

            pan.Controls.Add(web);
            return pan;
        }

        private void Rbox_LostFocus(object sender, EventArgs e)
        {
            (sender as RichTextBox).ReadOnly = true;
        }

        #endregion 独自コントロールの追加

        #region 独自コントロールのドラッグ操作
        private void Box_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control rbox = sender as Control;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                removeControl(rbox);
            }
            else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                tofrontControl(rbox);
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                expandControl(rbox);
            }
        }

        private void Box_MouseUp(object sender, MouseEventArgs e)
        {
            Control rbox = sender as Control;
            //　shift DCで消されたコントロールで呼び出される場合があるためコントロールの存在を確認する
            if (workingpanel.Controls.Contains(rbox))
            {
                if (status == astatus.移動)
                {
                    richBoxInfos[rbox.Tag as string].boxinfo.location = rbox.Location;
                }
                else if (status == astatus.サイズ)
                {
                    richBoxInfos[rbox.Tag as string].boxinfo.size = rbox.Size;
                    if (rbox.GetType() == typeof(Panel))
                    {
                        // Panel内のwebbrawserのサイズも変える
                        Control web = rbox.Controls[0];
                        web.Size = new Size(rbox.Size.Width - 60, rbox.Size.Height - 60);
                    } if (rbox.GetType() == typeof(PictureBox))
                    {
                        //　PictureBoxのサイズを絵のサイズに合わせる
                        rbox = match_aspect(rbox as PictureBox);
                        richBoxInfos[rbox.Tag as string].boxinfo.location = rbox.Location;
                        richBoxInfos[rbox.Tag as string].boxinfo.size = rbox.Size;
                    }
                }
            }
            statusclear();
        }

        private void Box_MouseMove(object sender, MouseEventArgs e)
        {
            Control rbox = sender as Control;

            Point cp = this.PointToClient(Cursor.Position);
            if (status == astatus.移動)
            {
                if (delta.X < 1) delta = new Point(cp.X - rbox.Location.X, cp.Y - rbox.Location.Y);
                rbox.Location = new Point(cp.X - delta.X, cp.Y - delta.Y);
            }
            else if (status == astatus.サイズ)
            {
                rbox.Size = new Size(cp.X - rbox.Location.X, cp.Y - rbox.Location.Y);
            }
        }

        private void Box_MouseDown(object sender, MouseEventArgs e)
        {
            //　マウスダウンの位置がboxの右下近辺ならばサイズ変更　それ以外は移動
            Control rbox = sender as Control;
            Point rightbottom = rbox.Location + rbox.Size;
            Point cp = this.PointToClient(Cursor.Position);
            if ((Math.Abs(rightbottom.X - cp.X) < 50) && (Math.Abs(rightbottom.Y - cp.Y) < 50))
            {
                status = astatus.サイズ;
                Cursor.Current = Cursors.Cross;
            }
            else
            {
                status = astatus.移動;
                Cursor.Current = Cursors.PanNW;
            }
        }
        #endregion 独自コントロールのドラッグ操作

        #region 独自コントロールの削除・前面・拡大
        private void removeControl(Control rbox)
        {
            string infokey = rbox.Tag as string;
            workingpanel.Controls.Remove(rbox);
            richBoxInfos.Remove(infokey);
        }

        private static void tofrontControl(Control rbox)
        {
            rbox.BringToFront();
            int zIndex = rbox.Parent.Controls.GetChildIndex(rbox);
        }

        private static void expandControl(Control rbox)
        {
            rbox.Size = rbox.Size + new Size(100, 100);
        }
        #endregion 独自コントロールの削除・前面・拡大

    }
}
