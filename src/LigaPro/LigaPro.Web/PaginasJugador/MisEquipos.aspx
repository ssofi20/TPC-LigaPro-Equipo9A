<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisEquipos.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.MisEquipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Mis Equipos</h2>
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalCrearEquipo">
                <i class="bi bi-plus-lg"></i>Crear Nuevo Equipo
            </button>
        </div>

        <!-- LISTA DE EQUIPOS (Repeater) -->
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater ID="rptEquipos" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 shadow-sm border-0">
                            <div class="card-body text-center">
                                <img src='<%# Eval("Imagen") %>' class="rounded-circle mb-3" width="80" height="80" style="object-fit: cover;" />
                                <h5 class="card-title fw-bold"><%# Eval("Nombre") %></h5>
                                <p class="card-text text-muted small">Administrador</p>
                                <a href='DetalleEquipo.aspx?id=<%# Eval("Id") %>' class="btn btn-outline-primary w-100 mt-2">Administrar Equipo
                                </a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <!-- Mensaje si no hay equipos -->
            <asp:Panel ID="pnlSinEquipos" runat="server" Visible="false" CssClass="col-12 text-center mt-5">
                <p class="text-muted">Aún no tienes equipos. ¡Crea uno para empezar a competir!</p>
            </asp:Panel>
        </div>
    </div>

    <!-- MODAL CREAR EQUIPO -->
    <div class="modal fade" id="modalCrearEquipo" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Crear Nuevo Equipo</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label">Nombre del Equipo</label>
                        <asp:TextBox ID="txtNombreEquipo" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombreEquipo" runat="server" ControlToValidate="txtNombreEquipo"
                            ErrorMessage="El nombre es obligatorio" CssClass="text-danger small" ValidationGroup="CrearEquipo" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Escudo (Opcional)</label>
                        <asp:FileUpload ID="fuEscudoEquipo" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEquipo" runat="server" Text="Crear Equipo" CssClass="btn btn-primary" ValidationGroup="CrearEquipo" OnClick="btnGuardarEquipo_Click"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
