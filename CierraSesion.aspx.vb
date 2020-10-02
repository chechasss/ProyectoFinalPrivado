Public Class CierraSesion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("Usuario") IsNot Nothing Then
            If Session("Usuario") <> "OUT" Then
                Session("Usuario") = "OUT"
            End If
        End If
        Response.AddHeader("REFRESH", "5;URL=inicio.aspx")
    End Sub

End Class