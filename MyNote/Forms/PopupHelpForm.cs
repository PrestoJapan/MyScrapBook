using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MyScrapBook
{
    public partial class PopupHelpForm : Form
    {
        public PopupHelpForm()
        {
            InitializeComponent();
            //現在のコードを実行しているAssemblyを取得
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            //指定されたマニフェストリソースを読み込む
            string[] names = myAssembly.GetManifestResourceNames();

            Stream stream = myAssembly.GetManifestResourceStream("MyScrapBook.help.rtf");
            RichTextBox1.LoadFile(stream, RichTextBoxStreamType.RichText);
        }

    }
}
