<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerFuncionario.aspx.cs" Inherits="EDP.Catalogos.Funcionarios.VerFuncionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true" />

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>

            <!-- Sección del menú lateral -->
            <div class="sidebar">
                <asp:LinkButton ID="aInformacionPersonal" runat="server" Text="Información personal" CssClass="active" OnClick="showHideInformacionPersonal" />
                <asp:LinkButton ID="aCurriculo" runat="server" Text="Currículo Vitae" CssClass="" OnClick="showHideCurriculo" />
                <asp:LinkButton ID="aInformacionFinanciera" runat="server" Text="Información financiera" CssClass="" OnClick="showHideInformacionFinanciera" />
                <asp:LinkButton ID="aAcciones" runat="server" Text="Documentos de Trámite" CssClass="" OnClick="showHideAcciones" />
                <asp:LinkButton ID="aComprobantes" runat="server" Text="Comprobantes" CssClass="" OnClick="showHideComprobantes" />
                <asp:LinkButton ID="aAmonestacionesAntecedentes" runat="server" Text="Amonestaciones y antecedentes" CssClass="" OnClick="showHideAmonestacionesAntecedentes" />
            </div>

            <!-- Sección que contiene todos los formularios para ingresar la información del funcionario a registrar -->
            <div class="content-funcionario">

                <!-- Sección que contiene la información personal -->
                <div id="divInformacionPersonal" runat="server">

                    <!-- Título Información personal -->
                    <div>
                        <hr />
                        <center>
                            <asp:Label runat="server" Text="Información personal" Font-Size="Large" ForeColor="Black"></asp:Label>
                        </center>
                        <hr />
                    </div>

                    <!-- Fomulario para ingresar los datos básicos de la información personal del funcionario -->
                    <div>

                        <!-- Sección de datos personales obligatorios -->
                        <div class="row">
                            <!-- Fotografía -->
                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mt-1 text-center">
                                <asp:Image ID="imgFoto" Width="200px" Height="220px" CssClass="img-thumbnail" runat="server" />
                            </div>

                            <!-- Estado -->
                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                <label>Estado</label>
                                <asp:DropDownList ID="EstadoDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <!-- Número de identificación -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Número de identificación</label>
                                <asp:TextBox class="form-control" ID="txtIdentificacion" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>

                            <!-- Nombre -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Nombre</label>
                                <asp:TextBox class="form-control" ID="txtNombre" runat="server"></asp:TextBox>

                                <div id="divNombreIncorrecto" runat="server" style="display: none">
                                    <asp:Label ID="lblNombreIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                </div>
                                <div id="divNombreIncorrectoTamano" runat="server" style="display: none">
                                    <asp:Label ID="lblNombreIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <!-- Primer apellido -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Primer apellido</label>
                                <asp:TextBox class="form-control" ID="txtPrimerApellido" runat="server"></asp:TextBox>

                                <div id="divPrimerApellidoIncorrecto" runat="server" style="display: none">
                                    <asp:Label ID="lblPrimerApellidoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                </div>
                                <div id="divPrimerApellidoIncorrectoTamano" runat="server" style="display: none">
                                    <asp:Label ID="lblPrimerApellidoIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <!-- Segundo apellido -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Segundo apellido</label>
                                <asp:TextBox class="form-control" ID="txtSegundoApellido" runat="server"></asp:TextBox>

                                <div id="divSegundoApellidoIncorrecto" runat="server" style="display: none">
                                    <asp:Label ID="lblSegundoApellidoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                </div>
                                <div id="divSegundoApellidoIncorrectoTamano" runat="server" style="display: none">
                                    <asp:Label ID="lblSegundoApellidoIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <!-- Sección de datos personales no obligatorios -->
                        <div class="row" runat="server" id="divDatosPersonales">
                            <!-- Correo -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Correo</label>
                                <asp:TextBox class="form-control" ID="txtCorreo" runat="server"></asp:TextBox>

                                <div id="divNoEsCorreo" runat="server" style="display: none">
                                    <asp:Label ID="lblNoEsCorreo" runat="server" Font-Size="Small" class="label alert-danger" Text="El formato correcto es ejemplo@ucr.ac.cr" ForeColor="Red"></asp:Label>
                                </div>
                                <div id="divCorreoIncorrectoTamano" runat="server" style="display: none">
                                    <asp:Label ID="lblCorreoIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 200 caracteres" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <!-- Número de teléfono -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Número de teléfono</label>
                                <asp:TextBox class="form-control" ID="txtTelefono" runat="server"></asp:TextBox>
                            </div>

                            <!-- Lugar de residencia -->
                            <%--<div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                <label>Lugar de residencia</label>
                                <asp:TextBox class="form-control" ID="txtResidencia" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>--%>

                            <!-- Tipo de sangre -->
                            <%--<div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Tipo de sangre</label>
                                <asp:DropDownList ID="SangreDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>--%>

                            <!-- Tipo de licencia de conducir -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Tipo de licencia de conducir</label>
                                <asp:DropDownList ID="LicenciaDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <!-- Observaciones -->
                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                <label>Observaciones </label>
                                <asp:TextBox class="form-control" ID="txtObservaciones" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!-- Sección de datos laborales -->
                    <div runat="server" id="divDatosLaborales">
                        <!-- Título Datos Laborales -->
                        <div class="mt-2">
                            <hr />

                            <center>
                                <asp:Label runat="server" Text="Datos laborales" Font-Size="Large" ForeColor="Black"></asp:Label>
                            </center>

                            <hr />
                        </div>

                        <!-- Fomulario para ingresar los datos básicos de los datos laborales del funcionario -->
                        <div class="row">

                            <!-- Extensión -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Extensión</label>
                                <asp:TextBox class="form-control" ID="txtExtension" runat="server"></asp:TextBox>
                            </div>

                            <!-- Fecha de ingreso a laborar -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Fecha de ingreso a laborar</label>
                                <asp:TextBox class="form-control" ID="txtFechaIngreso" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                            </div>

                            <!-- Puesto -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Puesto</label>
                                <asp:TextBox class="form-control" ID="txtPuesto" runat="server"></asp:TextBox>
                            </div>

                            <!-- Categoría laboral -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Categoría laboral</label>
                                <asp:DropDownList ID="CategoriaLaboralDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <!-- Fundevi o UCR -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Tipo de contratación</label>
                                <asp:DropDownList ID="SecciónDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <!-- Unidad, programa o laboratorio -->
                            <div class="form-group col-xs-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <label>Unidad, programa o laboratorio</label>
                                <asp:DropDownList ID="UnidadProgramaLaboratorioDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>

                <!-- Sección que contiene la información del curriculo -->
                <div id="divCurriculo" runat="server">
                    <!-- Título Curriculo -->
                    <div>
                        <hr />
                        <center>
                            <asp:Label runat="server" Text="Currículo Vitae" Font-Size="Large" ForeColor="Black"></asp:Label>
                        </center>

                        <hr />
                    </div>

                    <!-- tabs -->
                    <ul id="nav-bar" class="nav nav-tabs mt-2">
                        <li id="liFormacionAcademica" runat="server" role="presentation" class="active"><a onclick="verFormacionAcademica()">Formación académica</a></li>
                        <li id="liFormacionProfesional" runat="server" role="presentation"><a onclick="verFormacionProfesional()">Currículo vitae</a></li>
                        <li id="liFormacionPersonal" runat="server" role="presentation"><a onclick="verFormacionPersonal()">Personal</a></li>
                    </ul>
                    <!-- fin tabs -->

                    <!-- Vista de Formación Académica -->
                    <div id="viewFormacionAcademica" runat="server" style="display: block">
                        <!-- Estudios formales -->
                        <div class="mt-1 row">
                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                <label>Estudios formales</label>

                                <%-- Tabla para mostrar los estudios formales agregados --%>
                                <div class="form-group">
                                    <asp:Repeater ID="rpEstudios" runat="server" OnItemDataBound="rpEstudios_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="table table-striped row-border table-responsive">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>Nombre</th>
                                                        <th>Nombre del documento</th>
                                                        <th>Fecha de inicio</th>
                                                        <th>Fecha de finalización</th>
                                                        <th>Entregado</th>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnVerEstudio" runat="server" ToolTip="Ver" OnClick="btnVerEstudio_Click" CommandArgument='<%# Eval("IdEstudio") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEditarEstudio" runat="server" ToolTip="Editar" OnClick="btnEditarEstudio_Click" CommandArgument='<%# Eval("IdEstudio") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarEstudio" runat="server" ToolTip="Eliminar" OnClick="btnEliminarEstudio_Click" CommandArgument='<%# Eval("IdEstudio") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("Nombre") %>
                                                </td>
                                                <td>
                                                    <%# Eval("NombreDocumento") == null || Eval("NombreDocumento").ToString() == ""? "-" : Eval("NombreDocumento") %>
                                                </td>
                                                <td>
                                                    <%# Eval("FechaInicio", "{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td>
                                                    <%# Eval("FechaFinalizacion").ToString() == "01/01/1900"? "-" : Eval("FechaFinalizacion", "{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ckbEntregadoEstudioTabla" Enabled="false" runat="server" Checked='<%# Eval("Entregado") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            </table>
                                       
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <%-- fin tabla--%>

                                <div>
                                    <asp:Button ID="btnLevantarAgregarEstudio" runat="server" Text="Agregar estudio" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarEstudio_Click" />
                                </div>
                            </div>
                        </div>

                        <hr />

                        <!-- Cursos -->
                        <div class="row">
                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                <label>Cursos</label>

                                <%-- Tabla para mostrar los cursos agregados --%>
                                <div class="form-group">
                                    <asp:Repeater ID="rpCursos" runat="server" OnItemDataBound="rpCursos_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="table table-striped row-border table-responsive">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>Nombre</th>
                                                        <th>Nombre del documento</th>
                                                        <th>Fecha de inicio</th>
                                                        <th>Fecha de finalización</th>
                                                        <th>Entregado</th>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnVerCurso" runat="server" ToolTip="Ver" OnClick="btnVerCurso_Click" CommandArgument='<%# Eval("IdEstudio") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEditarCurso" runat="server" ToolTip="Editar" OnClick="btnEditarCurso_Click" CommandArgument='<%# Eval("IdEstudio") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarCurso" runat="server" ToolTip="Eliminar" OnClick="btnEliminarCurso_Click" CommandArgument='<%# Eval("IdEstudio") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("Nombre") %>
                                                </td>
                                                <td>
                                                    <%# Eval("NombreDocumento") != null? Eval("NombreDocumento") : "-" %>
                                                </td>
                                                <td>
                                                    <%# Eval("FechaInicio", "{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td>
                                                    <%# Eval("FechaFinalizacion").ToString() == "01/01/1900"? "-" : Eval("FechaFinalizacion", "{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ckbEntregadoCursoTabla" Enabled="false" runat="server" Checked='<%# Eval("Entregado") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            </table>
                                       
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <%-- fin tabla--%>

                                <div>
                                    <asp:Button ID="btnLevantarAgregarCurso" runat="server" Text="Agregar curso" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarCurso_Click" />
                                </div>
                            </div>
                        </div>

                        <hr />

                        <!-- Certificados -->
                        <div class="row">
                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                <label>Certificados</label>

                                <%-- Tabla para mostrar los certificados agregados --%>
                                <div class="form-group">
                                    <asp:Repeater ID="rpCertificados" runat="server" OnItemDataBound="rpCertificados_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="table table-striped row-border table-responsive">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>Nombre</th>
                                                        <th>Nombre del documento</th>
                                                        <th>Fecha de inicio</th>
                                                        <th>Fecha de finalización</th>
                                                        <th>Entregado</th>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnVerCertificado" runat="server" ToolTip="Ver" OnClick="btnVerCertificado_Click" CommandArgument='<%# Eval("IdEstudio") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEditarCertificado" runat="server" ToolTip="Editar" OnClick="btnEditarCertificado_Click" CommandArgument='<%# Eval("IdEstudio") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarCertificado" runat="server" ToolTip="Eliminar" OnClick="btnEliminarCertificado_Click" CommandArgument='<%# Eval("IdEstudio") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("Nombre") %>
                                                </td>
                                                <td>
                                                    <%# Eval("NombreDocumento") != null? Eval("NombreDocumento") : "-" %>
                                                </td>
                                                <td>
                                                    <%# Eval("FechaInicio", "{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td>
                                                    <%# Eval("FechaFinalizacion").ToString() == "01/01/1900"? "-" : Eval("FechaFinalizacion", "{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ckbEntregadoCertificadoTabla" Enabled="false" runat="server" Checked='<%# Eval("Entregado") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            </table>
                                       
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <%-- fin tabla--%>

                                <div>
                                    <asp:Button ID="btnLevantarAgregarCertificado" runat="server" Text="Agregar certificado" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarCertificado_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Vista de Formación Profesional -->
                    <div id="viewFormacionProfesional" runat="server" style="display: none">
                        <!-- Experiencia laboral -->
                        <div class="row mt-1">
                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                <label>Currículo vitae</label>

                                <%-- Tabla para mostrar la experiencia laboral agregada --%>
                                <div class="form-group">
                                    <asp:Repeater ID="rpExperienciaLaboral" runat="server" OnItemDataBound="rpExperienciaLaboral_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="table table-striped row-border table-responsive">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>Nombre</th>
                                                        <th>Descripción</th>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnVerExperienciaLaboral" runat="server" ToolTip="Ver" OnClick="btnVerExperienciaLaboral_Click" CommandArgument='<%# Eval("idCurriculum") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEditarExperienciaLaboral" runat="server" ToolTip="Editar" OnClick="btnEditarExperienciaLaboral_Click" CommandArgument='<%# Eval("idCurriculum") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarExperienciaLaboral" runat="server" ToolTip="Eliminar" OnClick="btnEliminarExperienciaLaboral_Click" CommandArgument='<%# Eval("idCurriculum") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("nombre") %>
                                                </td>
                                                <td>
                                                    <%# Eval("descripcion") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            </table>
                                       
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <%-- fin tabla--%>

                                <div>
                                    <asp:Button ID="btnLevantarAgregarExperienciaLaboral" runat="server" Text="Agregar currículo vitae" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarExperienciaLaboral_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Vista de Personal -->
                    <div id="viewFormacionPersonal" runat="server" style="display: none">
                        <!-- Habilidades blandas -->
                        <div class="row mt-1">
                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                <label>Habilidades</label>

                                <%-- Tabla para mostrar las habilidades blandas agregadas --%>
                                <div class="form-group">
                                    <asp:Repeater ID="rpHabilidadBlanda" runat="server" OnItemDataBound="rpHabilidadBlanda_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="table table-striped row-border table-responsive">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>Descripción</th>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnVerHabilidadBlanda" runat="server" ToolTip="Ver" OnClick="btnVerHabilidadBlanda_Click" CommandArgument='<%# Eval("IdHabilidadBlanda") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEditarHabilidadBlanda" runat="server" ToolTip="Editar" OnClick="btnEditarHabilidadBlanda_Click" CommandArgument='<%# Eval("IdHabilidadBlanda") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarHabilidadBlanda" runat="server" ToolTip="Eliminar" OnClick="btnEliminarHabilidadBlanda_Click" CommandArgument='<%# Eval("IdHabilidadBlanda") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("Descripcion") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            </table>
               
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <%-- fin tabla--%>
                                <div>
                                    <asp:Button ID="btnLevantarAgregarHabilidadBlanda" runat="server" Text="Agregar habilidad" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarHabilidadBlanda_Click" />
                                </div>
                            </div>
                        </div>

                        <hr />

                        <!-- Intereses personales -->
                        <div class="row mt-1">
                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                <label>Intereses personales</label>

                                <%-- Tabla para mostrar las intereses personales agregados --%>
                                <div class="form-group">
                                    <asp:Repeater ID="rpInteresesPersonales" runat="server" OnItemDataBound="rpInteresesPersonales_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="table table-striped row-border table-responsive">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>Descripción</th>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnVerInteresesPersonales" runat="server" ToolTip="Ver" OnClick="btnVerInteresPersonal_Click" CommandArgument='<%# Eval("IdInteresPersonal") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEditarInteresesPersonales" runat="server" ToolTip="Editar" OnClick="btnEditarInteresPersonal_Click" CommandArgument='<%# Eval("IdInteresPersonal") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarInteresesPersonales" runat="server" ToolTip="Eliminar" OnClick="btnEliminarInteresPersonal_Click" CommandArgument='<%# Eval("IdInteresPersonal") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("Descripcion") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            </table>
               
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <%-- fin tabla--%>
                                <div>
                                    <asp:Button ID="btnLevantarAgregarInteresPersonal" runat="server" Text="Agregar interés personal" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarInteresPersonal_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <!-- Sección que contiene la información financiera -->
                <div id="divInformacionFinanciera" runat="server">
                    <!-- Título Información financiera -->
                    <div>
                        <hr />

                        <center>
                            <asp:Label runat="server" Text="Información financiera" Font-Size="Large" ForeColor="Black"></asp:Label>
                        </center>

                        <hr />
                    </div>

                    <!-- Formulario para ingresar la información de las pensiones o embargos -->
                    <div class="row">
                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                            <label>Pensiones o embargos</label>

                            <%-- Tabla para mostrar las pensiones o embargos agregados --%>
                            <div class="form-group">
                                <asp:Repeater ID="rpPensionOEmbargos" runat="server" OnItemDataBound="rpPensionOEmbargos_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped row-border table-responsive">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Nombre del documento</th>
                                                    <th>Fecha de ingreso</th>
                                                    <th>Descripción</th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnVerPensionOEmbargo" runat="server" ToolTip="Ver" OnClick="btnVerPensionesOEmbargos_Click" CommandArgument='<%# Eval("IdPensionOEmbargo") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEditarPensionOEmbargo" runat="server" ToolTip="Editar" OnClick="btnEditarPensionesOEmbargos_Click" CommandArgument='<%# Eval("IdPensionOEmbargo") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminarPensionOEmbargo" runat="server" ToolTip="Eliminar" OnClick="btnEliminarPensionesOEmbargos_Click" CommandArgument='<%# Eval("IdPensionOEmbargo") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("NombreDocumento") != null? Eval("NombreDocumento") : "-" %>
                                            </td>
                                            <td>
                                                <%# Eval("FechaIngreso") %>
                                            </td>
                                            <td>
                                                <%# Eval("Descripcion") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </table>
                                       
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <%-- fin tabla--%>

                            <div>
                                <asp:Button ID="btnLevantarAgregarPensionOEmbargo" runat="server" Text="Agregar pensión o embargo" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarPensionOEmbargo_Click" />
                            </div>
                        </div>
                    </div>

                </div>

                <!-- Sección que contiene la información sobre las acciones -->
                <div id="divAcciones" runat="server">
                    <!-- Título Acciones de Personal -->
                    <div>
                        <hr />
                        <center>
                            <asp:Label runat="server" Text="Documentos de trámite" Font-Size="Large" ForeColor="Black"></asp:Label>
                        </center>
                        <hr />
                    </div>

                    <!-- Acciones de personal -->
                    <div class="mt-1 row">
                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                            <%-- Tabla para mostrar las acciones de personal agregadas --%>
                            <div class="form-group">
                                <asp:Repeater ID="rpAccionesPersonal" runat="server" OnItemDataBound="rpAccionesPersonal_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped row-border table-responsive">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                        <th>Número</th>
                                                        <th>Descripción</th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnVerAccionPersonal" runat="server" ToolTip="Ver" OnClick="btnVerAccionPersonal_Click" CommandArgument='<%# Eval("idDocumentoTramite") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEditarAccionPersonal" runat="server" ToolTip="Editar" OnClick="btnEditarAccionPersonal_Click" CommandArgument='<%# Eval("idDocumentoTramite") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminarAccionPersonal" runat="server" ToolTip="Eliminar" OnClick="btnEliminarAccionPersonal_Click" CommandArgument='<%# Eval("idDocumentoTramite") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("numero") %>
                                            </td>
                                            <td>
                                                <%# Eval("descripcion") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </table>
                                       
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <%-- fin tabla--%>

                            <div>
                                <asp:Button ID="btnLevantarAgregarAccionesPersonal" runat="server" Text="Agregar documento de trámite" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarAccionesPersonal_Click" />
                            </div>
                        </div>
                    </div>

                </div>

                <!-- Sección que contiene la información sobre los comprobantes -->
                <div id="divComprobantes" runat="server">
                    <!-- Título Comprobantes -->
                    <div>
                        <hr />
                        <center>
                            <asp:Label runat="server" Text="Comprobantes" Font-Size="Large" ForeColor="Black"></asp:Label>
                        </center>
                        <hr />
                    </div>

                    <!-- Comprobantes -->
                    <div class="mt-1 row">
                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                            <%-- Tabla para mostrar los comprobantes --%>
                            <div class="form-group">
                                <asp:Repeater ID="rpComprobantes" runat="server" OnItemDataBound="rpComprobantes_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped row-border table-responsive">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Fecha</th>
                                                    <th>Descripción</th>
                                                    <th>Nombre del documento</th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnVerComprobante" runat="server" ToolTip="Ver" OnClick="btnVerComprobante_Click" CommandArgument='<%# Eval("IdComprobante") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEditarComprobante" runat="server" ToolTip="Editar" OnClick="btnEditarComprobante_Click" CommandArgument='<%# Eval("IdComprobante") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminarComprobante" runat="server" ToolTip="Eliminar" OnClick="btnEliminarComprobante_Click" CommandArgument='<%# Eval("IdComprobante") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
                                            </td>
                                            <td>
                                                <%# Eval("Descripcion") %>
                                            </td>
                                            <td>
                                                <%# Eval("NombreDocumento") == null || Eval("NombreDocumento").ToString() == ""? "-" : Eval("NombreDocumento") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </table>
                   
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <%-- fin tabla--%>

                            <div>
                                <asp:Button ID="btnLevantarAgregarComprobantes" runat="server" Text="Agregar comprobante" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarComprobantes_Click" />
                            </div>
                        </div>
                    </div>

                </div>

                <!-- Sección que contiene la información sobre las amonestaciones y/o antecedentes -->
                <div id="divAmonestacionesAntecedentes" runat="server">
                    <!-- Título Amonestaciones y/o Antecedentes -->
                    <div>
                        <hr />
                        <center>
                            <asp:Label runat="server" Text="Amonestaciones y antecedentes" Font-Size="Large" ForeColor="Black"></asp:Label>
                        </center>
                        <hr />
                    </div>

                    <!-- Formulario para ingresar la información de las suspensiones o permisos -->
                    <div class="row mt-1">
                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                            <label>Suspensiones o permisos</label>

                            <%-- Tabla para mostrar las suspensiones o permisos agregadas --%>
                            <div class="form-group">
                                <asp:Repeater ID="rpSuspensionPermiso" runat="server" OnItemDataBound="rpSuspensionPermiso_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped row-border table-responsive">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Fecha de salida</th>
                                                    <th>Fecha de regreso</th>
                                                    <th>Tipo</th>
                                                    <th>Descripción</th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnVerSuspensionPermiso" runat="server" ToolTip="Ver" OnClick="btnVerSuspensionOPermiso_Click" CommandArgument='<%# Eval("IdSuspensionPermiso") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEditarSuspensionPermiso" runat="server" ToolTip="Editar" OnClick="btnEditarSuspensionOPermiso_Click" CommandArgument='<%# Eval("IdSuspensionPermiso") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminarSuspensionPermiso" runat="server" ToolTip="Eliminar" OnClick="btnEliminarSuspensionPermiso_Click" CommandArgument='<%# Eval("IdSuspensionPermiso") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("FechaSalida", "{0:dd/MM/yyyy}") %>
                                            </td>
                                            <td>
                                                <%# Eval("FechaRegreso").ToString() == "01/01/1900"? "-" : Eval("FechaRegreso", "{0:dd/MM/yyyy}") %>
                                            </td>
                                            <td>
                                                <%# Eval("Tipo").ToString() == "1"? "Permiso" : "Suspensión" %>
                                            </td>
                                            <td>
                                                <%# Eval("Descripcion") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </table>
                                       
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <%-- fin tabla--%>

                            <div>
                                <asp:Button ID="btnLevantarAgregarSuspensionPermiso" runat="server" Text="Agregar suspensión o permiso laboral" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarSuspensionPermiso_Click" />
                            </div>
                        </div>
                    </div>

                    <hr />

                    <!-- Formulario para ingresar la información de los antecedentes -->
                    <div class="row">
                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                            <label>Antecedentes</label>

                            <%-- Tabla para mostrar los antecedentes agregados --%>
                            <div class="form-group">
                                <asp:Repeater ID="rpAntecedentes" runat="server" OnItemDataBound="rpAntecedentes_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped row-border table-responsive">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Nombre</th>
                                                    <th>Fecha</th>
                                                    <th>Descripción</th>
                                                    <th>Nombre del documento</th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnVerAntecedente" runat="server" ToolTip="Ver" OnClick="btnVerAntecedente_Click" CommandArgument='<%# Eval("IdAntecedente") %>'><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEditarAntecedente" runat="server" ToolTip="Editar" OnClick="btnEditarAntecedente_Click" CommandArgument='<%# Eval("IdAntecedente") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminarAntecedente" runat="server" ToolTip="Eliminar" OnClick="btnEliminarAntecedente_Click" CommandArgument='<%# Eval("IdAntecedente") %>'><span class="glyphicon glyphicon-remove-circle"></span></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("Nombre") %>
                                            </td>
                                            <td>
                                                <%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
                                            </td>
                                            <td>
                                                <%# Eval("Descripcion") %>
                                            </td>
                                            <td>
                                                <%# Eval("NombreDocumento") == null || Eval("NombreDocumento").ToString() == ""? "-" : Eval("NombreDocumento") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </table>
               
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <%-- fin tabla--%>

                            <div>
                                <asp:Button ID="btnLevantarAgregarAntecedentes" runat="server" Text="Agregar antecedente" CssClass="btn-sm btn btn-default" OnClick="btnLevantarAgregarAntecedentes_Click" />
                            </div>
                        </div>
                    </div>

                </div>

                <!-- Botones -->
                <div class="row">
                    <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 text-right">
                        <asp:Button ID="btnGuardarInformacionPersonal" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarInformacion_Click" />
                        <asp:Button ID="btnCancelarInformacion" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                    </div>
                </div>


            </div>

            <!-- Llamado a las funciones JS -->
            <script type="text/javascript">
                function levantarModalCancelar() {
                    $('#modalConfirmacionCancelar').modal('show');
                };

                function cerrarModalAgregarEstudio() {
                    $('#modalAgregarEstudio').modal('hide');
                };

                function levantarModalAgregarEstudio() {
                    $('#modalAgregarEstudio').modal('show');
                }

                function cerrarModalAgregarCurso() {
                    $('#modalAgregarCurso').modal('hide');
                };

                function levantarModalAgregarCurso() {
                    $('#modalAgregarCurso').modal('show');
                }

                function cerrarModalAgregarCertificado() {
                    $('#modalAgregarCertificado').modal('hide');
                };

                function levantarModalAgregarCertificado() {
                    $('#modalAgregarCertificado').modal('show');
                }

                function cerrarModalAgregarExperienciaLaboral() {
                    $('#modalAgregarExperienciaLaboral').modal('hide');
                    verFormacionProfesional();
                };

                function levantarModalAgregarExperienciaLaboral() {
                    $('#modalAgregarExperienciaLaboral').modal('show');
                    verFormacionProfesional();
                }

                function cerrarModalAgregarHabilidadBlanda() {
                    $('#modalAgregarHabilidadBlanda').modal('hide');
                    verFormacionPersonal();
                };

                function levantarModalAgregarHabilidadBlanda() {
                    $('#modalAgregarHabilidadBlanda').modal('show');
                    verFormacionPersonal();
                }

                function cerrarModalAgregarInteresPersonal() {
                    $('#modalAgregarInteresPersonal').modal('hide');
                    verFormacionPersonal();
                };

                function levantarModalAgregarInteresPersonal() {
                    $('#modalAgregarInteresPersonal').modal('show');
                    verFormacionPersonal();
                }

                function cerrarModalAgregarPensionOEmbargo() {
                    $('#modalAgregarPensionOEmbargo').modal('hide');
                };

                function levantarModalAgregarPensionOEmbargo() {
                    $('#modalAgregarPensionOEmbargo').modal('show');
                }

                function cerrarModalAgregarAccionesPersonal() {
                    $('#modalAgregarAccionesPersonal').modal('hide');
                };

                function levantarModalAgregarAccionesPersonal() {
                    $('#modalAgregarAccionesPersonal').modal('show');
                }

                function cerrarModalAgregarComprobantes() {
                    $('#modalAgregarComprobantes').modal('hide');
                };

                function levantarModalAgregarComprobantes() {
                    $('#modalAgregarComprobantes').modal('show');
                }

                function cerrarModalAgregarAntecedentes() {
                    $('#modalAgregarAntecedentes').modal('hide');
                };

                function levantarModalAgregarAntecedentes() {
                    $('#modalAgregarAntecedentes').modal('show');
                }

                function cerrarModalAgregarSuspensionPermiso() {
                    $('#modalAgregarSuspensionPermiso').modal('hide');
                };

                function levantarModalAgregarSuspensionPermiso() {
                    $('#modalAgregarSuspensionPermiso').modal('show');
                }

                function cerrarModalVerEstudio() {
                    $('#modalVerEstudio').modal('hide');
                };

                function levantarModalVerEstudio() {
                    $('#modalVerEstudio').modal('show');
                }

                function cerrarModalVerCurso() {
                    $('#modalVerCurso').modal('hide');
                };

                function levantarModalVerCurso() {
                    $('#modalVerCurso').modal('show');
                }

                function cerrarModalVerCertificado() {
                    $('#modalVerCertificado').modal('hide');
                };

                function levantarModalVerCertificado() {
                    $('#modalVerCertificado').modal('show');
                }

                function cerrarModalVerEditarExperienciaLaboral() {
                    $('#modalVerEditarExperienciaLaboral').modal('hide');
                    verFormacionProfesional();
                };

                function levantarModalVerEditarExperienciaLaboral() {
                    $('#modalVerEditarExperienciaLaboral').modal('show');
                    verFormacionProfesional();
                }

                function cerrarModalVerExperienciaLaboral() {
                    $('#modalVerExperienciaLaboral').modal('hide');
                    verFormacionProfesional();
                };

                function levantarModalVerExperienciaLaboral() {
                    $('#modalVerExperienciaLaboral').modal('show');
                    verFormacionProfesional();
                }

                function cerrarModalVerHabilidadBlanda() {
                    $('#modalVerHabilidadBlanda').modal('hide');
                    verFormacionPersonal();
                };

                function levantarModalVerHabilidadBlanda() {
                    $('#modalVerHabilidadBlanda').modal('show');
                    verFormacionPersonal();
                }

                function cerrarModalVerInteresPersonal() {
                    $('#modalVerInteresPersonal').modal('hide');
                    verFormacionPersonal();
                };

                function levantarModalVerInteresPersonal() {
                    $('#modalVerInteresPersonal').modal('show');
                    verFormacionPersonal();
                }

                //function cerrarModalAgregarSalario() {
                //    $('#modalVerSalario').modal('hide');
                //};

                //function levantarModalAgregarSalario() {
                //    $('#modalVerSalario').modal('show');
                //}

                function cerrarModalVerPensionOEmbargo() {
                    $('#modalVerPensionOEmbargo').modal('hide');
                };

                function levantarModalVerPensionOEmbargo() {
                    $('#modalVerPensionOEmbargo').modal('show');
                }

                function cerrarModalVerAccionesPersonal() {
                    $('#modalVerAccionesPersonal').modal('hide');
                };

                function levantarModalVerAccionesPersonal() {
                    $('#modalVerAccionesPersonal').modal('show');
                }

                function cerrarModalVerComprobantes() {
                    $('#modalVerComprobantes').modal('hide');
                };

                function levantarModalVerComprobantes() {
                    $('#modalVerComprobantes').modal('show');
                }

                function cerrarModalVerAntecedentes() {
                    $('#modalVerAntecedentes').modal('hide');
                };

                function levantarModalVerAntecedentes() {
                    $('#modalVerAntecedentes').modal('show');
                }

                function cerrarModalVerSuspensionPermiso() {
                    $('#modalVerSuspensionPermiso').modal('hide');
                };

                function levantarModalVerSuspensionPermiso() {
                    $('#modalVerSuspensionPermiso').modal('show');
                }

                function levantarModalVerEditarAccionesPersonal() {
                    $('#modalVerEditarAccionesPersonal').modal('show');
                }

                function cerrarModalVerEditarComprobantes() {
                    $('#modalVerEditarComprobantes').modal('hide');
                };

                function cerrarModalVerEditarAccionesPersonal() {
                    $('#modalVerEditarAccionesPersonal').modal('hide');
                };

                function levantarModalVerEditarAccionesPersonal() {
                    $('#modalVerEditarAccionesPersonal').modal('show');
                }

                function verFormacionAcademica() {
                    document.getElementById('<%=liFormacionAcademica.ClientID%>').className = "active";
                    document.getElementById('<%=liFormacionProfesional.ClientID%>').className = "";
                    document.getElementById('<%=liFormacionPersonal.ClientID%>').className = "";

                    document.getElementById('<%=viewFormacionAcademica.ClientID%>').style.display = 'block';
                    document.getElementById('<%=viewFormacionProfesional.ClientID%>').style.display = 'none';
                    document.getElementById('<%=viewFormacionPersonal.ClientID%>').style.display = 'none';
                };

                function verFormacionProfesional() {
                    document.getElementById('<%=liFormacionAcademica.ClientID%>').className = "";
                    document.getElementById('<%=liFormacionProfesional.ClientID%>').className = "active";
                    document.getElementById('<%=liFormacionPersonal.ClientID%>').className = "";

                    document.getElementById('<%=viewFormacionAcademica.ClientID%>').style.display = 'none';
                    document.getElementById('<%=viewFormacionProfesional.ClientID%>').style.display = 'block';
                    document.getElementById('<%=viewFormacionPersonal.ClientID%>').style.display = 'none';
                };

                function verFormacionPersonal() {
                    document.getElementById('<%=liFormacionAcademica.ClientID%>').className = "";
                    document.getElementById('<%=liFormacionProfesional.ClientID%>').className = "";
                    document.getElementById('<%=liFormacionPersonal.ClientID%>').className = "active";

                    document.getElementById('<%=viewFormacionAcademica.ClientID%>').style.display = 'none';
                    document.getElementById('<%=viewFormacionProfesional.ClientID%>').style.display = 'none';
                    document.getElementById('<%=viewFormacionPersonal.ClientID%>').style.display = 'block';
                };
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal que se ejecuta cuando se quiere cancelar la vista de la información del funcionario -->
    <div id="modalConfirmacionCancelar" class="modal fade" role="alertdialog">
        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Confirma que desea salir?</h4>
                </div>

                <div class="modal-body">
                    Se perderán todos los cambios que no hayan sido guardados.
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnConfirmarCancelar" runat="server" Text="Confirmar" CssClass="btn btn-danger" OnClick="btnConfirmarCancelar_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para ver un estudio formal -->
    <div id="modalVerEstudio" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información del estudio</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Tipo de estudio</label>
                                    <asp:DropDownList ID="TipoEstudioDDL" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TipoEstudioDDL_TextChanged"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreEstudio" runat="server" OnTextChanged="txtNombreEstudio_TextChanged"></asp:TextBox>

                                    <div id="divNombreEstudioIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreEstudioIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreEstudioIncorrectoTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreEstudioIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="CertificadoEstudioUpdatePanel">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Título</label>
                                            <br />
                                            <asp:LinkButton ID="btnVerCertificadoEstudio" runat="server" CssClass="form-control" OnClick="btnVerCertificadoEstudio_Click"></asp:LinkButton>
                                            <br />
                                            <asp:FileUpload ID="fuCertificadoEstudio" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoEstudio" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoEstudio_Click" />
                                            <asp:Button ID="btnQuitarArchivoEstudio" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoEstudio_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoEstudioVacio" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoEstudio" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoEstudio" />
                                        <asp:PostBackTrigger ControlID="btnVerCertificadoEstudio" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de inicio <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaInicioEstudio" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaInicioEstudio_OnChanged"></asp:TextBox>

                                    <div id="divFechaInicioEstudioIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaInicioEstudioIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de finalización</label>
                                    <asp:TextBox class="form-control" ID="txtFechaFinalizacionEstudio" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>

                                    <div id="divFechaFinalizacionEstudioIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaFinalizacionEstudioIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="La fecha de finalización debe ser mayor a la fecha de inicio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Observaciones </label>
                                    <asp:TextBox class="form-control" ID="txtObservacionesEstudio" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Certificación entregada</label>
                                    <!-- Marcarlo cuando se sube un archivo y desmarcarlo cuando se elimina -->
                                    <asp:CheckBox ID="ckbEntregadoEstudio" Enabled="false" runat="server" />
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnEditarEstudio" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosEstudio_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para ver un curso informal -->
    <div id="modalVerCurso" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información del curso</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreCurso" runat="server" OnTextChanged="txtNombreCurso_TextChanged"></asp:TextBox>

                                    <div id="divNombreCursoIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreCursoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreCursoIncorrectoTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreCursoIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Certificado</label>
                                            <br />
                                            <asp:LinkButton ID="btnVerCertificadoCurso" runat="server" CssClass="form-control" OnClick="btnVerCertificadoCurso_Click"></asp:LinkButton>
                                            <br />
                                            <asp:FileUpload ID="fuCertificadoCurso" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoCurso" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoCurso_Click" />
                                            <asp:Button ID="btnQuitarArchivoCurso" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoCurso_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoCursoVacio" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoCurso" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoCurso" />
                                        <asp:PostBackTrigger ControlID="btnVerCertificadoCurso" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de inicio <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaInicioCurso" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaInicioCurso_OnChanged"></asp:TextBox>

                                    <div id="divFechaInicioCursoIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaInicioCursoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de finalización</label>
                                    <asp:TextBox class="form-control" ID="txtFechaFinalizacionCurso" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>

                                    <div id="divFechaFinalizacionCursoIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaFinalizacionCursoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="La fecha de finalización debe ser mayor a la fecha de inicio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Observaciones </label>
                                    <asp:TextBox class="form-control" ID="txtObservacionesCurso" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Certificación entregada</label>
                                    <!-- Marcarlo cuando se sube un archivo y desmarcarlo cuando se elimina -->
                                    <asp:CheckBox ID="ckbEntregadoCurso" Enabled="false" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnEditarCurso" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosCurso_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para ver un certificado -->
    <div id="modalVerCertificado" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información del certificado</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreCertificado" runat="server" OnTextChanged="txtNombreCertificado_TextChanged"></asp:TextBox>

                                    <div id="divNombreCertificadoIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lbNombreCertificadoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreCertificadoIncorrectoTamano" runat="server" style="display: none">
                                        <asp:Label ID="lbNombreCertificadoIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Certificado</label>
                                            <br />
                                            <asp:LinkButton ID="btnVerCertificadoCertificado" runat="server" CssClass="form-control" OnClick="btnVerCertificadoCertificado_Click"></asp:LinkButton>
                                            <br />
                                            <asp:FileUpload ID="fuCertificadoCertificado" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoCertificado" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoCertificado_Click" />
                                            <asp:Button ID="btnQuitarArchivoCertificado" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoCertificado_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoCertificadoVacio" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoCertificado" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoCertificado" />
                                        <asp:PostBackTrigger ControlID="btnVerCertificadoCertificado" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de inicio <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaInicioCertificado" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaInicioCertificado_OnChanged"></asp:TextBox>

                                    <div id="divFechaInicioCertificadoIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaInicioCertificadoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de finalización</label>
                                    <asp:TextBox class="form-control" ID="txtFechaFinalizacionCertificado" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>

                                    <div id="divFechaFinalizacionCertificadoIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaFinalizacionCertificadoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="La fecha de finalización debe ser mayor a la fecha de inicio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Observaciones </label>
                                    <asp:TextBox class="form-control" ID="txtObservacionesCertificado" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Certificación entregada</label>
                                    <!-- Marcarlo cuando se sube un archivo y desmarcarlo cuando se elimina -->
                                    <asp:CheckBox ID="ckbEntregadoCertificado" Enabled="false" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnEditarCertificado" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosCertificado_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para ver una experiencia laboral -->
    <div id="modalAgregarExperienciaLaboral" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar currículo vitae</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreEmpresaExperienciaLaboral" runat="server" OnTextChanged="txtNombreEmpresaExperienciaLaboral_TextChanged"></asp:TextBox>

                                    <div id="divNombreEmpresaExperienciaLaboralIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lbNombreEmpresaExperienciaLaboralIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>

                                    <div id="divNombreEmpresaExperienciaLaboralIncorrectoTamano" runat="server" style="display: none">
                                        <asp:Label ID="lbNombreEmpresaExperienciaLaboralIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción</label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionPuestoExperienciaLaboral" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <asp:UpdatePanel runat="server" ID="UpdatePanel15">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <asp:FileUpload ID="fuCurriculum" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoCurriculum" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoCurriculum_Click" />
                                            <asp:Button ID="btnQuitarArchivoCurriculum" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoCurriculum_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoCurriculumVacio" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoCurriculum" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoCurriculum" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarExperienciaLaboral" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarExperienciaLaboral_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para ver una habilidad blanda -->
    <div id="modalVerHabilidadBlanda" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información de la habilidad</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtDescripcionHabilidadBlanda" runat="server" OnTextChanged="txtDescripcionHabilidadBlanda_TextChanged"></asp:TextBox>

                                    <div id="divDescripcionHabilidadBlandaIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lbDescripcionHabilidadBlandaIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>

                                    <div id="divDescripcionHabilidadBlandaIncorrectoTamano" runat="server" style="display: none">
                                        <asp:Label ID="lbDescripcionHabilidadBlandaIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 250 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnEditarHabilidadBlanda" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosHabilidadBlanda_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para ver un interés personal -->
    <div id="modalVerInteresPersonal" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información del interés personal</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtDescripcionInteresPersonal" runat="server" OnTextChanged="txtDescripcionInteresPersonal_TextChanged"></asp:TextBox>

                                    <div id="divDescripcionInteresPersonalIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lbDescripcionInteresPersonalIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divDescripcionInteresPersonalIncorrectoTamano" runat="server" style="display: none">
                                        <asp:Label ID="lbDescripcionInteresPersonalIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 250 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnEditarInteresPersonal" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosInteresPersonal_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para ver pensiones o embargos -->
    <div id="modalVerPensionOEmbargo" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información de la pensión o embargo</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo <span class="rojo">*</span></label>
                                            <br />
                                            <asp:LinkButton ID="btnVerCertificadoPensionOEmbargo" runat="server" CssClass="form-control" OnClick="btnVerCertificadoPensionOEmbargo_Click"></asp:LinkButton>
                                            <br />
                                            <asp:FileUpload ID="fuCertificadoPensionOEmbargo" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoPensionOEmbargo" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoPensionOEmbargo_Click" />
                                            <asp:Button ID="btnQuitarArchivoPensionOEmbargo" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoPensionOEmbargo_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoPensionOEmbargoVacio" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoPensionOEmbargo" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoPensionOEmbargo" />
                                        <asp:PostBackTrigger ControlID="btnVerCertificadoPensionOEmbargo" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción </label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionPensionOEmbargo" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnEditarPensionOEmbargo" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosPensionOEmbargo_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <!-- Modal para ver un comprobante -->
    <div id="modalVerComprobantes" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información del comprobante</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Tipo de comprobante</label>
                                    <asp:DropDownList ID="TipoComprobantesDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaComprobantes" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaComprobantes_OnChanged"></asp:TextBox>

                                    <div id="divFechaComprobantesIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaComprobantesIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <br />
                                            <asp:LinkButton ID="btnVerCertificadoComprobantes" runat="server" CssClass="form-control" OnClick="btnVerCertificadoComprobantes_Click"></asp:LinkButton>
                                            <br />
                                            <asp:FileUpload ID="fuCertificadoComprobantes" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoComprobantes" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoComprobantes_Click" />
                                            <asp:Button ID="btnQuitarArchivoComprobantes" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoComprobantes_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoComprobantesVacio" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoComprobantes" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoComprobantes" />
                                        <asp:PostBackTrigger ControlID="btnVerCertificadoComprobantes" />
                                    </Triggers>
                                </asp:UpdatePanel>


                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción </label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionComprobantes" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnEditarComprobantes" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosComprobantes_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para ver un antecedente -->
    <div id="modalVerAntecedentes" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información del antecedente</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Tipo de antecedente</label>
                                    <asp:DropDownList ID="TipoAntecedentesDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreAntecedentes" runat="server" OnTextChanged="txtNombreAntecedentes_TextChanged"></asp:TextBox>

                                    <div id="divNombreAntecedentesIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreAntecedentesIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreAntecedentesIncorrectoTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreAntecedentesIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha</label>
                                    <asp:TextBox class="form-control" ID="txtFechaAntecedentes" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel7">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <br />
                                            <asp:LinkButton ID="btnVerCertificadoAntecedentes" runat="server" CssClass="form-control" OnClick="btnVerCertificadoAntecedentes_Click"></asp:LinkButton>
                                            <br />
                                            <asp:FileUpload ID="fuCertificadoAntecedentes" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoAntecedentes" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoAntecedentes_Click" />
                                            <asp:Button ID="btnQuitarArchivoAntecedentes" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoAntecedentes_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoAntecedentesVacio" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoAntecedentes" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoAntecedentes" />
                                        <asp:PostBackTrigger ControlID="btnVerCertificadoAntecedentes" />
                                    </Triggers>
                                </asp:UpdatePanel>


                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción </label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionAntecedentes" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnEditarAntecedentes" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosAntecedentes_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para ver una suspensión o permiso -->
    <div id="modalVerSuspensionPermiso" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información de la suspensión o permiso</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Tipo <span class="rojo">*</span></label>
                                    <asp:DropDownList ID="TipoSuspensionPermisoDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de salida <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaSalidaSuspensionPermiso" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaSalidaSuspensionPermiso_OnChanged"></asp:TextBox>

                                    <div id="divFechaSalidaSuspensionPermisoIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lbFechaSalidaSuspensionPermisoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de regreso</label>
                                    <asp:TextBox class="form-control" ID="txtFechaRegresoSuspensionPermiso" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>

                                    <div id="divFechaRegresoSuspensionPermisoIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lbFechaRegresoSuspensionPermisoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="La fecha de regreso debe ser mayor a la fecha de inicio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionSuspensionPermiso" TextMode="MultiLine" runat="server"></asp:TextBox>
                                
                                    <div id="divDescripcionSuspensionPermisoIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblDescripcionSuspensionPermisoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divDescripcionSuspensionPermisoIncorrectoTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblDescripcionSuspensionPermisoIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 250 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel14">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <br />
                                            <asp:LinkButton ID="btnVerArchivoSuspensionPermiso" runat="server" CssClass="form-control" OnClick="btnVerArchivoSuspensionPermiso_Click"></asp:LinkButton>
                                            <br />
                                            <asp:FileUpload ID="fuArchivoSuspensionPermiso" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoSuspensionPermiso" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoSuspensionPermisoAgregar_Click" />
                                            <asp:Button ID="btnQuitarArchivoSuspensionPermiso" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoSuspensionPermisoAgregar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoSuspensionPermisoVacio" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoSuspensionPermiso" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoSuspensionPermiso" />
                                        <%--<asp:PostBackTrigger ControlID="btnVerCertificadoSuspensionPermiso" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnEditarSuspensionPermiso" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosSuspensionPermiso_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar un estudio formal -->
    <div id="modalAgregarEstudio" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar un estudio</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Tipo de estudio</label>
                                    <asp:DropDownList ID="TipoEstudioDDLAgregar" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TipoEstudioDDLAgregar_TextChanged"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreEstudioAgregar" runat="server" OnTextChanged="txtNombreEstudioAgregar_TextChanged"></asp:TextBox>

                                    <div id="divNombreEstudioIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreEstudioIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreEstudioIncorrectoAgregarTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreEstudioIncorrectoAgregarTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="CertificadoEstudioUpdatePanelAgregar">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Título</label>
                                            <asp:FileUpload ID="fuCertificadoEstudioAgregar" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoEstudioAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoEstudioAgregar_Click" />
                                            <asp:Button ID="btnQuitarArchivoEstudioAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoEstudioAgregar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoEstudioVacioAgregar" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoEstudioAgregar" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoEstudioAgregar" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de inicio <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaInicioEstudioAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaInicioEstudioAgregar_OnChanged"></asp:TextBox>

                                    <div id="divFechaInicioEstudioIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaInicioEstudioIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de finalización</label>
                                    <asp:TextBox class="form-control" ID="txtFechaFinalizacionEstudioAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>

                                    <div id="divFechaFinalizacionEstudioIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaFinalizacionEstudioIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="La fecha de finalización debe ser mayor a la fecha de inicio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Observaciones </label>
                                    <asp:TextBox class="form-control" ID="txtObservacionesEstudioAgregar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Certificación entregada</label>
                                    <!-- Marcarlo cuando se sube un archivo y desmarcarlo cuando se elimina -->
                                    <asp:CheckBox ID="ckbEntregadoEstudioAgregar" Enabled="false" runat="server" />
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarEstudioAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarEstudio_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar un curso informal -->
    <div id="modalAgregarCurso" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar un curso</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreCursoAgregar" runat="server" OnTextChanged="txtNombreCursoAgregar_TextChanged"></asp:TextBox>

                                    <div id="divNombreCursoIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreCursoIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreCursoIncorrectoAgregarTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreCursoIncorrectoAgregarTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Certificado</label>
                                            <asp:FileUpload ID="fuCertificadoCursoAgregar" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoCursoAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoCursoAgregar_Click" />
                                            <asp:Button ID="btnQuitarArchivoCursoAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoCursoAgregar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoCursoVacioAgregar" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoCursoAgregar" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoCursoAgregar" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de inicio <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaInicioCursoAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaInicioCursoAgregar_OnChanged"></asp:TextBox>

                                    <div id="divFechaInicioCursoIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaInicioCursoIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de finalización</label>
                                    <asp:TextBox class="form-control" ID="txtFechaFinalizacionCursoAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>

                                    <div id="divFechaFinalizacionCursoIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaFinalizacionCursoIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="La fecha de finalización debe ser mayor a la fecha de inicio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Observaciones </label>
                                    <asp:TextBox class="form-control" ID="txtObservacionesCursoAgregar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Certificación entregada</label>
                                    <!-- Marcarlo cuando se sube un archivo y desmarcarlo cuando se elimina -->
                                    <asp:CheckBox ID="ckbEntregadoCursoAgregar" Enabled="false" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarCursoAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarCurso_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar un certificado -->
    <div id="modalAgregarCertificado" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar un certificado</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreCertificadoAgregar" runat="server" OnTextChanged="txtNombreCertificadoAgregar_TextChanged"></asp:TextBox>

                                    <div id="divNombreCertificadoIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lbNombreCertificadoIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreCertificadoIncorrectoAgregarTamano" runat="server" style="display: none">
                                        <asp:Label ID="lbNombreCertificadoIncorrectoAgregarTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Certificado</label>
                                            <asp:FileUpload ID="fuCertificadoCertificadoAgregar" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoCertificadoAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoCertificadoAgregar_Click" />
                                            <asp:Button ID="btnQuitarArchivoCertificadoAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoCertificadoAgregar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoCertificadoVacioAgregar" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoCertificadoAgregar" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoCertificadoAgregar" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de inicio <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaInicioCertificadoAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaInicioCertificadoAgregar_OnChanged"></asp:TextBox>

                                    <div id="divFechaInicioCertificadoIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaInicioCertificadoIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de finalización</label>
                                    <asp:TextBox class="form-control" ID="txtFechaFinalizacionCertificadoAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>

                                    <div id="divFechaFinalizacionCertificadoIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaFinalizacionCertificadoIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="La fecha de finalización debe ser mayor a la fecha de inicio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Observaciones </label>
                                    <asp:TextBox class="form-control" ID="txtObservacionesCertificadoAgregar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Certificación entregada</label>
                                    <!-- Marcarlo cuando se sube un archivo y desmarcarlo cuando se elimina -->
                                    <asp:CheckBox ID="ckbEntregadoCertificadoAgregar" Enabled="false" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarCertificadoAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarCertificado_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar una experiencia laboral -->
    <div id="modalVerEditarExperienciaLaboral" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información del currículo vitae</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreEmpresaExperienciaLaboralVerEditar" runat="server" OnTextChanged="txtNombreEmpresaExperienciaLaboralVerEditar_TextChanged"></asp:TextBox>

                                    <div id="divNombreEmpresaExperienciaLaboralIncorrectoVerEditar" runat="server" style="display: none">
                                        <asp:Label ID="lbNombreEmpresaExperienciaLaboralIncorrectoVerEditar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreEmpresaExperienciaLaboralIncorrectoVerEditarTamano" runat="server" style="display: none">
                                        <asp:Label ID="lbNombreEmpresaExperienciaLaboralIncorrectoVerEditarTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción</label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionPuestoExperienciaLaboralVerEditar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel16">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <br />
                                            <asp:LinkButton ID="btnVerCurriculum" runat="server" CssClass="form-control" OnClick="btnVerCertificadoAccionesPersonal_Click"></asp:LinkButton>
                                            <br />
                                            <asp:FileUpload ID="fuCurriculumVerEditar" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoCurriculumVerEditar" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoCurriculumVerEditar_Click" />
                                            <asp:Button ID="btnQuitarArchivoCurriculumVerEditar" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoCurriculumVerEditar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoCurriculumVacioVerEditar" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoCurriculumVerEditar" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoCurriculumVerEditar" />
                                        <asp:PostBackTrigger ControlID="btnVerCurriculum" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                 <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>

                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnVerEditarExperienciaLaboralVerEditar" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosExperienciaLaboral_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar una habilidad blanda -->
    <div id="modalAgregarHabilidadBlanda" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar habilidad blanda</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtDescripcionHabilidadBlandaAgregar" runat="server" OnTextChanged="txtDescripcionHabilidadBlandaAgregar_TextChanged"></asp:TextBox>

                                    <div id="divDescripcionHabilidadBlandaIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lbDescripcionHabilidadBlandaIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divDescripcionHabilidadBlandaIncorrectoAgregarTamano" runat="server" style="display: none">
                                        <asp:Label ID="lbDescripcionHabilidadBlandaIncorrectoAgregarTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 250 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarHabilidadBlandaAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarHabilidadBlanda_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar un interés personal -->
    <div id="modalAgregarInteresPersonal" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar interés personal</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtDescripcionInteresPersonalAgregar" runat="server" OnTextChanged="txtDescripcionInteresPersonalAgregar_TextChanged"></asp:TextBox>

                                    <div id="divDescripcionInteresPersonalIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lbDescripcionInteresPersonalIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divDescripcionInteresPersonalIncorrectoAgregarTamano" runat="server" style="display: none">
                                        <asp:Label ID="lbDescripcionInteresPersonalIncorrectoAgregarTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 250 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarInteresPersonalAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarInteresPersonal_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar pensiones o embargos -->
    <div id="modalAgregarPensionOEmbargo" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar una pensión o un embargo</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel9">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Certificado <span class="rojo">*</span></label>
                                            <asp:FileUpload ID="fuCertificadoPensionOEmbargoAgregar" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoPensionOEmbargoAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoPensionOEmbargoAgregar_Click" />
                                            <asp:Button ID="btnQuitarArchivoPensionOEmbargoAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoPensionOEmbargoAgregar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoPensionOEmbargoVacioAgregar" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoPensionOEmbargoAgregar" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoPensionOEmbargoAgregar" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción </label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionPensionOEmbargoAgregar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarPensionOEmbargoAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarPensionOEmbargo_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar una acción de personal -->
    <div id="modalAgregarAccionesPersonal" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar un documento de trámite</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre</label>
                                    <asp:TextBox class="form-control" ID="txtNombreAccionesPersonal" runat="server" OnTextChanged="txtNombreAccionesPersonal_TextChanged"></asp:TextBox>

                                    <div id="divNombreAccionesPersonalIncorrecto" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreAccionesPersonalIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>

                                    <div id="divNombreAccionesPersonalIncorrectoTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreAccionesPersonalIncorrectoTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 250 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <asp:FileUpload ID="fuCertificadoAccionesPersonal" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoAccionesPersonal" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoAccionesPersonal_Click" />
                                            <asp:Button ID="btnQuitarArchivoAccionesPersonal" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoAccionesPersonal_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoAccionesPersonalVacio" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoAccionesPersonal" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoAccionesPersonal" />
                                    </Triggers>
                                </asp:UpdatePanel>


                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción </label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionAccionesPersonal" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Número </label>
                                    <asp:TextBox class="form-control" ID="txtNumero" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarAccionesPersonal" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarAccionesPersonal_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <!-- Modal para agregar una acción de personal -->
    <div id="modalVerEditarAccionesPersonal" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Información de documento de trámite</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre</label>
                                    <asp:TextBox class="form-control" ID="txtNombreAccionesPersonalVerEditar" runat="server" OnTextChanged="txtNombreAccionesPersonalVerEditar_TextChanged"></asp:TextBox>

                                    <div id="divNombreAccionesPersonalIncorrectoVerEditar" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreAccionesPersonalIncorrectoVerEditar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreAccionesPersonalIncorrectoVerEditarTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreAccionesPersonalIncorrectoVerEditarTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 250 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel10">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <br />
                                            <asp:LinkButton ID="btnVerCertificadoAccionesPersonal" runat="server" CssClass="form-control" OnClick="btnVerCertificadoAccionesPersonal_Click"></asp:LinkButton>
                                            <br />
                                            <asp:FileUpload ID="fuCertificadoAccionesPersonalVerEditar" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoAccionesPersonalVerEditar" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoAccionesPersonalVerEditar_Click" />
                                            <asp:Button ID="btnQuitarArchivoAccionesPersonalVerEditar" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoAccionesPersonalVerEditar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoAccionesPersonalVacioVerEditar" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoAccionesPersonalVerEditar" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoAccionesPersonalVerEditar" />
                                        <asp:PostBackTrigger ControlID="btnVerCertificadoAccionesPersonal" />
                                    </Triggers>
                                </asp:UpdatePanel>


                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción </label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionAccionesPersonalVerEditar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Número </label>
                                    <asp:TextBox class="form-control" ID="txtNumeroVerEditar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnVerEditarAccionesPersonalVerEditar" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregarCambiosAccionesPersonal_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar un comprobante -->
    <div id="modalAgregarComprobantes" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar un comprobante</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Tipo de comprobante</label>
                                    <asp:DropDownList ID="TipoComprobantesDDLAgregar" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaComprobantesAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaComprobantesAgregar_OnChanged"></asp:TextBox>

                                    <div id="divFechaComprobantesIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblFechaComprobantesIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel11">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <asp:FileUpload ID="fuCertificadoComprobantesAgregar" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoComprobantesAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoComprobantesAgregar_Click" />
                                            <asp:Button ID="btnQuitarArchivoComprobantesAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoComprobantesAgregar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoComprobantesVacioAgregar" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoComprobantesAgregar" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoComprobantesAgregar" />
                                    </Triggers>
                                </asp:UpdatePanel>


                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción </label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionComprobantesAgregar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarComprobantesAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarComprobantes_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar un antecedente -->
    <div id="modalAgregarAntecedentes" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar un antecedente</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Tipo de antecedente</label>
                                    <asp:DropDownList ID="TipoAntecedentesDDLAgregar" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Nombre <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtNombreAntecedentesAgregar" runat="server" OnTextChanged="txtNombreAntecedentesAgregar_TextChanged"></asp:TextBox>

                                    <div id="divNombreAntecedentesIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreAntecedentesIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divNombreAntecedentesIncorrectoAgregarTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblNombreAntecedentesIncorrectoAgregarTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 100 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha</label>
                                    <asp:TextBox class="form-control" ID="txtFechaAntecedentesAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel12">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <asp:FileUpload ID="fuCertificadoAntecedentesAgregar" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoAntecedentesAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoAntecedentesAgregar_Click" />
                                            <asp:Button ID="btnQuitarArchivoAntecedentesAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoAntecedentesAgregar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoAntecedentesVacioAgregar" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoAntecedentesAgregar" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoAntecedentesAgregar" />
                                    </Triggers>
                                </asp:UpdatePanel>


                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción </label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionAntecedentesAgregar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <h6>Los campos marcados con <span class="rojo">*</span> son requeridos.</h6>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarAntecedentesAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarAntecedentes_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para agregar una suspensión o permiso -->
    <div id="modalAgregarSuspensionPermiso" class="modal fade" role="alertdialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Agregar un suspensión o un permiso</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Tipo <span class="rojo">*</span></label>
                                    <asp:DropDownList ID="TipoSuspensionPermisoDDLAgregar" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de salida <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtFechaSalidaSuspensionPermisoAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date" OnTextChanged="txtFechaSalidaSuspensionPermisoAgregar_OnChanged"></asp:TextBox>

                                    <div id="divFechaSalidaSuspensionPermisoIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lbFechaSalidaSuspensionPermisoIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Fecha de regreso</label>
                                    <asp:TextBox class="form-control" ID="txtFechaRegresoSuspensionPermisoAgregar" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>

                                    <div id="divFechaRegresoSuspensionPermisoIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lbFechaRegresoSuspensionPermisoIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="La fecha de regreso debe ser mayor a la fecha de inicio" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <label>Descripción <span class="rojo">*</span></label>
                                    <asp:TextBox class="form-control" ID="txtDescripcionSuspensionPermisoAgregar" TextMode="MultiLine" runat="server"></asp:TextBox>
                                
                                    <div id="divDescripcionSuspensionPermisoIncorrectoAgregar" runat="server" style="display: none">
                                        <asp:Label ID="lblDescripcionSuspensionPermisoIncorrectoAgregar" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="divDescripcionSuspensionPermisoIncorrectoAgregarTamano" runat="server" style="display: none">
                                        <asp:Label ID="lblDescripcionSuspensionPermisoIncorrectoAgregarTamano" runat="server" Font-Size="Small" class="label alert-danger" Text="La longitud no debe ser mayor a 250 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <!-- Ingreso para los archivos multimedia -->
                                <asp:UpdatePanel runat="server" ID="UpdatePanel13">
                                    <ContentTemplate>
                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <label>Adjuntar archivo</label>
                                            <asp:FileUpload ID="fuArchivoSuspensionPermisoAgregar" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                        </div>

                                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <asp:Button ID="btnSeleccionarArchivoSuspensionPermisoAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Cargar" OnClick="btnSeleccionarArchivoSuspensionPermisoAgregar_Click" />
                                            <asp:Button ID="btnQuitarArchivoSuspensionPermisoAgregar" runat="server" CssClass="btn btn-sm btn-primary" Text="Remover" OnClick="btnQuitarArchivoSuspensionPermisoAgregar_Click" Visible="false" />
                                            <asp:Label ID="lblArchivoSuspensionPermisoVacioAgregar" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSeleccionarArchivoSuspensionPermisoAgregar" />
                                        <asp:PostBackTrigger ControlID="btnQuitarArchivoSuspensionPermisoAgregar" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnAgregarSuspensionPermisoAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarSuspensionPermiso_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Volver</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
