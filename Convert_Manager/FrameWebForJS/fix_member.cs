using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class FixMember
    {
        public int row;
        public string m;   // 部材番号
        public double tx;
        public double ty;
        //public double tz;
        //public double tr;
    }

    public class fix_member
    {
        public const string KEY = "fix_member";
        private const string wFile = "B_Bane.tmp";

        private Dictionary<string, List<FixMember>> FixMemberList = new Dictionary<string, List<FixMember>>();

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

            var kk = new List<object[]>();

            foreach (var line in lst1)
            {
                str = line;

                /// 部材
                string tmp = comon.byteSubstr(ref str, 3);
                string No = tmp.Trim();

                var KV = new double[16];
                // KV 部材直角方向
                for (int i=0; i<16; i++)
                {
                    tmp = comon.byteSubstr(ref str, 10).Trim();
                    KV[i] = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                }
                // KU 部材軸方向
                var KU = new double[16];
                for (int i = 0; i < 16; i++)
                {
                    tmp = comon.byteSubstr(ref str, 10).Trim();
                    KU[i] = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;
                }

                kk.Add(new object[3] { No, KV, KU });

            }

            // 集計
            for (var j = 0; j < 16; j++)
            {
                var kkk = new List<FixMember>();
                var row = 0;
                foreach (var val in kk)
                {
                    var e = new FixMember();

                    e.m = (string)val[0];
                    var KV = (double[])val[1];
                    var KU = (double[])val[2];

                    e.ty = KV[j];
                    e.tx = KU[j];

                    // 有効判定
                    if (e.ty != 0 && e.tx != 0) {
                        e.row = row;
                        kkk.Add(e);
                        row++;
                    }
                }

                if (0 < kkk.Count)
                    this.FixMemberList.Add((j + 1).ToString(), kkk);
            }

        }


        public Dictionary<string, List<FixMember>> GetFixMember()
        {
            return FixMemberList;
        }
    }
}
