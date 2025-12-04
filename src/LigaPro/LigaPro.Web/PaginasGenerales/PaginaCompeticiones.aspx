<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaginaCompeticiones.aspx.cs" Inherits="LigaPro.Web.PaginasGenerales.PaginaCompeticiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="px-4 py-5 my-5 text-center">
            <h1 class="display-5 fw-bold text-body-emphasis">Competiciones</h1>
        </div>

        <div class="card border-0 shadow-sm">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <table class="table table-hover align-middle mb-0">
                        <thead class="bg-light">
                            <tr>
                                <th class="ps-4 py-3">Nombre</th>
                                <th class="ps-4 py-3">Organizador</th>
                                <th class="ps-4 py-3">Estructura</th>
                                <th class="ps-4 py-3">Inscriptos</th>
                                <th class="ps-4 py-3">Estado</th>
                            </tr>
                        </thead>
                        <tbody>

                            <asp:Repeater ID="rptTorneos" runat="server">
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
                                            <span class="badge bg-secondary badge bg-light text-dark border">
                                                <%# Eval("Organizador.NombrePublico") %>
                                            </span>
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
                                                    <div class="progress-bar bg-primary" role="progressbar"
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
                    <h5 class="text-muted">No exissten torneos activos</h5>
                </asp:Panel>
            </div>
        </div>

    </div>

</asp:Content>
