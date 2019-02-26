using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyScrapBook
{
    public partial class ScreenCopyForm : Form
    {
        private bool inaction = false;
        private Point startpoint;
        private Point endpoint;

        public ScreenCopyForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.AutoSize = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.Manual;
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1920 * 2, 1080 * 2); // 2画面(1920 * 1080)
            //this.Opacity = 50 / 100;
        }

        private void ScreenCopyForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!inaction) return;
            endpoint = new Point(Cursor.Position.X, Cursor.Position.Y);
            // 小さい座標をスタートとする
            int startx = Math.Min(startpoint.X, endpoint.X);
            int starty = Math.Min(startpoint.Y, endpoint.Y);
            rangepanel.Location = new Point(startx, starty);
            rangepanel.Size = new Size(Math.Abs(startpoint.X - endpoint.X), Math.Abs(startpoint.Y - endpoint.Y));
        }


        private void ScreenCopyForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!inaction)
            {
                inaction = !inaction;
                startpoint = new Point(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void ScreenCopyForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (inaction)
            {
                rangepanel.Size = new Size(0, 0);
                inaction = !inaction;
                endpoint = new Point(Cursor.Position.X, Cursor.Position.Y);
                // 小さい座標をスタートとする
                int startx = Math.Min(startpoint.X, endpoint.X);
                int starty = Math.Min(startpoint.Y, endpoint.Y);

                Bitmap bmp = new Bitmap(Math.Abs(endpoint.X - startpoint.X), Math.Abs(endpoint.Y - startpoint.Y));
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(new Point(startx, starty), new Point(0, 0), bmp.Size);
                g.Dispose();
                Clipboard.SetImage(bmp);
                //後片付け
                bmp.Dispose();

                // フォームを閉じる
                this.Close();
            }

        }

    }
}
