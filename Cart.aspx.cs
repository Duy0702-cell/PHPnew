using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemoAsp;

public partial class GioHang : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            XuLyThemTuQuery();
            NapGioHang();
        }
    }

    private void XuLyThemTuQuery()
    {
        int maSanPhamThem;
        if (!int.TryParse(Request.QueryString["add"], out maSanPhamThem))
        {
            return;
        }

        int soLuongThem;
        if (!int.TryParse(Request.QueryString["qty"], out soLuongThem))
        {
            soLuongThem = 1;
        }
        if (soLuongThem < 1)
        {
            soLuongThem = 1;
        }
        if (soLuongThem > 20)
        {
            soLuongThem = 20;
        }

        ProductService db = new ProductService();
        Product sp = db.GetProductById(maSanPhamThem);
        if (sp != null)
        {
            List<CartItem> gioHang = LayGioHang();
            CartItem item = gioHang.Find(x => x.Id == sp.Id);
            if (item != null)
            {
                item.Quantity += soLuongThem;
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
                    Quantity = soLuongThem
                });
            }
            Session["CART"] = gioHang;
        }

        Response.Redirect("Cart.aspx");
    }

    private void NapGioHang()
    {
        List<CartItem> gioHang = LayGioHang();
        gvGioHang.DataSource = gioHang;
        gvGioHang.DataBind();

        lblGioRong.Visible = gioHang.Count == 0;
        lblTongTien.Text = string.Format("{0:N0} đ", gioHang.Sum(x => x.Price * x.Quantity));
    }

    private List<CartItem> LayGioHang()
    {
        List<CartItem> gioHang = Session["CART"] as List<CartItem>;
        if (gioHang == null)
        {
            gioHang = new List<CartItem>();
            Session["CART"] = gioHang;
        }
        return gioHang;
    }

    protected void gvGioHang_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Xoa")
        {
            int maSanPham;
            if (int.TryParse(Convert.ToString(e.CommandArgument), out maSanPham))
            {
                List<CartItem> gioHang = LayGioHang();
                CartItem item = gioHang.Find(x => x.Id == maSanPham);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                Session["CART"] = gioHang;
                NapGioHang();
            }
        }
    }
}
