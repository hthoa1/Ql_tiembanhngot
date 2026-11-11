<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ql_tiembanhngot.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center align-items-center" style="min-height: 75vh;">
        <div class="col-md-5 col-lg-4">
            <div class="card border-0 shadow-lg rounded-4 overflow-hidden">
                <div class="card-header bg-warning text-dark text-center py-4 border-0">
                    <i class="fa-solid fa-cake-candles fa-3x mb-2"></i>
                    <h4 class="fw-bold mb-0 text-uppercase">Quản Lý Tiệm Bánh</h4>
                    <small class="fw-semibold">Đăng nhập hệ thống quản trị</small>
                </div>
                <div class="card-body p-4 bg-white">
                    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger p-2 mb-3" role="alert">
                        <small><asp:Label ID="lblError" runat="server"></asp:Label></small>
                    </asp:Panel>
                    
                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Tên đăng nhập</label>
                        <asp:TextBox ID="txtTenDangNhap" runat="server" CssClass="form-control bg-light" placeholder="Nhập tên đăng nhập"></asp:TextBox>
                    </div>
                    
                    <div class="mb-4">
                        <label class="form-label fw-semibold text-secondary">Mật khẩu</label>
                        <asp:TextBox ID="txtMatKhau" runat="server" TextMode="Password" CssClass="form-control bg-light" placeholder="Nhập mật khẩu"></asp:TextBox>
                    </div>
                    
                    <div class="d-grid">
                        <asp:Button ID="btnLogin" runat="server" Text="ĐĂNG NHẬP" CssClass="btn btn-warning fw-bold text-dark rounded-3 shadow-sm py-2" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
