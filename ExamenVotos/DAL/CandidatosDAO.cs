using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenVotos.DAL
{
    public class CandidatosDAO
    {
        public int IdCandidato { get; set; }
        public string Cedula { get; set; }
        public int IdPartido { get; set; }
        public string Descripcion { get; set; }


        public CandidatosDAO(int idCandidato, string cedula, int idPartido, string descripcion)
        {
            IdCandidato = idCandidato;
            Cedula = cedula;
            IdPartido = idPartido;
            Descripcion = descripcion;
        }

        public CandidatosDAO(string cedula, int idPartido)
        {
            Cedula = cedula;
            IdPartido = idPartido;
        }

        public CandidatosDAO(int idCandidato)
        {
            IdCandidato = idCandidato;
        }
    }
}
