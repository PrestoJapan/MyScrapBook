using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MyScrapBook
{
    public partial class MainForm : Form
    {
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
            rbox.ContextMenuStrip = this.contextMenuStrip1; // 右ボタンクリックで表示されるポップアップメニュー
            rbox.BackColor = Color.White;
            rbox.LostFocus += Rbox_LostFocus;
            return rbox;
        }

        private void Rbox_LostFocus(object sender, EventArgs e)
        {
            (sender as RichTextBox).ReadOnly = true;
        }

        //
        // RichBoxの削除
        //
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
                }
            }
            statusclear();
        }

        private void Box_MouseMove(object sender, MouseEventArgs e)
        {
            Control rbox = sender as Control;
            //rbox.SelectionLength = 0;

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

        //
        //
        // Picture
        //
        //
        //
        private PictureBox createPictureBox()
        {
            PictureBox pic = new PictureBox();
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.MouseDown += Box_MouseDown;
            pic.MouseMove += Box_MouseMove;
            pic.MouseUp += Box_MouseUp;
            pic.MouseDoubleClick += Box_MouseDoubleClick;
            pic.ContextMenuStrip = this.contextMenuStrip1; // 右ボタンクリックで表示されるポップアップメニュー
            return pic;
        }

    }
}
