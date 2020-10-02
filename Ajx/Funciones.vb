Imports System.Data
Imports System.Configuration
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Public Class Funciones
    Public Function Departamentos() As DataSet
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        Dim com As New MySqlCommand
        com.CommandText = "select dep.id_departamento,dep.id_region, dep.nombre_departamento,reg.nombre_region from db_a674f2_mydb.departamentos dep inner join db_a674f2_mydb.regiones reg on dep.id_region = reg.id_region"
        com.Connection = con
        Dim dbAdaptador As MySqlDataAdapter
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        Dim dsTabla As New DataSet
        dbAdaptador.Fill(dsTabla)
        con.Close()
        Return dsTabla
    End Function
    Public Function Regiones() As DataSet
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        Dim com As New MySqlCommand
        com.CommandText = "select id_region,nombre_region from db_a674f2_mydb.regiones"
        com.Connection = con
        Dim dbAdaptador As MySqlDataAdapter
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        Dim dsTabla As New DataSet
        dbAdaptador.Fill(dsTabla)
        con.Close()
        Return dsTabla
    End Function
    Public Function Municipios(strIdDepto As String) As DataSet
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        Dim com As New MySqlCommand
        com.CommandText = "select id_municipio,municipio from db_a674f2_mydb.municipios where id_departamento = " + strIdDepto
        com.Connection = con
        Dim dbAdaptador As MySqlDataAdapter
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        Dim dsTabla As New DataSet
        dbAdaptador.Fill(dsTabla)
        con.Close()
        Return dsTabla
    End Function
    Public Function Comercios(strIdMuni As String) As DataSet
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        Dim com As New MySqlCommand
        com.CommandText = "select com.id_comercio,com.nombre_comercio from db_a674f2_mydb.comercio com right join db_a674f2_mydb.sucursal suc on com.id_comercio = suc.id_comercio where com.estado = 1 and suc.estado = 1 and  suc.id_municipio = " + strIdMuni
        com.Connection = con
        Dim dbAdaptador As MySqlDataAdapter
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        Dim dsTabla As New DataSet
        dbAdaptador.Fill(dsTabla)
        con.Close()
        Return dsTabla
    End Function
    Public Function Sucursales(strIdComercio As String, strIdMunicipio As String) As DataSet
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        Dim com As New MySqlCommand
        com.CommandText = "select id_sucursal,nombre_sucursal from db_a674f2_mydb.sucursal where id_municipio = " + strIdMunicipio + " and id_comercio = " + strIdComercio
        com.Connection = con
        Dim dbAdaptador As MySqlDataAdapter
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        Dim dsTabla As New DataSet
        dbAdaptador.Fill(dsTabla)
        con.Close()
        Return dsTabla
    End Function
    Public Function FncIdQueja() As Integer
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        Dim com As New MySqlCommand
        com.CommandText = "select (coalesce(max(id_queja),0) + 1) IdQueja from db_a674f2_mydb.queja"
        com.Connection = con
        Dim dbAdaptador As MySqlDataAdapter
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        Dim dsTabla As New DataSet
        dbAdaptador.Fill(dsTabla)
        con.Close()
        Return CInt(dsTabla.Tables(0).Rows(0).Item("IdQueja").ToString)
    End Function
    Public Sub IngresaQueja(strIdSucursal As String, strFechaIncidente As String, strSugerencia As String, strQueja As String)
        Try
            '
            Dim ejecutado As Integer
            Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
            Dim con As MySqlConnection
            con = New MySqlConnection(strConString)
            Dim com As New MySqlCommand
            com.CommandText = "insert into db_a674f2_mydb.queja(id_queja,id_sucursal,fecha_incidente,incidente,sugerencia_mejora,fecha_queja) values(" + FncIdQueja().ToString + ", " + strIdSucursal.Trim + " , STR_TO_DATE('" + strFechaIncidente.Trim + "','%d/%m/%Y'),'" + strQueja.Trim + "','" + strSugerencia.Trim + "',now()); COMMIT;"
            com.Connection = con
            con.Open()
            ejecutado = com.ExecuteNonQuery()

            'Dim dbAdaptador As MySqlDataAdapter
            'dbAdaptador = New MySqlDataAdapter()
            'dbAdaptador.InsertCommand = com
            'Dim dsTabla As New DataSet
            'dbAdaptador.Fill(dsTabla)
            con.Close()
        Catch ex As Exception

        End Try
    End Sub

#Region "Login"
    Public Function IniciaSesion(strUser As String, strPass As String) As String
        Dim strPassEncriptado As String
        strPassEncriptado = Encriptar(strPass)
        Return ConsultaUsuario(strUser, strPassEncriptado)
    End Function
