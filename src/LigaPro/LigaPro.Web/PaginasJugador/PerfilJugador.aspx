<%@ Page Title="Mi Perfil - LigaPro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilJugador.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.PerfilJugador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">

    <style>
        /* Para el efecto al pasar el mouse */
        .card-hover {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            cursor: pointer;
        }

            .card-hover:hover {
                transform: translateY(-5px);
                box-shadow: 0 .5rem 1rem rgba(0,0,0,.15) !important;
            }

        .icon-box {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 64px;
            height: 64px;
            border-radius: 50%;
            margin-bottom: 1rem;    
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container py-5">

        <div class="row justify-content-center mb-5">
            <div class="col-md-8">
                <div class="card border-0 shadow-sm bg-primary text-white overflow-hidden">
                    <div class="card-body p-4 d-flex align-items-center">
                        <div class="bg-white text-primary rounded-circle d-flex align-items-center justify-content-center me-4" style="width: 80px; height: 80px;">
                            <i class="bi bi-person-fill fs-1"></i>
                        </div>
                        <div>
                            <h2 class="mb-0 fw-bold">Mi Panel de Jugador</h2>
                            <p class="mb-0 opacity-75">Administra tus datos y seguridad en LigaPro</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row justify-content-center g-4">

            <div class="col-md-4 col-sm-6">
                <a href="ModificarPerfilJugador.aspx" class="text-decoration-none text-dark">
                    <div class="card h-100 border-0 shadow-sm card-hover text-center p-3">
                        <div class="card-body">
                            <div class="icon-box bg-light text-primary">
                                <i class="bi bi-person-lines-fill fs-2"></i>
                            </div>
                            <h5 class="card-title fw-bold">Datos Personales</h5>
                            <p class="card-text text-muted small">Actualiza tu nombre, foto, edad y datos de contacto.</p>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-md-4 col-sm-6">
                <a href="ModificarSeguridadJugador.aspx" class="text-decoration-none text-dark">
                    <div class="card h-100 border-0 shadow-sm card-hover text-center p-3">
                        <div class="card-body">
                            <div class="icon-box bg-light text-warning">
                                <i class="bi bi-shield-lock-fill fs-2"></i>
                            </div>
                            <h5 class="card-title fw-bold">Seguridad</h5>
                            <p class="card-text text-muted small">Cambia tu contraseña y protege tu cuenta.</p>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-md-8">
                <hr class="my-4 text-muted opacity-25">
                <a href="/Auth/EliminarUsuario.aspx" class="text-decoration-none">
                    <div class="card border-danger border-opacity-25 shadow-sm card-hover bg-light">
                        <div class="card-body d-flex align-items-center p-4">
                            <div class="text-danger me-4">
                                <i class="bi bi-exclamation-triangle-fill fs-1"></i>
                            </div>
                            <div>
                                <h5 class="fw-bold text-danger mb-1">Eliminar Cuenta</h5>
                                <p class="text-muted small mb-0">Esta acción es irreversible. Se borrarán todos tus registros de torneos.</p>
                            </div>
                            <div class="ms-auto">
                                <i class="bi bi-chevron-right text-muted"></i>
                            </div>
                        </div>
                    </div>
                </a>
            </div>

        </div>
    </div>

</asp:Content>
