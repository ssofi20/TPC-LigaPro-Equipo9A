using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Torneo : Competicion
    {
        public bool TieneFaseDeGrupos { get; set; }
        public List<Fase> Fases { get; set; }
    }
}
