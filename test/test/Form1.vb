Imports System.Data.SqlClient

Public Class Form1
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim datanum As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\YuYu\Desktop\app\test\test\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        show_data()
    End Sub



    Public Sub show_data()
        Try
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from infotable"
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""

        End Try
    End Sub


    Private Sub update_btn_Click(sender As Object, e As EventArgs) Handles update_btn.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        '使用前端欄位變數 需要雙括號
        cmd.CommandText = "
        update infotable set name='" & TextBox1.Text & "',uid='" & TextBox2.Text & "',city='" & TextBox3.Text & "' where Id='" & TextBox4.Text & "'
        "
        cmd.ExecuteNonQuery()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        show_data()
    End Sub

    Private Sub display_Click_1(sender As Object, e As EventArgs) Handles display.Click
        show_data()
    End Sub

    Private Sub del_Click(sender As Object, e As EventArgs) Handles del.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        '使用前端欄位變數 需要雙括號
        cmd.CommandText = "delete from infotable where Id ='" & TextBox4.Text & "'"
        cmd.ExecuteNonQuery()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""

        show_data()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        Try

            datanum = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())
            'MessageBox.Show(datanum)

            '連接
            cmd = con.CreateCommand()
            '語法text
            cmd.CommandType = CommandType.Text
            'sql
            cmd.CommandText = "select * from infotable where id =" & datanum
            '執行
            cmd.ExecuteNonQuery()

            '??????
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)

            'dr = sql資料群
            Dim dr As SqlClient.SqlDataReader
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While dr.Read
                TextBox1.Text = dr.GetString(1).ToString()
                TextBox2.Text = dr.GetString(2).ToString()
                TextBox3.Text = dr.GetString(3).ToString()
                TextBox4.Text = dr.GetInt32(0)
            End While
        Catch ex As Exception

        End Try
    End Sub

    Private Sub add_btn_Click(sender As Object, e As EventArgs) Handles add_btn.Click
        Try
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into infotable  ([name],[uid],[city]) VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
            cmd.ExecuteNonQuery()

            show_data()
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            MessageBox.Show("done")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "
        select * from infotable where name like '%" & TextBox5.Text & "'
        union 
        select * from infotable where name like '" & TextBox5.Text & "%'
        union 
        select * from infotable where city like '%" & TextBox5.Text & "'
        union 
        select * from infotable where city like '" & TextBox5.Text & "%'
        "

        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub
End Class
