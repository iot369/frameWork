using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using yynet.model;

namespace yynet.sql{

    public partial class Bll_TB_PERMISSION: IPERMISSION
    {

        public TB_PERMISSION Get(string PERMISSION_ID)
        {
            TB_PERMISSION m_TB_PERMISSION = null;

            using (var conn = Common.GetSqlConnection())
            {
                String sql = "select `PERMISSION_ID`,`PERMISSION_NAME`,`PARENT_PERMISSION_ID` from `TB_PERMISSION` where `PERMISSION_ID`={@PERMISSION_ID}";
                sql = sql.Replace("{@PERMISSION_ID}", Common.sqlNull(PERMISSION_ID));
                var list = conn.Query<TB_PERMISSION>(sql).ToList();
                m_TB_PERMISSION = list.FirstOrDefault();
            }           
            return m_TB_PERMISSION;
        }

        public void Insert(TB_PERMISSION m_TB_PERMISSION)
        {
            String sql = "insert into TB_PERMISSION";
            sql += "(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID)";
            sql += " values(";
            sql += Common.sqlNull(m_TB_PERMISSION.PERMISSION_ID);
            sql += ",";
            sql += Common.sqlNull(m_TB_PERMISSION.PERMISSION_NAME);
            sql += ",";
            sql += Common.sqlNull(m_TB_PERMISSION.PARENT_PERMISSION_ID);
            sql += ");";
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            }
            
        }

        public bool existsChild(String permission_id)
        {
            string sql = "select count(*) from `TB_PERMISSION` where PARENT_PERMISSION_ID='"+permission_id+"'";
            using (var conn = Common.GetSqlConnection())
            {
                int result = conn.ExecuteScalar<int>(sql);
                if (result == 0)
                    return false;                
            }
            return true;
        }

        public void Update(TB_PERMISSION m_TB_PERMISSION)
        {
            string sql = "";         
            sql = "update `TB_PERMISSION` set `PERMISSION_NAME`={@PERMISSION_NAME},`PARENT_PERMISSION_ID`={@PARENT_PERMISSION_ID} where `PERMISSION_ID` = {@PERMISSION_ID} ";
            sql = sql.Replace("{@PERMISSION_ID}", Common.sqlNull(m_TB_PERMISSION.PERMISSION_ID));
            sql = sql.Replace("{@PERMISSION_NAME}", Common.sqlNull(m_TB_PERMISSION.PERMISSION_NAME));
            sql = sql.Replace("{@PARENT_PERMISSION_ID}", Common.sqlNull(m_TB_PERMISSION.PARENT_PERMISSION_ID));
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            } 
        }



        public void Delete(string PERMISSION_ID)
        {
            String sql = "delete from  `TB_PERMISSION` where `PERMISSION_ID`={@PERMISSION_ID}";
            sql = sql.Replace("{@PERMISSION_ID}", Common.sqlNull(PERMISSION_ID));            
            using (var conn = Common.GetSqlConnection())
            {
                var n = conn.Execute(sql);
            }
        }





        public IEnumerable<TB_PERMISSION> ListAll()
        {
            DataTable dt = new DataTable();

            String sql = "select `PERMISSION_ID`,`PERMISSION_NAME`,`PARENT_PERMISSION_ID` from `TB_PERMISSION`";
            IEnumerable<TB_PERMISSION> list = null;

            using (var conn = Common.GetSqlConnection())
            {
                list = conn.Query<TB_PERMISSION>(sql);
            }

            return list;
        }
    }
}