 using ExamenVotos.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenVotos.Formularios
{
    public partial class Partidos : System.Web.UI.Page
    {
        private static string conn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        private static List<PartidosDAL> partidos = new List<PartidosDAL>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();
            }
        }

        protected void LlenarGrid()
        {
            GridPartidos.DataSource = PartidosDAL.Obtener(conn);
            GridPartidos.DataBind();
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MostrarMensaje("El campo descripción no puede estar vacío");
                return;
            }

            PartidosDAL partido = new PartidosDAL(txtDescripcion.Text.Trim());

            int resp = PartidosDAL.Insertar(partido, conn);

            if (resp != 0)
            {
                MostrarMensaje("Ocurrió un error al guardar la información.");
            }
            else
            {
                LlenarGrid();
                Limpiar();
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void Limpiar()
        {
            txtDescripcion.Text = string.Empty;
        }


        public void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{mensaje}');", true);
        }
    }
}
