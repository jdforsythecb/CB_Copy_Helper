Imports System.IO

Public Class DesignTemplates

    '' constants to paths of design templates
    Private Const TEMPLATEDOLLAR As String = "g:\Quark Templates\zForCBCopy_Dollar.qxp"
    Private Const TEMPLATEPREMIER As String = "g:\Quark Templates\zForCBCopy_Premier.qxp"
    Private Const TEMPLATEMAILBACK As String = "g:\Quark Templates\zForCBCopy_Mailback.qxp"
    Private Const TEMPLATEBOOKLETBACK As String = "g:\Quark Templates\zForCBCopy_BookletBack.qxp"
    Private Const TEMPLATEBOOKLETFRONT As String = "g:\Quark Templates\zForCBCopy_BookletFront.qxp"
    Private Const TEMPLATEBOOKLETCOVER As String = "g:\Quark Templates\zForCBCopy_BookletCover.psd"
    Private Const TEMPLATECARTON As String = "g:\Quark Templates\zForCBCopy_Carton.psd"

    '' types of templates
    Public Enum TemplateTypes As Integer
        Dollar = 1
        Premier = 2
        Carton = 3
        BookletFront = 4
        BookletBack = 5
        BookletCover = 6
        Retrn = 7
        Window = 8
        Mailback = 9
    End Enum

    '' private copies of the public properties
    Private tmpl As TemplateTypes
    Private fld As String = ""
    Private fnt As String = ""
    Private svpth As String = ""

    '' public property allowing get/set template type
    Public Property TemplateType() As TemplateTypes
        Get
            Return tmpl
        End Get

        Set(value As TemplateTypes)
            tmpl = value
        End Set
    End Property

    '' public property allowing get/set folder number
    Public Property FolderNumber() As String
        Get
            Return fld
        End Get

        Set(value As String)
            fld = value
        End Set
    End Property

    '' public property allowing get/set font code
    Public Property FontCode() As String
        Get
            Return fnt
        End Get

        Set(value As String)
            fnt = value
        End Set
    End Property

    '' public property allowing get/set save path
    Public Property SavePath() As String
        Get
            Return svpth
        End Get

        Set(value As String)
            svpth = value
        End Set
    End Property



    '' constructor
    Public Sub New(Optional FolderNumber As String = "", Optional FontCode As String = "", Optional SavePath As String = "")
        If Not (FolderNumber = "") Then
            fld = FolderNumber
        End If
        If Not (FontCode = "") Then
            fnt = FontCode
        End If
        If Not (SavePath = "") Then
            svpth = SavePath
        End If
    End Sub




    '' public methods
    Public Sub createAndOpen()
        If (fld = "" Or fnt = "" Or svpth = "") Then
            '' can't do anything without folder or font
            Exit Sub
        End If

        Dim templatePath As String = getTemplatePath()
        Dim targetFileName As String = getFileName(Path.GetExtension(templatePath))
        Dim fullTargetPath As String = svpth & "\" & targetFileName

        '' check that the template file exists
        If Not (File.Exists(templatePath)) Then
            MessageBox.Show("Uh Oh! The template you chose is missing! Call for help!")
            Exit Sub
        End If

        '' check that the save path exists, and if not, create it
        If Not (Directory.Exists(svpth)) Then
            Directory.CreateDirectory(svpth)
            MessageBox.Show("Created a new folder for " & fld)
        End If

        '' check that the file we're creating doesn't exist
        If (File.Exists(fullTargetPath)) Then
            MessageBox.Show("Sorry, a design file with that name already exists. Please choose a different name or use the search and open the file to edit it.")
            Exit Sub
        End If

        '' we know the template exists, the save path exists, and the save file does not
        '' so we can safely copy the template to the new file
        Try

            File.Copy(templatePath, fullTargetPath)
        Catch copyError As IOException
            MessageBox.Show("Error creating new design file. Message: " & copyError.Message)
        End Try

        '' now that we've created the file, open it in the default program
        Dim openFile As New ProcessStartInfo()
        openFile.FileName = fullTargetPath
        openFile.UseShellExecute = True
        Process.Start(openFile)
    End Sub





    '' private helper methods
    Private Function getTemplatePath() As String
        '' vb's switch statement
        Select Case tmpl
            Case TemplateTypes.Dollar
                Return TEMPLATEDOLLAR
            Case TemplateTypes.Premier
                Return TEMPLATEPREMIER
            Case TemplateTypes.Mailback
                Return TEMPLATEMAILBACK
            Case TemplateTypes.BookletBack
                Return TEMPLATEBOOKLETBACK
            Case TemplateTypes.BookletFront
                Return TEMPLATEBOOKLETFRONT
            Case TemplateTypes.BookletCover
                Return TEMPLATEBOOKLETCOVER
            Case TemplateTypes.Carton
                Return TEMPLATECARTON
            Case Else
                Return ""
        End Select

    End Function

    Private Function getFileName(ByVal extension As String) As String
        Return (fld & " " & fnt & extension)
    End Function


End Class
