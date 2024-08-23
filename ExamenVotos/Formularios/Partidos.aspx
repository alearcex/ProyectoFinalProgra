<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Partidos.aspx.cs" Inherits="ExamenVotos.Formularios.Partidos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestión de Partidos</title>
    <link href="../CSS/Partidos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ul>
        <li><a href="Votos.aspx">Votar</a></li>
        <li><a href="Estadisticas.aspx">Estadísticas</a></li>
        <li><a href="Padron.aspx">Padrón</a></li>
        <li><a href="Candidatos.aspx">Candidatos</a></li>
        <li><a class="active" href="Partidos.aspx">Partidos</a></li>
        <li><a href="LogIn.aspx">Salir</a></li>
    </ul>
    <form id="form1" runat="server">
        <h1>Gestión de Partidos</h1>
        <br />
        <div class="form-group">
            <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" CssClass="labels"></asp:Label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="texto"></asp:TextBox>
        </div>
        <br />
        <div class="group-btn">
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="guardar" OnClick="btnLimpiar_Click" />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="guardar" />
        </div>
        <br />
        <asp:GridView ID="GridPartidos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style">
            <Columns>
                <asp:BoundField DataField="IdPartido" HeaderText="ID" ReadOnly="True" SortExpression="IdPartido" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
