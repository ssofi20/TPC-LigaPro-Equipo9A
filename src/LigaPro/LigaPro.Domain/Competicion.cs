using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    public abstract class Competicion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Deporte { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; } //"Inscripción Abierta", "En Curso", "Finalizado"
        public List<Equipo> EquiposInscritos { get; set; }
    }
}
