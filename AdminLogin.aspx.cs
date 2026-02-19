using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DangNhapQuanTri : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnDangNhap_Click(object sender, EventArgs e)
    {
        string taiKhoan = System.Configuration.ConfigurationManager.AppSettings["AdminUser"] ?? "admin";
        string matKhau = System.Configuration.ConfigurationManager.AppSettings["AdminPass"] ?? "123456";

        if (txtTaiKhoan.Text.Trim().Equals(taiKhoan, StringComparison.OrdinalIgnoreCase)
            && txtMatKhau.Text == matKhau)
        {
            Session["DangNhapQuanTri"] = true;
            Session["TaiKhoanQuanTri"] = txtTaiKhoan.Text.Trim();
            Response.Redirect("AdminProducts.aspx");
            return;
        }

        lblThongBaoLoi.Visible = true;
        lblThongBaoLoi.Text = "Sai tai khoan hoac mat khau.";
    }
}
