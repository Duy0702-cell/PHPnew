using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemoAsp;

public partial class ChiTietSanPham : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            KhoiTaoSoLuong();
            HienThiChiTiet();
        }
    }

    private void KhoiTaoSoLuong()
    {
        ddlSoLuong.Items.Clear();
        for (int i = 1; i <= 5; i++)
        {
            ddlSoLuong.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    private void HienThiChiTiet()
    {
        int maSanPham;
        if (!int.TryParse(Request.QueryString["id"], out maSanPham))
        {
            pnlKhongTimThay.Visible = true;
            return;
        }

        ProductService db = new ProductService();
        Product sp = db.GetProductById(maSanPham);
        if (sp == null)
        {
            pnlKhongTimThay.Visible = true;
            return;
        }

        hdnMaSanPham.Value = sp.Id.ToString();
        pnlChiTiet.Visible = true;

        lblMaSanPham.Text = "PV-" + sp.Id.ToString("00000");
        lblTenSanPham.Text = sp.Name;
        lblHang.Text = sp.Brand;
        lblGia.Text = string.Format("{0:N0} đ", sp.Price);
        lblMoTa.Text = sp.Description;

        List<string> dsNoiBat = TachNoiBat(sp);
        lblNoiBat1.Text = dsNoiBat[0];
        lblNoiBat2.Text = dsNoiBat[1];
        lblNoiBat3.Text = dsNoiBat[2];

        List<string> dsHinh = TaoDanhSachHinh(sp);
        imgSanPhamLon.ImageUrl = dsHinh[0];
        imgSanPhamLon.AlternateText = sp.Name;

        rptHinhPhu.DataSource = dsHinh;
        rptHinhPhu.DataBind();

        gvThongSo.DataSource = TaoBangThongSo(sp);
        gvThongSo.DataBind();

        NapSanPhamLienQuan(sp.Id);
    }

    private List<string> TachNoiBat(Product sp)
    {
        List<string> ds = new List<string>();

        string nguon = sp.NoiBat ?? string.Empty;
        nguon = nguon.Replace("\r\n", "|").Replace("\n", "|").Replace(";", "|");

        string[] tach = nguon.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < tach.Length; i++)
        {
            string item = tach[i].Trim();
            if (!string.IsNullOrWhiteSpace(item))
            {
                ds.Add(item);
            }
        }

        if (ds.Count == 0)
        {
            ds.Add("Hieu nang on dinh cho hoc tap, van phong va giai tri");
            ds.Add("Linh kien chinh hang, toi uu cho nhu cau thuc te");
            ds.Add("De dang nang cap RAM/SSD trong tuong lai");
        }

        string[] duPhong =
        {
            "Hieu nang on dinh cho hoc tap, van phong va giai tri",
            "Linh kien chinh hang, toi uu cho nhu cau thuc te",
            "De dang nang cap RAM/SSD trong tuong lai"
        };

        while (ds.Count < 3)
        {
            ds.Add(duPhong[ds.Count]);
        }

        if (ds.Count > 3)
        {
            ds = ds.Take(3).ToList();
        }

        return ds;
    }

    private void NapSanPhamLienQuan(int maSanPham)
    {
        ProductService db = new ProductService();
        List<Product> ds = db.GetProducts(0);
        List<Product> dsLienQuan = ds.Where(x => x.Id != maSanPham).Take(4).ToList();

        pnlSanPhamLienQuan.Visible = dsLienQuan.Count > 0;
        dlSanPhamLienQuan.DataSource = dsLienQuan;
        dlSanPhamLienQuan.DataBind();
    }

    private DataTable TaoBangThongSo(Product sp)
    {
        DataTable tb = new DataTable();
        tb.Columns.Add("TenThongSo");
        tb.Columns.Add("GiaTri");

        tb.Rows.Add("Thuong hieu", sp.Brand);
        tb.Rows.Add("Bao hanh", "36 thang");
        tb.Rows.Add("Tinh trang", "Moi 100% - nguyen seal");
        tb.Rows.Add("Ket noi", "USB / HDMI / LAN (tuy model)");
        tb.Rows.Add("Kho hang", "Con hang");
        tb.Rows.Add("Gia ban", string.Format("{0:N0} đ", sp.Price));

        return tb;
    }

    private List<string> TaoDanhSachHinh(Product sp)
    {
        List<string> ds = new List<string>();

        if (!string.IsNullOrWhiteSpace(sp.ImageUrl))
        {
            ds.Add(sp.ImageUrl);
            ds.Add(ThemThamSo(sp.ImageUrl, "sig", (sp.Id + 101).ToString()));
            ds.Add(ThemThamSo(sp.ImageUrl, "sig", (sp.Id + 202).ToString()));
        }

        ds = ds.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

        if (ds.Count == 0)
        {
            ds.Add("https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=700&q=60&sig=1");
            ds.Add("https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=700&q=60&sig=2");
            ds.Add("https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=700&q=60&sig=3");
        }

        return ds;
    }

    private string ThemThamSo(string url, string key, string value)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return string.Empty;
        }

        string dauNoi = url.Contains("?") ? "&" : "?";
        return url + dauNoi + key + "=" + value;
    }

    protected void btnThemVaoGio_Click(object sender, EventArgs e)
    {
        ChuyenSangGioHang();
    }

    protected void btnMuaNgay_Click(object sender, EventArgs e)
    {
        ChuyenSangGioHang();
    }

    private void ChuyenSangGioHang()
    {
        int maSanPham;
        if (!int.TryParse(hdnMaSanPham.Value, out maSanPham))
        {
            return;
        }

        int soLuong;
        if (!int.TryParse(ddlSoLuong.SelectedValue, out soLuong))
        {
            soLuong = 1;
        }

        Response.Redirect("Cart.aspx?add=" + maSanPham + "&qty=" + soLuong);
    }

    protected void dlSanPhamLienQuan_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "ThemGioLienQuan")
        {
            int maSanPham;
            if (int.TryParse(Convert.ToString(e.CommandArgument), out maSanPham))
            {
                Response.Redirect("Cart.aspx?add=" + maSanPham + "&qty=1");
            }
        }
    }
}
