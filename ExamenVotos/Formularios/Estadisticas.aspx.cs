using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenVotos.Formularios
{
    public partial class Estadisticas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosEstadisticas();
            }
        }

        private void CargarDatosEstadisticas()
        {
            string conn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            gvCandidatos.DataSource = ObtenerResultadosCandidatos(conn);
            gvCandidatos.DataBind();

            gvPartidos.DataSource = ObtenerResultadosPartidos(conn);
            gvPartidos.DataBind();

            var datosGenerales = ObtenerDatosGenerales(conn);
            lblTotalVotos.Text = $"Total de Votos: {datosGenerales.TotalVotos}";
            lblPorcVotantes.Text = $"Porcentaje votantes: {datosGenerales.PorcentajeVotantes}%";
            lblPorcAbstinencia.Text = $"Porcentaje abstinencia: {datosGenerales.porcentajeAbstinencia}%";
        }

        private DataTable ObtenerResultadosCandidatos(string conn)
        {
            string query = "SELECT pad.Nombre + ' ' + pad.PrimerApellido + ' ' + pad.SegundoApellido AS Nombre, " +
                "p.Descripcion AS Partido, " +
                "CAST( (CAST (COUNT(v.IdVoto) AS FLOAT) / (SELECT COUNT(*) FROM Votos) * 100) AS DECIMAL(5, 2)) AS Porcentaje, " +
                "COUNT(v.IdVoto) AS Votos " +
            "FROM Candidatos c " +
            "INNER JOIN Partidos p ON p.IdPartido = c.IdPartido " +
            "INNER JOIN Padron pad ON pad.Cedula = c.Cedula " +
            "LEFT JOIN Votos v ON v.IdCandidato = c.IdCandidato " +
            "GROUP BY pad.Nombre, pad.PrimerApellido, pad.SegundoApellido, p.Descripcion, c.IdCandidato";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable ObtenerResultadosPartidos(string conn)
        {
            string query = "SELECT p.Descripcion AS Partido, " +
                "CAST((CAST(COUNT(v.IdVoto) AS FLOAT) / (SELECT COUNT(*) FROM Votos) * 100) AS DECIMAL(5, 2)) AS Porcentaje," +
                "COUNT(v.IdVoto) AS Total " +
                "FROM Partidos p " +
                "LEFT JOIN Candidatos c ON c.IdPartido = p.IdPartido " +
                "LEFT JOIN Votos v ON v.IdCandidato = c.IdCandidato GROUP BY p.Descripcion";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private (int TotalVotos, decimal PorcentajeVotantes, decimal porcentajeAbstinencia) ObtenerDatosGenerales(string conn)
        {
            int totalVotos;
            int totalPadron;
            decimal porcentajeVotantes;
            decimal porcentajeAbstinencia;

            string queryTotalVotos = "SELECT COUNT(*) FROM Votos";
            string queryTotalPadron = "SELECT COUNT(*) FROM Padron";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(queryTotalVotos, connection))
                {
                    totalVotos = (int)cmd.ExecuteScalar();
                }

                using (SqlCommand cmd = new SqlCommand(queryTotalPadron, connection))
                {
                    totalPadron = (int)cmd.ExecuteScalar();
                }

                porcentajeVotantes = totalPadron > 0 ? Math.Round((decimal)totalVotos / totalPadron * 100, 2): 0;
                porcentajeAbstinencia = totalPadron > 0 ? 100 - porcentajeVotantes : 0;
            }

            return (totalVotos, porcentajeVotantes, porcentajeAbstinencia);
        }
    }
}