using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }

        //Relación (El Usuario-Jugador que lo creó y administra)
        public int IdUsuarioCreador { get; set; }
        //Navegación
        public List<EquipoJugador> Plantel { get; set; }

    }
}
