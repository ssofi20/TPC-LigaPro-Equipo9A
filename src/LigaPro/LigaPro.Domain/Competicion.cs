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
        public Deporte Deporte { get; set; }
        public string Estado { get; set; } //"Inscripción Abierta", "En Curso", "Finalizado"
        public Reglamento Reglamento { get; set; }
        public Usuario Admin { get; set; }
        public List<Equipo> EquiposInscritos { get; set; }
    }
}
