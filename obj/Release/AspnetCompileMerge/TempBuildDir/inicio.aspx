<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/home.Master" CodeBehind="inicio.aspx.vb" Inherits="WebDIACO_V2.inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DIACOTeEscucha</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Regreso" runat="server">
    <section class="content-header">
        <h1>
            DIACO Te Escucha
        </h1>
        <small>Un sitio dedicado a la captacion de comentarios e incidentes sobre los comercios.</small>
        <br />
        <small>No te quedes con la experiencia, Expresate porque <a role ="link" data-focusable= "true"><strong>#DIACOTEESCUCHA</strong></a></small>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-box">
        <div class="form-group">
            <a href="IngresoQuejas.aspx" class="btn btn-block btn-primary btn-lg" >
                <i class="fa fa-edit"></i>
                Ingresar Queja
            </a>
            <a href="Login.aspx" class="btn btn-block btn-success btn-sm" >
                <i class="fa fa-users"></i>
                Iniciar Sesion
            </a>
        </div>
    </div>
</asp:Content>