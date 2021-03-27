Imports iText.Kernel.Pdf
Imports iTextSharp.text.pdf
Imports System.Text
Imports System.IO
Imports System.Drawing.Printing
Imports System.Printing


Public Class Form1


    Private Sub AbrirPDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPDFToolStripMenuItem.Click

        'OpenFileDialog1.ShowDialog()
        'AxAcroPDF1.src = OpenFileDialog1.FileName

        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox1.Clear()

            ' Dim lector As New iText.Kernel.Pdf.PdfReader(OpenFileDialog1.FileName)
            ' Dim paginas As Integer = lector.NumberOfPages
            Dim oReader As New iTextSharp.text.pdf.PdfReader(OpenFileDialog1.FileName)
            Dim its As New iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy
            Dim contenidoPDF As String

            contenidoPDF = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(oReader, 1, its)
            If contenidoPDF = "" Then
                MsgBox("No es un pdf valido para leer")
                TextBox1.Clear()
            End If
            If contenidoPDF.Contains("Albarán/Posición:") = True Then
                MsgBox("Es albarán")
            Else
                MsgBox("No es albarán")
            End If


            contenidoPDF = contenidoPDF.Replace(" ", "")

            TextBox1.Text = contenidoPDF
            oReader.Close()

        End If

    End Sub

    Private Sub CerrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CerrarToolStripMenuItem.Click
        MsgBox("Lector de PDF´s Creado por Sistemas")
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ContadorDeArchivos As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
        ContadorDeArchivos = My.Computer.FileSystem.GetFiles(origenPath)


        'nos devuelve la cantidad de archivos  
        If ContadorDeArchivos.Count = 0 Then
            MsgBox("No existe archivos en la carpeta origen")
        Else
            If impresoraNormal = "" Then
                MsgBox("No tiene ninguna impresora selccionada. Seleccione una ")
            Else
                leerContenidoPdf()
                MsgBox("Proceso impresión finalizado")

            End If
        End If

    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ListBox1.Items.Clear()
        For I = 0 To PrinterSettings.InstalledPrinters.Count - 1
            ListBox1.Items.Add(PrinterSettings.InstalledPrinters.Item(I))
        Next


    End Sub




    Private Sub printImpresora_Click(sender As Object, e As EventArgs) Handles printImpresora.Click
        impresoraNormal = ListBox1.SelectedItem.ToString()
        Impresion_Documentos.setSeleccionarImpresoraNormal(impresoraNormal)
        TextBox3.Text = impresoraNormal

    End Sub
End Class
