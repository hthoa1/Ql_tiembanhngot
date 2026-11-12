<%@ Page Title="" Language="C#" MasterPageFile="~/Trangone.Master" AutoEventWireup="true" CodeFile="QuanLyDonDat.aspx.cs" Inherits="aspx_QuanLyDonDat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="d-flex justify-content-between align-items-center pb-2 mb-4 border-bottom">
        <h1 class="h3 fw-bold text-dark">
            <i class="fa-solid fa-calendar-check me-2 text-warning"></i>Quản Lý Đơn Đặt Bánh
        </h1>
        <span class="text-muted">Theo dõi và cập nhật trạng thái đơn đặt bánh của khách hàng</span>
    </div>

    <!-- Khung thông báo -->
    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-dismissible fade show" role="alert">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="row g-4">
        <!-- Form Cập Nhật / Thêm Đơn Đặt (Cột trái) -->
        <div class="col-md-4">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">
                        <asp:Label ID="lblFormTitle" runat="server" Text="Thêm Đơn Đặt Mới"></asp:Label>
                    </h5>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Mã đơn đặt</label>
                        <asp:TextBox ID="txtMaDon" runat="server" CssClass="form-control" placeholder="Ví dụ: DD01"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Tên khách hàng</label>
                        <asp:TextBox ID="txtTenKhachHang" runat="server" CssClass="form-control" placeholder="Nhập tên khách hàng"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Số điện thoại</label>
                        <asp:TextBox ID="txtSoDienThoai" runat="server" CssClass="form-control" placeholder="Nhập số điện thoại"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Ngày nhận bánh dự kiến</label>
                        <asp:TextBox ID="txtNgayNhan" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Tổng tiền (VNĐ)</label>
                        <asp:TextBox ID="txtTongTien" runat="server" CssClass="form-control" placeholder="Ví dụ: 300000"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Trạng thái đơn hàng</label>
                        <asp:DropDownList ID="ddlTrangThai" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Chờ xử lý" Value="Chờ xử lý" />
                            <asp:ListItem Text="Đang làm bánh" Value="Đang làm bánh" />
                            <asp:ListItem Text="Đã hoàn thành" Value="Đã hoàn thành" />
                            <asp:ListItem Text="Đã hủy" Value="Đã hủy" />
                        </asp:DropDownList>
                    </div>

                    <div class="d-grid gap-2">
                        <asp:Button ID="btnSave" runat="server" Text="Thêm Mới" CssClass="btn btn-warning fw-semibold text-dark" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy / Làm mới" CssClass="btn btn-outline-secondary fw-semibold" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Bảng Danh Sách Đơn Đặt Bánh (Cột phải) -->
        <div class="col-md-8">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">Danh Sách Đơn Đặt Bánh</h5>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDonDat" runat="server" AutoGenerateColumns="False" 
                            CssClass="table table-hover table-striped align-middle mb-0" GridLines="None"
                            DataKeyNames="MaDon" OnRowCommand="gvDonDat_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="MaDon" HeaderText="Mã Đơn" />
                                <asp:BoundField DataField="TenKhachHang" HeaderText="Khách Hàng" />
                                <asp:BoundField DataField="SoDienThoai" HeaderText="SĐT" />
                                <asp:BoundField DataField="NgayNhan" HeaderText="Ngày Nhận" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="TongTien" HeaderText="Tổng Tiền" DataFormatString="{0:N0} đ" />
                                <asp:BoundField DataField="TrangThai" HeaderText="Trạng Thái" />
                                
                                <asp:TemplateField HeaderText="Thao Tác" ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditDon" CommandArgument='<%# Eval("MaDon") %>' CssClass="btn btn-sm btn-outline-primary me-1">
                                            <i class="fa-solid fa-pen-to-square"></i> Sửa
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteDon" CommandArgument='<%# Eval("MaDon") %>' 
                                            CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa đơn đặt này không?');">
                                            <i class="fa-solid fa-trash"></i> Xóa
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

