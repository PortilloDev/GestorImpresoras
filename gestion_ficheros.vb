Imports System.IO

Module gestion_ficheros
    Public Function copiarArchivo(nombreDocumentos As String)
        Dim newPathAlbaran As String = albaranesPath & nombreDocumentos
        My.Computer.FileSystem.CopyFile(origenPath & nombreDocumentos, newPathAlbaran, True)
        Form1.TextBox1.Text += "Albarán " & nombreDocumentos & " copiado en carpeta Albarán - " & Now + vbNewLine
        imprimirImpresoraNormal(newPathAlbaran)
        Form1.TextBox1.Text += "Albarán " & nombreDocumentos & " imprimido por Impresora - " & Now + vbNewLine
        Return True
    End Function

    Public Function eliminarFicheroOrigen(nombreDocumentos)

        My.Computer.FileSystem.DeleteFile(origenPath & nombreDocumentos)
        Return True

    End Function
End Module
