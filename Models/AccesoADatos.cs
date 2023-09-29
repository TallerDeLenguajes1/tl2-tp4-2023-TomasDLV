namespace WebApi
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class AccesoADatosCadeteria
    {
        public Cadeteria Obtener(){
            string contenido = File.ReadAllText("Models/cadeteria.json");
            Cadeteria cadeteria;
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(contenido);
            
            return cadeteria;
        }


    }
    public class AccesoADatosCadetes
    {
        public List<Cadete> Obtener(){
            string contenido = File.ReadAllText("Models/cadetes.json");
            List<Cadete> cadetes;
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(contenido);
            
            return cadetes;
        }
    }
    public class AccesoADatosPedidos 
    {
       public List<Pedidos> Obtener(){
            string contenido = File.ReadAllText("Models/pedidos.json");
            List<Pedidos> pedidos;
            pedidos = JsonSerializer.Deserialize<List<Pedidos>>(contenido);
            
            return pedidos;
        }
        
        public void Guardar(List<Pedidos> pedidos){
            string contenido = JsonSerializer.Serialize(pedidos);
            File.WriteAllText("Models/pedidos.json", contenido);
        }

    }
}
