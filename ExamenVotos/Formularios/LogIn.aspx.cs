using ExamenVotos.AccesoDatos;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenVotos.Formularios
{
    public partial class LogIn : System.Web.UI.Page
    {
        private static string conn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            #region Validaciones
            if (string.IsNullOrEmpty(txtCedula.Text) || string.IsNullOrEmpty(txtPassword.Text))            
            {
                MostrarMensaje("Todos los campos son obligatorios.");
                return;
            }
            #endregion

            LogInDAL datos = new LogInDAL(txtCedula.Text, txtPassword.Text);

            var resultado = LogInDAL.ValidarIngreso(datos, conn);

            switch (resultado)
            {
                case 0:
                    MostrarMensaje("Cédula o contraseña incorrectos.");
                    break;
                case -1:
                    MostrarMensaje("Ha ocurrido un error al conectar con la base de datos.");
                    break;
                default:
                        Session["CedulaUsuario"] = txtCedula.Text;
                        Response.Redirect("Votos.aspx");
                    break;
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{mensaje}');", true);
        }

    }
}