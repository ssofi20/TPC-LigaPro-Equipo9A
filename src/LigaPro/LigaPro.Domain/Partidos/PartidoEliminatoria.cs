using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class PartidoEliminatoria : Partido
    {
        public int? IdCruce { get; set; } // Octavos, Cuartos...

        // Penales y tiempo extra
        public int? GolesA_Extra { get; set; }
        public int? GolesB_Extra { get; set; }
        public int? PenalesA { get; set; }
        public int? PenalesB { get; set; }

        public PartidoEliminatoria()
        {
            this.TipoPartido = "Eliminatoria";
        }
    }
}
