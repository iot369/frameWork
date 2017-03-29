using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using yynet.model;

namespace yynet.sql{

    public class Bll_TB_USER_ROLE:IUSERROLE{

        public void Save(String User_ID, IEnumerable<String> RoleIDs)
        {
            using (var conn = Common.GetSqlConnection())
            {
                String sql = "delete from TB_USER_ROLE where USER_ID=@USER_ID";
                sql = sql.Replace("@USER_ID", Common.sqlNull(User_ID));
                conn.Execute(sql);
                foreach (string id in RoleIDs)
                {
                    sql = "insert into TB_USER_ROLE(USER_ID,ROLE_ID) values(@USER_ID,@ROLE_ID)";
                    sql = sql.Replace("@USER_ID", Common.sqlNull(User_ID));
                    sql = sql.Replace("@ROLE_ID", Common.sqlNull(id));
                    conn.Execute(sql);
                }
            }            
        }


        public IEnumerable<String> GetRoleIds(String User_ID)
        {
            IEnumerable<String> RoleIds = null;

            using (var conn = Common.GetSqlConnection())
            {
                String sql = "select `USER_ID`,`ROLE_ID` from `TB_USER_ROLE` where `USER_ID`={@USER_ID}";
                sql = sql.Replace("{@USER_ID}", Common.sqlNull(User_ID));
                var list = conn.Query<TB_USER_ROLE>(sql).ToList();
                IList<String> ps = new List<String>();
                foreach (TB_USER_ROLE r in list)
                {
                    ps.Add(r.ROLE_ID);
                }
                RoleIds = ps as IEnumerable<String>;
            }
            return RoleIds;
        }

    

     

      
    }
}