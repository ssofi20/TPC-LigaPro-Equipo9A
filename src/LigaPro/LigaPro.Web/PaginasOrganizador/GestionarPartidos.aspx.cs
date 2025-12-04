using LigaPro.Datos;
using LigaPro.Domain.Actores;
using LigaPro.Domain.Actores.LigaPro.Domain.Actores;
using LigaPro.Domain.Partidos;
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

        private void CargarPartidos()
        {
            try
            {
                PartidoDatos datos = new PartidoDatos();

                List<PartidoDto> partidos = datos.ListarPartidosResumen(IdTorneoActual);

                if (partidos.Count > 0)
                {
                    rptPartidos.DataSource = partidos;
                    rptPartidos.DataBind();

                    pnlSinPartidos.Visible = false;
                    rptPartidos.Visible = true;
                }
                else
                {
                    pnlSinPartidos.Visible = true;
                    rptPartidos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                pnlSinPartidos.Visible = true;
            }
        }

        protected string ObtenerBadgeEstado(string estado)
        {
            switch (estado)
            {
                case "Finalizado":
                    return "<span class='badge bg-secondary'><i class='bi bi-check-all me-1'></i>Finalizado</span>";
                case "EnCurso":
                    return "<span class='badge bg-success'><i class='bi bi-play-circle me-1'></i>En Curso</span>";
                case "Suspendido":
                case "Cancelado":
                    return "<span class='badge bg-danger'><i class='bi bi-x-octagon me-1'></i>Suspendido</span>";
                case "Walkover":
                    return "<span class='badge bg-dark'>W.O.</span>";
                case "Pendiente":
                default:
                    return "<span class='badge bg-warning text-dark'><i class='bi bi-clock me-1'></i>Pendiente</span>";
            }
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

                if (torneo.TieneFaseDeGrupos)
                {
                    nuevoPartido = new PartidoGrupo();
                    nuevoPartido.TipoPartido = "Grupo";
                    ((PartidoGrupo)nuevoPartido).NombreGrupo = "Grupo " + ddlGrupo.SelectedValue;
                }
                else
                {
                    nuevoPartido = new PartidoEliminatoria();
                    nuevoPartido.TipoPartido = "Eliminatoria";
                }

                // 5. Llenar datos comunes
                nuevoPartido.IdTorneo = IdTorneoActual;
                nuevoPartido.FechaHora = fechaHora;
                nuevoPartido.Estado = "Pendiente";
                nuevoPartido.ResultadoEquipoA = 0;
                nuevoPartido.ResultadoEquipoB = 0;

                // 6. Guardar usando un método que acepte IDs de Equipos
                PartidoDatos pdatos = new PartidoDatos();

                if (nuevoPartido is PartidoGrupo)
                {
                    pdatos.CrearPartidoManual((PartidoGrupo)nuevoPartido, idEquipoLocal, idEquipoVisita);
                }
                else
                {
                    pdatos.CrearPartidoManual((PartidoEliminatoria)nuevoPartido, idEquipoLocal, idEquipoVisita);
                }

                // 7. Refrescar
                CargarPartidos();

                // Limpiar
                ddlLocalManual.SelectedIndex = 0;
                ddlVisitaManual.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el partido: " + ex.Message);
            }
        }

        protected void rptPartidos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idPartido = int.Parse(e.CommandArgument.ToString());

            /// CASO 1: CARGAR RESULTADO 
            if (e.CommandName == "CargarResultado")
            {
                hfIdPartidoResultado.Value = idPartido.ToString();

                // (Aquí va tu lógica de precarga existente...)
                PartidoDatos datos = new PartidoDatos();
                var p = datos.ObtenerPorId(idPartido);
                if (p != null)
                {
                    txtGolesLocal.Text = p.GolesLocal.ToString();
                    txtGolesVisita.Text = p.GolesVisita.ToString();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "PopRes", "abrirModalResultado();", true);
            }

            /// CASO 2: MODIFICAR 
            else if (e.CommandName == "Modificar")
            {
                hfIdPartidoModificar.Value = idPartido.ToString();

                PartidoDatos datos = new PartidoDatos();
                var p = datos.ObtenerPorId(idPartido);

                if (p != null)
                {
                    txtNuevaFecha.Text = p.FechaProgramada.ToString("yyyy-MM-dd");
                    txtNuevaHora.Text = p.FechaProgramada.ToString("HH:mm");

                    if (ddlEstadoModificar.Items.FindByValue(p.Estado) != null)
                    {
                        ddlEstadoModificar.SelectedValue = p.Estado;
                    }
                    else
                    {
                        ddlEstadoModificar.SelectedIndex = 0;
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "PopMod", "abrirModalModificar();", true);
            }

            /// CASO 3: CANCELAR
            else if (e.CommandName == "Cancelar")
            {
                hfIdPartidoCancelar.Value = idPartido.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "PopCancel", "abrirModalCancelar();", true);
            }
        }

        protected void btnGuardarModificacion_Click(object sender, EventArgs e)
        {
            try
            {
                int idPartido = int.Parse(hfIdPartidoModificar.Value);
                DateTime nuevaFecha = DateTime.Parse(txtNuevaFecha.Text + " " + txtNuevaHora.Text);

                string nuevoEstado = ddlEstadoModificar.SelectedValue;

                PartidoDatos datos = new PartidoDatos();

                datos.ModificarPartido(idPartido, nuevaFecha, nuevoEstado);

                CargarPartidos();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMod", $"alert('Error al modificar: {ex.Message}');", true);
            }
        }

        protected void btnConfirmarCancelacion_Click(object sender, EventArgs e)
        {
            try
            {
                int idPartido = int.Parse(hfIdPartidoCancelar.Value);

                PartidoDatos datos = new PartidoDatos();
                datos.EliminarPartido(idPartido);

                CargarPartidos();
            }
            catch (Exception ex)
            {
                // Manejar error
            }
        }

        protected void btnGuardarResultado_Click(object sender, EventArgs e)
        {
            try
            {
                int idPartido = int.Parse(hfIdPartidoResultado.Value);

                int golesL = string.IsNullOrEmpty(txtGolesLocal.Text) ? 0 : int.Parse(txtGolesLocal.Text);
                int golesV = string.IsNullOrEmpty(txtGolesVisita.Text) ? 0 : int.Parse(txtGolesVisita.Text);

                bool finalizado = true;

                PartidoDatos datos = new PartidoDatos();
                datos.CargarResultado(idPartido, golesL, golesV, finalizado);

                CargarPartidos();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertError", $"alert('Error: {ex.Message}');", true);
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