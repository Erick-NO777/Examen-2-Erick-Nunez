<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Menu.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="VotacionesDB.CapaVistas.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Inicio.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>PAGINA DE INICIO</h1>
    <asp:Label ID="lbienvenido" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <img src="../imagenes/Vote.jpg" alt="Vote">
</asp:Content>
