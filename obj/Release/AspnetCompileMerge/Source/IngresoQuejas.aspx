<%@ Page Title="" Language="vb" MasterPageFile="~/home.Master" CodeBehind="IngresoQuejas.aspx.vb" Inherits="WebDIACO_V2.IngresoQuejas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DIACOTeEscucha</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Regreso" runat="server">
    <section class="content-header">
        <ol class="breadcrumb">
            <li><a href="inicio.aspx"><i class="fa fa-home"></i>Home</a></li>
            <li class="active">Ingreso de Quejas</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Registro de Quejas / Comentarios</h1>
        </section>
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Registro de Quejas / Eventos</h3>

                    </div>
                    <div class="box-body table-responsive">
                        <div id="div_sel_departamento">
                        </div>
                        <div id="lblRegion">
                            <label>Región: </label>
                            <label id="txtRegion"></label>
                        </div>
                        <label>Seleccione Municipio</label>
                        <div id="div_sel_municipio">
                            <select class="form-control" disabled></select>
                        </div>
                        <label>Seleccione Comercio</label>
                        <div id="div_sel_comercio">
                            <select class="form-control" disabled></select>
                        </div>
                        <label>Seleccione sucursal</label>
                        <div id="div_sel_sucursal">
                            <select class="form-control" disabled></select>
                        </div>
                        <div class="form-group">
                            <label>Fecha del Incidente</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control" id="fec_incidente" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Describa el Incidente Ocurrido</label>
                            <textarea id="txtIncidente" class="form-control" rows="3" maxlength="3999" placeholder="Desriba el Incidente"></textarea>
                        </div>
                        <div class="form-group">
                            <label>Indique Propuesta de Mejora</label>
                            <textarea id="txtPropuesta" class="form-control" rows="3" maxlength="3999" placeholder="Propuesta de Mejora (opcional)"></textarea>
                        </div>
                    </div>
                    <!-- /.box-body -->
                    <div class="box-footer">
                        <button type="button" class="btn btn-primary" onclick="GuardarQueja();">Guardar</button>
                        <div id="div_RespuestaE">
                            <label>Grabado Exitosamente</label></div>
                        <div id="div_RespuestaERR">
                            <label>Error al Almacenar la Información</label></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            //alert("In Jquery");
            CargaDepartamentos();
            $(function () {
                //Datemask dd/mm/yyyy
                $("#datemask").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            });
            $("#div_RespuestaE").hide();
            $("#div_RespuestaERR").hide();
        });

        function CargaDepartamentos() {
            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajx/ajxFuncBD.aspx",
                data: {
                    pMetodo: "CargaDeptos"
                },
                success: function (data) {
                    $("#div_sel_departamento").html(data);
                },
                error: function () {
                    alert("error en el llenado de los datos");
                }
            });
        }

        $("#div_sel_departamento").change(function () {
            var depID = $("#sel_departamento").val().split("|")
            $("#txtRegion").text(depID[1]);
            CargaMunicipios(depID[0]);
        });

        function CargaMunicipios(idDepartamento) {
            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajx/ajxFuncBD.aspx",
                data: {
                    pMetodo: "CargaMuni",
                    pDepto: idDepartamento
                },
                success: function (data) {
                    $("#div_sel_municipio").html(data);
                },
                error: function () {
                    alert("error en el llenado de los datos");
                }
            });
        }

        $("#div_sel_municipio").change(function () {
            var IDMun = $("#sel_municipio").val();
            CargaComercios(IDMun);
        });

        function CargaComercios(idMunicipio) {
            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajx/ajxFuncBD.aspx",
                data: {
                    pMetodo: "CargaComercios",
                    pMuni: idMunicipio
                },
                success: function (data) {
                    $("#div_sel_comercio").html(data);
                },
                error: function () {
                    alert("error en el llenado de los datos");
                }
            });
        }

        $("#div_sel_comercio").change(function () {
            var IDSuc = $("#sel_comercio").val();
            var IDMun = $("#sel_municipio").val();
            CargaSucursales(IDSuc, IDMun);
        });

        function CargaSucursales(IDSuc, IDMun) {
            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajx/ajxFuncBD.aspx",
                data: {
                    pMetodo: "CargaSucursales",
                    pSucursal: IDSuc,
                    pMuni: IDMun
                },
                success: function (data) {
                    $("#div_sel_sucursal").html(data);
                },
                error: function () {
                    alert("error en el llenado de los datos");
                }
            });
        }

        $(function () {
            //Datemask dd/mm/yyyy
            $("#datemask").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $("[data-mask]").inputmask();
        });

        function GuardarQueja() {
            var IDSucursal = $("#sel_sucursal").val();
            var FecIncidente = $("#fec_incidente").val();
            var queja = $("#txtIncidente").val();
            var sugerencia = $("#txtPropuesta").val();
            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajx/ajxFuncBD.aspx",
                data: {
                    pMetodo: "GuardaQueja",
                    pSucursal: IDSucursal,
                    pFecIncidente: FecIncidente,
                    pQueja: queja,
                    pSugerencia: sugerencia
                },
                success: function (data) {
                    var respuesta = data.split("<");
                    if (respuesta[0] == "1") {
                        $("#div_RespuestaE").show();
                        setTimeout(function () { window.location = "/inicio.aspx"; }, 5000);
                    } else {
                        $("#div_RespuestaERR").show();
                    }
                },
                error: function () {
                    $("#div_RespuestaERR").show();
                }
            });
        }
    </script>
</asp:Content>

