using LigaPro.Domain;
using LigaPro.Domain.Actores;
using LigaPro.Domain.Actores.LigaPro.Domain.Actores;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Datos
{
    public class TorneoDatos
    {
        public void CrearNuevoTorneo(Torneo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                int idReglamento = CrearReglamento(nuevo.Reglas);

                datos.setearConsulta("INSERT INTO Torneos (IdOrganizador, Nombre, Estado, CupoMaximo, TieneFaseGrupos, IdReglamento) \r\nVALUES (@idOrganizador, @nombre, @estado, @cupoMaximo, @tieneFaseGrupos, @idReglamento)");
                datos.setearParametro("@idOrganizador", nuevo.Organizador.Id);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@estado", nuevo.Estado);
                datos.setearParametro("@cupoMaximo", nuevo.CupoMaximo);
                datos.setearParametro("@tieneFaseGrupos", nuevo.TieneFaseDeGrupos);
                datos.setearParametro("@idReglamento", idReglamento);

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

        private int CrearReglamento(Reglamento reglamento)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Reglamentos (PuntosPorVictoria, PuntosPorEmpate, PuntosPorDerrota, PartidosSuspensionPorRojaDirecta, TarjetasAmarillasParaSuspension)\r\nVALUES (@PuntosPorVictoria, @PuntosPorEmpate, @PuntosPorDerrota, @PartidosSuspensionPorRojaDirecta, @TarjetasAmarillasParaSuspension)\r\nSELECT SCOPE_IDENTITY()");
                datos.setearParametro("@PuntosPorVictoria", reglamento.PuntosPorVictoria);
                datos.setearParametro("@PuntosPorEmpate", reglamento.PuntosPorEmpate);
                datos.setearParametro("@PuntosPorDerrota", reglamento.PuntosPorDerrota);
                datos.setearParametro("@PartidosSuspensionPorRojaDirecta", reglamento.PuntosPorDerrota);
                datos.setearParametro("@TarjetasAmarillasParaSuspension", reglamento.TarjetasAmarillasParaSuspension);

                return Convert.ToInt32(datos.ejecutarScalar());
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

        public List<Torneo> listarCompeticion(int idOrganizador)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Torneo> lista = new List<Torneo>();
            try
            {
                datos.setearConsulta("SELECT T.IdTorneo, T.Nombre, T.Estado, T.CupoMaximo, T.TieneFaseGrupos, T.Activo, T.IdReglamento, R.PuntosPorEmpate, R.PuntosPorVictoria, R.PuntosPorDerrota, R.PartidosSuspensionPorRojaDirecta, R.TarjetasAmarillasParaSuspension\r\nFROM Torneos T\r\nINNER JOIN Reglamentos R ON R.IdReglamento = T.IdReglamento\r\nWHERE T.IdOrganizador = @IdOrganizador AND T.Activo = 1");
                datos.setearParametro("@IdOrganizador", idOrganizador);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Torneo aux = new Torneo();
                    
                    aux.Id = (int)datos.Lector["IdTorneo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Estado = (EstadoCompetencia)Enum.Parse(typeof(EstadoCompetencia), datos.Lector["Estado"].ToString());
                    aux.CupoMaximo = (int)datos.Lector["CupoMaximo"];
                    aux.TieneFaseDeGrupos = (bool)datos.Lector["TieneFaseGrupos"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    
                    aux.Reglas = new Reglamento();
                    aux.Reglas.Id = (int)datos.Lector["IdReglamento"];
                    aux.Reglas.PuntosPorEmpate = (int)datos.Lector["PuntosPorEmpate"];
                    aux.Reglas.PuntosPorVictoria = (int)datos.Lector["PuntosPorVictoria"];
                    aux.Reglas.PuntosPorDerrota = (int)datos.Lector["PuntosPorDerrota"];
                    aux.Reglas.PartidosSuspensionPorRojaDirecta = (int)datos.Lector["PartidosSuspensionPorRojaDirecta"];
                    aux.Reglas.TarjetasAmarillasParaSuspension = (int)datos.Lector["TarjetasAmarillasParaSuspension"];

                    lista.Add(aux);
                }
                return lista;
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
        public Torneo buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT T.IdTorneo, T.Nombre, T.Estado, T.CupoMaximo, T.TieneFaseGrupos, T.Activo, T.IdReglamento, R.PuntosPorEmpate, R.PuntosPorVictoria, R.PuntosPorDerrota, R.PartidosSuspensionPorRojaDirecta, R.TarjetasAmarillasParaSuspension\r\nFROM Torneos T\r\nINNER JOIN Reglamentos R ON R.IdReglamento = T.IdReglamento\r\nWHERE T.IdTorneo = @idTorneo");
                datos.setearParametro("@idTorneo", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Torneo aux = new Torneo();
                    aux.Id = (int)datos.Lector["IdTorneo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Estado = (EstadoCompetencia)Enum.Parse(typeof(EstadoCompetencia), datos.Lector["Estado"].ToString());
                    aux.CupoMaximo = (int)datos.Lector["CupoMaximo"];
                    aux.TieneFaseDeGrupos = (bool)datos.Lector["TieneFaseGrupos"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Reglas = new Reglamento();
                    aux.Reglas.Id = (int)datos.Lector["IdReglamento"];
                    aux.Reglas.PuntosPorEmpate = (int)datos.Lector["PuntosPorEmpate"];
                    aux.Reglas.PuntosPorVictoria = (int)datos.Lector["PuntosPorVictoria"];
                    aux.Reglas.PuntosPorDerrota = (int)datos.Lector["PuntosPorDerrota"];
                    aux.Reglas.PartidosSuspensionPorRojaDirecta = (int)datos.Lector["PartidosSuspensionPorRojaDirecta"];
                    aux.Reglas.TarjetasAmarillasParaSuspension = (int)datos.Lector["TarjetasAmarillasParaSuspension"];

                    return aux;
                }
                return null;
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

        public void modificarTorneo(Torneo aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                modificarReglamento(aux.Reglas);

                datos.setearConsulta("UPDATE Torneos SET Nombre = @Nombre, Estado = @Estado, CupoMaximo = @CupoMaximo, TieneFaseGrupos = @TieneFaseGrupos WHERE IdTorneo = @IdTorneo");
                datos.setearParametro("@IdTorneo", aux.Id);
                datos.setearParametro("@Nombre", aux.Nombre);
                datos.setearParametro("@Estado", aux.Estado);
                datos.setearParametro("@CupoMaximo", aux.CupoMaximo);
                datos.setearParametro("@TieneFaseGrupos", aux.TieneFaseDeGrupos);
                
                datos.ejecutarAccion();
                datos.cerrarConexion();
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

        private void modificarReglamento(Reglamento reglas)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("UPDATE Reglamentos SET PuntosPorVictoria = @PV, PuntosPorEmpate = @PE, PuntosPorDerrota = @PD, TarjetasAmarillasParaSuspension = @TAS, PartidosSuspensionPorRojaDirecta = @PSRD WHERE IdReglamento = @IdReglamento");
            datos.setearParametro("@IdReglamento", reglas.Id);
            datos.setearParametro("@PV", reglas.PuntosPorVictoria);
            datos.setearParametro("@PE", reglas.PuntosPorEmpate);
            datos.setearParametro("@PD", reglas.PuntosPorDerrota);
            datos.setearParametro("@TAS", reglas.TarjetasAmarillasParaSuspension);
            datos.setearParametro("@PSRD", reglas.PartidosSuspensionPorRojaDirecta);
            
            datos.ejecutarAccion();
            datos.cerrarConexion();
        }

        public void DesactivarTorneo(int IdTorneo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Torneos SET Activo = 0 WHERE IdTorneo = @IdTorneo");
                datos.setearParametro("@IdTorneo", IdTorneo);

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
