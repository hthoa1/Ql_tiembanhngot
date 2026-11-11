using Ql_tiembanhngot.ketnoi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ql_tiembanhngot
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["TenDangNhap"] != null)
            {
                Response.Redirect("~/aspx/ThongKe.aspx");
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!");
                return;
            }

            try
            {
                string sql = "SELECT TenDangNhap, Quyen, MaNV FROM TaiKhoan WHERE TenDangNhap = @User AND MatKhau = @Pass";
                SqlParameter[] p = {
                    new SqlParameter("@User", username),
                    new SqlParameter("@Pass", password)
                };

                DataTable dt = Database.GetData(sql, p);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Session["TenDangNhap"] = dt.Rows[0]["TenDangNhap"].ToString();
                    Session["Quyen"] = dt.Rows[0]["Quyen"].ToString();
                    Session["MaNV"] = dt.Rows[0]["MaNV"].ToString();

                    Response.Redirect("~/aspx/ThongKe.aspx");
                }
                else
                {
                    ShowError("Tên đăng nhập hoặc mật khẩu không chính xác!");
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi kết nối CSDL: " + ex.Message);
            }
        }

        private void ShowError(string msg)
        {
            pnlError.Visible = true;
            lblError.Text = msg;
        }
    }
}