using LigaPro.Datos;
using LigaPro.Domain.Actores;
using LigaPro.Domain.Actores.LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasGenerales
{
	public partial class PaginaCompeticiones : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            CargarTorneos();
		}

        private void CargarTorneos()
        {
            try
            {
                TorneoDatos datos = new TorneoDatos();
                OrganizadorDatos orgDatos = new OrganizadorDatos();
                List<Torneo> lista = datos.Listar();

                var listaActiva = lista.Where(x => x.Activo).ToList();

                if (listaActiva.Count > 0)
                {
                    rptTorneos.DataSource = listaActiva;
                    rptTorneos.DataBind();
                    pnlVacio.Visible = false;
                }
                else
                {
                    rptTorneos.DataSource = null;
                    rptTorneos.DataBind();
                    pnlVacio.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}