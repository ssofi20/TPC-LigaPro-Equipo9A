using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    public class Deporte
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantJugadores { get; set; }
        public bool PermiteEmpate { get; set; }
    }
}
