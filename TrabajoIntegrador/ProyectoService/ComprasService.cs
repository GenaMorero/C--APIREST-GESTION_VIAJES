using ProyectoData;
using ProyectoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService
{
    public class ComprasService
    {
        public ComprasService()
        {
            ArchivoCompra.Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaCompras.json");
            ArchivoCliente.Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaClientes.json");
            ArchivoProducto.Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaProductos.json");
        }
        public Archivo<Compra> ArchivoCompra = new Archivo<Compra>();
        public Archivo<Cliente> ArchivoCliente = new Archivo<Cliente>();
        public Archivo<Producto> ArchivoProducto = new Archivo<Producto>();
        public Resultado AgregarCompra(CompraDto compraDto)
        {
            Resultado resultado = new Resultado();
            Producto productoTemp = ArchivoProducto.Deserializar().FirstOrDefault(x => x.CodigoProducto == compraDto.CodigoProductoCompra && x.FechaEliminacion == null);
            Cliente clienteTemp = ArchivoCliente.Deserializar().FirstOrDefault(x => x.DniCliente == compraDto.DniClienteCompra  && x.FechaEliminacion ==null);
            resultado.ListaMensajes = compraDto.ValidacionCompraDto();
            if (productoTemp == null)
            {
                resultado.ListaMensajes.Add("El codigo de producto ingresado no existe");
            }
            else
            {
                if ((productoTemp.CantidadStock - compraDto.CantidadComprada) < 0)
                {
                    resultado.ListaMensajes.Add("No se puede realizar la compra no hay stock");
                }
            }
            if (clienteTemp == null)
            {
                resultado.ListaMensajes.Add("El codigo de cliente ingresado no coincide con ninguno cargado  anteriormente");
            }
            if (resultado.ListaMensajes.Count == 0)
            {
                double monto = productoTemp.PrecioProducto * 1.21;
                if (compraDto.CantidadComprada > 4)
                {
                    monto = monto * 0.75;
                }
                List<Compra> lista = ArchivoCompra.Deserializar();
                List<Producto> listaProductos = ArchivoProducto.Deserializar();

                listaProductos[listaProductos.FindIndex(x=>x.CodigoProducto==productoTemp.CodigoProducto)].CantidadStock -= compraDto.CantidadComprada;
                ArchivoProducto.Serializar(listaProductos);
                Compra compraData = new Compra()
                {
                    CodigoCompra = lista.Count() + 1,
                    CodigoProductoCompra = compraDto.CodigoProductoCompra,
                    DniClienteCompra = compraDto.DniClienteCompra,
                    FechaCompra = DateTime.Now,
                    CantidadComprada = compraDto.CantidadComprada,
                    FechaEntregaSolicitada = compraDto.FechaEntregaSolicitada,
                    EstadoCompra = (Enumerador.EnumeradorEstadoCompra)1,
                    MontoTotal = monto,
                    PuntoDestino = new Localizacion()
                    {
                        LatitudCliente = clienteTemp.LocalizacionCliente.LatitudCliente,
                        LongitudCliente = clienteTemp.LocalizacionCliente.LongitudCliente
                    },
                    FechaCreacion = DateTime.Now
                };
                compraData.TamañoCajaTotal = productoTemp.CalcularVolumen() * compraData.CantidadComprada;
                resultado.Success = true;
                resultado.ListaMensajes.Add($"Compra agregada correctamente. Codigo del producto comprado {compraDto.CodigoProductoCompra}, el dni del cliente {compraDto.DniClienteCompra}, con la siguiente cantidad comprada:{compraDto.CantidadComprada},con la fecha de entrega solicitada {compraDto.FechaEntregaSolicitada}");
                lista.Add(compraData);
                ArchivoCompra.Serializar(lista);
            }
            return resultado;
        }
        public List<CompraDto> GetListadoCompras()
        {
            List<CompraDto> lista = ArchivoCompra.Deserializar().Where(x => x.FechaEliminacion == null).Select(x => new CompraDto
            {
                FechaEntregaSolicitada = x.FechaEntregaSolicitada,
                CantidadComprada = x.CantidadComprada,
                DniClienteCompra = x.DniClienteCompra,
                CodigoProductoCompra = x.CodigoProductoCompra
            }).ToList();
            return lista;
        }
    }
}
