﻿Public Class inicio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("Usuario") Is Nothing Then
            Session("Usuario") = "OUT"
        Else
            If Session("Usuario") <> "OUT" Then
                Session("Usuario") = "OUT"
            End If
        End If
    End Sub

End Class