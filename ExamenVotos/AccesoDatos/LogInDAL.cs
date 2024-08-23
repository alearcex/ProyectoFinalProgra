using System;
using System.Data.SqlClient;
using System.Data;

namespace ExamenVotos.AccesoDatos
{
    public class LogInDAL
    {
        public string Cedula { get; set; }
        public string Contra { get; set; }

        public LogInDAL(string cedula, string contra)
        {
            Cedula = cedula;
            Contra = contra;
        }
        public static int ValidarIngreso(LogInDAL datos, string connectionString)
        {
            int response = 0;
            string query = "SELECT COUNT(*) FROM Usuarios " +
                           "WHERE Cedula = @Cedula AND Contraseña = @Contra";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Cedula", datos.Cedula);
                        cmd.Parameters.AddWithValue("@Contra", datos.Contra);

                        connection.Open();
                        response = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception)
            {
                response = -1;
            }

            return response;
        }

    }
}
