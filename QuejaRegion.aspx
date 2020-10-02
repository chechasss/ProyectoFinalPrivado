<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/home.Master" CodeBehind="QuejaRegion.aspx.vb" Inherits="WebDIACO_V2.QuejaRegion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DIACOTeEscucha</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Regreso" runat="server">
    <section class="content-header">
        <ol class="breadcrumb">
            <li><a href="inicio.aspx"><i class="fa fa-home"></i>Home</a></li>
            <li><a href="PrincipalMenu.aspx"><i class="fa fa-home"></i>Menu Principal</a></li>
            <li class="active">Quejas Region</li>
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
                            <h3 class="box-title">Quejas por Region</h3>
                            <div>
                                Seleccione la Region
                            </div>
                            <div id="sel_Regiones"></div>
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
                    pMetodo: "CargaRegiones"
                },
                success: function (data) {
                    $("#sel_Regiones").html(data);
                },
                error: function () {
                    alert("error en el llenado de los datos");
                }
            });
        }

        $("#sel_Regiones").change(function () {
            var depID = $("#sel_region").val();
            cargarReporte(depID);
        });

        function cargarReporte(strDepID) {
            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajx/ajxFuncBD.aspx",
                data: {
                    pMetodo: "QuejasRegion",
                    pMuni: strDepID
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
