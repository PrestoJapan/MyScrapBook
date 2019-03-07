using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyScrapBook
{
    public partial class MainForm : Form
    {
        #region DialogResult
        private DialogResult yesno(string caption, string message)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            return MessageBox.Show(this, message, caption, buttons, MessageBoxIcon.Question);
        }

        private DialogResult yes(string caption, string message)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            return MessageBox.Show(this, message, caption, buttons, MessageBoxIcon.Question);
        }
        #endregion DialogResult

        #region hint tools
        private List<Point> DrawBoxToBoxLine(BoxInfo box1, BoxInfo box2)
        {
            List<Point> edge1 = edges(box1);
            List<Point> edge2 = edges(box2);
            int min1 = 0;
            int min2 = 0;
            double mindist = double.MaxValue;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    double cdist = Math.Pow(edge1[i].X - edge2[j].X, 2) + Math.Pow(edge1[i].Y - edge2[j].Y, 2);
                    if (cdist < mindist)
                    {
                        mindist = cdist;
                        min1 = i;
                        min2 = j;
                    }
                }
            }
            // min1とmin2間でラインを引く
            return new List<Point>() { edge1[min1], edge2[min2]};
        }

        private List<Point> edges(BoxInfo box1)
        {
            // 上辺、右辺、下辺、左辺
            return new List<Point>() {
                new Point(box1.location.X + box1.size.Width / 2, box1.location.Y),
                new Point(box1.location.X + box1.size.Width, box1.location.Y + box1.size.Height / 2),
                new Point(box1.location.X + box1.size.Width / 2, box1.location.Y + box1.size.Height),
                new Point(box1.location.X, box1.location.Y + box1.size.Height / 2),
            };
        } 

        private Point CursorPosition()
        {
            return workingpanel.PointToClient(Cursor.Position);
        }

        // point 上の　Boxを調べる
        private string onBox(Point point, int page)
        {
            Dictionary<string, int> onboxes = new Dictionary<string, int>();
            foreach (string key in mediaBoxInfos.Keys)
            {
                int minx = mediaBoxInfos[key].boxinfo.location.X;
                int miny = mediaBoxInfos[key].boxinfo.location.Y;
                int maxx = minx + mediaBoxInfos[key].boxinfo.size.Width;
                int maxy = miny + mediaBoxInfos[key].boxinfo.size.Height;
                if (point.X > minx && point.X < maxx && 
                    point.Y > miny && point.Y < maxy &&
                    page == mediaBoxInfos[key].boxinfo.page)
                {
                    onboxes.Add(key, mediaBoxInfos[key].boxinfo.zorder);
                }
                Debug.WriteLine(key + ": = " + mediaBoxInfos[key].boxinfo.zorder.ToString());
            }

            if (onboxes.Count > 0)
            {
                var newTable = onboxes.OrderBy(x => x.Value);
                foreach (KeyValuePair<string, int> item in newTable)
                {
                    return item.Key;
                }
            }
            return "";
        }

        // PictureBoxのアスペクト比をイメージのそれと一致させる
        private PictureBox match_aspect(PictureBox pic)
        {
            float panelaspect = (float)pic.Size.Width / (float)pic.Size.Height;
            float orgaspect = (float)pic.Image.Size.Width / (float)pic.Image.Size.Height;
            float ph = (float)pic.Height;
            float pw = (float)pic.Width;
            PictureBox newpic = pic;

            if (panelaspect < orgaspect)
            {
                // Panelが上下に大きすぎるパターン　locationのyとsizeのHeightを調整する
                newpic.Location = new Point(pic.Location.X, 
                    (int)((float)pic.Location.Y + (ph - (pw / orgaspect)) / 2));
                newpic.Size = new Size(pic.Size.Width, (int)(pw / orgaspect));
            } else
            {
                // Panelが左右に大きすぎるパターン　locationのxとsizeのWidthを調整する
                newpic.Location = new Point((int)(pic.Location.X + ((pic.Width - (float)pic.Height * orgaspect) / 2)),
                    pic.Location.Y);
                newpic.Size = new Size((int)((float)pic.Height * orgaspect), pic.Height);
            }
            return newpic;
        }


        // 移動・サイズ変更処理作業変数のクリア
        private void statusclear()
        {
            status = astatus.NOP;
            delta = new Point();
            Cursor.Current = Cursors.Default;
        }

        public int getCurrentPage()
        {
            return int.Parse(toolStripPagesBox.Text.Replace("ページ", ""));
        }

        private Size setPageSize(string w_h_pix)
        {
            string[] wh = w_h_pix.Split(new char[] { 'x', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            return new Size() { Width = int.Parse(wh[0]), Height = int.Parse(wh[1]) };
        }

        private string savefilename(string suffix)
        {
            string datepath = DateTime.Now.ToShortDateString().Replace("/", "-") + "T" +
                DateTime.Now.ToLongTimeString().Replace(":", "-") + "." + suffix;
            return datepath;
        }

        private string getWorkDir()
        {
            return Properties.Settings.Default.workingDirectory;
        }

        private string getWorkfilefullpath()
        {
            return Path.Combine(Properties.Settings.Default.workingDirectory, Properties.Settings.Default.workingFile);
        }

        private string getWorkfile()
        {
            return Properties.Settings.Default.workingFile;
        }

        private string getWorkSubDir()
        {
            string namewithout = Properties.Settings.Default.workingFile.Replace("mlinf", "").Replace(".", "");
            return Path.Combine(Properties.Settings.Default.workingDirectory, namewithout);
        }
        #endregion hint tools


    }
}
