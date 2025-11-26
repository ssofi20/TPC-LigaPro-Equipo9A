<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionarCompetencias.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.GestionarCompetencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h3>Gestionar Competiciones</h3>
                    </div>
                    <div class="card-body">
                        <asp:Panel ID="PanelSeleccionar" runat="server" Visible="true">
                            <asp:GridView ID="dgvItems" runat="server" DataKeyNames="Id"
                                CssClass="table" AutoGenerateColumns="false"
                                OnSelectedIndexChanged="dgvItems_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                    <asp:BoundField HeaderText="Organizador" DataField="IdOrganizador" />
                                    <asp:BoundField HeaderText="Estado" DataField="Estado" />
                                    <asp:CommandField HeaderText="Accion" ShowSelectButton="true" SelectText="Modificar" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="PanelModificar" runat="server" Visible="false">
                            <div class="container mt-5">
                                <div class="row justify-content-center">
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h3>Crear Competencia</h3>
                                            </div>
                                            <div class="card-body">
                                                <!-- Campo Nombre -->
                                                <div class="mb-3">
                                                    <label for="txtNombre" class="form-label">Nombre</label>
                                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="txtNombre" CssClass="text-danger" runat="server" />
                                                </div>
                                                <!-- Campo Organizador-->
                                                <div class="mb-3">
                                                    <label for="ddlOrganizador" class="form-label">Organizador/a</label>
                                                    <asp:DropDownList ID="ddlOrganizador" runat="server" CssClass="form-label"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ErrorMessage="El organizador es obligatorio." ControlToValidate="ddlOrganizador" CssClass="text-danger" runat="server" />
                                                </div>
                                                <!-- Campo Reglamento -->
                                                <div class="mb-3">
                                                    <label for="txtPv" class="form-label">Puntos por victoria:</label>
                                                    <asp:TextBox ID="txtPv" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio." ControlToValidate="txtPv" CssClass="txt-danger" runat="server" />
                                                </div>

                                                <div class="mb-3">
                                                    <label for="txtPe" class="form-label">Puntos por empate:</label>
                                                    <asp:TextBox ID="txtPe" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio." ControlToValidate="txtPe" CssClass="txt-danger" runat="server" />
                                                </div>
                                                <div class="mb-3">
                                                    <label for="txtTas" class="form-label">Tarjetas amarillas para suspension:</label>
                                                    <asp:TextBox ID="txtTas" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio." ControlToValidate="txtTas" CssClass="txt-danger" runat="server" />
                                                </div>

                                                <div class="mb-3">
                                                    <label for="txtPsrd" class="form-label">Partidos Suspension Por Roja Directa:</label>
                                                    <asp:TextBox ID="txtPsrd" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ErrorMessage="Este campo es obligatorio." ControlToValidate="txtPsrd" CssClass="txt-danger" runat="server" />
                                                </div>

                                                <!-- Campo Fases Torneo -->
                                                <div class="mb-3">
                                                    <asp:RadioButton ID="rbConFases" GroupName="Fases" Text="Tiene Fases" AutoPostBack="true" OnCheckedChanged="rbConFases_CheckedChanged" runat="server" />
                                                </div>

                                                <div class="mb-3">
                                                    <asp:Panel ID="panelOpcionesFases" runat="server" Visible="false">

                                                        <asp:RadioButton ID="rbIda"
                                                            GroupName="formato"
                                                            Text="Ida"
                                                            runat="server" />

                                                        <asp:RadioButton ID="rbIdaVuelta"
                                                            GroupName="formato"
                                                            Text="Ida y vuelta"
                                                            runat="server" />
                                                    </asp:Panel>
                                                </div>

                                                <div class="mb-3">
                                                    <asp:RadioButton ID="rbSinFases" GroupName="Fases" Text="No tiene Fases" AutoPostBack="true" OnCheckedChanged="rbSinFases_CheckedChanged" runat="server" />
                                                </div>

                                                <div class="d-flex justify-content-center gap-3">
                                                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-success" OnClick="btnModificar_Click" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                                                </div>
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
