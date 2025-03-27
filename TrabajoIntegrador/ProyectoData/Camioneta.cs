using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoData
{
    public class Camioneta : FechaBase
    {
        public string PatenteCamioneta { get; set; }
        public double TamañoCamioneta { get; set; }
        public int DistanciaMaxCamioneta { get; set; }
    }
}
