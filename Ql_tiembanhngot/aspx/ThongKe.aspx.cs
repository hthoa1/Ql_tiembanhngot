using Ql_tiembanhngot.ketnoi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ql_tiembanhngot.aspx
{
    public partial class ThongKe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadSoLieuThongKe();
                LoadHoaDonGanDay();
            }
        }
        private void LoadSoLieuThongKe()
        {
            try
            {
                // 1. Tổng doanh thu
                DataTable dtDoanhThu = Database.GetData("SELECT SUM(TongTien) AS Total FROM HoaDon");
                if (dtDoanhThu != null && dtDoanhThu.Rows.Count > 0 && dtDoanhThu.Rows[0]["Total"] != DBNull.Value)
                {
                    decimal tongTien = Convert.ToDecimal(dtDoanhThu.Rows[0]["Total"]);
                    lblTongDoanhThu.Text = string.Format("{0:N0} đ", tongTien);
                }
                else
                {
                    lblTongDoanhThu.Text = "0 đ";
                }

                // 2. Tổng số hóa đơn
                DataTable dtHD = Database.GetData("SELECT COUNT(*) AS CountHD FROM HoaDon");
                if (dtHD != null && dtHD.Rows.Count > 0)
                {
                    lblTongHoaDon.Text = dtHD.Rows[0]["CountHD"].ToString();
                }

                // 3. Tổng sản phẩm bánh
                DataTable dtBanh = Database.GetData("SELECT COUNT(*) AS CountBanh FROM Banh");
                if (dtBanh != null && dtBanh.Rows.Count > 0)
                {
                    lblTongBanh.Text = dtBanh.Rows[0]["CountBanh"].ToString();
                }

                // 4. Tổng nguyên liệu kho
                DataTable dtNL = Database.GetData("SELECT COUNT(*) AS CountNL FROM NguyenLieu");
                if (dtNL != null && dtNL.Rows.Count > 0)
                {
                    lblTongNguyenLieu.Text = dtNL.Rows[0]["CountNL"].ToString();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần thiết
                System.Diagnostics.Debug.WriteLine("Lỗi tải thống kê: " + ex.Message);
            }
        }

        // Tải danh sách 10 hóa đơn mới nhất vào GridView
        private void LoadHoaDonGanDay()
        {
            string sql = @"SELECT TOP 10 hd.MaHD, nv.HoTen, hd.NgayLap, hd.TongTien, hd.PhuongThucTT, hd.TrangThai 
                           FROM HoaDon hd 
                           LEFT JOIN NhanVien nv ON hd.MaNV = nv.MaNV 
                           ORDER BY hd.NgayLap DESC";
            DataTable dt = Database.GetData(sql);
            gvHoaDon.DataSource = dt;
            gvHoaDon.DataBind();
        }
    }
}
