using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yynet.web
{
    public class CollectionUtilitity
    {

        private static IDictionary<string,string> to_dic(IList<string> list)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach(string t in list)
            {
                if (!dic.ContainsKey(t))
                {
                    dic.Add(t,t);
                }
            }
            return dic;
        }


        public static string compare (string a,string b,
            IList<string> list_a,IList<string> list_b)
        {
            string result = "";
            IList<string> ma = new List<string>();
            IList<string> mb = new List<string>();
            IDictionary<string, string> dic_a = to_dic(list_a);
            IDictionary<string, string> dic_b = to_dic(list_b);
            foreach(string t in dic_a.Keys)
            {
                if (!dic_b.ContainsKey(t))
                {
                    ma.Add(t);
                }
            }
            foreach (string t in dic_b.Keys)
            {
                if (!dic_a.ContainsKey(t))
                {
                    mb.Add(t);
                }
            }
            if (ma.Count > 0)
            {
                result += a+"中包含但"+b+"中不包含的元素包括（";
                foreach(string t in ma)
                {
                    result += t + ",";
                }
                result = result.Substring(0, result.Length - 1);
                result += "）";
            }
            if (mb.Count > 0)
            {
                if (ma.Count > 0)
                {
                    result += ",";
                }
                result += b + "中包含但" + a + "中不包含的元素包括（";
                foreach (string t in mb)
                {
                    result += t+",";
                }
                result = result.Substring(0, result.Length - 1);
                result += "）";
            }
            return result;
        }
    }
}