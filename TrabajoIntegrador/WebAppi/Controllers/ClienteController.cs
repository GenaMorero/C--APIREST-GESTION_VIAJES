using Microsoft.AspNetCore.Mvc;
using ProyectoDto;
using ProyectoService;

namespace WebAppi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ClienteController : Controller
    {
        private ClientesService administrador = new ClientesService();
        private Resultado resultado = new Resultado();
        public ClienteController()
        {
            administrador = new ClientesService();
            resultado = new Resultado();
        }

        [HttpGet("obtenerclientes")]
        public IActionResult GetListadoClientes()
        {
            List<ClienteDto> lista = administrador.DevolverListadoClientes();
            return Ok(lista);
        }
        [HttpPost("agregarcliente")]
        public IActionResult PostCliente([FromBody] ClienteDto cliente)
        {
            resultado = administrador.AgregarCliente(cliente);
            if (resultado.Success)
            {
                return Ok(resultado.ListaMensajes);
            }
            else
            {
                return BadRequest(resultado.ListaMensajes);
            }
        }
        [HttpPut("editarcliente")]
        public IActionResult EditarCliente([FromBody] ClienteDto cliente)
        {
            resultado = administrador.EditarCliente(cliente);
            if (resultado.Success)
            {
                return Ok(resultado.ListaMensajes);
            }
            else
            {
                return BadRequest(resultado.ListaMensajes);
            }
        }
        [HttpDelete("eliminarcliente/{dni}")]
        public IActionResult EliminarCLiente(int dni)
        {
            resultado = administrador.EliminarCliente(dni);
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
