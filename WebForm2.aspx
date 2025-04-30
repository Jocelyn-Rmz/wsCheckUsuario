<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="wsCheckUsuario.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="App_Themes/principal/principal.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="Reporte de Usuarios Registrados" cssClass="tituloContenido"></asp:Label>
    <br /><br />
    <asp:TextBox ID="TextBox1" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" runat="server"></asp:TextBox>
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/icon_logalum.GIF" />
    <br /><br />
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="5">
    <AlternatingRowStyle BackColor="#CCFFFF" Font-Names="Arial" Font-Size="Small" ForeColor="Black" />
    <HeaderStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="Medium" ForeColor="White" />
    <PagerStyle BackColor="#000099" Font-Names="Arial" Font-Size="Medium" ForeColor="White" />
    <RowStyle BackColor="#6699FF" Font-Names="Arial" Font-Size="Small" ForeColor="Black" />
    </asp:GridView>
</asp:Content>



