<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ayuda.aspx.cs" Inherits="EDP.Ayuda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true" />

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <!-- Sección del menú lateral -->
            <div class="sidebar">
                <asp:LinkButton ID="aIntroduccion" runat="server" Text="Introducción" CssClass="active" OnClick="showHideIntroduccion" />
                <asp:LinkButton ID="aAgregarFuncionario" runat="server" Text="Agregar un funcionario" CssClass="" OnClick="showHideAgregarFuncionario" />
            </div>

            <div class="content-funcionario">

                <!-- Sección que contiene la introducción -->
                <div id="divIntroduccion" runat="server">

                    <div>
                        <hr />
                        <center>
                            <asp:Label runat="server" Text="Introducción" Font-Size="Large" ForeColor="Black"></asp:Label>
                        </center>
                        <hr />
                    </div>

                    <div class="row">
                        <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 text-center">
                            <video width="500" height="400" controls="controls">
                                <source src='<%=Page.ResolveUrl("~/Videos/Introduccion.mp4") %>' type="video/mp4" />
                                Your browser does not support the video tag
                            </video>
                        </div>
                    </div>

                </div>

                <div id="divAgregarFuncionario" runat="server">
                    <div>
                        <hr />
                        <center>
                            <asp:Label runat="server" Text="Agregar funcionarios" Font-Size="Large" ForeColor="Black"></asp:Label>
                        </center>

                        <hr />
                    </div>

                    

                </div>
            </div>


            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
