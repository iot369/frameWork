using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using yynet.model;
using Dapper;
using DotNetLib.utils;

namespace yynet.sql{

    public partial class Bll_TB_USER : IUSER
    {

        public void SetPassword(string User_ID, string Password)
        {
            String sql = "update TB_USER set PASSWORD=@Password where USER_ID=@USER_ID";
            sql = sql.Replace("@Password", Common.sqlNull(DES.Crypto(Password)));
            sql = sql.Replace("@USER_ID", Common.sqlNull(User_ID));          
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            }
        }

        public bool isPasswordCorrect(string User_ID, string Password)
        {
            TB_USER user = Get(User_ID);
            if (DES.Crypto(Password) == user.PASSWORD)
                return true;
            return false;
        }

        public IEnumerable<TB_USER> GetList(int pageId, int pageSize, out int total)
        {
            total = 0;
            if (pageId <= 0)
                pageId = 1;
            if (pageSize <= 0)
                pageSize = 10;
            int offset = (pageId - 1) * pageSize;


            string sql = "";
            string sqlWhere = " where 1=1 ";
            string sqlOrder = " order by USER_ID ";

            List<TB_USER> list = null;

            using (var conn = Common.GetSqlConnection())
            {
                sql = "select `USER_ID`,`PASSWORD`,`REAL_NAME`,`SEX`,`ACCOUNT_STATUS`,`EMAIL`,`USER_IMAGE_PATH`,`TITLE` from `TB_USER`"
                + sqlWhere + sqlOrder + " limit " + offset + "," + pageSize;
                list = conn.Query<TB_USER>(sql).ToList();
                sql = "select count(*) from `TB_USER` " + sqlWhere;
                total = conn.QuerySingle<int>(sql);
            }         

            return list as IEnumerable<TB_USER>;
        }
    }
}