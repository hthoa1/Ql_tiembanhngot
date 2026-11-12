using N5_Qltiembanh.ketnoi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class aspx_QuanLyHoaDon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadNhanVienDropdown();
            LoadGridHoaDon();
            txtNgayLap.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    private void LoadNhanVienDropdown()
    {
        string sql = "SELECT MaNV, HoTen FROM NhanVien";
        DataTable dt = Database.GetData(sql);

        ddlNhanVien.DataSource = dt;
        ddlNhanVien.DataTextField = "HoTen";
        ddlNhanVien.DataValueField = "MaNV";
        ddlNhanVien.DataBind();

        ddlNhanVien.Items.Insert(0, new ListItem("-- Chọn nhân viên lập --", ""));
    }

    // 2. Tải danh sách Hóa đơn lên GridView (INNER JOIN với NhanVien)
    private void LoadGridHoaDon()
    {
        string sql = @"SELECT h.MaHD, n.HoTen, h.NgayLap, h.TongTien, h.TrangThai 
                         FROM HoaDon h 
                         INNER JOIN NhanVien n ON h.MaNV = n.MaNV 
                         ORDER BY h.NgayLap DESC";

        DataTable dt = Database.GetData(sql);
        gvHoaDon.DataSource = dt;
        gvHoaDon.DataBind();
    }

    // 3. Xử lý Thêm mới hoặc Cập nhật hóa đơn
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string maHD = txtMaHD.Text.Trim();
        string maNV = ddlNhanVien.SelectedValue;
        string ngayLapStr = txtNgayLap.Text.Trim();
        string trangThai = ddlTrangThai.SelectedValue;

        if (string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(txtTongTien.Text))
        {
            ShowMessage("Vui lòng nhập đầy đủ Mã hóa đơn, Chọn nhân viên và Tổng tiền!", "alert-danger");
            return;
        }

        if (!decimal.TryParse(txtTongTien.Text.Trim(), out decimal tongTien))
        {
            ShowMessage("Tổng tiền phải là số hợp lệ!", "alert-danger");
            return;
        }

        DateTime? ngayLap = null;
        if (DateTime.TryParse(ngayLapStr, out DateTime parsedDate))
        {
            ngayLap = parsedDate;
        }

        try
        {
            if (!txtMaHD.ReadOnly)
            {
                // Lệnh Thêm mới (INSERT)
                string sqlInsert = @"INSERT INTO HoaDon (MaHD, MaNV, NgayLap, TongTien, TrangThai) 
                                         VALUES (@MaHD, @MaNV, @NgayLap, @TongTien, @TrangThai)";

                SqlParameter[] pInsert = {
                        new SqlParameter("@MaHD", maHD),
                        new SqlParameter("@MaNV", maNV),
                        new SqlParameter("@NgayLap", (object)ngayLap ?? DBNull.Value),
                        new SqlParameter("@TongTien", tongTien),
                        new SqlParameter("@TrangThai", trangThai)
                    };

                int result = Database.ExecuteNonQuery(sqlInsert, pInsert);
                if (result > 0)
                {
                    ShowMessage("Lập hóa đơn mới thành công!", "alert-success");
                    ResetForm();
                    LoadGridHoaDon();
                }
            }
            else
            {
                // Lệnh Cập nhật (UPDATE)
                string sqlUpdate = @"UPDATE HoaDon 
                                         SET MaNV = @MaNV, NgayLap = @NgayLap, TongTien = @TongTien, TrangThai = @TrangThai 
                                         WHERE MaHD = @MaHD";

                SqlParameter[] pUpdate = {
                        new SqlParameter("@MaNV", maNV),
                        new SqlParameter("@NgayLap", (object)ngayLap ?? DBNull.Value),
                        new SqlParameter("@TongTien", tongTien),
                        new SqlParameter("@TrangThai", trangThai),
                        new SqlParameter("@MaHD", maHD)
                    };

                int result = Database.ExecuteNonQuery(sqlUpdate, pUpdate);
                if (result > 0)
                {
                    ShowMessage("Cập nhật thông tin hóa đơn thành công!", "alert-success");
                    ResetForm();
                    LoadGridHoaDon();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Lỗi xử lý cơ sở dữ liệu: " + ex.Message, "alert-danger");
        }
    }

    // 4. Xử lý thao tác Sửa / Xóa trên GridView
    protected void gvHoaDon_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string maHD = e.CommandArgument.ToString();

        if (e.CommandName == "EditHD")
        {
            string sql = "SELECT * FROM HoaDon WHERE MaHD = @MaHD";
            SqlParameter[] p = { new SqlParameter("@MaHD", maHD) };
            DataTable dt = Database.GetData(sql, p);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMaHD.Text = row["MaHD"].ToString();
                txtMaHD.ReadOnly = true; // Khóa mã hóa đơn không cho sửa

                ddlNhanVien.SelectedValue = row["MaNV"].ToString();

                if (row["NgayLap"] != DBNull.Value)
                {
                    DateTime dtNgay = Convert.ToDateTime(row["NgayLap"]);
                    txtNgayLap.Text = dtNgay.ToString("yyyy-MM-dd");
                }

                txtTongTien.Text = Convert.ToDecimal(row["TongTien"]).ToString("F0");
                ddlTrangThai.SelectedValue = row["TrangThai"].ToString();

                lblFormTitle.Text = "Cập Nhật Hóa Đơn";
                btnSave.Text = "Cập Nhật";
                btnSave.CssClass = "btn btn-primary fw-semibold";
            }
        }
        else if (e.CommandName == "DeleteHD")
        {
            try
            {
                string sqlDelete = "DELETE FROM HoaDon WHERE MaHD = @MaHD";
                SqlParameter[] pDelete = { new SqlParameter("@MaHD", maHD) };

                int result = Database.ExecuteNonQuery(sqlDelete, pDelete);
                if (result > 0)
                {
                    ShowMessage("Xóa hóa đơn thành công!", "alert-success");
                    ResetForm();
                    LoadGridHoaDon();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Không thể xóa hóa đơn này do có ràng buộc dữ liệu chi tiết hóa đơn: " + ex.Message, "alert-danger");
            }
        }
    }

    // 5. Nút Hủy / Làm mới form
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetForm();
    }

    private void ResetForm()
    {
        txtMaHD.Text = "";
        txtMaHD.ReadOnly = false;
        ddlNhanVien.SelectedIndex = 0;
        txtNgayLap.Text = DateTime.Now.ToString("yyyy-MM-dd");
        txtTongTien.Text = "";
        ddlTrangThai.SelectedIndex = 0;

        lblFormTitle.Text = "Lập Hóa Đơn Mới";
        btnSave.Text = "Lưu Hóa Đơn";
        btnSave.CssClass = "btn btn-warning fw-semibold text-dark";
    }

    private void ShowMessage(string message, string cssClass)
    {
        pnlMessage.Visible = true;
        pnlMessage.CssClass = "alert " + cssClass + " alert-dismissible fade show";
        lblMessage.Text = message;
    }
}