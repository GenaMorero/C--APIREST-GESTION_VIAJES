using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoData
{
    public class Cliente : FechaBase
    {
        public int DniCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string EmailContactoCliente { get; set; }
        public int TelefonoCliente { get; set; }
        public Localizacion LocalizacionCliente { get; set; }
        public DateTime FechaNacimientoCliente { get; set; }
    }
}
