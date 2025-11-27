<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarUsuario.aspx.cs" Inherits="LigaPro.Web.Auth.EliminarUsuario" %>

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
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CausesValidation="true" OnClick="btnEliminar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
