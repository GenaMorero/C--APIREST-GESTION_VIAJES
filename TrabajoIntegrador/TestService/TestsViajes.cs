using ProyectoData;
using ProyectoDto;
using ProyectoService;

namespace Test_Viajes
{
    public class Tests_Viajes
    {
        ViajesService administradorViajes = new ViajesService();
        ComprasService administradorCompras = new ComprasService();
        Archivo<Compra> ArchivoCompra = new Archivo<Compra>();
        Archivo<Viaje> ArchivoViaje = new Archivo<Viaje>();
        Resultado resultado = new Resultado();
        [SetUp]
        public void Setup()
        {
            ArchivoViaje = new Archivo<Viaje>() {
                
                Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaViajes.json")
            };
            ArchivoCompra = new Archivo<Compra>() {
                Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaCompras.json")
            };
            administradorViajes = new ViajesService();
            administradorCompras = new ComprasService();
            resultado = new Resultado();
            List<Compra> listaCompra = new List<Compra>();
            ArchivoCompra.Serializar(listaCompra);
            List<Viaje> listaViajes = new List<Viaje>();
            ArchivoViaje.Serializar(listaViajes);
        }
        [Test]
        public void Test_AgregarViaje_SinCompras_Deberia_false()
        {
            ViajeDto viaje = new ViajeDto()
            {
                FechaDesde = DateTime.Now.AddDays(1),
                FechaHasta = DateTime.Now.AddDays(8),
            };
            resultado = administradorViajes.AgregarViaje(viaje);
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("No hay compras para armar viajes"));
        }
        [Test]
        public void Test_AgregarViaje_FechaOcupada_Deberia_False()
        {

            CompraDto compra1 = new CompraDto()
            {
                CodigoProductoCompra = 1,
                DniClienteCompra = 240,
                CantidadComprada = 5,
                FechaEntregaSolicitada = DateTime.Now.AddDays(5),
            };
            CompraDto compra2 = new CompraDto()
            {
                CodigoProductoCompra = 3,
                DniClienteCompra = 240,
                CantidadComprada = 1,
                FechaEntregaSolicitada = DateTime.Now.AddDays(3),
            };
            CompraDto compra3 = new CompraDto()
            {
                CodigoProductoCompra = 4,
                DniClienteCompra = 240,
                CantidadComprada = 3,
                FechaEntregaSolicitada = DateTime.Now.AddDays(2),
            };
            administradorCompras.AgregarCompra(compra1);
            administradorCompras.AgregarCompra(compra2);
            administradorCompras.AgregarCompra(compra3);
            ViajeDto viaje = new ViajeDto()
            {
                FechaDesde = DateTime.Now.AddDays(1),
                FechaHasta = DateTime.Now.AddDays(8),
            };
            administradorViajes.AgregarViaje(viaje);
            resultado = administradorViajes.AgregarViaje(viaje);
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("Ya hay un viaje asignado en estas fechas"));

        }
        [Test]
        public void Test_AgregarViaje_Deberia_True()
        {
            
            CompraDto compra1 = new CompraDto() {
                CodigoProductoCompra = 1,
                DniClienteCompra = 240,
                CantidadComprada = 5,
                FechaEntregaSolicitada = DateTime.Now.AddDays(5),
           };
            CompraDto compra2 = new CompraDto()
            {
                CodigoProductoCompra = 3,
                DniClienteCompra = 240,
                CantidadComprada = 1,
                FechaEntregaSolicitada = DateTime.Now.AddDays(3),
            };
            CompraDto compra3 = new CompraDto()
            {
                CodigoProductoCompra = 4,
                DniClienteCompra = 240,
                CantidadComprada = 3,
                FechaEntregaSolicitada = DateTime.Now.AddDays(2),
            };
            administradorCompras.AgregarCompra(compra1);
            administradorCompras.AgregarCompra(compra2);
            administradorCompras.AgregarCompra(compra3);
            ViajeDto viaje = new ViajeDto()
            {
                FechaDesde=DateTime.Now.AddDays(1),
                FechaHasta=DateTime.Now.AddDays(8),
            };
            resultado = administradorViajes.AgregarViaje(viaje);
            List<Viaje> listaViajes = administradorViajes.GetViajes();
            Assert.That(resultado.Success, Is.EqualTo(true));
            Assert.That(listaViajes.Count, Is.EqualTo(1));
            Assert.That(listaViajes[0].IdCamionAsignado, Is.EqualTo(1));

        }


    }
}