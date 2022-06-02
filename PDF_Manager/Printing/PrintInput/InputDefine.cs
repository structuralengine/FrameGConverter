﻿using Newtonsoft.Json.Linq;
using PDF_Manager.Comon;
using System.Collections.Generic;
using System.Linq;

namespace PDF_Manager.Printing
{
    public class Define
    {
        public Dictionary<string, int> no = new Dictionary<string, int>();
    }

    internal class InputDefine
    {
        public const string KEY = "define";

        private Dictionary<int, Define> defines = new Dictionary<int, Define>();

        public InputDefine(Dictionary<string, object> value)
        {
            if (!value.ContainsKey(KEY))
                return;

            // データを取得する．
            var target = JObject.FromObject(value[KEY]).ToObject<Dictionary<string, object>>();

            // データを抽出する
            for (var i = 0; i < target.Count; i++)
            {
                // defineNo
                var key = target.ElementAt(i).Key; 
                int index = dataManager.parseInt(key);

                // define を構成する 基本荷重No群
                var item = JObject.FromObject(target.ElementAt(i).Value).ToObject<Dictionary<string, object>>();

                var _define = new Define();
                for (int j = 0; j < item.Count; j++)
                {
                    var id = item.ElementAt(j).Key;  // "C1", "C2"...
                    if (!id.Contains("C"))
                        continue;

                    int no = dataManager.parseInt(item.ElementAt(j).Value);

                    _define.no.Add(id, no);
                }
                this.defines.Add(index, _define);
            }
        }



    }

    /*
    private Dictionary<string, object> value = new Dictionary<string, object>();
        List<List<string[]>> data = new List<List<string[]>>();

        public void init(PdfDoc mc, Dictionary<string, object> value_)
        {
            value = value_;
            //nodeデータを取得する
            var target = JObject.FromObject(value["define"]).ToObject<Dictionary<string, object>>();

            // 集まったデータはここに格納する
            data = new List<List<string[]>>();
            List<string[]> body = new List<string[]>();


            for (int i = 0; i < target.Count; i++)
            {
                string[] line = new String[11];
                line[0] = target.ElementAt(i).Key;
                var item = JObject.FromObject(target.ElementAt(i).Value);

                //Keyをsortするため
                var itemDic = JObject.FromObject(target.ElementAt(i).Value).ToObject<Dictionary<string, object>>();
                string[] kk = itemDic.Keys.ToArray();
                Array.Sort(kk);

                int count = 0;

                for (int j = 0; j < kk.Length - 1; j++)
                {
                    string key = kk[j];
                    line[count + 1] = dataManager.TypeChange(item[key]);
                    count++;
                    if (count == 10)
                    {
                        body.Add(line);
                        count = 0;
                        line = new string[11];
                        line[0] = "";
                    }
                }
                if (count > 0)
                {
                    for (int k = 1; k < 11; k++)
                    {
                        line[k] = line[k] == null ? "" : line[k];
                    }

                    body.Add(line);
                }
            }
            if (body.Count > 0)
            {
                data.Add(body);
            }


        public void DefinePDF(PdfDoc mc)
        {
            int bottomCell = mc.bottomCell;

            // 全行の取得
            int count = 20;
            for (int i = 0; i < data.Count; i++)
            {
                count += (data[i].Count + 2) * mc.single_Yrow;
            }
            // 改ページ判定
            mc.DataCountKeep(count);

            //　ヘッダー
            string[,] header_content = {
                { "DefineNo", "C1", "C2", "C3", "C4" , "C5", "C6", "C7", "C8", "C9", "C10"}
            };

            // タイトルの印刷
            switch (mc.language)
            {
                case "ja":
                    mc.PrintContent("Defineデータ", 0);
                    break;
                case "en":
                    mc.PrintContent("Define DATA", 0);
                    break;
            }

            mc.CurrentRow(2);

            // ヘッダーのx方向の余白
            int[,] header_Xspacing = {
                 { 20, 60, 100, 140, 180, 220, 260, 300, 340, 380, 420 },
            };

            mc.Header(header_content, header_Xspacing);

            // ボディーのx方向の余白
            int[,] body_Xspacing = { { 27, 67, 107, 147, 187, 227, 267, 307, 347, 387, 427 } };

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    for (int l = 0; l < data[i][j].Length; l++)
                    {
                        mc.CurrentColumn(body_Xspacing[0, l]); //x方向移動
                        mc.PrintContent(data[i][j][l]);  // print
                    }
                    if (!(i == data.Count - 1 && j == data[i].Count - 1))
                    {
                        mc.CurrentRow(1); // y方向移動
                    }
                }
            }

        }
    }
            */

}

