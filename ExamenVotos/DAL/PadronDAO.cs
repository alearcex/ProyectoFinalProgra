namespace ExamenVotos.DAL
{
    public class PadronDAO
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int Edad { get; set; }

        public PadronDAO(string cedula, string nombre, string primerApellido, string segundoApellido, int edad)
        {
            Cedula = cedula;
            Nombre = nombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            Edad = edad;
        }

        public PadronDAO(string cedula)
        {
            Cedula = cedula;
        }
    }
}
