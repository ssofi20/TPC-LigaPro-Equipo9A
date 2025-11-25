using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Relaciones
{
    public class Solicitud
    {
        // Datos propios de la tabla Solicitudes
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdEquipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } // "Pendiente", "Aceptada", "Rechazada"

        // PROPIEDADES EXTENDIDAS (Para mostrar en pantalla sin hacer otra consulta)
        // Estas no se guardan en la tabla Solicitudes, pero las llenamos con un JOIN
        public string Nombre { get; set; }   // Nombre del Jugador
        public string Apellido { get; set; } // Apellido del Jugador
    }
}
