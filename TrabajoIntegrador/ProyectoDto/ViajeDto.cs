using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDto
{
    public class ViajeDto
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public List<string> ValidarViaje()
        {
            List<string> list = new List<string>();
            if (FechaDesde<DateTime.Now)
            {
                list.Add("La fecha desde no puede ser menor a la de hoy");
            }
            if (FechaDesde.AddDays(7)==FechaHasta)
            {
                list.Add("La fecha hasta no puede ser mayor a 7 de la fecha desde");
            }
            return list;
        }
    }
}
