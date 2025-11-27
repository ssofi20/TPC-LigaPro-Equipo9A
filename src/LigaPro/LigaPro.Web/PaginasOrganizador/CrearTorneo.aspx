<%@ Page Title="Nuevo Torneo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearTorneo.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.CrearTorneo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .step-card {
            border-left: 5px solid #0d6efd;
            transition: transform 0.2s;
        }

            .step-card:hover {
                transform: translateY(-2px);
            }

        .form-label {
            font-weight: 600;
            color: #495057;
        }

        .header-step {
            background-color: #fff;
            border-bottom: 1px solid #e9ecef;
        }
        /* Animación suave para el panel de puntos */
        .fade-in {
            animation: fadeIn 0.5s;
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-lg-9">

                <!-- CABECERA -->
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <div>
                        <h2 class="fw-bold mb-1">Nuevo Torneo</h2>
                        <p class="text-muted mb-0">Configura tu competición en 3 pasos.</p>
                    </div>
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                        CssClass="btn btn-outline-secondary btn-sm" CausesValidation="false" OnClick="btnCancelar_Click" />
                </div>

                <!-- PASO 1: DATOS GENERALES -->
                <div class="card shadow-sm mb-4 step-card border-primary">
                    <div class="card-header header-step py-3">
                        <h5 class="mb-0 text-primary"><i class="bi bi-info-circle-fill me-2"></i>1. Datos Generales</h5>
                    </div>
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-8">
                                <label for="txtNombre" class="form-label">Nombre del Torneo</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-lg" placeholder="Ej: Copa Apertura 2025"></asp:TextBox>
                                <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="txtNombre" CssClass="text-danger small" runat="server" Display="Dynamic" />
                            </div>
                            <div class="col-md-4">
                                <label for="txtCupos" class="form-label">Cupo Máximo</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtCupos" runat="server" CssClass="form-control form-control-lg" TextMode="Number" placeholder="16"></asp:TextBox>
                                    <span class="input-group-text"><i class="bi bi-people"></i></span>
                                </div>
                                <asp:RequiredFieldValidator ErrorMessage="Define el cupo." ControlToValidate="txtCupos" CssClass="text-danger small" runat="server" Display="Dynamic" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- PASO 2: ESTRUCTURA (Con AJAX para mostrar/ocultar puntos) -->
                <asp:UpdatePanel ID="upFormato" runat="server">
                    <ContentTemplate>
                        <div class="card shadow-sm mb-4 step-card" style="border-left-color: #198754;">
                            <div class="card-header header-step py-3">
                                <h5 class="mb-0 text-success"><i class="bi bi-trophy-fill me-2"></i>2. Estructura</h5>
                            </div>
                            <div class="card-body">

                                <label class="form-label mb-3">¿Cómo se jugará el torneo?</label>
                                <div class="row mb-4">
                                    <div class="col-md-6">
                                        <div class="form-check p-3 border rounded bg-light h-100">
                                            <asp:RadioButton ID="rbConFases" GroupName="Fases" runat="server"
                                                AutoPostBack="true" OnCheckedChanged="Formato_Changed" CssClass="form-check-input" />
                                            <label class="form-check-label fw-bold ms-2" for="<%= rbConFases.ClientID %>">
                                                Con Fase de Grupos
                                            </label>
                                            <div class="text-muted small ms-4 mt-1">
                                                Los equipos se dividen en grupos y suman puntos para clasificar.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-check p-3 border rounded bg-light h-100">
                                            <asp:RadioButton ID="rbSinFases" GroupName="Fases" runat="server"
                                                AutoPostBack="true" OnCheckedChanged="Formato_Changed" CssClass="form-check-input" Checked="true" />
                                            <label class="form-check-label fw-bold ms-2" for="<%= rbSinFases.ClientID %>">
                                                Eliminación Directa (Sin Grupos)
                                            </label>
                                            <div class="text-muted small ms-4 mt-1">
                                                Solo llaves de eliminación (Octavos, Cuartos, etc.). No hay tabla de puntos.
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- PANEL PUNTOS (Solo visible si hay grupos) -->
                                <asp:Panel ID="pnlPuntos" runat="server" Visible="false" CssClass="bg-success bg-opacity-10 p-4 rounded fade-in">
                                    <h6 class="fw-bold text-success mb-3"><i class="bi bi-calculator me-2"></i>Configuración de Puntos (Fase de Grupos)</h6>
                                    <div class="row g-3">
                                        <div class="col-md-4">
                                            <label class="form-label small">Por Victoria</label>
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-text bg-white text-success fw-bold">+</span>
                                                <asp:TextBox ID="txtPuntosVictoria" runat="server" CssClass="form-control" TextMode="Number" Text="3"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtPuntosVictoria" CssClass="text-danger" runat="server" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-label small">Por Empate</label>
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-text bg-white text-primary fw-bold">=</span>
                                                <asp:TextBox ID="txtPuntosEmpate" runat="server" CssClass="form-control" TextMode="Number" Text="1"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtPuntosEmpate" CssClass="text-danger" runat="server" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-label small">Por Derrota</label>
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-text bg-white text-danger fw-bold">-</span>
                                                <asp:TextBox ID="txtPuntosDerrota" runat="server" CssClass="form-control" TextMode="Number" Text="0"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtPuntosDerrota" CssClass="text-danger" runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- PASO 3: REGLAS -->
                <div class="card shadow-sm mb-4 step-card" style="border-left-color: #dc3545;">
                    <div class="card-header header-step py-3">
                        <h5 class="mb-0 text-danger"><i class="bi bi-file-earmark-ruled-fill me-2"></i>3. Reglas Disciplinarias</h5>
                    </div>
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label class="form-label">Límite de Amarillas</label>
                                <div class="input-group">
                                    <span class="input-group-text bg-warning border-warning text-white"><i class="bi bi-files"></i></span>
                                    <asp:TextBox ID="txtAmarillas" runat="server" CssClass="form-control" TextMode="Number" Text="5"></asp:TextBox>
                                    <span class="input-group-text bg-light">para suspensión</span>
                                </div>
                                <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtAmarillas" CssClass="text-danger" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Roja Directa</label>
                                <div class="input-group">
                                    <span class="input-group-text bg-danger border-danger text-white"><i class="bi bi-file-fill"></i></span>
                                    <asp:TextBox ID="txtRojas" runat="server" CssClass="form-control" TextMode="Number" Text="1"></asp:TextBox>
                                    <span class="input-group-text bg-light">fechas sanción</span>
                                </div>
                                <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtRojas" CssClass="text-danger" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- BOTÓN CREAR -->
                <div class="d-grid gap-2 mb-5">
                    <asp:Button ID="btnCrear" runat="server" Text="Confirmar y Crear Torneo"
                        CssClass="btn btn-primary btn-lg shadow fw-bold py-3" OnClick="btnCrear_Click" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>
