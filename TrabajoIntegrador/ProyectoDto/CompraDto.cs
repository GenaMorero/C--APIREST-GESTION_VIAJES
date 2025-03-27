using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDto
{
    public class CompraDto
    {
        public int CodigoProductoCompra { get; set; }
        public int DniClienteCompra { get; set; }
        public int CantidadComprada { get; set; }
        public DateTime FechaEntregaSolicitada { get; set; }
        public List<string> ValidacionCompraDto()
        {
            List<string> listaErrores = new List<string>();
            if (CantidadComprada==0) 
            {
                listaErrores.Add("La cantidad comprada no puede ser 0");
            }
            if (FechaEntregaSolicitada<DateTime.Now)
            {
                listaErrores.Add("La fecha de entrega solicitada no puede ser menor a la actual");
            }
            return listaErrores;
        }
    }
}
