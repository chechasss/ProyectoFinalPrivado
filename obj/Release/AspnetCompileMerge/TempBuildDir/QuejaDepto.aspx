<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/home.Master" CodeBehind="QuejaDepto.aspx.vb" Inherits="WebDIACO_V2.QuejaDepto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DIACOTeEscucha</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Regreso" runat="server">
    <section class="content-header">
        <ol class="breadcrumb">
            <li><a href="inicio.aspx"><i class="fa fa-home"></i>Home</a></li>
            <li><a href="PrincipalMenu.aspx"><i class="fa fa-home"></i>Menu Principal</a></li>
            <li class="active">Quejas Departamento</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Right side column. Contains the navbar and content of the page -->
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Quejas por Departamento</h3>
                            <div>
                                Seleccione el Departamento
                            </div>
                            <div id="sel_Deptos"></div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div id="tab_Reporte">
                            </div>

                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </section>
        <!-- /.content -->

    </div>
    <!-- /.content-wrapper -->
    <script type="text/javascript">
        $(document).ready(function () {
            //alert("In Jquery");
            cargarDeptos();
            //cargarReporte();
        });

        function cargarDeptos() {
            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajx/ajxFuncBD.aspx",
                data: {
                    pMetodo: "CargaDeptos"
                },
                success: function (data) {
                    $("#sel_Deptos").html(data);
                },
                error: function () {
                    alert("error en el llenado de los datos");
                }
            });
        }

        $("#sel_Deptos").change(function () {
            var depID = $("#sel_departamento").val().split("|")
            cargarReporte(depID[0]);
        });

        function cargarReporte(strDepID) {
            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajx/ajxFuncBD.aspx",
                data: {
                    pMetodo: "QuejasDepto",
                    pDepto: strDepID
                },
                success: function (data) {
                    var Adata = data.split("#");
                    var Bdata = Adata[0].split("|")
                    if (Bdata[0] == 1) {
                        $("#tab_Reporte").html(Bdata[1]);
                    } else {
                        alert("No existen datos para el departamento seleccionado");
                    }
                },
                error: function () {
                    alert("error en el llenado de los datos");
                }
            });
        }

        $(function () {
            $('#example2').dataTable({
                "bPaginate": true,
                "bLengthChange": false,
                "bFilter": false,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": false
            });
            /*
                 * DONUT CHART
                 * -----------
                 */



        });
    </script>
</asp:Content>
