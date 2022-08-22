using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class Define
    {
        public int row;
        public Dictionary<string, int> CaseNumbers = new Dictionary<string, int>();
    }

    class define
    {
        public const string KEY = "define";
        private const string wFile1 = "Ku_DefNo.tmp";
        private const string wFile2 = "Ku_Define2.tmp";

        Dictionary<string, Define> DefineList = new Dictionary<string, Define>();

        public define(Dictionary<string, string> wdata)
        {
            /// 番号
            if (wdata.ContainsKey(define.wFile1))
            {
                var str = wdata[define.wFile1];
                while (str.Length > 9)
                {
                    var tmp = comon.byteSubstr(ref str, 10);
                    var no = (0 < tmp.Length) ? Convert.ToInt32(tmp) : -1;

                }
            }

            /// 内容
            if (wdata.ContainsKey(define.wFile2))
            {
                var str = wdata[define.wFile2];

            }




        }


        public Dictionary<string, Dictionary<string, int>> GetDefine()
        {
            var result = new Dictionary<string, Dictionary<string, int>>();

            foreach(var a in DefineList)
            {
                var b = new Dictionary<string, int>();
                b.Add("row", a.Value.row);
                foreach(var c in a.Value.CaseNumbers)
                {
                    b.Add(c.Key, c.Value);
                }
                result.Add(a.Key, b);
            }

            return result;
        }
    }
}
