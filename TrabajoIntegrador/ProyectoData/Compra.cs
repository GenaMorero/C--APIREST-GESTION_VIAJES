using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProyectoData.Enumerador;

namespace ProyectoData
{
    public class Compra : FechaBase
    {
        public int CodigoCompra { get; set; }
        public int CodigoProductoCompra { get; set; }
        public int DniClienteCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public int CantidadComprada { get; set; }
        public DateTime FechaEntregaSolicitada { get; set; }
        public EnumeradorEstadoCompra EstadoCompra { get; set; }
        public double MontoTotal { get; set; }
        public Localizacion PuntoDestino { get; set; }
        public double TamañoCajaTotal { get; set; }
        public double GetDistance()
        {
            double latitudLocal = -31.25033;
            double longintudLocal = -61.4867;
            double EarthRadius = 6371;
            double Lat = (PuntoDestino.LatitudCliente - latitudLocal) * (Math.PI / 180);
            double Lon = (PuntoDestino.LongitudCliente - longintudLocal) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(latitudLocal * (Math.PI / 180)) * Math.Cos(PuntoDestino.LatitudCliente * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = EarthRadius * c;
            return distance;
        }
    }
}
