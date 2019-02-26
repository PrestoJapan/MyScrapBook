using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MyScrapBook
{
    public partial class MainForm : Form
    {
        private DialogResult yesno(string caption, string message)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            return MessageBox.Show(this, message, caption, buttons, MessageBoxIcon.Question);
        }

        private DialogResult yes(string caption, string message)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            return MessageBox.Show(this, message, caption, buttons, MessageBoxIcon.Question);
        }

        private string savefilename(string suffix)
        {
            string datepath = DateTime.Now.ToShortDateString().Replace("/", "-") + "T" +
                DateTime.Now.ToLongTimeString().Replace(":", "-") + "." + suffix;
            return datepath;
        }

        public bool readNote()
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

                readMethod(ofd.FileName);
                return true;
            } else
            {
                return false;
            }
        }

        private void readMethod(string filename)
        {
            radios = new List<RadioButton>();
            try
            {
                MLINF metafile = JsonConvert.DeserializeObject<MLINF>(File.ReadAllText(filename));
                saveboxinfos = metafile.boxinfos;
            }
            catch (Exception ex)
            {
                saveboxinfos = JsonConvert.DeserializeObject<List<BoxInfo>>(File.ReadAllText(filename));
            }

            int count = 0;
            //　初期化
            maxpage = 1;
            foreach (string fkey in richBoxInfos.Keys)
            {
                if (richBoxInfos[fkey].boxinfo.page == getCurrentPage())
                {
                    Controls.Remove(richBoxInfos[fkey].controlbox);
                }
            }
            richBoxInfos = new Dictionary<string, RichBoxsInfo>();

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
                        RichBoxsInfo rinfo = new RichBoxsInfo();
                        rinfo.boxinfo = info;
                        rinfo.controlbox = rbox;
                        if (rinfo.boxinfo.page == 0) zeropage = true;
                        // サイズと位置
                        rbox.Size = info.size;
                        rbox.Location = info.location;
                        rbox.Tag = info.filename;
                        info.text = rbox.Text;
                        rbox.ReadOnly = true;

                        richBoxInfos.Add(info.filename, rinfo);
                    }
                    else if (info.filename.Contains("png"))
                    {
                        // Imageの追加
                        PictureBox pic = createPictureBox();
                        RichBoxsInfo rinfo = new RichBoxsInfo();
                        rinfo.boxinfo = info;
                        rinfo.controlbox = pic;
                        if (rinfo.boxinfo.page == 0) zeropage = true;
                        pic.Load(cfile);
                        pic.Location = info.location;
                        //                        pic.Size = pic.Image.Size;
                        pic.Size = rinfo.boxinfo.size; 
                        pic.Tag = info.filename;

                        richBoxInfos.Add(info.filename, rinfo);
                    }
                    if (count < info.page) { count = info.page; }
                }
                catch (Exception ex)
                {
                    // ファイルが見つからないエラー
                }
            }
            if (zeropage) foreach (RichBoxsInfo info in richBoxInfos.Values) { info.boxinfo.page++; }
            pagedraw(1);
            onofftoolStrip(true);
            return;
        }

        private void saveNote()
        {
            saveboxinfos = new List<BoxInfo>();
            foreach (string timekey in richBoxInfos.Keys)
            {
                saveboxinfos.Add(richBoxInfos[timekey].boxinfo);
            }
            MLINF metafile = new MLINF() {boxinfos = saveboxinfos , notesize = setPageSize(toolStripPageSize.Text)};
            File.WriteAllText(getWorkfilefullpath(), JsonConvert.SerializeObject(metafile));
            MessageBox.Show(Properties.Settings.Default.workingFile + "を上書き保存しました。");
        }

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
                }
                this.Text = String.Format("{0}  {1} ページ", getWorkfile(), getCurrentPage());
            } else
            {
                return false;
            }
            return true;
        }

        private string getWorkDir()
        {
            return Properties.Settings.Default.workingDirectory;
        }

        private string getWorkfilefullpath()
        {
            return Path.Combine(Properties.Settings.Default.workingDirectory, Properties.Settings.Default.workingFile);
        }

        private string getWorkfile()
        {
            return Properties.Settings.Default.workingFile;
        }

        private string getWorkSubDir()
        {
            string namewithout = Properties.Settings.Default.workingFile.Replace("mlinf","").Replace(".","");
            return Path.Combine(Properties.Settings.Default.workingDirectory, namewithout);
        }

    }
}
