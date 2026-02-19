<%@ Page Title="Quan ly san pham" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="AdminProducts.aspx.cs" Inherits="QuanLySanPham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="admin-header">
        <h2 class="section-title">Quan ly san pham</h2>
        <asp:Button ID="btnDangXuat" runat="server" Text="Dang xuat" CssClass="btn" OnClick="btnDangXuat_Click" />
    </div>

    <asp:Label ID="lblThongBaoLoi" runat="server" CssClass="msg error" Visible="false"></asp:Label>
    <asp:Label ID="lblThongBaoThanhCong" runat="server" CssClass="msg success" Visible="false"></asp:Label>

    <div class="admin-box">
        <asp:HiddenField ID="hdnMaSanPham" runat="server" />

        <label>Ten san pham</label>
        <asp:TextBox ID="txtTenSanPham" runat="server" CssClass="admin-input" />

        <label>Hang</label>
        <asp:TextBox ID="txtHang" runat="server" CssClass="admin-input" />

        <label>Gia</label>
        <asp:TextBox ID="txtGia" runat="server" CssClass="admin-input" />

        <label>URL hinh anh</label>
        <asp:TextBox ID="txtHinhAnh" runat="server" CssClass="admin-input" />

        <label>Mo ta</label>
        <asp:TextBox ID="txtMoTa" runat="server" CssClass="admin-input" TextMode="MultiLine" Rows="3" />

        <label>Noi bat (3 y, cach nhau bang dau |)</label>
        <asp:TextBox ID="txtNoiBat" runat="server" CssClass="admin-input" TextMode="MultiLine" Rows="2" />

        <div class="admin-actions">
            <asp:Button ID="btnLuu" runat="server" Text="Luu" CssClass="btn" OnClick="btnLuu_Click" />
            <asp:Button ID="btnMoi" runat="server" Text="Moi" CssClass="btn secondary" OnClick="btnMoi_Click" CausesValidation="false" />
        </div>
    </div>

    <asp:GridView ID="gvSanPham" runat="server" AutoGenerateColumns="false" CssClass="cart-table" GridLines="None" OnRowCommand="gvSanPham_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="ProductName" HeaderText="Ten" />
            <asp:BoundField DataField="Brand" HeaderText="Hang" />
            <asp:BoundField DataField="Price" HeaderText="Gia" DataFormatString="{0:N0} đ" />
            <asp:TemplateField HeaderText="Thao tac">
                <ItemTemplate>
                    <asp:LinkButton ID="btnSua" runat="server" CommandName="SuaDong" CommandArgument='<%# Eval("Id") %>'>Sua</asp:LinkButton>
                    |
                    <asp:LinkButton ID="btnXoa" runat="server" CommandName="XoaDong" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Ban chac muon xoa?');">Xoa</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
