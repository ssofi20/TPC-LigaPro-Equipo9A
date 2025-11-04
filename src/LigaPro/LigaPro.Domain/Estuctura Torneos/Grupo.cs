using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        //Relación
        public int IdFase { get; set; }

        //Navegación
        public List<Inscripcion> Inscripciones { get; set; }
        public List<PartidoLiga> Partidos { get; set; }
    }
}
