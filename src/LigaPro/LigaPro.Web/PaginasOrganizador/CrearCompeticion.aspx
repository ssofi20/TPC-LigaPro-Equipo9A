<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearCompeticion.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.CrearCompeticion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-5">
        <h2 class="fw-bold mb-4">Crear Competición</h2>
        <div class="row">
            <div class="col-md-6">

                <!-- Campo Nombre -->
                <div class="mb-4">
                    <label for="txtNombre" class="form-label fw-semibold">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-lg rounded-3"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio."
                        ControlToValidate="txtNombre" CssClass="text-danger" runat="server" />
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
                    <asp:Button ID="btnCrear" runat="server" Text="Aceptar"
                        CssClass="btn btn-success px-4 py-2" CausesValidation="true" OnClick="btnCrear_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                        CssClass="btn btn-secondary px-4 py-2" CausesValidation="false" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </div>
    </div>





</asp:Content>
