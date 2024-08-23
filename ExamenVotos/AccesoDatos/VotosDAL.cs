using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ExamenVotos.AccesoDatos
{
    public class VotosDAL
    {
        public int IdVoto { get; set; }
        public string Cedula { get; set; }
        public int IdCandidato { get; set; }
        public DateTime Fecha { get; set; }

        public VotosDAL(string cedula, int idCandidato, DateTime fecha)
        {
            Cedula = cedula;
            IdCandidato = idCandidato;
            Fecha = fecha;
        }

        public static List<CandidatosGrid> ConsultaCandidatos(string connectionString)
        {
            //un poco extenso el query
            List<CandidatosGrid> candidatos = new List<CandidatosGrid>();
            string query = "SELECT pad.Nombre + ' ' + pad.PrimerApellido + ' ' + pad.SegundoApellido AS NombreCompleto, " +
                "p.IdPartido, " +
                "p.Descripcion, " +
                "c.IdCandidato " +
                "FROM Candidatos c " +
                "INNER JOIN Partidos p ON p.IdPartido = c.IdPartido " +
                "INNER JOIN Padron pad ON pad.Cedula = c.Cedula";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
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

            return candidatos;
        }

        public static int GuardarVoto(VotosDAL voto, string connectionString)
        {
            int response = 0;

            string query = "INSERT INTO Votos (Cedula, IdCandidato, Fecha) VALUES (@Cedula, @IdCandidato, @Fecha)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Cedula", voto.Cedula));
                        cmd.Parameters.Add(new SqlParameter("@IdCandidato", voto.IdCandidato));
                        cmd.Parameters.Add(new SqlParameter("@Fecha", voto.Fecha));

                        connection.Open();
                        response = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                response = -1;
            }

            return response;
        }

        public static int ValidarVoto(string cedula, string connectionString)
        {
            int response = 0;
            string query = "SELECT COUNT(*) FROM Votos WHERE Cedula = @Cedula";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Cedula", cedula);
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

    public class CandidatosGrid
    {
        public int IdCandidato { get; set; }
        public string NombreCompleto { get; set; }
        public int IdPartido { get; set; }
        public string Partido { get; set; }

        public CandidatosGrid(string nombre, int idPartido, string partido, int idcandidato)
        {
            NombreCompleto = nombre;
            IdCandidato = idcandidato;
            IdPartido = idPartido;
            Partido = partido;
        }

    }

}
