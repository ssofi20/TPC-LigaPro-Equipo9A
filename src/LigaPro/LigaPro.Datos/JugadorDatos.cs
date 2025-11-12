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
    }
}
