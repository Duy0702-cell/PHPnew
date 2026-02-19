<%@ Page Title="Chi tiet san pham" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="ChiTietSanPham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlKhongTimThay" runat="server" CssClass="admin-box" Visible="false">
        <p>Khong tim thay san pham ban can xem.</p>
    </asp:Panel>

    <asp:Panel ID="pnlChiTiet" runat="server" Visible="false">
        <asp:HiddenField ID="hdnMaSanPham" runat="server" />

        <p class="product-breadcrumb">
            <a href="Default.aspx">Trang chu</a> / <span>Chi tiet san pham</span>
        </p>

        <div class="product-layout">
            <div class="product-gallery">
                <asp:Image ID="imgSanPhamLon" runat="server" CssClass="main-image" />
                <div class="thumb-list">
                    <asp:Repeater ID="rptHinhPhu" runat="server">
                        <ItemTemplate>
                            <asp:Image ID="imgPhu" runat="server" CssClass="thumb-item" ImageUrl='<%# Container.DataItem %>' />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="product-summary">
                <h2><asp:Label ID="lblTenSanPham" runat="server" /></h2>
                <p class="meta-line">Ma SP: <asp:Label ID="lblMaSanPham" runat="server" /></p>
                <p class="meta-line">Thuong hieu: <strong><asp:Label ID="lblHang" runat="server" /></strong></p>
                <p class="price product-price"><asp:Label ID="lblGia" runat="server" /></p>

                <ul class="highlights">
                    <li><asp:Label ID="lblNoiBat1" runat="server" /></li>
                    <li><asp:Label ID="lblNoiBat2" runat="server" /></li>
                    <li><asp:Label ID="lblNoiBat3" runat="server" /></li>
                </ul>

                <div class="buy-row">
                    <span>So luong</span>
                    <asp:DropDownList ID="ddlSoLuong" runat="server" CssClass="qty-select"></asp:DropDownList>
                </div>

                <div class="buy-actions">
                    <asp:Button ID="btnThemVaoGio" runat="server" Text="Them vao gio hang" CssClass="btn" OnClick="btnThemVaoGio_Click" />
                    <asp:Button ID="btnMuaNgay" runat="server" Text="Mua ngay" CssClass="btn secondary" OnClick="btnMuaNgay_Click" />
                </div>

                <div class="policy-box">
                    <h4>Uu dai va dich vu</h4>
                    <ul>
                        <li>Mien phi giao hang noi thanh cho don tu 500.000d</li>
                        <li>Bao hanh chinh hang, doi moi trong 7 ngay dau</li>
                        <li>Ho tro ky thuat cai dat tu xa</li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="detail-panels">
            <section class="product-panel">
                <h3>Mo ta san pham</h3>
                <p><asp:Label ID="lblMoTa" runat="server" /></p>
            </section>

            <section class="product-panel">
                <h3>Thong so ky thuat</h3>
                <asp:GridView ID="gvThongSo" runat="server" AutoGenerateColumns="false" CssClass="spec-table" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="TenThongSo" HeaderText="Thong so" />
                        <asp:BoundField DataField="GiaTri" HeaderText="Gia tri" />
                    </Columns>
                </asp:GridView>
            </section>
        </div>

        <asp:Panel ID="pnlSanPhamLienQuan" runat="server" CssClass="product-panel" Visible="false">
            <h3>San pham lien quan</h3>
            <asp:DataList ID="dlSanPhamLienQuan" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" CellPadding="6" OnItemCommand="dlSanPhamLienQuan_ItemCommand">
                <ItemTemplate>
                    <article class="card">
                        <asp:Image ID="imgLienQuan" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' AlternateText='<%# Eval("Name") %>' />
                        <h3>
                            <asp:HyperLink ID="lnkTenLienQuan" runat="server" NavigateUrl='<%# Eval("Id", "Product.aspx?id={0}") %>' Text='<%# Eval("Name") %>'></asp:HyperLink>
                        </h3>
                        <p class="price"><%# string.Format("{0:N0}", Eval("Price")) %> đ</p>
                        <asp:LinkButton ID="btnThemGioLienQuan" runat="server" CssClass="btn small" CommandName="ThemGioLienQuan" CommandArgument='<%# Eval("Id") %>'>Them gio hang</asp:LinkButton>
                    </article>
                </ItemTemplate>
            </asp:DataList>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
