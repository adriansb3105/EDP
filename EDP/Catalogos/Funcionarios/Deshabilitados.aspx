<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Deshabilitados.aspx.cs" Inherits="EDP.Catalogos.Funcionarios.Deshabilitados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true"/>
    
    <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 mt-1">
        <hr />
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row">

                    <%-- tabla--%>

                    <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
				        <table class="table table-bordered">
					        <thead style="text-align: center !important; align-content: center">
						        <tr style="text-align: center" class="btn-primary">
							        <th></th>
                                    <th class="center">Foto</th>
                                    <th class="center">Identificación</th>
							        <th class="center">Nombre</th>
                                    <th class="center">Apellidos</th>
                                    <th class="center">Extensión</th>
						        </tr>
					        </thead>
					        <tr>
						        <td>
						        </td>
                                <td>
						        </td>
                                <td>
							        <asp:TextBox ID="txtBuscarNumeroIdentificacion" runat="server" CssClass="form-control chat-input center" placeholder="Buscar identificación" AutoPostBack="true" OnTextChanged="btnFiltroBuscar_Click"></asp:TextBox>
						        </td>
						        <td>
							        <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control chat-input center" placeholder="Buscar nombre" AutoPostBack="true" OnTextChanged="btnFiltroBuscar_Click"></asp:TextBox>
						        </td>
                                <td>
							        <asp:TextBox ID="txtBuscarApellidos" runat="server" CssClass="form-control chat-input center" placeholder="Buscar apellidos" AutoPostBack="true" OnTextChanged="btnFiltroBuscar_Click"></asp:TextBox>
						        </td>
                                <td>
							        <asp:TextBox ID="txtBuscarExtension" runat="server" CssClass="form-control chat-input center" placeholder="Buscar extensión" AutoPostBack="true" OnTextChanged="btnFiltroBuscar_Click"></asp:TextBox>
						        </td>
					        </tr>
					        <asp:repeater id="rpFuncionario" runat="server" OnItemDataBound="rpFuncionario_ItemDataBound">
					            <HeaderTemplate>
					            </HeaderTemplate>
					            
                                <ItemTemplate>
						            <tr style="text-align: center">
							            <td>
                                            <asp:LinkButton ID="btnVer" runat="server" ToolTip="Ver" OnClick="btnVer_Click" CommandArgument='<%# Eval("numeroIdentificacion") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Eval("numeroIdentificacion") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>--%>
                                            <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Habilitar" OnClick="btnHabilitar_Click" CommandArgument='<%# Eval("numeroIdentificacion") %>'><span class="glyphicon glyphicon-unchecked"></span></asp:LinkButton>
                                        </td>
                                        <td>
                                            <img src="<%# Eval("rutaFotografia") %>" alt="<%# Eval("nombre") %>" class="img-thumbnail" style="width:100px; height:110px">
                                        </td>
                                        <td>
                                            <%# Eval("numeroIdentificacion") %>
                                        </td>
                                        <td>
                                            <%# Eval("nombre") %>
                                        </td>
                                        <td>
                                            <%# Eval("primerApellido") %> <%# Eval("segundoApellido") %>
                                        </td>
                                        <td>
                                            <%# Eval("extension").ToString()==""? "-" : Eval("extension") %>
                                        </td>
						            </tr>
					            </ItemTemplate>
					            
                                <FooterTemplate>
					            </FooterTemplate>
				            </asp:repeater>
				        </table>
			        </div>

                    <%--paginación--%>
			        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
				        <center>
                             <table class="table" style="max-width:664px;">
                                 <tr style="padding:1px !important">
                                     <td style="padding:1px !important">
                                         <asp:LinkButton ID="lbPrimero" runat="server" CssClass="btn btn-primary" OnClick="lbPrimero_Click"><span class="glyphicon glyphicon-fast-backward"></span></asp:LinkButton>
                                     </td>
                                     <td style="padding:1px !important">
                                         <asp:LinkButton ID="lbAnterior" runat="server" CssClass="btn btn-default" OnClick="lbAnterior_Click"><span class="glyphicon glyphicon-backward"></asp:LinkButton>
                                     </td>
                                      <td style="padding:1px !important">
                                          <asp:DataList ID="rptPaginacion" runat="server"
                                            OnItemCommand="rptPaginacion_ItemCommand"
                                            OnItemDataBound="rptPaginacion_ItemDataBound" RepeatDirection="Horizontal">
                                              <ItemTemplate>
                                                  <asp:LinkButton ID="lbPaginacion" runat="server" CssClass="btn btn-default"
                                                    CommandArgument='<%# Eval("IndexPagina") %>' CommandName="nuevaPagina"
                                                    Text='<%# Eval("PaginaText") %>' ></asp:LinkButton>
                                              </ItemTemplate>
                                          </asp:DataList>
                                      </td>
                                     <td style="padding:1px !important">
                                         <asp:LinkButton ID="lbSiguiente" CssClass="btn btn-default" runat="server" OnClick="lbSiguiente_Click"><span class="glyphicon glyphicon-forward"></asp:LinkButton>
                                     </td>
                                     <td style="padding:1px !important">
                                         <asp:LinkButton ID="lbUltimo" CssClass="btn btn-primary" runat="server" OnClick="lbUltimo_Click"><span class="glyphicon glyphicon-fast-forward"></asp:LinkButton>
                                     </td>
                                     <td style="padding:1px !important">
                                         <asp:Label ID="lblpagina" runat="server" Text=""></asp:Label>
                                     </td>
                                 </tr>
                             </table>
                         </center>
			        </div>

                </div>

                <!-- script tabla jquery -->
    <script type="text/javascript">
        $('#tblTarea thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tblTarea thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });
        // DataTable
        var table = $('#tblTarea').DataTable({
            orderCellsTop: true,
            "iDisplayLength": 10,
            "aLengthMenu": [[2, 5, 10, -1], [2, 5, 10, "All"]],
            "colReorder": true,
            "select": false,
            "stateSave": true,
            "dom": 'Bfrtip',
            "buttons": [
                'pdf', 'excel', 'copy', 'print'
            ],
            "language": {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "decimal": ",",
                "thousands": ".",
                "sSelect": "1 fila seleccionada",
                "select": {
                    rows: {
                        _: "Ha seleccionado %d filas",
                        0: "Dele click a una fila para seleccionarla",
                        1: "1 fila seleccionada"
                    }
                },
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                }
            }
        });
        // aplicar filtro
        $("#tblTarea thead input").on('keyup change', function () {
            table
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });
        function stopPropagation(evt) {
            if (evt.stopPropagation !== undefined) {
                evt.stopPropagation();
            } else {
                evt.cancelBubble = true;
            }
        };
    </script>
    <!-- fin script tabla jquery -->

            </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    

</asp:Content>
