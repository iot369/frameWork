using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yynet.web
{
    public class CookieHelper
    {
        public static void cookie_set(string key, string value, HttpResponse Response, DateTime expireDate)
        {
            Response.Cookies[key].Value = HttpUtility.UrlEncode(value);
            Response.Cookies[key].Expires = expireDate;
        }

        public static void cookie_clear(string key, HttpRequest Request, HttpResponse Response)
        {
            HttpCookie cookie = Request.Cookies[key];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
        }

        public static string cookie_get(string key, HttpRequest Request)
        {
            string ret = "";
            if (Request.Cookies[key] != null)
            {
                ret = HttpUtility.UrlDecode(Request.Cookies[key].Value);
            }
            return ret;
        }
    }
}