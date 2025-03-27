using ProyectoData;
using ProyectoDto;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;

namespace ProyectoService
{
    public class ProductoService
    {
        public ProductoService()
        {
            Archivo.Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaProductos.json");
        }
        public Archivo<Producto> Archivo = new Archivo<Producto>();
        public Resultado AgregarProducto(ProductoDto producto)
        {
            Resultado resultado = new Resultado();
            resultado.ListaMensajes = producto.ValidacionNulosProducto();
            List<Producto> lista = Archivo.Deserializar();
            if (lista.Exists(x=>x.NombreProducto==producto.NombreProducto && x.Marca==producto.Marca))
            {
                resultado.ListaMensajes.Add("Ya existe un producto con ese nombre y marca");
            }
            if (resultado.ListaMensajes.Count == 0)
            {
                Producto productoF = new Producto()
                {
                    NombreProducto = producto.NombreProducto,
                    ProfundidadCaja = producto.ProfundidadCaja,
                    AnchoCaja = producto.AnchoCaja,
                    AltoCaja = producto.AltoCaja,
                    Marca = producto.Marca,
                    StockMinimo = producto.StockMinimo,
                    CantidadStock = producto.CantidadStock,
                    PrecioProducto = producto.PrecioProducto,
                    CodigoProducto = lista.Count() + 1,
                    FechaCreacion = DateTime.Now
                };
                lista.Add(productoF);
                Archivo.Serializar(lista);
                resultado.Success = true;
                resultado.ListaMensajes.Add($"Producto agregado correctamente. Con el nombre:{producto.NombreProducto}, con la marca: {producto.Marca}, con alto, ancho y profundidad: {producto.AnchoCaja}, {producto.AnchoCaja}, {producto.ProfundidadCaja}, con el precio unitario: {producto.PrecioProducto}, con el stock minimo: {producto.StockMinimo}, y con la siguiente cantidad en stock {producto.CantidadStock}");
            }
                return resultado;
        }
        public Resultado ActualizarStockProducto(int idProducto, int stock)
        {
            Resultado resultado = new Resultado();
            List<Producto> lista = Archivo.Deserializar();
            if (stock<=0)
            {
                resultado.ListaMensajes.Add("El stock a agregar no puede ser 0 o menor 0");
            }
            if (lista.FirstOrDefault(x => x.CodigoProducto == idProducto) == null) 
            {
                resultado.ListaMensajes.Add("El id ingresado no corresponde a ningun producto");
            }   
            if (resultado.ListaMensajes.Count==0)
            {
                lista[idProducto - 1].FechaActualizacion = DateTime.Now;
                lista[idProducto - 1].CantidadStock += stock;
                resultado.Success = true;
                resultado.ListaMensajes.Add($"Stock actualizado, hay {lista[idProducto-1].CantidadStock} en stock");
                Archivo.Serializar(lista);
            }
            return resultado;
        }
        public List<ProductoDto> GetListadoProducto()
        {
            List<ProductoDto> lista = Archivo.Deserializar().Where(x => x.FechaEliminacion == null).Select(x => new ProductoDto
            {
                NombreProducto=x.NombreProducto,
                Marca=x.Marca,
                AltoCaja=x.AltoCaja,
                AnchoCaja=x.AnchoCaja,
                ProfundidadCaja=x.ProfundidadCaja,
                PrecioProducto=x.PrecioProducto,
                StockMinimo=x.StockMinimo,
                CantidadStock=x.CantidadStock
            }).ToList();
            return lista;

        }
    }
}

