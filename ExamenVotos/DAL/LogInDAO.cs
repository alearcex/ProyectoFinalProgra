using System;

namespace ExamenVotos.DAL
{
    public class LogInDAO
    {
        public string Cedula { get; set; }
        public string Contra { get; set; }

        public LogInDAO(string cedula, string contra)
        {
            Cedula = cedula;
            Contra = contra;
        }
    }
}
