<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ThongKe.aspx.cs" Inherits="Ql_tiembanhngot.aspx.ThongKe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="row mb-4">
        <div class="col-md-12">
            <h2 class="fw-bold text-dark"><i class="fa-solid fa-chart-line me-2 text-warning"></i>Tổng Quan & Thống Kê Doanh Thu</h2>
            <p class="text-muted">Thống kê nhanh các chỉ số hoạt động kinh doanh của tiệm bánh.</p>
            <hr />
        </div>
    </div>

    <!-- Các thẻ Widget thống kê nhanh -->
    <div class="row g-4 mb-4">
        <!-- Tổng doanh thu -->
        <div class="col-xl-3 col-md-6">
            <div class="card border-0 shadow-sm rounded-4 bg-primary text-white p-3">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="text-uppercase fw-semibold mb-1 opacity-75">Tổng Doanh Thu</h6>
                        <h3 class="fw-bold mb-0"><asp:Label ID="lblTongDoanhThu" runat="server" Text="0 đ"></asp:Label></h3>
                    </div>
                    <div class="fs-1 opacity-50">
                        <i class="fa-solid fa-sack-dollar"></i>
                    </div>
                </div>
            </div>
        </div>

        <!-- Tổng hóa đơn -->
        <div class="col-xl-3 col-md-6">
            <div class="card border-0 shadow-sm rounded-4 bg-success text-white p-3">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="text-uppercase fw-semibold mb-1 opacity-75">Tổng Hóa Đơn</h6>
                        <h3 class="fw-bold mb-0"><asp:Label ID="lblTongHoaDon" runat="server" Text="0"></asp:Label></h3>
                    </div>
                    <div class="fs-1 opacity-50">
                        <i class="fa-solid fa-file-invoice-dollar"></i>
                    </div>
                </div>
            </div>
        </div>

        <!-- Tổng sản phẩm bánh -->
        <div class="col-xl-3 col-md-6">
            <div class="card border-0 shadow-sm rounded-4 bg-warning text-dark p-3">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="text-uppercase fw-semibold mb-1 opacity-75">Sản Phẩm Bánh</h6>
                        <h3 class="fw-bold mb-0"><asp:Label ID="lblTongBanh" runat="server" Text="0"></asp:Label></h3>
                    </div>
                    <div class="fs-1 opacity-50">
                        <i class="fa-solid fa-cookie-bite"></i>
                    </div>
                </div>
            </div>
        </div>

        <!-- Tổng nguyên liệu trong kho -->
        <div class="col-xl-3 col-md-6">
            <div class="card border-0 shadow-sm rounded-4 bg-danger text-white p-3">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="text-uppercase fw-semibold mb-1 opacity-75">Nguyên Liệu Kho</h6>
                        <h3 class="fw-bold mb-0"><asp:Label ID="lblTongNguyenLieu" runat="server" Text="0"></asp:Label></h3>
                    </div>
                    <div class="fs-1 opacity-50">
                        <i class="fa-solid fa-boxes-stacked"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bảng hiển thị hóa đơn gần đây -->
    <div class="card border-0 shadow-sm rounded-4 p-4 bg-white">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h5 class="fw-bold text-secondary mb-0"><i class="fa-solid fa-clock-rotate-left me-2"></i>Hóa Đơn Bán Hàng Gần Nhất</h5>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvHoaDon" runat="server" CssClass="table table-hover align-middle" AutoGenerateColumns="False" EmptyDataText="Chưa có dữ liệu hóa đơn nào.">
                <Columns>
                    <asp:BoundField DataField="MaHD" HeaderText="Mã HĐ" />
                    <asp:BoundField DataField="HoTen" HeaderText="Nhân Viên Lập" />
                    <asp:BoundField DataField="NgayLap" HeaderText="Ngày Lập" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField DataField="TongTien" HeaderText="Tổng Tiền" DataFormatString="{0:N0} đ" />
                    <asp:BoundField DataField="PhuongThucTT" HeaderText="Phương Thức TT" />
                    <asp:BoundField DataField="TrangThai" HeaderText="Trạng Thái" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
