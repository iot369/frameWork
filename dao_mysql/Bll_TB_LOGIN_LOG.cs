using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using yynet.model;
using System.Data;

namespace yynet.sql
{
    public class Bll_TB_LOGIN_LOG:ILOGINLOG
    {
        public void AddLog(TB_LOGIN_LOG log)
        {
            String sql = "insert into `TB_LOGIN_LOG`";
            sql += "(`LOG_USER_ID`,`LOG_IP`,`LOG_TIME`,`LOG_RESULT`)";
            sql += " values(";
            sql += Common.sqlNull(log.LOG_USER_ID);
            sql += ",";
            sql += Common.sqlNull(log.LOG_IP);
            sql += ",";
            sql += Common.sqlNull(log.LOG_TIME.ToString("yyyy-MM-dd HH:mm:ss"));
            sql += ",";        
            sql += Common.sqlNull(log.LOG_RESULT);
            sql += ");";
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            }
        }

        public IEnumerable<TB_LOGIN_LOG> GetList(int pageId, int pageSize, out int total)
        {
            total = 0;
            if (pageId <= 0)
                pageId = 1;
            if (pageSize <= 0)
                pageSize = 10;
            int offset =  (pageId - 1) * pageSize;

            string sql = "";
            string sqlWhere = " where 1=1 ";
            string sqlOrder = " order by LOG_ID ";
            List<TB_LOGIN_LOG> list = null;
            
            using (var conn = Common.GetSqlConnection())
            {
                sql = "select `LOG_ID`,`LOG_USER_ID`,`LOG_IP`,`LOG_TIME`,`LOG_RESULT` from `TB_LOGIN_LOG` "
                + sqlWhere + sqlOrder + " limit " + offset + "," + pageSize;
                list = conn.Query<TB_LOGIN_LOG>(sql).ToList();
                sql = "select count(*) from `TB_LOGIN_LOG` " + sqlWhere;
                total = conn.QuerySingle<int>(sql);
            }            

            return list as IEnumerable<TB_LOGIN_LOG>;
        }
    }
}
