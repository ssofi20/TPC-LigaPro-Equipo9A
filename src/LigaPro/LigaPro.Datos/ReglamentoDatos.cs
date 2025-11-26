using LigaPro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Datos
{
    public class ReglamentoDatos
    {
        public int agregar(Reglamento reg)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Reglamentos (PuntosPorVictoria, PuntosPorEmpate, TarjetasAmarillasParaSuspension, PartidosSuspensionPorRojaDirecta) OUTPUT INSERTED.IdReglamento VALUES (@PuntosPorVictoria, @PuntosPorEmpate, @TarjetasAmarillasParaSuspension, @PartidosSuspensionPorRojaDirecta)");
                datos.setearParametro("@PuntosPorVictoria", reg.PuntosPorVictoria);
                datos.setearParametro("@PuntosPorEmpate", reg.PuntosPorEmpate);
                datos.setearParametro("@TarjetasAmarillasParaSuspension", reg.TarjetasAmarillasParaSuspension);
                datos.setearParametro("@PartidosSuspensionPorRojaDirecta", reg.PartidosSuspensionPorRojaDirecta);

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
    }
}
