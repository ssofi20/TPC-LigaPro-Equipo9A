using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigaPro.Datos;
using LigaPro.Domain.Actores;

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
        public List<EquipoJugador> ListarJugadoresDeEquipo(int idEquipo)
        {
            List<EquipoJugador> list = new List<EquipoJugador>();
            return list;
            //return datos.ListarJugadoresDeEquipo(idEquipo);
        }

        // AGREGAR JUGADOR A EQUIPO
        public void AgregarJugadorAEquipo(EquipoJugador nuevoIntegrante)
        {
            // Podrías validar que el jugador no esté ya en el equipo
            //datos.InsertarJugadorEnEquipo(nuevoIntegrante);
        }
    }
}
