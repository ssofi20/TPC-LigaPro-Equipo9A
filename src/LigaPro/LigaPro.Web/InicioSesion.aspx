<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="LigaPro.Web.InicioSesion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <div class="card shadow-sm">
                    <div class="card-header">
                        <h3>Iniciar Sesión</h3>
                    </div>
                    <div class="card-body">
                        <!-- Email -->
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio." ControlToValidate="txtEmail" CssClass="text-danger" Display="Dynamic" runat="server" />
                        </div>

                        <!-- Contraseña -->
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria." ControlToValidate="txtPassword" CssClass="text-danger" Display="Dynamic" runat="server" />
                        </div>

                        <!-- Mensaje de Error -->
                        <div class="mb-3">
                            <asp:Literal ID="lblError" runat="server" />
                        </div>

                        <!-- Botón de Login -->
                        <div class="d-grid">
                            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnLogin_Click"/>
                        </div>

                        <div class="text-center mt-3">
                            <a href="RecuperarPassword.aspx">¿Olvidaste tu contraseña?</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
