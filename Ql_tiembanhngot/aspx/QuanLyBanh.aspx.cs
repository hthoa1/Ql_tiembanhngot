using N5_Qltiembanh.ketnoi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class aspx_QuanLyBanh : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadLoaiBanhDropdown();
            LoadGridBanh();
        }
    }
        private void LoadLoaiBanhDropdown()
    {
        string sql = "SELECT MaLoai, TenLoai FROM LoaiBanh";
        DataTable dt = Database.GetData(sql);

        ddlLoaiBanh.DataSource = dt;
        ddlLoaiBanh.DataTextField = "TenLoai";
        ddlLoaiBanh.DataValueField = "MaLoai";
        ddlLoaiBanh.DataBind();

        ddlLoaiBanh.Items.Insert(0, new ListItem("-- Chọn loại bánh --", ""));
    }

    // 2. Tải danh sách bánh lên GridView (INNER JOIN với LoaiBanh)
    private void LoadGridBanh()
    {
        string sql = @"SELECT b.MaBanh, b.TenBanh, l.TenLoai, b.GiaBan, b.DonViTinh, b.HinhAnh 
                           FROM Banh b 
                           INNER JOIN LoaiBanh l ON b.MaLoai = l.MaLoai";

        DataTable dt = Database.GetData(sql);
        gvBanh.DataSource = dt;
        gvBanh.DataBind();
    }

    // 3. Xử lý lưu (Thêm mới hoặc Cập nhật)
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string maBanh = txtMaBanh.Text.Trim();
        string tenBanh = txtTenBanh.Text.Trim();
        string maLoai = ddlLoaiBanh.SelectedValue;
        string donViTinh = txtDonViTinh.Text.Trim();
        string hinhAnh = txtHinhAnh.Text.Trim();

        if (string.IsNullOrEmpty(maBanh) || string.IsNullOrEmpty(tenBanh) || string.IsNullOrEmpty(maLoai) || string.IsNullOrEmpty(txtGiaBan.Text))
        {
            ShowMessage("Vui lòng nhập đầy đủ các thông tin bắt buộc!", "alert-danger");
            return;
        }

        if (!decimal.TryParse(txtGiaBan.Text.Trim(), out decimal giaBan))
        {
            ShowMessage("Giá bán phải là số hợp lệ!", "alert-danger");
            return;
        }

        try
        {
            if (!txtMaBanh.ReadOnly)
            {
                // Thêm mới
                string sqlInsert = @"INSERT INTO Banh (MaBanh, TenBanh, MaLoai, GiaBan, DonViTinh, HinhAnh) 
                                         VALUES (@MaBanh, @TenBanh, @MaLoai, @GiaBan, @DonViTinh, @HinhAnh)";

                SqlParameter[] pInsert = {
                        new SqlParameter("@MaBanh", maBanh),
                        new SqlParameter("@TenBanh", tenBanh),
                        new SqlParameter("@MaLoai", maLoai),
                        new SqlParameter("@GiaBan", giaBan),
                        new SqlParameter("@DonViTinh", (object)donViTinh ?? DBNull.Value),
                        new SqlParameter("@HinhAnh", (object)hinhAnh ?? DBNull.Value)
                    };

                int result = Database.ExecuteNonQuery(sqlInsert, pInsert);
                if (result > 0)
                {
                    ShowMessage("Thêm mới bánh thành công!", "alert-success");
                    ResetForm();
                    LoadGridBanh();
                }
            }
            else
            {
                // Cập nhật
                string sqlUpdate = @"UPDATE Banh 
                                         SET TenBanh = @TenBanh, MaLoai = @MaLoai, GiaBan = @GiaBan, DonViTinh = @DonViTinh, HinhAnh = @HinhAnh 
                                         WHERE MaBanh = @MaBanh";

                SqlParameter[] pUpdate = {
                        new SqlParameter("@TenBanh", tenBanh),
                        new SqlParameter("@MaLoai", maLoai),
                        new SqlParameter("@GiaBan", giaBan),
                        new SqlParameter("@DonViTinh", (object)donViTinh ?? DBNull.Value),
                        new SqlParameter("@HinhAnh", (object)hinhAnh ?? DBNull.Value),
                        new SqlParameter("@MaBanh", maBanh)
                    };

                int result = Database.ExecuteNonQuery(sqlUpdate, pUpdate);
                if (result > 0)
                {
                    ShowMessage("Cập nhật thông tin bánh thành công!", "alert-success");
                    ResetForm();
                    LoadGridBanh();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Lỗi xử lý: " + ex.Message, "alert-danger");
        }
    }

    // 4. Xử lý sự kiện Sửa / Xóa trên GridView
    protected void gvBanh_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string maBanh = e.CommandArgument.ToString();

        if (e.CommandName == "EditBanh")
        {
            string sql = "SELECT * FROM Banh WHERE MaBanh = @MaBanh";
            SqlParameter[] p = { new SqlParameter("@MaBanh", maBanh) };
            DataTable dt = Database.GetData(sql, p);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMaBanh.Text = row["MaBanh"].ToString();
                txtMaBanh.ReadOnly = true;

                txtTenBanh.Text = row["TenBanh"].ToString();
                ddlLoaiBanh.SelectedValue = row["MaLoai"].ToString();
                txtGiaBan.Text = Convert.ToDecimal(row["GiaBan"]).ToString("F0");
                txtDonViTinh.Text = row["DonViTinh"].ToString();
                txtHinhAnh.Text = row["HinhAnh"].ToString();

                lblFormTitle.Text = "Cập Nhật Thông Tin Bánh";
                btnSave.Text = "Cập Nhật";
                btnSave.CssClass = "btn btn-primary fw-semibold";
            }
        }
        else if (e.CommandName == "DeleteBanh")
        {
            try
            {
                string sqlDelete = "DELETE FROM Banh WHERE MaBanh = @MaBanh";
                SqlParameter[] pDelete = { new SqlParameter("@MaBanh", maBanh) };

                int result = Database.ExecuteNonQuery(sqlDelete, pDelete);
                if (result > 0)
                {
                    ShowMessage("Xóa sản phẩm thành công!", "alert-success");
                    ResetForm();
                    LoadGridBanh();
                }
            }
            catch (Exception)
            {
                ShowMessage("Không thể xóa sản phẩm này do đã có ràng buộc dữ liệu trong Hóa đơn hoặc Chi tiết hóa đơn!", "alert-danger");
            }
        }
    }

    // 5. Nút Hủy / Làm mới
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetForm();
    }

    private void ResetForm()
    {
        txtMaBanh.Text = "";
        txtMaBanh.ReadOnly = false;
        txtTenBanh.Text = "";
        ddlLoaiBanh.SelectedIndex = 0;
        txtGiaBan.Text = "";
        txtDonViTinh.Text = "";
        txtHinhAnh.Text = "";

        lblFormTitle.Text = "Thêm Bánh Mới";
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

