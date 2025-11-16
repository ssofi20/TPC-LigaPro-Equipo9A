<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarPerfilAdmin.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.ModificarPerfilAdmin" %>

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
                    
                        <!-- Nombre -->
                        <div class="mb-3">
                            <label for="txtNombrePublico" class="form-label">Nombre Publico</label>
                            <asp:TextBox ID="txtNombrePublico" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="txtNombrePublico" CssClass="text-danger" runat="server" />
                        </div>
                        <!-- Logo -->
                        <div class="mb-3">
                            <label for="FileUploadControl" class="form-label">Subir Imagen</label>
                            <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="form-control" />
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
