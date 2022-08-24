using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class Gouiki
    {
        public double iDistance = 0;
        public double jDistance = 0;
        public double A = 0;
        public double I = 0;
    }

    public class gouiki
    {
        private const string wFile = "Buzai_G.tmp";

        private Dictionary<string, Gouiki> GouikiList = new Dictionary<string, Gouiki>();

        public string message = "";

        public gouiki(Dictionary<string, string> wdata)
        {
            if (!wdata.ContainsKey(gouiki.wFile))
                return;

            var str = wdata[gouiki.wFile];

            // 1行の抽出
            var lst1 = new List<string>();
            while (str.Length > 0)
            {
                string line = comon.byteSubstr(ref str, 56);
                lst1.Add(line);
            }

            int i = 1;
            foreach (var line in lst1)
            {
                str = line;

                /// 部材
                var g = new Gouiki();

                string tmp = comon.byteSubstr(ref str, 14).Trim();
                g.jDistance = (0 < tmp.Length) ? Convert.ToDouble(tmp): 0;

                tmp = comon.byteSubstr(ref str, 14).Trim();
                g.iDistance = (0 < tmp.Length) ? Convert.ToDouble(tmp): 0;

                tmp = comon.byteSubstr(ref str, 14).Trim();
                g.A = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                tmp = comon.byteSubstr(ref str, 14).Trim();
                g.I = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                GouikiList.Add(i.ToString(), g);
                i++;
            }

            foreach(var line in GouikiList)
            {
                var g = line.Value;
                if(g.iDistance != 0 || g.jDistance != 0)
                {
                    message = "剛域を有するデータの変換は対応していません。";
                    break;
                }
            }


        }
    }
}
