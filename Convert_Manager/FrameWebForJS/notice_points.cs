using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class NoticePoint
    {
        public int row;

        public string m;   // 要素番号
        public double[] Points;
    }

    public class notice_points
    {
        public const string KEY = "notice_points";
        private const string wFile = "Chakumoku.tmp";

        private List<NoticePoint> noticepoints = new List<NoticePoint>();

        public notice_points(Dictionary<string, string> wdata)
        {
            if (!wdata.ContainsKey(notice_points.wFile))
                return;

            var str = wdata[notice_points.wFile];

            // 1行の抽出
            var lst1 = new List<string>();
            while (str.Length > 0)
            {
                string line = comon.byteSubstr(ref str, 1053);
                lst1.Add(line);
            }
            int row = 0;
            noticepoints = new List<NoticePoint>();
            foreach (var line in lst1)
            {
                str = line;

                var np = new NoticePoint();

                /// 部材
                string tmp = comon.byteSubstr(ref str, 4);
                np.m = tmp.Trim();

                // 
                var p = new List<double>();
                for (int i = 0; i < 50; i++)
                {
                    tmp = comon.byteSubstr(ref str, 9).Trim();
                    if(0 < tmp.Length)
                    {
                        p.Add(Convert.ToDouble(tmp));
                    }
                }
                if(0 < p.Count)
                {
                    var partitions = notice_points.partition<double>(p, 20);
                    foreach(var pnt in partitions)
                    {
                        noticepoints.Add(
                            new NoticePoint() { 
                                row = row,
                                m=np.m,
                                Points = pnt.ToArray()
                            });
                        row++;
                    }
                }

            }
        }

        private static List<List<T>> partition<T>(List<T> values, int chunkSize)
        {
            return values.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        public List<NoticePoint> GetNoticePoint()
        {
            return noticepoints;
        }
    }
}
