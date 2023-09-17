namespace WebApi
{
    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private int telefono;
        //private List<Pedidos> listaPedidos = new List<Pedidos>();// ListadoPedidos quitado en tp2

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        //public List<Pedidos> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }
        public Cadete(int id, string nombre, string direccion, int telefono)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }
        
        /*En tp2 no va
        public void AgregarPedido(Pedidos nuevoPedido)
        {
            
            nuevoPedido.Estado = "EnCamino";
            ListaPedidos.Add(nuevoPedido);
            Console.WriteLine("El cadete ha recibido el pedido y esta " + nuevoPedido.Estado + ".");
        }
        public int PedidosEntregados(){
            int cantPedidosRealizados = 0;
            foreach (Pedidos pedido in ListaPedidos)
            {
                if (pedido.Estado == "Entregado")
                {
                    cantPedidosRealizados += 1;
                }
            }
            return cantPedidosRealizados;
        }*/
        

    }
}