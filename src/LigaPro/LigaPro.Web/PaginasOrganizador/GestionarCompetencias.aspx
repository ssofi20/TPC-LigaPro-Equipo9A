<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionarCompetencias.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.GestionarCompetencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .action-link {
            color: #6c757d !important;
            text-decoration: none !important;
            margin: 0 4px;
            padding: 2px 6px !important;
            font-size: 0.85rem !important;
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
        <h2 class="mb-4 text-center fw-bold">Gestionar competiciones</h2>
        <div class="row justify-content-center">
            <div class="col-md-12">
                <div class="card shadow-sm">

                    <div class="card-body">
                        <!-- Panel seleccion-->
                        <asp:Panel ID="PanelSeleccionar" runat="server" Visible="true">

                            <!-- Card header -->
                            <div class="card-header bg-light">
                                <div class="row fw-bold">
                                    <div class="col-4">Nombre</div>
                                    <div class="col-3">Organizador</div>
                                    <div class="col-3">Estado</div>
                                    <div class="col-2 text-end">Acciones</div>
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

                                                <div class="col-3">
                                                    <span class="badge bg-secondary">
                                                        <%#SepararEspacios(Eval("Estado").ToString()) %>
                                                    </span>
                                                </div>

                                                <div class="col-2 text-end">
                                                    <asp:LinkButton ID="btnEdit" runat="server"
                                                        Command="Select" CssClass="action-link py-0 px-1"
                                                        Text="Editar"> 
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
                        </asp:Panel>
                        <!-- PANEL MODIFICAR -->
                        <asp:Panel ID="PanelModificar" runat="server" Visible="false">
                            <div class="container mt-4">
                                <div class="row justify-content-center">
                                    <div class="col-md-10">
                                        <div class="card-body">

                                            <!-- Campo Nombre -->
                                            <div class="mb-3">
                                                <label for="txtNombre" class="form-label">Nombre</label>
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio."
                                                    ControlToValidate="txtNombre" CssClass="text-danger" runat="server" />
                                            </div>

                                            <!-- Organizador -->
                                            <div class="mb-3">
                                                <label for="ddlOrganizador" class="form-label">Organizador/a</label>
                                                <asp:DropDownList ID="ddlOrganizador" runat="server" CssClass="form-select"></asp:DropDownList>
                                            </div>

                                            <!-- Reglamento -->
                                            <div class="mb-3">
                                                <label for="txtPv" class="form-label">Puntos por victoria</label>
                                                <asp:TextBox ID="txtPv" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="mb-3">
                                                <label for="txtPe" class="form-label">Puntos por empate</label>
                                                <asp:TextBox ID="txtPe" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="mb-3">
                                                <label for="txtTas" class="form-label">Tarjetas amarillas para suspensión</label>
                                                <asp:TextBox ID="txtTas" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="mb-3">
                                                <label for="txtPsrd" class="form-label">Partidos suspensión por roja directa</label>
                                                <asp:TextBox ID="txtPsrd" runat="server" CssClass="form-control" />
                                            </div>

                                            <!-- Fases -->
                                            <div class="mb-3">
                                                <asp:RadioButton ID="rbConFases" GroupName="Fases" Text="Tiene Fases"
                                                    AutoPostBack="true" OnCheckedChanged="rbConFases_CheckedChanged" runat="server" />
                                            </div>

                                            <asp:Panel ID="panelOpcionesFases" runat="server" Visible="false">
                                                <div class="mb-3">
                                                    <asp:RadioButton ID="rbIda" GroupName="formato" Text="Ida" runat="server" />
                                                    <asp:RadioButton ID="rbIdaVuelta" GroupName="formato" Text="Ida y vuelta" runat="server" />
                                                </div>
                                            </asp:Panel>

                                            <div class="mb-3">
                                                <asp:RadioButton ID="rbSinFases" GroupName="Fases" Text="No tiene Fases"
                                                    AutoPostBack="true" OnCheckedChanged="rbSinFases_CheckedChanged" runat="server" />
                                            </div>

                                            <div class="d-flex justify-content-center gap-3 mt-3">
                                                <asp:Button ID="btnModificar" runat="server" Text="Modificar"
                                                    CssClass="btn btn-success" OnClick="btnModificar_Click" />
                                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                                    CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                                            </div>

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
        </div>
    </div>





</asp:Content>
