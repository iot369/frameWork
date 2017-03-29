using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yynet.web
{
    public class CommonConfig
    {
        public static readonly int admin_page_size = 10;

        public static bool isForDemo
        {
            get
            {
                if ("true" == System.Configuration.ConfigurationManager.AppSettings["for_demo"])
                {
                    return true;
                }
                return false;
            }
        }
    }
}