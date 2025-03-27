using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using ProyectoDto;
using ProyectoService;

namespace WebAppi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CompraController : Controller
    {
        private ComprasService administrador = new ComprasService();
        private Resultado resultado = new Resultado();
        public CompraController()
        {
            administrador = new ComprasService();
            resultado = new Resultado();
        }
        [HttpPost("AgregarCompra")]
        public IActionResult AgregarCompra([FromBody]CompraDto compra)
        {
            resultado=administrador.AgregarCompra(compra);
            if (resultado.Success)
            {
                return Ok(resultado.ListaMensajes);
            }
            else
            {
                return BadRequest(resultado.ListaMensajes);
            }
        }
        [HttpGet("GetLitsadoCompras")]
        public IActionResult GetCompras()
        {
            List<CompraDto> listaViajes = administrador.GetListadoCompras();
            if (listaViajes.Count == 0)
            {
                return Ok("La lista esta vacia");
            }
            else
            {
                return Ok(listaViajes);
            }
        }
    }
}
