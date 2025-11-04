using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class EventoPartido
    {
        public int Id { get; set; }
        public TipoEvento Tipo { get; set; }
        public int MinutoJuego { get; set; }

        // Relaciones
        public int IdPartido { get; set; }
        public int IdEquipoJugador { get; set; } //Quien lo hizo
        public int? IdJugadorAsistencia { get; set; } // Nullable, para asistencias
        public Partido Partido { get; set; }
        public EquipoJugador Jugador { get; set; }
    }
}
