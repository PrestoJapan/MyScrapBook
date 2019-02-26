using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace MyScrapBook
{
    public partial class CamaraForm : Form
    {
        private List<PictureBox> bigimages = new List<PictureBox>();
        private List<PictureBox> smallimages = new List<PictureBox>();

        public CamaraForm()
        {
            InitializeComponent();
            for (int i = 0; i < 4 * 10; i++)
            {
                bigimages.Add(new PictureBox() { Size = new Size(200, 200), SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.DarkGray });
                smallimages.Add(new PictureBox() { Size = new Size(80, 80), SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.DarkGray });
            }
            for (int i = 0; i < 4 * 10; i++)
            {
                flowLayoutPanel1.Controls.Add(bigimages[i]);
                flowLayoutPanel2.Controls.Add(smallimages[i]);
            }
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel2.AutoScroll = true;
            splitContainer1.Dock = DockStyle.Fill;
        }

        private string timestamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        private void saveImages(Bitmap bmp)
        {
            bmp.Save(@"c:\tmp\" + timestamp() + ".png");
        }

        private async void camerashot(int i, string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            Type img = response.Content.GetType();
            byte[] data = await response.Content.ReadAsByteArrayAsync();
            MemoryStream ms = new MemoryStream(data);
            Bitmap bmp = new Bitmap(ms);
            saveImages(bmp);
            bigimages[i].Image = bmp;
            smallimages[i].Image = bmp;
            ms.Close();
            Debug.WriteLineIf(true, i.ToString());
        }

        private void CamaraForm_Load(object sender, EventArgs e)
        {

        }

        int shotnumber = 0;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (shotnumber == 40) shotnumber = 0;
            // 別タスクで動くため、順番は保証されない。
            camerashot(shotnumber++, @"http://172.16.1.145:14230/?action=snapshot");
            camerashot(shotnumber++, @"http://172.16.1.165:8080/?action=snapshot");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (shotnumber == 40) shotnumber = 0;
            camerashot(shotnumber++, @"http://172.16.1.165:8080/?action=snapshot");

        }
    }
}
