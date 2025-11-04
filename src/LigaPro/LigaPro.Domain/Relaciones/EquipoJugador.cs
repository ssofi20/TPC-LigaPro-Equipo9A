using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores   
{
    public class EquipoJugador
    {
        public int Id { get; set; }
        public int NumeroCamiseta { get; set; }
        public string Posicion { get; set; }
        public bool EsCapitan { get; set; }

        // Relaciones
        public int IdEquipo { get; set; }
        public int IdJugador { get; set; }

        //Navegación
        public Equipo Equipo { get; set; }
        public Jugador Jugador { get; set; }
        public List<EventoPartido> Eventos { get; set; }
        public List<Sancion> Sanciones { get; set; }
    }
}
