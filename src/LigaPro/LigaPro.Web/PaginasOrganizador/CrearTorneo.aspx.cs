using LigaPro.Datos;
using LigaPro.Domain;
using LigaPro.Domain.Actores;
using LigaPro.Domain.Actores.LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Web.UI;

namespace LigaPro.Web.PaginasOrganizador
{
    public partial class CrearTorneo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Auth/InicioSesion.aspx");
            }
        }

        // Al cambiar el radio button para mostrar opciones de puntos
        protected void Formato_Changed(object sender, EventArgs e)
        {
            pnlPuntos.Visible = rbConFases.Checked;
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                Usuario usuario = (Usuario)Session["UsuarioLogueado"];
                OrganizadorDatos orgDatos = new OrganizadorDatos();
                TorneoNegocio negocio = new TorneoNegocio();

                // CONFIGURAR REGLAMENTO
                Reglamento reglas = new Reglamento();
                reglas.TarjetasAmarillasParaSuspension = int.Parse(txtAmarillas.Text);
                reglas.PartidosSuspensionPorRojaDirecta = int.Parse(txtRojas.Text);

                Torneo nuevo = new Torneo();

                // Lógica de Puntos
                if (rbConFases.Checked)
                {
                    // Si hay grupos, usamos lo que escribió el usuario
                    reglas.PuntosPorVictoria = int.Parse(txtPuntosVictoria.Text);
                    reglas.PuntosPorEmpate = int.Parse(txtPuntosEmpate.Text);
                    reglas.PuntosPorDerrota = int.Parse(txtPuntosDerrota.Text);
                    nuevo.CantidadGrupos = int.Parse(txtCantidadGrupos.Text);
                }
                else
                {
                    // Si es eliminatoria directa, guardamos defaults (no afectan el juego)
                    reglas.PuntosPorVictoria = 3;
                    reglas.PuntosPorEmpate = 1;
                    reglas.PuntosPorDerrota = 0;
                    nuevo.CantidadGrupos = 0;
                }

                // CONFIGURAR TORNEO
                nuevo.Nombre = txtNombre.Text;
                nuevo.Activo = true;
                nuevo.Estado = EstadoCompetencia.InscripcionAbierta;
                nuevo.CupoMaximo = int.Parse(txtCupos.Text);

                // La propiedad clave: ¿Tiene grupos o no?
                nuevo.TieneFaseDeGrupos = rbConFases.Checked;

                // Asignar objetos
                nuevo.Reglas = reglas;
                nuevo.Organizador = orgDatos.ObtenerInfoAdmin(usuario.Id);

                // 3. GUARDAR
                negocio.CrearTorneo(nuevo);

                Response.Redirect("MisTorneos.aspx", false);
            }
            catch (Exception ex)
            {
                // Manejo de error (puedes agregar un Label de error en el ASPX)
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisTorneos.aspx");
        }
    }
}