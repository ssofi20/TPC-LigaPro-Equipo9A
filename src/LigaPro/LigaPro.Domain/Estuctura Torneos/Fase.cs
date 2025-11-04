using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Fase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoFase Tipo { get; set; }
        public int Orden { get; set; } //Orden de la fase en la competición

        //Relación con la competición
        public int IdTorneo { get; set; }

        //Navegación
        public List<Grupo> Grupos { get; set; }
        public List<Cruce> Cruces { get; set; }
    }
}
