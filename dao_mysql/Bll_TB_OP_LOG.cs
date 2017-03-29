using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using yynet.model;
using System.Data;
using System.Data.SqlClient;

namespace yynet.sql
{
    public class Bll_TB_OP_LOG : IOPLOG
    {
        public void AddLog(TB_OP_LOG log)
        {
            String sql = "insert into `TB_OP_LOG`";
            sql += "(`OP_USER_ID`,`OPER_NAME`,`OPER_IP`,`OPER_TIME`,`OPER_DESC`)";
            sql += " values(";
            sql += Common.sqlNull(log.OP_USER_ID);
            sql += ",";
            sql += Common.sqlNull(log.OPER_NAME);
            sql += ",";
            sql += Common.sqlNull(log.OPER_IP);
            sql += ",";
            sql += Common.sqlNull(log.OPER_TIME.ToString("yyyy-MM-dd HH:mm:ss"));
            sql += ",";
            sql += Common.sqlNull(log.OPER_DESC);
            sql += ");";
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            }            
        }

        public IEnumerable<TB_OP_LOG> GetList(int pageId, int pageSize, out int total)
        {
            total = 0;
            if (pageId <= 0)
                pageId = 1;
            if (pageSize <= 0)
                pageSize = 10;
            int offset = (pageId - 1) * pageSize;

            List<TB_OP_LOG> list = null;

            string sql = "";
            string sqlWhere = " where 1=1 ";
            string sqlOrder = " order by OP_ID ";

            using (var conn = Common.GetSqlConnection())
            {
                sql = "select `OP_ID`,`OP_USER_ID`,`OPER_NAME`,`OPER_IP`,`OPER_TIME`,`OPER_DESC` from `TB_OP_LOG` "
                + sqlWhere + sqlOrder + " limit " + offset + "," + pageSize;
                list = conn.Query<TB_OP_LOG>(sql).ToList();
                sql = "select count(*) from `TB_OP_LOG` " + sqlWhere;
                total = conn.QuerySingle<int>(sql);
            }
          
            return list as IEnumerable<TB_OP_LOG>;
        }
    }
}
