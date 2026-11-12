<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuanLyLoaiBanh.aspx.cs" Inherits="Ql_tiembanhngot.aspx.QuanLyLoaiBanh" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center pb-2 mb-4 border-bottom">
        <h1 class="h3 fw-bold text-dark">
            <i class="fa-solid fa-list-check me-2 text-warning"></i>Quản Lý Loại Bánh
        </h1>
        <span class="text-muted">Danh mục các nhóm bánh trong cửa hàng</span>
    </div>

    <!-- Khung thông báo -->
    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-dismissible fade show" role="alert">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="row g-4">
        <!-- Form Nhập / Sửa Loại Bánh (Cột trái) -->
        <div class="col-md-4">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">
                        <asp:Label ID="lblFormTitle" runat="server" Text="Thêm Loại Bánh Mới"></asp:Label>
                    </h5>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Mã loại bánh</label>
                        <asp:TextBox ID="txtMaLoai" runat="server" CssClass="form-control" placeholder="Ví dụ: LB04"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Tên loại bánh</label>
                        <asp:TextBox ID="txtTenLoai" runat="server" CssClass="form-control" placeholder="Ví dụ: Bánh Tart"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Mô tả</label>
                        <asp:TextBox ID="txtMoTa" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Nhập mô tả loại bánh"></asp:TextBox>
                    </div>

                    <div class="d-grid gap-2">
                        <asp:Button ID="btnSave" runat="server" Text="Thêm Mới" CssClass="btn btn-warning fw-semibold text-dark" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy / Làm mới" CssClass="btn btn-outline-secondary fw-semibold" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Bảng Danh Sách Loại Bánh (Cột phải) -->
        <div class="col-md-8">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-white py-3">
                    <h5 class="mb-0 fw-bold text-secondary">Danh Sách Loại Bánh</h5>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <asp:GridView ID="gvLoaiBanh" runat="server" AutoGenerateColumns="False" 
                            CssClass="table table-hover table-striped align-middle mb-0" GridLines="None"
                            DataKeyNames="MaLoai" OnRowCommand="gvLoaiBanh_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="MaLoai" HeaderText="Mã Loại" />
                                <asp:BoundField DataField="TenLoai" HeaderText="Tên Loại Bánh" />
                                <asp:BoundField DataField="MoTa" HeaderText="Mô Tả" />
                                
                                <asp:TemplateField HeaderText="Thao Tác" ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditLoai" CommandArgument='<%# Eval("MaLoai") %>' CssClass="btn btn-sm btn-outline-primary me-1">
                                            <i class="fa-solid fa-pen-to-square"></i> Sửa
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteLoai" CommandArgument='<%# Eval("MaLoai") %>' 
                                            CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa loại bánh này?');">
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
