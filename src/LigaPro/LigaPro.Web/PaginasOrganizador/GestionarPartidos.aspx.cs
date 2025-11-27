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

        private void CargarPartidos()
        {
            
            pnlSinPartidos.Visible = true;
        }

        private void CargarEquipos()
        {
            
        }

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
            //Lógica para insertar partido manual

            CargarPartidos();
        }

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