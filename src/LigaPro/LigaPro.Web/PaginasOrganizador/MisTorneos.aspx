<%@ Page Title="Mis Torneos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisTorneos.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.MisTorneos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">

    <style>
        .table-hover tbody tr:hover {
            background-color: #f8f9fa;
        }

        .badge-estado {
            font-size: 0.8em;
            padding: 0.5em 0.8em;
        }
    </style>

    <script>
        function abrirModalEditar() {
            var myModal = new bootstrap.Modal(document.getElementById('modalEditarTorneo'));
            myModal.show();
        }
        function abrirModalEliminar() {
            var myModal = new bootstrap.Modal(document.getElementById('modalEliminarTorneo'));
            myModal.show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container py-5">

        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1">Mis Torneos</h2>
                <p class="text-muted">Gestiona tus competiciones activas.</p>
            </div>
            <a href="CrearTorneo.aspx" class="btn btn-primary shadow-sm">
                <i class="bi bi-plus-lg me-2"></i>Nuevo Torneo
            </a>
        </div>

        <div class="card border-0 shadow-sm">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <table class="table table-hover align-middle mb-0">
                        <thead class="bg-light">
                            <tr>
                                <th class="ps-4 py-3">Nombre</th>
                                <th>Estructura</th>
                                <th>Cupos</th>
                                <th>Estado</th>
                                <th class="text-end pe-4">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptTorneos" runat="server" OnItemCommand="rptTorneos_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td class="ps-4 py-3">
                                            <div class="d-flex align-items-center">
                                                <div class="bg-primary bg-opacity-10 text-primary rounded p-2 me-3">
                                                    <i class="bi bi-trophy-fill"></i>
                                                </div>
                                                <div>
                                                    <h6 class="mb-0 fw-bold"><%# Eval("Nombre") %></h6>
                                                    <small class="text-muted">ID: <%# Eval("Id") %></small>
                                                </div>
                                            </div>
                                        </td>

                                        <td>
                                            <%# (bool)Eval("TieneFaseDeGrupos") 
                                                ? "<span class='badge bg-info text-dark'><i class='bi bi-grid-3x3 me-1'></i>Con Grupos</span>" 
                                                : "<span class='badge bg-light text-dark border'><i class='bi bi-list-ol me-1'></i>Eliminatoria/Liga</span>" 
                                            %>
                                        </td>

                                        <td>
                                            <div class="d-flex align-items-center">
                                                <div class="progress flex-grow-1 me-2" style="height: 6px; width: 60px;">
                                                    <div class="progress-bar" role="progressbar"
                                                        style='width: <%# (int)Eval("CupoMaximo") > 0 ? ((int)Eval("CantidadInscriptos") * 100 / (int)Eval("CupoMaximo")) : 0 %>%'>
                                                    </div>
                                                </div>
                                                <small class="fw-bold">
                                                    <%# Eval("CantidadInscriptos") %> / <%# Eval("CupoMaximo") %>
                                                </small>
                                            </div>
                                        </td>

                                        <td>
                                            <span class="badge bg-secondary badge-estado">
                                                <%# Eval("Estado") %>
                                            </span>
                                        </td>

                                        <td class="text-end pe-4">
                                            <div class="btn-group" role="group">
                                                <asp:LinkButton ID="btnPartidos" runat="server" CssClass="btn btn-sm btn-outline-primary"
                                                    CommandName="Partidos" CommandArgument='<%# Eval("Id") %>' ToolTip="Gestionar Partidos">
                                                    <i class="bi bi-calendar-week"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-secondary"
                                                    CommandName="Editar" CommandArgument='<%# Eval("Id") %>' ToolTip="Configuración">
                                                    <i class="bi bi-pencil-square"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger"
                                                    CommandName="ConfirmarEliminar" CommandArgument='<%# Eval("Id") %>' ToolTip="Eliminar">
                                                    <i class="bi bi-trash"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

                <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="text-center py-5">
                    <div class="text-muted opacity-50 mb-2">
                        <i class="bi bi-inbox display-1"></i>
                    </div>
                    <h5 class="text-muted">No tienes torneos activos</h5>
                    <p class="small text-muted">Crea uno nuevo para comenzar a gestionar.</p>
                </asp:Panel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEditarTorneo" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Editar Torneo</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfIdTorneoEditar" runat="server" />

                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="form-label">Nombre del Torneo</label>
                            <asp:TextBox ID="txtNombreEditar" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Estado</label>
                            <asp:DropDownList ID="ddlEstadoEditar" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>

                        <div class="col-12">
                            <hr />
                            <h6 class="fw-bold">Reglas</h6>
                        </div>

                        <div class="col-md-3">
                            <label class="form-label small">Pts Victoria</label>
                            <asp:TextBox ID="txtPvEditar" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small">Pts Empate</label>
                            <asp:TextBox ID="txtPeEditar" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small">Amarillas</label>
                            <asp:TextBox ID="txtTasEditar" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small">Rojas</label>
                            <asp:TextBox ID="txtPsrdEditar" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>

                        <div class="col-12 mt-3">
                            <div class="form-check form-switch">
                                <asp:CheckBox ID="chkFasesEditar" runat="server" CssClass="form-check-input" />
                                <label class="form-check-label">Tiene Fase de Grupos</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnGuardarEdicion" runat="server" Text="Guardar Cambios"
                        CssClass="btn btn-primary" OnClick="btnGuardarEdicion_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEliminarTorneo" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">Eliminar Torneo</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>Estás a punto de eliminar el torneo <strong>
                        <asp:Label ID="lblNombreTorneoEliminar" runat="server"></asp:Label></strong>.</p>
                    <div class="alert alert-warning small">
                        Esta acción es irreversible (Baja lógica).
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Contraseña para confirmar:</label>
                        <asp:TextBox ID="txtPassEliminar" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:HiddenField ID="hfIdTorneoEliminar" runat="server" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Confirmar Eliminación"
                        CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
