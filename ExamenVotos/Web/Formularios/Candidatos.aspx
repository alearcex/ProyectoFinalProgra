<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Candidatos.aspx.cs" Inherits="ExamenVotos.Formularios.Candidatos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestión de Candidatos</title>
    <link href="../CSS/Candidatos.css" rel="stylesheet" type="text/css" />
</head>
<body>
     <ul>
        <li><a href="Padron.aspx">Padrón</a></li>
        <li><a class="active" href="Candidatos.aspx">Candidatos</a></li>
        <li><a href="Partidos.aspx">Partidos</a></li>
        <li><a href="LogIn.aspx">Salir</a></li>
    </ul>
    <form id="form1" runat="server">
        <h1>Gestión de Candidatos</h1>
        <br />
        <div class="form-group">
            <asp:Label ID="lblCedula" runat="server" Text="Cédula" CssClass="labels"></asp:Label>
            <asp:TextBox ID="txtCedula" runat="server" CssClass="texto"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <asp:Label ID="lblPartido" runat="server" Text="Partido" CssClass="labels"></asp:Label>
            <asp:DropDownList ID="ddlPartidos" runat="server" CssClass="dropdown"></asp:DropDownList>
        </div>
        <br />
        <div class="group-btn">
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="guardar" OnClick="btnLimpiar_Click" />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="guardar" />
        </div>
        <br />
        <asp:GridView ID="GridCandidatos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" OnRowCommand="AccionesGrid">
            <Columns>
                <asp:BoundField DataField="IdCandidato" HeaderText="ID" ReadOnly="True" SortExpression="IdCandidato" />
                <asp:BoundField DataField="Cedula" HeaderText="Cédula" ReadOnly="True" SortExpression="Cedula" />
                <asp:BoundField DataField="Descripcion" HeaderText="Partido" ReadOnly="True" SortExpression="DescripcionPartido" />
                <asp:BoundField DataField="IdPartido">
                    <ItemStyle CssClass="ocultar-columna" />
                    <ControlStyle CssClass="ocultar-columna" />
                    <HeaderStyle CssClass="ocultar-columna" />
                </asp:BoundField>
                <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="EDITAR" ControlStyle-CssClass="editar">
                    <ControlStyle CssClass="editar"></ControlStyle>
                </asp:ButtonField>
                <asp:ButtonField ButtonType="Button" Text="Eliminar" CommandName="ELIMINAR" ControlStyle-CssClass="eliminar">
                    <ControlStyle CssClass="eliminar"></ControlStyle>
                </asp:ButtonField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
