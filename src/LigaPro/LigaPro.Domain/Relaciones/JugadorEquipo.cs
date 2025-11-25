using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Relaciones
{
    // Esta clase sirve SOLO para listar jugadores dentro de un equipo
    public class JugadorEquipo
    {

        // Datos de la tabla Jugadores
        public int IdJugador { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        // Datos de la tabla EquipoJugador
        public int NumeroCamiseta { get; set; }
        public string Posicion { get; set; }
        public bool EsCapitan { get; set; }

        // Propiedad auxiliar para mostrar nombre completo si quieres
        public string NombreCompleto
        {
            get { return Nombres + " " + Apellidos; }
        }

    }
}
