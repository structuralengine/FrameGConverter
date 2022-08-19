﻿using Convert_Manager.FrameWebForJS;
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

        result.Add(node.KEY, node.GetElement(wdata));

    }

    // json 文字列を取得
    public string getJsonString()
    {
        //jsonに変換する
        string json = JsonConvert.SerializeObject(result);
        return json;
    }




}



