<%@ Page Title="Nuevo Torneo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearTorneo.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.CrearTorneo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* MINIMALISMO TOTAL */
        body {
            background-color: #f8f9fa; /* Fondo gris muy suave */
        }

        .card {
            border: 1px solid #e9ecef;
            box-shadow: 0 2px 4px rgba(0,0,0,0.02); /* Sombra casi invisible */
        }

        .form-label {
            font-size: 0.85rem;
            text-transform: uppercase;
            letter-spacing: 0.05em;
            color: #6c757d;
            font-weight: 600;
            margin-bottom: 0.5rem;
        }

        .form-control, .input-group-text {
            border: 1px solid #dee2e6;
            background-color: #fff;
            padding: 0.7rem 1rem;
            font-size: 0.95rem;
        }

            .form-control:focus {
                border-color: #212529; /* Borde negro al enfocar */
                box-shadow: none;
            }

        /* DISEÑO DE RADIO BUTTONS COMO TARJETAS (TILES) */
        .radio-tile-group {
            display: flex;
            gap: 1rem;
        }

        .radio-tile {
            position: relative;
            display: flex;
            align-items: center;
            padding: 1.5rem;
            border: 1px solid #dee2e6;
            border-radius: 8px;
            background: #fff;
            cursor: pointer;
            transition: all 0.2s ease;
            width: 100%;
        }

            .radio-tile:hover {
                border-color: #adb5bd;
                background-color: #fcfcfc;
            }

            /* Truco para detectar cuando el radio dentro del div está chequeado (Soporte moderno) */
            .radio-tile:has(input:checked) {
                border-color: #212529;
                background-color: #fff;
                box-shadow: 0 0 0 1px #212529; /* Simula borde doble */
            }

            /* Estilo del texto dentro del radio tile */
            .radio-tile label {
                cursor: pointer;
                width: 100%;
                margin-left: 10px;
                font-weight: 600;
                color: #212529;
            }

            .radio-tile small {
                display: block;
                font-weight: 400;
                color: #6c757d;
                margin-top: 4px;
            }

        /* Animación suave */
        .fade-in {
            animation: fadeIn 0.4s ease-out;
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(5px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-lg-8">

                <div class="d-flex justify-content-between align-items-end mb-5">
                    <div>
                        <h2 class="fw-bold text-dark mb-1">Crear Torneo</h2>
                        <p class="text-muted mb-0">Configuración de la nueva competencia</p>
                    </div>
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                        CssClass="btn btn-link text-decoration-none text-muted p-0" CausesValidation="false" OnClick="btnCancelar_Click" />
                </div>

                <div class="card mb-4 p-4">
                    <h5 class="fw-bold mb-4">1. Información General</h5>
                    <div class="row g-4">
                        <div class="col-md-8">
                            <label for="txtNombre" class="form-label">Nombre del Torneo</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej. Torneo Apertura 2025"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtNombre" CssClass="text-danger small mt-1 d-block" runat="server" Display="Dynamic" />
                        </div>
                        <div class="col-md-4">
                            <label for="txtCupos" class="form-label">Cupo de Equipos</label>
                            <asp:TextBox ID="txtCupos" runat="server" CssClass="form-control" TextMode="Number" placeholder="16"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtCupos" CssClass="text-danger small mt-1 d-block" runat="server" Display="Dynamic" />
                        </div>
                    </div>
                </div>

                <asp:UpdatePanel ID="upFormato" runat="server">
                    <ContentTemplate>
                        <div class="card mb-4 p-4">
                            <h5 class="fw-bold mb-4">2. Formato de Competencia</h5>

                            <div class="row g-3">
                                <div class="col-md-6">
                                    <div class="radio-tile">
                                        <asp:RadioButton ID="rbConFases" GroupName="Fases" runat="server"
                                            AutoPostBack="true" OnCheckedChanged="Formato_Changed" />
                                        <label for="<%= rbConFases.ClientID %>">
                                            Fase de Grupos
                                            <small>Estilo Mundial. Grupos + Eliminatorias.</small>
                                        </label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="radio-tile">
                                        <asp:RadioButton ID="rbSinFases" GroupName="Fases" runat="server"
                                            AutoPostBack="true" OnCheckedChanged="Formato_Changed" Checked="true" />
                                        <label for="<%= rbSinFases.ClientID %>">
                                            Eliminación Directa
                                            <small>Estilo Copa. Sin grupos previos.</small>
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnlPuntos" runat="server" Visible="false" CssClass="mt-4 pt-4 border-top fade-in">

                                <h6 class="fw-bold mb-3">Configuración de Grupos</h6>
                                <div class="row g-3 mb-4">
                                    <div class="col-md-6">
                                        <label class="form-label">Cantidad de Grupos</label>
                                        <div class="input-group">
                                            <span class="input-group-text bg-light text-muted">#</span>
                                            <asp:TextBox ID="txtCantidadGrupos" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ej. 4"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ErrorMessage="Indica cuántos grupos tendrá el torneo" ControlToValidate="txtCantidadGrupos" CssClass="text-danger small mt-1 d-block" runat="server" Display="Dynamic" />
                                    </div>
                                </div>
                                <h6 class="fw-bold mb-3">Sistema de Puntuación</h6>
                                <div class="row g-3">
                                    <div class="col-md-4">
                                        <label class="form-label">Por Victoria</label>
                                        <asp:TextBox ID="txtPuntosVictoria" runat="server" CssClass="form-control" TextMode="Number" Text="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtPuntosVictoria" CssClass="text-danger" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Por Empate</label>
                                        <asp:TextBox ID="txtPuntosEmpate" runat="server" CssClass="form-control" TextMode="Number" Text="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtPuntosEmpate" CssClass="text-danger" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Por Derrota</label>
                                        <asp:TextBox ID="txtPuntosDerrota" runat="server" CssClass="form-control" TextMode="Number" Text="0"></asp:TextBox>
                                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtPuntosDerrota" CssClass="text-danger" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="card mb-5 p-4">
                    <h5 class="fw-bold mb-4">3. Disciplina</h5>
                    <div class="row g-4">
                        <div class="col-md-6">
                            <label class="form-label">Acumulación Amarillas</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtAmarillas" runat="server" CssClass="form-control" TextMode="Number" Text="5"></asp:TextBox>
                                <span class="input-group-text bg-light text-muted small">tarjetas</span>
                            </div>
                            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtAmarillas" CssClass="text-danger" runat="server" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Sanción Roja Directa</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtRojas" runat="server" CssClass="form-control" TextMode="Number" Text="1"></asp:TextBox>
                                <span class="input-group-text bg-light text-muted small">partidos</span>
                            </div>
                            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtRojas" CssClass="text-danger" runat="server" />
                        </div>
                    </div>
                </div>

                <div class="d-grid">
                    <asp:Button ID="btnCrear" runat="server" Text="Crear Torneo"
                        CssClass="btn btn-dark btn-lg py-3 fw-bold" OnClick="btnCrear_Click" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>
