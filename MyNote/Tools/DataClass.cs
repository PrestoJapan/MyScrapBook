using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyScrapBook
{
    // マルチリンクの情報
    public class MLINF
    {
        public string memo = "ScrapBook";
        public string author = "Yosuke Sugiyama 2019";
        public Size notesize = new Size() {Width = 1920, Height=1080};
        public List<BoxInfo> boxinfos;
    }
    
    // ボックスの情報
    public class MediaBoxsInfo
    {
        public BoxInfo boxinfo;
        public Control controlbox;

        public Point center()
        {
            return new Point(boxinfo.location.X + boxinfo.size.Width / 2 , boxinfo.location.Y + boxinfo.size.Height / 2);
        }
    }
    
    public class BoxInfo
    {
        //　Boxの区別はファイルの拡張子で行う
        public string filename = "";
        public Size size = new Size();
        public Point location = new Point();
        public int page = 0;
        public bool visible = true;
        public int zorder = 0;
        public string text = "";
        // Box間のリンクを定義
        public List<string> linkboxes = new List<string>();
    }
    
}
