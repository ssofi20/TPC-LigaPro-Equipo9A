using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    public class Reglamento
    {
        public int Id { get; set; }
        public int PuntosPorVictoria { get; set; }
        public int PuntosPorEmpate { get; set; }
        public int PuntosPorDerrota { get; set; }
        public int TarjetasAmarillasParaSuspension { get; set; }
        public int PartidosSuspensionPorRojaDirecta { get; set; }
    }
}
