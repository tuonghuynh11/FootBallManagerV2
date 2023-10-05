# FootballManager
 
# FBM

Ứng dụng hỗ trợ các tổ chức bóng đá quản lý các câu lạc bộ.

## 1. Mô tả 

Hiện nay việc quản lý các câu lạc bộ của tổ chức bóng đá khá là phức tạp và tốn nhiều công sức. Từ việc quản lí các nhân sự của 
một câu lạc bộ đến việc quản lý các giải đấu mà câu lạc bộ ấy tham gia và còn rất nhiều vấn đề liên quan khác nữa. Do đó, một
phần mềm có khả năng quản lý các câu lạc bộ của một tổ chức một cách rõ ràng, cụ thể, trực quan, sinh động và đặc biệt bất kì 
tổ chức bóng đá nào cũng có thể sử dụng được là cực kỳ cần thiết..

### 2. Mục đích, yêu cầu, người dùng hướng tới của đề tài

#### Mục đích

* Phần mềm được tạo ra nhằm mục đích giúp những tổ chức bóng đá có cái nhìn trực quan hơn đối với các thông tin cần thiết về tổ chức của mình, nâng cao năng suất và chất lượng của về mặt lưu trữ và quản lý dữ liệu.
* Phần mềm được tạo ra nhằm mục đích giúp các câu lạc bộ quản lý được dễ dàng và thuận tiện hơn.
* Hỗ trợ các câu lạc bộ có thể chuẩn bị một đội hình chất lượng khi thi đấu . 

#### Yêu cầu

* UI/UX hợp lý, rõ ràng, thuận tiện cho người sử dụng. 

* Ứng dụng có những tính năng cơ bản. 

* Phân chia quyền hạn rõ ràng. 

#### Người dùng

* Quản lý của một tổ chức bóng đá

* Ban huấn luyện của các câu lạc bộ

### 3. Tổng quan sản phẩm

#### 3.1 Chức năng
<details>
  <summary>Chức năng chung</summary>
 
- Đăng nhập
- Đăng xuất
- Quên mật khẩu
- Theo dõi số liệu tổng quan của tổ chức 
- Thiết lập các thông tin cá nhân
- Xem thông tin của các đội bóng
- Xem thông tin các giải đấu
- Theo dõi thông tin các trận đấu.
- Xem thông tin các cầu thủ
- Báo cáo lỗi

</details>

  ###### Admin (Quản trị viên)

  <details>
    <summary>Quản lý toàn bộ danh sách các câu lạc bộ có trong tổ chức</summary>

  - Tìm kiếm
  - Sắp xếp
  - Xóa
  - Xem chi tiết
  - Sửa
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý toàn bộ danh sách cầu thủ trong các đội bóng</summary>

  - Tìm kiếm
  - Xóa
  - Xem chi tiết
  - Sửa

  </details>

  <details>
    <summary>Quản lý toàn bộ danh sách ban huấn luyện của các đội bóng</summary>

  - Tìm kiếm
  - Thêm
  - Xóa
  - Xem chi tiết
  - Sửa
  - Cấp tài khoản sử dung

  </details>

  <details>
    <summary>Quản lý thị trường chuyển nhượng</summary>

  - Hủy bỏ phiên chuyển nhượng
  - Xem chi tiết phiên chuyển nhượng (đội mua, đội bán, cầu thủ đang chuyển nhượng)
  - Xác nhận phiên chuyển nhượng

  </details>

  <details>
    <summary>Quản lý các giải đấu</summary>

  - Thêm
  - Xóa
  - Cập nhật thông tin

  </details>

  <details>
    <summary>Quản lý các trận đấu</summary>

  - Thêm
  - Xóa
  - Cập nhật thông tin

  </details>

  <details>
    <summary>Quản lý các tài khoản được cấp </summary>

  - Thêm (tài khoản chủ tịch CLB, HLV trưởng, trợ lý HLV)
  - Xóa

  </details>


  ###### President (Chủ tịch CLB)

  <details>
    <summary>Quản lý toàn bộ danh sách ban huấn luyện của đội bóng </summary>

  - Tìm kiếm
  - Sắp xếp
  - Xóa
  - Xem chi tiết
  - Sửa

  </details>

  <details>
    <summary>Quản lý toàn bộ danh sách  cầu thủ của đội bóng </summary>

  - Tìm kiếm
  - Sắp xếp
  - Xóa
  - Xem chi tiết
  - Sửa

  </details>

  <details>
    <summary>Quản lý các phiên chuyển nhượng của đội bóng</summary>

  - Thêm
  - Xóa
  - Xem chi tiết

  </details>

  <details>
    <summary>Quản lý lịch tập luyện của đội</summary>

  - Thêm
  - Xóa
  - Xem chi tiết

  </details>

  <details>
    <summary>Quản lý đội hình chiến thuật</summary>

  - Sắp xếp

  </details>

  <details>
    <summary>Quản lý lịch thi đấu </summary>

  - Tìm kiếm
  - Xem thông tin


  </details>


  ###### Coach (Huấn luyện viên)
