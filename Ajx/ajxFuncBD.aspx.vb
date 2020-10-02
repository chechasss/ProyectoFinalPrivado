Imports System.IO
Imports Newtonsoft.Json
Partial Class AjxFuncBD
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strMetodo As String
        Dim strIdDepto As String
        Dim strIdMuni As String
        Dim strIdComercio As String
        Dim strFechaIncidente As String
        Dim strSugerencia As String
        Dim strQueja As String
        Dim strIdSucursal As String
        If Not IsPostBack Then
            If Request.RequestType = "POST" Then
                strMetodo = Request.Form("pMetodo")
            Else
                strMetodo = Request.QueryString("pMetodo")
            End If
        End If

        If strMetodo = "CargaDeptos" Then
            Response.Write(CargaDepartamentos())
        ElseIf strMetodo = "CargaRegiones" Then
            Response.Write(CargaRegiones())
        ElseIf strMetodo = "CargaMuni" Then
            strIdDepto = Request.Form("pDepto")
            Response.Write(CargaMunicipios(strIdDepto))
        ElseIf strMetodo = "CargaComercios" Then
            strIdMuni = Request.Form("pMuni")
            Response.Write(CargaComercios(strIdMuni))
        ElseIf strMetodo = "CargaSucursales" Then
            strIdComercio = Request.Form("pSucursal")
            strIdMuni = Request.Form("pMuni")
            Response.Write(CargaSucursales(strIdComercio, strIdMuni))
        ElseIf strMetodo = "GuardaQueja" Then
            strIdSucursal = Request.Form("pSucursal")
            strFechaIncidente = Request.Form("pFecIncidente")
            strSugerencia = Request.Form("pSugerencia")
            strQueja = Request.Form("pQueja")
            Response.Write(GuardaQueja(strIdSucursal, strFechaIncidente, strSugerencia, strQueja))
        ElseIf strMetodo = "IniciaSesion" Then
            Dim strUser As String = Request.QueryString("pUser")
            Dim strPass As String = Request.QueryString("pPass")
            Response.Write(IniciaSesion(strUser, strPass))
        ElseIf strMetodo = "QuejasDepto" Then
            strIdDepto = Request.Form("pDepto")
            Response.Write(QuejasDepto(strIdDepto))
        ElseIf strMetodo = "QuejasMuni" Then
            strIdMuni = Request.Form("pMuni")
            Response.Write(QuejasMuni(strIdMuni))
        ElseIf strMetodo = "QuejasRegion" Then
            strIdMuni = Request.Form("pMuni")
            Response.Write(QuejasRegion(strIdMuni))
        End If
    End Sub

    Protected Function CargaDepartamentos() As String
        Dim objGeneral As New Funciones
        Dim dsDepartamentos As DataSet
        Dim strDepartamentos As String
        dsDepartamentos = objGeneral.Departamentos
        Dim strOpcion As String
        strDepartamentos = "<div class=""form-group""><select class=""form-control"" id=""sel_departamento""><option value =""0"">Seleccione</option>"
        For Each fila As DataRow In dsDepartamentos.Tables(0).Rows
            strOpcion = "<option value= """ + fila.Item("id_departamento").ToString + "|" + fila.Item("nombre_region").ToString + """>" + fila.Item("nombre_departamento").ToString + "</option>"
            strDepartamentos += strOpcion
        Next
        strDepartamentos = strDepartamentos + "</select></div>"
        Return strDepartamentos
    End Function
    Protected Function CargaRegiones() As String
        Dim objGeneral As New Funciones
        Dim dsRegiones As DataSet
        Dim strRegiones As String
        dsRegiones = objGeneral.Regiones
        Dim strOpcion As String
        strRegiones = "<div class=""form-group""><select class=""form-control"" id=""sel_region""><option value =""0"">Seleccione</option>"
        For Each fila As DataRow In dsRegiones.Tables(0).Rows
            strOpcion = "<option value= """ + fila.Item("id_region").ToString + """>" + fila.Item("nombre_region").ToString + "</option>"
            strRegiones += strOpcion
        Next
        strRegiones = strRegiones + "</select></div>"
        Return strRegiones
    End Function
    Protected Function CargaMunicipios(strIdDepto As String) As String
        Dim objGeneral As New Funciones
        Dim dsDepartamentos As DataSet
        Dim strDepartamentos As String
        dsDepartamentos = objGeneral.Municipios(strIdDepto)
        Dim strOpcion As String
        strDepartamentos = "<div class=""form-group""><select class=""form-control"" id=""sel_municipio""><option value =""0"">Seleccione</option>"
        For Each fila As DataRow In dsDepartamentos.Tables(0).Rows
            strOpcion = "<option value= """ + fila.Item("id_municipio").ToString + """>" + fila.Item("municipio").ToString + "</option>"
            strDepartamentos += strOpcion
        Next
        strDepartamentos = strDepartamentos + "</select></div>"
        Return strDepartamentos
    End Function
    Protected Function CargaComercios(strIdMuni As String) As String
        Dim objGeneral As New Funciones
        Dim dsDepartamentos As DataSet
        Dim strDepartamentos As String
        Dim strCodigo As String = "0"
        dsDepartamentos = objGeneral.Comercios(strIdMuni)
        Dim strOpcion As String
        If dsDepartamentos.Tables(0).Rows.Count > 0 Then
            strDepartamentos = "<div class=""form-group""><select class=""form-control"" id=""sel_comercio""><option value =""0"">Seleccione</option>"
            For Each fila As DataRow In dsDepartamentos.Tables(0).Rows
                strOpcion = "<option value= """ + fila.Item("id_comercio").ToString + """>" + fila.Item("nombre_comercio").ToString + "</option>"
                strDepartamentos += strOpcion
            Next
            strDepartamentos = strDepartamentos + "</select></div>"
            strCodigo = "1"
        Else
            strDepartamentos = "<div class=""form-group""><select class=""form-control"" disabled><option value =""0"">No Existen comercios registrados para el municipio seleccionado</option></select>"
            strCodigo = "0"
        End If
        Return strDepartamentos
    End Function
    Protected Function CargaSucursales(strIdComercio As String, strIdMunicipio As String) As String
        Dim objGeneral As New Funciones
        Dim dsSucursales As DataSet
        Dim strDepartamentos As String
        dsSucursales = objGeneral.Sucursales(strIdComercio, strIdMunicipio)
        Dim strOpcion As String
        If dsSucursales.Tables(0).Rows.Count > 0 Then
            strDepartamentos = "<div class=""form-group""><select class=""form-control"" id=""sel_sucursal""><option value =""0"">Seleccione</option>"
            For Each fila As DataRow In dsSucursales.Tables(0).Rows
                strOpcion = "<option value= """ + fila.Item("id_sucursal").ToString + """>" + fila.Item("nombre_sucursal").ToString + "</option>"
                strDepartamentos += strOpcion
            Next
            strDepartamentos = strDepartamentos + "</select></div>"
        Else
            strDepartamentos = "<div class=""form-group""><select class=""form-control"" disabled><option value =""0"">No Existen comercios registrados para el municipio seleccionado</option></select>"
        End If
        Return strDepartamentos
    End Function
    Protected Function GuardaQueja(strIdSucursal As String, strFechaIncidente As String, strSugerencia As String, strQueja As String) As String
        Try
            Dim objGeneral As New Funciones
            objGeneral.IngresaQueja(strIdSucursal, strFechaIncidente, strSugerencia, strQueja)
            Return "1<"
        Catch ex As Exception
            Return "0<"
        End Try
    End Function
    Protected Function IniciaSesion(strUser As String, strPass As String) As String
        Dim objGeneral As New Funciones
        Dim strUsuario As String
        Dim strResultado As String
        strUsuario = objGeneral.IniciaSesion(strUser, strPass)
        If strUsuario = strUser Then 'correcto
            Session("Usuario") = strUsuario
            strResultado = "1"
        ElseIf strUsuario = "0" Then 'inactivo
            strResultado = "2"
        ElseIf strUsuario = "1" Then 'Contraseña incorrecta
            strResultado = "3"
        Else ' no se encontro usuario
            strResultado = "0"
        End If

        Dim strBuilder As New StringBuilder()
        Dim strWriter As New StringWriter(strBuilder)
        Using jsString As New JsonTextWriter(strWriter)
            With jsString
                .Formatting = Newtonsoft.Json.Formatting.Indented
                .WriteStartObject()
                .WritePropertyName("CodigoRespuesta")
                .WriteValue(strResultado)
                .WriteEndObject()
            End With
        End Using
        Return strBuilder.ToString()
    End Function
    Protected Function QuejasDepto(strIdDepto As String) As String
        Dim objGeneral As New Funciones
        Dim dsQuejas As DataSet
        Dim strTabla As String
        strTabla = "1|<table id = ""example2"" Class=""table table-bordered table-hover"" aria-describedby=""example2_info"">"
        strTabla += "<thead><tr><th>Departamento</th><th>Municipio</th><th>Comercio</th><th>Sucursal</th>"
        strTabla += "<th>Fecha incidente</th><th>Fecha Reporte</th></tr></thead><tbody>"

        dsQuejas = objGeneral.QuejasDepto(strIdDepto)
        If dsQuejas.Tables(0).Rows.Count > 0 Then
            For Each drFila As DataRow In dsQuejas.Tables(0).Rows
                strTabla += "<tr><td>" + drFila.Item("nombre_departamento") + "</td><td>" + drFila.Item("municipio") + "</td><td>" + drFila.Item("nombre_comercio") + "</td><td>" + drFila.Item("nombre_sucursal") + "</td><td>" + drFila.Item("fecha_incidente") + "</td><td>" + drFila.Item("fecha_queja") + "</td></tr>"
            Next
            strTabla += "</tbody></table> #"
        Else
            strTabla = "0|Error #"
        End If

        Return strTabla
    End Function
    Protected Function QuejasMuni(strIdMuni As String) As String
        Dim objGeneral As New Funciones
        Dim dsQuejas As DataSet
        Dim strTabla As String
        strTabla = "1|<table id = ""example2"" Class=""table table-bordered table-hover"" aria-describedby=""example2_info"">"
        strTabla += "<thead><tr><th>Departamento</th><th>Municipio</th><th>Comercio</th><th>Sucursal</th>"
        strTabla += "<th>Fecha incidente</th><th>Fecha Reporte</th></tr></thead><tbody>"

        dsQuejas = objGeneral.QuejasMunicipio(strIdMuni)
        If dsQuejas.Tables(0).Rows.Count > 0 Then
            For Each drFila As DataRow In dsQuejas.Tables(0).Rows
                strTabla += "<tr><td>" + drFila.Item("nombre_departamento") + "</td><td>" + drFila.Item("municipio") + "</td><td>" + drFila.Item("nombre_comercio") + "</td><td>" + drFila.Item("nombre_sucursal") + "</td><td>" + drFila.Item("fecha_incidente") + "</td><td>" + drFila.Item("fecha_queja") + "</td></tr>"
            Next
            strTabla += "</tbody></table> #"
        Else
            strTabla = "0|Error #"
        End If

        Return strTabla
    End Function
    Protected Function QuejasRegion(strIdRegion As String) As String
        Dim objGeneral As New Funciones
        Dim dsQuejas As DataSet
        Dim strTabla As String
        strTabla = "1|<table id = ""example2"" Class=""table table-bordered table-hover"" aria-describedby=""example2_info"">"
        strTabla += "<thead><tr><th>Departamento</th><th>Municipio</th><th>Comercio</th><th>Sucursal</th>"
        strTabla += "<th>Fecha incidente</th><th>Fecha Reporte</th></tr></thead><tbody>"

        dsQuejas = objGeneral.QuejasRegion(strIdRegion)
        If dsQuejas.Tables(0).Rows.Count > 0 Then
            For Each drFila As DataRow In dsQuejas.Tables(0).Rows
                strTabla += "<tr><td>" + drFila.Item("nombre_departamento") + "</td><td>" + drFila.Item("municipio") + "</td><td>" + drFila.Item("nombre_comercio") + "</td><td>" + drFila.Item("nombre_sucursal") + "</td><td>" + drFila.Item("fecha_incidente") + "</td><td>" + drFila.Item("fecha_queja") + "</td></tr>"
            Next
            strTabla += "</tbody></table> #"
        Else
            strTabla = "0|Error #"
        End If

        Return strTabla
    End Function
End Class