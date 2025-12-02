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
        public int IdTorneo { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; } // "Pendiente", "Finalizado"
        public string TipoPartido { get; set; } // "Grupo" o "Eliminatoria"

        // Resultados Base
        public int ResultadoEquipoA { get; set; }
        public int ResultadoEquipoB { get; set; }

        // Relaciones (IDs de Inscripciones)
        public int IdInscripcionA { get; set; }
        public int IdInscripcionB { get; set; }

        // Navegación (Para mostrar nombres)
        public string NombreLocal { get; set; }
        public string NombreVisita { get; set; }
    }
}
