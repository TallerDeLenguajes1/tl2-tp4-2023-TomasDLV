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
        private AccesoADatosCadeteria accesoCadeteria= new AccesoADatosCadeteria();
        private AccesoADatosCadetes accesoCadetes= new AccesoADatosCadetes();

        private AccesoADatosPedidos accesoPedidos= new AccesoADatosPedidos();

        public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }
        public int NroPedidosCreados { get => nroPedidosCreados; set => nroPedidosCreados = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public List<Pedidos> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }
        public Informe CadInforme { get => cadInforme; set => cadInforme = value; }
        public AccesoADatosCadeteria AccesoCadeteria { get => accesoCadeteria; set => accesoCadeteria = value; }
        public AccesoADatosCadetes AccesoCadetes { get => accesoCadetes; set => accesoCadetes = value; }
        public AccesoADatosPedidos AccesoPedidos { get => accesoPedidos; set => accesoPedidos = value; }

        private static Cadeteria instance;
        
        public static Cadeteria Instance()
        {
          
                // Crear la instancia Cadeteria si aún no existe.
                if (instance == null)
                {
                    AccesoADatosCadeteria AccesoCadeteria= new AccesoADatosCadeteria();
                    
                    instance = AccesoCadeteria.Obtener();
                    instance.ListaCadetes = instance.AccesoCadetes.Obtener();
                    instance.ListaPedidos = instance.AccesoPedidos.Obtener();
                    instance.NroPedidosCreados = instance.ListaPedidos.Count;
                    
                }
                return instance;
            
        }

        public Cadeteria(string nombre, int telefono, int nroPedidosCreados)
        {
            this.nombre = nombre;
            this.telefono = telefono;
            this.nroPedidosCreados = nroPedidosCreados;
            this.listaCadetes = new List<Cadete>();
        }


        public bool AgregarPedido()
        {
            Pedidos nuevoPedido = new Pedidos(nroPedidosCreados + 1); // Crea una instancia de Pedido ; NOTA: necesito AGREGAR OBS
            if (nuevoPedido!=null)
            {
                NroPedidosCreados += 1; // Incremento la cantidad de pedidos creados
                listaPedidos.Add(nuevoPedido);
                return true;
            }else
            {
                return false;
            }
            
            //Console.WriteLine("Se creo el pedido nro: "+nuevoPedido.Nro+ " y se lo agrego a la lista");
        }

        public bool AsignarPedido(int idPedido, int idCadete)
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
                        return true;
                    }else
                    {
                        
                        return false;
                    }
                }
                else
                {
                    //Console.WriteLine("El pedido que ingresaste no se encontro en la lista de pedidos");
                    return false;
                }
            }
            else
            {
                //Console.WriteLine("No se encontro el cadete que ingresaste");
                return false;
            }
        }
        public bool CambiarCadetePedido(int idPedido, int idCadete)
        {
            Cadete cadeteBuscado = listaCadetes.FirstOrDefault(cadete => cadete.Id == idCadete);
            if (cadeteBuscado != null)
            {
                Pedidos pedidoBuscado = ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
                if (pedidoBuscado != null)
                {
                    pedidoBuscado.IdCadeteEncargado = idCadete;
                    return true;
                }else
                {
                    return false;
                }
            }
            else
            {
                //Console.WriteLine("No se encontro el cadete que ingresaste");
                return false;
            }
        }

        public bool CambiarEstadoPedido(int idPedido, int estado) // Este metodo recibe por parametro la id del pedido a entregar, busca que cadete lo posee y lo cambia de estado
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
                        return false;
                }
                pedidoEncontrado.Estado = nuevoEstado;
                return true;

            }
            else
            {
                //Console.WriteLine("Ingrese un ID válido.");
                return false;
            }
        }
        public bool EliminarPedido(int idPedido)
        { // Esta funcion da de alta un pedio por una id recibida
            Pedidos pedidoEncontrado = ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
            if (pedidoEncontrado != null)
            {
                ListaPedidos.Remove(pedidoEncontrado);
                //Console.WriteLine("Se elimino el Pedido "+ pedidoEncontrado.Nro + " exitosamente");
                return true;
            }
            else
            {
                //Console.WriteLine("No se encontro el pedido para eliminar");
                return false;
            }
            
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
        public bool CrearInforme()
        {
            var nuevoInforme = new Informe(this.listaPedidos, this.ListaCadetes);
            if (nuevoInforme != null)
            {
                CadInforme = nuevoInforme;
                return true;
            }else
            {
                return false;
            }
            
        }
    }
}