#End Region
#Region "Funciones Locales"
    Private Function Encriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
    End Function
    Private Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
    End Function
    Public Function ConsultaUsuario(strUser As String, strPass As String) As String
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim com As New MySqlCommand
        Dim dbAdaptador As MySqlDataAdapter
        Dim dsTabla As New DataSet
        Dim con As MySqlConnection
        Dim strEstadoUser As String
        con = New MySqlConnection(strConString)
        con.Open()
        com.CommandText = "select id_usuario,nombre_usuario,password,estado from db_a674f2_mydb.usuarios where upper(nombre_usuario) = upper('" + strUser.Trim + "') and password = '" + strPass.Trim + "'"
        com.Connection = con
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        dbAdaptador.Fill(dsTabla)
        con.Close()
        If dsTabla.Tables(0).Rows.Count > 0 Then
            strEstadoUser = dsTabla.Tables(0).Rows(0).Item("estado")
            If strEstadoUser = "1" Then
                Return dsTabla.Tables(0).Rows(0).Item("nombre_usuario").ToString
            Else
                Return "0"
            End If
        Else
            If ConsultaUsuarioSinPass(strUser) = "1" Then
                Return "1"
            Else
                Return "-1"
            End If
        End If
        'Return "1"
    End Function
    Public Function ConsultaUsuarioSinPass(strUser As String) As Integer
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim com As New MySqlCommand
        Dim dbAdaptador As MySqlDataAdapter
        Dim dsTabla As New DataSet
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        com.CommandText = "select id_usuario,nombre_usuario,password,estado from db_a674f2_mydb.usuarios where upper(nombre_usuario) = upper('" + strUser.Trim + "')"
        com.Connection = con
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        dbAdaptador.Fill(dsTabla)
        con.Close()
        If dsTabla.Tables(0).Rows.Count > 0 Then
            Return "1"
        Else
            Return "0"
        End If
        'Return "1"
    End Function
#End Region
#Region "Quejas"
    Public Function QuejasDepto(strIdDepto As String) As DataSet
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        Dim com As New MySqlCommand
        com.CommandText = "select d.nombre_departamento,m.municipio,c.nombre_comercio,s.nombre_sucursal,q.fecha_incidente,q.fecha_queja  from db_a674f2_mydb.queja q left join db_a674f2_mydb.sucursal s on  q.id_sucursal = s.id_sucursal left join db_a674f2_mydb.municipios m on s.id_municipio = m.id_municipio  left join db_a674f2_mydb.departamentos d on m.id_departamento = d.id_departamento left join db_a674f2_mydb.comercio c on s.id_comercio = c.id_comercio where d.id_departamento = " + strIdDepto
        com.Connection = con
        Dim dbAdaptador As MySqlDataAdapter
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        Dim dsTabla As New DataSet
        dbAdaptador.Fill(dsTabla)
        con.Close()
        Return dsTabla
    End Function

    Public Function QuejasMunicipio(strMunicipio As String) As DataSet
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        Dim com As New MySqlCommand
        com.CommandText = "select d.nombre_departamento,m.municipio,c.nombre_comercio,s.nombre_sucursal,q.fecha_incidente,q.fecha_queja  from db_a674f2_mydb.queja q left join db_a674f2_mydb.sucursal s on  q.id_sucursal = s.id_sucursal left join db_a674f2_mydb.municipios m on s.id_municipio = m.id_municipio  left join db_a674f2_mydb.departamentos d on m.id_departamento = d.id_departamento left join db_a674f2_mydb.comercio c on s.id_comercio = c.id_comercio where m.id_municipio = " + strMunicipio
        com.Connection = con
        Dim dbAdaptador As MySqlDataAdapter
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        Dim dsTabla As New DataSet
        dbAdaptador.Fill(dsTabla)
        con.Close()
        Return dsTabla
    End Function

    Public Function QuejasRegion(strRegion As String) As DataSet
        Dim strConString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim con As MySqlConnection
        con = New MySqlConnection(strConString)
        con.Open()
        Dim com As New MySqlCommand
        com.CommandText = "select d.nombre_departamento,m.municipio,c.nombre_comercio,s.nombre_sucursal,q.fecha_incidente,q.fecha_queja  from db_a674f2_mydb.queja q left join db_a674f2_mydb.sucursal s on  q.id_sucursal = s.id_sucursal left join db_a674f2_mydb.municipios m on s.id_municipio = m.id_municipio  left join db_a674f2_mydb.departamentos d on m.id_departamento = d.id_departamento left join db_a674f2_mydb.comercio c on s.id_comercio = c.id_comercio where d.id_region = " + strRegion
        com.Connection = con
        Dim dbAdaptador As MySqlDataAdapter
        dbAdaptador = New MySqlDataAdapter()
        dbAdaptador.SelectCommand = com
        Dim dsTabla As New DataSet
        dbAdaptador.Fill(dsTabla)
        con.Close()
        Return dsTabla
    End Function

#End Region
End Class
