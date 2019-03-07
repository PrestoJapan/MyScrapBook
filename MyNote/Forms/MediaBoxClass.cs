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
            if (!editstart) return;
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
                            addHTML(CursorPosition(), html);
                            return;
                        }

                        if (fmt.Contains("Rich") || fmt.Contains("Text") || fmt.Contains("String"))
                        {
                            // RichTextBoxの作成　tagはBoxInfoと同じ内容
                            RichTextBox rbox = createRichBox();
                            BoxInfo info = new BoxInfo()
                            {
                                filename = savefilename("rtf"),
                                size = new Size(200, 200),
                                visible = true,
                                location = CursorPosition(),
                                page = getCurrentPage()
                            };
                            rbox.Size = info.size;
                            rbox.Location = info.location;
                            rbox.Tag = info.filename;
                            rbox.Paste();
                            info.text = rbox.Text;
                            rbox.ReadOnly = true; // 必ずPaste後に行うこと

                            // １秒以内にダブルクリックされていないことを確認し、ファイル名が重複しないようにする
                            if (!mediaBoxInfos.ContainsKey(info.filename))
                            {
                                mediaBoxInfos.Add(info.filename, new MediaBoxsInfo { boxinfo = info, controlbox = rbox });
                                workingpanel.Controls.Add(rbox);
                                rbox.SaveFile(Path.Combine(getWorkSubDir(), info.filename));
                            }
                            break;
                        }
                        else if (fmt.Contains("Bitmap"))
                        {
                            // Imageの追加
                            addImage(CursorPosition());
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
            mediaBoxInfos.Add(info.filename, new MediaBoxsInfo { boxinfo = info, controlbox = pic });
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
            mediaBoxInfos.Add(info.filename, new MediaBoxsInfo { boxinfo = info, controlbox = web });
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
            Debug.WriteLine(rbox.Tag as String);
            //　shift DCで消されたコントロールで呼び出される場合があるためコントロールの存在を確認する
            if (workingpanel.Controls.Contains(rbox))
            {
                if (status == astatus.移動)
                {
                    mediaBoxInfos[rbox.Tag as string].boxinfo.location = rbox.Location;
                }
                else if (status == astatus.サイズ右下 || status == astatus.サイズ右上 || 
                    status == astatus.サイズ左下 || status == astatus.サイズ左上)
                {
                    mediaBoxInfos[rbox.Tag as string].boxinfo.size = rbox.Size;
                    if (rbox.GetType() == typeof(Panel))
                    {
                        // Panel内のwebbrawserのサイズも変える
                        Control web = rbox.Controls[0];
                        web.Size = new Size(rbox.Size.Width - 60, rbox.Size.Height - 60);
                    } if (rbox.GetType() == typeof(PictureBox))
                    {
                        //　PictureBoxのサイズを絵のサイズに合わせる
                        rbox = match_aspect(rbox as PictureBox);
                        mediaBoxInfos[rbox.Tag as string].boxinfo.location = rbox.Location;
                        mediaBoxInfos[rbox.Tag as string].boxinfo.size = rbox.Size;
                    }
                } else if (status == astatus.ライン)
                {
                    //　ライン情報をBox間のラインに変更する
                    string startbox = onBox(tmpline.start, getCurrentPage());
                    string endbox = onBox(tmpline.end, getCurrentPage());
                    //// ラインを引き直す
                    if (startbox.Length > 0 && endbox.Length > 0)
                    {
                        mediaBoxInfos[startbox].boxinfo.linkboxes.Add(endbox);
                        mediaBoxInfos[endbox].boxinfo.linkboxes.Add(startbox);
                        status = astatus.NOP;
                        workingpanel.Refresh();
                    }
                }
            }
            statusclear();
            workingpanel.Refresh();
        }

        private void Box_MouseMove(object sender, MouseEventArgs e)
        {
            Control rbox = sender as Control;
            Point cp = CursorPosition();
            Point lefttop = rbox.Location;
            Point leftbottom = new Point(lefttop.X, lefttop.Y + rbox.Height);
            Point righttop = new Point(lefttop.X + rbox.Width, lefttop.Y);
            Point rightbottom = new Point(lefttop.X + rbox.Width, lefttop.Y + rbox.Height);
            if (status == astatus.移動)
            {
                if (delta.X < 1) delta = new Point(cp.X - rbox.Location.X, cp.Y - rbox.Location.Y);
                rbox.Location = new Point(cp.X - delta.X, cp.Y - delta.Y);
            }
            else if (status == astatus.サイズ左上)
            {
                rbox.Location = cp;
                rbox.Size = new Size(Math.Abs(rightbottom.X - cp.X), Math.Abs(rightbottom.Y - cp.Y));
            }
            else if (status == astatus.サイズ左下)
            {
                rbox.Location = new Point(cp.X, righttop.Y);
                rbox.Size = new Size(Math.Abs(righttop.X - cp.X), Math.Abs(cp.Y - righttop.Y));
            }
            else if (status == astatus.サイズ右上)
            {
                rbox.Location = new Point(leftbottom.X, cp.Y);
                rbox.Size = new Size(Math.Abs(cp.X - leftbottom.X), Math.Abs(leftbottom.Y - cp.Y));
            }
            else if (status == astatus.サイズ右下)
            {
                rbox.Size = new Size(Math.Abs(cp.X - lefttop.X), Math.Abs(cp.Y - lefttop.Y));
            }
            else if (status == astatus.ライン)
            {
                // 線を引く
                tmpline.end = CursorPosition();
                workingpanel.Refresh();
            }
            if (rbox.Size.Width < 30) rbox.Size = new Size(30, rbox.Size.Height);
            if (rbox.Size.Height < 30) rbox.Size = new Size(rbox.Size.Width, 30);
        }

        private void Box_MouseDown(object sender, MouseEventArgs e)
        {
            Point cp = CursorPosition();
            if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && ((Control.ModifierKeys & Keys.Control) == Keys.Control))
            {
                // ラインの開始
                status = astatus.ライン;
                tmpline = new LinePoint() { start = cp, end = cp };
                return;
            }
            //　マウスダウンの位置がboxの右下近辺ならばサイズ変更　それ以外は移動
            Control rbox = sender as Control;
            Point rightbottom = rbox.Location + rbox.Size;
            if ((Math.Abs(rbox.Location.X - cp.X) < 50) && (Math.Abs(rbox.Location.Y - cp.Y) < 50))
            {
                status = astatus.サイズ左上;
                Cursor.Current = Cursors.Cross;
            }
            else if ((Math.Abs(rbox.Location.X - cp.X) < 50) && (Math.Abs(rightbottom.Y - cp.Y) < 50))
            {
                status = astatus.サイズ左下;
                Cursor.Current = Cursors.Cross;
            }
            else if ((Math.Abs(rightbottom.X - cp.X) < 50) && (Math.Abs(rbox.Location.Y - cp.Y) < 50))
            {
                status = astatus.サイズ右上;
                Cursor.Current = Cursors.Cross;
            }
            else if ((Math.Abs(rightbottom.X - cp.X) < 50) && (Math.Abs(rightbottom.Y - cp.Y) < 50))
            {
                status = astatus.サイズ右下;
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
            mediaBoxInfos.Remove(infokey);
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

        LinePoint tmpline = new LinePoint();


        // Boxをつなぐラインの描画
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red, 3);
            Graphics g = e.Graphics;
            int cpage = getCurrentPage();

            foreach (string skey in mediaBoxInfos.Keys)
            {
                foreach (string ekey in mediaBoxInfos[skey].boxinfo.linkboxes)
                {
                    if (mediaBoxInfos[skey].boxinfo.page == cpage)
                    {
                        List<Point> lines = DrawBoxToBoxLine(mediaBoxInfos[skey].boxinfo, mediaBoxInfos[ekey].boxinfo);
                        g.DrawLine(pen, lines[0].X, lines[0].Y, lines[1].X, lines[1].Y);
                    }
                }
            }
            if (status == astatus.ライン) g.DrawLine(pen, tmpline.start.X, tmpline.start.Y, tmpline.end.X, tmpline.end.Y);
            g.Dispose();
        }
    }

    class LinePoint
    {
        public Point start;
        public Point end;
    }
}
