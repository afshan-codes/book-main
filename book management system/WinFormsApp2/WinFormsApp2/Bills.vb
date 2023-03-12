Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Bills
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= C: \Users\afshan barlaskar\Documents\Bookshopvbdb.mdf;Integrated Security=True;Connect Timeout=30")
    Public Property UserName As String
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
    Private Sub Bills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Populate()
        UnameLbl1.Text = UserName
    End Sub
    Dim key = 0, Stock = 0, i = 0, GrdTotal = 0

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub
    Private Sub Reset()
        key = 0
        QtyTb.Text = ""
        ClientNameTb.Text = ""
        BnameTb.Text = ""
    End Sub

    Private Sub BVC_Click(sender As Object, e As EventArgs) Handles BVC.Click
        Reset()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString("Bookshop", New Font("Century Gothic", 25), Brushes.MidnightBlue, 350, 40)
        e.Graphics.DrawString("=====Your Bill=====", New Font("Century Gothic", 16), Brushes.MidnightBlue, 300, 70)
        Dim bm As New Bitmap(Me.BillDGV.Width, Me.BillDGV.Height)
        BillDGV.DrawToBitmap(bm, New Rectangle(0, 0, Me.BillDGV.Width, Me.BillDGV.Height))
        e.Graphics.DrawImage(bm, 60, 120)
        e.Graphics.DrawString("Total Amount Rs" + GrdTotal.ToString, New Font("Century Gothic", 15), Brushes.MidnightBlue, 280, 500)
        e.Graphics.DrawString("==========Thanks For Buying in our Shop==========", New Font("Century Gothic", 15), Brushes.Crimson, 150, 580)

    End Sub

    Private Sub BVC1_Click(sender As Object, e As EventArgs) Handles BVC1.Click
        PrintPreviewDialog1.Show()
        AddBill()


    End Sub

    Private Sub AddBill()
        Try
            Con.Open()
            Dim query As String
            query = "Upadate BillTbl set Title= ('" & UnameLbl1.Text & " ','" & ClientNameTb.Text & " ',' " & GrdTotal & ")"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Overloads Sub UpdateBook()
        Dim NewQty = Stock - Convert.ToInt32(QtyTb.Text)
        Con.Open()
        Dim query As String
        query = "Update BookTbl set Quantity=" & NewQty & " where BId=" & key & ""
        Dim cmd As SqlCommand
        cmd = New SqlCommand(query, Con)
        cmd.ExecuteNonQuery()
        MsgBox("BOOK UPDATED SUCCESSFULLY")
        Con.Close()
        Populate()
    End Sub

    Private Sub AddToBillBtn_Click(sender As Object, e As EventArgs) Handles AddToBillBtn.Click
        If PriceTb.Text = "" Or QtyTb.Text = "" Then
            MsgBox("enter the Quantity")
        ElseIf BnameTb.Text = "" Then
            MsgBox("Select the box")
        ElseIf Convert.ToInt32(QtyTb.Text) > Stock Then
            MsgBox("Not Enough Stock")

        Else
            Dim rnum As Integer = BillDGV.Rows.Add()
            i = i + 1
            Dim total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PriceTb.Text)
            BillDGV.Rows.Item(rnum).Cells("Column1").Value = i
            BillDGV.Rows.Item(rnum).Cells("Column2").Value = BnameTb.Text
            BillDGV.Rows.Item(rnum).Cells("Column3").Value = PriceTb.Text
            BillDGV.Rows.Item(rnum).Cells("Column4").Value = QtyTb.Text
            BillDGV.Rows.Item(rnum).Cells("Column5").Value = total
            GrdTotal = GrdTotal + total
            Dim Tot As String
            Tot = "Rs" + Convert.ToString(GrdTotal)
            TotalLbl.Text = Tot
            UpdateBook()

        End If
    End Sub

    Private Sub BookdDGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BookdDGV.CellContentClick
        Dim row As DataGridViewRow = BookdDGV.Rows(e.RowIndex)
        BnameTb.Text = row.Cells(1).Value.ToString
        PriceTb.Text = row.Cells(2).Value.ToString
        Stock = Convert.ToInt32(row.Cells(5).Value.ToString)
        If BnameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)

        End If
    End Sub
End Class