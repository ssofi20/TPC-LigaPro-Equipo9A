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
            // 1. Identidad
            public int Id { get; set; }
            public string Nombre { get; set; }
            public bool Activo { get; set; } = true; 

            // 2. Estado y Capacidad
            public EstadoCompetencia Estado { get; set; } 
            public int CupoMaximo { get; set; } 

            // 3. Configuración Estructural (Lo que define cómo se juega)
            public bool TieneFaseDeGrupos { get; set; } // TRUE = Grupos + Eliminatoria, FALSE = Liga o Eliminatoria directa

            // 4. Reglas de Juego (Puntos y Sanciones)
            public Reglamento Reglas { get; set; }

            // 5. Relaciones de Navegación (Listas)
            public Organizador Organizador { get; set; }
            public List<Equipo> EquiposInscritos { get; set; } = new List<Equipo>();
            public List<Partido> Partidos { get; set; } = new List<Partido>();

            // Propiedad auxiliar para conteo rápido
            public int CantidadInscriptos => EquiposInscritos != null ? EquiposInscritos.Count : 0;
        }
    }
}
