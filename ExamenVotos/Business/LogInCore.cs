using ExamenVotos.DAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ExamenVotos.Business
{
    public class LogInCore
    {
        public static int ValidarIngreso(LogInDAO datos, string connectionString)
        {
            int response = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ValidarUsuarioSP", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@cedula", datos.Cedula);
                        cmd.Parameters.AddWithValue("@contra", datos.Contra);

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
