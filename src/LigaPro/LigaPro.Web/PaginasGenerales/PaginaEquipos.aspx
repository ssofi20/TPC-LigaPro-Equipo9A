<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaginaEquipos.aspx.cs" Inherits="LigaPro.Web.PaginasGenerales.PaginaEquipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5" >
        <div class="px-4 py-5 my-5 text-center">
            <h1 class="display-5 fw-bold text-body-emphasis">Equipos</h1>
        </div>

        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater ID="rptEquipos" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 shadow-sm border-0">
                            <div class="card-body text-center">
                                <img src='<%# Eval("Imagen") %>' class="rounded-circle mb-3" width="80" height="80" style="object-fit: cover;" />
                                <h5 class="card-title fw-bold"><%# Eval("Nombre") %></h5>
                                <p class="card-text text-muted small"><%# Eval("NombreCreador") %> </p>
                                </a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Panel ID="pnlSinEquipos" runat="server" Visible="false" CssClass="col-12 text-center mt-5">
                <p class="text-muted">No hay equipos activos en este momento</p>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
