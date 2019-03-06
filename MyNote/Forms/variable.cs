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
        private enum astatus { 移動, サイズ, NOP, 削除 };
        private astatus status = astatus.NOP;
        private Point delta = new Point();
        // ページ機能
        public Dictionary<string, RichBoxsInfo> richBoxInfos = new Dictionary<string, RichBoxsInfo>();
        public string currentControlTag = "";
        public int maxpage = 1;
        public FlowLayoutPanel pagePanel = new FlowLayoutPanel();
        public int movetargetpage = 0;
        //　保存場所
        public List<BoxInfo> saveboxinfos;
        // Popup  
        PopupForm fm = new PopupForm();
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
