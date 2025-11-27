<%@ Page Title="Gestión de Torneo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionarPartidos.aspx.cs" Inherits="LigaPro.Web.PaginasOrganizador.GestionarPartidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tournament-header {
            background-color: #fff;
            border-bottom: 1px solid #e9ecef;
            padding: 2rem 0;
            margin-bottom: 2rem;
        }
        
        .badge-soft {
            background-color: #f8f9fa;
            color: #495057;
            border: 1px solid #dee2e6;
            font-weight: 500;
        }

        .table-matches td {
            vertical-align: middle;
        }
        .score-box {
            display: inline-block;
            width: 35px;
            height: 35px;
            line-height: 35px;
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            border-radius: 4px;
            text-align: center;
            font-weight: 700;
        }
    </style>

    <script>
        function abrirModalResultado() {
            var myModal = new bootstrap.Modal(document.getElementById('modalCargarResultado'));
            myModal.show();
        }
        function abrirModalNuevoPartido() {
            var myModal = new bootstrap.Modal(document.getElementById('modalNuevoPartido'));
            myModal.show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- ENCABEZADO DEL TORNEO -->
    <div class="tournament-header">
        <div class="container">
            <div class="d-flex justify-content-between align-items-center">
                <div>
                    <div class="d-flex align-items-center gap-2 mb-1">
                        <h2 class="fw-bold mb-0 text-dark">
                            <asp:Label ID="lblNombreTorneo" runat="server" Text="Cargando..."></asp:Label>
                        </h2>
                        <asp:Label ID="lblEstadoTorneo" runat="server" CssClass="badge bg-success small">En Curso</asp:Label>
                    </div>
                    <div class="text-muted small">
                        <i class="bi bi-grid-3x3 me-1"></i> <asp:Label ID="lblFormato" runat="server" Text="-"></asp:Label>
                        <span class="mx-2">|</span>
                        <i class="bi bi-people me-1"></i> <asp:Label ID="lblInscriptos" runat="server" Text="0/0"></asp:Label> Equipos
                    </div>
                </div>
                
                <!-- Acciones Principales -->
                <div class="d-flex gap-2">
                    <asp:Button ID="btnGenerarFixture" runat="server" Text="Generar Fixture Automático" 
                        CssClass="btn btn-dark shadow-sm" OnClick="btnGenerarFixture_Click" 
                        OnClientClick="return confirm('¿Generar fixture automáticamente? Esto borrará partidos existentes si el torneo no ha comenzado.');" />
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        
        <!-- PESTAÑAS DE NAVEGACIÓN -->
        <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
            <li class="nav-item">
                <button class="nav-link active text-dark fw-bold" id="partidos-tab" data-bs-toggle="tab" data-bs-target="#partidos" type="button">
                    <i class="bi bi-calendar-event me-2"></i>Partidos
                </button>
            </li>
            <li class="nav-item" runat="server" id="liPosiciones" visible="false">
                <button class="nav-link text-dark fw-bold" id="posiciones-tab" data-bs-toggle="tab" data-bs-target="#posiciones" type="button">
                    <i class="bi bi-list-ol me-2"></i>Tabla de Posiciones
                </button>
            </li>
            <li class="nav-item">
                <button class="nav-link text-dark fw-bold" id="equipos-tab" data-bs-toggle="tab" data-bs-target="#equipos" type="button">
                    <i class="bi bi-shield me-2"></i>Equipos Inscriptos
                </button>
            </li>
        </ul>

        <!-- CONTENIDO DE PESTAÑAS -->
        <div class="tab-content" id="myTabContent">
            
            <!-- TAB 1: PARTIDOS -->
            <div class="tab-pane fade show active" id="partidos" role="tabpanel">
                
                <!-- Toolbar Partidos -->
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h5 class="fw-bold mb-0">Calendario de Encuentros</h5>
                    <button type="button" class="btn btn-outline-primary btn-sm" onclick="abrirModalNuevoPartido()">
                        <i class="bi bi-plus-lg me-1"></i> Nuevo Partido Manual
                    </button>
                </div>

                <!-- Lista de Partidos -->
                <div class="card border-0 shadow-sm">
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <table class="table table-hover table-matches mb-0 align-middle">
                                <thead class="bg-light text-muted small text-uppercase">
                                    <tr>
                                        <th class="ps-4">Fecha/Fase</th>
                                        <th class="text-end">Local</th>
                                        <th class="text-center" style="width: 100px;">Resultado</th>
                                        <th>Visitante</th>
                                        <th>Estado</th>
                                        <th class="text-end pe-4">Acción</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptPartidos" runat="server" OnItemCommand="rptPartidos_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="ps-4">
                                                    <span class="fw-bold d-block"><%# Eval("NombreFase") %></span> <!-- Ej: Fecha 1, Octavos -->
                                                    <small class="text-muted"><%# Eval("FechaProgramada", "{0:dd/MM HH:mm}") %></small>
                                                </td>
                                                
                                                <td class="text-end fw-bold">
                                                    <%# Eval("NombreLocal") %>
                                                </td>
                                                
                                                <td class="text-center">
                                                    <div class="d-flex justify-content-center align-items-center gap-1">
                                                        <span class="score-box"><%# Eval("GolesLocal") ?? "-" %></span>
                                                        <span class="text-muted">:</span>
                                                        <span class="score-box"><%# Eval("GolesVisita") ?? "-" %></span>
                                                    </div>
                                                </td>
                                                
                                                <td class="fw-bold">
                                                    <%# Eval("NombreVisita") %>
                                                </td>

                                                <td>
                                                    <%# Eval("Jugado") != null && (bool)Eval("Jugado") ? 
                                                        "<span class='badge bg-light text-dark border'>Finalizado</span>" : 
                                                        "<span class='badge bg-warning text-dark'>Pendiente</span>" %>
                                                </td>

                                                <td class="text-end pe-4">
                                                    <asp:LinkButton ID="btnCargar" runat="server" CssClass="btn btn-sm btn-primary" 
                                                        CommandName="CargarResultado" CommandArgument='<%# Eval("Id") %>'>
                                                        <i class="bi bi-pencil-square"></i> Cargar
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        
                        <asp:Panel ID="pnlSinPartidos" runat="server" Visible="false" CssClass="text-center py-5">
                            <p class="text-muted">Aún no se han generado partidos para este torneo.</p>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <!-- TAB 2: POSICIONES -->
            <div class="tab-pane fade" id="posiciones" role="tabpanel">
                <div class="card border-0 shadow-sm p-5 text-center">
                    <h5 class="text-muted">Tabla de Posiciones</h5>
                    <p class="small text-muted">Aquí se mostrará la tabla de puntos generada automáticamente.</p>
                </div>
            </div>

            <!-- TAB 3: EQUIPOS -->
            <div class="tab-pane fade" id="equipos" role="tabpanel">
                <div class="row row-cols-1 row-cols-md-4 g-4">
                    <asp:Repeater ID="rptEquipos" runat="server">
                        <ItemTemplate>
                            <div class="col">
                                <div class="card h-100 border-0 shadow-sm text-center p-3">
                                    <img src='<%# Eval("Imagen") != null ? Eval("Imagen") : "/Uploads/default-team.png" %>' 
                                         class="rounded-circle mx-auto mb-3" width="60" height="60" style="object-fit:cover;">
                                    <h6 class="fw-bold mb-0"><%# Eval("Nombre") %></h6>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

        </div>
    </div>

    <!-- MODALES -->

    <!-- MODAL CARGAR RESULTADO -->
    <div class="modal fade" id="modalCargarResultado" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Resultado del Partido</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body text-center">
                    <asp:HiddenField ID="hfIdPartidoResultado" runat="server" />
                    
                    <div class="d-flex justify-content-center align-items-center gap-3 mb-4">
                        <div class="text-end">
                            <h5 class="fw-bold mb-2"><asp:Label ID="lblLocalModal" runat="server" Text="Local"></asp:Label></h5>
                            <asp:TextBox ID="txtGolesLocal" runat="server" CssClass="form-control form-control-lg text-center mx-auto" 
                                TextMode="Number" Width="80px"></asp:TextBox>
                        </div>
                        <div class="h3 text-muted">-</div>
                        <div class="text-start">
                            <h5 class="fw-bold mb-2"><asp:Label ID="lblVisitaModal" runat="server" Text="Visita"></asp:Label></h5>
                            <asp:TextBox ID="txtGolesVisita" runat="server" CssClass="form-control form-control-lg text-center mx-auto" 
                                TextMode="Number" Width="80px"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="form-check d-inline-block">
                        <asp:CheckBox ID="chkFinalizado" runat="server" CssClass="form-check-input" Checked="true" />
                        <label class="form-check-label">Marcar como Finalizado</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardarResultado" runat="server" Text="Guardar Resultado" 
                        CssClass="btn btn-primary w-100" OnClick="btnGuardarResultado_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL NUEVO PARTIDO MANUAL -->
    <div class="modal fade" id="modalNuevoPartido" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Crear Cruce Manual</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label">Equipo Local</label>
                        <asp:DropDownList ID="ddlLocalManual" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Equipo Visitante</label>
                        <asp:DropDownList ID="ddlVisitaManual" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="row g-2 mb-3">
                        <div class="col-6">
                            <label class="form-label">Fecha</label>
                            <asp:TextBox ID="txtFechaManual" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-6">
                            <label class="form-label">Hora</label>
                            <asp:TextBox ID="txtHoraManual" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Fase / Jornada</label>
                        <asp:TextBox ID="txtFaseManual" runat="server" CssClass="form-control" placeholder="Ej: Fecha 1, Octavos..."></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCrearPartidoManual" runat="server" Text="Crear Partido" 
                        CssClass="btn btn-dark w-100" OnClick="btnCrearPartidoManual_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>