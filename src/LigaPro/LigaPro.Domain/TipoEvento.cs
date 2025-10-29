using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    public class TipoEvento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Deporte Deporte { get; set; }
    }
}
