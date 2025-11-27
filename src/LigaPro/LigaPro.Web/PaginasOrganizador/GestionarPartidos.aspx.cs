using LigaPro.Datos;
using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasOrganizador
{
    public partial class GestionarPartidos : System.Web.UI.Page
    {
        public int IdTorneoActual { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string idStr = Request.QueryString["id"];
            if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int idTemp))
            {
                Response.Redirect("MisTorneos.aspx");
                return;
            }
            IdTorneoActual = idTemp;

            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Auth/InicioSesion.aspx");
            }

            if (!IsPostBack)
            {
                CargarInfoTorneo();
                CargarPartidos();
                CargarEquipos();
            }
        }

        private void CargarInfoTorneo()
        {
            TorneoDatos datos = new TorneoDatos();

            var torneo = datos.buscarPorId(IdTorneoActual);

            if (torneo != null)
            {
                lblNombreTorneo.Text = torneo.Nombre;
                lblEstadoTorneo.Text = torneo.Estado.ToString();

                lblFormato.Text = torneo.TieneFaseDeGrupos ? "Fase de Grupos + Eliminatoria" : "Eliminatoria / Liga";

                lblInscriptos.Text = $"{torneo.CantidadInscriptos}/{torneo.CupoMaximo}";

                liPosiciones.Visible = torneo.TieneFaseDeGrupos;
            }
        }

        //FALTA COMPLETAR
        private void CargarPartidos()
        {
            
            pnlSinPartidos.Visible = true;
        }

        private void CargarEquipos()
        {
           
            TorneoDatos datos = new TorneoDatos();
            try
            {
                List<Equipo> equipos = datos.ListarEquiposInscriptos(IdTorneoActual);

                rptEquipos.DataSource = equipos;
                rptEquipos.DataBind();

                ddlLocalManual.DataSource = equipos;
                ddlLocalManual.DataTextField = "Nombre";
                ddlLocalManual.DataValueField = "Id";
                ddlLocalManual.DataBind();
                ddlLocalManual.Items.Insert(0, new ListItem("Seleccionar Local", "0"));

                ddlVisitaManual.DataSource = equipos;
                ddlVisitaManual.DataTextField = "Nombre";
                ddlVisitaManual.DataValueField = "Id";
                ddlVisitaManual.DataBind();
                ddlVisitaManual.Items.Insert(0, new ListItem("Seleccionar Visita", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error cargando equipos: " + ex.Message + "');</script>");
            }
        }

        //FALTA COMPLETAR
        protected void rptPartidos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CargarResultado")
            {
                int idPartido = int.Parse(e.CommandArgument.ToString());
                hfIdPartidoResultado.Value = idPartido.ToString();

                //agrregarr

                ScriptManager.RegisterStartupScript(this, GetType(), "PopRes", "abrirModalResultado();", true);
            }
        }

        //FALTA COMPLETAR
        protected void btnGuardarResultado_Click(object sender, EventArgs e)
        {
            try
            {
                int idPartido = int.Parse(hfIdPartidoResultado.Value);
                int golesL = int.Parse(txtGolesLocal.Text);
                int golesV = int.Parse(txtGolesVisita.Text);
                bool finalizado = chkFinalizado.Checked;

                // crear una funcion tipo negocio.CargarResultado(idPartido, golesL, golesV, finalizado);

                CargarPartidos();
            }
            catch (Exception ex) { }
        }

        protected void btnCrearPartidoManual_Click(object sender, EventArgs e)
        {
            try
            {
                int idLocal = int.Parse(ddlLocalManual.SelectedValue);
                int idVisita = int.Parse(ddlVisitaManual.SelectedValue);
                string fechaStr = txtFechaManual.Text;
                string horaStr = txtHoraManual.Text;
                string fase = txtFaseManual.Text;

                if (idLocal == 0 || idVisita == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Debes seleccionar ambos equipos');", true);
                    return;
                }

                if (idLocal == idVisita)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('El equipo local y visitante no pueden ser el mismo');", true);
                    return;
                }

                DateTime fechaHora = DateTime.Parse(fechaStr + " " + horaStr);

                Partido nuevoPartido = new Partido();
                //cargar propiedades del nuevoPartido

                //guardar en base de datos
                PartidoNegocio negocio = new PartidoNegocio();

                /* Para hacer: pensar que si es para fase de grupos
                 * hay que cargar un partido de tipo liga, y si es eliminatoria
                 * hay que cargar un partido de otro tipo.
                 * SOBRECARGA DE METODOS??
                 * Porque, por ejemplo, en fase de grupos no se necesita ganar si o si, 
                 * pero en eliminatorias si. Hay tiempo extra y/o penales.
                 */
                negocio.CrearPartido(nuevoPartido);

                CargarPartidos();

                //limpiar los campos
                txtFaseManual.Text = "";
                ddlLocalManual.SelectedIndex = 0;
                ddlVisitaManual.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error al crear partido: {ex.Message}');", true);
            }
        }

        //FALTA COMPLETAR
        protected void btnGenerarFixture_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un negocio.GenerarFixtureAutomatico(IdTorneoActual);

                CargarPartidos();
            }
            catch (Exception ex)
            {
                // Alert error
            }
        }
    }
}