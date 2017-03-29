using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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


            DataTable dt = new DataTable();
            string sql = "";
            string sqlWhere = " where 1=1 ";
            string sqlOrder = " order by USER_ID ";
            using (
            SqlConnection conn = new SqlConnection(DbConfig.connStr))
            {
                conn.Open();
                sql = "select [USER_ID],[PASSWORD],[REAL_NAME],[SEX],[ACCOUNT_STATUS],[EMAIL],[USER_IMAGE_PATH],[TITLE] from [TB_USER]" + sqlWhere + sqlOrder;
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

            IList<TB_USER> list = new List<TB_USER>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                TB_USER m = new TB_USER();
                m.USER_ID = dt.Rows[i]["USER_ID"] as string;
                m.PASSWORD = "";
                m.REAL_NAME = dt.Rows[i]["REAL_NAME"] as string;
                m.SEX = dt.Rows[i]["SEX"] as string;
                m.ACCOUNT_STATUS = dt.Rows[i]["ACCOUNT_STATUS"] as string;
                m.EMAIL = dt.Rows[i]["EMAIL"] as string;
                m.USER_IMAGE_PATH = dt.Rows[i]["USER_IMAGE_PATH"] as string;
                m.TITLE = dt.Rows[i]["TITLE"] as string;
                list.Add(m);
            }

            return list as IEnumerable<TB_USER>;
        }
    }
}