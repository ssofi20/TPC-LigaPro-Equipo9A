using LigaPro.Datos;
using LigaPro.Domain.Actores;
using LigaPro.Domain.Actores.LigaPro.Domain.Actores;
using LigaPro.Negocio;
using LigaPro.Web.PaginasJugador;
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
                lblFormato.Text = torneo.TieneFaseDeGrupos ? "Fase de Grupos + Eliminatoria" : "Eliminatoria";
                lblInscriptos.Text = $"{torneo.CantidadInscriptos}/{torneo.CupoMaximo}";
                liPosiciones.Visible = torneo.TieneFaseDeGrupos;

                CargarConfiguracionModal(torneo);
            }
        }

        private void CargarConfiguracionModal(Torneo torneo)
        {
            ddlGrupo.Items.Clear();

            if ((int)torneo.Estado == 2)
            {
                pnlInputGrupo.Visible = true;
                pnlInputFase.Visible = false;

                char letra = 'A';
                for (int i = 0; i < torneo.CantidadGrupos; i++)
                {
                    string nombreGrupo = $"Grupo {letra}";
                    ddlGrupo.Items.Add(new ListItem(nombreGrupo, letra.ToString()));
                    letra++;
                }
            }
            else if ((int)torneo.Estado == 3)
            {
                pnlInputGrupo.Visible = false;
                pnlInputFase.Visible = true;
            }
            else
            {
                pnlInputGrupo.Visible = false;
                pnlInputFase.Visible = false;
            }
        }

        //FALTA COMPLETAR
        private void CargarPartidos()
        {

            pnlSinPartidos.Visible = true;
        }

        private void CargarEquipos()
        {
            // Usamos TorneoDatos porque ahí creamos el método que busca en la tabla Inscripciones
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
                ddlLocalManual.Items.Insert(0, new ListItem("-- Seleccionar Local --", "0"));

                ddlVisitaManual.DataSource = equipos;
                ddlVisitaManual.DataTextField = "Nombre";
                ddlVisitaManual.DataValueField = "Id";
                ddlVisitaManual.DataBind();
                ddlVisitaManual.Items.Insert(0, new ListItem("-- Seleccionar Visita --", "0"));
            }
            catch (Exception ex)
            {
                // Opcional: Mostrar error si falla la carga
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error al cargar equipos: {ex.Message}');", true);
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
                // 1. Obtener IDs de Equipos del Dropdown
                int idEquipoLocal = int.Parse(ddlLocalManual.SelectedValue);
                int idEquipoVisita = int.Parse(ddlVisitaManual.SelectedValue);

                // Validaciones básicas
                if (idEquipoLocal == 0 || idEquipoVisita == 0) { /* Alert: Seleccionar equipos */ return; }
                if (idEquipoLocal == idEquipoVisita) { /* Alert: Mismos equipos */ return; }

                // 2. Fecha y Hora
                DateTime fechaHora = DateTime.Parse(txtFechaManual.Text + " " + txtHoraManual.Text);

                // 3. Obtener info del Torneo para decidir el tipo
                TorneoDatos tDatos = new TorneoDatos();
                Torneo torneo = tDatos.buscarPorId(IdTorneoActual);

                // 4. Crear el objeto Partido adecuado
                Partido nuevoPartido;

                // LÓGICA DE DECISIÓN:
                // Si el usuario escribió algo en "Fase" que parece un número (ej: "1", "2"), asumimos Jornada de Grupo.
                // Si escribió texto (ej: "Octavos"), asumimos Eliminatoria.
                // O simplificamos: Por defecto "Grupo" si el torneo tiene fases, sino "Eliminatoria".

                if (torneo.TieneFaseDeGrupos)
                {
                    nuevoPartido = new PartidoGrupo();
                    // Intentamos parsear la fase como número de jornada, sino ponemos 0
                    int jornada;
                    //bool esNumero = int.TryParse(txtFaseManual.Text, out jornada);
                    //((PartidoGrupo)nuevoPartido).NumeroJornada = esNumero ? jornada : 1;
                    // IdGrupo queda pendiente o null por ahora
                }
                else
                {
                    nuevoPartido = new PartidoEliminatoria();
                    // Aquí podríamos buscar el IdCruce basado en el texto txtFaseManual.Text
                    // Por ahora lo dejamos null
                }

                // 5. Llenar datos comunes
                nuevoPartido.IdTorneo = IdTorneoActual;
                nuevoPartido.FechaHora = fechaHora;
                nuevoPartido.Estado = "Pendiente";

                // NOTA: Necesitamos los IDs de Inscripción, no de Equipo.
                // La capa de datos se encargará de traducir idEquipo -> idInscripcion
                // Pasamos los IDs de equipo temporalmente en propiedades auxiliares o usamos un método inteligente en Datos.

                // 6. Guardar usando un método que acepte IDs de Equipos
                PartidoDatos pdatos = new PartidoDatos();
                pdatos.CrearPartidoManual(nuevoPartido, idEquipoLocal, idEquipoVisita);

                // 7. Refrescar
                CargarPartidos();

                // Limpiar
                //txtFaseManual.Text = "";
                ddlLocalManual.SelectedIndex = 0;
                ddlVisitaManual.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                // Mostrar error
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