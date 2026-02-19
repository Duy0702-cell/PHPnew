using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemoAsp;

public partial class TrangChu : System.Web.UI.Page
{
    private const int SO_SAN_PHAM_MOI_TRANG = 12;

    private int TrangHienTai
    {
        get
        {
            object o = ViewState["TrangHienTai"];
            if (o == null)
            {
                return 0;
            }
            return (int)o;
        }
        set
        {
            ViewState["TrangHienTai"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TrangHienTai = 0;
            NapDanhSachSanPham();
        }
    }

    private void NapDanhSachSanPham()
    {
        ProductService db = new ProductService();
        DataTable tbSanPham = db.GetProductsTable(0);

        PagedDataSource nguon = new PagedDataSource();
        nguon.DataSource = tbSanPham.DefaultView;
        nguon.AllowPaging = true;
        nguon.PageSize = SO_SAN_PHAM_MOI_TRANG;

        if (nguon.PageCount == 0)
        {
            dlSanPham.DataSource = null;
            dlSanPham.DataBind();
            pnlPhanTrang.Visible = false;
            return;
        }

        if (TrangHienTai < 0)
        {
            TrangHienTai = 0;
        }
        if (TrangHienTai >= nguon.PageCount)
        {
            TrangHienTai = nguon.PageCount - 1;
        }

        nguon.CurrentPageIndex = TrangHienTai;

        dlSanPham.DataSource = nguon;
        dlSanPham.DataBind();

        pnlPhanTrang.Visible = nguon.PageCount > 1;
        btnTrangTruoc.Enabled = !nguon.IsFirstPage;
        btnTrangSau.Enabled = !nguon.IsLastPage;
        lblThongTinTrang.Text = "Trang " + (TrangHienTai + 1) + " / " + nguon.PageCount;
    }

    protected void btnTrangTruoc_Click(object sender, EventArgs e)
    {
        TrangHienTai -= 1;
        NapDanhSachSanPham();
    }

    protected void btnTrangSau_Click(object sender, EventArgs e)
    {
        TrangHienTai += 1;
        NapDanhSachSanPham();
    }

    protected void dlSanPham_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "ThemGio")
        {
            int maSanPham;
            if (int.TryParse(Convert.ToString(e.CommandArgument), out maSanPham))
            {
                ProductService db = new ProductService();
                Product sp = db.GetProductById(maSanPham);
                if (sp != null)
                {
                    ThemVaoGioHang(sp);
                }
            }
            Response.Redirect("Cart.aspx");
        }
    }

    private void ThemVaoGioHang(Product sp)
    {
        List<CartItem> gioHang = Session["CART"] as List<CartItem>;
        if (gioHang == null)
        {
            gioHang = new List<CartItem>();
        }

        CartItem item = gioHang.Find(x => x.Id == sp.Id);
        if (item != null)
        {
            item.Quantity += 1;
        }
        else
        {
            gioHang.Add(new CartItem
            {
                Id = sp.Id,
                Name = sp.Name,
                Brand = sp.Brand,
                Price = sp.Price,
                ImageUrl = sp.ImageUrl,
                Quantity = 1
            });
        }

        Session["CART"] = gioHang;
    }
}
