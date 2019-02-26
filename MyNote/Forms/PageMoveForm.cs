using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyScrapBook
{
    public partial class PageMoveForm : Form
    {
        Button targetpage;
        Dictionary<string, RichBoxsInfo> richBoxInfos;
        Dictionary<int, int> order = new Dictionary<int,int>();


        public PageMoveForm()
        {
            InitializeComponent();
        }

        public void setrichBoxInfo(Dictionary<string, RichBoxsInfo> info, int pagenumber)
        {
            richBoxInfos = info;
            for (int i = 1; i <= pagenumber; i++)
            {
                Button page = new Button();
                page.Text = i.ToString() + "ページ";
                page.Tag = i;
                page.Size = new Size(75, 25);
                page.BackColor = Color.White;
                page.MouseDown += PageBox_MouseDown;                
                flowLayoutPanel1.Controls.Add(page);
            }
        }

        private void PageBox_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Button tb in flowLayoutPanel1.Controls) tb.BackColor = Color.White;
            targetpage = sender as Button;
            targetpage.BackColor = Color.OrangeRed;
        }

        private void Up_Click(object sender, EventArgs e)
        {
            if (targetpage == null) return;
            flowLayoutPanel1.Controls.SetChildIndex(targetpage, flowLayoutPanel1.Controls.GetChildIndex(targetpage) - 1);
        }

        private void Down_Click(object sender, EventArgs e)
        {
            if (targetpage == null) return;
            flowLayoutPanel1.Controls.SetChildIndex(targetpage, flowLayoutPanel1.Controls.GetChildIndex(targetpage) + 1);
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (Button btn in flowLayoutPanel1.Controls) {
                order.Add(i++, (int)btn.Tag);
            }

            foreach ( RichBoxsInfo val in richBoxInfos.Values) {
                val.boxinfo.page = order[val.boxinfo.page];
            }
            this.Close();

        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
