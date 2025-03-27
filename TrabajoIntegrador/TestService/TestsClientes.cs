using ProyectoDto;
using ProyectoService;

namespace TestService
{
    public class Tests_Clientes
    {
        ClientesService administrador = new ClientesService();
        Resultado resultado = new Resultado();
        [SetUp]
        public void Setup()
        {
            
            administrador = new ClientesService();
            resultado= new Resultado();
        }
        [Test]
        public void Test_EliminarCliente_NotFound_Deberia_False()
        {
            resultado = administrador.EliminarCliente(-1);
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("No se encontro el cliente"));
        }
        [Test]
        public void Test_EditarCliente_NotFound_Deberia_False()
        {
            ClienteDto cliente = new ClienteDto();
            resultado = administrador.EditarCliente(cliente);
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("No se encontro el cliente"));
        }
        [Test]
        public void Test_AgregarCliente_FaltaDni_Deberia_False()
        {
            ClienteDto cliente= new ClienteDto() { 
            DniCliente=0,
            NombreCliente="Juan",
            ApellidoCliente="Gonzales",
            EmailContactoCliente="Example@gmail.com",
            TelefonoCliente= 202020,
            LatitudCliente=102030,
            LongitudCliente=405060,
            FechaNacimientoCliente=DateTime.Now,
            };
            resultado= administrador.AgregarCliente(cliente);
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("El dni ingresado es invalido"));
        }
        [Test]
        public void Test_AgregarCliente_FaltaNombre_Deberia_False()
        {
            ClienteDto cliente = new ClienteDto()
            {
                DniCliente = 103020,
                ApellidoCliente = "Gonzales",
                EmailContactoCliente = "Example@gmail.com",
                TelefonoCliente = 202020,
                LatitudCliente = 102030,
                LongitudCliente = 405060,
                FechaNacimientoCliente = DateTime.Now,
            };
            resultado = administrador.AgregarCliente(cliente);
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("El nombre ingresado es invalido"));
        }
        [Test]
        public void Test_AgregarCliente_FaltaApellido_Deberia_False()
        {
            ClienteDto cliente = new ClienteDto()
            {
                DniCliente = 103020,
                NombreCliente = "Juan",
                EmailContactoCliente = "Example@gmail.com",
                TelefonoCliente = 202020,
                LatitudCliente = 102030,
                LongitudCliente = 405060,
                FechaNacimientoCliente = DateTime.Now,
            };
            resultado = administrador.AgregarCliente(cliente);
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("El apellido ingresado es invalido"));
        }
        [Test]
        public void Test_AgregarCliente_FaltaTelefono_Deberia_False()
        {
            ClienteDto cliente = new ClienteDto()
            {
                DniCliente = 103020,
                NombreCliente = "Juan",
                ApellidoCliente = "Gonzales",
                EmailContactoCliente = "Example@gmail.com",
                LatitudCliente = 102030,
                LongitudCliente = 405060,
                FechaNacimientoCliente = DateTime.Now,
            };
            resultado = administrador.AgregarCliente(cliente);
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("El telefono ingresado es invalido"));
        }
        [Test]
        public void Test_AgregarCliente_FaltaEmail_Deberia_False()
        {
            ClienteDto cliente = new ClienteDto()
            {
                DniCliente = 103020,
                NombreCliente = "Juan",
                ApellidoCliente = "Gonzales",
                TelefonoCliente = 202020,
                LatitudCliente = 102030,
                LongitudCliente = 405060,
                FechaNacimientoCliente = DateTime.Now,
            };
            resultado = administrador.AgregarCliente(cliente);
            Assert.That(resultado.Success, Is.EqualTo(false));
            Assert.That(resultado.ListaMensajes.Count, Is.EqualTo(1));
            Assert.That(resultado.ListaMensajes[0], Is.EqualTo("El email ingresado es invalido"));
        }
    }
}