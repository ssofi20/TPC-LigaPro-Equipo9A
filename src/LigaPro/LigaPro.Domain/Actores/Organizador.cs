using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Organizador
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario UsuarioOrganizador { get; set; }
        public string NombrePublico { get; set; }
        public string Logo { get; set; }
        public string EmailContacto { get; set; }
        public string NumeroTelefono { get; set; }
    }
}
