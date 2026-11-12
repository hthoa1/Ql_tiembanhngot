<%@ Page Title="" Language="C#" MasterPageFile="~/Trangone.Master" AutoEventWireup="true" CodeFile="QuanLyBanh.aspx.cs" Inherits="aspx_QuanLyBanh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="d-flex justify-content-between align-items-center pb-2 mb-4 border-bottom">
        <h1 class="h3 fw-bold text-dark">
            <i class="fa-solid fa-cookie-bite me-2 text-warning"></i>Quản Lý Sản Phẩm Bánh
        </h1>
        <span class="text-muted">Danh mục và thông tin chi tiết các loại bánh</span>
    </div>

    <!-- Khung thông báo -->
    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-dismissible fade show" role="alert">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="row g-4">
        <!-- Form Nhập / Sửa Sản Phẩm (Cột trái) -->
        <div class="col-md-4">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">
                        <asp:Label ID="lblFormTitle" runat="server" Text="Thêm Bánh Mới"></asp:Label>
                    </h5>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Mã bánh</label>
                        <asp:TextBox ID="txtMaBanh" runat="server" CssClass="form-control" placeholder="Ví dụ: B05"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Tên bánh</label>
                        <asp:TextBox ID="txtTenBanh" runat="server" CssClass="form-control" placeholder="Nhập tên bánh"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Loại bánh</label>
                        <asp:DropDownList ID="ddlLoaiBanh" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Giá bán (VNĐ)</label>
                        <asp:TextBox ID="txtGiaBan" runat="server" CssClass="form-control" placeholder="Ví dụ: 150000"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Đơn vị tính</label>
                        <asp:TextBox ID="txtDonViTinh" runat="server" CssClass="form-control" placeholder="Ví dụ: Cái, Chiếc"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Tên file hình ảnh</label>
                        <asp:TextBox ID="txtHinhAnh" runat="server" CssClass="form-control" placeholder="Ví dụ: banh1.jpg"></asp:TextBox>
                    </div>

                    <div class="d-grid gap-2">
                        <asp:Button ID="btnSave" runat="server" Text="Thêm Mới" CssClass="btn btn-warning fw-semibold text-dark" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy / Làm mới" CssClass="btn btn-outline-secondary fw-semibold" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Bảng Danh Sách Sản Phẩm (Cột phải) -->
        <div class="col-md-8">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">Danh Sách Bánh Hiện Có</h5>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <asp:GridView ID="gvBanh" runat="server" AutoGenerateColumns="False" 
                            CssClass="table table-hover table-striped align-middle mb-0" GridLines="None"
                            DataKeyNames="MaBanh" OnRowCommand="gvBanh_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="MaBanh" HeaderText="Mã Bánh" />
                                <asp:BoundField DataField="TenBanh" HeaderText="Tên Bánh" />
                                <asp:BoundField DataField="TenLoai" HeaderText="Loại Bánh" />
                                <asp:BoundField DataField="GiaBan" HeaderText="Giá Bán" DataFormatString="{0:N0} đ" />
                                <asp:BoundField DataField="DonViTinh" HeaderText="ĐVT" />
                                <asp:BoundField DataField="HinhAnh" HeaderText="Hình Ảnh" />
                                
                                <asp:TemplateField HeaderText="Thao Tác" ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditBanh" CommandArgument='<%# Eval("MaBanh") %>' CssClass="btn btn-sm btn-outline-primary me-1">
                                            <i class="fa-solid fa-pen-to-square"></i> Sửa
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteBanh" CommandArgument='<%# Eval("MaBanh") %>' 
                                            CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa sản phẩm này?');">
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

