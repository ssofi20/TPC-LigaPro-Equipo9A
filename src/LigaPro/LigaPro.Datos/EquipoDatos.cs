using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LigaPro.Datos
{
    public class EquipoDatos
    {
        // INSERTAR UN EQUIPO A LA BASE DE DATOS
        public void InsertarEquipo(Equipo nuevoEquipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string query = "INSERT INTO Equipos(IdUsuarioCreador, Nombre, Imagen) VALUES (@idCreador, @nombre, @imagen)";
                datos.setearConsulta(query);
                datos.setearParametro("@idCreador", nuevoEquipo.IdUsuarioCreador);
                datos.setearParametro("@nombre", nuevoEquipo.Nombre);
                datos.setearParametro("@imagen", (object)nuevoEquipo.Imagen ?? DBNull.Value);
                datos.ejecutarAccion();
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

        // LISTAR TODOS LOS EQUIPOS DE UN USUARIO
        public List<Equipo> ListarEquiposPorCreador(int idUsuarioCreador)
        {
            List<Equipo> lista = new List<Equipo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdEquipo, Nombre, Imagen FROM Equipos WHERE IdUsuarioCreador = @IdUsuario");

                datos.setearParametro("@IdUsuario", idUsuarioCreador);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Equipo aux = new Equipo();
                    aux.Id = (int)datos.Lector["IdEquipo"];
                    aux.IdUsuarioCreador = idUsuarioCreador;
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.Imagen = (string)datos.Lector["Imagen"];
                    else
                        aux.Imagen = "/Uploads/default-team.png";

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

        // OBTENER UN EQUIPO POR SU ID
        public Equipo ObtenerEquipoPorId(int idEquipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdEquipo, IdUsuarioCreador, Nombre, Imagen FROM Equipos WHERE IdEquipo = @idEquipo");
                datos.setearParametro("@idEquipo", idEquipo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Equipo aux = new Equipo();
                    aux.Id = idEquipo;
                    aux.IdUsuarioCreador = (int)datos.Lector["IdUsuarioCreador"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.Imagen = (string)datos.Lector["Imagen"];
                    return aux;
                }
                return null;
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
