<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionarCompetencias.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.GestionarCompetencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .action-link {
            color: #6c757d !important;
            text-decoration: none !important;
            margin: 0 4px;
            padding: 2px 6px !important;
            font-size: 0.80rem !important;
        }

            .action-link:not(:last-child)::after {
                content: "|";
                margin-left: 6px;
                color: #b4b4b4;
            }

            .action-link:hover {
                color: #5a6268 !important;
            }

            .action-link.text-danger {
                color: #dc3545 !important;
            }

                .action-link.text-danger:hover {
                    color: #bb2d3b !important;
                }

        .table-modern {
            width: 100%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-12">
                <!-- Panel seleccion-->
                <asp:Panel ID="PanelSeleccionar" runat="server" Visible="true">
                    <h2 class="mb-4 text-center fw-bold">Gestionar competiciones</h2>
                    <div class="card shadow-sm">

                        <div class="card-body">

                            <!-- Card header -->
                            <div class="card-header bg-light">
                                <div class="row fw-bold">
                                    <div class="col-4">Nombre</div>
                                    <div class="col-3">Organizador</div>
                                    <div class="col-2">Estado</div>
                                    <div class="col-3 text-end">Acciones</div>
                                </div>
                            </div>

                            <asp:GridView ID="dgvItems" runat="server" DataKeyNames="Id"
                                AutoGenerateColumns="false"
                                CssClass="table table-modern" GridLines="None" ShowHeader="false"
                                OnSelectedIndexChanged="dgvItems_SelectedIndexChanged"
                                OnRowCommand="dgvItems_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="row align-items-center py-2 border-bottom">

                                                <div class="col-4">
                                                    <%#Eval("Nombre") %>
                                                </div>

                                                <div class="col-3">
                                                    <%#Eval("OrganizadorCompetencia.NombrePublico") %>
                                                </div>

                                                <div class="col-2">
                                                    <span class="badge bg-secondary">
                                                        <%#SepararEspacios(Eval("Estado").ToString()) %>
                                                    </span>
                                                </div>

                                                <div class="col-3 text-end">
                                                    <asp:LinkButton ID="btnEdit" runat="server"
                                                        CommandName="Select" CssClass="action-link py-0 px-1"
                                                        Text="Editar"> 
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="btnVerEquipo" runat="server"
                                                        CssClass="action-link py-0 px-1 ms-1"
                                                        Text="Ver Equipos"
                                                        CommandName="VerEquipos" CommandArgument='<%# Eval("Id") %>'>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="btnFixture" runat="server"
                                                        CssClass="action-link py-0 px-1 ms-1"
                                                        Text="Generar Fixture"
                                                        CommandName="GenerarFixture" CommandArgument='<%# Eval("Id") %>'>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="btnCargarResultados" runat="server"
                                                        CssClass="action-link py-0 px-1 ms-1"
                                                        Text="Cargar Resultados"
                                                        CommandName="CargarResultados" CommandArgument='<%# Eval("Id") %>'>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="btnEliminar" runat="server"
                                                        CssClass="action-link py-0 px-1 ms-1"
                                                        Text="Eliminar"
                                                        CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'>
                                                    </asp:LinkButton>

                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>

                <!-- PANEL MODIFICAR -->
                <asp:Panel ID="PanelModificar" runat="server" Visible="false">
                    <div class="container mt-5">
                        <h2 class="fw-bold mb-4">Modificar Competición</h2>
                        <div class="row">
                            <div class="col-md-6">

                                <!-- Campo Nombre -->
                                <div class="mb-4">
                                    <label for="txtNombre" class="form-label fw-semibold">Nombre</label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-lg rounded-3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio."
                                        ControlToValidate="txtNombre" CssClass="text-danger" runat="server" />
                                </div>

                                <!-- Campo Estado -->
                                <div class="mb-4">
                                    <label class="form-label fw-semibold">Estado</label>
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select form-select-lg rounded-3">
                                    </asp:DropDownList>
                                </div>

                                <!-- Fases -->
                                <div class="mb-4 mb-2 d-flex align-items-center">
                                    <asp:RadioButton ID="rbConFases" GroupName="Fases" Text="Tiene Fases" CssClass="ms-3"
                                        AutoPostBack="true" OnCheckedChanged="rbConFases_CheckedChanged" runat="server" />

                                    <asp:RadioButton ID="rbSinFases" GroupName="Fases" Text="No tiene Fases" CssClass="ms-3"
                                        AutoPostBack="true" OnCheckedChanged="rbSinFases_CheckedChanged" runat="server" />
                                </div>

                                <asp:Panel ID="panelOpcionesFases" runat="server" Visible="false">
                                    <div class="mb-3 ms-4">
                                        <asp:RadioButton ID="rbIda" GroupName="formato" Text="Ida" CssClass="d-block mb-2" runat="server" />
                                        <asp:RadioButton ID="rbIdaVuelta" GroupName="formato" Text="Ida y vuelta" CssClass="d-block" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-md-6">
                                <!-- Reglamento -->
                                <div class="mb-4">
                                    <label for="txtPv" class="form-label fw-semibold">Puntos por victoria</label>
                                    <asp:TextBox ID="txtPv" runat="server" CssClass="form-control form-control-lg rounded-3" />
                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio."
                                        ControlToValidate="txtPv" CssClass="text-danger" runat="server" />
                                </div>

                                <div class="mb-4">
                                    <label for="txtPe" class="form-label fw-semibold">Puntos por empate</label>
                                    <asp:TextBox ID="txtPe" runat="server" CssClass="form-control form-control-lg rounded-3" />
                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio."
                                        ControlToValidate="txtPe" CssClass="text-danger" runat="server" />
                                </div>

                                <div class="mb-4">
                                    <label for="txtTas" class="form-label fw-semibold">Tarjetas amarillas para suspensión</label>
                                    <asp:TextBox ID="txtTas" runat="server" CssClass="form-control form-control-lg rounded-3" />
                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio."
                                        ControlToValidate="txtTas" CssClass="text-danger" runat="server" />
                                </div>

                                <div class="mb-4">
                                    <label for="txtPsrd" class="form-label fw-semibold">Partidos suspensión por roja directa</label>
                                    <asp:TextBox ID="txtPsrd" runat="server" CssClass="form-control form-control-lg rounded-3" />
                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio."
                                        ControlToValidate="txtPsrd" CssClass="text-danger" runat="server" />
                                </div>

                                <!-- Botones -->
                                <div class="d-flex justify-content-end gap-3 mt-4">
                                    <asp:Button ID="btnModificar" runat="server" Text="Modificar"
                                        CssClass="btn btn-success px-4 py-2" CausesValidation="true" OnClick="btnModificar_Click" />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                        CssClass="btn btn-secondary px-4 py-2" CausesValidation="false" OnClick="btnCancelar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>


                <asp:Panel ID="PanelEliminar" runat="server" Visible="false">
                    <div class="container mt-4">
                        <div class="row justify-content-center">
                            <div class="col-md-10">
                                <div class="card-body">
                                    <!-- Contraseña -->
                                    <div class="mb-3">
                                        <label for="txtPass" class="form-label">Contraseña</label>
                                        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria para poder eliminar la cuenta." ControlToValidate="txtPass" CssClass="text-danger" runat="server" />
                                    </div>

                                    <div class="mb-3 text-center">
                                        <label for="txtVerificacion" class="form-label">¿Esta seguro que quiere eliminar su cuenta?</label>
                                    </div>

                                    <div class="d-flex justify-content-center gap-3">
                                        <asp:Button ID="btnBaja" runat="server" Text="Eliminar" CssClass="btn btn-danger" CausesValidation="true" OnClick="btnBaja_Click" />
                                        <asp:Button ID="btnCancelarBaja" runat="server" Text="Cancelar" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelarBaja_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

        </div>
    </div>





</asp:Content>
