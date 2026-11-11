<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CaiDat.aspx.cs" Inherits="Ql_tiembanhngot.aspx.CaiDat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mb-4">
        <div class="col-md-12">
            <h2 class="fw-bold text-dark"><i class="fa-solid fa-gear me-2 text-warning"></i>Cài Đặt Hệ Thống</h2>
            <p class="text-muted">Quản lý thông tin cấu hình tiệm bánh và thay đổi mật khẩu bảo mật tài khoản.</p>
            <hr />
        </div>
    </div>

    <!-- Thông báo trạng thái -->
    <asp:Panel ID="pnlAlert" runat="server" Visible="false" CssClass="alert alert-dismissible fade show shadow-sm mb-4" role="alert">
        <asp:Label ID="lblAlertMessage" runat="server"></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="row g-4">
        <!-- Cột 1: Đổi mật khẩu tài khoản -->
        <div class="col-lg-6">
            <div class="card border-0 shadow-sm rounded-4 p-4 bg-white h-100">
                <h5 class="fw-bold text-secondary mb-3"><i class="fa-solid fa-key me-2 text-warning"></i>Đổi Mật Khẩu Tài Khoản</h5>
                <hr class="text-muted opacity-25" />
                
                <div class="mb-3">
                    <label class="form-label fw-semibold">Mật khẩu hiện tại</label>
                    <asp:TextBox ID="txtMatKhauCu" runat="server" TextMode="Password" class="form-control" placeholder="Nhập mật khẩu cũ"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Mật khẩu mới</label>
                    <asp:TextBox ID="txtMatKhauMoi" runat="server" TextMode="Password" class="form-control" placeholder="Nhập mật khẩu mới"></asp:TextBox>
                </div>

                <div class="mb-4">
                    <label class="form-label fw-semibold">Xác nhận mật khẩu mới</label>
                    <asp:TextBox ID="txtXacNhanMatKhau" runat="server" TextMode="Password" class="form-control" placeholder="Nhập lại mật khẩu mới"></asp:TextBox>
                </div>

                <div class="mt-auto">
                    <asp:Button ID="btnDoiMatKhau" runat="server" Text="Cập Nhật Mật Khẩu" CssClass="btn btn-warning fw-bold text-dark px-4 shadow-sm" OnClick="btnDoiMatKhau_Click" />
                </div>
            </div>
        </div>

        <!-- Cột 2: Cấu hình thông tin tiệm bánh -->
        <div class="col-lg-6">
            <div class="card border-0 shadow-sm rounded-4 p-4 bg-white h-100">
                <h5 class="fw-bold text-secondary mb-3"><i class="fa-solid fa-store me-2 text-warning"></i>Thông Tin Tiệm Bánh Ngọt</h5>
                <hr class="text-muted opacity-25" />

                <div class="mb-3">
                    <label class="form-label fw-semibold">Tên Tiệm / Cửa Hàng</label>
                    <asp:TextBox ID="txtTenTiem" runat="server" CssClass="form-control" Text="Hệ Thống Quản Lý Tiệm Bánh Ngọt"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Số Điện Thoại Liên Hệ</label>
                    <asp:TextBox ID="txtDienThoai" runat="server" CssClass="form-control" Text="0901234567"></asp:TextBox>
                </div>

                <div class="mb-4">
                    <label class="form-label fw-semibold">Địa Chỉ Cửa Hàng</label>
                    <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" Text="Long Xuyên, An Giang"></asp:TextBox>
                </div>

                <div class="mt-auto">
                    <asp:Button ID="btnLuuCauHinh" runat="server" Text="Lưu Cấu Hình" CssClass="btn btn-dark fw-bold px-4 shadow-sm" OnClick="btnLuuCauHinh_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
