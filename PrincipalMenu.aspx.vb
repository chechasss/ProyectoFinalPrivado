Public Class PrincipalMenu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("Usuario") Is Nothing Then
            Session("Usuario") = "OUT"
            Response.AddHeader("REFRESH", "1;URL=CierraSesion.aspx")
        Else
            If Session("Usuario") = "OUT" Then
                Response.AddHeader("REFRESH", "1;URL=CierraSesion.aspx")
            End If
        End If

    End Sub

End Class