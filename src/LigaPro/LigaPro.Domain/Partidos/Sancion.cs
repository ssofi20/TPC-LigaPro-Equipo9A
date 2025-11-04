using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Sancion
    {
        public int Id { get; set; }
        public string Motivo { get; set; }
        public int FechasDeSuspension { get; set; }
        public int FechasCumplidas { get; set; }

        //Relaciones
        public int IdEquipoJugador { get; set; } //el sancionado
        public int IdPartidoOrigen { get; set; } //donde se generó

        //Navegación
        public virtual EquipoJugador Jugador { get; set; }
    }
}
