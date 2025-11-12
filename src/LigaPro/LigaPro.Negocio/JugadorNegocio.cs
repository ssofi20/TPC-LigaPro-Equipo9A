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

        //Metodo para validar si el email ya existe
        public bool ValidarEmailExistente(string email)
        {
            return usuarioDatos.VerificarEmailExistente(email);
        }

        // Metodo para registrar un nuevo jugador junto con su usuario
        public void RegistrarNuevoJugador(Usuario nuevoUsuario, Jugador nuevoJugador)
        {
            // 1. Hashear la contraseña del usuario
            Seguridad seguridad = new Seguridad();
            nuevoUsuario.PasswordHash = seguridad.HashPassword(nuevoUsuario.PasswordHash);

            // 2. Asignar valores por defecto al usuario
            nuevoUsuario.FechaRegistro = DateTime.Now;
            nuevoUsuario.Rol = Domain.RolUsuario.Jugador;

            // 3. Guardar el usuario en la base de datos
            int idUsuarioCreado = usuarioDatos.InsertarUsuario(nuevoUsuario);

            // 4. Asignar el IdUsuario al jugador y guardarlo en la base de datos
            nuevoJugador.IdUsuarioJugador = idUsuarioCreado;
            jugadorDatos.InsertarJugador(nuevoJugador);
        }
    }
}
