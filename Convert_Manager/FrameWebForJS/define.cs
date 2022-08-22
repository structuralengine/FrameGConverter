using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class Define
    {
        public int row;
        public Dictionary<string, int> no = new Dictionary<string, int>();
    }

    class define
    {
        public const string KEY = "define";
        private const string wFile = "Ku_Define2.tmp";

        Dictionary<string, Define> DefineList = new Dictionary<string, Define>();

        public define(Dictionary<string, string> wdata)
        {
            if (!wdata.ContainsKey(define.wFile))
                return;

            var str = wdata[define.wFile];
            string[] del = { "\r\n" };
            string[] arr = str.Split(del, StringSplitOptions.None);

            for (int i = 1; i < arr.Length; i++)
            {
                var lst1 = arr[i].Split("\t");
            }
        }


        public Dictionary<string, Dictionary<string, int>> GetDefine()
        {
            var result = new Dictionary<string, Dictionary<string, int>>();

            foreach(var a in DefineList)
            {
                var b = new Dictionary<string, int>();
                b.Add("row", a.Value.row);
                foreach(var c in a.Value.no)
                {
                    b.Add(c.Key, c.Value);
                }
                result.Add(a.Key, b);
            }

            return result;
        }
    }
}
