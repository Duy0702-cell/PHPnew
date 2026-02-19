<%@ Page Title="Trang chủ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="TrangChu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title">Sản phẩm nổi bật</h2>

    <asp:DataList ID="dlSanPham" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" CellPadding="6" OnItemCommand="dlSanPham_ItemCommand">
        <ItemTemplate>
            <article class="card">
                <asp:Image ID="imgSanPham" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' AlternateText='<%# Eval("ProductName") %>' />
                <h3>
                    <asp:HyperLink ID="lnkTenSP" runat="server" NavigateUrl='<%# Eval("Id", "Product.aspx?id={0}") %>' Text='<%# Eval("ProductName") %>'></asp:HyperLink>
                </h3>
                <p class="price"><%# string.Format("{0:N0}", Eval("Price")) %> đ</p>
                <p class="meta"><%# Eval("Brand") %></p>
                <asp:LinkButton ID="btnThemGio" runat="server" CssClass="btn small" CommandName="ThemGio" CommandArgument='<%# Eval("Id") %>'>Thêm giỏ hàng</asp:LinkButton>
            </article>
        </ItemTemplate>
    </asp:DataList>

    <asp:Panel ID="pnlPhanTrang" runat="server" CssClass="pager" Visible="false">
        <asp:Button ID="btnTrangTruoc" runat="server" Text="Trang trước" CssClass="btn secondary" OnClick="btnTrangTruoc_Click" />
        <asp:Label ID="lblThongTinTrang" runat="server" CssClass="info" />
        <asp:Button ID="btnTrangSau" runat="server" Text="Trang sau" CssClass="btn secondary" OnClick="btnTrangSau_Click" />
    </asp:Panel>
</asp:Content>
