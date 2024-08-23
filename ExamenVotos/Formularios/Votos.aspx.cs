using ExamenVotos.AccesoDatos;
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
            GridCandidatos.DataSource = VotosDAL.ConsultaCandidatos(conn);
            GridCandidatos.DataBind();
        }

        protected void GuardarVoto(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VOTAR")
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridCandidatos.Rows[indice];

                string cedula = Session["CedulaUsuario"].ToString();
                int idCandidato = Convert.ToInt32(fila.Cells[0].Text);
                DateTime fecha = DateTime.Now;

                VotosDAL voto = new VotosDAL(cedula, idCandidato, fecha);
                int existeVoto = VotosDAL.ValidarVoto(cedula, conn);

                if (existeVoto == 0) 
                {
                    int resp = VotosDAL.GuardarVoto(voto, conn);

                    if (resp == -1)
                    {
                        MostrarMensaje("Ocurrió un error al registrar el voto.");
                    }
                    else
                    {
                        MostrarMensaje("El voto se registró correctamente.");
                    }
                }
                else if(existeVoto > 0)
                {
                    MostrarMensaje("Ya hay un voto registrado con esta cédula.");
                }
                else
                {
                    MostrarMensaje("Ocurrió un error al registrar el voto.");
                }
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{mensaje}');", true);
        }
    }
}
