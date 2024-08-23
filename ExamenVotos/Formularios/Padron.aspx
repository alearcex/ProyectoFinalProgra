<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Padron.aspx.cs" Inherits="ExamenVotos.Formularios.Padron" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestión de Padrón</title>
    <link href="../CSS/Padron.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ul>
        <li><a href="Votos.aspx">Votar</a></li>
        <li><a href="Estadisticas.aspx">Estadísticas</a></li>
        <li><a class="active" href="Padron.aspx">Padrón</a></li>
        <li><a href="Candidatos.aspx">Candidatos</a></li>
        <li><a href="Partidos.aspx">Partidos</a></li>
        <li><a href="LogIn.aspx">Salir</a></li>
    </ul>
    <form id="form1" runat="server">
        <h1>Gestión de Padrón</h1>
        <br />
        <div class="form-group">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="labels"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="texto"></asp:TextBox>
            <asp:Label ID="lblCedula" runat="server" Text="Cédula" CssClass="labels"></asp:Label>
            <asp:TextBox ID="txtCedula" runat="server" CssClass="texto"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <asp:Label ID="lblApellido1" runat="server" Text="Primer Apellido" CssClass="labels"></asp:Label>
            <asp:TextBox ID="txtApellido1" runat="server" CssClass="texto"></asp:TextBox>
            <asp:Label ID="lblEdad" runat="server" Text="Edad" CssClass="labels"></asp:Label>
            <asp:TextBox ID="txtEdad" runat="server" CssClass="texto"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <asp:Label ID="lblApellido2" runat="server" Text="Segundo Apellido" CssClass="labels"></asp:Label>
            <asp:TextBox ID="txtApellido2" runat="server" CssClass="texto"></asp:TextBox>
        </div>
        <div class="group-btn">
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="guardar" OnClick="btnLimpiar_Click" />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="guardar" />
        </div>
        <br />
        <asp:GridView ID="GridPadron" runat="server" AutoGenerateColumns="False" CssClass="gridview-style">
            <Columns>
                <asp:BoundField DataField="Cedula" HeaderText="Cédula" ReadOnly="True" SortExpression="Cedula" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" SortExpression="Nombre" />
                <asp:BoundField DataField="PrimerApellido" HeaderText="Primer Apellido" ReadOnly="True" SortExpression="PrimerApellido" />
                <asp:BoundField DataField="SegundoApellido" HeaderText="Segundo Apellido" ReadOnly="True" SortExpression="SegundoApellido" />
                <asp:BoundField DataField="Edad" HeaderText="Edad" ReadOnly="True" SortExpression="Edad" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
