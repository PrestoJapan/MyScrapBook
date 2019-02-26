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
    public partial class PopupForm : Form
    {
        public PopupForm()
        {
            InitializeComponent();
            textBox1.Text = @"フォームをダブルクリック: クリップボードからコピー
Shift + リッチテキストをダブルクリック: 削除
Ctrole + リッチテキストをダブルクリック: 拡大
Alt + リッチテキストをダブルクリック: 最前面

印刷: 表示されているフォームの可視部を画像ファイルにする
スクリーンショット：　スクリーンの部分ショットをクリップボードにコピーする
　　　クリック後マウスをドラッグし領域を指定する
ページ追加：　ページを追加する　追加したページは消せない
参照ページ：　表示中のフォームイメージを別のフォームで参照用として開く
　　　このページは編集不可
データフォルダ:　ノートのファイルが保存されたフォルダをエクスプローラで開く
背面黒：　背面を黒色に変え、ボックスの境界を鮮明にする
ページ並べ替え：　ページの順番を変える
Web表示：　Url(http://www.xxx.com)のみで構成されるボックスの場合、Webページを別のフォームで表示する
    
別フォームではマウスのドラッグで画像の移動、SHIFT+クリックで画像の縮小、CTRL+クリックで画像の拡大

";


            textBox1.Font = new Font(textBox1.Font.OriginalFontName, 16);
            textBox1.Select(0, 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
