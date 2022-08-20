using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    class member
    {
        public const string KEY = "member";
        private const string wFile = "Buzai.tmp";

        public static Dictionary<string, object> GetElement(Dictionary<string, string> wdata)
        {
            if (!wdata.ContainsKey(member.wFile))
                return null;

            var result = new Dictionary<string, object>();

            var str = wdata[member.wFile];
            // 1行の抽出
            var lst = new List<string>();
            while(str.Length>0)
            {
                string line = comon.byteSubstr(ref str, 244);
                lst.Add(line);
            }

            foreach(var line in lst)
            {
                str = line;
                // 部材番号
                int No = Convert.ToInt32(comon.byteSubstr(ref str, 3));
                int ni = Convert.ToInt32(comon.byteSubstr(ref str, 3));

            }


            return result;
        }
    }
}
