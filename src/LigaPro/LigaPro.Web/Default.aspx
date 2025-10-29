<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LigaPro.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="px-4 py-5 my-5 text-center">
        <h1 class="display-5 fw-bold text-body-emphasis">Bienvenido a LigaPro</h1>
        <div class="col-lg-6 mx-auto">
            <p class="lead mb-4">La plataforma definitiva para organizar y seguir tus ligas y torneos amateurs. Crea tu competición, inscribe equipos y deja que nosotros nos encarguemos del resto.</p>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col">
                <h2 class="text-center mb-4">Deportes Disponibles en la Plataforma</h2>

                <asp:GridView ID="dgvDeportes" runat="server"
                    CssClass="table table-hover table-striped text-center shadow-sm"
                    AutoGenerateColumns="False"
                    GridLines="None">
                    <HeaderStyle CssClass="table-dark" />
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Deporte" />
                        <asp:BoundField DataField="CantJugadores" HeaderText="Jugadores por Equipo" />
                        <asp:BoundField DataField="PermiteEmpate" HeaderText="Permite Empate" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>
</asp:Content>
