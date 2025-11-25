<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleEquipo.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.DetalleEquipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                            <asp:Button ID="btnEditar" runat="server" Text="Editar Información" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnEditar_Click" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Equipo" CssClass="btn btn-outline-danger btn-sm" OnClientClick="return confirm('¿Seguro que deseas eliminar este equipo?');" OnClick="btnEliminar_Click"/>
                        </div>
                    </div>
                </div>

                <div class="card shadow-sm border-0 mt-3">
                    <div class="card-body">
                        <h6 class="card-title text-muted fw-bold">Estadísticas</h6>
                        <ul class="list-group list-group-flush small">
                            <li class="list-group-item d-flex justify-content-between align-items-center px-0">Jugadores
                               
                                <span class="badge bg-primary rounded-pill">
                                    <asp:Label ID="lblCantidadJugadores" runat="server" Text="0"></asp:Label></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center px-0">Torneos Jugados
                               
                                <span class="badge bg-secondary rounded-pill">0</span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="col-md-8">
                <div class="card shadow-sm border-0">
                    <div class="card-header bg-white py-3 d-flex justify-content-between align-items-center">
                        <h5 class="mb-0 fw-bold">Plantel de Jugadores</h5>
                        <button type="button" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#modalAgregarJugador">
                            <i class="bi bi-person-plus-fill me-1"></i>Agregar Jugador
                       
                        </button>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <table class="table table-hover align-middle mb-0">
                                <thead class="table-light">
                                    <tr>
                                        <th scope="col" class="ps-4">Jugador</th>
                                        <th scope="col">Posición</th>
                                        <th scope="col">Dorsal</th>
                                        <th scope="col" class="text-end pe-4">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptJugadores" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="ps-4">
                                                    <div class="d-flex align-items-center">
                                                        <img src="https://ui-avatars.com/api/?name=<%# Eval("Nombre") %>&background=random"
                                                            class="rounded-circle me-2" width="35" height="35">
                                                        <div>
                                                            <div class="fw-bold"><%# Eval("Nombre") %> <%# Eval("Apellido") %></div>
                                                            <div class="text-muted small">DNI: <%# Eval("Dni") %></div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td><%# Eval("Posicion") %></td>
                                                <td><span class="badge bg-light text-dark border"><%# Eval("NumeroCamiseta") %></span></td>
                                                <td class="text-end pe-4">
                                                    <asp:LinkButton ID="btnBajaJugador" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        CssClass="btn btn-link text-danger p-0" OnClick="btnBajaJugador_Click" ToolTip="Dar de baja">
                                                        <i class="bi bi-x-circle"></i>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr runat="server" visible='<%# rptJugadores.Items.Count == 0 %>'>
                                                <td colspan="4" class="text-center py-5 text-muted">
                                                    <i class="bi bi-people display-4 d-block mb-2"></i>
                                                    Este equipo aún no tiene jugadores.
                                                </td>
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

    <div class="modal fade" id="modalAgregarJugador" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Nuevo Jugador</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label">DNI o Nombre para buscar</label>
                        <asp:TextBox ID="txtBusquedaJugador" runat="server" CssClass="form-control" placeholder="Buscar..."></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnGuardarJugador" runat="server" Text="Agregar al Equipo" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
