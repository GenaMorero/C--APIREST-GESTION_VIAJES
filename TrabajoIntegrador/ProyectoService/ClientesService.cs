using ProyectoData;
using ProyectoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService
{
    public class ClientesService
    {
        public ClientesService()
        {
            Archivo.Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaClientes.json");
        }
        public Archivo<Cliente> Archivo = new Archivo<Cliente>();
        public List<ClienteDto> DevolverListadoClientes()
        {
            List<ClienteDto> lista = Archivo.Deserializar().Where(x => x.FechaEliminacion == null).Select(x => new ClienteDto
            {
                DniCliente=x.DniCliente,
                NombreCliente=x.NombreCliente,
                ApellidoCliente=x.ApellidoCliente,
                EmailContactoCliente=x.EmailContactoCliente,
                TelefonoCliente=x.TelefonoCliente,
                LatitudCliente=x.LocalizacionCliente.LatitudCliente,
                LongitudCliente=x.LocalizacionCliente.LongitudCliente,
                FechaNacimientoCliente=x.FechaNacimientoCliente
            }).ToList();
            return lista;
        }
        public Resultado AgregarCliente(ClienteDto clienteDto)
        {
            Resultado resultado = new Resultado();
            List<Cliente> lista = Archivo.Deserializar();
            resultado.ListaMensajes = clienteDto.ValidarCliente();
            if (resultado.ListaMensajes.Count == 0)
            {
                if (lista.Exists(x => x.DniCliente == clienteDto.DniCliente))
                {
                    resultado.ListaMensajes.Add("El dni ingresado ya se encuentra en el sistema, no es valido");
                }
                else
                {
                    Cliente clienteData = new Cliente()
                    {
                        DniCliente = clienteDto.DniCliente,
                        NombreCliente = clienteDto.NombreCliente,
                        ApellidoCliente = clienteDto.ApellidoCliente,
                        EmailContactoCliente = clienteDto.EmailContactoCliente,
                        TelefonoCliente = clienteDto.TelefonoCliente,
                        LocalizacionCliente = new Localizacion()
                        {
                            LatitudCliente = clienteDto.LatitudCliente,
                            LongitudCliente = clienteDto.LongitudCliente,
                        },
                        FechaNacimientoCliente = clienteDto.FechaNacimientoCliente,
                        FechaCreacion = DateTime.Now
                    };
                    lista.Add(clienteData);
                    Archivo.Serializar(lista);
                    resultado.ListaMensajes.Add($"El cliente fue agregado correctamente. Con el Dni: {clienteDto.DniCliente}, con nombre y apellido: {clienteDto.NombreCliente},{clienteDto.NombreCliente}, con el email {clienteDto.EmailContactoCliente}, con localizacion{clienteDto.LongitudCliente},{clienteDto.LongitudCliente}, con fecha de nacimiento {clienteDto.FechaNacimientoCliente}");
                    resultado.Success = true;
                }
            }
            return resultado;
        }
        public Resultado EliminarCliente(int DniCliente)
        {
            Resultado resultado = new Resultado();
            List<Cliente> lista = Archivo.Deserializar();
            Cliente clienteTemp = lista.FirstOrDefault(x => x.DniCliente == DniCliente);
            if (clienteTemp != null)
            {
                if (clienteTemp.FechaEliminacion ==null)
                {
                    lista[lista.IndexOf(clienteTemp)].FechaEliminacion = DateTime.Now;
                    resultado.Success = true;
                    resultado.ListaMensajes.Add("Eliminado Correctamente");
                    Archivo.Serializar(lista);
                }
                else
                {
                    resultado.ListaMensajes.Add("El cliente ya fue eliminado previamente");
                }
            }
            else
            {
                resultado.ListaMensajes.Add("No se encontro el cliente");
            }
            return resultado;
        }
        public Resultado EditarCliente(ClienteDto clienteDto)
        {
            Resultado resultado = new Resultado();
            List<Cliente> lista = Archivo.Deserializar();
            Cliente clienteTemp = lista.FirstOrDefault(x => x.DniCliente == clienteDto.DniCliente);
            if (clienteTemp != null)
            {
                resultado.ListaMensajes = clienteDto.ValidarCliente();
                if (resultado.ListaMensajes.Count == 0)
                {
                    int index = lista.IndexOf(clienteTemp);
                    lista[index].DniCliente = clienteDto.DniCliente;
                    lista[index].NombreCliente = clienteDto.NombreCliente;
                    lista[index].ApellidoCliente = clienteDto.ApellidoCliente;
                    lista[index].EmailContactoCliente = clienteDto.EmailContactoCliente;
                    lista[index].TelefonoCliente = clienteDto.TelefonoCliente;
                    lista[index].LocalizacionCliente = new Localizacion()
                    {
                        LatitudCliente = clienteDto.LatitudCliente,
                        LongitudCliente = clienteDto.LongitudCliente,
                    };
                    lista[index].FechaActualizacion = DateTime.Now;
                    lista[index].FechaNacimientoCliente = clienteDto.FechaNacimientoCliente;
                    resultado.Success = true;
                    resultado.ListaMensajes.Add($"Editado Correctamente. Con el Dni: {clienteDto.DniCliente}, con nombre y apellido: {clienteDto.NombreCliente},{clienteDto.NombreCliente}, con el email {clienteDto.EmailContactoCliente}, con localizacion{clienteDto.LongitudCliente},{clienteDto.LongitudCliente}, con fecha de nacimiento {clienteDto.FechaNacimientoCliente}");
                    Archivo.Serializar(lista);
                }
            }
            else
            {
                resultado.ListaMensajes.Add("No se encontro el cliente");
            }
            return resultado;
        }
    }
}
