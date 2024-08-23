using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ExamenVotos.AccesoDatos
{
    public class CandidatosDAL
    {
        public int IdCandidato { get; set; }
        public string Cedula { get; set; }
        public int IdPartido { get; set; }
        public string Descripcion { get; set; }

        //Constructor para el grid
        public CandidatosDAL(int idCandidato, string cedula, int idPartido, string descripcion)
        {
            IdCandidato = idCandidato;
            Cedula = cedula;
            IdPartido = idPartido;
            Descripcion = descripcion;
        }

        //Constructor para Guardar
        public CandidatosDAL(string cedula, int idPartido)
        {
            Cedula = cedula;
            IdPartido = idPartido;
        }


        public static List<CandidatosDAL> Obtener(string connectionString)
        {
            List<CandidatosDAL> listaCandidatos = new List<CandidatosDAL>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = " SELECT c.IdCandidato, c.Cedula, c.IdPartido, p.Descripcion " +
                    "FROM Candidatos c " +
                    "INNER JOIN Partidos p ON p.IdPartido = c.IdPartido";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CandidatosDAL c = new CandidatosDAL(
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

            return listaCandidatos;
        }

        public static int Insertar(CandidatosDAL candidato, string connectionString)
        {
            int response = 0;
            string query = "INSERT INTO Candidatos (Cedula, IdPartido) VALUES (@Cedula, @IdPartido)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Cedula", candidato.Cedula ));
                        command.Parameters.Add(new SqlParameter("@IdPartido", candidato.IdPartido ));

                        connection.Open();
                        response = command.ExecuteNonQuery();
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
