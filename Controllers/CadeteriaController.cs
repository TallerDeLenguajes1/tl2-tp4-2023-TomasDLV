using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadeteriaController : ControllerBase
    {
        // Supongamos que Cadeteria es una clase Singleton.
        private Cadeteria _cadeteria;
        private readonly ILogger<CadeteriaController> _logger;

        // Constructor para inicializar la Cadeteria.
        public CadeteriaController(ILogger<CadeteriaController> logger)
        {
            _logger = logger;
            _cadeteria = Cadeteria.Instance();
        }

        // GET api/cadeteria/pedidos
        [HttpGet("pedidos")]
        [Route("pedidos")]
        public IActionResult GetPedidos()
        {
            var pedidos = _cadeteria.ListaPedidos;
            return Ok(pedidos);
        }

        // GET api/cadeteria/cadetes
        
        [HttpGet]
        [Route("cadetes")]

        public IActionResult GetCadetes()
        {
            List<Cadete> listadoCadetes = _cadeteria.ListaCadetes;
            return Ok(listadoCadetes);
        }

        // GET api/cadeteria/informe
        [HttpGet("informe")]
        public ActionResult<Informe> GetInforme()
        {
            _cadeteria.CrearInforme();
            var informe = _cadeteria.CadInforme;
            return Ok(informe);
        }

        // POST api/cadeteria/agregarpedido
        [HttpPost("agregarpedido")]
        public ActionResult AgregarPedido()
        {
            int cantP = _cadeteria.ListaPedidos.Count;
            _cadeteria.AgregarPedido();
            _cadeteria.AccesoPedidos.Guardar(_cadeteria.ListaPedidos);
            if (_cadeteria.ListaPedidos.Count == cantP + 1)
            {
                return Ok("Pedido agregado correctamente.");
            }
            else
            {
                return StatusCode(500, "No se pudo cargar el pedido");
            }

        }

        // PUT api/cadeteria/asignarpedido/5/3
        [HttpPut("asignarpedido/{idPedido}/{idCadete}")]
        public ActionResult AsignarPedido(int idPedido, int idCadete)
        {
            _cadeteria.AsignarPedido(idPedido, idCadete);
            _cadeteria.AccesoPedidos.Guardar(_cadeteria.ListaPedidos);
            return Ok($"Pedido {idPedido} asignado al cadete {idCadete}.");
        }

        // PUT api/cadeteria/cambiarestadopedido/5/2
        [HttpPut("cambiarestadopedido/{idPedido}/{nuevoEstado}")] // ESTADOS 1 = "EnPreparacion" | 2 = "EnCamino" | 3 = "Entregado"
        public ActionResult CambiarEstadoPedido(int idPedido, int nuevoEstado)
        {
            _cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado);
            _cadeteria.AccesoPedidos.Guardar(_cadeteria.ListaPedidos);
            return Ok($"Estado del pedido {idPedido} cambiado a {nuevoEstado}.");
        }

        // PUT api/cadeteria/cambiarcadetepedido/5/7
        [HttpPut("cambiarcadetepedido/{idPedido}/{idNuevoCadete}")]
        public ActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
        {
            _cadeteria.CambiarCadetePedido(idPedido, idNuevoCadete);
            _cadeteria.AccesoPedidos.Guardar(_cadeteria.ListaPedidos);
            return Ok($"Cadete del pedido {idPedido} cambiado a {idNuevoCadete}.");
        }
    }
}
