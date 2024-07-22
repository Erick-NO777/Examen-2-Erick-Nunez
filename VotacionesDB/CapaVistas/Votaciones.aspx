<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Menu.Master" AutoEventWireup="true" CodeBehind="Votaciones.aspx.cs" Inherits="VotacionesDB.CapaVistas.Votaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Votaciones.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>VOTACIONES</h1>
    <asp:DropDownList ID="ddlCandidatos" runat="server"></asp:DropDownList>
    <asp:Button ID="btnVotar" runat="server" Text="Votar" OnClick="btnVotar_Click" />
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
</asp:Content>
