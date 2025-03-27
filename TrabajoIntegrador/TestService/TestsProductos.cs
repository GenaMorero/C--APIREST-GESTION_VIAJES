using ProyectoDto;
using ProyectoService;

namespace TestService
{
    public class Tests_Productos
    {
        ProductoService administrador = new ProductoService();
        Resultado resultado = new Resultado();
        [SetUp]
        public void Setup()
        {
            
            administrador = new ProductoService();
            resultado= new Resultado();
        }
        [Test]
        public void Test_ActualizarProducto_Stock_Nulo_Deberia_False()
        {
            resultado = administrador.ActualizarStockProducto(1, 0);
            
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("El stock a agregar no puede ser 0 o menor 0"));
        }
        [Test]
        public void Test_ActualizarProducto_Id_Nulo_Deberia_False()
        {
            resultado = administrador.ActualizarStockProducto(0, 10);

            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("El id ingresado no corresponde a ningun producto"));
        }

    }
}