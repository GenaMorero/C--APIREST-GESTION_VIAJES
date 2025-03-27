using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDto
{
    public class ProductoDto
    {
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public double AltoCaja { get; set; }
        public double AnchoCaja { get; set; }
        public double ProfundidadCaja { get; set; }
        public double PrecioProducto { get; set; }
        public int StockMinimo { get; set; }
        public int CantidadStock { get; set; }
        public List<string> ValidacionNulosProducto()
        {
            List<string> lista =new List<string>();
            if (String.IsNullOrEmpty(NombreProducto))
            {
                lista.Add("Nombre ingresado incorrectamente");
            }
            if (String.IsNullOrEmpty(Marca))
            {
                lista.Add("Marca ingresada incorrectamente");
            }
            if (AltoCaja==0)
            {
                lista.Add("Alto de la caja ingresada incorrectamente");
            }
            if (AnchoCaja==0) 
            {
                lista.Add("Ancho de la caja ingresado incorrectamente");
            }
            if (ProfundidadCaja==0)
            {
                lista.Add("Profundidad de la caja ingresado incorrectamente");
            }
            if (PrecioProducto == 0)
            {
                lista.Add("Precio del producto ingresado incorrectamente");
            }
            if (StockMinimo == 0)
            {
                lista.Add("Stock minimo ingresado incorrectamente");
            }
            return lista;
        }
    }
}
