Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Net.Mime.MediaTypeNames
Imports System.Security.Cryptography

Public Class users
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= C: \Users\afshan barlaskar\Documents\Bookshopvbdb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Populate()
        Con.Open()
        Dim query = "select * from UserTbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        UserDGV.DataSource = ds.Tables(0)

        Con.Close()


    End Sub
    Private Sub Reset()
        UnameTb.Text = ""
        PhoneTb.Text = ""
        AddressTb.Text = ""
        PasswordTb.Text = ""
        Key = 0
    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles PasswordTb.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If UnameTb.Text = "" Or PhoneTb.Text = "" Or AddressTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("MISSSING INFORMATION")
        Else
            Con.Open()
            Dim query As String
            query = "insert into UserTbl values ('" & UnameTb.Text & " ','" & PhoneTb.Text & " ',' " & AddressTb.Text & "',' " & PasswordTb.Text & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("USER SAVED SUCCESSFULLY")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles UserDGV.CellContentClick

    End Sub

    Private Sub users_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Populate()

    End Sub
    Dim Key = 0

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If Key = 0 Then
            MsgBox("SELECT THE USER TO BE DELETED")
        Else
            Con.Open()
            Dim query As String
            query = "delete from UserTbl where id=" & Key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("USER DELETED SUCCESSFULLY")
            Con.Close()
            Populate()
            Reset()

        End If
    End Sub

    Private Sub UserDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles UserDGV.CellMouseClick
        Dim row As DataGridViewRow = UserDGV.Rows(e.RowIndex)
        UnameTb.Text = row.Cells(1).Value.ToString
        PhoneTb.Text = row.Cells(2).Value.ToString
        AddressTb.Text = row.Cells(3).Value.ToString
        PasswordTb.Text = row.Cells(4).Value.ToString
        If UnameTb.Text = "" Then
            Key = 0
        Else
            Key = Convert.ToInt32(row.Cells(0).Value.ToString)

        End If
    End Sub

    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()

    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If UnameTb.Text = "" Or PhoneTb.Text = "" Or AddressTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("MISSSING INFORMATION")
        Else
            Con.Open()
            Dim query As String
            query = "Upadate UserTbl set name='" & UnameTb.Text & "',Phone='" & PhoneTb.Text & "',Address='" & AddressTb.Text & "',Password='" & PasswordTb.Text & "' where Id=" & Key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("USER UPDATED SUCCESSFULLY")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click


    End Sub
End Class