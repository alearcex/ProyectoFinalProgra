using ExamenVotos.Business;
using ExamenVotos.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenVotos.Formularios
{
    public partial class Votos : System.Web.UI.Page
    {
        private static string conn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGridCandidatos();
            }
        }

        protected void LlenarGridCandidatos()
        {
            GridCandidatos.DataSource = VotosCore.ConsultaCandidatos(conn);
            GridCandidatos.DataBind();
        }

        protected void AccionesGrid(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VOTAR")
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridCandidatos.Rows[indice];

                string cedula = Session["CedulaUsuario"].ToString();
                int idCandidato = Convert.ToInt32(fila.Cells[0].Text);
                DateTime fecha = DateTime.Now;

                VotosDAO voto = new VotosDAO(cedula, idCandidato, fecha);
                int resp = VotosCore.GuardarVoto(voto, conn);

                if (resp != 0)
                {
                    MostrarMensaje("Ocurrió un error al registrar el voto.");
                }
                else
                {
                    MostrarMensaje("El voto se registró correctamente.");
                }
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{mensaje}');", true);
        }
    }
}
