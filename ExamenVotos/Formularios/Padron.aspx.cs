using ExamenVotos.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenVotos.Formularios
{
    public partial class Padron : System.Web.UI.Page
    {
        private static string conn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();
            }
        }

        protected void LlenarGrid()
        {
            GridPadron.DataSource = PadronDAL.Obtener(conn);
            GridPadron.DataBind();
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            #region Validaciones
            if (string.IsNullOrEmpty(txtCedula.Text) || string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtApellido1.Text) || string.IsNullOrEmpty(txtEdad.Text))
            {
                MostrarMensaje("Todos los campos son obligatorios.");
                return;
            }

            if (!int.TryParse(txtEdad.Text, out int edad) || edad < 18)
            {
                MostrarMensaje("Edad debe ser un número entero mayor o igual a 18.");
                return;
            }
            #endregion

            PadronDAL padron = new PadronDAL(
                txtCedula.Text.Trim(),
                txtNombre.Text.Trim(),
                txtApellido1.Text.Trim(),
                txtApellido2.Text.Trim(),
                edad
            );

            int resp = PadronDAL.Insertar(padron, conn);

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
            txtCedula.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido1.Text = string.Empty;
            txtApellido2.Text = string.Empty;
            txtEdad.Text = string.Empty;
        }


        public void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{mensaje}');", true);
        }

    }
}
