using N5_Qltiembanh.ketnoi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class aspx_QuanLyDonDat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGridDonDat();
        }
    }
    private void LoadGridDonDat()
    {
        string sql = "SELECT MaDon, TenKhachHang, SoDienThoai, NgayNhan, TongTien, TrangThai FROM DonDatBanh";
        DataTable dt = Database.GetData(sql);
        gvDonDat.DataSource = dt;
        gvDonDat.DataBind();
    }

    // 2. Xử lý Thêm mới hoặc Cập nhật đơn đặt bánh
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string maDon = txtMaDon.Text.Trim();
        string tenKH = txtTenKhachHang.Text.Trim();
        string sdt = txtSoDienThoai.Text.Trim();
        string ngayNhanStr = txtNgayNhan.Text.Trim();
        string trangThai = ddlTrangThai.SelectedValue;

        if (string.IsNullOrEmpty(maDon) || string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(txtTongTien.Text))
        {
            ShowMessage("Vui lòng nhập đầy đủ Mã đơn, Tên khách hàng và Tổng tiền!", "alert-danger");
            return;
        }

        if (!decimal.TryParse(txtTongTien.Text.Trim(), out decimal tongTien))
        {
            ShowMessage("Tổng tiền phải là số hợp lệ!", "alert-danger");
            return;
        }

        DateTime? ngayNhan = null;
        if (DateTime.TryParse(ngayNhanStr, out DateTime parsedDate))
        {
            ngayNhan = parsedDate;
        }

        try
        {
            if (!txtMaDon.ReadOnly)
            {
                // Lệnh Thêm mới (INSERT)
                string sqlInsert = @"INSERT INTO DonDatBanh (MaDon, TenKhachHang, SoDienThoai, NgayNhan, TongTien, TrangThai) 
                                         VALUES (@MaDon, @TenKhachHang, @SoDienThoai, @NgayNhan, @TongTien, @TrangThai)";

                SqlParameter[] pInsert = {
                        new SqlParameter("@MaDon", maDon),
                        new SqlParameter("@TenKhachHang", tenKH),
                        new SqlParameter("@SoDienThoai", (object)sdt ?? DBNull.Value),
                        new SqlParameter("@NgayNhan", (object)ngayNhan ?? DBNull.Value),
                        new SqlParameter("@TongTien", tongTien),
                        new SqlParameter("@TrangThai", trangThai)
                    };

                int result = Database.ExecuteNonQuery(sqlInsert, pInsert);
                if (result > 0)
                {
                    ShowMessage("Thêm đơn đặt bánh thành công!", "alert-success");
                    ResetForm();
                    LoadGridDonDat();
                }
            }
            else
            {
                // Lệnh Cập nhật (UPDATE)
                string sqlUpdate = @"UPDATE DonDatBanh 
                                         SET TenKhachHang = @TenKhachHang, SoDienThoai = @SoDienThoai, 
                                             NgayNhan = @NgayNhan, TongTien = @TongTien, TrangThai = @TrangThai 
                                         WHERE MaDon = @MaDon";

                SqlParameter[] pUpdate = {
                        new SqlParameter("@TenKhachHang", tenKH),
                        new SqlParameter("@SoDienThoai", (object)sdt ?? DBNull.Value),
                        new SqlParameter("@NgayNhan", (object)ngayNhan ?? DBNull.Value),
                        new SqlParameter("@TongTien", tongTien),
                        new SqlParameter("@TrangThai", trangThai),
                        new SqlParameter("@MaDon", maDon)
                    };

                int result = Database.ExecuteNonQuery(sqlUpdate, pUpdate);
                if (result > 0)
                {
                    ShowMessage("Cập nhật đơn đặt bánh thành công!", "alert-success");
                    ResetForm();
                    LoadGridDonDat();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Lỗi xử lý cơ sở dữ liệu: " + ex.Message, "alert-danger");
        }
    }

    // 3. Xử lý thao tác Sửa / Xóa trên GridView
    protected void gvDonDat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string maDon = e.CommandArgument.ToString();

        if (e.CommandName == "EditDon")
        {
            string sql = "SELECT * FROM DonDatBanh WHERE MaDon = @MaDon";
            SqlParameter[] p = { new SqlParameter("@MaDon", maDon) };
            DataTable dt = Database.GetData(sql, p);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMaDon.Text = row["MaDon"].ToString();
                txtMaDon.ReadOnly = true; // Khóa mã đơn không cho chỉnh sửa

                txtTenKhachHang.Text = row["TenKhachHang"].ToString();
                txtSoDienThoai.Text = row["SoDienThoai"].ToString();

                if (row["NgayNhan"] != DBNull.Value)
                {
                    DateTime ngayNhan = Convert.ToDateTime(row["NgayNhan"]);
                    txtNgayNhan.Text = ngayNhan.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtNgayNhan.Text = "";
                }

                txtTongTien.Text = Convert.ToDecimal(row["TongTien"]).ToString("F0");
                ddlTrangThai.SelectedValue = row["TrangThai"].ToString();

                lblFormTitle.Text = "Cập Nhật Đơn Đặt Bánh";
                btnSave.Text = "Cập Nhật";
                btnSave.CssClass = "btn btn-primary fw-semibold";
            }
        }
        else if (e.CommandName == "DeleteDon")
        {
            try
            {
                string sqlDelete = "DELETE FROM DonDatBanh WHERE MaDon = @MaDon";
                SqlParameter[] pDelete = { new SqlParameter("@MaDon", maDon) };

                int result = Database.ExecuteNonQuery(sqlDelete, pDelete);
                if (result > 0)
                {
                    ShowMessage("Xóa đơn đặt bánh thành công!", "alert-success");
                    ResetForm();
                    LoadGridDonDat();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Không thể xóa đơn này do có ràng buộc dữ liệu: " + ex.Message, "alert-danger");
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
        txtMaDon.Text = "";
        txtMaDon.ReadOnly = false;
        txtTenKhachHang.Text = "";
        txtSoDienThoai.Text = "";
        txtNgayNhan.Text = "";
        txtTongTien.Text = "";
        ddlTrangThai.SelectedIndex = 0;

        lblFormTitle.Text = "Thêm Đơn Đặt Mới";
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