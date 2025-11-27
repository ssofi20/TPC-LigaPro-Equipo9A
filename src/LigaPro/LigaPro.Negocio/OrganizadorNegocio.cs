using LigaPro.Datos;
using LigaPro.Domain;
using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Negocio
{
    public class OrganizadorNegocio
    {
        private UsuarioDatos _usuarioDatos = new UsuarioDatos();
        private OrganizadorDatos _organizadorDatos = new OrganizadorDatos();

        public bool ValidarEmailExistente(string email)
        {
            return _usuarioDatos.VerificarEmailExistente(email);
        }

        public void RegistrarNuevoOrganizador(Usuario nuevoUsuario, Organizador nuevoOrganizador)
        {
            Seguridad seguridad = new Seguridad();
            nuevoUsuario.PasswordHash = seguridad.HashPassword(nuevoUsuario.PasswordHash);

            nuevoUsuario.FechaRegistro = DateTime.Now;
            nuevoUsuario.Rol = RolUsuario.Organizador; 
            nuevoUsuario.NombreUsuario = nuevoUsuario.Email;

            int nuevoIdUsuario = _usuarioDatos.InsertarUsuario(nuevoUsuario);

            nuevoOrganizador.IdUsuario = nuevoIdUsuario;

            _organizadorDatos.InsertarOrganizador(nuevoOrganizador);
        }
    }
}
