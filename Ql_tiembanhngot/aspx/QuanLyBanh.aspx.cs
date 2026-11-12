using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ql_tiembanhngot
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath).ToLower();

            // Kiểm tra trạng thái đăng nhập
            if (Session["TenDangNhap"] == null)
            {
                if (!currentPage.Equals("default.aspx") && !Request.AppRelativeCurrentExecutionFilePath.Equals("~/Default.aspx", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
            }
            else
            {
                if (currentPage.Equals("default.aspx") || Request.AppRelativeCurrentExecutionFilePath.Equals("~/Default.aspx", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("~/aspx/ThongKe.aspx");
                    return;
                }

                // Gán thông tin người dùng và phân quyền
                if (lblUserCurrent != null && Session["TenDangNhap"] != null)
                {
                    lblUserCurrent.Text = Session["TenDangNhap"].ToString();
                }

                if (lblQuyenCurrent != null && Session["Quyen"] != null)
                {
                    string quyen = Session["Quyen"].ToString();
                    lblQuyenCurrent.Text = quyen;
                    PhanQuyenSidebar(quyen);
                }
            }
        }

        // Hàm phân quyền hiển thị các menu trên Sidebar
        private void PhanQuyenSidebar(string quyen)
        {
            string q = quyen.ToLower();

            if (q.Contains("quản lý") || q.Contains("admin"))
            {
                if (menuThongKe != null) menuThongKe.Visible = true;
                if (menuHoaDon != null) menuHoaDon.Visible = true;
                if (menuDonDat != null) menuDonDat.Visible = true;
                if (menuBanh != null) menuBanh.Visible = true;
                if (menuLoaiBanh != null) menuLoaiBanh.Visible = true;
                if (menuNguyenLieu != null) menuNguyenLieu.Visible = true;
                if (menuNhanVien != null) menuNhanVien.Visible = true;
                if (menuCaiDat != null) menuCaiDat.Visible = true;
            }
            else if (q.Contains("thu ngân"))
            {
                if (menuThongKe != null) menuThongKe.Visible = false;
                if (menuHoaDon != null) menuHoaDon.Visible = true;
                if (menuDonDat != null) menuDonDat.Visible = true;
                if (menuBanh != null) menuBanh.Visible = true;
                if (menuLoaiBanh != null) menuLoaiBanh.Visible = false;
                if (menuNguyenLieu != null) menuNguyenLieu.Visible = false;
                if (menuNhanVien != null) menuNhanVien.Visible = false;
                if (menuCaiDat != null) menuCaiDat.Visible = false;
            }
            else
            {
                if (menuThongKe != null) menuThongKe.Visible = false;
                if (menuHoaDon != null) menuHoaDon.Visible = false;
                if (menuDonDat != null) menuDonDat.Visible = true;
                if (menuBanh != null) menuBanh.Visible = true;
                if (menuLoaiBanh != null) menuLoaiBanh.Visible = true;
                if (menuNguyenLieu != null) menuNguyenLieu.Visible = true;
                if (menuNhanVien != null) menuNhanVien.Visible = false;
                if (menuCaiDat != null) menuCaiDat.Visible = false;
            }
        }

        // Xử lý đăng xuất
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}
