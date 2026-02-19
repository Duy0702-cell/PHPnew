<%@ Page Title="Giỏ hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="GioHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title">Giỏ hàng của bạn</h2>

    <asp:Label ID="lblGioRong" runat="server" Text="Chưa có sản phẩm nào." Visible="false"></asp:Label>

    <asp:GridView ID="gvGioHang" runat="server" AutoGenerateColumns="false" CssClass="cart-table" GridLines="None" OnRowCommand="gvGioHang_RowCommand">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Tên sản phẩm" />
            <asp:BoundField DataField="Brand" HeaderText="Hãng" />
            <asp:BoundField DataField="Price" HeaderText="Giá" DataFormatString="{0:N0} đ" />
            <asp:BoundField DataField="Quantity" HeaderText="SL" />
            <asp:TemplateField HeaderText="Thao tác">
                <ItemTemplate>
                    <asp:LinkButton ID="btnXoa" runat="server" CommandName="Xoa" CommandArgument='<%# Eval("Id") %>'>Xóa</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <p class="total">Tổng tiền: <strong><asp:Label ID="lblTongTien" runat="server" /></strong></p>
</asp:Content>
