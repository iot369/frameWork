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
            String sql = "insert into [TB_OP_LOG]";
            sql += "([OP_USER_ID],[OPER_NAME],[OPER_IP],[OPER_TIME],[OPER_DESC])";
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

            DataTable dt = new DataTable();
            string sql = "";
            string sqlWhere = " where 1=1 ";
            string sqlOrder = " order by OP_ID ";

            using (

            SqlConnection conn = new SqlConnection(DbConfig.connStr))
            {
                conn.Open();
                sql = "select [OP_ID],[OP_USER_ID],[OPER_NAME],[OPER_IP],[OPER_TIME],[OPER_DESC] from [TB_OP_LOG]" + sqlWhere + sqlOrder;
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
            IList<TB_OP_LOG> list = new List<TB_OP_LOG>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                TB_OP_LOG m = new TB_OP_LOG();
                m.OP_ID = Convert.ToInt32(dt.Rows[i]["OP_ID"].ToString());
                m.OP_USER_ID = dt.Rows[i]["OP_USER_ID"] as string;
                m.OPER_NAME = dt.Rows[i]["OPER_NAME"] as string;
                m.OPER_IP = dt.Rows[i]["OPER_IP"] as string;
                m.OPER_TIME =(DateTime) dt.Rows[i]["OPER_TIME"];
                m.OPER_DESC = dt.Rows[i]["OPER_DESC"] as string;
                list.Add(m);
            }

            return list as IEnumerable<TB_OP_LOG>;
        }
    }
}
