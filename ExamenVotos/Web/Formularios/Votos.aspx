<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Votos.aspx.cs" Inherits="ExamenVotos.Formularios.Votos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="../css/Votos.css" rel="stylesheet" />
    <title>Gestión de Votos</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridCandidatos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" OnRowCommand="AccionesGrid">
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
