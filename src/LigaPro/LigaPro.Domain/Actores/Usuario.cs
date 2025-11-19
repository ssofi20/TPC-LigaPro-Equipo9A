using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain.Actores
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string NombreUsuario { get; set; }
        public RolUsuario Rol { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo {get; set; }
    }
}
