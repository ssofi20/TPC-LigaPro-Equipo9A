using LigaPro.Domain.Actores.LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigaPro.Datos;

namespace LigaPro.Negocio
{
    public class TorneoNegocio
    {
        public void CrearTorneo(Torneo nuevo)
        {
            TorneoDatos datos = new TorneoDatos();
            datos.CrearNuevoTorneo(nuevo);
        }
    }
}
