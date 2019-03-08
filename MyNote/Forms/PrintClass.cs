using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyScrapBook
{
    public partial class MainForm : Form
    {
        //********************************************************************************
        //
        //
        // 印刷のためにBMPにする

        //
        //********************************************************************************
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        static public extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        public Bitmap CaptureControl(Control ctrl)
        {
            ctrl.Size = workingpanel.Size;

            Bitmap img = new Bitmap(ctrl.Width, ctrl.Height);
            Graphics memg = Graphics.FromImage(img);
            IntPtr dc = memg.GetHdc();
            PrintWindow(ctrl.Handle, dc, 0);
            memg.ReleaseHdc(dc);
            memg.Dispose();
            return img;
        }

        private string printpath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\cpage.png";
        public void print2()
        {
            //コントロールの外観を描画するBitmapの作成
            Bitmap bmp = CaptureControl(workingpanel);
            bmp.Save(printpath);
            bmp.Dispose();

            //PrintDocumentオブジェクトの作成
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPage);

            //PrintDialogクラスの作成
            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
            File.Delete(printpath);
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //コントロールの外観を描画するBitmapの作成
            Image img = Image.FromFile(printpath);
            e.Graphics.DrawImage(img, e.PageBounds); // .MarginBounds);
            //次のページがないことを通知する
            e.HasMorePages = false;
            img.Dispose();
        }


    }
}
