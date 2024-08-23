using ExamenVotos.AccesoDatos;
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
        private static List<PartidosDAL> partidos = new List<PartidosDAL>();

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
            GridCandidatos.DataSource = CandidatosDAL.Obtener(conn);
            GridCandidatos.DataBind();
        }

        protected void LlenarDropdownPartidos()
        {
            partidos = PartidosDAL.Obtener(conn);

            ddlPartidos.DataSource = partidos;
            ddlPartidos.DataTextField = "Descripcion";
            ddlPartidos.DataValueField = "IdPartido";
            ddlPartidos.DataBind();
            ddlPartidos.Items.Insert(0, new ListItem("--Seleccione un partido--", "0"));
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

            CandidatosDAL candidato = new CandidatosDAL(
                txtCedula.Text.Trim(),
                int.Parse(ddlPartidos.SelectedValue)
            );

            int resp = CandidatosDAL.Insertar(candidato, conn);

            if (resp != 0)
            {
                MostrarMensaje("Ocurrió un error al guardar la información. Por favor verifique los datos que se están ingresando");
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
