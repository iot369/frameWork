using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using yynet.model;
using Dapper;

namespace yynet.sql{

    public partial class Bll_TB_PERMISSION: IPERMISSION
    {
        public IEnumerable<TB_PERMISSION> GetList(int pageId,int pageSize,out int total)
        {
            total = 0;
            if (pageId <= 0)
                pageId = 1;
            if (pageSize <= 0)
                pageSize = 10;
            int offset = (pageId - 1) * pageSize;

            
            string sql = "";
            string sqlWhere = " where 1=1 ";
            string sqlOrder = " order by PERMISSION_ID ";

            List<TB_PERMISSION> list = null;

            using (var conn = Common.GetSqlConnection())
            {
                sql = "select `PERMISSION_ID`,`PERMISSION_NAME`,`PARENT_PERMISSION_ID` from `TB_PERMISSION`"
                + sqlWhere + sqlOrder + " limit " + offset + "," + pageSize;
                list = conn.Query<TB_PERMISSION>(sql).ToList();
                sql = "select count(*) from `TB_PERMISSION` " + sqlWhere;
                total = conn.QuerySingle<int>(sql);
            }        

            return list as IEnumerable<TB_PERMISSION>;
        }
    }
}