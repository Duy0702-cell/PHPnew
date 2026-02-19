using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemoAsp;

public partial class QuanLySanPham : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Session["DangNhapQuanTri"] is bool) || !(bool)Session["DangNhapQuanTri"])
        {
            Response.Redirect("AdminLogin.aspx");
            return;
        }

        if (!IsPostBack)
        {
            NapDanhSachSanPham();
        }
    }

    private void NapDanhSachSanPham()
    {
        ProductService db = new ProductService();
        gvSanPham.DataSource = db.GetProductsTable(0);
        gvSanPham.DataBind();
    }

    protected void btnLuu_Click(object sender, EventArgs e)
    {
        lblThongBaoLoi.Visible = false;
        lblThongBaoThanhCong.Visible = false;

        if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
        {
            lblThongBaoLoi.Visible = true;
            lblThongBaoLoi.Text = "Ten san pham khong duoc de trong.";
            return;
        }

        decimal gia;
        if (!decimal.TryParse(txtGia.Text.Trim(), out gia))
        {
            lblThongBaoLoi.Visible = true;
            lblThongBaoLoi.Text = "Gia khong hop le.";
            return;
        }

        Product sp = new Product();
        sp.Name = txtTenSanPham.Text.Trim();
        sp.Brand = txtHang.Text.Trim();
        sp.Price = gia;
        sp.ImageUrl = txtHinhAnh.Text.Trim();
        sp.Description = txtMoTa.Text.Trim();
        sp.NoiBat = txtNoiBat.Text.Trim();

        ProductService db = new ProductService();
        string loi;

        if (string.IsNullOrEmpty(hdnMaSanPham.Value))
        {
            if (db.AddProduct(sp, out loi))
            {
                lblThongBaoThanhCong.Visible = true;
                lblThongBaoThanhCong.Text = "Them san pham thanh cong.";
                XoaForm();
            }
            else
            {
                lblThongBaoLoi.Visible = true;
                lblThongBaoLoi.Text = "Them that bai: " + loi;
            }
        }
        else
        {
            int maSanPham;
            if (!int.TryParse(hdnMaSanPham.Value, out maSanPham))
            {
                lblThongBaoLoi.Visible = true;
                lblThongBaoLoi.Text = "ID khong hop le.";
                return;
            }

            sp.Id = maSanPham;
            if (db.UpdateProduct(sp, out loi))
            {
                lblThongBaoThanhCong.Visible = true;
                lblThongBaoThanhCong.Text = "Cap nhat san pham thanh cong.";
                XoaForm();
            }
            else
            {
                lblThongBaoLoi.Visible = true;
                lblThongBaoLoi.Text = "Cap nhat that bai: " + loi;
            }
        }

        NapDanhSachSanPham();
    }

    protected void gvSanPham_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int maSanPham;
        if (!int.TryParse(Convert.ToString(e.CommandArgument), out maSanPham))
        {
            return;
        }

        ProductService db = new ProductService();

        if (e.CommandName == "SuaDong")
        {
            Product sp = db.GetProductById(maSanPham);
            if (sp != null)
            {
                hdnMaSanPham.Value = sp.Id.ToString();
                txtTenSanPham.Text = sp.Name;
                txtHang.Text = sp.Brand;
                txtGia.Text = sp.Price.ToString("0.##");
                txtHinhAnh.Text = sp.ImageUrl;
                txtMoTa.Text = sp.Description;
                txtNoiBat.Text = sp.NoiBat;
            }
            return;
        }

        if (e.CommandName == "XoaDong")
        {
            string loi;
            if (db.DeleteProduct(maSanPham, out loi))
            {
                lblThongBaoThanhCong.Visible = true;
                lblThongBaoThanhCong.Text = "Xoa san pham thanh cong.";
                XoaForm();
            }
            else
            {
                lblThongBaoLoi.Visible = true;
                lblThongBaoLoi.Text = "Xoa that bai: " + loi;
            }

            NapDanhSachSanPham();
        }
    }

    protected void btnMoi_Click(object sender, EventArgs e)
    {
        XoaForm();
        lblThongBaoLoi.Visible = false;
        lblThongBaoThanhCong.Visible = false;
    }

    protected void btnDangXuat_Click(object sender, EventArgs e)
    {
        Session.Remove("DangNhapQuanTri");
        Session.Remove("TaiKhoanQuanTri");
        Response.Redirect("AdminLogin.aspx");
    }

    private void XoaForm()
    {
        hdnMaSanPham.Value = "";
        txtTenSanPham.Text = "";
        txtHang.Text = "";
        txtGia.Text = "";
        txtHinhAnh.Text = "";
        txtMoTa.Text = "";
        txtNoiBat.Text = "";
    }
}
