using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigaPro.Domain;

namespace LigaPro.Datos
{
    public class DeporteDatos
    {
        public List<Deporte> Listar()
        {
            List<Deporte> lista = new List<Deporte>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // 1. Seteamos la consulta SQL
                datos.setearConsulta("SELECT Id, Nombre, PermiteEmpate FROM Deporte");

                // 2. Ejecutamos la lectura
                datos.ejecutarLectura();

                // 3. Leemos los resultados uno por uno
                while (datos.Lector.Read())
                {
                    Deporte aux = new Deporte();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.PermiteEmpate = (bool)datos.Lector["PermiteEmpate"];

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
    }
}
