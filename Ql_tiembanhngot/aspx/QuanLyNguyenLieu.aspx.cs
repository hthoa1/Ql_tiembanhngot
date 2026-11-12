using N5_Qltiembanh.ketnoi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class aspx_QuanLyNguyenLieu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGridNguyenLieu();
        }
    }
    private void LoadGridNguyenLieu()
    {
        string sql = "SELECT MaNL, TenNL, SoLuongTon, DonViTinh, NhaCC, NguongCanhBao FROM NguyenLieu";
        DataTable dt = Database.GetData(sql);
        gvNguyenLieu.DataSource = dt;
        gvNguyenLieu.DataBind();
    }

    // 2. Xử lý Thêm mới hoặc Cập nhật nguyên liệu
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string maNL = txtMaNL.Text.Trim();
        string tenNL = txtTenNL.Text.Trim();
        string donViTinh = txtDonViTinh.Text.Trim();
        string nhaCC = txtNhaCC.Text.Trim();

        if (string.IsNullOrEmpty(maNL) || string.IsNullOrEmpty(tenNL) || string.IsNullOrEmpty(txtSoLuongTon.Text) || string.IsNullOrEmpty(txtNguongCanhBao.Text))
        {
            ShowMessage("Vui lòng điền đầy đủ các thông tin bắt buộc!", "alert-danger");
            return;
        }

        if (!decimal.TryParse(txtSoLuongTon.Text.Trim(), out decimal soLuongTon))
        {
            ShowMessage("Số lượng tồn phải là một số hợp lệ!", "alert-danger");
            return;
        }

        if (!decimal.TryParse(txtNguongCanhBao.Text.Trim(), out decimal nguongCanhBao))
        {
            ShowMessage("Ngưỡng cảnh báo phải là một số hợp lệ!", "alert-danger");
            return;
        }

        try
        {
            if (!txtMaNL.ReadOnly)
            {
                // Thêm mới (INSERT kèm NhaCC)
                string sqlInsert = @"INSERT INTO NguyenLieu (MaNL, TenNL, SoLuongTon, DonViTinh, NhaCC, NguongCanhBao) 
                                         VALUES (@MaNL, @TenNL, @SoLuongTon, @DonViTinh, @NhaCC, @NguongCanhBao)";

                SqlParameter[] pInsert = {
                        new SqlParameter("@MaNL", maNL),
                        new SqlParameter("@TenNL", tenNL),
                        new SqlParameter("@SoLuongTon", soLuongTon),
                        new SqlParameter("@DonViTinh", (object)donViTinh ?? DBNull.Value),
                        new SqlParameter("@NhaCC", (object)nhaCC ?? DBNull.Value),
                        new SqlParameter("@NguongCanhBao", nguongCanhBao)
                    };

                int result = Database.ExecuteNonQuery(sqlInsert, pInsert);
                if (result > 0)
                {
                    ShowMessage("Thêm nguyên liệu mới thành công!", "alert-success");
                    ResetForm();
                    LoadGridNguyenLieu();
                }
            }
            else
            {
                // Cập nhật (UPDATE kèm NhaCC)
                string sqlUpdate = @"UPDATE NguyenLieu 
                                         SET TenNL = @TenNL, SoLuongTon = @SoLuongTon, 
                                             DonViTinh = @DonViTinh, NhaCC = @NhaCC, NguongCanhBao = @NguongCanhBao 
                                         WHERE MaNL = @MaNL";

                SqlParameter[] pUpdate = {
                        new SqlParameter("@TenNL", tenNL),
                        new SqlParameter("@SoLuongTon", soLuongTon),
                        new SqlParameter("@DonViTinh", (object)donViTinh ?? DBNull.Value),
                        new SqlParameter("@NhaCC", (object)nhaCC ?? DBNull.Value),
                        new SqlParameter("@NguongCanhBao", nguongCanhBao),
                        new SqlParameter("@MaNL", maNL)
                    };

                int result = Database.ExecuteNonQuery(sqlUpdate, pUpdate);
                if (result > 0)
                {
                    ShowMessage("Cập nhật thông tin nguyên liệu thành công!", "alert-success");
                    ResetForm();
                    LoadGridNguyenLieu();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Lỗi xử lý cơ sở dữ liệu: " + ex.Message, "alert-danger");
        }
    }

    // 3. Xử lý thao tác Sửa / Xóa trên GridView
    protected void gvNguyenLieu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string maNL = e.CommandArgument.ToString();

        if (e.CommandName == "EditNL")
        {
            string sql = "SELECT * FROM NguyenLieu WHERE MaNL = @MaNL";
            SqlParameter[] p = { new SqlParameter("@MaNL", maNL) };
            DataTable dt = Database.GetData(sql, p);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMaNL.Text = row["MaNL"].ToString();
                txtMaNL.ReadOnly = true; // Khóa mã nguyên liệu không cho sửa

                txtTenNL.Text = row["TenNL"].ToString();
                txtSoLuongTon.Text = Convert.ToDecimal(row["SoLuongTon"]).ToString("F0");
                txtDonViTinh.Text = row["DonViTinh"].ToString();
                txtNhaCC.Text = row["NhaCC"] != DBNull.Value ? row["NhaCC"].ToString() : "";
                txtNguongCanhBao.Text = Convert.ToDecimal(row["NguongCanhBao"]).ToString("F0");

                lblFormTitle.Text = "Cập Nhật Nguyên Liệu";
                btnSave.Text = "Cập Nhật";
                btnSave.CssClass = "btn btn-primary fw-semibold";
            }
        }
        else if (e.CommandName == "DeleteNL")
        {
            try
            {
                string sqlDelete = "DELETE FROM NguyenLieu WHERE MaNL = @MaNL";
                SqlParameter[] pDelete = { new SqlParameter("@MaNL", maNL) };

                int result = Database.ExecuteNonQuery(sqlDelete, pDelete);
                if (result > 0)
                {
                    ShowMessage("Xóa nguyên liệu thành công!", "alert-success");
                    ResetForm();
                    LoadGridNguyenLieu();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Không thể xóa nguyên liệu này do có ràng buộc dữ liệu: " + ex.Message, "alert-danger");
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
        txtMaNL.Text = "";
        txtMaNL.ReadOnly = false;
        txtTenNL.Text = "";
        txtSoLuongTon.Text = "";
        txtDonViTinh.Text = "";
        txtNhaCC.Text = "";
        txtNguongCanhBao.Text = "";

        lblFormTitle.Text = "Thêm Nguyên Liệu Mới";
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