using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Competicion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public EstadoCompetencia Estado { get; set; }

        public string TipoCompetencia { get; set; }

        //Relaciones
        public int IdOrganizador { get; set; }
        public int IdReglamento { get; set; }

        //Navegacion
       /*public Reglamento Reglas { get; set; }
        public Organizador OrganizadorCompetencia { get; set; }*/
        public List<Equipo> EquiposInscritos { get; set; }
        public List<Partido> PartidosProgramados { get; set; }
    }
}
