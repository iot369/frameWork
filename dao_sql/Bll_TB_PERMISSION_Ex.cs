using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using yynet.model;

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

            DataTable dt = new DataTable();
            string sql = "";
            string sqlWhere = " where 1=1 ";
            string sqlOrder = " order by PERMISSION_ID ";

            using (
               
            SqlConnection conn = new SqlConnection(DbConfig.connStr))
            {
                conn.Open();
                sql = "select [PERMISSION_ID],[PERMISSION_NAME],[PARENT_PERMISSION_ID] from [TB_PERMISSION]" + sqlWhere + sqlOrder;
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
                    total =  Convert.ToInt32(  pa_recordCount.Value);
                }
                conn.Close();
            }
            IList<TB_PERMISSION> list = new List<TB_PERMISSION>();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                TB_PERMISSION m = new TB_PERMISSION();
                m.PERMISSION_ID = dt.Rows[i]["PERMISSION_ID"] as string;
                m.PERMISSION_NAME = dt.Rows[i]["PERMISSION_NAME"] as string;
                if (dt.Rows[i]["PARENT_PERMISSION_ID"] != null)
                {
                    m.PARENT_PERMISSION_ID = dt.Rows[i]["PARENT_PERMISSION_ID"] as string;
                }
                list.Add(m);
            }

            return list as IEnumerable<TB_PERMISSION>;
        }
    }
}