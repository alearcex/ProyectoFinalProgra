using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ExamenVotos.AccesoDatos
{
    public class PadronDAL
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int Edad { get; set; }

        public PadronDAL(string cedula, string nombre, string primerApellido, string segundoApellido, int edad)
        {
            Cedula = cedula;
            Nombre = nombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            Edad = edad;
        }

        public PadronDAL(string cedula)
        {
            Cedula = cedula;
        }

        public static List<PadronDAL> Obtener(string connectionString)
        {
            List<PadronDAL> listaPadron = new List<PadronDAL>();
            string query = "SELECT Cedula, Nombre, PrimerApellido, SegundoApellido, Edad FROM Padron";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PadronDAL p = new PadronDAL(
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

            return listaPadron;
        }

        public static int Insertar(PadronDAL padron, string connectionString)
        {
            int response = 0;
            string queryPadron = "INSERT INTO Padron (Cedula, Nombre, PrimerApellido, SegundoApellido, Edad) " +
                "VALUES (@Cedula, @Nombre, @PrimerApellido, @SegundoApellido, @Edad)";

            //Esto lo quise agregar para que las contraseñas sean autogeneradas
            string queryUsuarios = "INSERT INTO Usuarios (Cedula, Contraseña) VALUES (@Cedula, 'tse' + RIGHT(REPLICATE('0', 4) + @Cedula, 4))";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand commandPadron = new SqlCommand(queryPadron, connection, transaction))
                            {
                                commandPadron.Parameters.Add(new SqlParameter("@Cedula", padron.Cedula));
                                commandPadron.Parameters.Add(new SqlParameter("@Nombre", padron.Nombre));
                                commandPadron.Parameters.Add(new SqlParameter("@PrimerApellido", padron.PrimerApellido));
                                commandPadron.Parameters.Add(new SqlParameter("@SegundoApellido", padron.SegundoApellido));
                                commandPadron.Parameters.Add(new SqlParameter("@Edad", padron.Edad));

                                commandPadron.ExecuteNonQuery();
                            }

                            using (SqlCommand commandUsuarios = new SqlCommand(queryUsuarios, connection, transaction))
                            {
                                commandUsuarios.Parameters.Add(new SqlParameter("@Cedula", padron.Cedula ?? (object)DBNull.Value));
                                commandUsuarios.ExecuteNonQuery();
                            }

                            //Esto lo agruegué para que se confirme la transacción hasta que los dos insert hayan salido bien
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            // Si ocurre un error en alguno de los insert, se revierte la transacción
                            transaction.Rollback();
                            response = -1;
                        }
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
