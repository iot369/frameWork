using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using yynet.model;

namespace yynet.sql{

    public partial class Bll_TB_USER: IUSER{

        public TB_USER Get(string USER_ID)
        {
            TB_USER m_TB_USER = null;

            using (var conn = Common.GetSqlConnection())
            {
                String sql = "select `USER_ID`,`PASSWORD`,`REAL_NAME`,`SEX`,`ACCOUNT_STATUS`,`EMAIL`,`USER_IMAGE`,`USER_IMAGE_PATH`,`TITLE` from `TB_USER` where `USER_ID`={@USER_ID}";
                sql = sql.Replace("{@USER_ID}", Common.sqlNull(USER_ID));
                var list = conn.Query<TB_USER>(sql).ToList();
                m_TB_USER = list.FirstOrDefault();
            }
            return m_TB_USER;           
        }
    

        public void Insert(TB_USER m_TB_USER) 
        {
            String sql = "insert into TB_USER";
            sql += "(USER_ID,PASSWORD,REAL_NAME,SEX,ACCOUNT_STATUS,EMAIL,USER_IMAGE,USER_IMAGE_PATH,TITLE)";
            sql += " values(";
            sql += Common.sqlNull(m_TB_USER.USER_ID);
            sql += ",";
            sql += Common.sqlNull(m_TB_USER.PASSWORD);
            sql += ",";
            sql += Common.sqlNull(m_TB_USER.REAL_NAME);
            sql += ",";
            sql += Common.sqlNull(m_TB_USER.SEX);
            sql += ",";
            sql += Common.sqlNull(m_TB_USER.ACCOUNT_STATUS);
            sql += ",";
            sql += Common.sqlNull(m_TB_USER.EMAIL);
            sql += ",";
            sql += "@USER_IMAGE";
            sql += ",";
            sql += Common.sqlNull(m_TB_USER.USER_IMAGE_PATH);
            sql += ",";
            sql += Common.sqlNull(m_TB_USER.TITLE);
            sql += ");";
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql,new TB_USER { USER_IMAGE= m_TB_USER.USER_IMAGE });
            }
        }


       


        public void Update(TB_USER m_TB_USER) 
        {
            string sql = "";
            sql = "update `TB_USER` set `REAL_NAME`={@REAL_NAME},`SEX`={@SEX},`ACCOUNT_STATUS`={@ACCOUNT_STATUS},`EMAIL`={@EMAIL},USER_IMAGE=@USER_IMAGE,USER_IMAGE_PATH={@USER_IMAGE_PATH},TITLE={@TITLE} where `USER_ID` = {@USER_ID} ";
            sql = sql.Replace("{@USER_ID}", Common.sqlNull(m_TB_USER.USER_ID));
            sql = sql.Replace("{@REAL_NAME}", Common.sqlNull(m_TB_USER.REAL_NAME));
            sql = sql.Replace("{@SEX}", Common.sqlNull(m_TB_USER.SEX));
            sql = sql.Replace("{@ACCOUNT_STATUS}", Common.sqlNull(m_TB_USER.ACCOUNT_STATUS));
            sql = sql.Replace("{@EMAIL}", Common.sqlNull(m_TB_USER.EMAIL));
            sql = sql.Replace("{@TITLE}", Common.sqlNull(m_TB_USER.TITLE));
            sql = sql.Replace("{@USER_IMAGE_PATH}", Common.sqlNull(m_TB_USER.USER_IMAGE_PATH));            
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql,new TB_USER { USER_IMAGE = m_TB_USER.USER_IMAGE });
            }
        }
    

        public void Delete(string USER_ID)
        {
            String sql = "delete from  `TB_USER` where `USER_ID`={@USER_ID}";
            sql = sql.Replace("{@USER_ID}",Common.sqlNull(USER_ID));
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            }
        }



      
    }
}