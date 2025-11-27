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
    public partial class CrearTorneo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                    if (usuario != null && usuario.Id != 0 && usuario.Rol == Domain.RolUsuario.Organizador)
                    {
                       
                    }
                    else
                    {
                        Response.Redirect("/Auth/InicioSesion.aspx", false);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
            try
            {
                Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                CompeticionDatos datos = new CompeticionDatos();
                ReglamentoDatos regDatos = new ReglamentoDatos();
                Reglamento reg = new Reglamento();
                Competicion nuevo = new Competicion();
                OrganizadorDatos orgDatos = new OrganizadorDatos();
                nuevo.OrganizadorCompetencia = new Organizador();
                nuevo.Reglas = new Reglamento();

                reg.PuntosPorVictoria = int.Parse(txtPv.Text);
                reg.PuntosPorEmpate = int.Parse(txtPe.Text);
                reg.TarjetasAmarillasParaSuspension = int.Parse(txtTas.Text);
                reg.PartidosSuspensionPorRojaDirecta = int.Parse(txtPsrd.Text);

                int idReg = regDatos.agregar(reg);
                reg.Id = idReg;

                nuevo.OrganizadorCompetencia = orgDatos.ObtenerInfoAdmin(usuario.Id);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Estado = EstadoCompetencia.InscripcionAbierta;
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
                        nuevo.Formato = TipoLiga.Ida.ToString();
                    }
                }
                else if (rbSinFases.Checked)
                {
                    nuevo.Fases = false;
                    nuevo.Formato = null;
                }
                datos.agregarComp(nuevo);
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerfilAdmin.aspx");
        }
    }
}