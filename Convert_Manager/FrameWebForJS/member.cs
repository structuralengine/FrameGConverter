using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class Member
    {
        public string ni; // 節点番号
        public string nj;
        public string e;  // 材料番号
        //public double cg; // コードアングル
    }


    class member : element
    {
        public const string KEY = "member";
        private const string wFile = "Buzai.tmp";

        private Dictionary<string, Member> MemberList = new Dictionary<string, Member>();


        public member(Dictionary<string, string> wdata)
        {
            if (!wdata.ContainsKey(member.wFile))
                return;

            var str = wdata[member.wFile];

            // 1行の抽出
            var lst1 = new List<string>();
            while (str.Length > 0)
            {
                string line = comon.byteSubstr(ref str, 244);
                lst1.Add(line);
            }

            var ee = new List<object[]>();

            foreach (var line in lst1)
            {
                str = line;

                /// 部材
                var m = new Member();

                string tmp = comon.byteSubstr(ref str, 3);
                string No = tmp.Trim();

                tmp = comon.byteSubstr(ref str, 10).Trim();
                m.ni = tmp.Trim();

                tmp = comon.byteSubstr(ref str, 10).Trim();
                m.nj = tmp.Trim();

                m.e = No;

                // 部材集計
                if (!MemberList.ContainsKey(No))
                    if(0 < m.ni.Length + m.nj.Length)
                        this.MemberList.Add(No, m);

                /// 材料
                tmp = comon.byteSubstr(ref str, 32);
                var name = tmp.Trim();

                tmp = comon.byteSubstr(ref str, 10).Trim();
                var n = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 1;

                tmp = comon.byteSubstr(ref str, 10).Trim();
                var E = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                tmp = comon.byteSubstr(ref str, 10).Trim();
                var Xp = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                tmp = comon.byteSubstr(ref str, 5);
                string mark = tmp.Trim();

                if (name.Length == 0)
                    name = mark;

                double[] A = new double[6];
                for (var j = 0; j < 6; j++)
                {
                    tmp = comon.byteSubstr(ref str, 11).Trim();
                    A[j] = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;
                }

                double[] Iz = new double[6];
                for (var j = 0; j < 6; j++)
                {
                    tmp = comon.byteSubstr(ref str, 13).Trim();
                    Iz[j] = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;
                }

                ee.Add(new object[8] { No, mark, name, n, E, Xp, A, Iz });

            }

            // 材料集計
            base.setElementList(ee);


        }

        public Dictionary<string, Member> GetMember()
        {
            return MemberList;
        }


    }

}
