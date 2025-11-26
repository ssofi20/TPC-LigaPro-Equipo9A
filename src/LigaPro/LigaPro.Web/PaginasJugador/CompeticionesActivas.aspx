<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompeticionesActivas.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.CompeticionesActivas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <h2 class="mb-4"><i class="bi bi-trophy-fill text-warning me-2"></i>Torneos y Ligas Disponibles</h2>

        <div class="row row-cols-1 row-cols-lg-2 g-4">
            <asp:Repeater ID="rptTorneos" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card shadow-sm h-100">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-start">
                                    <div>
                                        <h4 class="card-title fw-bold"><%# Eval("Nombre") %></h4>
                                        <span class="badge bg-primary mb-2"><%# Eval("Disciplina") %></span>
                                    </div>
                                    <span class="badge bg-success">Abierto</span>
                                </div>
                                <p class="card-text mt-3"><%# Eval("Descripcion") %></p>
                                <div class="d-flex justify-content-between text-muted small mt-3">
                                    <span><i class="bi bi-calendar-event"></i>Inicio: <%# Eval("FechaInicio", "{0:dd/MM/yyyy}") %></span>
                                    <span><i class="bi bi-people"></i>Cupos: <%# Eval("CuposDisponibles") %></span>
                                </div>
                            </div>
                            <div class="card-footer bg-white border-0 pb-3">
                                <asp:Button ID="btnPreInscribir" runat="server" Text="Inscribir Equipo"
                                    CommandName="PreInscribir" CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-primary w-100" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <div class="modal fade" id="modalInscripcion" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">Confirmar Inscripción</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>Vas a inscribirte en el torneo: <strong>
                        <asp:Label ID="lblNombreTorneoModal" runat="server"></asp:Label></strong></p>

                    <div class="mb-3">
                        <label class="form-label">Selecciona tu equipo:</label>
                        <asp:DropDownList ID="ddlMisEquipos" runat="server" CssClass="form-select"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvEquipo" runat="server" ControlToValidate="ddlMisEquipos"
                            InitialValue="0" ErrorMessage="Debes seleccionar un equipo" CssClass="text-danger small" ValidationGroup="Inscripcion" />
                    </div>

                    <asp:HiddenField ID="hfIdTorneoSeleccionado" runat="server" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarInscripcion" runat="server" Text="Confirmar Inscripción"
                        CssClass="btn btn-success" ValidationGroup="Inscripcion" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function abrirModalInscripcion() {
            var myModal = new bootstrap.Modal(document.getElementById('modalInscripcion'));
            myModal.show();
        }
    </script>

</asp:Content>
