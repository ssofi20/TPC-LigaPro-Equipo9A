<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleEquipo.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.DetalleEquipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function abrirModalEditar() {
            var myModal = new bootstrap.Modal(document.getElementById('modalEditarEquipo'));
            myModal.show();
        }
        function abrirModalEliminar() {
            var myModal = new bootstrap.Modal(document.getElementById('modalEliminarConfirmacion'));
            myModal.show();
        }
    </script>

    <div class="container mt-4">
        <div class="mb-3">
            <a href="MisEquipos.aspx" class="text-decoration-none text-muted">
                <i class="bi bi-arrow-left"></i>Volver a Mis Equipos
            </a>
        </div>

        <div class="row">
            <div class="col-md-4 mb-4">
                <div class="card shadow-sm border-0">
                    <div class="card-body text-center p-4">
                        <asp:Image ID="imgEscudo" runat="server" CssClass="rounded-circle mb-3 border"
                            Width="150" Height="150" Style="object-fit: cover;" />

                        <h3 class="fw-bold text-primary mb-1">
                            <asp:Label ID="lblNombreEquipo" runat="server" Text="Nombre Equipo"></asp:Label>
                        </h3>
                        <span class="badge bg-success mb-4">Activo</span>

                        <div class="d-grid gap-2">
                            <asp:Button ID="btnAbrirEditar" runat="server" Text="Editar Información"
                                CssClass="btn btn-outline-secondary btn-sm" OnClick="btnAbrirEditar_Click" />

                            <button type="button" class="btn btn-outline-danger btn-sm" onclick="abrirModalEliminar()">
                                Eliminar Equipo
                            </button>
                        </div>
                    </div>
                </div>

                <div class="card shadow-sm border-0 mt-3">
                    <div class="card-body">
                        <h6 class="card-title text-muted fw-bold">Estadísticas</h6>
                        <ul class="list-group list-group-flush small">
                            <li class="list-group-item d-flex justify-content-between px-0">Jugadores <span class="badge bg-primary rounded-pill">
                                <asp:Label ID="lblCantidadJugadores" runat="server" Text="0"></asp:Label></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between px-0">Solicitudes <span class="badge bg-warning text-dark rounded-pill">
                                <asp:Label ID="lblCantidadSolicitudes" runat="server" Text="0"></asp:Label></span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="col-md-8">
                <div class="card shadow-sm border-0">
                    <div class="card-header bg-white">
                        <ul class="nav nav-tabs card-header-tabs" id="myTab" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link active" id="plantel-tab" data-bs-toggle="tab" data-bs-target="#plantel" type="button" role="tab">Plantel</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="solicitudes-tab" data-bs-toggle="tab" data-bs-target="#solicitudes" type="button" role="tab">
                                    Solicitudes <span class="badge bg-danger ms-1" id="badgeSolicitudes" runat="server">0</span>
                                </button>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">
                        <div class="tab-content" id="myTabContent">

                            <div class="tab-pane fade show active" id="plantel" role="tabpanel">
                                <div class="table-responsive mt-2">
                                    <table class="table table-hover align-middle mb-0">
                                        <thead class="table-light">
                                            <tr>
                                                <th>Jugador</th>
                                                <th>Posición</th>
                                                <th>Dorsal</th>
                                                <th class="text-end">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptJugadores" runat="server" OnItemCommand="rptJugadores_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <div class="d-flex align-items-center">
                                                                <img src="https://ui-avatars.com/api/?name=<%# Eval("Nombres") %>&background=random" class="rounded-circle me-2" width="35" height="35">
                                                                <div>
                                                                    <div class="fw-bold"><%# Eval("Nombres") %> <%# Eval("Apellidos") %></div>
                                                                    <%# (bool)Eval("EsCapitan") ? "<span class='badge bg-warning text-dark' style='font-size:0.7em'>C</span>" : "" %>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td><%# Eval("Posicion") %></td>
                                                        <td><span class="badge bg-light text-dark border"><%# Eval("NumeroCamiseta") %></span></td>
                                                        <td class="text-end">
                                                            <asp:LinkButton ID="btnEditarJugador" runat="server"
                                                                CommandName="EditarJugador"
                                                                CommandArgument='<%# Eval("IdJugador") + "|" + Eval("NumeroCamiseta") + "|" + Eval("Posicion") %>'
                                                                CssClass="btn btn-link text-secondary p-0 me-2" ToolTip="Editar">
                                    <i class="bi bi-pencil-square"></i>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="btnEliminarJugador" runat="server"
                                                                CommandName="ConfirmarEliminar"
                                                                CommandArgument='<%# Eval("IdJugador") %>'
                                                                CssClass="btn btn-link text-danger p-0" ToolTip="Dar de baja">
                                    <i class="bi bi-trash"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="modal fade" id="modalEditarJugador" tabindex="-1" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title">Editar Jugador</h5>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                            <asp:HiddenField ID="hfIdJugadorEditar" runat="server" />

                                            <div class="mb-3">
                                                <label class="form-label">Número de Camiseta</label>
                                                <asp:TextBox ID="txtCamisetaEditar" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                            </div>

                                            <div class="mb-3">
                                                <label class="form-label">Posición</label>
                                                <asp:DropDownList ID="ddlPosicionEditar" runat="server" CssClass="form-select">
                                                    <asp:ListItem Text="Portero" Value="Portero" />
                                                    <asp:ListItem Text="Línea de defensa" Value="Línea de defensa" />
                                                    <asp:ListItem Text="Mediocentro" Value="Mediocentro" />
                                                    <asp:ListItem Text="Mediapunta" Value="Mediapunta" />
                                                    <asp:ListItem Text="Mediocentro defensivo" Value="Mediocentro defensivo" />
                                                    <asp:ListItem Text="Interior" Value="Interior" />
                                                    <asp:ListItem Text="Delantero" Value="Delantero" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                                            <asp:Button ID="btnGuardarEdicionJugador" runat="server" Text="Guardar Cambios"
                                                CssClass="btn btn-primary" OnClick="btnGuardarEdicionJugador_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal fade" id="modalBajaJugador" tabindex="-1" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content">
                                        <div class="modal-header bg-danger text-white">
                                            <h5 class="modal-title">¿Dar de baja?</h5>
                                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                            <p>¿Seguro que quieres eliminar a este jugador del equipo?</p>
                                            <asp:HiddenField ID="hfIdJugadorEliminar" runat="server" />
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                                            <asp:Button ID="btnConfirmarBaja" runat="server" Text="Sí, Eliminar"
                                                CssClass="btn btn-danger" OnClick="btnConfirmarBaja_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <script>
                                function abrirModalEditarJugador() {
                                    var myModal = new bootstrap.Modal(document.getElementById('modalEditarJugador'));
                                    myModal.show();
                                }
                                function abrirModalBajaJugador() {
                                    var myModal = new bootstrap.Modal(document.getElementById('modalBajaJugador'));
                                    myModal.show();
                                }
                            </script>

                            <div class="tab-pane fade" id="solicitudes" role="tabpanel">
                                <h5 class="card-title mb-3 mt-2">Jugadores esperando aprobación</h5>
                                <div class="table-responsive">
                                    <table class="table align-middle">
                                        <tbody>
                                            <asp:Repeater ID="rptSolicitudes" runat="server" OnItemCommand="rptSolicitudes_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <div class="fw-bold"><%# Eval("Nombre") %> <%# Eval("Apellido") %></div>
                                                            <small class="text-muted">Quiere unirse al equipo</small>
                                                        </td>
                                                        <td class="text-end">
                                                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CommandName="Aceptar" CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn btn-success btn-sm me-2" />
                                                            <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" CommandName="Rechazar" CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn btn-outline-danger btn-sm" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr runat="server" visible='<%# rptSolicitudes.Items.Count == 0 %>'>
                                                        <td colspan="2" class="text-center text-muted py-3">No hay solicitudes pendientes.</td>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEditarEquipo" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Editar Equipo</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombreEditar" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Cambiar Escudo</label>
                        <asp:FileUpload ID="fuEscudoEditar" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEdicion" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardarEdicion_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEliminarConfirmacion" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">¿Eliminar Equipo?</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>Estás a punto de eliminar este equipo permanentemente. Esta acción no se puede deshacer y se perderán todos los datos asociados.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Sí, Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
