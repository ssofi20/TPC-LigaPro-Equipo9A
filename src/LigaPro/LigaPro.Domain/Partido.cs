using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    public class Partido
    {
        public int Id { get; set; }
        public int NumeroFecha { get; set; }
        public DateTime FechaHora { get; set; }
        public Equipo Equipo1 { get; set; }
        public Equipo Equipo2 { get; set; }
        public int ResultadoEquipo1 { get; set; }
        public int ResultadoEquipo2 { get; set; }
        public int IdCompeticion { get; set; }
        public Competicion Competicion { get; set; }
        public string Estado { get; set; }
        public List<EventoPartido> Eventos { get; set; }
    }
}
