using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class PartidoEliminatoria : Partido
    {
        // Propiedades específicas de PartidoEliminatoria 
        // (Nulleables porque no siempre se juegan tiempos extra o penales)
        public int? GolesA_Extra { get; set; }
        public int? GolesB_Extra { get; set; }
        public int? PenalesA { get; set; }
        public int? PenalesB { get; set; }

        // Relación
        public int IdCruce { get; set; }

        // Propiedad de Navegación
        public Cruce Cruce { get; set; }
    }
}
