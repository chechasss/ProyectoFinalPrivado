<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/home.Master" CodeBehind="PrincipalMenu.aspx.vb" Inherits="WebDIACO_V2.PrincipalMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>DIACOTeEscucha</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Regreso" runat="server">
     <section class="content-header">
        <ol class="breadcrumb">
            <li><a href="inicio.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Menu Principal</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
            <div class="col-lg-3 col-xs-6">
              <!-- small box -->
              <div class="small-box bg-green">
                <div class="inner">
                  <h1>
                    Quejas por Municipio
                  </h1>
                </div>
                <div class="icon">
                  <i class="ion ion-stats-bars"></i>
                </div>
                <a href="/QuejaMuni.aspx" class="small-box-footer">
                  Ir <i class="fa fa-arrow-circle-right"></i>
                </a>
              </div>
            </div><!-- ./col -->
            <div class="col-lg-3 col-xs-6">
              <!-- small box -->
              <div class="small-box bg-yellow">
                <div class="inner">
                  <h1>
                    Quejas por Departamento
                  </h1>
                  </div>
                <div class="icon">
                  <i class="ion ion-person-add"></i>
                </div>
                <a href="/QuejaDepto.aspx" class="small-box-footer">
                  Ir <i class="fa fa-arrow-circle-right"></i>
                </a>
              </div>
            </div><!-- ./col -->
            <div class="col-lg-3 col-xs-6">
              <!-- small box -->
              <div class="small-box bg-red">
                <div class="inner">
                  <h1>
                    Quejas por Region
                  </h1>
                </div>
                <div class="icon">
                  <i class="ion ion-pie-graph"></i>
                </div>
                <a href="QuejaRegion.aspx" class="small-box-footer">
                  Ir <i class="fa fa-arrow-circle-right"></i>
                </a>
              </div>
            </div><!-- ./col -->
          </div><!-- /.row -->
</asp:Content>
