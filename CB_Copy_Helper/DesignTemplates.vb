Imports System.IO

Public Class DesignTemplates

    '' constants to paths of design templates
    '' Church Budget
    Private Const TEMPLATEDOLLAR As String = "g:\CopySetting\Templates\Indesign\Dollar Envelope.indd"
    Private Const TEMPLATEPREMIER As String = "g:\CopySetting\Templates\Indesign\Premier Envelope.indd"
    Private Const TEMPLATEMAILBACK As String = "g:\CopySetting\Templates\Quark\Mailback.qxp"
    Private Const TEMPLATEBOOKLETBACK As String = "g:\CopySetting\Templates\Indesign\Booklet Back.indd"
    Private Const TEMPLATEBOOKLETFRONT As String = "g:\CopySetting\Templates\Indesign\Booklet Front.indd"
    Private Const TEMPLATEBOOKLETCOVER As String = "g:\CopySetting\Templates\Photoshop\Booklet Cover.psd"
    Private Const TEMPLATECARTON As String = "g:\CopySetting\Templates\Photoshop\Carton.psd"
    Private Const TEMPLATEBIZHUBCOVER As String = "g:\CopySetting\Templates\Quark\Booklet Cover Bizhub.qxp"
    Private Const TEMPLATERETURN As String = "g:\CopySetting\Templates\Indesign\Return.indd"
    Private Const TEMPLATEWINDOW As String = "g:\CopySetting\Templates\Indesign\Window.indd"

    '' Monthly Mail
    Private Const TEMPLATEMMDOLLAR As String = "g:\CopySetting\Templates\Indesign\Dollar Envelope MM.indd"

    '' types of templates
    Public Enum TemplateTypes As Integer
        Dollar = 1
        Premier = 2
        Carton = 3
        BookletFront = 4
        BookletBack = 5
        BookletCover = 6
        BizhubCover = 7
        Mailback = 8
        ReturnEnv = 9
        Window = 10
    End Enum

    '' company divisions
    Public Enum CompanyTypes As Integer
        ChurchBudget = 1
        MonthlyMail = 2
    End Enum

    '' private copies of the public properties
    Private tmpl As TemplateTypes
    Private fld As String = ""
    Private fnt As String = ""
    Private svpth As String = ""
    Private comp As CompanyTypes

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

    '' public property allowing get/set company type
    Public Property Company() As CompanyTypes
        Get
            Return comp
        End Get
        Set(value As CompanyTypes)
            comp = value
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

        '' copy to clipboard "FOLDER FONT" (entered font text up until the first space)
        '' e.g. "A0101 A Weekly" becomes "A0101 A"
        Dim i As Integer = fnt.IndexOf(" ")
        '' if the index is -1, then " " was not found
        If (i <> -1) Then
            '' use the font code up to the first " "
            Clipboard.SetText(fld.ToUpper & " " & fnt.Substring(0, i))
        Else
            '' there is no " " so use the whole font code
            Clipboard.SetText(fld.ToUpper & " " & fnt)
        End If

        Dim templatePath As String = getTemplatePath()
        Dim targetFileName As String = getFileName(Path.GetExtension(templatePath))
        Dim fullTargetPath As String = ""

        If (comp = CompanyTypes.MonthlyMail) Then
            fullTargetPath = svpth & "\MM" & targetFileName
        Else
            fullTargetPath = svpth & "\" & targetFileName
        End If


        '' check that the template file exists
        If Not (File.Exists(templatePath)) Then
            MessageBox.Show("Uh Oh! The template you chose is missing! Call for help!")
            Exit Sub
        End If

        '' check that the save path exists, and if not, create it
        If Not (Directory.Exists(svpth)) Then
            Directory.CreateDirectory(svpth)
            MessageBox.Show("Created a new folder for " & fld & "at: " & svpth)
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
        Select Case tmpl
            Case TemplateTypes.Dollar
                If (comp = CompanyTypes.ChurchBudget) Then
                    Return TEMPLATEDOLLAR
                Else
                    Return TEMPLATEMMDOLLAR
                End If

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

            Case TemplateTypes.BizhubCover
                Return TEMPLATEBIZHUBCOVER

            Case TemplateTypes.ReturnEnv
                Return TEMPLATERETURN

            Case TemplateTypes.Window
                Return TEMPLATEWINDOW

            Case Else
                Return ""
        End Select

    End Function

    Private Function getFileName(ByVal extension As String) As String
        Return (fld & " " & fnt & extension)
    End Function


End Class
