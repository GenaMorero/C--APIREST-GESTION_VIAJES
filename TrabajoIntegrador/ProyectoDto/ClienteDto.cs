using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDto
{
    public class ClienteDto
    {
        public int DniCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string EmailContactoCliente { get; set; }
        public int TelefonoCliente { get; set; }
        public double LatitudCliente { get; set; }
        public double LongitudCliente { get; set; }
        public DateTime FechaNacimientoCliente { get; set; }
        public List<string> ValidarCliente()
        {
            List<string> lista = new List<string>();
            if (DniCliente <= 0)
            {
                lista.Add("El dni ingresado es invalido");
            }
            if (string.IsNullOrEmpty(NombreCliente))
            {
                lista.Add("El nombre ingresado es invalido");
            }
            if (string.IsNullOrEmpty(ApellidoCliente))
            {
                lista.Add("El apellido ingresado es invalido");
            }
            if (string.IsNullOrEmpty(EmailContactoCliente))
            {
                lista.Add("El email ingresado es invalido");
            }
            if (TelefonoCliente==0)
            {
                lista.Add("El telefono ingresado es invalido");
            }
            if (FechaNacimientoCliente== DateTime.MinValue)
            {
                lista.Add("Fecha de nacimiento invalida");
            }
            return lista;
        }
    }
}
