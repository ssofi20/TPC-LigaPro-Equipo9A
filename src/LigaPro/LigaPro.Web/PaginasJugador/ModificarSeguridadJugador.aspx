<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarSeguridadJugador.aspx.cs" Inherits="LigaPro.Web.PaginasJugador.ModificarSeguridadJugador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h3>Cambio de Contraseña</h3>
                    </div>
                    <div class="card-body">

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
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
