using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasGenerales
{
    public partial class PaginaEquipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarEquipos();
        }
        protected void CargarEquipos()
        {
            EquipoNegocio equipoNegocio = new EquipoNegocio();

            try
            {
                List<Equipo> listaEquipos = equipoNegocio.ListarEquipos();

                if (listaEquipos != null && listaEquipos.Count > 0)
                {
                    rptEquipos.DataSource = listaEquipos;
                    rptEquipos.DataBind();
                    pnlSinEquipos.Visible = false;
                }
                else
                {
                    pnlSinEquipos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}