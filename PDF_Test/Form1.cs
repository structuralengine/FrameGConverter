using SevenZipExtractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GConvert_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // memo: Shift-JISを扱うためのおまじない
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 読み込みたいテキストを開く
            using (StreamReader st = new StreamReader(@"../../../TestData/4_2-FrameG.frd"))
            {
                using (ArchiveFile archiveFile = new ArchiveFile(st.BaseStream, SevenZipFormat.Lzh))
                {
                    //archiveFile.Extract("Output");
                    foreach(Entry ent in archiveFile.Entries)
                    {
                        string name = ent.FileName;

                        var compStream = new MemoryStream();
                        ent.Extract(@"../../../TestData/wdata/"+ name);
                        ent.Extract(compStream);

                        using (StreamReader st2 = new StreamReader(compStream,false))
                        {
                            var a = st2.ReadToEnd();
                        }
                    }

                }

                MessageBox.Show("ｵﾜﾀ＼(^o^)／");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(sender, null);
        }
    }
}
