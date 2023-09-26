namespace WebApi
{
    
    public class InformeCadete
    {
        private int idCadete;
        private int pedidosEntregados;
        private int montoACobrar;
        public int IdCadete { get => idCadete; }
        public int PedidosEntregados { get => pedidosEntregados;  }
        public int MontoACobrar { get => montoACobrar; }

        public InformeCadete(int idCadete,List<Pedidos> listaPedidos){
            this.idCadete = idCadete;
            this.pedidosEntregados=0;
            foreach (Pedidos pedido in listaPedidos)
            {
                if (pedido.Estado == "Entregado" && pedido.IdCadeteEncargado == idCadete)
                {
                    this.pedidosEntregados++;
                }
            }
            this.montoACobrar = PedidosEntregados*500;
        }


    }
    public class Informe
    {
        private int montoTotalGanado;
        private int totalEnvios;
        private double promedioEnviosXCadete;
        private List<InformeCadete> listaInformesCadetes;
        public int MontoTotalGanado { get => montoTotalGanado; set => montoTotalGanado = value; }
        public int TotalEnvios { get => totalEnvios; set => totalEnvios = value; }
        public double PromedioEnviosXCadete { get => promedioEnviosXCadete; set => promedioEnviosXCadete = value; }
        public List<InformeCadete> ListaInformesCadetes { get => listaInformesCadetes; set => listaInformesCadetes = value; }

        public Informe(){

        }
        public Informe(List<Pedidos> ListaPedidos,List<Cadete> ListaCadetes )
        {
            totalEnvios = 0;
            montoTotalGanado = 0;
            listaInformesCadetes = new List<InformeCadete>();
            foreach (Cadete cadete in ListaCadetes)
            {
                
                var nuevoInforme = new InformeCadete(cadete.Id,ListaPedidos);

                totalEnvios = totalEnvios+nuevoInforme.PedidosEntregados;
                montoTotalGanado += nuevoInforme.MontoACobrar;
                listaInformesCadetes.Add(nuevoInforme);
            }
            if (ListaCadetes.Count == 0)
            {
                promedioEnviosXCadete = 0;
            }else
            {
                promedioEnviosXCadete = totalEnvios/ListaCadetes.Count;
            }
        }

    }
}