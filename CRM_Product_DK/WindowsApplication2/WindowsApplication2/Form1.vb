Imports CRMmanager.CRMmanager

Public Class Form1

    Private ss As New CRMmanager.CRMmanager
    'Private WithEvents trans_call As New CRMmanager.CRMmanager

    Private WithEvents kk As New CRMmanager.CRMmanager

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ss.SetUserInfo(tbCompany1.Text, tbUserId1.Text, tbPwd1.Text, "127.0.0.1", "8886", True)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ss.SetUserInfo(tbCompany2.Text, tbUserId2.Text, tbPwd2.Text, "127.0.0.1", "8886", True)
    End Sub



    'Private Sub trans_call_Call_Trans(ByVal telno As String) Handles trans_call.Call_Trans
    '    MessageBox.Show("전달받은 전화번호는 : " & telno)
    'End Sub

    'Private Sub kk_Call_Trans(ByVal telno As String) Handles kk.Call_Trans
    '    MessageBox.Show("dll --> telno:" & telno)

    'End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim tm As String = Format(Now, "yyyyMMddHHmmss")
        If rbInBound.Checked Then
            ss.POPUP(TextBox1.Text.Trim, tm, "1")
        Else
            ss.POPUP(TextBox1.Text.Trim, tm, "2")
        End If
    End Sub
End Class
