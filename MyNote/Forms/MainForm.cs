﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyScrapBook
{
    public partial class MainForm : Form
    {
        #region MainForm
        public MainForm()
        {
            InitializeComponent();
            initializeMethod();
        }

        private void initializeMethod()
        {
//            this.MaximumSize = Screen.AllScreens[0].Bounds.Size;
            createContextMenu();
            BackColor = Color.Black;
            // workingpanelイベントの登録
            workingpanel.MouseDoubleClick += Workingpanel_MouseDoubleClick;
            workingpanel.AllowDrop = true;
            workingpanel.DragDrop += Workingpanel_DragDrop;
            workingpanel.DragEnter += Workingpanel_DragEnter;
            workingpanel.Size = setPageSize(toolStripPageSize.Text);
            // 
            toolStripPagesBox.DropDownItems.Add("ページ1");
            toolStripPagesBox.Text = toolStripPagesBox.DropDownItems[0].Text;
            onofftoolStrip(false);
        }


        #endregion MainForm
    }

}
