using Ql_tiembanhngot.ketnoi;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ql_tiembanhngot.aspx
{
    public partial class CaiDat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
        }
        protected void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string username = Session["TenDangNhap"].ToString();
            string matKhauCu = txtMatKhauCu.Text.Trim();
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string xacNhan = txtXacNhanMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(xacNhan))
            {
                ShowAlert("Vui lòng nhập đầy đủ thông tin mật khẩu!", "alert-danger");
                return;
            }

            if (matKhauMoi != xacNhan)
            {
                ShowAlert("Mật khẩu mới và xác nhận mật khẩu không khớp!", "alert-danger");
                return;
            }

            try
            {
                // 1. Kiểm tra mật khẩu cũ có chính xác không
                string checkSql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @User AND MatKhau = @Pass";
                SqlParameter[] checkParams = {
                    new SqlParameter("@User", username),
                    new SqlParameter("@Pass", matKhauCu)
                };

                System.Data.DataTable dt = Database.GetData(checkSql, checkParams);
                int count = Convert.ToInt32(dt.Rows[0][0]);

                if (count > 0)
                {
                    // 2. Tiến hành cập nhật mật khẩu mới
                    string updateSql = "UPDATE TaiKhoan SET MatKhau = @NewPass WHERE TenDangNhap = @User";
                    SqlParameter[] updateParams = {
                        new SqlParameter("@NewPass", matKhauMoi),
                        new SqlParameter("@User", username)
                    };

                    Database.ExecuteNonQuery(updateSql, updateParams);
                    ShowAlert("Đổi mật khẩu thành công!", "alert-success");
                    ClearPasswordForm();
                }
                else
                {
                    ShowAlert("Mật khẩu hiện tại không chính xác!", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Lỗi hệ thống: " + ex.Message, "alert-danger");
            }
        }

        // Xử lý lưu cấu hình chung (giả lập lưu trữ session hoặc thông báo thành công)
        protected void btnLuuCauHinh_Click(object sender, EventArgs e)
        {
            // Bạn có thể lưu vào CSDL hoặc file cấu hình tùy ý, ở đây thông báo thành công giao diện
            ShowAlert("Đã cập nhật thông tin cấu hình hệ thống thành công!", "alert-success");
        }

        // Hàm tiện ích hiển thị thông báo alert Bootstrap
        private void ShowAlert(string message, string cssClass)
        {
            pnlAlert.Visible = true;
            pnlAlert.CssClass = "alert alert-dismissible fade show shadow-sm mb-4 " + cssClass;
            lblAlertMessage.Text = message;
        }

        private void ClearPasswordForm()
        {
            txtMatKhauCu.Text = "";
            txtMatKhauMoi.Text = "";
            txtXacNhanMatKhau.Text = "";
        }
    }
}