using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigaPro.Datos;
using LigaPro.Domain;
using LigaPro.Domain.Actores;
using LigaPro.Domain.Relaciones;

namespace LigaPro.Negocio
{
    public class EquipoNegocio
    {
        private EquipoDatos datos = new EquipoDatos();

        // CREAR UN EQUIPO NUEVO
        public void CrearEquipo(Equipo equipo)
        {
            EquipoDatos datos = new EquipoDatos();
            datos.InsertarEquipo(equipo);
        }

        // LISTAR EQUIPOS POR USUARIO CREADOR
        public List<Equipo> ListarEquiposPorCreador(int idUsuario)
        {
            return datos.ListarEquiposPorCreador(idUsuario);
        }

        // OBTENER EQUIPO POR ID
        public Equipo ObtenerEquipoPorId(int id)
        {
            return datos.ObtenerEquipoPorId(id);
        }

        // LISTAR JUGADORES DE UN EQUIPO
        public List<JugadorEquipo> ListarJugadoresDeEquipo(int idEquipo)
        {
            return datos.ListarJugadoresDeEquipo(idEquipo);
        }

        // LISTAR SOLICITUDES PENDIENTES DE UN EQUIPO
        public List<Solicitud> ListarSolicitudesPendientes(int idEquipo)
        {
            EquipoNegocio negocio = new EquipoNegocio();

            return datos.ListarSolicitudesPendientes(idEquipo);
        }

        // ACTUALIZAR INFORMACIÓN DE UN EQUIPO
        public bool ActualizarEquipo(Equipo equipo)
        {
            return datos.ModificarEquipo(equipo);
        }

        // ELIMINAR UN EQUIPO
        public bool EliminarEquipo(int idEquipo)
        {
            return datos.EliminarEquipo(idEquipo);
        }

        // AGREGAR JUGADOR AL EQUIPO POR SOLICITUD ACEPTADA
        public void AceptarSolicitud(int idUsuario, int idEquipo)
        {
            datos.AgregarJugadorAEquipo(idUsuario, idEquipo);
            datos.ActualizarEstadoSolicitud(idUsuario, idEquipo, "Aceptada");
        }

        // RECHAZAR SOLICITUD DE UN JUGADOR
        public void RechazarSolicitud(int idUsuarioSolicitante, int IdEquipoSeleccionado)
        {
            datos.RechazarSolicitud(idUsuarioSolicitante, IdEquipoSeleccionado);
            datos.ActualizarEstadoSolicitud(idUsuarioSolicitante, IdEquipoSeleccionado, "Rechazada");
        }

        // ELIMINAR JUGADOR DE UN EQUIPO
        public void EliminarJugadorDeEquipo(int idUsuario, int idEquipo)
        {
            datos.EliminarJugadorDeEquipo(idUsuario, idEquipo);
        }

        // ACTUALIZAR DATOS DE UN JUGADOR EN EL EQUIPO
        public void ActualizarDatosJugadorEquipo(EquipoJugador jugadorEquipo)
        {
            datos.ActualizarDatosJugadorEnEquipo(jugadorEquipo);
        }

        // LISTAR TODOS LOS EQUIPOS
        public List<Equipo> ListarTodosLosEquipos(int idUsuario)
        {
            return datos.ListarTodosLosEquipos(idUsuario);
        }

        // BUSCAR EQUIPOS CON FILTRO AVANZADO
        public List<Equipo> BuscarEquipos(string busqueda, int idUsuario)
        {
            if (string.IsNullOrEmpty(busqueda)) return new List<Equipo>();
            return datos.BuscarEquipos(busqueda, idUsuario);
        }

        // CREAR SOLICITUD
        public void CrearSolicitud(int idUsuario, int idEquipo)
        {
            datos.CrearSolicitud(idUsuario, idEquipo);
        }
    }
}
