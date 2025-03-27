using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProyectoData
{
    public class Archivo <Clase>
    {
        public string Direccion { get; set; }
        public List<Clase> Data { get; set; }
        public List<Clase> Deserializar()
        {
            List<Clase> lista;
            string contenido = File.ReadAllText(Direccion);
            
            lista = JsonConvert.DeserializeObject<List<Clase>>(contenido);
            if (lista != null )
            {
                return lista;
            }
            else
            {
                return new List<Clase> { };
            }
        }
        public void Serializar(List<Clase> lista)
        {
            string contenido = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(Direccion, contenido);
        }
    }
}
