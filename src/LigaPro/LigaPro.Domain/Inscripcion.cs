using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    internal class Inscripcion
    {
        public int IdEquipo { get; set; }
        public int IdCompetencia { get; set; }

        public DateTime FechaInscripcion { get; set; }

        public string Estado { get; set; }
    }
}
