<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuanLyHoaDon.aspx.cs" Inherits="Ql_tiembanhngot.aspx.QuanLyHoaDon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="d-flex justify-content-between align-items-center pb-2 mb-4 border-bottom">
        <h1 class="h3 fw-bold text-dark">
            <i class="fa-solid fa-file-invoice-dollar me-2 text-warning"></i>Quản Lý Hóa Đơn Bán Hàng
        </h1>
        <span class="text-muted">Theo dõi và quản lý hóa đơn doanh thu của cửa hàng</span>
    </div>

    <!-- Khung thông báo -->
    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-dismissible fade show" role="alert">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="row g-4">
        <!-- Form Thêm / Cập Nhật Hóa Đơn (Cột trái) -->
        <div class="col-md-4">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">
                        <asp:Label ID="lblFormTitle" runat="server" Text="Lập Hóa Đơn Mới"></asp:Label>
                    </h5>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Mã hóa đơn</label>
                        <asp:TextBox ID="txtMaHD" runat="server" CssClass="form-control" placeholder="Ví dụ: HD01"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Nhân viên lập</label>
                        <asp:DropDownList ID="ddlNhanVien" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Ngày lập</label>
                        <asp:TextBox ID="txtNgayLap" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Tổng tiền (VNĐ)</label>
                        <asp:TextBox ID="txtTongTien" runat="server" CssClass="form-control" placeholder="Ví dụ: 250000"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Trạng thái thanh toán</label>
                        <asp:DropDownList ID="ddlTrangThai" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Đã thanh toán" Value="Đã thanh toán" />
                            <asp:ListItem Text="Chưa thanh toán" Value="Chưa thanh toán" />
                            <asp:ListItem Text="Đã hủy" Value="Đã hủy" />
                        </asp:DropDownList>
                    </div>

                    <div class="d-grid gap-2">
                        <asp:Button ID="btnSave" runat="server" Text="Lưu Hóa Đơn" CssClass="btn btn-warning fw-semibold text-dark" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy / Làm mới" CssClass="btn btn-outline-secondary fw-semibold" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Bảng Danh Sách Hóa Đơn (Cột phải) -->
        <div class="col-md-8">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">Danh Sách Hóa Đơn Bán Hàng</h5>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <asp:GridView ID="gvHoaDon" runat="server" AutoGenerateColumns="False" 
                            CssClass="table table-hover table-striped align-middle mb-0" GridLines="None"
                            DataKeyNames="MaHD" OnRowCommand="gvHoaDon_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="MaHD" HeaderText="Mã HĐ" />
                                <asp:BoundField DataField="HoTen" HeaderText="Nhân Viên Lập" />
                                <asp:BoundField DataField="NgayLap" HeaderText="Ngày Lập" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="TongTien" HeaderText="Tổng Tiền" DataFormatString="{0:N0} đ" />
                                <asp:BoundField DataField="TrangThai" HeaderText="Trạng Thái" />
                                
                                <asp:TemplateField HeaderText="Thao Tác" ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditHD" CommandArgument='<%# Eval("MaHD") %>' CssClass="btn btn-sm btn-outline-primary me-1">
                                            <i class="fa-solid fa-pen-to-square"></i> Sửa
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteHD" CommandArgument='<%# Eval("MaHD") %>' 
                                            CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa hóa đơn này không?');">
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
