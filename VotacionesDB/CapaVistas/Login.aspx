<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VotacionesDB.CapaVistas.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="../css/Logincss.css" rel="stylesheet" />
</head>
<body>
    <section class="container">
        <div class="login-container">
            <div class="circle circle-one"></div>
            <div class="form-container">
                <img src="https://raw.githubusercontent.com/hicodersofficial/glassmorphism-login-form/master/assets/illustration.png" alt="illustration" class="illustration" />
                <h1 class="opacity">LOGIN</h1>
                <form id="form1" runat="server" >
                    <asp:TextBox ID="tnombre" runat="server" placeholder="NOMBRE"></asp:TextBox>
                    <asp:TextBox ID="tcedula" type ="Password" runat="server" placeholder="CEDULA"></asp:TextBox>
                    <asp:Button ID="bingresar" runat="server" Text="INGRESAR" OnClick="bingresar_Click"/>
                    <asp:Label ID="lerror" runat="server" Text=""></asp:Label>
                </form>
            </div>
            <div class="circle circle-two"></div>
        </div>
        <div class="theme-btn-container"></div>
    </section>
    <script src="../java/Theme.js"></script>
</body>
</html>
