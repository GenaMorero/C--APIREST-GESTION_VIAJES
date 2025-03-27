using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoData
{
    public class Viaje : FechaBase
    {
        public int CodigoViaje { get; set; }
        public int IdCamionAsignado { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public double PorcentajeOcupacion { get; set; }
        public List<int> CodigoCompras { get; set; }
    }
}
