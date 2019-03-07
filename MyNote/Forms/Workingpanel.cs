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
        #region Workingpanel Drag&Drop
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
                if (files.Length != 1) return;
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
                        addImage(CursorPosition());
                        break;
                }
                Debug.WriteLine(fileName);
            }
        }

        private void Workingpanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            addControlToWorkingpanel();
        }

        private void Workingpanel_MouseUp(object sender, MouseEventArgs e)
        {
            statusclear();
        }

        #endregion Workingpanel Drag&Drop

        #region コントロールのZOrder
        private void set_zOrderToInfo(int page)
        {
            // ページ内のオブジェのZorderをセットする
            foreach (string fkey in mediaBoxInfos.Keys)
            {
                if (mediaBoxInfos[fkey].boxinfo.page == getCurrentPage())
                {
                    mediaBoxInfos[fkey].boxinfo.zorder = workingpanel.Controls.GetChildIndex(mediaBoxInfos[fkey].controlbox);
                }
            }
        }

        private void set_zOrderToControl(int page)
        {
            // ページ内のオブジェのZorderをセットする
            foreach (string fkey in mediaBoxInfos.Keys)
            {
                if (mediaBoxInfos[fkey].boxinfo.page == getCurrentPage())
                {
                    workingpanel.Controls.SetChildIndex(mediaBoxInfos[fkey].controlbox, mediaBoxInfos[fkey].boxinfo.zorder);
                }
            }
        }
        #endregion コントロールのZOrder



    }
}
