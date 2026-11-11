using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ql_tiembanhngot.ketnoi
{
    public class Database
    {
        private static string connectionString = @"Data Source=HTHOA\SQLEXPRESS;Initial Catalog=QuanLyTiemBanh;Integrated Security=True;Encrypt=False";

        // 1. Hàm lấy dữ liệu (SELECT) trả về DataTable, hỗ trợ truyền tham số
        public static DataTable GetData(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            conn.Open();
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Lỗi truy vấn dữ liệu: " + ex.Message);
                        }
                    }
                }
            }
            return dt;
        }

        // 2. Hàm thực thi câu lệnh (INSERT, UPDATE, DELETE), hỗ trợ tham số
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    try
                    {
                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi thực thi câu lệnh: " + ex.Message);
                    }
                }
            }
            return rowsAffected;
        }

        // 3. Hàm lấy giá trị đơn (COUNT, MAX, SUM,...)
        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    try
                    {
                        conn.Open();
                        result = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi thực thi Scalar: " + ex.Message);
                    }
                }
            }
            return result;
        }
    }
}