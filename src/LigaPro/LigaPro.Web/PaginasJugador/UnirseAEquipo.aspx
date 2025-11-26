<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnirseAEquipo.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.UnirseAEquipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">

        <div class="row justify-content-center mb-4">
            <div class="col-md-8 text-center">
                <h2 class="mb-3">Encuentra tu equipo</h2>
                <div class="input-group input-group-lg shadow-sm">
                    <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" placeholder="Nombre del equipo o creador..."></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary px-4" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>

        <h4 class="mb-3">Resultados</h4>
        <div class="row row-cols-1 row-cols-md-3 g-4 mb-5">
            <asp:Repeater ID="rptEquiposEncontrados" runat="server" OnItemCommand="rptEquiposEncontrados_ItemCommand">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 border-0 shadow-sm">
                            <div class="card-body text-center">
                                <img src='<%# Eval("Imagen") != null ? Eval("Imagen") : "/Uploads/default-team.png" %>'
                                    class="rounded-circle mb-3" width="80" height="80" style="object-fit: cover;">
                                <h5 class="card-title fw-bold"><%# Eval("Nombre") %></h5>
                                <p class="card-text text-muted small">Creador: <%# Eval("NombreCreador") %></p>

                                <asp:Button ID="btnSolicitar" runat="server"
                                    Text='<%# Eval("EstadoSolicitud") == null ? "Solicitar Unirme" : (Eval("EstadoSolicitud").ToString() == "Pendiente" ? "Solicitud Enviada" : "Ya eres miembro") %>'
                                    Enabled='<%# Eval("EstadoSolicitud") == null %>'
                                    CssClass='<%# Eval("EstadoSolicitud") == null ? "btn btn-outline-success w-100 mt-2 rounded-pill" : "btn btn-secondary w-100 mt-2 rounded-pill" %>'
                                    CommandName="Unirse" CommandArgument='<%# Eval("Id") %>' />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Panel ID="pnlSinResultados" runat="server" Visible="false" CssClass="col-12 text-center text-muted">
                <p>No se encontraron equipos.</p>
            </asp:Panel>
        </div>
    </div>

    <div class="modal fade" id="modalExito" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center">
                <div class="modal-body p-4">
                    <div class="mb-3 text-success">
                        <i class="bi bi-check-circle-fill display-1"></i>
                    </div>
                    <h4 class="mb-2">¡Solicitud Enviada!</h4>
                    <p class="text-muted">El administrador del equipo revisará tu solicitud.</p>
                    <button type="button" class="btn btn-success px-4" data-bs-dismiss="modal">Entendido</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function mostrarModalExito() {
            var myModal = new bootstrap.Modal(document.getElementById('modalExito'));
            myModal.show();
        }
    </script>

</asp:Content>
