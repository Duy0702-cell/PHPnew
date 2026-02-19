HUONG DAN CHAY DU AN ASP.NET WEB FORMS (VS 2013)

1) Mo Visual Studio 2013 Ultimate.
2) Chon: File -> Open -> Web Site...
3) Tro den thu muc: C:\Users\Admin\Desktop\demo asp
4) Chon Default.aspx -> Set As Start Page.
5) Nhan Ctrl + F5 de chay.

CAU TRUC FILE THEO KIEU ASP.NET WEB FORMS:
- Moi trang co 2 file:
  + Giao dien: .aspx
  + Code-behind: .aspx.cs
- Vi du:
  + Default.aspx + Default.aspx.cs
  + AdminProducts.aspx + AdminProducts.aspx.cs
  + Master page: Site.Master + Site.Master.cs

DA CHINH THEO 2 YEU CAU MOI:
1) Dat ten theo style bai giang (tieng Viet, de hieu)
- Class: TrangChu, ChiTietSanPham, GioHang, DangNhapQuanTri, QuanLySanPham, MasterPage
- Control: dlSanPham, gvGioHang, gvSanPham, txtTenSanPham, lblThongBaoLoi...

2) Tach lop DataProvider de quan ly truy van Access
- File: App_Code\DataProvider.cs
- ProductService da dung DataProvider cho:
  + Lay du lieu (DataTable)
  + Them/Sua/Xoa
  + Truy van theo ID

STYLE CODE GIU DUNG MUC TIEU LOP HOC:
- Dung control server (DataList, GridView, Label, Image)
- Dung DataBind() trong code-behind
- Xu ly su kien ItemCommand/RowCommand
- Tach ro Page_Load, ham nap du lieu, ham xu ly su kien

CAU HINH DATABASE ACCESS:
- Connection string dang tro den:
  |DataDirectory|\Shop.mdb
- Vi vay ban can dat file Access ten Shop.mdb vao thu muc:
  C:\Users\Admin\Desktop\demo asp\App_Data\Shop.mdb

NEU BAN DA CO FILE DB KHAC (VD: dbASPX.accdb):
- Copy file do vao App_Data, doi ten thanh Shop.mdb
  HOAC
- Sua trong Web.config dong Data Source thanh ten file cua ban.

SCHEMA BANG MAC DINH:
- Ten bang: Products
- Cac cot:
  Id (AutoNumber, PK)
  ProductName (Text)
  Brand (Text)
  Price (Currency)
  ImageUrl (Text)
  Description (Long Text)
  NoiBat (Long Text): luu 3 y noi bat trong 1 o, cach nhau bang dau |

File SQL mau tao bang: App_Data\create_products.sql
File SQL them cot NoiBat cho DB cu: App_Data\add_noibat_column.sql

QUAN TRI SAN PHAM:
- Trang dang nhap: AdminLogin.aspx
- Trang quan ly: AdminProducts.aspx
- Tai khoan mac dinh: admin
- Mat khau mac dinh: 123456
- Doi tai khoan/mat khau trong Web.config (appSettings: AdminUser, AdminPass)

THEM NHIEU SAN PHAM DE TEST PHAN TRANG:
- Danh sach trang chu da phan trang 12 san pham/trang (3x4)
- Nut Trang truoc / Trang sau o cuoi danh sach
- File seed: App_Data\seed_products_48.sql
- Chay file SQL nay trong Access de chen them 48 san pham mau
