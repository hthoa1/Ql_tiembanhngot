USE [QuanLyTiemBanh]
GO

-- =============================================
-- 1. THÊM DỮ LIỆU BẢNG LOẠI BÁNH
-- =============================================
INSERT INTO [dbo].[LoaiBanh] ([MaLoai], [TenLoai], [MoTa]) VALUES
('LB01', N'Bánh Kem Sinh Nhật', N'Các loại bánh kem trang trí dùng cho sinh nhật, kỷ niệm'),
('LB02', N'Bánh Ngọt Âu', N'Các loại bánh ngọt phong cách châu Âu như Tiramisu, Mousse'),
('LB03', N'Bánh Mì & Bánh Mặn', N'Bánh mì bơ tỏi, bánh mì ngọt, bánh mặn ăn liền');
GO

-- =============================================
-- 2. THÊM DỮ LIỆU BẢNG NHÂN VIÊN
-- =============================================
INSERT INTO [dbo].[NhanVien] ([MaNV], [HoTen], [SoDienThoai], [ChucVu]) VALUES
('NV01', N'Huỳnh Thanh Hòa', '0901234567', N'Quản lý'),
('NV02', N'Nguyễn Văn A', '0912345678', N'Thu ngân'),
('NV03', N'Trần Thị B', '0987654321', N'Thợ bánh');
GO

-- =============================================
-- 3. THÊM DỮ LIỆU BẢNG TÀI KHOẢN (Mật khẩu mẫu: 123456)
-- =============================================
INSERT INTO [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [Quyen], [MaNV]) VALUES
('admin', '123456', N'Quản lý', 'NV01'),
('thungan', '123456', N'Thu ngân', 'NV02');
GO

-- =============================================
-- 4. THÊM DỮ LIỆU BẢNG NGUYÊN LIỆU
-- =============================================
INSERT INTO [dbo].[NguyenLieu] ([MaNL], [TenNL], [DonViTinh], [SoLuongTon], [NguongCanhBao], [NhaCC], [NgayNhap]) VALUES
('NL01', N'Bột mì số 11', N'Kg', 100.00, 10.00, N'Công ty TNHH Thực Phẩm Tân Việt', GETDATE()),
('NL02', N'Đường tinh luyện', N'Kg', 50.00, 5.00, N'Công ty TNHH Thực Phẩm Tân Việt', GETDATE()),
('NL03', N'Bơ lạt Anchor', N'Kg', 30.00, 5.00, N'Đại lý Bơ Sữa Hoàng Gia', GETDATE()),
('NL04', N'Sữa tươi không đường', N'Lít', 40.00, 8.00, N'Đại lý Bơ Sữa Hoàng Gia', GETDATE()),
('NL05', N'Trứng gà', N'Quả', 500.00, 50.00, N'Nông sản & Trứng Phước Long', GETDATE()),
('NL06', N'Bột Cacao', N'Kg', 15.00, 3.00, N'Công ty TNHH Thực Phẩm Tân Việt', GETDATE());
GO

-- =============================================
-- 5. THÊM DỮ LIỆU BẢNG BÁNH (SẢN PHẨM)
-- =============================================
INSERT INTO [dbo].[Banh] ([MaBanh], [TenBanh], [MaLoai], [GiaBan], [DonViTinh], [HinhAnh]) VALUES
('B01', N'Bánh Kem Socola', 'LB01', 250000.00, N'Cái', 'socola.jpg'),
('B02', N'Bánh Tiramisu', 'LB02', 180000.00, N'Ổ', 'tiramisu.jpg'),
('B03', N'Bánh Mì Bơ Tỏi', 'LB03', 45000.00, N'Túi', 'botoi.jpg');
GO

-- =============================================
-- 6. THÊM DỮ LIỆU BẢNG CÔNG THỨC
-- =============================================
INSERT INTO [dbo].[CongThuc] ([MaLoai], [MaNL], [DinhMuc], [DonViTinh]) VALUES
('LB01', 'NL01', 0.30, N'Kg'),
('LB01', 'NL02', 0.15, N'Kg'),
('LB01', 'NL03', 0.10, N'Kg'),
('LB01', 'NL05', 4.00, N'Quả'),
('LB01', 'NL06', 0.05, N'Kg'),
('LB02', 'NL01', 0.20, N'Kg'),
('LB02', 'NL03', 0.15, N'Kg'),
('LB02', 'NL04', 0.20, N'Lít'),
('LB03', 'NL01', 0.40, N'Kg'),
('LB03', 'NL03', 0.08, N'Kg');
GO

-- =============================================
-- 7. THÊM DỮ LIỆU BẢNG ĐƠN ĐẶT BÁNH
-- =============================================
INSERT INTO [dbo].[DonDatBanh] ([MaDon], [TenKhachHang], [SoDienThoai], [TenLoai], [NgayDat], [NgayNhan], [YeuCauChiTiet], [TongTien], [TienCoc], [TrangThai], [SoLuong]) VALUES
('DD01', N'Nguyễn Thị Lan', '0911222333', N'Bánh Kem Sinh Nhật', GETDATE(), DATEADD(day, 2, GETDATE()), N'Viết chữ: Chúc Mừng Sinh Nhật', 250000.00, 50000.00, N'Đã xác nhận', 1);
GO

-- =============================================
-- 8. THÊM DỮ LIỆU BẢNG HÓA ĐƠN & CHI TIẾT HÓA ĐƠN
-- =============================================
INSERT INTO [dbo].[HoaDon] ([MaHD], [MaNV], [MaDon], [NgayLap], [TongTien], [PhuongThucTT], [TrangThai]) VALUES
('HD01', 'NV02', 'DD01', GETDATE(), 250000.00, N'Tiền mặt', N'Đã thanh toán');
GO

INSERT INTO [dbo].[ChiTietHoaDon] ([MaHD], [MaBanh], [SoLuong], [DonGia], [ThanhTien]) VALUES
('HD01', 'B01', 1, 250000.00, 250000.00);
GO