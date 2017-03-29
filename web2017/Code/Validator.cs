using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace yynet.web
{
    public class Validator
    {
        public static bool IsEmail(string str_Email)
        {
            for (int i = 0; i < str_Email.Length; ++i)
            {
                if (';' == str_Email[i])
                {
                    return false;
                }
            }

            return Regex.IsMatch(str_Email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }

        public static bool IPCheck(string IP)
        {
            string num = "(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)";
            return Regex.IsMatch(IP, ("^" + num + "\\." + num + "\\." + num + "\\." + num + "$"));
        }
        public static bool IsUrl(string str_url)
        {
            return Regex.IsMatch(str_url, @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }

    }
}