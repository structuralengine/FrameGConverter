using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    static class node
    {
        public const string KEY = "node";
        private const string wFile = "$1.txt";

        public static Dictionary<string, object> GetElement(Dictionary<string, string> wdata)
        {
            if (!wdata.ContainsKey(node.wFile))
                return null;

            var result = new Dictionary<string, object>();

            var str = wdata[node.wFile];
            string[] del = { "\r\n" };
            string[] arr = str.Split(del, StringSplitOptions.None);
            for(int i=1; i<arr.Length; i++)
            {
                var columns = arr[i].Split("\t");

                if (columns.Length < 3)
                    continue;

                var pos = new Dictionary<string, double>();
                pos.Add("x", Convert.ToDouble(columns[1]));
                pos.Add("y", Convert.ToDouble(columns[2]));

                result.Add( columns[0], pos );
            }

            return result;
        }
    }
}
