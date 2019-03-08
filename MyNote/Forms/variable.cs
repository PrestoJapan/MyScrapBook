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
        // ブックの情報
        public MLINF metaInfo = new MLINF();
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
        private ContextMenuStrip contextPictureMenuStrip;
        private ContextMenuStrip contextHTMLMenuStrip;
        private ContextMenuStrip contextPageMenuStrip;

        // Pageのポップアップ
        private string printtmppath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\cpage.png";
        #endregion variable

    }


    // ブックのメタ情報
    public class MLINF
    {
        public string memo = "ScrapBook";
        public string author = "Yosuke Sugiyama 2019";
        public Size notesize = new Size() { Width = 1920, Height = 1080 };
        public DateTimeOffset createDateTime;
        public mediainformation bookmetainfo;
        public List<BoxInfo> boxinfos;

        public static mediainformation getAllBoxInfo(List<BoxInfo> boxes)
        {
            mediainformation metadata = new mediainformation();
            List<string> list = new List<string>();
            foreach (BoxInfo box in boxes)
            {
                list.AddRange(box.mediainfo.keys);
            }
            metadata.keys = list;
            return metadata;
        }
    }

    // ボックスの情報 作業用
    public class MediaBoxsInfo
    {
        public BoxInfo boxinfo;
        public Control controlbox;

        public string getKeys()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in boxinfo.mediainfo.keys) { sb.Append(key + Environment.NewLine); }
            return sb.ToString();
        }
    }

    //　ボックスのデータベース情報
    public class mediainformation
    {
        public List<string> keys = new List<string>();
        public List<string> referencekeys = new List<string>();
        public Dictionary<string, int> keypoint = new Dictionary<string, int>();

    }

    //　ボックスの情報　保存用
    public class BoxInfo
    {
        //　Boxの区別はファイルの拡張子で行う
        public string filename = "";
        public Size size = new Size();
        public Point location = new Point();
        public int rotate = 0;
        public int page = 0;
        public bool visible = true;
        public int zorder = 0;
        public string text = "";
        public mediainformation mediainfo = new mediainformation(); // 情報

        // Box間のリンクを定義
        public List<LinkInfo> linkboxes = new List<LinkInfo>();

        public List<Point> edges()
        {
            // 上辺、右辺、下辺、左辺
            return new List<Point>() {
                new Point(location.X + size.Width / 2, location.Y),
                new Point(location.X + size.Width, location.Y + size.Height / 2),
                new Point(location.X + size.Width / 2, location.Y + size.Height),
                new Point(location.X, location.Y + size.Height / 2),
            };
        }

        public bool isInside(Point point, int tpage)
        {
            int minx = location.X;
            int miny = location.Y;
            int maxx = minx + size.Width;
            int maxy = miny + size.Height;
            if (point.X > minx && point.X < maxx &&
                point.Y > miny && point.Y < maxy &&
                page == tpage)
            {
                return true;
            }
            return false;
        }

        public Point center()
        {
            return new Point(location.X + size.Width / 2, location.Y + size.Height / 2);
        }
    }

    public class LinePoint
    {
        public Point start;
        public Point end;
        public Color color = Color.LightCoral;
        public int Width = 3;
    }

    public class LinkInfo
    {
        public string pairbox = "";
        public bool parent = false;
    }
}
