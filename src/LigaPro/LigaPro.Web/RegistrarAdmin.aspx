<%@ Page Title="Registrar Administrador" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarAdmin.aspx.cs" Inherits="LigaPro.Web.RegistrarAdmin" %>

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
                        <!-- Campo Nombre Publico -->
                        <div class="mb-3">
                            <label for="txtNombrePublico" class="form-label">Nombre Publico</label>
                            <asp:TextBox ID="txtNombrePublico" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="txtNombrePublico" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- Campo Seleccionar Logo -->
                        <div class="mb-3">
                            <label for="FileUploadControl" class="form-label">Subir Imagen</label>
                            <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="form-control" />
                        </div>
                        <!-- Campo Telefono Contacto-->
                        <div class="mb-3">
                            <label for="txtTelefonoContacto" class="form-label">Teléfono de Contacto</label>
                            <asp:TextBox ID="txtTelefonoContacto" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El teléfono de contacto es obligatorio." ControlToValidate="txtTelefonoContacto" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- Campo Password -->
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria." ControlToValidate="txtPassword" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- Campo Confirmar Password -->
                        <div class="mb-3">
                            <label for="txtConfirmPassword" class="form-label">Confirmar Contraseña</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:CompareValidator ErrorMessage="Las contraseñas no coinciden." ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" Operator="Equal" CssClass="text-danger" runat="server" />
                        </div>

                        <div class="d-grid">
                            <asp:Button ID="btnRegistrarAdmin" runat="server" Text="Crear Cuenta de Admin" CssClass="btn btn-success" OnClick="btnRegistrarAdmin_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
