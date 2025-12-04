using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    using System;
    using System.Collections.Generic;

    namespace LigaPro.Domain.Actores
    {
        public class Torneo
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public bool Activo { get; set; } = true; 

            public EstadoCompetencia Estado { get; set; } 
            public int CupoMaximo { get; set; } 

            public bool TieneFaseDeGrupos { get; set; }
            public int CantidadGrupos { get; set; }

            public Reglamento Reglas { get; set; }

            public Organizador Organizador { get; set; }
            public List<Partido> Partidos { get; set; } = new List<Partido>();

            public int CantidadInscriptos { get; set; }
        }
    }
}
