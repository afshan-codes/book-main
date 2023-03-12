Imports System.Data.SqlClient

Public Class Books
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= C: \Users\afshan barlaskar\Documents\Bookshopvbdb.mdf;Integrated Security=True;Connect Timeout=30")
    Public Property Key As Integer

    Private Sub Populate()
        Con.Open()
        Dim query = "select * from BooksTbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        BookdDGV.DataSource = ds.Tables(0)

        Con.Close()


    End Sub
    Private Sub Filter()
        Con.Open()
        Dim query = "select * from BooksTbl where Category='" & CatCb.SelectedItem.ToString & "'"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        BookdDGV.DataSource = ds.Tables(0)

        Con.Close()
    End Sub
    Private Sub Reset()
        BookNameTb.Text = ""
        QtyTb.Text = ""
        PriceTb.Text = ""
        AuthorTb.Text = ""
        CatCb.SelectedIndex = -1
        Key = 0
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles BookNameTb.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub



    Private Sub SaveBtn1_Click(sender As Object, e As EventArgs) Handles SaveBtn1.Click
        If BookNameTb.Text = "" Or AuthorTb.Text = "" Or QtyTb.Text = "" Or PriceTb.Text = "" Or CatCb.SelectedIndex = -1 Then
            MsgBox("MISSSING INFORMATION")
        Else
            Con.Open()
            Dim query As String
            query = "Upadate BookTbl set Title= ('" & BookNameTb.Text & " ','" & AuthorTb.Text & " ','" & CatCb.SelectedItem.ToString & " ',' " & QtyTb.Text & "',' " & PriceTb.Text & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("BOOK SAVED SUCCESSFULLY")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub BookdDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles BookdDGV.CellMouseClick
        Dim row As DataGridViewRow = BookdDGV.Rows(e.RowIndex)
        BookNameTb.Text = row.Cells(1).Value.ToString
        AuthorTb.Text = row.Cells(2).Value.ToString
        QtyTb.Text = row.Cells(3).Value.ToString
        CatCb.Text = row.Cells(4).Value.ToString
        PriceTb.Text = row.Cells(5).Value.ToString
        If BookNameTb.Text = "" Then
            Key = 0
        Else
            Key = Convert.ToInt32(row.Cells(0).Value.ToString)

        End If
    End Sub

    Private Sub Books_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Populate()

    End Sub

    Private Sub ResetBtn1_Click(sender As Object, e As EventArgs) Handles ResetBtn1.Click
        Reset()

    End Sub

    Private Sub DeleteBtn1_Click(sender As Object, e As EventArgs) Handles DeleteBtn1.Click
        If Key = 0 Then
            MsgBox("Select THE BOOK To BE DELETED")
        Else
            Con.Open()
            Dim query As String
            query = "delete from BookTbl where BId=" & Key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Book DELETED SUCCESSFULLY")
            Con.Close()
            Populate()
            Reset()

        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()

    End Sub

    Private Sub EditBtn1_Click(sender As Object, e As EventArgs) Handles EditBtn1.Click
        If BookNameTb.Text = "" Or AuthorTb.Text = "" Or QtyTb.Text = "" Or PriceTb.Text = "" Or CatCb.SelectedIndex = -1 Then
            MsgBox("MISSSING INFORMATION")
        Else
            Con.Open()
            Dim query As String
            query = "Upadate BookTbl set Title ='" & BookNameTb.Text & "',Author='" & AuthorTb.Text & "',Quantity='" & QtyTb.Text & "',Price='" & PriceTb.Text & "',Category='" & FilterCb.SelectedItem.ToString() & " where BId=" & Key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("BOOK UPDATED SUCCESSFULLY")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub ComboBox2_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles FilterCb.SelectionChangeCommitted
        Filter()

    End Sub

    Private Sub RefreshBtn_Click(sender As Object, e As EventArgs) Handles RefreshBtn.Click
        Populate()

    End Sub
End Class