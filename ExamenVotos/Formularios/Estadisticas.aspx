<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="ExamenVotos.Formularios.Estadisticas" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estadísticas</title>
    <link href="../CSS/Estadisticas.css" rel="stylesheet" type="text/css" />
</head>
<body>
     <ul>
        <li><a href="Votos.aspx">Votar</a></li>
        <li><a  href="Estadisticas.aspx">Estadísticas</a></li>
        <li><a href="Padron.aspx">Padrón</a></li>
        <li><a href="Candidatos.aspx">Candidatos</a></li>
        <li><a href="Partidos.aspx">Partidos</a></li>
        <li><a href="LogIn.aspx">Salir</a></li>
    </ul>
    <form id="form1" runat="server">
        <div style="padding: 20px;">
            <h2>Estadísticas Generales de las Elecciones</h2>

            <asp:Label ID="lblTotalVotos" runat="server" Text="Total de Votos:"></asp:Label><br />
            <asp:Label ID="lblPorcVotantes" runat="server" Text="Personas que Votaron: "></asp:Label><br />
            <asp:Label ID="lblPorcAbstinencia" runat="server" Text="Porcentaje de Abstinencia: "></asp:Label><br />
            <br />

            <h3>Resultados por Candidato</h3>
            <asp:GridView ID="gvCandidatos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Partido" HeaderText="Partido" />
                    <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje" />
                    <asp:BoundField DataField="Votos" HeaderText="Cantidad de Votos" />
                </Columns>
            </asp:GridView>
            <br />

            <h3>Resultados por Partido</h3>
            <asp:GridView ID="gvPartidos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style">
                <Columns>
                    <asp:BoundField DataField="Partido" HeaderText="Descripción del Partido" />
                    <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje" />
                    <asp:BoundField DataField="Total" HeaderText="Total de Votos" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
