using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyScrapBook
{
    public partial class WebForm : Form
    {
        WebBrowser web;
        MainForm main;

        public WebForm()
        {
            InitializeComponent();
            web = new WebBrowser();
            web.Location = new Point(0, 0);
            web.Size = this.Size;
            web.ScriptErrorsSuppressed = true;
            web.Dock = DockStyle.Fill;
            web.ScrollBarsEnabled = true;
            panel1.Dock = DockStyle.Fill;
            panel1.Controls.Add(web);
        }

        public void gotoweb(string url, MainForm fm)
        {
            web.Navigate(url);
            main = fm;
        }

        private void WebForm_SizeChanged(object sender, EventArgs e)
        {
            web.Size = this.Size;
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            main.print2();
        }
    }
}
