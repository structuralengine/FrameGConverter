using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class LoadMember
    {
        public string m1;
        public string m2;
        public string direction;
        public string mark;
        public string L1;
        public double L2;
        public double P1;
        public double P2;
    }

    public class LoadNode
    {
        public string n;
        public double tx;
        public double ty;
        public double tz;
        public double rx;
        public double ry;
        public double rz;
    }

    public class Load
    {
        public int fix_node;
        public int fix_member;
        public int element;
        public int joint;
        public string symbol;
        public double LL_pitch;
        public double rate;
        public string name;
        public LoadNode[] load_node;
        public LoadMember[] load_member;

        [NonSerialized]
        public int inputCaseNo; // 実荷重番号
    }


    class load
    {
        public const string KEY = "load";
        private const string wFile1 = "Kajyu.tmp"; // 荷重名称

        private Dictionary<string, Load> LoadList = new Dictionary<string, Load>();


        public load(Dictionary<string, string> wdata)
        {
            if (!wdata.ContainsKey(load.wFile1))
                return;

            var str = wdata[load.wFile1];

            // 1行の抽出
            var lst1 = new List<string>();
            while (str.Length > 0)
            {
                string line = comon.byteSubstr(ref str, 88);
                lst1.Add(line);
            }

            for (var i= 0; i<lst1.Count; i++)
            {
                str = lst1[i];

                /// 部材
                var lo = new Load();

                string tmp = comon.byteSubstr(ref str, 10).Trim();
                lo.rate = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 1;

                tmp = comon.byteSubstr(ref str, 4).Trim();
                lo.inputCaseNo = (0 < tmp.Length) ? Convert.ToInt32(tmp) : -1;

                tmp = comon.byteSubstr(ref str, 5);
                lo.symbol = tmp.Trim();

                tmp = comon.byteSubstr(ref str, 41);
                lo.name = tmp.Trim();

                tmp = comon.byteSubstr(ref str, 3).Trim();
                lo.fix_node = (0 < tmp.Length) ? Convert.ToInt32(tmp) : 1;

                tmp = comon.byteSubstr(ref str, 3).Trim();
                lo.element = (0 < tmp.Length) ? Convert.ToInt32(tmp) : 1;

                tmp = comon.byteSubstr(ref str, 3).Trim();
                lo.fix_member = (0 < tmp.Length) ? Convert.ToInt32(tmp) : 1;

                tmp = comon.byteSubstr(ref str, 3).Trim();
                lo.joint = (0 < tmp.Length) ? Convert.ToInt32(tmp) : 1;

                LoadList.Add((i + 1).ToString(), lo);


            }



        }

        public Dictionary<string, Load> GetLoad()
        {
            return LoadList;
        }

    }
}
