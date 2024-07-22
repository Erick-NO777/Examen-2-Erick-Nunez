<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Menu.Master" AutoEventWireup="true" CodeBehind="Candidatos.aspx.cs" Inherits="VotacionesDB.CapaVistas.Candidatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Candidatos.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>INGRESO DE CANDIDATOS</h1>
    <p>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </p>
    <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblPartido" runat="server" Text="Partido:"></asp:Label>
    <asp:TextBox ID="txtPartido" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblPropuesta" runat="server" Text="Propuesta:"></asp:Label>
    <asp:TextBox ID="txtPropuesta" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
</asp:Content>
