using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using yynet.model;
using System.Data.SqlClient;
using System.Data;

namespace yynet.sql
{
    public class Bll_TB_LOGIN_LOG:ILOGINLOG
    {
        public void AddLog(TB_LOGIN_LOG log)
        {
            String sql = "insert into [TB_LOGIN_LOG]";
            sql += "([LOG_USER_ID],[LOG_IP],[LOG_TIME],[LOG_RESULT])";
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

            DataTable dt = new DataTable();
            string sql = "";
            string sqlWhere = " where 1=1 ";
            string sqlOrder = " order by LOG_ID ";

            using (

            SqlConnection conn = new SqlConnection(DbConfig.connStr))
            {
                conn.Open();
                sql = "select [LOG_ID],[LOG_USER_ID],[LOG_IP],[LOG_TIME],[LOG_RESULT] from [TB_LOGIN_LOG]" + sqlWhere + sqlOrder;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "p_splitpage";

                    SqlParameter pa_sql = new SqlParameter("@sql", sql);
                    SqlParameter pa_page = new SqlParameter("@page", pageId);
                    SqlParameter pa_pageSize = new SqlParameter("@pageSize", pageSize);
                    SqlParameter pa_pageCount = new SqlParameter();
                    {
                        pa_pageCount.ParameterName = "@pageCount";
                        pa_pageCount.Direction = ParameterDirection.InputOutput;
                        pa_pageCount.DbType = DbType.Int32;
                        pa_pageCount.Value = 0;
                    }

                    SqlParameter pa_recordCount = new SqlParameter();
                    {
                        pa_recordCount.ParameterName = "@recordCount";
                        pa_recordCount.Direction = ParameterDirection.InputOutput;
                        pa_recordCount.DbType = DbType.Int32;
                        pa_recordCount.Value = 0;
                    }
                    SqlParameter pa_searchTime = new SqlParameter();
                    {
                        pa_searchTime.ParameterName = "@SearchTime";
                        pa_searchTime.Direction = ParameterDirection.InputOutput;
                        pa_searchTime.DbType = DbType.Int32;
                        pa_searchTime.Value = 0;
                    }

                    cmd.Parameters.Add(pa_sql);
                    cmd.Parameters.Add(pa_page);
                    cmd.Parameters.Add(pa_pageSize);
                    cmd.Parameters.Add(pa_pageCount);
                    cmd.Parameters.Add(pa_recordCount);
                    cmd.Parameters.Add(pa_searchTime);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[1];
                    total = Convert.ToInt32(pa_recordCount.Value);
                }
                conn.Close();
            }
            IList<TB_LOGIN_LOG> list = new List<TB_LOGIN_LOG>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                TB_LOGIN_LOG m = new TB_LOGIN_LOG();
                m.LOG_ID = Convert.ToInt32(dt.Rows[i]["LOG_ID"].ToString());
                m.LOG_USER_ID = dt.Rows[i]["LOG_USER_ID"] as string;
                m.LOG_IP = dt.Rows[i]["LOG_IP"] as string;
                m.LOG_TIME = (DateTime)dt.Rows[i]["LOG_TIME"];
                m.LOG_RESULT = dt.Rows[i]["LOG_RESULT"] as string;
                list.Add(m);
            }

            return list as IEnumerable<TB_LOGIN_LOG>;
        }
    }
}
