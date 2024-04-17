Imports System.Data.SqlClient

Public Class Form1

    Private DataAdapter As SqlDataAdapter
    Private Dataset As DataSet
    Private IposicionfilaActual As Integer
    Dim cnx As SqlConnection


    Private Sub Habilirar(ByVal sw As Boolean)
        txtCarnet.Enabled = sw
        txtNombres.Enabled = sw
        txtApellidos.Enabled = sw
        txtEmail.Enabled = sw
        txtTelefono.Enabled = sw
        txtDireccion.Enabled = sw
        txtBuscar.Enabled = sw

        txtCarnet.Clear()
        txtNombres.Clear()
        txtApellidos.Clear()
        txtEmail.Clear()
        txtTelefono.Clear()
        txtDireccion.Clear()
        txtBuscar.Clear()
    End Sub

    Private Sub bntMostrar_Click(sender As Object, e As EventArgs) Handles bntMostrar.Click
        cnx = New SqlConnection
        cnx.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=BaseDeDatos;Integrated Security=True"
        Dim Command As New SqlCommand("select * from Datos Where Carnet like '" + Trim(txtCarnet.Text) + "' ", cnx)


        Dim DataReader As SqlDataReader
        cnx.Open()
        DataReader = Command.ExecuteReader

        If DataReader.HasRows = False Then
            MsgBox("El usuario no existe")
            DataReader.Close()
            cnx.Close()
            Return
        End If


        While DataReader.Read
            txtNombres.AppendText(Trim(DataReader("Nombres")))
            txtApellidos.AppendText(Trim(DataReader("Apellidos")))
            txtEmail.AppendText(Trim(DataReader("Email")))
            txtTelefono.AppendText(Trim(DataReader("Telefono")))
            txtDireccion.AppendText(Trim(DataReader("Direccion")))
        End While



        DataReader.Close()
        cnx.Close()

        'Dim Command As New SqlCommand("exec Buscar '" + Trim(txtCarnet.Text) + "' ", cnx) Para procedimiento almacenado

        'dataGrid_inactivos.DataSource = bindingSource1;  //Asigna a la tabla DataGridView la conexión de la base de datos para mostrar el histórico de empresas inactivas
        'GetData("select * from Empresas Where Estado like'" + "INACTIVO" + "'"); // Consulta para mostrar los datos del histórico. Consulta "GetData" para conocer los parámetros de la conexión
        'dataGrid_inactivos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

        'Private void GetData(String selectCommand)  //Procedimiento con los parámetros de conexión para traer los datos y mostrarlos en la tabla del formulario
        '{
        '    Try
        '    {
        '        String ConnectionStringhistorico = "Data Source=.\\SQLExpress;Persist Security Info=False;User ID=sa;Password=admin;Initial Catalog=DataTributarios;Pooling=False";  //Coneción de la base de datos. Explicada anteriormente
        '        dataAdapter = New SqlDataAdapter(selectCommand, ConnectionStringhistorico);
        '        SqlCommandBuilder commandBuilder = New SqlCommandBuilder(dataAdapter);
        '        //Pooling=False, permite que no se creen piscinas que guarden la conexión, donde se consulten lal próximas veces
        '        // Llena una tabla de datos nueva y la asocia a BindingSource.
        '        DataTable table = New DataTable();  //Crea un objeto nuevo tipo tabla
        '        table.Locale = System.Globalization.CultureInfo.InvariantCulture;  //localiza la tabla
        '        dataAdapter.Fill(table); // LLeva la tabla creada al comando dataAdapter
        '        bindingSource1.DataSource = table; //asigna a la tabla la fuente de datos de la DB
        '        //dataGrid_inactivos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader); // Cambiar el tamaño de las columnas DataGridView para ajustar el contenido recién cargado.
        '    }
        '    Catch (SqlException)
        '    {
        '        MessageBox.Show("El valor de la cadena ConnectionString " +
        '            "No es válido, 'Query, cargar empresas inactivas'", "Atención");
        '    }
        '}
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Trim(txtNombres.Text = "" Or txtCarnet.Text = "") Then MsgBox("Faltan Datos") : Exit Sub

        cnx = New SqlConnection
        cnx.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=BaseDeDatos;Integrated Security=True"
        DataAdapter = New SqlDataAdapter("insert into dbo.Datos values('" + Trim(txtCarnet.Text) + "',
        '" + Trim(txtNombres.Text) + "', '" + Trim(txtApellidos.Text) + "', '" + Trim(txtEmail.Text) + "',
        '" + Trim(txtTelefono.Text) + "', '" + Trim(txtDireccion.Text) + "')", cnx)

        Dim cmd As SqlCommandBuilder = New SqlCommandBuilder(DataAdapter)
        Dataset = New DataSet
        Try
            DataAdapter.Fill(Dataset, "Datos")
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

        MsgBox("Guardado con Exito") : Habilirar(False)

    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Habilirar(True)
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        cnx = New SqlConnection
        cnx.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=BaseDeDatos;Integrated Security=True"
        'Dim Command As New SqlCommand("select * from Datos Where Carnet like '" + Trim(txtCarnet.Text) + "' ", cnx)
        DataAdapter = New SqlDataAdapter("select * from dbo.Datos where Carnet= '" + Trim(txtBuscar.Text) + "' ", cnx)

        'Dim cmd As SqlCommandBuilder = New SqlCommandBuilder(DataAdapter)
        Dataset = New DataSet
        cnx.Open()
        DataAdapter.Fill(Dataset, "Datos")
        cnx.Close()
        tabla.DataSource = Dataset
        tabla.DataMember = "Datos"

        tabla.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader)

    End Sub

    Private Sub bntEditar_Click(sender As Object, e As EventArgs) Handles bntEditar.Click
        Me.DataAdapter.Update(Dataset, "Datos")
    End Sub

    Private Sub txtCarnet_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCarnet.KeyDown
        If (e.KeyCode = Keys.Enter) Then

            bntMostrar.PerformClick()
        End If
        Exit Sub
    End Sub

    Private Sub txtCarnet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCarnet.KeyPress

        If Not IsNumeric(e.KeyChar) And e.KeyChar <> vbBack And e.KeyChar <> Chr(13) Then
            e.Handled = True
        End If

        ''Solo Texto
        '   Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        '       If Not Char.IsDigit(e.KeyChar) Then
        '           e.Handled = False
        '       ElseIf Char.IsControl(e.KeyChar) Then
        '           e.Handled = False
        '       Else
        '           e.Handled = True
        '       End If
        '   End Sub
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        End
    End Sub

    Private Sub btnLlenar_listbox_Click(sender As Object, e As EventArgs) Handles btnLlenar_listbox.Click
        Chk_istBox1.Items.Clear()
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=BaseDeDatos;Integrated Security=True"
            Dim Command As New SqlCommand("select Nombres from Datos", cnx)

            Dim DataReader As SqlDataReader
            cnx.Open()
            DataReader = Command.ExecuteReader

            While DataReader.Read
                Chk_istBox1.Items.Add(Trim(DataReader("Nombres")))
            End While
            DataReader.Close()
            cnx.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'If (ListBox_Impuestos.CheckedItems.Count > 0) // Verifica Then si hay impuestos agregados y seleccionados en la lista
        '    {
        '        For (i = 0; i < ListBox_Impuestos.CheckedItems.Count; i++) // Inicia un ciclo que recorre todos los impuestos agregados a la lista, de tal manera que se evaluen y se
        '        {                                                      
        '            Try
        '            {
        '            //almacenen uno a uno en la base de datos
        '            varimpuesto = ListBox_Impuestos.CheckedItems[i].ToString(); //Almacena en una variable el nombre del impuesto encontrado en cada vuelta del ciclo 'for'
        '            OleDbCommand commandImpuesto = New OleDbCommand("Insert into Impuestos_Empresas(Empresa,Nit_empresa,Impuesto) Values('" + txtempresa.Text + "','" + txtnit_empresa.Text + "','" + varimpuesto + "');", micnx);
        '            commandImpuesto.ExecuteNonQuery();  //Ejecuta la orden command para insertar los datos.

        '            }
        '            Catch (Exception EX) // Evita salirse del programa, si no puede abrir la DB o si encuentra errores de acceso.
        '            { 
        '                If (EX.HResult == -2147217833) Then
        '                                        {
        '                    MessageBox.Show("Ha escrito un campo demasiado largo," + "\r" + "revise donde debe reducir la cantidad de caracteres", "Error de escritura del usuario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        '                    micnx.Close(); //Cerrar conexión
        '                    Return;
        '                }

        '                MessageBox.Show(EX.Message, "Error de acceso DataBase,  'Query Insert impuestos', proceso actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        '                micnx.Close(); //Cerrar conexión
        '                Return;
        '            }
        '        }
        '    }

    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If (txtCarnet.Text = "") Then
            MsgBox("El campo text está vacio")
            Return
        End If

        Try
            cnx = New SqlConnection
            cnx.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=BaseDeDatos;Integrated Security=True"
            Dim Command As New SqlCommand("update Datos SET Nombres= '" + txtNombres.Text + "',Apellidos= '" + txtApellidos.Text + "',Email= '" + txtEmail.Text + "',Telefono= '" + txtTelefono.Text + "', Direccion= '" + txtDireccion.Text + "' ", cnx)
            cnx.Open()

            Dim res As Integer = Command.ExecuteNonQuery()

            If (res > 0) Then
                MsgBox("usuario actualizado con exito")
            Else
                MsgBox("usuario no encontrado")
            End If

            cnx.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If (txtCarnet.Text = "") Then
            MsgBox("El campo text está vacio")
            Return
        End If

        Try
            cnx = New SqlConnection
            cnx.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=BaseDeDatos;Integrated Security=True"
            Dim Command As New SqlCommand("delete from Datos where Carnet= '" + txtCarnet.Text + "' ", cnx)
            cnx.Open()

            Dim res As Integer = Command.ExecuteNonQuery()

            If (res > 0) Then
                MsgBox("usuario borrado con exito")
            Else
                MsgBox("usuario no encontrado")
            End If

            cnx.Close()

        Catch ex As Exception
        MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnProbar_Click(sender As Object, e As EventArgs) Handles btnProbar.Click
        MsgBox("La fecha es: " + DateTimePicker1.Value.Date.ToString("dd/MM/yyyy"))
    End Sub

    Private Sub tabla_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles tabla.RowHeaderMouseClick
        txtCarnet.Text = tabla.Rows(e.RowIndex).Cells(0).Value
        'Dim rowIndex As Integer = e.RowIndex
        'txtCarnet.Text = tabla.Rows(rowIndex).Cells(0).Value
        'MessageBox.Show(txtCarnet.Text)

    End Sub

    Private Sub btnMostrarGrid_Click(sender As Object, e As EventArgs) Handles btnMostrarGrid.Click
        cnx = New SqlConnection
        cnx.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=BaseDeDatos;Integrated Security=True"
        'Dim Command As New SqlCommand("select * from Datos Where Carnet like '" + Trim(txtCarnet.Text) + "' ", cnx)
        DataAdapter = New SqlDataAdapter("select * from dbo.Datos", cnx) 'Se utiliza SqlDataAdapter porque esta se utilizan para rellenar un DataSet o un datatable para mostrar los datos en un DataGridView

        'Dim cmd As SqlCommandBuilder = New SqlCommandBuilder(DataAdapter)
        'El DataSet es una representación de datos residente en memoria. Un DataSet contiene colecciones de DataTables y DataRelations.
        'El DataTable permite representar una determinada tabla en memoria, de modo que podamos interactuar con ella.
        Dataset = New DataSet
        'cnx.Open()
        DataAdapter.Fill(Dataset, "Datos")
        'cnx.Close()
        tabla.DataSource = Dataset
        tabla.DataMember = "Datos"

        tabla.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
