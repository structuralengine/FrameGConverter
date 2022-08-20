using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class Element
    {
        public double E;
        // public double G;
        public double Xp;
        public double A;
        // public double J;
        public double Iy;
        public double Iz;
        public string name;

        // FrameWebには無い
        [NonSerialized]
        public string mark;
        [NonSerialized]
        public double n;
    }

    class element
    {
        public const string KEY = "element";

        protected Dictionary<string, Dictionary<string, Element>> ElementList = new Dictionary<string, Dictionary<string, Element>>();

        public Dictionary<string, Dictionary<string, Element>> GetElement()
        {
            return ElementList;
        }

        public void setElementList(List<object[]> ee)
        {
            // 材料集計
            for (var j = 0; j < 6; j++)
            {
                var eee = new Dictionary<string, Element>();
                foreach (var val in ee)
                {
                    var e = new Element();

                    var No = (string)val[0];
                    e.mark = (string)val[1];
                    e.name = (string)val[2];
                    var n = (double)val[3];
                    e.E = (double)val[4];
                    e.Xp = (double)val[5];
                    var A = (double[])val[6];
                    var Iz = (double[])val[7];

                    if (A[j] != 0)
                    {
                        e.A = A[j] * n;
                    }
                    if (Iz[j] != 0)
                    {
                        e.Iz = Iz[j] * n;
                    }
                    // 有効判定
                    if (e.E != 0 && e.Xp != 0 && e.A != 0 && e.Iz != 0)
                    {
                        eee.Add(No, e);
                    }

                }

                if (0 < eee.Count)
                    this.ElementList.Add((j + 1).ToString(), eee);
            }

        }
    }
}
