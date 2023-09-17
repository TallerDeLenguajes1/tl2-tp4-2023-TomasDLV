namespace WebApi
{
    public class Cliente
    {
        private string nombre;
        private string direccion;
        private int telefono;
        private string datosReferenciaDireccion;
        
        public string Nombre { get => nombre;}
        public string Direccion { get => direccion;}
        public int Telefono { get => telefono;}
        public string DatosReferenciaDireccion { get => datosReferenciaDireccion;}

        public Cliente(string nombre, string direccion, int telefono, string datosReferenciaDireccion)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.datosReferenciaDireccion = datosReferenciaDireccion;
        }

        
    }
}