<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarPerfilJugador.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.ModificarPerfilJugador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h3>Modificar datos</h3>
                    </div>
                    <div class="card-body">
                        <!-- NOMBRE/S -->
                        <div class="mb-3">
                            <label for="txtNombre" class="form-label">Nombre/s</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="txtNombre" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- APELLIDO/S -->
                        <div class="mb-3">
                            <label for="txtApellido" class="form-label">Apellido/s</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio." ControlToValidate="txtApellido" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- FECHA NACIMIENTO -->
                        <div class="mb-3">
                            <label for="txtFechaNacimiento" class="form-label">Fecha de Nacimiento</label>
                            <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="La fecha de nacimiento es obligatoria." ControlToValidate="txtFechaNacimiento" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- NOMBRE DE USUARIO -->
                        <div class="mb-3">
                            <label for="txtNombreUsuario" class="form-label">Nombre de usuario</label>
                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El nombre de usuario es obligatorio." ControlToValidate="txtNombreUsuario" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- EMAIL -->
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio." ControlToValidate="txtEmail" CssClass="text-danger" runat="server" />

                            <div class="d-flex justify-content-center gap-3">
                                <asp:Button ID="btnAceptar" runat="server" Text="Modificar" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
