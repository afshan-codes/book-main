Imports System.Data.SqlClient
Public Class Dashboard
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= C: \Users\afshan barlaskar\Documents\Bookshopvbdb.mdf;Integrated Security=True;Connect Timeout=30")

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim BookNum As Integer
        Con.Open()
        Dim sql = "select COUNT (*) from BookTbl "
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        BookNum = cmd.ExecuteScalar
        BookLbl.Text = BookNum
        Con.Close()

    End Sub
End Class