<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarSecurityAdmin.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.ModificarSecurityAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h3>Registro de Administrador</h3>
                        <p class="text-muted">Crea una cuenta para organizar Ligas y Torneos.</p>
                    </div>
                    <div class="card-body">
                        <!-- Campo Email -->
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio." ControlToValidate="txtEmail" CssClass="text-danger" runat="server" />
                        </div>

                        <!-- Campo Telefono Contacto-->
                        <div class="mb-3">
                            <label for="txtTelefonoContacto" class="form-label">Teléfono de Contacto</label>
                            <asp:TextBox ID="txtTelefonoContacto" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El teléfono de contacto es obligatorio." ControlToValidate="txtTelefonoContacto" CssClass="text-danger" runat="server" />
                        </div>

                        <!-- Campo  Old Password -->
                        <div class="mb-3">
                            <label for="txtOldPass" class="form-label">Contraseña actual</label>
                            <asp:TextBox ID="txtOldPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria." ControlToValidate="txtOldPass" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- Campo New Password -->
                        <div class="mb-3">
                            <label for="txtNewPass" class="form-label">Nueva contraseña</label>
                            <asp:TextBox ID="txtNewPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria." ControlToValidate="txtNewPass" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- Campo Confirmar Password -->
                        <div class="mb-3">
                            <label for="txtConfirmNewPass" class="form-label">Confirmar Contraseña</label>
                            <asp:TextBox ID="txtConfirmNewPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:CompareValidator ErrorMessage="Las contraseñas no coinciden." ControlToValidate="txtConfirmNewPass" ControlToCompare="txtNewPass" Operator="Equal" CssClass="text-danger" runat="server" />
                        </div>

                        <div class="d-flex justify-content-center gap-3">
                            <asp:Button ID="btnAceptar" runat="server" Text="Modificar" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
