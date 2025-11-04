using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Partido
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoPartido Estado { get; set; }
        public int ResultadoEquipoA { get; set; }
        public int ResultadoEquipoB { get; set; }

        // Relaciones
        public int IdCompeticion { get; set; }
        public int IdInscripcionA { get; set; }
        public int IdInscripcionB { get; set; }

        // Navegación
        public Inscripcion EquipoA { get; set; }
        public Inscripcion EquipoB { get; set; }
        public List<EventoPartido> Eventos { get; set; }
    }
}
