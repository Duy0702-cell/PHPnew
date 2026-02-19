<%@ Page Title="Dang nhap quan tri" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="DangNhapQuanTri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="admin-box">
        <h2 class="section-title">Dang nhap quan tri</h2>
        <asp:Label ID="lblThongBaoLoi" runat="server" CssClass="msg error" Visible="false"></asp:Label>

        <label>Tai khoan</label>
        <asp:TextBox ID="txtTaiKhoan" runat="server" CssClass="admin-input" />

        <label>Mat khau</label>
        <asp:TextBox ID="txtMatKhau" runat="server" TextMode="Password" CssClass="admin-input" />

        <asp:Button ID="btnDangNhap" runat="server" Text="Dang nhap" CssClass="btn" OnClick="btnDangNhap_Click" />
        <p class="hint">Mac dinh: admin / 123456 (doi trong Web.config)</p>
    </div>
</asp:Content>
