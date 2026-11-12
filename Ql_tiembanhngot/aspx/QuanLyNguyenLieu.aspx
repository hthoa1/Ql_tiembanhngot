<%@ Page Title="" Language="C#" MasterPageFile="~/Trangone.Master" AutoEventWireup="true" CodeFile="QuanLyNguyenLieu.aspx.cs" Inherits="aspx_QuanLyNguyenLieu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="d-flex justify-content-between align-items-center pb-2 mb-4 border-bottom">
        <h1 class="h3 fw-bold text-dark">
            <i class="fa-solid fa-boxes-stacked me-2 text-warning"></i>Kho Nguyên Liệu
        </h1>
        <span class="text-muted">Quản lý số lượng tồn kho và nhà cung cấp nguyên liệu</span>
    </div>

    <!-- Khung thông báo -->
    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-dismissible fade show" role="alert">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="row g-4">
        <!-- Form Nhập / Sửa Nguyên Liệu (Cột trái) -->
        <div class="col-md-4">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">
                        <asp:Label ID="lblFormTitle" runat="server" Text="Thêm Nguyên Liệu Mới"></asp:Label>
                    </h5>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Mã nguyên liệu</label>
                        <asp:TextBox ID="txtMaNL" runat="server" CssClass="form-control" placeholder="Ví dụ: NL01"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Tên nguyên liệu</label>
                        <asp:TextBox ID="txtTenNL" runat="server" CssClass="form-control" placeholder="Nhập tên nguyên liệu"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Số lượng tồn</label>
                        <asp:TextBox ID="txtSoLuongTon" runat="server" CssClass="form-control" placeholder="Ví dụ: 50"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Đơn vị tính</label>
                        <asp:TextBox ID="txtDonViTinh" runat="server" CssClass="form-control" placeholder="Ví dụ: Kg, Lít, Gói"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Nhà cung cấp</label>
                        <asp:TextBox ID="txtNhaCC" runat="server" CssClass="form-control" placeholder="Nhập tên nhà cung cấp"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Ngưỡng cảnh báo</label>
                        <asp:TextBox ID="txtNguongCanhBao" runat="server" CssClass="form-control" placeholder="Ví dụ: 5"></asp:TextBox>
                    </div>

                    <div class="d-grid gap-2">
                        <asp:Button ID="btnSave" runat="server" Text="Thêm Mới" CssClass="btn btn-warning fw-semibold text-dark" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy / Làm mới" CssClass="btn btn-outline-secondary fw-semibold" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Bảng Danh Sách Nguyên Liệu (Cột phải) -->
        <div class="col-md-8">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">Danh Sách Nguyên Liệu Trong Kho</h5>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <asp:GridView ID="gvNguyenLieu" runat="server" AutoGenerateColumns="False" 
                            CssClass="table table-hover table-striped align-middle mb-0" GridLines="None"
                            DataKeyNames="MaNL" OnRowCommand="gvNguyenLieu_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="MaNL" HeaderText="Mã NL" />
                                <asp:BoundField DataField="TenNL" HeaderText="Tên Nguyên Liệu" />
                                <asp:BoundField DataField="SoLuongTon" HeaderText="SL Tồn" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="DonViTinh" HeaderText="ĐVT" />
                                <asp:BoundField DataField="NhaCC" HeaderText="Nhà Cung Cấp" />
                                <asp:BoundField DataField="NguongCanhBao" HeaderText="Ngưỡng Cảnh Báo" DataFormatString="{0:N0}" />
                                
                                <asp:TemplateField HeaderText="Thao Tác" ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditNL" CommandArgument='<%# Eval("MaNL") %>' CssClass="btn btn-sm btn-outline-primary me-1">
                                            <i class="fa-solid fa-pen-to-square"></i> Sửa
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteNL" CommandArgument='<%# Eval("MaNL") %>' 
                                            CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa nguyên liệu này?');">
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

