using LigaPro.Domain.Actores;
using LigaPro.Domain.Relaciones;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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

        // LISTAR TODOS LOS EQUIPOS DE UN USUARIO ACTIVOS
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

        // LISTAR TODOS LOS JUGADORES DE UN EQUIPO
        public List<JugadorEquipo> ListarJugadoresDeEquipo(int idEquipo)
        {
            List<JugadorEquipo> lista = new List<JugadorEquipo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT J.IdJugador, J.Nombres, J.Apellidos, EJ.NumeroCamiseta, EJ.Posicion, EJ.EsCapitan\r\nFROM EquipoJugador EJ\r\nINNER JOIN Jugadores J ON EJ.IdJugador = J.IdJugador\r\nWHERE EJ.IdEquipo = @idEquipo");
                datos.setearParametro("@idEquipo", idEquipo);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    JugadorEquipo aux = new JugadorEquipo();

                    // IDs y Textos obligatorios (asumimos que siempre están)
                    aux.IdJugador = (int)datos.Lector["IdJugador"];

                    // Validación de Nombres (por si acaso viniera null)
                    object nombres = datos.Lector["Nombres"];
                    aux.Nombres = nombres != DBNull.Value ? (string)nombres : "";

                    object apellidos = datos.Lector["Apellidos"];
                    aux.Apellidos = apellidos != DBNull.Value ? (string)apellidos : "";

                    // --- VALIDACIÓN DE VALORES NULOS (Aquí estaba el error) ---

                    // 1. Camiseta: Si es NULL en BD, ponemos 0
                    if (datos.Lector["NumeroCamiseta"] != DBNull.Value)
                        aux.NumeroCamiseta = (int)datos.Lector["NumeroCamiseta"];
                    else
                        aux.NumeroCamiseta = 0; // Valor por defecto

                    // 2. Posición: Si es NULL en BD, ponemos un guión
                    if (datos.Lector["Posicion"] != DBNull.Value)
                        aux.Posicion = (string)datos.Lector["Posicion"];
                    else
                        aux.Posicion = "-"; // Valor por defecto

                    // 3. Capitán: Si es NULL en BD, ponemos false
                    if (datos.Lector["EsCapitan"] != DBNull.Value)
                        aux.EsCapitan = (bool)datos.Lector["EsCapitan"];
                    else
                        aux.EsCapitan = false; // Valor por defecto

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

        // LISTAR SOLICITUDES PENDIENTES DE UN EQUIPO
        public List<Solicitud> ListarSolicitudesPendientes(int idEquipo)
        {
            List<Solicitud> lista = new List<Solicitud>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT S.Id, S.IdUsuario, S.IdEquipo, S.FechaSolicitud, S.Estado , J.Nombres, J.Apellidos\r\nFROM Solicitudes S\r\nJOIN Usuarios U ON S.IdUsuario = U.IdUsuario\r\nJOIN Jugadores J ON U.IdUsuario = J.IdUsuarioJugador\r\nWHERE S.IdEquipo = @idEquipo AND S.Estado = 'Pendiente'");
                datos.setearParametro("@idEquipo", idEquipo);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Solicitud aux = new Solicitud();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.IdEquipo = (int)datos.Lector["IdEquipo"];
                    aux.FechaCreacion = (DateTime)datos.Lector["FechaSolicitud"];
                    aux.Estado = (string)datos.Lector["Estado"];
                    aux.Nombre = (string)datos.Lector["Nombres"];
                    aux.Apellido = (string)datos.Lector["Apellidos"];
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

        // ACTUALIZAR LOS DATOS DE UN EQUIPO
        public bool ModificarEquipo(Equipo equipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Equipos SET Nombre = @nombre, Imagen = @imagen WHERE IdEquipo = @idEquipo");
                datos.setearParametro("@nombre", equipo.Nombre);
                datos.setearParametro("@imagen", (object)equipo.Imagen ?? DBNull.Value);
                datos.setearParametro("@idEquipo", equipo.Id);
                datos.ejecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ELIMINACION LÓGICA DE UN EQUIPO
        public bool EliminarEquipo(int idEquipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Equipos SET Activo = 0 WHERE IdEquipo = @Id");
                datos.setearParametro("@Id", idEquipo);
                datos.ejecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // AGREGAR UN JUGADOR A UN EQUIPO (ACEPTAR SOLICITUD)
        public void AgregarJugadorAEquipo(int idUsuario, int idEquipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO EquipoJugador (IdEquipo, IdJugador) SELECT @idEquipo, IdJugador FROM Jugadores WHERE IdUsuarioJugador = @idUsuario");
                datos.setearParametro("@idEquipo", idEquipo);
                datos.setearParametro("@idUsuario", idUsuario);
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

        // ACTUALIZAR EL ESTADO DE UNA SOLICITUD
        public void ActualizarEstadoSolicitud(int idUsuario, int idEquipo, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "UPDATE Solicitudes SET Estado = @estado WHERE IdUsuario = @idUsuario AND IdEquipo = @idEquipo";

                datos.setearConsulta(consulta);
                datos.setearParametro("@estado", nuevoEstado);
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idEquipo", idEquipo);

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

        // RECHAZAR SOLICITUD DE UN JUGADOR
        public void RechazarSolicitud(int idUsuario, int idEquipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Solicitudes SET Estado = 'Rechazada' WHERE IdUsuario = @idUsuario AND IdEquipo = @idEquipo");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idEquipo", idEquipo);
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

        // ELIMINAR JUGADOR DE UN EQUIPO
        public void EliminarJugadorDeEquipo(int idUsuario, int idEquipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM EquipoJugador WHERE IdEquipo = @idEquipo AND IdJugador = (SELECT IdJugador FROM Jugadores WHERE IdJugador = @idUsuario)");
                datos.setearParametro("@idEquipo", idEquipo);
                datos.setearParametro("@idUsuario", idUsuario);
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

        // ACTUALIZAR DETALLES DE UN JUGADOR EN UN EQUIPO
        public void ActualizarDatosJugadorEnEquipo(EquipoJugador detalles)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE EquipoJugador SET NumeroCamiseta = @numeroCamiseta, Posicion = @posicion, EsCapitan = @esCapitan WHERE IdEquipo = @idEquipo AND IdJugador = @idJugador");
                datos.setearParametro("@numeroCamiseta", detalles.NumeroCamiseta);
                datos.setearParametro("@posicion", detalles.Posicion);
                datos.setearParametro("@esCapitan", detalles.EsCapitan);
                datos.setearParametro("@idEquipo", detalles.IdEquipo);
                datos.setearParametro("@idJugador", detalles.IdJugador);
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

        // LISTAR TODOS LOS EQUIPOS ACTIVOS
        public List<Equipo> ListarTodosLosEquipos(int idUsuario)
        {
            List<Equipo> lista = new List<Equipo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT E.IdEquipo, E.IdUsuarioCreador, E.Nombre, E.Imagen, J.Nombres, J.Apellidos,\r\n                    (\r\n                        CASE \r\n                            WHEN EXISTS (\r\n                                SELECT 1 FROM EquipoJugador EJ \r\n                                INNER JOIN Jugadores JUG ON EJ.IdJugador = JUG.IdJugador \r\n                                WHERE EJ.IdEquipo = E.IdEquipo AND JUG.IdUsuarioJugador = @idUsuario\r\n                            ) THEN 'Aceptada'\r\n                    \r\n                            WHEN EXISTS (\r\n                                SELECT 1 FROM Solicitudes S \r\n                                WHERE S.IdEquipo = E.IdEquipo AND S.IdUsuario = @idUsuario AND S.Estado = 'Pendiente'\r\n                            ) THEN 'Pendiente'\r\n                    \r\n                            ELSE NULL\r\n                        END\r\n                    ) AS EstadoSolicitud\r\n                    FROM Equipos E\r\n                    INNER JOIN Jugadores J ON E.IdUsuarioCreador = J.IdUsuarioJugador\r\n                    WHERE E.Activo = 1");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Equipo aux = new Equipo();
                    aux.Id = (int)datos.Lector["IdEquipo"];
                    aux.IdUsuarioCreador = (int)datos.Lector["IdUsuarioCreador"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    // Validamos nulos
                    if (datos.Lector["EstadoSolicitud"] != DBNull.Value)
                        aux.EstadoSolicitud = (string)datos.Lector["EstadoSolicitud"];
                    else
                        aux.EstadoSolicitud = null;

                    aux.NombreCreador = (string)datos.Lector["Nombres"] + " " + (string)datos.Lector["Apellidos"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.Imagen = (string)datos.Lector["Imagen"];
                    else
                        aux.Imagen = "/Uploads/default-team.png";

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // BUSCAR EQUIPOS POR CRITERIO
        public List<Equipo> BuscarEquipos(string busqueda, int idUsuario)
        {
            List<Equipo> lista = new List<Equipo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT E.IdEquipo, E.IdUsuarioCreador, E.Nombre, E.Imagen, J.Nombres, J.Apellidos,\r\n                    (\r\n                        CASE \r\n                            WHEN EXISTS (\r\n                                SELECT 1 FROM EquipoJugador EJ \r\n                                INNER JOIN Jugadores JUG ON EJ.IdJugador = JUG.IdJugador \r\n                                WHERE EJ.IdEquipo = E.IdEquipo AND JUG.IdUsuarioJugador = @idUsuario\r\n                            ) THEN 'Aceptada'\r\n                    \r\n                            WHEN EXISTS (\r\n                                SELECT 1 FROM Solicitudes S \r\n                                WHERE S.IdEquipo = E.IdEquipo AND S.IdUsuario = @idUsuario AND S.Estado = 'Pendiente'\r\n                            ) THEN 'Pendiente'\r\n                    \r\n                            ELSE NULL\r\n                        END\r\n                    ) AS EstadoSolicitud\r\n                    FROM Equipos E\r\n                    INNER JOIN Jugadores J ON J.IdUsuarioJugador = E.IdUsuarioCreador\r\n                    WHERE E.Activo = 1 \r\n                    AND (E.Nombre LIKE @busqueda + '%' OR (J.Nombres + ' ' + J.Apellidos) LIKE '%' + @busqueda + '%')");
                datos.setearParametro("@busqueda", busqueda);
                datos.setearParametro("@idUsuario", idUsuario); // <--- IMPORTANTE: Asegúrate de pasar este parámetro
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    // ... (El mapeo es idéntico al método anterior) ...
                    Equipo aux = new Equipo();
                    aux.Id = (int)datos.Lector["IdEquipo"];
                    aux.IdUsuarioCreador = (int)datos.Lector["IdUsuarioCreador"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    if (datos.Lector["EstadoSolicitud"] != DBNull.Value)
                        aux.EstadoSolicitud = (string)datos.Lector["EstadoSolicitud"];
                    else
                        aux.EstadoSolicitud = null;

                    aux.NombreCreador = (string)datos.Lector["Nombres"] + " " + (string)datos.Lector["Apellidos"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.Imagen = (string)datos.Lector["Imagen"];
                    else
                        aux.Imagen = "/Uploads/default-team.png";

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // CREAR SOLICITUD DE UN JUGADOR A UN EQUIPO
        public void CrearSolicitud(int idUsuario, int idEquipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("IF NOT EXISTS (SELECT 1 FROM EquipoJugador WHERE IdEquipo = @idEquipo AND IdJugador = (SELECT IdJugador FROM Jugadores WHERE IdUsuarioJugador = @idUsuario))\r\nAND NOT EXISTS (SELECT 1 FROM Solicitudes WHERE IdUsuario = @idUsuario AND IdEquipo = @idEquipo AND Estado = 'Pendiente')\r\nBEGIN\r\n    INSERT INTO Solicitudes (IdUsuario, IdEquipo, Estado, FechaSolicitud) \r\n    VALUES (@idUsuario, @idEquipo, 'Pendiente', GETDATE())\r\nEND");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idEquipo", idEquipo);
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

        public List<Equipo> listarEquiposPorTorneo(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Equipo> lista = new List<Equipo>();
            try
            {
                datos.setearConsulta("SELECT E.IdEquipo, E.Nombre, E.Imagen FROM Inscripciones I INNER JOIN Equipos E ON I.IdEquipo = E.IdEquipo WHERE I.IdTorneo = @IdTorneo;");
                datos.setearParametro("@IdTorneo", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Equipo aux = new Equipo();
                    aux.Id = (int)datos.Lector["IdEquipo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.Imagen = (string)datos.Lector["Imagen"];
                    else
                        aux.Imagen = "/Uploads/default-team.png";

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Equipo> Listar()
        {
            List<Equipo> lista = new List<Equipo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT E.IdEquipo, E.IdUsuarioCreador, E.Nombre, E.Imagen, J.Nombres, J.Apellidos FROM Equipos E INNER JOIN Jugadores J ON J.IdUsuarioJugador = E.IdUsuarioCreador WHERE E.Activo = 1;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Equipo aux = new Equipo();
                    aux.Id = (int)datos.Lector["IdEquipo"];
                    aux.IdUsuarioCreador = (int)datos.Lector["IdUsuarioCreador"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    aux.NombreCreador = (string)datos.Lector["Nombres"] + " " + (string)datos.Lector["Apellidos"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.Imagen = (string)datos.Lector["Imagen"];
                    else
                        aux.Imagen = "/Uploads/default-team.png";

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }
    }
}
