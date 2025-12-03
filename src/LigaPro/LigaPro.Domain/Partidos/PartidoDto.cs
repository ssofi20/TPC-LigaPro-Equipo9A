using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Partidos
{
    public class PartidoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // "Grupo A" o "Octavos"
        public DateTime FechaProgramada { get; set; }
        public string NombreLocal { get; set; }
        public string NombreVisita { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisita { get; set; }
        public string Estado { get; set; }
        public bool Jugado { get; set; }
    }
}
