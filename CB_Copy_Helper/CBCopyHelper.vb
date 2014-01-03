'' for IO.FileInfo
Imports System.IO
'' for regex-ing folder search strings
Imports System.Text.RegularExpressions

Public Class CBCopyHelperForm
    '' refresh settings


    '' temporary constants - will be settings later
    'Private Const PNGFONTPATH As String = "c:\pngfont\pngfont2.exe"
    'Private Const FONTTOOLSPATH As String = "c:\FontTool\fnttool3.exe"

    '' constants for paths
    Private Const CBPROOFPATH As String = "g:\_CBProofs\"
    Private Const MMPROOFPATH As String = "g:\_MMProofs\"
    Private Const STRIPINPATH As String = "g:\jdforsythe\Settings\Photoshop Scripts\CreateStripinDroplet.exe"
    Private Const STRIPINTEMPLATE As String = "g:\jdforsythe\Templates\Photoshop\Dollar_Addressed_Stripins.psd"

    '' enum for company
    Enum CompanyTypes As Integer
        ChurchBudget = 1
        McDaniel = 2
        United = 3
        MonthlyMail = 4
    End Enum

    '' enum for template type
    Enum TemplateTypes As Integer
        Dollar = 1
        Premier = 2
        Carton = 3
        BookletFront = 4
        BookletBack = 5
        BookletCover = 6
        BizhubCover = 7
        Mailback = 8
        Window = 9
        Retrn = 10
    End Enum

    '' global to hold the company
    Dim company As New CompanyTypes

    '' global to hold the lists (for selectedIndex on clicks)
    Dim copyFileList As New List(Of IO.FileInfo)
    Dim proofFileList As New List(Of IO.FileInfo)








    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' helper functions
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Function getSearchSalts(ByVal input As String) As Array
        If Not (company = CompanyTypes.MonthlyMail) Then
            '' if the first character is a letter, this is Church Budget, and salts will be first character and then the rest
            '' else it's McDaniel or United, salts will be first two characters, then the rest
            If (Regex.IsMatch(input.Substring(0, 1), "[A-Za-z]")) Then
                company = CompanyTypes.ChurchBudget
                Return {input.Substring(0, 1).ToUpper, _
                        input.Substring(1, input.Length - 1)}
            Else
                If (input.Substring(0, 1) = "6") Then
                    company = CompanyTypes.United
                Else
                    company = CompanyTypes.McDaniel
                End If
                '' both companys have the same salt pattern
                Return {input.Substring(0, 2), _
                        input.Substring(2)}
            End If
        Else
            '' monthly mail doesn't have a second salt, just one
            Return {input, ""}
        End If
    End Function

    Private Function getArrayOfTopLevelPaths(ByVal salts() As String) As Array
        If (company = CompanyTypes.ChurchBudget) Then
            Return {"g:\cb" & salts(0).ToUpper, _
                    "g:\Full Color Sheets\cb" & salts(0).ToUpper, _
                    "g:\CHKBK\cb\CB" & salts(0).ToUpper, _
                    "g:\CARTONS\cb\CB" & salts(0).ToUpper, _
                    "g:\CHKBK\UV COVERS"
                    }
        ElseIf (company = CompanyTypes.McDaniel) Then
            Return {"g:\MCDANIEL\MC" & salts(0), _
                    "g:\Full Color Sheets\McDaniel\" & salts(0), _
                    "g:\CHKBK\McDaniel", _
                    "g:\CARTONS\McDaniel", _
                    "g:\CHKBK\UV COVERS"
                   }
        ElseIf (company = CompanyTypes.United) Then
            Return {"g:\UNITED\Un" & salts(0), _
                    "g:\Full Color Sheets\United\" & salts(0), _
                    "g:\CHKBK\United", _
                    "g:\CARTONS\United", _
                    "g:\CHKBK\UV COVERS"
                   }
        Else
            '' Monthly mail
            Return {"h:\MM" & salts(0).Substring(0, 1) & "\MM" & salts(0).Substring(0, 1) & "-" & salts(0).Substring(1, 1), _
                    "g:\WSCAN\MM" & salts(0).Substring(0, 1) & "\MM" & salts(0).Substring(0, 1) & "-" & salts(0).Substring(1, 1)
                   }
        End If
    End Function

    Private Function getFolderTrees(ByRef paths() As String) As List(Of IO.DirectoryInfo)
        Dim pathLst As New List(Of IO.DirectoryInfo)
        For Each Path As String In paths
            Dim dir As New IO.DirectoryInfo(Path)
            '' add each top level path
            pathLst.Add(dir)
            Try
                '' add all subpaths
                pathLst.AddRange(dir.EnumerateDirectories("*", SearchOption.AllDirectories))
            Catch DirNotFound As DirectoryNotFoundException
                '' console.writeline("{0}", DirNotFound.Message)
            Catch UnAuthDir As UnauthorizedAccessException
                '' console.writeline("{0}", UnAuthDir.Message)
            Catch LongPath As PathTooLongException
                '' console.writeline("{0}", LongPath.Message)
            End Try
        Next
        Return pathLst
    End Function

    Private Function doFileSearch(ByVal filter As String, pathList As List(Of IO.DirectoryInfo), salts() As String) As List(Of IO.FileInfo)
        Dim fileList As New List(Of IO.FileInfo)
        '' iterate through paths
        For Each path In pathList
            Try
                For Each fi In path.EnumerateFiles(filter).OrderBy(Function(x) x.Name)
                    Try
                        If (fi.Name.Contains(salts(0)) And fi.Name.Contains(salts(1))) Then
                            fileList.Add(fi)
                        End If
                    Catch ex As Exception
                        '' if we aren't authorized or can't read the fileinfo for whatever reason, we won't be
                        '' able to open it, so don't worry about adding it to the list
                    End Try
                Next
            Catch ex As Exception

            End Try
        Next

        Return fileList
    End Function

    '' returns a prettified folder number (e.g. "A-0101" instead of "A0101")
    '' if it returns an empty string, then the folder number wasn't value
    Private Function getPrettyFolderNumber(ByVal ugly As String) As String
        Dim pretty As String = ""
        '' not taking into account weird things like premier, all folder numbers for CB are 5 characters
        '' if it's not, then it's not a real folder number and we return an empty string
        If (ugly.Length = 5) Then
            If (company = CompanyTypes.ChurchBudget) Then
                pretty = ugly.Insert(1, "-").ToUpper
            ElseIf (company = CompanyTypes.McDaniel Or company = CompanyTypes.United) Then
                pretty = ugly.Insert(2, "-")
            End If
        ElseIf (ugly.Length = 4) Then
            '' for monthly mail, there are no dashes or anything, the pretty folder number is just the 4 digit number
            If (company = CompanyTypes.MonthlyMail) Then
                pretty = ugly
            End If
        End If

        Return pretty
    End Function

    Private Function getSavePath(prettyFolder As String, templateType As TemplateTypes) As String
        If (templateType = TemplateTypes.Dollar Or _
            templateType = TemplateTypes.Premier Or _
            templateType = TemplateTypes.Mailback) Then

            If (company = CompanyTypes.ChurchBudget) Then
                Return "g:\CB" & prettyFolder.Substring(0, 1).ToUpper & "\" & prettyFolder
            ElseIf (company = CompanyTypes.McDaniel) Then
                Return "g:\MCDANIEL\MC" & prettyFolder.Substring(0, 2) & "\" & prettyFolder
            ElseIf (company = CompanyTypes.United) Then
                Return "g:\UNITED\Un" & prettyFolder.Substring(0, 2) & "\" & prettyFolder
            Else
                '' Monthly mail (don't create subfolders per church for MM)
                Return "h:\MM" & prettyFolder.Substring(0, 1) & "\MM" & prettyFolder.Substring(0, 1) & "-" & prettyFolder.Substring(1, 1)
            End If


        ElseIf (templateType = TemplateTypes.BookletFront Or _
                templateType = TemplateTypes.BookletBack) Then
            If (company = CompanyTypes.ChurchBudget) Then
                Return "g:\CHKBK\cb\CB" & prettyFolder.Substring(0, 1).ToUpper & "\" & prettyFolder
            ElseIf (company = CompanyTypes.McDaniel) Then
                Return "g:\CHKBK\McDaniel\" & prettyFolder
            ElseIf (company = CompanyTypes.United) Then
                Return "g:\CHKBK\United\" & prettyFolder
            Else
                Return ""
            End If


        ElseIf (templateType = TemplateTypes.BookletCover Or _
                templateType = TemplateTypes.BizhubCover) Then
            Return "g:\CHKBK\UV COVERS"


        ElseIf (templateType = TemplateTypes.Carton) Then
            If (company = CompanyTypes.ChurchBudget) Then
                Return "g:\CARTONS\cb\CB" & prettyFolder.Substring(0, 1).ToUpper
            ElseIf (company = CompanyTypes.McDaniel) Then
                Return "g:\CARTONS\McDaniel"
            ElseIf (company = CompanyTypes.United) Then
                Return "g:\CARTONS\United"
            Else
                Return ""
            End If


        Else
            Return ""


        End If

    End Function



    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' event handlers
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub uiTxtFolderNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles uiTxtFolderNumber.KeyDown

        '' any time the text is changing (or even on pressing return) disable the create copy
        '' buttons so they can't create copy for a partial folder number
        uiTxtFontCode.Enabled = False
        uiBtnTemplateDollar.Enabled = False
        uiBtnTemplatePremier.Enabled = False
        uiBtnTemplateMailback.Enabled = False
        uiBtnTemplateBookFront.Enabled = False
        uiBtnTemplateBookBack.Enabled = False
        uiBtnTemplateBookCover.Enabled = False
        uiBtnTemplateCarton.Enabled = False
        uiBtnTemplateBizhub.Enabled = False

        '' only search if they pressed enter
        If Not (e.KeyCode = Keys.Return) Then
            Exit Sub
        End If

        '' highlight the text in the box
        uiTxtFolderNumber.SelectAll()


        '' clear the lists
        copyFileList.Clear()
        proofFileList.Clear()
        uiLstDesignFiles.Items.Clear()
        uiLstProofFiles.Items.Clear()

        '' counts for copy folders/files and proof files
        Dim copyFileCount As Integer = 0
        Dim copyFolderCount As Integer = 0
        Dim proofFilecount As Integer = 0
        '' holds a list of folders we've counted
        Dim usedFolders As New List(Of String)


        '' trim whitespace from the search box
        Dim input As String = uiTxtFolderNumber.Text.Trim
        uiTxtFolderNumber.Text = input

        '' if the search isn't empty or less than ?? characters (2 for now)
        If (input <> "" And input.Length > 1) Then

            '' we need the two search "salts" (e.g. "a" and "0101" or "32" and "056")
            Dim salts(2) As String
            salts = getSearchSalts(input)

            '' get a list of paths for copy files
            Dim pathList As List(Of IO.DirectoryInfo) = getFolderTrees(getArrayOfTopLevelPaths(salts))

            '' do a search in all those paths for files containing the two salts
            copyFileList.AddRange(doFileSearch("*.*", pathList, salts))

            '' add the copy files to the list
            For Each fi In copyFileList
                uiLstDesignFiles.Items.Add(fi.Name)
                '' increment the file count
                copyFileCount += 1
                '' if this file's folder isn't in the list of used folders, add it
                If Not (usedFolders.Contains(fi.Directory.ToString)) Then
                    usedFolders.Add(fi.Directory.ToString)
                    copyFolderCount += 1
                End If
            Next


            '' get path(s) for proofs
            pathList.Clear()
            If Not (company = CompanyTypes.MonthlyMail) Then
                pathList.Add(New IO.DirectoryInfo(CBPROOFPATH))
            Else
                pathList.Add(New IO.DirectoryInfo(MMPROOFPATH))
            End If

            '' do a search in the proof path(s) for files containing the two salts and *.pdf
            proofFileList.AddRange(doFileSearch("*.pdf", pathList, salts))

            '' add the proof files to the list
            For Each fi In proofFileList
                uiLstProofFiles.Items.Add(fi.Name)
                '' increment the file count
                proofFilecount += 1
            Next

            'MessageBox.Show("Files found: " & copyFileList.Count)

            '' update the count labels
            lblCopyFilesFound.Text = copyFileCount & " file(s) found in " & copyFolderCount & " folder(s)"
            lblProofFilesFound.Text = proofFilecount & " proof(s) found"

            '' now that we've "opened" that folder number, enable the buttons to add copy
            uiTxtFontCode.Enabled = True
            uiBtnTemplateDollar.Enabled = True
            uiBtnTemplatePremier.Enabled = True
            uiBtnTemplateMailback.Enabled = True
            uiBtnTemplateBookFront.Enabled = True
            uiBtnTemplateBookBack.Enabled = True
            uiBtnTemplateBookCover.Enabled = True
            uiBtnTemplateCarton.Enabled = True
            uiBtnTemplateBizhub.Enabled = True

        End If


    End Sub

    '' double-click on design file to open
    '' and copy folder number to clipboard
    Private Sub uiLstDesignFiles_DoubleClick(sender As Object, e As EventArgs) Handles uiLstDesignFiles.DoubleClick
        '' debug - to show the proper path/file is being selected
        'MessageBox.Show(copyFileList(uiLstDesignFiles.SelectedIndex).FullName)

        Clipboard.SetText(uiTxtFolderNumber.Text)

        '' open the file in its default program
        Dim openFile As New ProcessStartInfo()
        openFile.FileName = copyFileList(uiLstDesignFiles.SelectedIndex).FullName
        openFile.UseShellExecute = True
        Process.Start(openFile)
    End Sub

    '' left-click on design file to copy parent directory path to clipboard
    '' right-click on design file to open in explorer
    Private Sub uiLstDesignFiles_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles uiLstDesignFiles.MouseDown
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            '' did we actually click on an item in the box
            Dim selInd As Integer = uiLstDesignFiles.IndexFromPoint(e.X, e.Y)
            '' if the returned selindex is not -1, we clicked something
            If (selInd <> -1) Then
                '' copy the file path of the item selected, minus the filename, to the clipboard
                Clipboard.SetText(copyFileList(selInd).DirectoryName)
            End If

            '' determine if this is a right mouse click
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            '' did we actually right-click on an item in the box
            Dim selInd As Integer = uiLstDesignFiles.IndexFromPoint(e.X, e.Y)

            '' if the returned selected index from cursor coordinates is not -1, then we clicked on something
            If (selInd <> -1) Then

                '' get the full path of the item selected
                Dim selectedPath As String = copyFileList(selInd).FullName

                '' open explorer and select the file clicked
                Call Shell("explorer.exe /select," & selectedPath, AppWinStyle.NormalFocus)

            End If
        End If
    End Sub


    '' double-click on proof file to open
    Private Sub uiLstProofFiles_DoubleClick(sender As Object, e As EventArgs) Handles uiLstProofFiles.DoubleClick
        '' debug - to show the proper path/file is being selected
        'MessageBox.Show(proofFileList(uiLstProofFiles.SelectedIndex).FullName)

        '' open the file in its default program
        Dim openFile As New ProcessStartInfo()
        openFile.FileName = proofFileList(uiLstProofFiles.SelectedIndex).FullName
        openFile.UseShellExecute = True
        Process.Start(openFile)
    End Sub

    '' left-click on proof file to copy folder location to clipboard
    '' right-click on proof file to open in explorer
    Private Sub uiLstProofFiles_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles uiLstProofFiles.MouseDown
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            '' did we actually click on an item in the box
            Dim selInd As Integer = uiLstProofFiles.IndexFromPoint(e.X, e.Y)
            '' if the returned selindex is not -1, we clicked something
            If (selInd <> -1) Then
                '' copy the file path of the item selected, minus the filename, to the clipboard
                Clipboard.SetText(proofFileList(selInd).DirectoryName)
            End If


        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            '' did we actually right-click on an item in the box
            Dim selInd As Integer = uiLstProofFiles.IndexFromPoint(e.X, e.Y)

            '' if the returned selected index from cursor coordinates is not -1, then we clicked on something
            If (selInd <> -1) Then

                '' get the full path of the item selected
                Dim selectedPath As String = proofFileList(selInd).FullName

                '' open explorer and select the file clicked
                Call Shell("explorer.exe /select," & selectedPath, AppWinStyle.NormalFocus)

            End If
        End If
    End Sub

    Private Sub uiBtnPrintProof_Click(sender As Object, e As EventArgs) Handles uiBtnPrintProof.Click
        Dim ind As Integer = uiLstProofFiles.SelectedIndex
        '' if the selectedIndex is -1, nothing is selected
        If (ind = -1) Then
            MessageBox.Show("You must select a proof to print")
        Else
            Try
                Dim fi As String = proofFileList(ind).FullName
                Dim myprocess As New Process
                myprocess.StartInfo.CreateNoWindow = False
                myprocess.StartInfo.Verb = "print"
                myprocess.StartInfo.FileName = fi
                myprocess.Start()
                myprocess.WaitForExit(10000)
                Try
                    myprocess.CloseMainWindow()
                    myprocess.Close()
                Catch ex As Exception

                End Try
            Catch ex As Exception
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub uiBtnOpenPNG_Click(sender As Object, e As EventArgs) Handles uiBtnOpenPNG.Click
        Dim folder As String = uiTxtFolderNumber.Text
        If (folder <> "" And folder.Length = 5 And Not company = CompanyTypes.MonthlyMail) Then
            Dim cmd As String = My.Settings.PngFontPath & " /o=" & folder.ToUpper & " /t=DoubleWide_CB"
            Call Shell(cmd, AppWinStyle.MaximizedFocus)
        ElseIf (folder <> "" And folder.Length = 4 And company = CompanyTypes.MonthlyMail) Then
            Dim cmd As String = My.Settings.PngFontPath & " /o=" & folder.ToUpper & " /t=DoubleWide_MM"
            Call Shell(cmd, AppWinStyle.MaximizedFocus)
        Else
            MessageBox.Show("You must input a proper folder number before opening PNG Font")
        End If
    End Sub

    Private Sub uiBtnOpenFontTools_Click(sender As Object, e As EventArgs) Handles uiBtnOpenFontTools.Click
        Dim folder As String = uiTxtFolderNumber.Text

        If (folder <> "" And folder.Length = 5 And Not company = CompanyTypes.MonthlyMail) Then
            Dim cmd As String = My.Settings.FontToolsPath & " /o=" & folder
            Call Shell(cmd, AppWinStyle.MaximizedFocus)
        ElseIf (folder <> "" And folder.Length = 4 And company = CompanyTypes.MonthlyMail) Then
            Dim cmd As String = My.Settings.FontToolsPath & " /o=" & folder
            Call Shell(cmd, AppWinStyle.MaximizedFocus)
        Else
            MessageBox.Show("You must input a proper folder number before opening Font Tools")
        End If
    End Sub

    Private Sub uiBtnTemplateDollar_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateDollar.Click
        Dim prettyFolder As String = getPrettyFolderNumber(uiTxtFolderNumber.Text.Trim)
        Dim fontCode As String = uiTxtFontCode.Text.Trim
        If (prettyFolder = "") Then
            MessageBox.Show("Please enter a valid folder number before creating a new design")
        ElseIf (fontCode = "") Then
            MessageBox.Show("Please enter a font code before creating a new design")
        Else

            '' copy template, rename, copy the folder/font to the clipboard, and open the file
            Dim template As New DesignTemplates(prettyFolder, fontCode)
            template.TemplateType = DesignTemplates.TemplateTypes.Dollar
            template.SavePath = getSavePath(prettyFolder, TemplateTypes.Dollar)
            If (company = CompanyTypes.MonthlyMail) Then
                template.Company = DesignTemplates.CompanyTypes.MonthlyMail
            Else
                template.Company = DesignTemplates.CompanyTypes.ChurchBudget
            End If
            template.createAndOpen()
        End If
    End Sub

    Private Sub uiBtnTemplatePremier_Click(sender As Object, e As EventArgs) Handles uiBtnTemplatePremier.Click
        Dim prettyFolder As String = getPrettyFolderNumber(uiTxtFolderNumber.Text.Trim)
        Dim fontCode As String = uiTxtFontCode.Text.Trim
        If (prettyFolder = "") Then
            MessageBox.Show("Please enter a valid folder number before creating a new design")
        ElseIf (fontCode = "") Then
            MessageBox.Show("Please enter a font code before creating a new design")
        Else
            '' copy template, rename, copy the folder/font to the clipboard, and open the file
            Dim template As New DesignTemplates(prettyFolder, fontCode)
            template.TemplateType = DesignTemplates.TemplateTypes.Premier
            template.SavePath = getSavePath(prettyFolder, TemplateTypes.Premier)
            If (company = CompanyTypes.MonthlyMail) Then
                template.Company = DesignTemplates.CompanyTypes.MonthlyMail
            Else
                template.Company = DesignTemplates.CompanyTypes.ChurchBudget
            End If
            template.createAndOpen()
        End If
    End Sub

    Private Sub uiBtnTemplateCarton_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateCarton.Click
        Dim prettyFolder As String = getPrettyFolderNumber(uiTxtFolderNumber.Text.Trim)
        Dim fontCode As String = uiTxtFontCode.Text.Trim
        If (prettyFolder = "") Then
            MessageBox.Show("Please enter a valid folder number before creating a new design")
        ElseIf (fontCode = "") Then
            MessageBox.Show("Please enter a font code before creating a new design")
        Else
            '' copy template, rename, copy the folder/font to the clipboard, and open the file
            Dim template As New DesignTemplates(prettyFolder, fontCode)
            template.TemplateType = DesignTemplates.TemplateTypes.Carton
            template.SavePath = getSavePath(prettyFolder, TemplateTypes.Carton)
            template.createAndOpen()
        End If
    End Sub

    Private Sub uiBtnTemplateBookFront_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateBookFront.Click
        Dim prettyFolder As String = getPrettyFolderNumber(uiTxtFolderNumber.Text.Trim)
        Dim fontCode As String = uiTxtFontCode.Text.Trim
        If (prettyFolder = "") Then
            MessageBox.Show("Please enter a valid folder number before creating a new design")
        ElseIf (fontCode = "") Then
            MessageBox.Show("Please enter a font code before creating a new design")
        Else
            '' copy template, rename, copy the folder/font to the clipboard, and open the file
            Dim template As New DesignTemplates(prettyFolder, fontCode)
            template.TemplateType = DesignTemplates.TemplateTypes.BookletFront
            template.SavePath = getSavePath(prettyFolder, TemplateTypes.BookletFront)
            template.createAndOpen()
        End If
    End Sub

    Private Sub uiBtnTemplateBookBack_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateBookBack.Click
        Dim prettyFolder As String = getPrettyFolderNumber(uiTxtFolderNumber.Text.Trim)
        Dim fontCode As String = uiTxtFontCode.Text.Trim
        If (prettyFolder = "") Then
            MessageBox.Show("Please enter a valid folder number before creating a new design")
        ElseIf (fontCode = "") Then
            MessageBox.Show("Please enter a font code before creating a new design")
        Else
            '' copy template, rename, copy the folder/font to the clipboard, and open the file
            Dim template As New DesignTemplates(prettyFolder, fontCode)
            template.TemplateType = DesignTemplates.TemplateTypes.BookletBack
            template.SavePath = getSavePath(prettyFolder, TemplateTypes.BookletBack)
            template.createAndOpen()
        End If
    End Sub

    Private Sub uiBtnTemplateBookCover_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateBookCover.Click
        Dim prettyFolder As String = getPrettyFolderNumber(uiTxtFolderNumber.Text.Trim)
        Dim fontCode As String = uiTxtFontCode.Text.Trim
        If (prettyFolder = "") Then
            MessageBox.Show("Please enter a valid folder number before creating a new design")
        ElseIf (fontCode = "") Then
            MessageBox.Show("Please enter a font code before creating a new design")
        Else
            '' copy template, rename, copy the folder/font to the clipboard, and open the file
            Dim template As New DesignTemplates(prettyFolder, fontCode)
            template.TemplateType = DesignTemplates.TemplateTypes.BookletCover
            template.SavePath = getSavePath(prettyFolder, TemplateTypes.BookletCover)
            template.createAndOpen()
        End If
    End Sub

    Private Sub uiBtnBizhub_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateBizhub.Click
        Dim prettyFolder As String = getPrettyFolderNumber(uiTxtFolderNumber.Text.Trim)
        Dim fontCode As String = uiTxtFontCode.Text.Trim
        If (prettyFolder = "") Then
            MessageBox.Show("Please enter a valid folder number before creating a new design")
        ElseIf (fontCode = "") Then
            MessageBox.Show("Please enter a font code before creating a new design")
        Else
            '' copy template, rename, copy the folder/font to the clipboard, and open the file
            Dim template As New DesignTemplates(prettyFolder, fontCode)
            template.TemplateType = DesignTemplates.TemplateTypes.BizhubCover
            template.SavePath = getSavePath(prettyFolder, TemplateTypes.BizhubCover)
            template.createAndOpen()
        End If
    End Sub

    Private Sub uiBtnTemplateMailback_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateMailback.Click
        Dim prettyFolder As String = getPrettyFolderNumber(uiTxtFolderNumber.Text.Trim)
        Dim fontCode As String = uiTxtFontCode.Text.Trim
        If (prettyFolder = "") Then
            MessageBox.Show("Please enter a valid folder number before creating a new design")
        ElseIf (fontCode = "") Then
            MessageBox.Show("Please enter a font code before creating a new design")
        Else
            '' copy template, rename, copy the folder/font to the clipboard, and open the file
            Dim template As New DesignTemplates(prettyFolder, fontCode)
            template.TemplateType = DesignTemplates.TemplateTypes.Mailback
            template.SavePath = getSavePath(prettyFolder, TemplateTypes.Mailback)
            template.createAndOpen()
        End If
    End Sub

    Private Sub CBCopyHelperForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '' refresh the settings on open
        My.Settings.Reload()

        If (My.Settings.isMM) Then
            company = CompanyTypes.MonthlyMail
        End If

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim stngFrm As New Settings()
        '' show as a modal dialog, so this doesn't return until the dialog is closed
        stngFrm.ShowDialog()

        '' reload the settings and check the company setting
        My.Settings.Reload()
        If (My.Settings.isMM) Then
            company = CompanyTypes.MonthlyMail
        Else
            '' if this isn't monthly mail, unset the company variable so the company
            '' is set by the folder number
            company = Nothing
        End If
    End Sub

    Private Sub uiBtnStripin_Click(sender As Object, e As EventArgs) Handles uiBtnStripin.Click

        Dim folder As String = uiTxtFolderNumber.Text
        Dim cmd As String = ""

        If (folder <> "" And folder.Length = 5 And Not company = CompanyTypes.MonthlyMail) Then

            '' open stripin droplet
            cmd = STRIPINPATH & " " & STRIPINTEMPLATE
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

            '' open font tools
            cmd = My.Settings.FontToolsPath & " /o=" & folder
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

        ElseIf (folder <> "" And folder.Length = 4 And company = CompanyTypes.MonthlyMail) Then

            '' open stripin droplet
            cmd = STRIPINPATH & " " & STRIPINTEMPLATE
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

            '' open font tools
            cmd = My.Settings.FontToolsPath & " /o=" & folder
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

        Else
            MessageBox.Show("You must input a proper folder number before opening Font Tools")
        End If

    End Sub
End Class
