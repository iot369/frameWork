using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using yynet.model;
using Dapper;

namespace yynet.sql{

    public partial class Bll_TB_ROLE: IROLE
    {
        public IEnumerable<TB_ROLE> GetList(int pageId,int pageSize,out int total)
        {
            total = 0;
            if (pageId <= 0)
                pageId = 1;
            if (pageSize <= 0)
                pageSize = 10;
            int offset = (pageId - 1) * pageSize;
            
            string sql = "";
            string sqlWhere = " where 1=1 ";
            string sqlOrder = " order by ROLE_ID ";

            List<TB_ROLE> list = null;

            using (var conn = Common.GetSqlConnection())
            {
                sql = "select `ROLE_ID`,`ROLE_NAME` from `TB_ROLE`"
                + sqlWhere + sqlOrder + " limit " + offset + "," + pageSize;
                list = conn.Query<TB_ROLE>(sql).ToList();
                sql = "select count(*) from `TB_ROLE` " + sqlWhere;
                total = conn.QuerySingle<int>(sql);
            }          

            return list as IEnumerable<TB_ROLE>;
        }
    }
}