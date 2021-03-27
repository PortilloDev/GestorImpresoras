Imports System.Drawing.Printing
Imports System.IO


Module Impresion_Documentos
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal pszPrinter As String) As Boolean

    Public Function imprimirImpresoraNormal(path As String)
        ImpresoraPapelDefault()

        Using p As New Process

            p.StartInfo.FileName = path
            p.StartInfo.Verb = "PrintTo"
            p.StartInfo.Arguments = Chr(34) & impresoraNormal & Chr(34)
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            p.Start()
            p.WaitForExit(5000)
            'p.CloseMainWindow()
            p.Kill()
            p.Close()

        End Using

        PDFCreatorDefault()
    End Function

    Public Function imprimirPorZebra(path As String)
        ZebraPapelDefault()

        Using p As New Process

            p.StartInfo.FileName = path
            p.StartInfo.Verb = "PrintTo"
            p.StartInfo.Arguments = Chr(34) & impresoraZebra & Chr(34)
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            p.Start()
            p.WaitForExit(6000)
            'p.CloseMainWindow()
            p.Kill()
            p.Close()

        End Using

        PDFCreatorDefault()
    End Function

    Private Function PDFCreatorDefault()
        SetDefaultPrinter("Microsoft Print to PDF")
    End Function

    Private Function ZebraDefault()
        SetDefaultPrinter("Microsoft Print to PDF")
    End Function

    Private Function ImpresoraPapelDefault()
        SetDefaultPrinter(impresoraNormal)
    End Function

    Private Function ZebraPapelDefault()
        SetDefaultPrinter(impresoraZebra)
    End Function

    Public Function setSeleccionarZebra(nombreZebra)
        impresoraZebra = nombreZebra
    End Function

    Public Function setSeleccionarImpresoraNormal(nombreImpresora)
        impresoraNormal = nombreImpresora
    End Function

End Module
