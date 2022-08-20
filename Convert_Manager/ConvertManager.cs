using Convert_Manager.FrameWebForJS;
using Newtonsoft.Json;
using SevenZipExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class ConvertManager
{
    public Dictionary<string, string> wdata = new Dictionary<string, string>();

    private Dictionary<string, object> result = new Dictionary<string, object>();

    public ConvertManager(Stream BaseStream)
    {

        using (ArchiveFile archiveFile = new ArchiveFile(BaseStream, SevenZipFormat.Lzh))
        {
            foreach (Entry ent in archiveFile.Entries)
            {
                var ms = new MemoryStream();
                ent.Extract(ms);
                string str = Encoding.GetEncoding(932).GetString(ms.ToArray()); // MemoryStreamをstringに変換する
                wdata.Add(ent.FileName, str);
            }
        }

        // 節点
        var _node = new node(wdata);
        // 支点

        // 部材 と 材料
        var _member = new member(wdata);
        // バネ
        var _fix_member = new fix_member(wdata);
        // 結合

        // 着目点

        // 荷重

        // 組合せ

        // PickUP



        /// 組み換え処理
        // 剛域

        // 杭データ



        /// 書き出し
        // 節点
        result.Add(node.KEY, _node.GetNode());
        // 部材
        result.Add(member.KEY, _member.GetMember());
        // 材料
        result.Add(element.KEY, _member.GetElement());
        // バネ
        result.Add(fix_member.KEY, _fix_member.GetFixMember());

    }

    // json 文字列を取得
    public string getJsonString()
    {
        //jsonに変換する
        string json = JsonConvert.SerializeObject(result);
        return json;
    }




}



