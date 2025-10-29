using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    class Partido
    {
        public int Id { get; set; }
        public int IdCompeticion { get; set; }
        public int NumeroFecha { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; }
        public Equipo Equipo1 { get; set; }
        public Equipo Equipo2 { get; set; }
        public Equipo Ganador { get; set; }
        public int PuntosE1 { get; set; }
        public int PuntosE2 { get; set; }
        public bool Empate { get; set; }
        
    }
}
