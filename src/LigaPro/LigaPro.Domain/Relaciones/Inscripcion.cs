using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Inscripcion
    {
        public int Id { get; set; }

        //Relaciones
        public int IdCompetencia { get; set; }
        public int IdEquipo { get; set; }

        //Navegacion
        public Equipo Equipo { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public List<EquipoJugador> Plantel { get; set; }
    }
}
