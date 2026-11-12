# 🎂 Hệ Thống Quản Lý Tiệm Bánh Ngọt (Bakery Manager)(còn chỉnh sửa thêm :' )

Ứng dụng web quản lý tiệm bánh ngọt được phát triển bằng **ASP.NET Web Forms**, thiết kế giao diện hiện đại với **Bootstrap 5** và kết nối cơ sở dữ liệu trực tiếp bằng **SQL Server**.

---

## 🚀 Các Chức Năng Chính & Phân Công Thành Viên

Dự án được phối hợp thực hiện bởi các thành viên trong nhóm:

| Thành Viên | Nhiệm Vụ & Các Module Phụ Trách |
| :--- | :--- |
| **Huỳnh Thanh Hòa** | • Thiết lập kết nối CSDL chung (`Database.cs`) & Xây dựng trang Đăng nhập (`Default.aspx`)<br>• Phát triển Khung giao diện hệ thống & Phân quyền (`MasterPage.master`)<br>• Module **Cài đặt hệ thống** & Module **Thống kê tổng quan** (`ThongKe.aspx`) |
| **Bùi Quốc Huy** | • Module **Quản lý sản phẩm Bánh** (`Banh.aspx`)<br>• Module **Quản lý Loại Bánh** (`LoaiBanh.aspx`) |
| **Trần Bảo Phát** | • Module **Lập Hóa Đơn** bán hàng (`HoaDon.aspx`)<br>• Module **Quản lý Đơn Đặt Bánh** (`DonDatBanh.aspx`) |
| **Nguyễn Thành Phú** | • Module **Quản lý Nhân Viên** (`QuanLyNhanVien.aspx`)<br>• Module **Quản lý Kho Nguyên Liệu** (`QuanLyNguyenLieu.aspx`) |

---

## 🛠️ Công Nghệ Sử Dụng

* **Frontend:** HTML5, CSS3, JavaScript, **Bootstrap 5**, **FontAwesome 6** (giao diện responsive).
* **Backend:** C#, **ASP.NET Web Forms** (.NET Framework 4.8).
* **Database:** **Microsoft SQL Server** (sử dụng ADO.NET thuần với `SqlConnection`, `SqlCommand`, `DataTable` kết hợp tham số an toàn chống SQL Injection).
* **Công cụ phát triển:** Visual Studio, SQL Server Management Studio (SSMS), Git/GitHub.

---

## 📂 Cấu Trúc Thư Mục Dự Án

```text
ql_banh/
│
├── aspx/                   # Chứa các trang giao diện chức năng của hệ thống
│   ├── CaiDat.aspx
│   ├── DonDatBanh.aspx
│   ├── HoaDon.aspx
│   ├── Banh.aspx
│   ├── LoaiBanh.aspx
│   ├── QuanLyNhanVien.aspx
│   ├── QuanLyNguyenLieu.aspx
│   └── ThongKe.aspx
│
├── ketnoi/                 # Lớp xử lý kết nối cơ sở dữ liệu dùng chung
│   └── Database.cs         # Cung cấp các hàm GetData, ExecuteNonQuery, ExecuteScalar
│
├── Default.aspx            # Đăng nhập hệ thống vào thống kê
├── MasterPage.master       # Khung giao diện chung (Sidebar phân quyền & Header)
└── README.md               # Tài liệu hướng dẫn dự án
