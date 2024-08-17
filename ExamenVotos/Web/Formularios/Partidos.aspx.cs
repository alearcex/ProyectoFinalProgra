using ExamenVotos.Business;
using ExamenVotos.DAL;
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
        private static List<PartidosDAO> partidos = new List<PartidosDAO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();
            }
        }

        protected void LlenarGrid()
        {
            GridPartidos.DataSource = PartidoCore.ObtenerPartidos(conn);
            GridPartidos.DataBind();
        }

        protected void AccionesGrid(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridPartidos.Rows[indice];

                txtIdPartido.Text = fila.Cells[0].Text;
                txtDescripcion.Text = fila.Cells[1].Text;
            }
            else if (e.CommandName == "ELIMINAR")   
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridPartidos.Rows[indice];

                PartidosDAO partido = new PartidosDAO(Convert.ToInt32(fila.Cells[0].Text));

                int resp = PartidoCore.EjecutarStoredProcedure("ELIMINAR", partido, conn);

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
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MostrarMensaje("El campo descripción no puede estar vacío");
                return;
            }

            PartidosDAO partido = new PartidosDAO(int.Parse(txtIdPartido.Text), txtDescripcion.Text.Trim());

            int resp = PartidoCore.EjecutarStoredProcedure("GUARDAR", partido, conn);

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
            txtIdPartido.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }


        public void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{mensaje}');", true);
        }
    }
}
