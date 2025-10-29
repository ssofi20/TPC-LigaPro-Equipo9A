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
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio." ControlToValidate="txtEmail" CssClass="text-danger" runat="server" />
                        </div>

                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria." ControlToValidate="txtPassword" CssClass="text-danger" runat="server" />
                        </div>

                        <div class="mb-3">
                            <label for="txtConfirmPassword" class="form-label">Confirmar Contraseña</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:CompareValidator ErrorMessage="Las contraseñas no coinciden." ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" Operator="Equal" CssClass="text-danger" runat="server" />
                        </div>

                        <div class="d-grid">
                            <asp:Button ID="btnRegistrarAdmin" runat="server" Text="Crear Cuenta de Admin" CssClass="btn btn-success"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
