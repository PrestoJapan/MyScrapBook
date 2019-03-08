using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace MyScrapBook
{
    public partial class WebCameraForm : Form
    {
        WebBrowser web;
        MainForm main;

        public WebCameraForm()
        {
            InitializeComponent();
            web = new WebBrowser();
            web.Location = new Point(0, 0);
            web.Size = new Size(1920, 1080);
            web.ScriptErrorsSuppressed = true;
            web.Dock = DockStyle.Fill;
            web.ScrollBarsEnabled = true;
            panel1.Size = new Size(1920, 1080);
            panel1.Controls.Add(web);
        }

        public async void gotoweb(string url, MainForm fm)
        {
            // debug
            //web.Navigate(url);
            //main = fm;

            //var parameters = new Dictionary<string, string>()
            //{
            //    { "action", "snapshot" }
            //};
            //var client = new HttpClient();
            //var response = await client.GetAsync(@"http://172.16.1.145:14230/?action=snapshot");
            //Type img = response.Content.GetType();
            //byte[] data = await response.Content.ReadAsByteArrayAsync();
            //MemoryStream ms = new MemoryStream(data);
            //Bitmap bmp = new Bitmap(ms);
            //pictureBox1.Image = bmp;
            //ms.Close();
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
