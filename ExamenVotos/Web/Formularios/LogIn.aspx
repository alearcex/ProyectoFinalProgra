<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="ExamenVotos.Formularios.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../css/LogIn.css" rel="stylesheet" />
    <title>Examen 2 | Ingreso</title>
</head>
<body>
    <form id="form1" runat="server">        
        <h3>Bienvenido al sistema de votos</h3>
        <br />
        <asp:Label ID="lblCedula" runat="server" Text="Cédula"></asp:Label>
        <asp:TextBox ID="txtCedula" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblPassword" runat="server" Text="Contraseña"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="btnIngresar" runat="server" OnClick="btnIngresar_Click" Text="Ingresar" />
    </form>
</body>
</html>
