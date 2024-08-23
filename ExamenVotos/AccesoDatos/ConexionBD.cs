using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamenVotos.Modelo
{
    public class ConexionBD
    {
        public static SqlConnection obtenerConexion()
        {
            string rutaconexion = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection conexion = new SqlConnection(rutaconexion);
            conexion.Open();
            return conexion;
        }
    }
}