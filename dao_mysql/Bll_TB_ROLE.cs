using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using yynet.model;

namespace yynet.sql{

    public partial class Bll_TB_ROLE: IROLE
    {

        public TB_ROLE Get(string ROLE_ID)
        {
            TB_ROLE m_TB_ROLE = null;

            using (var conn = Common.GetSqlConnection())
            {
                String sql = "select `ROLE_ID`,`ROLE_NAME` from `TB_ROLE` where `ROLE_ID`={@ROLE_ID}";
                sql = sql.Replace("{@ROLE_ID}", Common.sqlNull(ROLE_ID));
                var list = conn.Query<TB_ROLE>(sql).ToList();
                m_TB_ROLE = list.FirstOrDefault();
            }           
            return m_TB_ROLE;
        }

        public void Insert(TB_ROLE m_TB_ROLE)
        {

            String sql = "insert into TB_ROLE";
            sql += "(ROLE_ID,ROLE_NAME)";
            sql += " values(";
            sql += Common.sqlNull(m_TB_ROLE.ROLE_ID);
            sql += ",";
            sql += Common.sqlNull(m_TB_ROLE.ROLE_NAME);            
            sql += ");";
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            }
            
        }

   

        public void Update(TB_ROLE m_TB_ROLE)
        {
            string sql = "";         
            sql = "update `TB_ROLE` set `ROLE_NAME`={@ROLE_NAME} where `ROLE_ID` = {@ROLE_ID} ";
            sql = sql.Replace("{@ROLE_ID}", Common.sqlNull(m_TB_ROLE.ROLE_ID));
            sql = sql.Replace("{@ROLE_NAME}", Common.sqlNull(m_TB_ROLE.ROLE_NAME));
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            } 
        }



        public void Delete(string ROLE_ID)
        {
            String sql = "delete from  `TB_ROLE` where `ROLE_ID`={@ROLE_ID}";
            sql = sql.Replace("{@ROLE_ID}", Common.sqlNull(ROLE_ID));            
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            }
        }
        

        public IEnumerable<TB_ROLE> ListAll()
        {
            DataTable dt = new DataTable();

            String sql = "select `ROLE_ID`,`ROLE_NAME` from `TB_ROLE`";
            IEnumerable<TB_ROLE> list = null;

            using (var conn = Common.GetSqlConnection())
            {
                list = conn.Query<TB_ROLE>(sql);
            }

            return list;
        }
    }
}