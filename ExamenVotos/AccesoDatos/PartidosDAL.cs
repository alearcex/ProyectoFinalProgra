using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamenVotos.AccesoDatos
{
    public class PartidosDAL
    {
        public int IdPartido { get; set; }
        public string Descripcion { get; set; }

        public PartidosDAL(int idPartido, string descripcion)
        {
            IdPartido = idPartido;
            Descripcion = descripcion;
        }

        public PartidosDAL(string descripcion)
        {
            Descripcion = descripcion;
        }

        public static List<PartidosDAL> Obtener(string connectionString)
        {
            List<PartidosDAL> listaPartidos = new List<PartidosDAL>();
            string query = "SELECT IdPartido, Descripcion FROM Partidos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PartidosDAL p = new PartidosDAL(
                                Convert.ToInt32(reader["IdPartido"]),
                                reader["Descripcion"].ToString());

                            listaPartidos.Add(p);
                        }
                    }
                }
            }
            return listaPartidos;
        }

        public static int Insertar(PartidosDAL partido, string connectionString)
        {
            int response = 0;
            string query = "INSERT INTO Partidos (Descripcion) VALUES (@Descripcion)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@Descripcion", partido.Descripcion ));

                        conn.Open();
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
