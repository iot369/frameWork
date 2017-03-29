using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using yynet.model;

namespace yynet.sql
{
    public class Bll_TB_ROLE_PERMISSION:IROLEPERMISSION
    {
        public void Save(String Role_ID, IEnumerable<String> PermissionIDs)
        {
            using (var conn = Common.GetSqlConnection())
            {
                String sql = "delete from TB_ROLE_PERMISSION where ROLE_ID=@ROLE_ID";
                sql = sql.Replace("@ROLE_ID", Common.sqlNull(Role_ID));
                conn.Execute(sql);
                foreach(string id in PermissionIDs)
                {
                    sql = "insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values(@ROLE_ID,@PERMISSION_ID)";
                    sql = sql.Replace("@ROLE_ID", Common.sqlNull(Role_ID));
                    sql = sql.Replace("@PERMISSION_ID", Common.sqlNull(id));
                    conn.Execute(sql);
                }               
            }
        }

        public IEnumerable<String> GetPermissionIds(String Role_ID)
        {
            IEnumerable<String> permission_ids = null;

            using (var conn = Common.GetSqlConnection())
            {
                String sql = "select `ROLE_ID`,`PERMISSION_ID` from `TB_ROLE_PERMISSION` where `ROLE_ID`={@ROLE_ID}";
                sql = sql.Replace("{@ROLE_ID}", Common.sqlNull(Role_ID));
                var list = conn.Query<TB_ROLE_PERMISSION>(sql).ToList();
                IList<String> ps = new List<String>();
                foreach(TB_ROLE_PERMISSION r in list)
                {
                    ps.Add(r.PERMISSION_ID);
                }
                permission_ids = ps as IEnumerable<String>;
            }
            return permission_ids;
        }
    }
}
