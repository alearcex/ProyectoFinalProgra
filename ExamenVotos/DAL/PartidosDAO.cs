using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenVotos.DAL
{
    public class PartidosDAO
    {
        public int IdPartido { get; set; }
        public string Descripcion { get; set; }

        public PartidosDAO(int idPartido, string descripcion)
        {
            IdPartido = idPartido;
            Descripcion = descripcion;
        }

        public PartidosDAO(int idPartido)
        {
            IdPartido = idPartido;
        }
    }
}
