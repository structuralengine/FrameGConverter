using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class Vector3
    {
        public double x;
        public double y;
        //public double z;
    }

    class node
    {
        public const string KEY = "node";
        private const string wFile = "$1.txt";

        private Dictionary<string, Vector3>  NodeList = new Dictionary<string, Vector3>();

        public node(Dictionary<string, string> wdata)
        {
            if (!wdata.ContainsKey(node.wFile))
                return;

            var str = wdata[node.wFile];
            string[] del = { "\r\n" };
            string[] arr = str.Split(del, StringSplitOptions.None);
            for (int i = 1; i < arr.Length; i++)
            {
                var columns = arr[i].Split("\t");

                if (columns.Length < 3)
                    continue;

                var pos = new Vector3();
                pos.x = Convert.ToDouble(columns[1]);
                pos.y = Convert.ToDouble(columns[2]);

                NodeList.Add(columns[0], pos);
            }
        }


        public  Dictionary<string, Vector3> GetNode()
        {
            return NodeList;
        }
    }
}
