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
        #region variable
        //　アプリの状態
        private bool editstart = false;
        //　ドラッグ＆ドロップの状態　移動量
        private enum astatus { 移動, サイズ左上, サイズ左下, サイズ右上, サイズ右下, NOP, 削除, ライン };
        private astatus status = astatus.NOP;
        private Point delta = new Point();
        // ページ機能
        public Dictionary<string, MediaBoxsInfo> mediaBoxInfos = new Dictionary<string, MediaBoxsInfo>();
        public string currentControlTag = "";
        public int maxpage = 1;
        public FlowLayoutPanel pagePanel = new FlowLayoutPanel();
        public int movetargetpage = 0;
        //　保存場所
        public List<BoxInfo> saveboxinfos;
        // Popup  
        PopupHelpForm fm = new PopupHelpForm();
        // Popupの組み合わせ
        private ContextMenuStrip contextRichMenuStrip;
        private ContextMenuStrip contextWorkPanelMenuStrip;
        private ContextMenuStrip contextPictureMenuStrip;
        private ContextMenuStrip contextHTMLMenuStrip;

        // Pageのポップアップ
        private string printtmppath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\cpage.png";
        #endregion variable

    }
}
