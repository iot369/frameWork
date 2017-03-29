using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace yynet.sql
{
    public class DbConfig
    {
        public static readonly string connStr =
           ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
    }
}
