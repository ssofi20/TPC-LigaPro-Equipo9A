using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    public class EventoPartido
    {
        public int Id { get; set; }
        public int IdPartido { get; set; }
        public Partido Partido { get; set; }
        public int IdJugador { get; set; }
        public Jugador Jugador { get; set; }
        public TipoEvento Tipo { get; set; }
        public int MinutoJuego { get; set; }
    }
}
