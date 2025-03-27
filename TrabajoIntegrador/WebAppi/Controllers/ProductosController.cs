using Microsoft.AspNetCore.Mvc;
using ProyectoDto;
using ProyectoService;

namespace WebAppi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductosController : Controller
    {
        private ProductoService administrador = new ProductoService();
        private Resultado resultado = new Resultado();
        public ProductosController()
        {
            administrador = new ProductoService();
            resultado = new Resultado();
        }
        [HttpPost("AgregarProducto")]
        public IActionResult AgregarProducto([FromBody]ProductoDto producto) {
            resultado = administrador.AgregarProducto(producto);
            if (resultado.Success)
            {
                return Ok(resultado.ListaMensajes);
            }
            else
            {
                return BadRequest(resultado.ListaMensajes);
            }
        }
        [HttpPut("ActualizarProducto/{id}")]
        public IActionResult ActualizarProducto(int id,[FromBody] int stock)
        {
            resultado = administrador.ActualizarStockProducto(id,stock);
            if (resultado.Success)
            {
                return Ok(resultado.ListaMensajes);
            }
            else
            {
                return BadRequest(resultado.ListaMensajes);
            }
        }
        [HttpGet("GetListadoProductos")]
        public IActionResult GetProductos()
        {
            List<ProductoDto> listaViajes = administrador.GetListadoProducto();
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
