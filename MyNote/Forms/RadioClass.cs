using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyScrapBook
{
    public partial class MainForm : Form
    {
        private List<RadioButton> radios = new List<RadioButton>();

        private void addPageRadio(int count)
        {
            pagePanel.Controls.Clear();
            for (int i = 0; i < count; i++)
            {
                RadioButton cb = new RadioButton() { Text = (i + 1).ToString(), Tag = i, Width = 40 };
                if (i == 0) cb.Checked = true; // 必ずCheckedChangedの定義前に設定すること　
                cb.CheckedChanged += CheckedChanged;
                pagePanel.Controls.Add(cb);
                radios.Add(cb);
            }
            maxpage = count;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            RadioButton cb = sender as RadioButton;
            if (cb.Checked)
            {
                pagedraw((int)cb.Tag);
                set_zOrderToControl((int)cb.Tag);
            } else
            {
                set_zOrderToInfo(getCurrentPage());
            }
        }


    }
}
