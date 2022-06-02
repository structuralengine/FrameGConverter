﻿using PDF_Manager;
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

namespace PDF_Test
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
                // テキストファイルをString型で読み込みコンソールに表示
                String line = st.ReadToEnd();

                // データの読み込み
                var p = new PrintInput(line);

                p.createPDF();

                MessageBox.Show("ｵﾜﾀ＼(^o^)／");
            }
        }
    }
}