<details>
<summary>Quản lý toàn bộ danh sách cầu thủ của đội bóng </summary>

  - Tìm kiếm
  - Sắp xếp
  - Xem chi tiết

  </details>

  <details>
    <summary>Quản lý các phiên chuyển nhượng của đội bóng</summary>

  - Thêm
  - Xóa
  - Xem chi tiết

  </details>

  <details>
    <summary>Quản lý lịch tập luyện của đội</summary>

  - Thêm
  - Xóa
  - Xem chi tiết

  </details>

  <details>
    <summary>Quản lý đội hình chiến thuật</summary>

  - Sắp xếp

  </details>

  <details>
    <summary>Quản lý lịch thi đấu </summary>

  - Tìm kiếm
  - Xem thông tin

  </details>


###### Assistant (Trợ lý huấn luyện viên)
<details>
<summary>Quản lý toàn bộ danh sách cầu thủ của đội bóng </summary>

  - Tìm kiếm
  - Sắp xếp
  - Xem chi tiết

  </details>

  <details>
    <summary>Quản lý các phiên chuyển nhượng của đội bóng</summary>

  - Xem chi tiết

  </details>

  <details>
    <summary>Quản lý lịch tập luyện của đội</summary>

  - Thêm
  - Xóa
  - Xem chi tiết

  </details>

  <details>
    <summary>Quản lý đội hình chiến thuật</summary>

  - Xem chi tiết

  </details>

  <details>
    <summary>Quản lý lịch thi đấu </summary>

  - Tìm kiếm
  - Xem thông tin

  </details>


#### 3.2 Công nghệ sử dụng

- Công cụ: Visual Studio, SQL Server Management Studio, Github Desktop, Microsoft SQL Server, Microsoft Azure
- Ngôn ngữ lập trình: C#, TSQL
- Thư viện: .NET Framework, MaterialDesignXAML, Show Me The XAML, Entity Framework, Devexpress Framework, WPF

## 4. Hướng dẫn cài đặt
<details>
    <summary>Đối với người dùng</summary>

  * Liên hệ với nhà phát triển để được hỗ trợ khởi tạo cơ sở dữ liệu và kết nối đến cơ sở dữ liệu.
  * (Thêm sau)
    * Dowload phần mềm tại: 
(Thêm sau)

</details>

<details>
    <summary>Đối với nhà phát triển</summary>

  * Dowload, giải nén phần mềm
    * Github: (Thêm sau)
    * Google Drive: (Thêm sau)
  * Cài đặt database
    * Khuyến nghị sử dụng các dịch vụ đám mây như Azure, AWS,… để sử dụng tất cả tính năng hiện có của chương trình  (server đi kèm với chương trình đã đóng).
    * Ngoài ra có thể sử dụng SQL Server (Lưu ý: cách này sẽ mất đi tính năng tương tác giữa các user ở các máy tính khác nhau).
  * Khởi tạo Database bằng cách chạy script chứa trong file TaoCSL.sql
  * Kết nối với Database vừa tạo bằng cách thay đổi connectionStrings trong file App.config.
  * Có thể sử dụng project Seeds để tạo dữ liệu giả.
  * Đăng nhập với vai trò admin
      * tên đăng nhập: admin
      * mật khẩu: 1234

</details>

## 5. Hướng dẫn sử dụng

* Video demo: (thêm sau)

## 6. Tác giả

| STT | MSSV     | Họ và tên                                                  | Lớp      | 
| --- | -------- | ---------------------------------------------------------- | -------- | 
| 1   | 21520123| [Huỳnh Mạnh Tường](https://github.com/tuonghuynh11)           | KTPM2021 | 
| 2   | 21520341| [Dương Ngọc Mẫn](https://github.com/DNM03)              | KTPM2021 | 
| 3   | 21520613| [Nguyễn Hoàng Quốc Bảo](https://github.com/QuocBaoKho) | KTPM2021 | 
| 4   | 21520839| [Lê Phan Hiển](https://github.com/hienlephan2003)         	  | KTPM2021 | 
* Sinh viên khoa Công nghệ Phần mềm, trường Đại học Công nghệ Thông tin, Đại học Quốc gia thành phố Hồ Chí Minh.

## 7. Giảng viên hướng dẫn

* Thầy Nguyễn Tấn Toàn, giảng viên Khoa Công Nghệ Phần Mềm, trường Đại học Công nghệ Thông tin, Đại học Quốc gia Thành phố Hồ Chí Minh.
