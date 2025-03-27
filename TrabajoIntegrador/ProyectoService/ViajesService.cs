using ProyectoData;
using ProyectoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService
{
    public class ViajesService
    {
        public ViajesService()
        {
            ArchivoViaje.Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaViajes.json");
            ArchivoCompra.Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaCompras.json");
            ArchivoCamioneta.Direccion = Path.GetFullPath("..\\ProyectoData\\Listas\\ListaCamionetas.json");
        }
        private Archivo<Viaje> ArchivoViaje = new Archivo<Viaje>();
        private Archivo<Compra> ArchivoCompra = new Archivo<Compra>();
        private Archivo<Camioneta> ArchivoCamioneta = new Archivo<Camioneta>();
        public Resultado AgregarViaje(ViajeDto viajeDto)
        {
            Resultado resultado = new Resultado();
            resultado.ListaMensajes = viajeDto.ValidarViaje();
            if (resultado.ListaMensajes.Count == 0)
            {
                List<Viaje> listaViajes = ArchivoViaje.Deserializar();
                foreach (Viaje item in listaViajes)
                {
                    if ((viajeDto.FechaDesde <= item.FechaDesde && viajeDto.FechaHasta >= item.FechaDesde) ||
                        (viajeDto.FechaDesde >= item.FechaDesde && viajeDto.FechaDesde <= item.FechaHasta))
                    {
                        resultado.ListaMensajes.Add("Ya hay un viaje asignado en estas fechas");
                        break;
                    }
                }
                List<Compra> listaCompra = ArchivoCompra.Deserializar();
                if (listaCompra.Count == 0)
                {
                    resultado.ListaMensajes.Add("No hay compras para armar viajes");
                }
                if (resultado.ListaMensajes.Count == 0)
                {
                    List<Camioneta> listaCamionetas = ArchivoCamioneta.Deserializar();
                    listaCompra.OrderBy(x => x.TamañoCajaTotal);
                    double distancia = 0;
                    double capacidad = 0;
                    int c = 0;
                    int codigoViaje=1;
                    if (listaViajes.Count>0)
                    {
                        codigoViaje = listaViajes.Last().CodigoViaje + 1;
                    }
                    for (int i = 0; i < listaCamionetas.Count; i++)
                    {
                        Viaje viajeTemp = new Viaje();
                        viajeTemp.CodigoViaje = codigoViaje;
                        viajeTemp.CodigoCompras = new List<int>();
                        if (i != 0)
                        {
                            viajeTemp.CodigoCompras.Clear();
                        }
                        c = c + 1;
                        double capacidadOcupada = 0;
                        switch (i)
                        {
                            case 0:
                                capacidad = 3300;
                                distancia = 350;
                                break;
                            case 1:
                                capacidad = 5800;
                                distancia = 550;
                                break;
                            case 2:
                                capacidad = 6700;
                                distancia = 750;
                                break;
                        }
                        foreach (Compra item in listaCompra)
                        {
                            if ((capacidadOcupada + item.TamañoCajaTotal) > capacidad)
                            {
                                break;
                            }
                            double valor = item.GetDistance();
                            if (item.GetDistance() < distancia && ((capacidadOcupada + item.TamañoCajaTotal) < capacidad && item.EstadoCompra == Enumerador.EnumeradorEstadoCompra.Open) 
                                &&item.FechaEntregaSolicitada>=viajeDto.FechaDesde && item.FechaEntregaSolicitada<=viajeDto.FechaHasta)
                            {
                                viajeTemp.CodigoCompras.Add(item.CodigoCompra);
                                item.EstadoCompra = Enumerador.EnumeradorEstadoCompra.Ready_To_Dispatch;
                                capacidadOcupada += item.TamañoCajaTotal;
                            }
                        }
                        if (capacidadOcupada != 0)
                        {
                            viajeTemp.PorcentajeOcupacion =(capacidadOcupada * 100) / capacidad;
                            viajeTemp.FechaDesde = viajeDto.FechaDesde;
                            viajeTemp.FechaHasta = viajeDto.FechaHasta;
                            viajeTemp.FechaCreacion = DateTime.Now;
                            viajeTemp.IdCamionAsignado = c;
                            listaViajes.Add(viajeTemp);
                            resultado.ListaMensajes.Add($"Viaje {i + 1} asignado correctamente, con Fechas {viajeTemp.FechaDesde}, {viajeTemp.FechaHasta}, en el camion {viajeTemp.IdCamionAsignado}, con el siguiente porcentaje de ocupacion{viajeTemp.PorcentajeOcupacion}");
                        }
                    }
                    foreach (Compra item in listaCompra)
                    {
                        if (item.EstadoCompra == Enumerador.EnumeradorEstadoCompra.Open && item.FechaEntregaSolicitada > viajeDto.FechaDesde && item.FechaEntregaSolicitada < viajeDto.FechaHasta)
                        {
                            item.FechaEntregaSolicitada.AddDays(14);
                        }
                    }
                    resultado.Success = true;
                    ArchivoCompra.Serializar(listaCompra);
                    ArchivoViaje.Serializar(listaViajes);

                }
            }
            return resultado;
        }
        public List<Viaje> GetViajes()
        {
            return ArchivoViaje.Deserializar();
        }
    }
}