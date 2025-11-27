<%@ Page Title="Mis Torneos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisTorneos.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.MisTorneos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">

    <style>
        .table-hover tbody tr:hover {
            background-color: #f8f9fa;
        }

        .btn-actions {
            padding: 0.25rem 0.5rem;
            font-size: 0.875rem;
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
                <p class="text-muted">Gestiona tus competiciones y fases.</p>
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
                                <th class="ps-4 py-3">Nombre del Torneo</th>
                                <th>Formato</th>
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
                                            <%# (bool)Eval("Fases") ? "<span class='badge bg-info text-dark'>Con Fases</span>" : "<span class='badge bg-light text-dark border'>Liga Única</span>" %>
                                        </td>
                                        <td>
                                            <span class="badge bg-secondary"><%# Eval("Estado") %></span>
                                        </td>
                                        <td class="text-end pe-4">
                                            <div class="dropdown">
                                                <button class="btn btn-sm btn-outline-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                    <i class="bi bi-gear-fill me-1"></i>Gestionar
                                                </button>
                                                <ul class="dropdown-menu dropdown-menu-end shadow">

                                                    <li>
                                                        <asp:LinkButton ID="btnEditar" runat="server" CssClass="dropdown-item"
                                                            CommandName="Editar" CommandArgument='<%# Eval("Id") %>'>
                                                            <i class="bi bi-pencil me-2 text-warning"></i> Editar Configuración
                                                        </asp:LinkButton>
                                                    </li>

                                                    <li>
                                                        <hr class="dropdown-divider">
                                                    </li>
                                                    <li>
                                                        <h6 class="dropdown-header">Competición</h6>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="btnPartidos" runat="server" CssClass="dropdown-item"
                                                            CommandName="Partidos" CommandArgument='<%# Eval("Id") %>'>
                                                            <i class="bi bi-calendar-week me-2 text-primary"></i> Gestionar Partidos
                                                        </asp:LinkButton>
                                                    </li>

                                                    <li>
                                                        <hr class="dropdown-divider">
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="btnEliminar" runat="server" CssClass="dropdown-item text-danger"
                                                            CommandName="ConfirmarEliminar" CommandArgument='<%# Eval("Id") %>'>
                                                            <i class="bi bi-trash me-2"></i> Eliminar Torneo
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

                <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="text-center py-5">
                    <i class="bi bi-inbox display-4 text-muted"></i>
                    <p class="mt-3 text-muted">No has creado ningún torneo aún.</p>
                </asp:Panel>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modalEditarTorneo" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title fw-bold">Configuración del Torneo</h5>
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
                            <h6 class="fw-bold">Reglas de Puntuación</h6>
                        </div>

                        <div class="col-md-3">
                            <label class="form-label small">Puntos Victoria</label>
                            <asp:TextBox ID="txtPvEditar" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small">Puntos Empate</label>
                            <asp:TextBox ID="txtPeEditar" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small">Amarillas (Susp)</label>
                            <asp:TextBox ID="txtTasEditar" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small">Roja (Partidos)</label>
                            <asp:TextBox ID="txtPsrdEditar" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>

                        <div class="col-12">
                            <hr />
                            <h6 class="fw-bold">Formato</h6>
                            <div class="form-check form-switch">
                                <asp:CheckBox ID="chkFasesEditar" runat="server" CssClass="form-check-input" />
                                <label class="form-check-label">Incluye Fase de Grupos y Eliminatorias</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
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
                    <p>
                        Estás a punto de eliminar el torneo <strong>
                            <asp:Label ID="lblNombreTorneoEliminar" runat="server"></asp:Label></strong>.
                    </p>
                    <div class="alert alert-warning small">
                        <i class="bi bi-exclamation-triangle me-1"></i>Esta acción dará de baja el torneo y todos sus partidos.
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Confirma tu contraseña para continuar:</label>
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
