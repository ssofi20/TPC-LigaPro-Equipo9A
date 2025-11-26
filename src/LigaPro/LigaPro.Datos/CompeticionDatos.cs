using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Datos
{
    public class CompeticionDatos
    {
        public void agregarComp(Competicion nuevo, bool Fases)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Competiciones (IdOrganizador, IdReglamento, Nombre, Estado, TieneFaseDeGrupos) values (@IdOrganizador, @IdReglamento, @Nombre, @Estado, @TieneFaseDeGrupos)");
                datos.setearParametro("@IdOrganizador", nuevo.IdOrganizador);
                datos.setearParametro("@IdReglamento", nuevo.IdReglamento);
                datos.setearParametro("@Nombre",nuevo.Nombre);
                datos.setearParametro("@Estado", nuevo.Estado);
                datos.setearParametro("@TieneFaseDeGrupos", Fases);

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
        public void agregarComp(Liga nuevo, bool Fases)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Competiciones (IdOrganizador, IdReglamento, Nombre, Estado, FormatoLiga, TieneFaseDeGrupos) values (@IdOrganizador, @IdReglamento, @Nombre, @Estado, @FormatoLiga, @TieneFaseDeGrupos)");
                datos.setearParametro("@IdOrganizador", nuevo.IdOrganizador);
                datos.setearParametro("@IdReglamento", nuevo.IdReglamento);
                datos.setearParametro("@Nombre",nuevo.Nombre);
                datos.setearParametro("@Estado", nuevo.Estado);
                datos.setearParametro("@FormatoLiga",nuevo.Formato);
                datos.setearParametro("@TieneFaseDeGrupos", Fases);

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
