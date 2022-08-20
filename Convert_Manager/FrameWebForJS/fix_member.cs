using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class FixMember
    {
        public string m;   // 部材番号
        public double tx;
        public double ty;
        public double tz;
        public double tr;
    }

    class fix_member
    {
        public const string KEY = "fix_member";
        private const string wFile = "B_Bane.tmp";

        private Dictionary<string, List<FixMember>> result = new Dictionary<string, List<FixMember>>();

        public fix_member(Dictionary<string, string> wdata)
        {
            if (!wdata.ContainsKey(fix_member.wFile))
                return;

            var str = wdata[fix_member.wFile];

            // 1行の抽出
            var lst1 = new List<string>();
            while (str.Length > 0)
            {
                string line = comon.byteSubstr(ref str, 323);
                lst1.Add(line);
            }

            foreach (var line in lst1)
            {
                str = line;


            }


            }


        public Dictionary<string, List<FixMember>> GetFixMember()
        {
            return result;
        }
    }
}
