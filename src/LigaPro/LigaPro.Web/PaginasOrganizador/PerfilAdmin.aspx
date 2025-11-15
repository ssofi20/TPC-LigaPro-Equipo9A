<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilAdmin.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.PerfilAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- DASHBOARD DEL ORGANIZADOR (ADMINISTRADOR DE COMPETICIONES) -->
    <div class="container">
        <div class="row">
            <div class="col align-self-center">
                <br />
                <h1 class="text-center">Gestionar Perfil</h1>
                <!-- Cards Gestion y cerrar cuenta -->

                <a href="/Default.aspx" style="text-decoration: none">
                    <div class="card shadow-sm mb-3 border-0 rounded-3">
                        <div class="card-body d-flex align-items-center">
                            <img src=" " class="rounded-circle me-3" width="60" height="60" />
                            <div class="flex-grow-1">
                                <h5 class="mb-0 fw-semibold">Perfil</h5>
                                <small class="text-muted">Editar información personal</small>
                            </div>
                        </div>
                    </div>
                </a>

                <a href="/Default.aspx" style="text-decoration: none">
                    <div class="card shadow-sm mb-3 border-0 rounded-3">
                        <div class="card-body d-flex align-items-center">
                            <img src=" " class="rounded-circle me-3" width="60" height="60" />
                            <div class="flex-grow-1">
                                <h5 class="mb-0 fw-semibold">Eliminar cuenta</h5>
                            </div>
                        </div>
                    </div>
                </a>

            </div>
        </div>
    </div>

</asp:Content>
