﻿using Newtonsoft.Json.Linq;
using PDF_Manager.Comon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PDF_Manager.Printing
{
    public class ReacCombine
    {
        public List<Reac> tx_max = new List<Reac>();
        public List<Reac> tx_min = new List<Reac>();
        public List<Reac> ty_max = new List<Reac>();
        public List<Reac> ty_min = new List<Reac>();
        public List<Reac> tz_max = new List<Reac>();
        public List<Reac> tz_min = new List<Reac>();
        public List<Reac> mx_max = new List<Reac>();
        public List<Reac> mx_min = new List<Reac>();
        public List<Reac> my_max = new List<Reac>();
        public List<Reac> my_min = new List<Reac>();
        public List<Reac> mz_max = new List<Reac>();
        public List<Reac> mz_min = new List<Reac>();


        public void Add(string key, Reac value)
        {
            // key と同じ名前の変数を取得する
            Type type = this.GetType();
            FieldInfo field = type.GetField(key);
            if (field == null)
            {
                throw new Exception(String.Format("ReacCombineクラスの変数{0} に値{1}を登録しようとしてエラーが発生しました", key, value));
            }
            var val = (List<Reac>)field.GetValue(this);

            // 変数に値を追加する
            val.Add(value);

            // 変数を更新する
            field.SetValue(this, val);
        }

    }


    class ResultReacCombine
    {
        public const string KEY = "reacCombine";

        private Dictionary<string, ReacCombine> reacs = new Dictionary<string, ReacCombine>();

        public ResultReacCombine(Dictionary<string, object> value, string key = ResultReacCombine.KEY)
        {
            if (!value.ContainsKey(key))
                return;

            // データを取得する．
            var target = JObject.FromObject(value[key]).ToObject<Dictionary<string, object>>();


            // データを抽出する
            for (var i = 0; i < target.Count; i++)
            {
                var No = dataManager.toString(target.ElementAt(i).Key);  // ケース番号
                var val = JToken.FromObject(target.ElementAt(i).Value);

                var Rec = ((JObject)val).ToObject<Dictionary<string, object>>();
                var _reac = ResultReacCombine.getReacCombine(Rec);

                this.reacs.Add(No, _reac);
            }
        }

        public static ReacCombine getReacCombine(Dictionary<string, object> Rec)
        {
            var _reac = new ReacCombine();

            for (int i = 0; i < Rec.Count; i++)
            {
                var elist = JObject.FromObject(Rec.ElementAt(i).Value).ToObject<Dictionary<string, object>>();
                var k = Rec.ElementAt(i).Key;

                for (int j = 0; j < elist.Count; j++)
                {
                    var item = JObject.FromObject(elist.ElementAt(j).Value);

                    var re = new Reac();

                    re.n = dataManager.toString(elist.ElementAt(j).Key);
                    re.tx = dataManager.parseDouble(item["tx"]);
                    re.ty = dataManager.parseDouble(item["ty"]);
                    re.tz = dataManager.parseDouble(item["tz"]);
                    re.mx = dataManager.parseDouble(item["mx"]);
                    re.my = dataManager.parseDouble(item["my"]);
                    re.mz = dataManager.parseDouble(item["mz"]);
                    re.caseStr = dataManager.toString(item["case"]);
                    re.comb = dataManager.toString(item["comb"]);

                    _reac.Add(k, re);
                }
            }
            return _reac;
        }

    }
}
