using N5_Qltiembanh.ketnoi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class aspx_QuanLyLoaiBanh : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGridLoaiBanh();
        }
    }
    private void LoadGridLoaiBanh()
    {
        string sql = "SELECT MaLoai, TenLoai, MoTa FROM LoaiBanh";
        DataTable dt = Database.GetData(sql);
        gvLoaiBanh.DataSource = dt;
        gvLoaiBanh.DataBind();
    }

    // 2. Xử lý Thêm mới hoặc Cập nhật
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string maLoai = txtMaLoai.Text.Trim();
        string tenLoai = txtTenLoai.Text.Trim();
        string moTa = txtMoTa.Text.Trim();

        if (string.IsNullOrEmpty(maLoai) || string.IsNullOrEmpty(tenLoai))
        {
            ShowMessage("Vui lòng nhập đầy đủ Mã loại và Tên loại bánh!", "alert-danger");
            return;
        }

        try
        {
            if (!txtMaLoai.ReadOnly)
            {
                // Lệnh Thêm mới (INSERT)
                string sqlInsert = "INSERT INTO LoaiBanh (MaLoai, TenLoai, MoTa) VALUES (@MaLoai, @TenLoai, @MoTa)";
                SqlParameter[] pInsert = {
                        new SqlParameter("@MaLoai", maLoai),
                        new SqlParameter("@TenLoai", tenLoai),
                        new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value)
                    };

                int result = Database.ExecuteNonQuery(sqlInsert, pInsert);
                if (result > 0)
                {
                    ShowMessage("Thêm mới loại bánh thành công!", "alert-success");
                    ResetForm();
                    LoadGridLoaiBanh();
                }
            }
            else
            {
                // Lệnh Cập nhật (UPDATE)
                string sqlUpdate = "UPDATE LoaiBanh SET TenLoai = @TenLoai, MoTa = @MoTa WHERE MaLoai = @MaLoai";
                SqlParameter[] pUpdate = {
                        new SqlParameter("@TenLoai", tenLoai),
                        new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value),
                        new SqlParameter("@MaLoai", maLoai)
                    };

                int result = Database.ExecuteNonQuery(sqlUpdate, pUpdate);
                if (result > 0)
                {
                    ShowMessage("Cập nhật thông tin loại bánh thành công!", "alert-success");
                    ResetForm();
                    LoadGridLoaiBanh();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Lỗi xử lý: " + ex.Message, "alert-danger");
        }
    }

    // 3. Xử lý sự kiện nút Sửa / Xóa trên GridView
    protected void gvLoaiBanh_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string maLoai = e.CommandArgument.ToString();

        if (e.CommandName == "EditLoai")
        {
            string sql = "SELECT * FROM LoaiBanh WHERE MaLoai = @MaLoai";
            SqlParameter[] p = { new SqlParameter("@MaLoai", maLoai) };
            DataTable dt = Database.GetData(sql, p);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMaLoai.Text = row["MaLoai"].ToString();
                txtMaLoai.ReadOnly = true; // Khóa Mã loại không cho sửa

                txtTenLoai.Text = row["TenLoai"].ToString();
                txtMoTa.Text = row["MoTa"].ToString();

                lblFormTitle.Text = "Cập Nhật Loại Bánh";
                btnSave.Text = "Cập Nhật";
                btnSave.CssClass = "btn btn-primary fw-semibold";
            }
        }
        else if (e.CommandName == "DeleteLoai")
        {
            try
            {
                string sqlDelete = "DELETE FROM LoaiBanh WHERE MaLoai = @MaLoai";
                SqlParameter[] pDelete = { new SqlParameter("@MaLoai", maLoai) };

                int result = Database.ExecuteNonQuery(sqlDelete, pDelete);
                if (result > 0)
                {
                    ShowMessage("Xóa loại bánh thành công!", "alert-success");
                    ResetForm();
                    LoadGridLoaiBanh();
                }
            }
            catch (Exception)
            {
                ShowMessage("Không thể xóa loại bánh này do đang chứa các Sản phẩm Bánh hoặc Công thức liên quan!", "alert-danger");
            }
        }
    }

    // 4. Nút Hủy / Làm mới
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetForm();
    }

    private void ResetForm()
    {
        txtMaLoai.Text = "";
        txtMaLoai.ReadOnly = false;
        txtTenLoai.Text = "";
        txtMoTa.Text = "";

        lblFormTitle.Text = "Thêm Loại Bánh Mới";
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