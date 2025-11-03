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
        public EstadoCompetencia Estado { get; set; }
        public Reglamento Reglamento { get; set; }
        public Usuario OrganizadorCompetencia { get; set; }
        public List<Equipo> EquiposInscritos { get; set; }
    }
}
