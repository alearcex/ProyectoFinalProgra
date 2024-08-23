<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Votos.aspx.cs" Inherits="ExamenVotos.Formularios.Votos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/Votos.css" rel="stylesheet" />
    <title>Votos</title>
</head>
<body>
    <ul>
        <li><a class="active" href="Votos.aspx">Votar</a></li>
        <li><a href="Estadisticas.aspx">Estadísticas</a></li>
        <li><a href="Padron.aspx">Padrón</a></li>
        <li><a href="Candidatos.aspx">Candidatos</a></li>
        <li><a href="Partidos.aspx">Partidos</a></li>
        <li><a href="LogIn.aspx">Salir</a></li>
    </ul>
    <form id="form1" runat="server">
        <asp:GridView ID="GridCandidatos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" OnRowCommand="GuardarVoto">
            <Columns>
                <asp:BoundField DataField="IdCandidato" HeaderText="ID Candidato" />
                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                <asp:BoundField DataField="Partido" HeaderText="Partido" />
                <asp:ButtonField ButtonType="Button" CommandName="VOTAR" Text="Votar" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
