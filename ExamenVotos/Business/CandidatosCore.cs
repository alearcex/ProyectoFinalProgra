using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ExamenVotos.DAL;

namespace ExamenVotos.Business
{
    public class CandidatosCore
    {
        public static List<CandidatosDAO> ObtenerCandidatos(string connectionString)
        {
            List<CandidatosDAO> listaCandidatos = new List<CandidatosDAO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("CandidatosSP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@accion", "SELECT");
                        command.Parameters.AddWithValue("@idCandidato", DBNull.Value);
                        command.Parameters.AddWithValue("@cedula", DBNull.Value);
                        command.Parameters.AddWithValue("@idPartido", DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CandidatosDAO c = new CandidatosDAO(
                                    Convert.ToInt32(reader["IdCandidato"]),
                                    reader["Cedula"].ToString(),
                                    Convert.ToInt32(reader["IdPartido"]),
                                    reader["Descripcion"].ToString()
                                    );

                                listaCandidatos.Add(c);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaCandidatos;
        }

        public static int EjecutarStoredProcedure(string accion, CandidatosDAO candidato, string connectionString)
        {
            int response = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("CandidatosSP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@accion", accion);
                        command.Parameters.AddWithValue("@idCandidato", candidato.IdCandidato != 0 ? (object)candidato.IdCandidato : DBNull.Value);
                        command.Parameters.AddWithValue("@cedula", candidato.Cedula ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@idPartido", candidato.IdPartido != 0 ? (object)candidato.IdPartido : DBNull.Value);

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
