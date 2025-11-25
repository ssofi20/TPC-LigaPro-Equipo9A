<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnirseAEquipo.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.UnirseAEquipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center mb-5">
            <div class="col-md-8 text-center">
                <h2 class="mb-3">Encuentra tu equipo</h2>
                <div class="input-group input-group-lg shadow-sm">
                    <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" placeholder="Nombre del equipo..."></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary px-4" />
                </div>
            </div>
        </div>

        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater ID="rptEquiposEncontrados" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 border-0 shadow-sm">
                            <div class="card-body text-center">
                                <img src='<%# Eval("Imagen") != null ? Eval("Imagen") : "/Uploads/default-team.png" %>'
                                    class="rounded-circle mb-3" width="80" height="80" style="object-fit: cover;">
                                <h5 class="card-title fw-bold"><%# Eval("Nombre") %></h5>
                                <p class="card-text text-muted small">
                                    Creado por: <%# Eval("NombreCreador") ?? "Admin" %>
                                </p>

                                <asp:Button ID="btnSolicitar" runat="server" Text="Solicitar Unirme"
                                    CommandName="Unirse" CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-outline-success w-100 mt-2 rounded-pill" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Panel ID="pnlSinResultados" runat="server" Visible="false" CssClass="col-12 text-center text-muted mt-4">
                <i class="bi bi-search display-6"></i>
                <p class="mt-2">No se encontraron equipos con ese nombre.</p>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
