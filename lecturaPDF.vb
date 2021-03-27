Imports iText.Kernel.Pdf
Imports iTextSharp.text.pdf
Imports System.Text
Imports System.IO
Module LecturaPDF


    Public leidoAlbaran, leidoEtiqueta, leidotarea As Boolean

    Public Function leerContenidoPdf()


        Dim its As New iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy
        Dim nombreAlbaran As String = ""
        Dim ContadorDeArchivos As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
        Dim tipoDocumento As String

        'nos devuelve la cantidad de archivos  
        ContadorDeArchivos = My.Computer.FileSystem.GetFiles(origenPath)

        setVariablesValidacionLecturasDocumentos()



        Try


            For Each archivos As String In Directory.GetFiles(origenPath, "*.pdf", SearchOption.AllDirectories)
                Dim contenidoPDF As String = ""
                ' extraer el nombre del fichero  
                archivos = archivos.Substring(archivos.LastIndexOf("\") + 1).ToString


                'guarda contenido pdf en una cadena
                Dim oReader As New iTextSharp.text.pdf.PdfReader(origenPath & archivos)
                contenidoPDF = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(oReader, 1, its)
                oReader.Close()

                'Limpiar cadena de espacios, salto carros y lineas
                contenidoPDF = contenidoPDF.Replace(" ", "")
                contenidoPDF = Replace(Replace(contenidoPDF, Chr(10), ""), Chr(13), "")
                tipoDocumento = LecturaPDF.reconoceTipoDocumento(contenidoPDF)

                Select Case tipoDocumento

                    Case "albaran"

                        nombreAlbaran = LecturaPDF.obtenerNombreAlbaran(contenidoPDF)
                        LecturaPDF.remplazarNombreAlbaran(archivos, nombreAlbaran)
                        archivos = nombreAlbaran & ".pdf"
                        Form1.TextBox1.Text += "Albarán " & archivos & " renombrado correctamente " & Now + vbNewLine

                        copiarArchivo(archivos)
                        Form1.TextBox1.Text += "Albarán " & archivos & " detectado como albáran -  " & Now + vbNewLine


                    Case "etiqueta"

                        imprimirImpresoraNormal(origenPath & archivos)
                        Form1.TextBox1.Text += "Etiqueta " & archivos & " imprimido por Impresora - " & Now + vbNewLine


                    Case "tareas"

                        imprimirImpresoraNormal(origenPath & archivos)
                        Form1.TextBox1.Text += "Tareas " & archivos & " imprimido por Impresora - " & Now + vbNewLine
                    Case ""


                        Form1.TextBox1.Text += "El documento " & archivos & " no es pdf valido" & Now + vbNewLine

                    Case Else

                        Form1.TextBox1.Text += "No es albarán, etiqueta o tarea" & Now + vbNewLine

                End Select

                eliminarFicheroOrigen(archivos)
                Form1.TextBox1.Text += tipoDocumento & archivos & " imprimido por Impresora - " & Now + vbNewLine
                contenidoPDF = vbNullString
            Next





            ' errores  
        Catch ex As Exception
            Form1.TextBox1.Text += "ERROR ->" & ex.Message.ToString & " - " & Now + vbNewLine
        End Try
    End Function

    Private Function remplazarNombreAlbaran(nombreActual As String, nombreAlbaran As String)

        My.Computer.FileSystem.RenameFile(origenPath & nombreActual, nombreAlbaran & ".pdf")
    End Function

    Public Function setVariablesValidacionLecturasDocumentos()
        leidoAlbaran = False
        leidoEtiqueta = False
        leidotarea = False
    End Function


    Private Function obtenerNombreAlbaran(contenido As String)
        Dim posicionCadenaAlbaran As Integer
        Dim cadenaAlbaran As String = "Albarán/Posición:"
        Dim nombreAlbaran As String

        posicionCadenaAlbaran = InStr(contenido, cadenaAlbaran)
        nombreAlbaran = Mid(contenido, posicionCadenaAlbaran + 17, 7)

        Return nombreAlbaran

    End Function

    Private Function reconoceTipoDocumento(contenidoDocumento As String)


        If contenidoDocumento.Contains("Albarán/Posición:") = True And leidoAlbaran = False Then
            leidoAlbaran = True
            Return "albaran"
        End If

        If contenidoDocumento.Contains("INSTRUCCIONESDEINSPECCIÓN") = True And leidotarea = False Then
            leidotarea = True
            Return "tareas"
        End If

        If contenidoDocumento.Contains("FECHA/SELLO") = True And leidoEtiqueta = False Then
            leidoEtiqueta = True
            Return "etiqueta"
        End If

        Return ""
    End Function
End Module
