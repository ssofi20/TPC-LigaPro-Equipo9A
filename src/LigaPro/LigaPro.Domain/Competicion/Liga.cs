using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    internal class Liga : Competicion
    {
        public TipoLiga Formato { get; set; }
        public List<PartidoLiga> PartidosDeLiga { get; set; }
    }
}
