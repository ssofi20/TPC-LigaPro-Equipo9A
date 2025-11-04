using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class PartidoLiga : Partido
    {
        public int NumeroJornada { get; set; }

        //Relación (Nullable por si es una Liga pura y no un grupo de torneo)
        public int? IdGrupo { get; set; }
    }
}
