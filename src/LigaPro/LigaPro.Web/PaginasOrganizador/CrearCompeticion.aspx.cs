using LigaPro.Datos;
using LigaPro.Domain;
using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasOrganizador
{
    public partial class CrearCompeticion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    OrganizadorDatos datos = new OrganizadorDatos();
                    List<Organizador> listaOrg = datos.listar();

                    ddlOrganizador.DataSource = listaOrg;
                    ddlOrganizador.DataValueField = "Id";
                    ddlOrganizador.DataTextField = "NombrePublico";
                    ddlOrganizador.DataBind();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if(!Page.IsValid)
            {
                return;
            }
            try
            {
                CompeticionDatos datos = new CompeticionDatos();
                ReglamentoDatos regDatos = new ReglamentoDatos();
                Reglamento reg = new Reglamento();
                Competicion nuevo = new Competicion();
                nuevo.OrganizadorCompetencia = new Organizador();
                nuevo.Reglas = new Reglamento();

                int idOrganizador = int.Parse(ddlOrganizador.SelectedValue);
                int idReg = regDatos.agregar(reg);
                reg.Id = idReg;

                reg.PuntosPorVictoria = int.Parse(txtPv.Text);
                reg.PuntosPorEmpate = int.Parse(txtPe.Text);
                reg.TarjetasAmarillasParaSuspension = int.Parse(txtTas.Text);
                reg.PartidosSuspensionPorRojaDirecta = int.Parse(txtPsrd.Text);
                regDatos.agregar(reg);

                nuevo.Nombre = txtNombre.Text;
                nuevo.Estado = EstadoCompetencia.InscripcionAbierta;
                nuevo.OrganizadorCompetencia.Id = idOrganizador;
                nuevo.Reglas.Id = idReg;                

                if (rbConFases.Checked)
                {
                    nuevo.Fases = true;

                    if (rbIdaVuelta.Checked)
                    {
                        nuevo.Formato = TipoLiga.IdaYVuelta.ToString();
                    }
                    else if (rbIda.Checked)
                    {
                        nuevo.Formato = TipoLiga.Ida.ToString() ;
                    }

                    datos.agregarComp(nuevo);
                }
                else if (rbSinFases.Checked)
                {
                    nuevo.Fases = false;
                    datos.agregarComp(nuevo);
                }

                Response.Redirect("PerfilAdmin.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rbConFases_CheckedChanged(object sender, EventArgs e)
        {
            panelOpcionesFases.Visible = rbConFases.Checked;
        }

        protected void rbSinFases_CheckedChanged(object sender, EventArgs e)
        {
            panelOpcionesFases.Visible = false;
        }
    }
}