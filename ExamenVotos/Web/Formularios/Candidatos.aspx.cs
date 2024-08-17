using ExamenVotos.Business;
using ExamenVotos.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenVotos.Formularios
{
    public partial class Candidatos : System.Web.UI.Page
    {
        private static string conn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        private static List<PartidosDAO> partidos = new List<PartidosDAO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();
                LlenarDropdownPartidos();
            }
        }

        protected void LlenarGrid()
        {
            GridCandidatos.DataSource = CandidatosCore.ObtenerCandidatos(conn);
            GridCandidatos.DataBind();
        }

        protected void LlenarDropdownPartidos()
        {
            partidos = PartidoCore.ObtenerPartidos(conn);

            ddlPartidos.DataSource = partidos;
            ddlPartidos.DataTextField = "Descripcion";
            ddlPartidos.DataValueField = "IdPartido";
            ddlPartidos.DataBind();
            ddlPartidos.Items.Insert(0, new ListItem("--Seleccione un partido--", "0"));
        }

        protected void AccionesGrid(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridCandidatos.Rows[indice];

                txtCedula.Text = fila.Cells[1].Text;
                ddlPartidos.SelectedValue = fila.Cells[3].Text;
            }
            else if (e.CommandName == "ELIMINAR")
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridCandidatos.Rows[indice];

                CandidatosDAO candidato = new CandidatosDAO(Convert.ToInt32(fila.Cells[0].Text));

                int resp = CandidatosCore.EjecutarStoredProcedure("ELIMINAR", candidato, conn);

                if (resp != 0)
                {
                    MostrarMensaje("Ocurrió un error al eliminar el registro.");
                }
                else
                {
                    MostrarMensaje("La información se eliminó correctamente.");
                    LlenarGrid();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            #region Validaciones
            if (string.IsNullOrEmpty(txtCedula.Text))
            {
                MostrarMensaje("El campo cédula no puede estar vacío");
                return;
            }

            if (ddlPartidos.SelectedValue == "0")
            {
                MostrarMensaje("Debe seleccionar un partido.");
                return;
            }
            #endregion

            CandidatosDAO candidato = new CandidatosDAO(
                txtCedula.Text.Trim(),
                int.Parse(ddlPartidos.SelectedValue)
            );

            int resp = CandidatosCore.EjecutarStoredProcedure("GUARDAR", candidato, conn);

            if (resp != 0)
            {
                MostrarMensaje("Ocurrió un error al guardar la información.");
            }
            else
            {
                LlenarGrid();
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void Limpiar()
        {
            txtCedula.Text = string.Empty;
            ddlPartidos.SelectedValue = "0";
        }

        public void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{mensaje}');", true);
        }
    }
}
