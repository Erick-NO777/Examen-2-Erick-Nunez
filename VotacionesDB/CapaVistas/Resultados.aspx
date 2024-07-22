<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Menu.Master" AutoEventWireup="true" CodeBehind="Resultados.aspx.cs" Inherits="VotacionesDB.CapaVistas.Resultados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Resultados.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>RESULTADOS DE LA ELECCION</h1>
    <p>Ganador de la Elección:</p>
    <p id="ganador"><asp:Label ID="lganador" runat="server" /></p>
    <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Candidato" />
            <asp:BoundField DataField="Partido" HeaderText="Partido" />
            <asp:BoundField DataField="Votos" HeaderText="Votos Totales" />
            <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje" DataFormatString="{0:F2}%" />
        </Columns>
    </asp:GridView>
</asp:Content>
