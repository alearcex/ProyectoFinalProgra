using System;

namespace ExamenVotos.DAL
{
    public class VotosDAO
    {
        public int IdVoto { get; set; }
        public string Cedula { get; set; }
        public int IdCandidato { get; set; }
        public DateTime Fecha { get; set; }

        public VotosDAO(string cedula, int idCandidato, DateTime fecha)
        {
            Cedula = cedula;
            IdCandidato = idCandidato;
            Fecha = fecha;
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
