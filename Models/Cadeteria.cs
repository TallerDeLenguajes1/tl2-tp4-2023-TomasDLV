namespace WebApi
{
    public class Cadeteria
    {
        private string nombre;
        private int telefono;
        private List<Cadete> listaCadetes = new List<Cadete>();
        private int nroPedidosCreados;
        private List<Pedidos> listaPedidos = new List<Pedidos>();

        private Informe cadInforme = new Informe();

        public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }
        public int NroPedidosCreados { get => nroPedidosCreados; set => nroPedidosCreados = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public List<Pedidos> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }
        public Informe CadInforme { get => cadInforme; set => cadInforme = value; }
        private static Cadeteria instance;
        
        public static Cadeteria Instance
        {
            get
            {
                // Crear la instancia Cadeteria si aún no existe.
                if (instance == null)
                {
                    AccesoADatos cargar = new AccesoJson();
                    instance = cargar.CargarInfoCadeteria();
                }
                return instance;
            }
        }

        public Cadeteria(string nombre, int telefono, int nroPedidosCreados)
        {
            this.nombre = nombre;
            this.telefono = telefono;
            this.nroPedidosCreados = nroPedidosCreados;
            this.listaCadetes = new List<Cadete>();
        }


        public void AgregarPedido()
        {
            Pedidos nuevoPedido = new Pedidos(nroPedidosCreados + 1); // Crea una instancia de Pedido ; NOTA: necesito AGREGAR OBS
            NroPedidosCreados += 1; // Incremento la cantidad de pedidos creados
            listaPedidos.Add(nuevoPedido);
            //Console.WriteLine("Se creo el pedido nro: "+nuevoPedido.Nro+ " y se lo agrego a la lista");
        }

        public void AsignarPedido(int idPedido, int idCadete)
        {
            Cadete cadeteBuscado = listaCadetes.FirstOrDefault(cadete => cadete.Id == idCadete);
            if (cadeteBuscado != null)
            {
                Pedidos pedidoBuscado = ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
                if (pedidoBuscado != null)
                {
                    //Si el pedido no tiene cadete asignado lo agrega
                    if (pedidoBuscado.IdCadeteEncargado == null)
                    {
                        pedidoBuscado.IdCadeteEncargado = idCadete;
                        //Console.WriteLine("Pedido nro "+ idPedido +" asignado al cadete: " + cadeteBuscado.Nombre);
                    }
                }
                else
                {
                    //Console.WriteLine("El pedido que ingresaste no se encontro en la lista de pedidos");
                }
            }
            else
            {
                //Console.WriteLine("No se encontro el cadete que ingresaste");
            }
        }
        public void CambiarCadetePedido(int idPedido, int idCadete)
        {
            Cadete cadeteBuscado = listaCadetes.FirstOrDefault(cadete => cadete.Id == idCadete);
            if (cadeteBuscado != null)
            {
                Pedidos pedidoBuscado = ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
                if (pedidoBuscado != null)
                {
                    pedidoBuscado.IdCadeteEncargado = idCadete;
                }
            }
            else
            {
                //Console.WriteLine("No se encontro el cadete que ingresaste");
            }
        }

        public void CambiarEstadoPedido(int idPedido, int estado) // Este metodo recibe por parametro la id del pedido a entregar, busca que cadete lo posee y lo cambia de estado
        {

            // Console.WriteLine("Ingrese el ID del pedido a cambiar de estado: ");

            // int.TryParse(Console.ReadLine(), out int idPedido);
            Pedidos pedidoEncontrado = listaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);

            if (pedidoEncontrado != null)
            {
                // Console.WriteLine("Seleccione el estado al que cambiar:");
                // Console.WriteLine("a) Pendiente");
                // Console.WriteLine("b) En Camino");
                // Console.WriteLine("c) Entregado");

                // Console.Write("Opción: ");
                //string opcionEstado = Console.ReadLine();

                string nuevoEstado = "";

                switch (estado)
                {
                    case 1:
                        nuevoEstado = "Pendiente";
                        break;
                    case 2:
                        nuevoEstado = "EnCamino";
                        break;
                    case 3:
                        nuevoEstado = "Entregado";
                        break;
                    default:
                        //Console.WriteLine("Opción no válida.");
                        return;
                }
                pedidoEncontrado.Estado = nuevoEstado;

            }
            else
            {
                //Console.WriteLine("Ingrese un ID válido.");
            }
        }
        public void EliminarPedido(int idPedido)
        { // Esta funcion da de alta un pedio por una id recibida
            Pedidos pedidoEncontrado = ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
            if (pedidoEncontrado != null)
            {
                ListaPedidos.Remove(pedidoEncontrado);
                //Console.WriteLine("Se elimino el Pedido "+ pedidoEncontrado.Nro + " exitosamente");
            }
            else
            {
                //Console.WriteLine("No se encontro el pedido para eliminar");
            }
            ///Console.WriteLine("No se encontro el pedido " + idPedido + ".");
        }
        public double JornalACobrar(int idCadete)
        {
            double cantPedidosEntregados = 0;
            foreach (Pedidos pedido in ListaPedidos)
            {
                if (pedido.IdCadeteEncargado == idCadete && pedido.Estado == "Entregado")
                {
                    cantPedidosEntregados++;
                }
            }
            return 500 * cantPedidosEntregados;
        }
        public void CrearInforme()
        {
            var nuevoInforme = new Informe(this.listaPedidos, this.ListaCadetes);
            CadInforme = nuevoInforme;
        }
    }
}