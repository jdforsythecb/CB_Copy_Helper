Public Class QuickOpenPNGFont
    '' church budget
    Private Const CBDW As String = "DoubleWide_CB"
    Private Const CBBK As String = "Booklet"
    Private Const CBUV As String = "BookletCover"
    Private Const CBCN As String = "Carton_CB"
    Private Const CBTN As String = "Num10_CB"
    Private Const CBKY As String = "Kyocera_CB"

    '' monthly mail
    Private Const MMDW As String = "DoubleWide_MM"
    Private Const MMCN As String = "Carton_MM"
    Private Const MMTN As String = "Num10_MM"
    Private Const MMRT As String = "Return_MM"
    Private Const MMKY As String = "Kyocera_MM"

    Private pngfntpath As String = ""
    Private folder As String = ""
    Private cmp As CompanyTypes
    Private jb As JobTypes


    '' job types
    Public Enum JobTypes As Integer
        DoubleWide = 1
        Kyocera = 2
        NumberTen = 3
        Booklet = 4
        UVCover = 5
        Carton = 6
        ReturnEnv = 7
    End Enum

    '' company types
    Public Enum CompanyTypes As Integer
        ChurchBudget = 1
        MonthlyMail = 2
    End Enum


    '' could create public properties and let the arguments to the constructor be optional

    '' constructor (requires arguments:)
    '' path as string = path to png font
    '' folder as string = folder number
    '' job as QOPF.JobTypes = type of job to open
    '' comp as QOPF.CompanyTypes = company division to open as
    Public Sub New(path As String, fld As String, job As JobTypes, comp As CompanyTypes)
        pngfntpath = path
        folder = fld
        cmp = comp
        jb = job
    End Sub

    Public Sub openPNG()
        Dim cmd As String = pngfntpath & " /o=" & folder & " /t=" & getJobTypeString()
        Call Shell(cmd, AppWinStyle.MaximizedFocus)
    End Sub


    Private Function getJobTypeString() As String

        If (cmp = CompanyTypes.ChurchBudget) Then

            Select Case jb
                Case JobTypes.DoubleWide
                    Return CBDW
                Case JobTypes.Kyocera
                    Return CBKY
                Case JobTypes.NumberTen
                    Return CBTN
                Case JobTypes.Booklet
                    Return CBBK
                Case JobTypes.UVCover
                    Return CBUV
                Case JobTypes.Carton
                    Return CBCN
                Case Else
                    Return ""
            End Select

        Else

            Select Case jb
                Case JobTypes.DoubleWide
                    Return MMDW
                Case JobTypes.Kyocera
                    Return MMKY
                Case JobTypes.NumberTen
                    Return MMTN
                Case JobTypes.Carton
                    Return MMCN
                Case JobTypes.ReturnEnv
                    Return MMRT
                Case Else
                    Return ""
            End Select

        End If
    End Function

End Class
