using Ql_tiembanhngot.ketnoi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ql_tiembanhngot.aspx
{
	public partial class QuanLyNhanVien : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridNhanVien();
            }
        }
        private void LoadGridNhanVien()
        {
            string sql = "SELECT MaNV, HoTen, ChucVu, SoDienThoai FROM NhanVien";
            DataTable dt = Database.GetData(sql);
            gvNhanVien.DataSource = dt;
            gvNhanVien.DataBind();
        }

        // 2. Xử lý Thêm mới hoặc Cập nhật thông tin nhân viên
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string chucVu = txtChucVu.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(hoTen))
            {
                ShowMessage("Vui lòng điền đầy đủ Mã nhân viên và Họ tên!", "alert-danger");
                return;
            }

            try
            {
                if (!txtMaNV.ReadOnly)
                {
                    // Thêm mới (INSERT)
                    string sqlInsert = @"INSERT INTO NhanVien (MaNV, HoTen, ChucVu, SoDienThoai) 
                                         VALUES (@MaNV, @HoTen, @ChucVu, @SoDienThoai)";

                    SqlParameter[] pInsert = {
                        new SqlParameter("@MaNV", maNV),
                        new SqlParameter("@HoTen", hoTen),
                        new SqlParameter("@ChucVu", (object)chucVu ?? DBNull.Value),
                        new SqlParameter("@SoDienThoai", (object)soDienThoai ?? DBNull.Value)
                    };

                    int result = Database.ExecuteNonQuery(sqlInsert, pInsert);
                    if (result > 0)
                    {
                        ShowMessage("Thêm nhân viên mới thành công!", "alert-success");
                        ResetForm();
                        LoadGridNhanVien();
                    }
                }
                else
                {
                    // Cập nhật (UPDATE)
                    string sqlUpdate = @"UPDATE NhanVien 
                                         SET HoTen = @HoTen, ChucVu = @ChucVu, SoDienThoai = @SoDienThoai 
                                         WHERE MaNV = @MaNV";

                    SqlParameter[] pUpdate = {
                        new SqlParameter("@HoTen", hoTen),
                        new SqlParameter("@ChucVu", (object)chucVu ?? DBNull.Value),
                        new SqlParameter("@SoDienThoai", (object)soDienThoai ?? DBNull.Value),
                        new SqlParameter("@MaNV", maNV)
                    };

                    int result = Database.ExecuteNonQuery(sqlUpdate, pUpdate);
                    if (result > 0)
                    {
                        ShowMessage("Cập nhật thông tin nhân viên thành công!", "alert-success");
                        ResetForm();
                        LoadGridNhanVien();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi xử lý cơ sở dữ liệu: " + ex.Message, "alert-danger");
            }
        }

        // 3. Xử lý thao tác Sửa / Xóa trên GridView
        protected void gvNhanVien_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string maNV = e.CommandArgument.ToString();

            if (e.CommandName == "EditNV")
            {
                string sql = "SELECT * FROM NhanVien WHERE MaNV = @MaNV";
                SqlParameter[] p = { new SqlParameter("@MaNV", maNV) };
                DataTable dt = Database.GetData(sql, p);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtMaNV.Text = row["MaNV"].ToString();
                    txtMaNV.ReadOnly = true; // Khóa mã nhân viên không cho sửa khi đang cập nhật

                    txtHoTen.Text = row["HoTen"].ToString();
                    txtChucVu.Text = row["ChucVu"] != DBNull.Value ? row["ChucVu"].ToString() : "";
                    txtSoDienThoai.Text = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : "";

                    lblFormTitle.Text = "Cập Nhật Nhân Viên";
                    btnSave.Text = "Cập Nhật";
                    btnSave.CssClass = "btn btn-primary fw-semibold";
                }
            }
            else if (e.CommandName == "DeleteNV")
            {
                try
                {
                    string sqlDelete = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                    SqlParameter[] pDelete = { new SqlParameter("@MaNV", maNV) };

                    int result = Database.ExecuteNonQuery(sqlDelete, pDelete);
                    if (result > 0)
                    {
                        ShowMessage("Xóa nhân viên thành công!", "alert-success");
                        ResetForm();
                        LoadGridNhanVien();
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Không thể xóa nhân viên này do có ràng buộc dữ liệu hóa đơn: " + ex.Message, "alert-danger");
                }
            }
        }

        // 4. Nút Hủy / Làm mới form
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            txtMaNV.Text = "";
            txtMaNV.ReadOnly = false;
            txtHoTen.Text = "";
            txtChucVu.Text = "";
            txtSoDienThoai.Text = "";

            lblFormTitle.Text = "Thêm Nhân Viên Mới";
            btnSave.Text = "Thêm Mới";
            btnSave.CssClass = "btn btn-warning fw-semibold text-dark";
        }

        private void ShowMessage(string message, string cssClass)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = "alert " + cssClass + " alert-dismissible fade show";
            lblMessage.Text = message;
        }
    }
}
