using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigaPro.Domain;

namespace LigaPro.Datos
{
    public class DeporteDatos
    {
        public List<Deporte> Listar()
        {
            List<Deporte> lista = new List<Deporte>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdDeporte, Nombre, CantidadJugadores, PermiteEmpate FROM DEPORTES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Deporte aux = new Deporte();
                    aux.Id = (int)datos.Lector["IdDeporte"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.CantJugadores = (int)datos.Lector["CantidadJugadores"];
                    aux.PermiteEmpate = (bool)datos.Lector["PermiteEmpate"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
