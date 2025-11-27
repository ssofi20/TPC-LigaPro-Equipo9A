<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisCompeticiones.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.MisCompeticiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-center fw-bold">Gestionar competiciones</h2>
        <div class="row justify-content-center">
            <div class="col-md-12">
                <div class="card shadow-sm">

                    <div class="card-body">
                        <!-- Panel seleccion-->
                        <asp:Panel ID="PanelSeleccionar" runat="server" Visible="true">

                            <!-- Card header -->
                            <div class="card-header bg-light">
                                <div class="row fw-bold">
                                    <div class="col-4 text-start">Nombre</div>
                                    <div class="col-4 text-center">Organizador</div>
                                    <div class="col-4 text-end">Estado</div>
                                </div>
                            </div>

                            <asp:GridView ID="dgvItems" runat="server" DataKeyNames="Id"
                                AutoGenerateColumns="false"
                                CssClass="table table-modern" GridLines="None" ShowHeader="false"
                                OnSelectedIndexChanged="dgvItems_SelectedIndexChanged"
                                OnRowCommand="dgvItems_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="row align-items-center py-2 border-bottom">

                                                <div class="col-4 text-start">
                                                    <%#Eval("Nombre") %>
                                                </div>

                                                <div class="col-4 text-center">
                                                    <%#Eval("OrganizadorCompetencia.NombrePublico") %>
                                                </div>

                                                <div class="col-4 text-end">
                                                    <span class="badge bg-secondary">
                                                        <%#SepararEspacios(Eval("Estado").ToString()) %>
                                                    </span>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
