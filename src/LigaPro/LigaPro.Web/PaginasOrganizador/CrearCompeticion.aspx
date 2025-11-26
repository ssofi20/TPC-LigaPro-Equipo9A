<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearCompeticion.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.CrearCompeticion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



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

                        <div class="d-grid">
                            <asp:Button ID="btnCrear" runat="server" Text="Crear Competencia" CssClass="btn btn-success" OnClick="btnCrear_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
