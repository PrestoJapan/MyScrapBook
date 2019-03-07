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
    //
    // フォームの画像イメージを受け取って表示する　複数のページを同時に見たい時に使う
    //
    public partial class ReferenceForm : Form
    {
        Bitmap originalimg;
        double currentscale= 1;
        enum opemode {移動, 拡大, 縮小, NOP};
        opemode status = opemode.NOP;
        Point start;

        public ReferenceForm()
        {
            InitializeComponent();

            //　拡大・縮小のポップアップメニュー
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            ;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Point end = Cursor.Position; // スクリーン座標で移動量を調べる
            Point delta = new Point(start.X - end.X, start.Y - end.Y);
            switch (status)
            {
                case opemode.移動:
                    pictureBox1.Location = new Point(pictureBox1.Location.X - delta.X, pictureBox1.Location.Y - delta.Y);
                    break;
                case opemode.縮小:
                    if (currentscale > 0.3) currentscale -= 0.03;
                    pictureBox1.Image = ResizeImage(originalimg, currentscale);
                    break;
                case opemode.拡大:
                    if (currentscale < 5) currentscale += 0.03;
                    pictureBox1.Image = ResizeImage(originalimg, currentscale);
                    break;
            }
            status = opemode.NOP;
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            status = opemode.移動;
            start = Cursor.Position; // スクリーン座標で移動量を調べる
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)  
                status = opemode.縮小;
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                status = opemode.拡大;
        }

        public void displayPage(Bitmap img, int page, string title)
        {
            originalimg = img;
            currentscale = 1;
            pictureBox1.Image = ResizeImage(img, currentscale);
            this.Text = title;
        }

        public static Bitmap ResizeImage(Bitmap image, double scale)
        {
            Bitmap result = new Bitmap((int)(image.Width * scale), (int)(image.Height * scale));
            Graphics g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, result.Width, result.Height);

            return result;
        }

        private void 拡大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentscale < 5) currentscale += 0.25;
            pictureBox1.Image = ResizeImage(originalimg, currentscale);
        }

        private void 縮小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentscale > 0.5) currentscale -= 0.25;
            pictureBox1.Image = ResizeImage(originalimg, currentscale);
        }
    }
}
