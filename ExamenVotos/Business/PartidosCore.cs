using ExamenVotos.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ExamenVotos.Business
{
    public class PartidoCore
    {
        public static List<PartidosDAO> ObtenerPartidos(string connectionString)
        {
            List<PartidosDAO> listaPartidos = new List<PartidosDAO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("PartidosSP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@accion", "SELECT");
                        command.Parameters.AddWithValue("@idPartido", DBNull.Value);
                        command.Parameters.AddWithValue("@descripcion", DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PartidosDAO p = new PartidosDAO(
                                    Convert.ToInt32(reader["IdPartido"]),
                                    reader["Descripcion"].ToString());

                                listaPartidos.Add(p);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaPartidos;
        }

        public static int EjecutarStoredProcedure(string accion, PartidosDAO partido, string connectionString)
        {
            int response = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("PartidosSP", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@accion", accion);
                        command.Parameters.AddWithValue("@idPartido", partido.IdPartido);
                        command.Parameters.AddWithValue("@descripcion", partido.Descripcion);

                        conn.Open();

                        command.ExecuteNonQuery();
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