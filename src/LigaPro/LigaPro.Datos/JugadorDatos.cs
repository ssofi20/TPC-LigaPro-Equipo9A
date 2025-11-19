using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigaPro.Domain.Actores;

namespace LigaPro.Datos
{
    public class JugadorDatos
    {
        public void InsertarJugador(Jugador nuevoJugador)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string query = "INSERT INTO Jugadores(IdUsuarioJugador, Nombres, Apellidos, FechaNacimiento) VALUES (@idUsuario, @nombres, @apellidos, @fechaNacimiento)";
                datos.setearConsulta(query);

                datos.setearParametro("@idUsuario", nuevoJugador.IdUsuarioJugador);
                datos.setearParametro("@nombres", nuevoJugador.Nombres);
                datos.setearParametro("@apellidos", nuevoJugador.Apellidos);
                datos.setearParametro("@fechaNacimiento", nuevoJugador.FechaNacimiento);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Jugador ObtenerInfoJugador(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Jugador aux = new Jugador();
            try
            {
                datos.setearConsulta("Select J.IdJugador, J.IdUsuarioJugador, J.Nombres, J.Apellidos, J.FechaNacimiento FROM Jugadores J, Usuarios U WHERE J.IdUsuarioJugador = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    aux.Id = (int)datos.Lector["IdJugador"];
                    aux.IdUsuarioJugador = (int)datos.Lector["IdUsuarioJugador"];
                    aux.Nombres = (string)datos.Lector["Nombres"];
                    aux.Apellidos = (string)datos.Lector["Apellidos"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                }
                datos.cerrarConexion();
                return aux;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificarJugador(Jugador jugador, Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Jugadores SET Nombres = @Nombres, Apellidos = @Apellidos, FechaNacimiento = @FechaNacimiento FROM Jugadores WHERE IdUsuarioJugador = @IdUsuarioJugador");
                datos.setearParametro("@Nombres", jugador.Nombres);
                datos.setearParametro("@Apellidos", jugador.Apellidos);
                datos.setearParametro("@FechaNacimiento", jugador.FechaNacimiento);
                datos.setearParametro("@IdUsuarioJugador", user.Id);

                datos.ejecutarAccion();
                datos.cerrarConexion();

                datos.setearConsulta("UPDATE Usuarios SET Email = @Email, NombreUsuario = @NombreUsuario WHERE IdUsuario = @IdUsuario");
                datos.setearParametro("@Email", user.Email);
                datos.setearParametro("@NombreUsuario", user.NombreUsuario);
                datos.setearParametro("@IdUsuario", user.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificarPassJugador(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET PasswordHash = @PasswordHash WHERE IdUsuario = @IdUsuario");
                datos.setearParametro("@PasswordHash", user.PasswordHash);
                datos.setearParametro("@IdUsuario", user.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
