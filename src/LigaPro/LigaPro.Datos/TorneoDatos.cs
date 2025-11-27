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

        public List<Torneo> listarCompeticion(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Torneo> lista = new List<Torneo>();
            try
            {
                datos.setearConsulta("SELECT C.IdCompeticion, C.Nombre, C.Estado, C.FormatoLiga, C.TieneFaseDeGrupos, C.Activo, O.IdOrganizador, O.NombrePublico, R.IdReglamento, R.PuntosPorVictoria, R.PuntosPorEmpate, R.TarjetasAmarillasParaSuspension, R.PartidosSuspensionPorRojaDirecta FROM Competiciones C INNER JOIN Organizadores O ON O.IdOrganizador = C.IdOrganizador INNER JOIN Reglamentos R ON R.IdReglamento = C.IdReglamento WHERE O.IdOrganizador = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Torneo aux = new Torneo();
                    aux.Organizador = new Organizador();
                    aux.Reglas = new Reglamento();

                    aux.Organizador.Id = (int)datos.Lector["IdOrganizador"];
                    aux.Organizador.NombrePublico = (string)datos.Lector["NombrePublico"];

                    aux.Reglas.Id = (int)datos.Lector["IdReglamento"];
                    aux.Reglas.PuntosPorVictoria = (int)datos.Lector["PuntosPorVictoria"];
                    aux.Reglas.PuntosPorEmpate = (int)datos.Lector["PuntosPorEmpate"];
                    aux.Reglas.TarjetasAmarillasParaSuspension = (int)datos.Lector["TarjetasAmarillasParaSuspension"];
                    aux.Reglas.PartidosSuspensionPorRojaDirecta = (int)datos.Lector["PartidosSuspensionPorRojaDirecta"];


                    aux.Id = (int)datos.Lector["IdCompeticion"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Estado = (EstadoCompetencia)Enum.Parse(typeof(EstadoCompetencia), datos.Lector["Estado"].ToString());
                    ////aux.Formato = datos.Lector["FormatoLiga"] != DBNull.Value
                    //    ? datos.Lector["FormatoLiga"].ToString()
                    //    : null;
                    ////aux.Fases = datos.Lector["TieneFaseDeGrupos"] != DBNull.Value
                    //    ? Convert.ToBoolean(datos.Lector["TieneFaseDeGrupos"])
                    //    : false;
                    aux.Activo = (bool)datos.Lector["Activo"];
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
                datos.setearConsulta("SELECT C.IdCompeticion, C.Nombre, C.Estado, C.FormatoLiga, C.TieneFaseDeGrupos, O.IdOrganizador, O.NombrePublico, R.IdReglamento, R.PuntosPorVictoria, R.PuntosPorEmpate, R.TarjetasAmarillasParaSuspension, R.PartidosSuspensionPorRojaDirecta FROM Competiciones C INNER JOIN Organizadores O ON O.IdOrganizador = C.IdOrganizador INNER JOIN Reglamentos R ON R.IdReglamento = C.IdReglamento WHERE C.IdCompeticion = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Torneo aux = new Torneo();
                    aux.Organizador = new Organizador();
                    aux.Reglas = new Reglamento();

                    aux.Organizador.Id = (int)datos.Lector["IdOrganizador"];
                    aux.Organizador.NombrePublico = (string)datos.Lector["NombrePublico"];

                    aux.Reglas.Id = (int)datos.Lector["IdReglamento"];
                    aux.Reglas.PuntosPorVictoria = (int)datos.Lector["PuntosPorVictoria"];
                    aux.Reglas.PuntosPorEmpate = (int)datos.Lector["PuntosPorEmpate"];
                    aux.Reglas.TarjetasAmarillasParaSuspension = (int)datos.Lector["TarjetasAmarillasParaSuspension"];
                    aux.Reglas.PartidosSuspensionPorRojaDirecta = (int)datos.Lector["PartidosSuspensionPorRojaDirecta"];

                    aux.Id = (int)datos.Lector["IdCompeticion"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Estado = (EstadoCompetencia)Enum.Parse(typeof(EstadoCompetencia), datos.Lector["Estado"].ToString());
                    //aux.Formato = datos.Lector["FormatoLiga"] != DBNull.Value
                    //    ? datos.Lector["FormatoLiga"].ToString()
                    //    : null;
                    //aux.Fases = datos.Lector["TieneFaseDeGrupos"] != DBNull.Value
                    //    ? Convert.ToBoolean(datos.Lector["TieneFaseDeGrupos"])
                    //    : false;
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
                datos.setearConsulta("UPDATE Reglamentos SET PuntosPorVictoria = @PV, PuntosPorEmpate = @PE, TarjetasAmarillasParaSuspension = @TAS, PartidosSuspensionPorRojaDirecta = @PSRD WHERE IdReglamento = @IdReglamento");
                datos.setearParametro("@PV", aux.Reglas.PuntosPorVictoria);
                datos.setearParametro("@PE", aux.Reglas.PuntosPorEmpate);
                datos.setearParametro("@TAS", aux.Reglas.TarjetasAmarillasParaSuspension);
                datos.setearParametro("@PSRD", aux.Reglas.PartidosSuspensionPorRojaDirecta);
                datos.setearParametro("@IdReglamento", aux.Reglas.Id);
                datos.ejecutarAccion();
                datos.cerrarConexion();

                AccesoDatos datosComp = new AccesoDatos();


                datosComp.setearConsulta("UPDATE Competiciones SET IdReglamento = @IdReglamento, Nombre = @Nombre, Estado = @Estado, FormatoLiga = @FormatoLiga, TieneFaseDeGrupos = @TieneFaseDeGrupos WHERE IdCompeticion = @IdCompeticion");
                datosComp.setearParametro("@IdReglamento", aux.Reglas.Id);
                datosComp.setearParametro("@Nombre", aux.Nombre);
                datosComp.setearParametro("@Estado", aux.Estado);
                //datosComp.setearParametro("@FormatoLiga", aux.Formato != null ? (object)aux.Formato : DBNull.Value);
                //datosComp.setearParametro("@TieneFaseDeGrupos", aux.Fases);
                datosComp.setearParametro("@IdCompeticion", aux.Id);
                datosComp.ejecutarAccion();
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

        public void DesactivarTorneo(Torneo aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Competiciones SET Activo = 0 WHERE IdCompeticion = @Id");
                datos.setearParametro("@Id", aux.Id);

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
