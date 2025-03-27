using Microsoft.AspNetCore.Mvc;
using ProyectoDto;
using ProyectoService;

namespace WebAppi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ViajesController : Controller
    {
        private ViajesService administrador = new ViajesService();
        private Resultado resultado = new Resultado();
        public ViajesController()
        {
            administrador = new ViajesService();
            resultado = new Resultado();
        }
        [HttpPost("AgregarViaje")]
        public IActionResult PostViaje([FromBody] ViajeDto fecha)
        {
            resultado = administrador.AgregarViaje(fecha);
            if (resultado.Success)
            {
                return Ok(resultado.ListaMensajes);
            }
            else
            {
                return BadRequest(resultado.ListaMensajes);
            }
        }
    }
}
