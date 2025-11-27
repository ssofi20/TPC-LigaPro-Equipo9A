<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionarPartidos.aspx.cs" Inherits="LigaPro.Web.PaginasPartidos.GestionarPartidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-12">
                <div class="card-body">
                    <!-- Panel Cargar Resultados -->
                    <asp:Panel ID="PanelCargarResultados" runat="server" Visible="true">
                        <h2 class="mb-4 text-center fw-bold">Cargar Resultados</h2>
                        <div class="container mt-4">
                            <div class="row justify-content-center">
                                <div class="col-md-10">
                                    <div class="card-body">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- PANEL Ver Equipos -->
                    <asp:Panel ID="PanelVerEquipos" runat="server" Visible="false">
                        <h2 class="mb-4 text-center fw-bold">Ver Equipos</h2>
                        <div class="container mt-4">
                            <div class="row justify-content-center">
                                <div class="col-md-10">
                                    <div class="card-body">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- PANEL Generar Fixture -->
                    <asp:Panel ID="PanelGenerarFixture" runat="server" Visible="false">
                        <h2 class="mb-4 text-center fw-bold">Generar Fixture</h2>
                        <div class="container mt-4">
                            <div class="row justify-content-center">
                                <div class="col-md-10">
                                    <div class="card-body">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
