using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    public class EquipoJugador
    {
        public int IdEquipo { get; set; }
        public Equipo Equipo { get; set; }
        public int IdJugador { get; set; }
        public Jugador Jugador { get; set; }
        public bool EsCapitan { get; set; }
        public int NumeroCamiseta { get; set; }
    }
}
