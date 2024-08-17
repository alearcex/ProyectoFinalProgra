using ExamenVotos.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExamenVotos.Business
{
    public class PadronCore
    {
        public static List<PadronDAO> ObtenerPadron(string connectionString)
        {
            List<PadronDAO> listaPadron = new List<PadronDAO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("PadronSP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@accion", "SELECT");
                        command.Parameters.AddWithValue("@cedula", DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", DBNull.Value);
                        command.Parameters.AddWithValue("@apellido1", DBNull.Value);
                        command.Parameters.AddWithValue("@apellido2", DBNull.Value);
                        command.Parameters.AddWithValue("@edad", DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PadronDAO p = new PadronDAO(
                                    reader["Cedula"].ToString(),
                                    reader["Nombre"].ToString(),
                                    reader["PrimerApellido"].ToString(),
                                    reader["SegundoApellido"].ToString(),
                                    Convert.ToInt32(reader["Edad"])
                                );

                                listaPadron.Add(p);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaPadron;
        }

        public static int EjecutarStoredProcedure(string accion, PadronDAO padron, string connectionString)
        {
            int response = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("PadronSP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@accion", accion);
                        command.Parameters.AddWithValue("@cedula", padron.Cedula ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", padron.Nombre ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@apellido1", padron.PrimerApellido ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@apellido2", padron.SegundoApellido ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@edad", padron.Edad > 0 ? (object)padron.Edad : DBNull.Value);

                        connection.Open();

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
