<%@ Page Title="Torneos Disponibles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TorneosActivos.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.TorneosActivos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
    <script>
        function abrirModalInscribir() {
            var myModal = new bootstrap.Modal(document.getElementById('modalInscribir'));
            myModal.show();
        }
        function abrirModalBaja() {
            var myModal = new bootstrap.Modal(document.getElementById('modalBaja'));
            myModal.show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container py-5">
        <h2 class="fw-bold mb-4">Competiciones Disponibles</h2>

        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            <asp:Repeater ID="rptTorneos" runat="server" OnItemCommand="rptTorneos_ItemCommand">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 shadow-sm border-0">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-start mb-2">
                                    <h5 class="card-title fw-bold text-primary"><%# Eval("Nombre") %></h5>
                                    <span class="badge bg-success">Abierto</span>
                                </div>

                                <p class="card-text text-muted small">Inscripción habilitada para equipos.</p>

                                <div class="mb-3">
                                    <div class="d-flex justify-content-between small mb-1">
                                        <span>Cupos ocupados</span>
                                        <span class="fw-bold"><%# Eval("CantidadInscriptos") %> / <%# Eval("CupoMaximo") %></span>
                                    </div>
                                    <div class="progress" style="height: 6px;">
                                        <div class="progress-bar bg-primary" role="progressbar"
                                            style='width: <%# (int)Eval("CupoMaximo") > 0 ? ((int)Eval("CantidadInscriptos") * 100 / (int)Eval("CupoMaximo")) : 0 %>%'>
                                        </div>
                                    </div>
                                </div>

                                <div class="d-grid gap-2">
                                    <asp:Button ID="btnInscribir" runat="server" Text="Inscribir un Equipo"
                                        CommandName="Inscribir" CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-outline-primary btn-sm" />

                                    <asp:Button ID="btnBaja" runat="server" Text="Darse de Baja"
                                        CommandName="Baja" CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-outline-danger btn-sm" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="text-center mt-5">
            <p class="text-muted">No hay torneos con inscripción abierta en este momento.</p>
        </asp:Panel>
    </div>

    <div class="modal fade" id="modalInscribir" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">Inscribirse al Torneo</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <p>Selecciona con cuál de tus equipos quieres participar:</p>
                    <asp:HiddenField ID="hfIdTorneoInscribir" runat="server" />

                    <div class="mb-3">
                        <asp:DropDownList ID="ddlMisEquiposInscribir" runat="server" CssClass="form-select"></asp:DropDownList>
                        <asp:Label ID="lblErrorInscripcion" runat="server" CssClass="text-danger small mt-2" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarInscripcion" runat="server" Text="Confirmar Inscripción"
                        CssClass="btn btn-primary" OnClick="btnConfirmarInscripcion_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalBaja" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">Retirarse del Torneo</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <p>Selecciona el equipo que deseas retirar de la competencia:</p>
                    <asp:HiddenField ID="hfIdTorneoBaja" runat="server" />

                    <div class="mb-3">
                        <asp:DropDownList ID="ddlMisEquiposBaja" runat="server" CssClass="form-select"></asp:DropDownList>
                        <asp:Label ID="lblErrorBaja" runat="server" CssClass="text-danger small mt-2" Visible="false"></asp:Label>
                    </div>
                    <div class="alert alert-warning small">
                        <i class="bi bi-exclamation-triangle"></i>Perderás tu lugar en el cupo.
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarBaja" runat="server" Text="Confirmar Baja"
                        CssClass="btn btn-danger" OnClick="btnConfirmarBaja_Click"/>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalExito" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center border-0 shadow-lg">
                <div class="modal-body p-5">
                    <div class="mb-4 text-success animate__animated animate__bounceIn">
                        <i class="bi bi-check-circle-fill display-1"></i>
                    </div>
                    <h3 class="fw-bold mb-3">¡Operación Exitosa!</h3>
                    <p class="text-muted mb-4" id="pMensajeExito">La acción se realizó correctamente.</p>
                    <button type="button" class="btn btn-success btn-lg px-5 rounded-pill shadow-sm" data-bs-dismiss="modal">Entendido</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function mostrarExito(mensaje) {
            var modalInscribir = bootstrap.Modal.getInstance(document.getElementById('modalInscribir'));
            if (modalInscribir) modalInscribir.hide();

            var modalBaja = bootstrap.Modal.getInstance(document.getElementById('modalBaja'));
            if (modalBaja) modalBaja.hide();

            document.getElementById('pMensajeExito').innerText = mensaje;
            var myModal = new bootstrap.Modal(document.getElementById('modalExito'));
            myModal.show();
        }
    </script>

</asp:Content>
