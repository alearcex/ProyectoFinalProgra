using ExamenVotos.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExamenVotos.Business
{
    public class VotosCore
    {
        public static List<CandidatosGrid> ConsultaCandidatos(string connectionString)
        {
            List<CandidatosGrid> candidatos = new List<CandidatosGrid>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("VotosSP", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@accion", "SELECT");
                        cmd.Parameters.AddWithValue("@cedula", DBNull.Value);
                        cmd.Parameters.AddWithValue("@idCandidato", DBNull.Value);
                        cmd.Parameters.AddWithValue("@fecha", DBNull.Value);

                        connection.Open();
                        cmd.ExecuteNonQuery();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CandidatosGrid cg = new CandidatosGrid(
                                    reader["NombreCompleto"].ToString(),
                                    Convert.ToInt32(reader["IdPartido"]),
                                    reader["Descripcion"].ToString(),
                                    Convert.ToInt32(reader["IdCandidato"])
                                );

                                candidatos.Add(cg);
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return candidatos;
        }

        public static int GuardarVoto(VotosDAO voto, string connectionString)
        {
            int response = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("VotosSP", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@accion", "GUARDAR");
                        cmd.Parameters.AddWithValue("@cedula", voto.Cedula);
                        cmd.Parameters.AddWithValue("@idCandidato", voto.IdCandidato);
                        cmd.Parameters.AddWithValue("@fecha", voto.Fecha);

                        connection.Open();
                        cmd.ExecuteNonQuery();
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
