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
            string[] wh = w_h_pix.Split(new char[] { 'x', ' ' }, StringSplitOptions.RemoveEmptyEntries);
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
