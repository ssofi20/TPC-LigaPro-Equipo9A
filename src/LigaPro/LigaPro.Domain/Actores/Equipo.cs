using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public bool Activo { get; set; }
        public int IdUsuarioCreador { get; set; }
        public string NombreCreador { get; set; }

        //Propiedad para cuando listamos un equipo a un jugador
        public string EstadoSolicitud{ get; set; }
    }
}
