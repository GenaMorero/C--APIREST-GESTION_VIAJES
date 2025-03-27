namespace ProyectoData
{
    public class Producto : FechaBase
    {
        public int CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public double AltoCaja { get; set; }
        public double AnchoCaja { get; set; }
        public double ProfundidadCaja { get; set;}
        public double PrecioProducto { get; set; }
        public int StockMinimo { get; set; }
        public int CantidadStock { get; set; }
        public double CalcularVolumen()
        {
            return AltoCaja* AnchoCaja * ProfundidadCaja;
        }
    }
}