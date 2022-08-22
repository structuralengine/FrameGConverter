using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Manager.FrameWebForJS
{
    public class LoadMember
    {
        public int row;

        public string m1;
        public string m2;
        public string direction;
        public string mark;
        public string L1;
        public double L2;
        public double P1;
        public double P2;

        public bool Enable()
        {
            var val1 = m1.Trim().Length
                     + m2.Trim().Length
                     + L1.Trim().Length;
            var val2 = (L2 != 0);
            var val3 = (P1 != 0);
            var val4 = (P2 != 0);

            if (0 < val1 || val2 || val3 || val4 )
            {
                return true;
            }
            return false;
        }
    }

    public class LoadNode
    {
        public int row;

        public string n;
        public double tx;
        public double ty;
        //public double tz;
        //public double rx;
        //public double ry;
        public double rz;

        public bool Enable()
        {
            var val1 = n.Trim().Length;
            var val2 = (tx != 0);
            var val3 = (ty != 0);
            var val4 = (rz != 0);
            if (0 < val1 || val2 || val3 || val4)
            {
                return true;
            }
            return false;
        }
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
        public LoadNode[] load_node = new LoadNode[0];
        public LoadMember[] load_member = new LoadMember[0];

        [NonSerialized]
        public int inputCaseNo; // 実荷重番号
    }


    class load
    {
        public const string KEY = "load";
        private const string wFile1 = "Kajyu.tmp"; // 荷重名称
        private const string wFile2 = ""; // 合成荷重
        private const string wFile3 = ""; // 
        private const string wFile = "K_JituBuzai.tmp"; // 実荷重強度

        private Dictionary<string, Load> LoadList = new Dictionary<string, Load>();


        public load(Dictionary<string, string> wdata)
        {
            var tmpLoadList = new Dictionary<string, Load>();

            /// 荷重名称の読み込み
            if (wdata.ContainsKey(load.wFile1))
            {
                var str = wdata[load.wFile1];

                // 1行の抽出
                var lst1 = new List<string>();
                while (str.Length > 0)
                {
                    string line = comon.byteSubstr(ref str, 88);
                    lst1.Add(line);
                }

                for (var i = 0; i < lst1.Count; i++)
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

                    tmpLoadList.Add((i + 1).ToString(), lo);
                }

            }

            /// 荷重強度の読み込み
            if (wdata.ContainsKey(load.wFile))
            {
                var str = wdata[load.wFile];

                // 1行の抽出
                var lst1 = new List<string>();
                while (str.Length > 0)
                {
                    string line = comon.byteSubstr(ref str, 124);
                    lst1.Add(line);
                }

                for (var i = 0; i < lst1.Count; i++)
                {
                    str = lst1[i];

                    /// 部材
                    string tmp = comon.byteSubstr(ref str, 3).Trim();
                    var CaseNo = (0 < tmp.Length) ? Convert.ToInt32(tmp) : -1;

                    var key = "nothing";
                    foreach (var a in tmpLoadList){
                        if (a.Value.inputCaseNo == CaseNo){
                            key = a.Key;
                            break;
                        }
                    }

                    if (!tmpLoadList.ContainsKey(key))
                        continue;

                    var ll = tmpLoadList[key];

                    var ln = new List<LoadNode>(tmpLoadList[key].load_node);
                    var lm = new List<LoadMember>(tmpLoadList[key].load_member);

                    // 要素荷重
                    var fm = new LoadMember();

                    tmp = comon.byteSubstr(ref str, 3);
                    fm.m1 = tmp.Trim();

                    tmp = comon.byteSubstr(ref str, 3);
                    fm.m2 = gDecodeMinusIntegerStr(tmp.Trim());

                    tmp = comon.byteSubstr(ref str, 12);
                    fm.mark = tmp.Trim();

                    tmp = comon.byteSubstr(ref str, 12);
                    fm.L1 = tmp.Trim();

                    tmp = comon.byteSubstr(ref str, 12).Trim();
                    fm.L2 = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                    tmp = comon.byteSubstr(ref str, 12).Trim();
                    fm.P1 = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                    tmp = comon.byteSubstr(ref str, 12).Trim();
                    fm.P2 = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                    tmp = comon.byteSubstr(ref str, 12);
                    fm.direction = tmp.Trim(); // S V M H

                    if(fm.Enable())
                        lm.Add(fm);

                    // 節点荷重
                    var fn = new LoadNode();

                    tmp = comon.byteSubstr(ref str, 3);
                    fn.n = tmp.Trim();

                    tmp = comon.byteSubstr(ref str, 12).Trim();
                    fn.tx = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                    tmp = comon.byteSubstr(ref str, 11).Trim();
                    fn.ty = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                    tmp = comon.byteSubstr(ref str, 12).Trim();
                    fn.rz = (0 < tmp.Length) ? Convert.ToDouble(tmp) : 0;

                    if (fn.Enable())
                        ln.Add(fn);

                    // 登録
                    ll.load_node = ln.ToArray();
                    ll.load_member = lm.ToArray();

                }
            }

            /// FrameG -> FrameWebforJS に変換
            this.exchangeData(tmpLoadList);
        }

        /// <summary>
        /// FrameG -> FrameWebforJS に変換
        /// </summary>
        /// <param name="tmpLoadList"></param>
        private void exchangeData(Dictionary<string, Load> tmpLoadList)
        {

            LoadList = tmpLoadList;

            // row に入力する
            foreach(var a in LoadList)
            {
                var b = a.Value.load_member;
                for (var i=0; i<b.Length; i++)
                {
                    b[i].row = i + 1;
                }
                var c = a.Value.load_node;
                for (var i = 0; i < c.Length; i++)
                {
                    c[i].row = i + 1;
                }
            }
        }


        /// <summary>
        /// 英数字で表現された文字列をマイナス整数の文字列に変換する
        /// 実荷重の固定長文字列”st部材番号(n)”で桁数が足りなくなったために用意した
        /// </summary>
        /// <param name="rsValStr"></param>
        /// <returns></returns>
        private string gDecodeMinusIntegerStr(string rsValStr) 
        {
            string result = "";

            string SVAL = rsValStr.Trim();

            if (SVAL.Length == 0)
                return SVAL;

            char sTmp = SVAL[0];

            if (!char.IsWhiteSpace(sTmp) && char.IsLetter(sTmp)){
                var a1 = (int)sTmp;
                var a2 = (int)'A';
                var a3 = (int)'1';
                var a = a1 - a2 + a3;
                result = "-";
                result += Microsoft.VisualBasic.Strings.Chr(a);
                result += SVAL.Substring(1);
            }
            else {
                result = SVAL;
            }

            return result;
        }


        public Dictionary<string, Load> GetLoad()
        {
            return LoadList;
        }

    }
}
