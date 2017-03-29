using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yynet.web
{
    public class IpHelper
    {
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            if (HttpContext.Current == null) return "localhost";
            HttpRequest request = HttpContext.Current.Request;

            //获取客户端真实IP
            string clientIp = request.Headers["CDN-SRC-IP"];

            if (string.IsNullOrEmpty(clientIp))
            {
                clientIp = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

            if (string.IsNullOrEmpty(clientIp))
            {
                clientIp = request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(clientIp))
            {
                clientIp = request.UserHostAddress;
            }

            return clientIp;
        }
    }
}