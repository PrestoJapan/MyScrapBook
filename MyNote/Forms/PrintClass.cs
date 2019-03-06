using System;
using System.Collections.Generic;
using System.Drawing;
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
        //
        //********************************************************************************
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        static public extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        /// <summary>
        /// コントロールのイメージを取得する
        /// </summary>
        /// <param name="ctrl">キャプチャするコントロール</param>
        /// <returns>取得できたイメージ</returns>
        static public Bitmap CaptureControl(Control ctrl)
        {
            ctrl.Size = new Size(4000, 4000);
            Bitmap img = new Bitmap(ctrl.Width, ctrl.Height);
            Graphics memg = Graphics.FromImage(img);
            IntPtr dc = memg.GetHdc();
            PrintWindow(ctrl.Handle, dc, 0);
            memg.ReleaseHdc(dc);
            memg.Dispose();
            return img;
        }
    }
}
