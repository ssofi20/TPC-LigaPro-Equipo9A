using LigaPro.Datos;
using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Negocio
{
    public class JugadorNegocio
    {
        private UsuarioDatos usuarioDatos = new UsuarioDatos();
        private JugadorDatos jugadorDatos = new JugadorDatos();

        public bool ValidarEmailExistente(string email)
        {
            return usuarioDatos.VerificarEmailExistente(email);
        }

        public void RegistrarNuevoJugador(Usuario nuevoUsuario, Jugador nuevoJugador)
        {
            Seguridad seguridad = new Seguridad();
            nuevoUsuario.PasswordHash = seguridad.HashPassword(nuevoUsuario.PasswordHash);

            nuevoUsuario.FechaRegistro = DateTime.Now;
            nuevoUsuario.Rol = Domain.RolUsuario.Jugador;

            int idUsuarioCreado = usuarioDatos.InsertarUsuario(nuevoUsuario);

            nuevoJugador.IdUsuarioJugador = idUsuarioCreado;
            jugadorDatos.InsertarJugador(nuevoJugador);
        }
    }
}
