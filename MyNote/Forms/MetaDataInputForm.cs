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
    public partial class MetaDataInputForm : Form
    {
        mediainformation minfo = new mediainformation();

        public MetaDataInputForm()
        {
            InitializeComponent();
        }

        public MetaDataInputForm(mediainformation info)
        {
            InitializeComponent();
            setInfo(info);
        }

        //public MetaDataInputForm(int pagenumber, Dictionary<string, MediaBoxsInfo> allboxes)
        //{
        //    InitializeComponent();
        //    StringBuilder sb = new StringBuilder();
        //    foreach (MediaBoxsInfo box in allboxes.Values)
        //    {
        //        if (box.boxinfo.page == pagenumber)
        //        {
        //            sb.Append(box.getKeys());
        //        }
        //    }
        //    inputtext.Text = sb.ToString();
        //    inputtext.Dock = DockStyle.Fill;
        //    button1.Visible = false;
        //}

        public void setInfo(mediainformation info)
        {
            minfo = info;
            StringBuilder sb = new StringBuilder();
            foreach (string key in minfo.keys)
            {
                sb.Append(key + Environment.NewLine);
            }
            inputtext.Text = sb.ToString();
        }

        private void 登録_Click(object sender, EventArgs e)
        {
            minfo.keys = inputtext.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            this.Close();
        }

    }
}
