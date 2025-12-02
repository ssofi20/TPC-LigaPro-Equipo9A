using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class PartidoGrupo : Partido
    {
        public int NumeroJornada { get; set; }
        public int IdGrupo { get; set; }

        public PartidoGrupo()
        {
            this.TipoPartido = "Grupo";
        }
    }
}
