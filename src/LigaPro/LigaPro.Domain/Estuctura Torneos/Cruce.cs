using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Cruce
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        //Relaciones
        public int IdFase { get; set; }

        //Propiedad de Navegación
        public PartidoEliminatoria Partido { get; set; }
    }
}
