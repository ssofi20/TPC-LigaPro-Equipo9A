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
                bool TieneFase = false;

                int idReg = regDatos.agregar(reg);
                reg.Id = idReg;

                reg.PuntosPorVictoria = int.Parse(txtPv.Text);
                reg.PuntosPorEmpate = int.Parse(txtPe.Text);
                reg.TarjetasAmarillasParaSuspension = int.Parse(txtTas.Text);
                reg.PartidosSuspensionPorRojaDirecta = int.Parse(txtPsrd.Text);
                regDatos.agregar(reg);

                if (rbConFases.Checked)
                {
                    Liga nuevo = new Liga();
                    TieneFase = true;

                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Estado = EstadoCompetencia.InscripcionAbierta;
                    nuevo.IdOrganizador = int.Parse(ddlOrganizador.SelectedValue);
                    nuevo.IdReglamento = reg.Id;

                    if (rbIdaVuelta.Checked)
                    {
                        nuevo.Formato = TipoLiga.IdaYVuelta;
                    }
                    else if (rbIda.Checked)
                    {
                        nuevo.Formato = TipoLiga.Ida;
                    }

                    datos.agregarComp(nuevo, TieneFase);
                }
                else if (rbSinFases.Checked)
                {
                    Competicion nuevoTorneo = new Competicion();

                    nuevoTorneo.Nombre = txtNombre.Text;
                    nuevoTorneo.Estado = EstadoCompetencia.InscripcionAbierta;
                    nuevoTorneo.IdOrganizador = int.Parse(ddlOrganizador.SelectedValue);
                    nuevoTorneo.IdReglamento = reg.Id;

                    TieneFase = false;
                    datos.agregarComp(nuevoTorneo, TieneFase);
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