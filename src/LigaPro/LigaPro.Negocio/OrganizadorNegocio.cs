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

        //Metodo para validar si el email ya existe
        public bool ValidarEmailExistente(string email)
        {
            return _usuarioDatos.VerificarEmailExistente(email);
        }

        //Metodo principal para registrar un organizador
        public void RegistrarNuevoOrganizador(Usuario nuevoUsuario, Organizador nuevoOrganizador)
        {
            // 1. Hashear la contraseña{
            Seguridad seguridad = new Seguridad();
            nuevoUsuario.PasswordHash = seguridad.HashPassword(nuevoUsuario.PasswordHash);

            // 2. Asignar valores por defecto
            nuevoUsuario.FechaRegistro = DateTime.Now;
            nuevoUsuario.Rol = RolUsuario.Organizador; 
            nuevoUsuario.NombreUsuario = nuevoUsuario.Email;

            // 3. Insertar el usuario y obtener el Id generado
            int nuevoIdUsuario = _usuarioDatos.InsertarUsuario(nuevoUsuario);

            // 4. Asignar el IdUsuario al organizador
            nuevoOrganizador.IdUsuario = nuevoIdUsuario;

            // 5. Insertar el organizador
            _organizadorDatos.InsertarOrganizador(nuevoOrganizador);
        }
    }
}
