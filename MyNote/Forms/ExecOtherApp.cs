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

        #region アプリの実行と終了後の処理
        public void explorer()
        {
            Process proc = Process.Start(getWorkDir());
        }

        private void invokeApp(string exename)
        {
            Process p = new Process();
            p.StartInfo.FileName = exename;
            p.SynchronizingObject = this;
            p.Exited += app_Exited;
            p.EnableRaisingEvents = true;
            p.Start();
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
        #endregion アプリの実行と終了後の処理


    }
}
