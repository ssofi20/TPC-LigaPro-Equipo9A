using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Datos
{
    public class PartidoDatos
    {
        public void CrearPartidoManual(PartidoGrupo partido, int idEquipoLocal, int idEquipoVisita)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("INSERT INTO Partidos (IdInscripcionA, IdInscripcionB, IdTorneo, IdGrupo,FechaHora, ResultadoEquipoA, ResultadoEquipoB, TipoPartido, Estado)\r\nSELECT \r\n\t(SELECT IdInscripcion FROM Inscripciones WHERE IdEquipo = @idEquipoLocal AND IdTorneo = @idTorneo),    \r\n\t(SELECT IdInscripcion FROM Inscripciones WHERE IdEquipo = @idEquipoVisita AND IdTorneo = @idTorneo),\r\n\t@idTorneo,\r\n\t(SELECT IdGrupo FROM Grupos WHERE IdTorneo = @idTorneo AND Nombre = @nombreGrupo),\r\n\t@fechaHora,\r\n\t@resultadoA,\r\n\t@resultadoB, \r\n\t@tipoPartido,\r\n\t@estado;");
                datos.setearParametro("@idTorneo", partido.IdTorneo);
                datos.setearParametro("@idEquipoLocal", idEquipoLocal);
                datos.setearParametro("@idEquipoVisita", idEquipoVisita);
                datos.setearParametro("@nombreGrupo", partido.NombreGrupo);
                datos.setearParametro("@fechaHora", partido.FechaHora);
                datos.setearParametro("@resultadoA", partido.ResultadoEquipoA);
                datos.setearParametro("@resultadoB", partido.ResultadoEquipoB);
                datos.setearParametro("@tipoPartido", partido.TipoPartido);
                datos.setearParametro("@estado", partido.Estado);

                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public void AsignarEquipoAGrupo(int idTorneo, int idEquipo, string nombreGrupo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DECLARE @IdInscripcion INT;\r\nDECLARE @IdGrupo INT;\r\n\r\nSELECT @IdInscripcion = IdInscripcion \r\nFROM Inscripciones \r\nWHERE IdEquipo = @idEquipo AND IdTorneo = @idTorneo;\r\n\r\nSELECT @IdGrupo = IdGrupo \r\nFROM Grupos \r\nWHERE IdTorneo = @idTorneo AND Nombre = @nombreGrupo;\r\n\r\nIF NOT EXISTS (SELECT 1 FROM InscripcionGrupo WHERE IdInscripcion = @IdInscripcion AND IdGrupo = @IdGrupo)\r\nBEGIN\r\n\tINSERT INTO InscripcionGrupo (IdGrupo, IdInscripcion) \r\n\tVALUES (@IdGrupo, @IdInscripcion);\r\nEND");
                datos.setearParametro("@idTorneo", idTorneo);
                datos.setearParametro("@idEquipo", idEquipo);
                datos.setearParametro("@nombreGrupo", nombreGrupo);
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

        public void CrearPartidoManual(PartidoEliminatoria partido, int idEquipoLocal, int idEquipoVisita)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Partidos (\r\n    IdInscripcionA, \r\n    IdInscripcionB, \r\n    IdTorneo,       \r\n    FechaHora, \r\n    ResultadoEquipoA, \r\n    ResultadoEquipoB, \r\n    TipoPartido, \r\n    Estado\r\n)\r\nSELECT \r\n    (SELECT IdInscripcion \r\n     FROM Inscripciones \r\n     WHERE IdEquipo = @idEquipoLocal AND IdTorneo = @idTorneo),\r\n     \r\n    (SELECT IdInscripcion \r\n     FROM Inscripciones \r\n     WHERE IdEquipo = @idEquipoVisita AND IdTorneo = @idTorneo),\r\n     \r\n    @idTorneo,\r\n    @fechaHora,\r\n    @resultadoA,\r\n    @resultadoB,\r\n    @tipoPartido,\r\n    @estado");
                datos.setearParametro("@idTorneo", partido.IdTorneo);
                datos.setearParametro("@idEquipoLocal", idEquipoLocal);
                datos.setearParametro("@idEquipoVisita", idEquipoVisita);
                datos.setearParametro("@fechaHora", partido.FechaHora);
                datos.setearParametro("@resultadoA", partido.ResultadoEquipoA);
                datos.setearParametro("@resultadoB", partido.ResultadoEquipoB);
                datos.setearParametro("@tipoPartido", partido.TipoPartido);
                datos.setearParametro("@estado", partido.Estado);

                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }
    }
}
