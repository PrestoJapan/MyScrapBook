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
    public partial class MetaDataOutputForm : Form
    {
        public MetaDataOutputForm()
        {
        }

        public MetaDataOutputForm(int page, Dictionary<string, MediaBoxsInfo> allboxes)
        {
            InitializeComponent();
            StringBuilder sb = new StringBuilder();
            foreach (MediaBoxsInfo box in allboxes.Values)
            {
                if (box.boxinfo.page == page)
                {
                    sb.Append(box.getKeys());
                }
            }
            textBox1.Text = sb.ToString();
        }
    }
}
