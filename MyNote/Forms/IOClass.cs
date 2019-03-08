using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MyScrapBook
{
    public partial class MainForm : Form
    {
        #region ReadNote
        public bool readDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = Properties.Settings.Default.workingFile;
            ofd.InitialDirectory = Properties.Settings.Default.workingDirectory;
            ofd.Filter = "MyBookファイル(*.mlinf;)|*.mlinf|ノート(*.mlinf)|*.mlinf";
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                Properties.Settings.Default.workingDirectory = Path.GetDirectoryName(ofd.FileName);
                Properties.Settings.Default.workingFile = Path.GetFileName(ofd.FileName);

                readFile(ofd.FileName);
                return true;
            } else
            {
                return false;
            }
        }

        private void readFile(string filename)
        {
            radios = new List<RadioButton>();
            try
            {
                MLINF metafile = JsonConvert.DeserializeObject<MLINF>(File.ReadAllText(filename));
                // 内容を変数に割り当てる
                metaInfo.author = metafile.author;
                metaInfo.bookmetainfo = metafile.bookmetainfo;
                metaInfo.boxinfos = metafile.boxinfos;
                metaInfo.createDateTime = metafile.createDateTime;
                metaInfo.memo = metafile.memo;
                metaInfo.notesize = metafile.notesize;
                saveboxinfos = metafile.boxinfos;
                //　ページサイズを設定する
                setPage(metafile.notesize.Width.ToString() + " x " + metafile.notesize.Height.ToString());
            }
            catch (Exception ex)
            {
                // 移行の処理　多分2019 04から不要
                MessageBox.Show("ファイルのバージョンが異なります。");
                Application.Exit();
            }

            int count = 0;
            //　初期化
            maxpage = 1;
            foreach (string fkey in mediaBoxInfos.Keys)
            {
                if (mediaBoxInfos[fkey].boxinfo.page == getCurrentPage())
                {
                    Controls.Remove(mediaBoxInfos[fkey].controlbox);
                }
            }
            mediaBoxInfos = new Dictionary<string, MediaBoxsInfo>();

            Boolean zeropage = false;
            if (saveboxinfos == null) return;
            foreach (BoxInfo info in saveboxinfos)
            {
                if (!info.visible) continue;
                try
                {
                    string cfile = Path.Combine(getWorkSubDir(), info.filename);
                    if (info.filename.Contains("rtf"))
                    {
                        RichTextBox rbox = createRichBox();
                        rbox.LoadFile(cfile);
                        MediaBoxsInfo rinfo = new MediaBoxsInfo();
                        rinfo.boxinfo = info;
                        rinfo.controlbox = rbox;
                        if (rinfo.boxinfo.page == 0) zeropage = true;
                        // サイズと位置
                        rbox.Size = info.size;
                        rbox.Location = info.location;
                        rbox.Tag = info.filename;
                        info.text = rbox.Text;
                        rbox.ReadOnly = true;

                        mediaBoxInfos.Add(info.filename, rinfo);
                    }
                    else if (info.filename.Contains("png"))
                    {
                        // Imageの追加
                        PictureBox pic = createPictureBox();
                        MediaBoxsInfo rinfo = new MediaBoxsInfo();
                        rinfo.boxinfo = info;
                        rinfo.controlbox = pic;
                        if (rinfo.boxinfo.page == 0) zeropage = true;
                        pic.Load(cfile);
                        // 回転
                        for (int i=0;i< rinfo.boxinfo.rotate; i++) rotateControl(pic);
                        pic.Location = info.location;
                        rinfo.boxinfo.size = info.size;
                        //                        pic.Size = pic.Image.Size;
                        pic.Size = rinfo.boxinfo.size; 
                        pic.Tag = info.filename;

                        mediaBoxInfos.Add(info.filename, rinfo);
                    }
                    else if (info.filename.Contains("html"))
                    {
                        // Html Panelの追加
                        MediaBoxsInfo rinfo = new MediaBoxsInfo() { boxinfo = info }; 
                        Panel web = createWebBrowser(File.ReadAllText(cfile), rinfo.boxinfo);
                        rinfo.controlbox = web;
                        if (info.page == 0) zeropage = true;
                        mediaBoxInfos.Add(info.filename, rinfo);
                    }
                    if (count < info.page) { count = info.page; }
                }
                catch (Exception ex)
                {
                    // ファイルが見つからないエラー
                }
            }
            if (zeropage) foreach (MediaBoxsInfo info in mediaBoxInfos.Values) { info.boxinfo.page++; }
            pagedraw(1);
            onofftoolStrip(true);
            return;
        }

        public bool readNote0()
        {
            bool flag = readDialog();
            if (!flag) return false;
            // toolstripのページ修正
            int max = 1;
            foreach (MediaBoxsInfo info in mediaBoxInfos.Values)
            {
                if (info.boxinfo.page > max) max = info.boxinfo.page;
            }
            toolStripPagesBox.DropDownItems.Clear();
            for (int i = 1; i <= max; i++) { toolStripPagesBox.DropDownItems.Add(string.Format("ページ{0}", i)); }
            // 描画
            pagedraw(1);
            set_zOrderToControl(1);
            return true;
        }


        #endregion ReadNote

        #region SaveNote
        private void saveNote()
        {
            saveboxinfos = new List<BoxInfo>();
            foreach (string timekey in mediaBoxInfos.Keys)
            {
                saveboxinfos.Add(mediaBoxInfos[timekey].boxinfo);
            }
            metaInfo.bookmetainfo = MLINF.getAllBoxInfo(saveboxinfos);
            metaInfo.notesize = setPageSize(toolStripPageSize.Text);
            metaInfo.boxinfos = saveboxinfos;
            File.WriteAllText(getWorkfilefullpath(), JsonConvert.SerializeObject(metaInfo));
            MessageBox.Show(Properties.Settings.Default.workingFile + "を上書き保存しました。");
        }

        public void savedata()
        {
            if (editstart)
            {
                set_zOrderToInfo(getCurrentPage()); // 枠の前後
                saveNote();
            }
        }
        #endregion SaveNote

        #region NewNote
        private bool newBook()
        {
            //　Bookの作成
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "NewNote.mlinf";
            sfd.InitialDirectory = @"C:\";
            sfd.Filter = "MyBookファイル(*.mlinf;)|*.mlinf|ノート(*.mlinf)|*.mlinf";
            sfd.FilterIndex = 2;
            sfd.Title = "作成するファイル名を入力してください";
            sfd.RestoreDirectory = true;
            sfd.OverwritePrompt = true;
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.workingDirectory = Path.GetDirectoryName(sfd.FileName);
                Properties.Settings.Default.workingFile = Path.GetFileName(sfd.FileName);

                // ファイルとサブフォルダを作る
                if (File.Exists(sfd.FileName))
                {
                    //readMethod(sfd.FileName);
                    yes("エラー", "すでにノートは存在しています。ノートを開いて使ってください。");
                    return false;
                } else
                {
                    File.Create(sfd.FileName).Close();
                    if (!Directory.Exists(getWorkSubDir()))
                    {
                        Directory.CreateDirectory(getWorkSubDir());
                    }
                    metaInfo = new MLINF() { createDateTime = DateTime.Now };
                }
                this.Text = String.Format("{0}  {1} ページ", getWorkfile(), getCurrentPage());
            } else
            {
                return false;
            }
            return true;
        }
        #endregion NewNote

    }
}
