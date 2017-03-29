using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace yynet.sql
{
    public class Common
    {



        public static IDbConnection GetSqlConnection()
        {
            return new System.Data.SqlClient.SqlConnection(DbConfig.connStr); ;
        }

        public static string sqlNull(string str)
        {
            if (str == null)
                return "null";
            else
                return "'"+str+"'";
        }

        public static bool isLogEnabled
        {
            get
            {
                return true;
            }
        }

        public static void log(string userId, string msg)
        {

        }

        public static void error(string userId,string msg)
        {

        }
    }
}
