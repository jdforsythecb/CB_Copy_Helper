﻿'' to do:
'' separate search results for cover, carton, booklet
'' alert if church's own folder doesn't exist (offer to move files into one) - checkbox in settings - only for setters



'' for IO.FileInfo
Imports System.IO
'' for regex-ing folder search strings
Imports System.Text.RegularExpressions
'' for PNG Font Bridge
Imports PNGFontBridge

Public Class CBCopyHelperForm

    '' constants for paths
    Private Const CBPROOFPATH As String = "g:\_CBProofs\"
    Private Const MMPROOFPATH As String = "g:\_MMProofs\"
    Private Const STRIPINDOLLARPATH As String = "g:\jdforsythe\Settings\Photoshop Scripts\StripinsForCBCopyHelper\CreateStripinDollarDroplet.exe"
    Private Const STRIPINDOLLARTEMPLATE As String = "g:\jdforsythe\Templates\Photoshop\Dollar_Addressed_Stripins.psd"
    Private Const STRIPINBOOKLETPATH As String = "g:\jdforsythe\Settings\Photoshop Scripts\StripinsForCBCopyHelper\CreateStripinBookletDroplet.exe"
    Private Const STRIPINBOOKLETTEMPLATE As String = "g:\jdforsythe\Templates\Photoshop\Booklet_Stripins.psd"

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
        ReturnEnv = 9
        Window = 10
    End Enum

    '' enum for types of folders
    Enum DesignFileTypes As Integer
        Envelopes = 1
        Booklets = 2
        Covers = 3
        Cartons = 4
        OldBizHub = 5
    End Enum

    '' global to hold the company
    Dim company As New CompanyTypes

    '' global to hold the lists (for selectedIndex on clicks)
    Dim copyFileList As New List(Of IO.FileInfo)
    Dim proofFileList As New List(Of IO.FileInfo)
    Dim pngFontList As New Dictionary(Of String, Dictionary(Of String, String))
    '' PNGFontBridge instance
    Dim PNGPreview As New PNGFontBridge.PNGFontBridge


    '' draggable state
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' overriding windows forms titlebar drag method - to enable dragging window without border/caption
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' see msdn.microsoft.com/en-us/ms645618(VS.85).aspx - constants we're looking for
    Private Const WM_NCHITTEST As Integer = &H84
    Private Const HTCLIENT As Integer = &H1
    Private Const HTCAPTION As Integer = &H2

    Protected Overrides Sub WndProc(ByRef m As Message)
        '' call the default class code
        MyBase.WndProc(m)

        '' our own implementation to see if we're getting the WM_NCHITTEST
        'If Not ((m.Result = 0) Or (m.Result = 1) Or (m.Result = 14)) Then Console.WriteLine(m.Result)
        Select Case m.Msg
            Case WM_NCHITTEST
                If m.Result = HTCLIENT Then m.Result = HTCAPTION
        End Select
    End Sub


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

    '' gets an array of all top level paths to search recursively in for the salts
    '' returns an array of arrays
    '' [0] top level path
    '' [1] type of design files at that path DesignFileTypes.Envelopes, etc
    Private Function getArrayOfTopLevelPaths(ByVal salts() As String) As Array
        If (company = CompanyTypes.ChurchBudget) Then
            Return {"g:\cb" & salts(0).ToUpper, _
                    "g:\Full Color Sheets\cb" & salts(0).ToUpper, _
                    "g:\CHKBK\cb\CB" & salts(0).ToUpper, _
                    "g:\CHKBK\CUTS", _
                    "g:\CARTONS\cb\CB" & salts(0).ToUpper, _
                    "g:\CHKBK\UV COVERS", _
                    "g:\Full Color Sheets\_Coated\cb" & salts(0).ToUpper
                    }
        ElseIf (company = CompanyTypes.McDaniel) Then
            Return {"g:\MCDANIEL\MC" & salts(0), _
                    "g:\Full Color Sheets\McDaniel\" & salts(0), _
                    "g:\CHKBK\McDaniel", _
                    "g:\CHKBK\CUTS", _
                    "g:\CARTONS\McDaniel", _
                    "g:\CHKBK\UV COVERS", _
                    "g:\Full Color Sheets\_Coated\McDaniel\" & salts(0)
                   }
        ElseIf (company = CompanyTypes.United) Then
            Return {"g:\UNITED\Un" & salts(0), _
                    "g:\Full Color Sheets\United\" & salts(0), _
                    "g:\CHKBK\United", _
                    "g:\CHKBK\CUTS", _
                    "g:\CARTONS\United", _
                    "g:\CHKBK\UV COVERS", _
                    "g:\Full Color Sheets\_Coated\United\" & salts(0)
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
                        If ((fi.Name.Contains(salts(0).ToUpper()) Or fi.Name.Contains(salts(0).ToLower())) And fi.Name.Contains(salts(1))) Then
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
    '' if it returns an empty string, then the folder number wasn't valid
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
            templateType = TemplateTypes.Mailback Or _
            templateType = TemplateTypes.ReturnEnv Or _
            templateType = TemplateTypes.Window) Then

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

    Private Sub openTemplate(template As TemplateTypes)
        Dim prettyFolder As String = getPrettyFolderNumber(uiTxtFolderNumber.Text.Trim)
        Dim fontCode As String = uiTxtFontCode.Text.Trim
        If (prettyFolder = "") Then
            MessageBox.Show("Please enter a valid folder number before creating a new design")
        ElseIf (fontCode = "") Then
            MessageBox.Show("Please enter a font code before creating a new design")
        Else

            '' copy template, rename, copy the folder/font to the clipboard, and open the file
            Dim templateOpen As New DesignTemplates(prettyFolder, fontCode)

            Select Case template
                Case TemplateTypes.Dollar
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.Dollar
                Case TemplateTypes.Premier
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.Premier
                Case TemplateTypes.Mailback
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.Mailback
                Case TemplateTypes.BookletFront
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.BookletFront
                Case TemplateTypes.BookletBack
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.BookletBack
                Case TemplateTypes.BookletCover
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.BookletCover
                Case TemplateTypes.Carton
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.Carton
                Case TemplateTypes.BizhubCover
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.BizhubCover
                Case TemplateTypes.ReturnEnv
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.ReturnEnv
                Case TemplateTypes.Window
                    templateOpen.TemplateType = DesignTemplates.TemplateTypes.Window
                Case Else
                    MessageBox.Show("Template Type error. Call for help.")
            End Select

            templateOpen.SavePath = getSavePath(prettyFolder, template)

            If (company = CompanyTypes.MonthlyMail) Then
                templateOpen.Company = DesignTemplates.CompanyTypes.MonthlyMail
            Else
                templateOpen.Company = DesignTemplates.CompanyTypes.ChurchBudget
            End If

            templateOpen.createAndOpen()
        End If
    End Sub

    Private Sub quickOpenPng(jobType As QuickOpenPNGFont.JobTypes)
        Dim folder As String = uiTxtFolderNumber.Text.Trim
        Dim comp As QuickOpenPNGFont.CompanyTypes

        If (company = CompanyTypes.MonthlyMail And folder <> "" And folder.Length = 4) Then
            comp = QuickOpenPNGFont.CompanyTypes.MonthlyMail
        ElseIf (folder <> "" And folder.Length = 5) Then
            comp = QuickOpenPNGFont.CompanyTypes.ChurchBudget
        Else
            MessageBox.Show("You must input a proper folder number before opening PNG Font")
            Exit Sub
        End If

        Dim png As New QuickOpenPNGFont(My.Settings.PngFontPath, uiTxtFolderNumber.Text.Trim, _
                                        jobType, comp)
        png.openPNG()

    End Sub

    Private Sub toggleButtons(enabled As Boolean)
        uiTxtFontCode.Enabled = enabled
        uiBtnTemplateDollar.Enabled = enabled
        uiBtnTemplatePremier.Enabled = enabled
        uiBtnTemplateMailback.Enabled = enabled
        uiBtnTemplateBookFront.Enabled = enabled
        uiBtnTemplateBookBack.Enabled = enabled
        uiBtnTemplateBookCover.Enabled = enabled
        uiBtnTemplateCarton.Enabled = enabled
        uiBtnTemplateBizhub.Enabled = enabled
        uiBtnTemplateReturn.Enabled = enabled
        uiBtnTemplateWindow.Enabled = enabled


        uiBtnOpenPngDW.Enabled = enabled
        uiBtnOpenPngKY.Enabled = enabled
        uiBtnOpenPngTN.Enabled = enabled
        uiBtnOpenPngBK.Enabled = enabled
        uiBtnOpenPngUV.Enabled = enabled
        uiBtnOpenPngCN.Enabled = enabled
        uiBtnOpenPngRT.Enabled = enabled

        uiBtnStripinDollar.Enabled = enabled
        'uiBtnStripinBooklet.Enabled = enabled

        uiBtnOpenFontTools.Enabled = enabled
        uiBtnPrintAllFonts.Enabled = enabled
        uiBtnOpenFTPremier.Enabled = enabled
        uiBtnPrintAllPremier.Enabled = enabled
        uiBtnOpenFTReturn.Enabled = enabled
        uiBtnOpenFTWindow.Enabled = enabled
    End Sub


    Private Sub loadPNGFonts()
        Dim folder = uiTxtFolderNumber.Text.Trim

        pngFontList.Clear()
        uiListViewPNGs.Items.Clear()
        uiPictureBoxPNGPreview.Image = Nothing

        '' get the list of copy from this folder into the listview for png preview (suppress errors)
        pngFontList = PNGPreview.getCopyListFromFolderNumber(folder, True)

        For Each env As KeyValuePair(Of String, Dictionary(Of String, String)) In pngFontList
            '' add the items to the list view
            Dim resultItem As New ListViewItem
            '' id
            resultItem.Text = env.Key
            '' code
            resultItem.SubItems.Add(env.Value.Item("fontCode"))
            '' title
            resultItem.SubItems.Add(env.Value.Item("fontTitle"))
            '' add the listview item to the listview
            uiListViewPNGs.Items.Add(resultItem)
        Next

        '' if there were any results, select the first one
        If (uiListViewPNGs.Items.Count > 0) Then uiListViewPNGs.Items(0).Selected = True

    End Sub

    Private Sub showPNGImage(ByVal id As String)
        Dim imageInfo As Dictionary(Of String, String) = pngFontList.Item(id)
        '' if the image isn't blank...
        If Not (imageInfo.Item("image") = "") Then
            uiPictureBoxPNGPreview.Image = PNGPreview.getBitmapFromBase64(imageInfo.Item("image"))
        Else
            '' if the image is empty, clear out the image in the display
            uiPictureBoxPNGPreview.Image = Nothing
        End If
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' event handlers
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub uiTxtFolderNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles uiTxtFolderNumber.KeyDown

        '' any time the text is changing (or even on pressing return) disable the create copy
        '' buttons so they can't create copy for a partial folder number
        toggleButtons(False)

        '' only search if they pressed enter
        If Not (e.KeyCode = Keys.Return) Then
            Exit Sub
        End If

        '' prevent 'ding' sound when pressing enter in text box
        e.SuppressKeyPress = True

        '' highlight the text in the box
        uiTxtFolderNumber.SelectAll()


        '' clear the lists
        copyFileList.Clear()
        proofFileList.Clear()
        uiLstDesignFiles.Items.Clear()
        uiLstProofFiles.Items.Clear()

        uiTxtFontCode.Text = ""

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
            toggleButtons(True)

            '' set off the PNG Preview search
            Try
                loadPNGFonts()
            Catch ex As Exception
                '' don't worry about an exception
            End Try


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


    Private Sub openFontTool(ByVal folder As String, Optional ByVal character As String = "", Optional ByVal prepend As Boolean = True)
        If (folder <> "" And folder.Length = 5 And Not company = CompanyTypes.MonthlyMail) Or _
           (folder <> "" And folder.Length = 4 And company = CompanyTypes.MonthlyMail) Then
            Dim cmd As String = ""
            If (prepend) Then
                cmd = My.Settings.FontToolsPath & " /o=" & character & folder
            Else
                cmd = My.Settings.FontToolsPath & " /o=" & folder & character
            End If

            Call Shell(cmd, AppWinStyle.MaximizedFocus)
        Else
            MessageBox.Show("You must input a proper folder number before opening Font Tools")
        End If
    End Sub

    Private Sub printFonttool(ByVal folder As String, Optional ByVal character As String = "", Optional ByVal prepend As Boolean = True)
        If (folder <> "" And folder.Length = 5 And Not company = CompanyTypes.MonthlyMail) Or _
           (folder <> "" And folder.Length = 4 And company = CompanyTypes.MonthlyMail) Then
            Dim cmd As String = ""
            If (prepend) Then
                cmd = My.Settings.FontToolsPath & " /o=" & character & folder & " /p"
            Else
                cmd = My.Settings.FontToolsPath & " /o=" & folder & character & " /p"
            End If
            Call Shell(cmd, AppWinStyle.MaximizedFocus)
        Else
            MessageBox.Show("You must input a proper folder number before printing fonts")
        End If
    End Sub

    Private Sub uiBtnOpenFontTools_Click(sender As Object, e As EventArgs) Handles uiBtnOpenFontTools.Click
        openFontTool(uiTxtFolderNumber.Text)
    End Sub

    Private Sub uiBtnPrintAllFonts_Click(sender As Object, e As EventArgs) Handles uiBtnPrintAllFonts.Click
        printFonttool(uiTxtFolderNumber.Text)
    End Sub

    Private Sub uiBtnPrintAllPremier_Click(sender As Object, e As EventArgs) Handles uiBtnPrintAllPremier.Click
        printFonttool(uiTxtFolderNumber.Text, "p", True)
    End Sub

    Private Sub uiBtnOpenFTPremier_Click(sender As Object, e As EventArgs) Handles uiBtnOpenFTPremier.Click
        openFontTool(uiTxtFolderNumber.Text, "p", True)
    End Sub

    Private Sub uiBtnOpenFTReturn_Click(sender As Object, e As EventArgs) Handles uiBtnOpenFTReturn.Click
        openFontTool(uiTxtFolderNumber.Text, "r", False)
    End Sub

    Private Sub uiBtnOpenFTWindow_Click(sender As Object, e As EventArgs) Handles uiBtnOpenFTWindow.Click
        openFontTool(uiTxtFolderNumber.Text, "w", False)
    End Sub

    Private Sub uiBtnTemplateDollar_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateDollar.Click
        openTemplate(TemplateTypes.Dollar)
    End Sub

    Private Sub uiBtnTemplatePremier_Click(sender As Object, e As EventArgs) Handles uiBtnTemplatePremier.Click
        openTemplate(TemplateTypes.Premier)
    End Sub

    Private Sub uiBtnTemplateCarton_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateCarton.Click
        openTemplate(TemplateTypes.Carton)
    End Sub

    Private Sub uiBtnTemplateBookFront_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateBookFront.Click
        openTemplate(TemplateTypes.BookletFront)
    End Sub

    Private Sub uiBtnTemplateBookBack_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateBookBack.Click
        openTemplate(TemplateTypes.BookletBack)
    End Sub

    Private Sub uiBtnTemplateBookCover_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateBookCover.Click
        openTemplate(TemplateTypes.BookletCover)
    End Sub

    Private Sub uiBtnBizhub_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateBizhub.Click
        openTemplate(TemplateTypes.BizhubCover)
    End Sub

    Private Sub uiBtnTemplateMailback_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateMailback.Click
        openTemplate(TemplateTypes.Mailback)
    End Sub

    Private Sub uiBtnTemplateReturn_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateReturn.Click
        openTemplate(TemplateTypes.ReturnEnv)
    End Sub

    Private Sub uiBtnTemplateWindow_Click(sender As Object, e As EventArgs) Handles uiBtnTemplateWindow.Click
        openTemplate(TemplateTypes.Window)
    End Sub

    Private Sub CBCopyHelperForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '' refresh the settings on open
        My.Settings.Reload()

        If (My.Settings.isMM) Then
            company = CompanyTypes.MonthlyMail
        End If

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs)
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

    Private Sub uiBtnStripinDollar_Click(sender As Object, e As EventArgs) Handles uiBtnStripinDollar.Click

        Dim folder As String = uiTxtFolderNumber.Text
        Dim cmd As String = ""

        If (folder <> "" And folder.Length = 5 And Not company = CompanyTypes.MonthlyMail) Then

            '' open stripin droplet
            cmd = STRIPINDOLLARPATH & " " & STRIPINDOLLARTEMPLATE
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

            '' open font tools
            cmd = My.Settings.FontToolsPath & " /o=" & folder
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

        ElseIf (folder <> "" And folder.Length = 4 And company = CompanyTypes.MonthlyMail) Then

            '' open stripin droplet
            cmd = STRIPINDOLLARPATH & " " & STRIPINDOLLARTEMPLATE
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

            '' open font tools
            cmd = My.Settings.FontToolsPath & " /o=" & folder
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

        Else
            MessageBox.Show("You must input a proper folder number before opening Font Tools")
        End If

    End Sub

    Private Sub uiBtnStripinBooklet_Click(sender As Object, e As EventArgs)

        Dim folder As String = uiTxtFolderNumber.Text
        Dim cmd As String = ""

        If (folder <> "" And folder.Length = 5 And Not company = CompanyTypes.MonthlyMail) Then

            '' open stripin droplet
            cmd = STRIPINBOOKLETPATH & " " & STRIPINBOOKLETTEMPLATE
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

            '' open png font
            quickOpenPng(QuickOpenPNGFont.JobTypes.Booklet)

        ElseIf (folder <> "" And folder.Length = 4 And company = CompanyTypes.MonthlyMail) Then

            '' open stripin droplet
            cmd = STRIPINBOOKLETPATH & " " & STRIPINBOOKLETTEMPLATE
            Call Shell(cmd, AppWinStyle.MaximizedFocus)

            '' open png font
            quickOpenPng(QuickOpenPNGFont.JobTypes.Booklet)

        Else
            MessageBox.Show("You must input a proper folder number before opening Font Tools")
        End If

    End Sub

    Private Sub uiBtnOpenPngDW_Click(sender As Object, e As EventArgs) Handles uiBtnOpenPngDW.Click
        quickOpenPng(QuickOpenPNGFont.JobTypes.DoubleWide)
    End Sub

    Private Sub uiBtnOpenPngKY_Click(sender As Object, e As EventArgs) Handles uiBtnOpenPngKY.Click
        quickOpenPng(QuickOpenPNGFont.JobTypes.Kyocera)
    End Sub

    Private Sub uiBtnOpenPngTN_Click(sender As Object, e As EventArgs) Handles uiBtnOpenPngTN.Click
        quickOpenPng(QuickOpenPNGFont.JobTypes.NumberTen)
    End Sub

    Private Sub uiBtnOpenPngBK_Click(sender As Object, e As EventArgs) Handles uiBtnOpenPngBK.Click
        quickOpenPng(QuickOpenPNGFont.JobTypes.Booklet)
    End Sub

    Private Sub uiBtnOpenPngUV_Click(sender As Object, e As EventArgs) Handles uiBtnOpenPngUV.Click
        quickOpenPng(QuickOpenPNGFont.JobTypes.UVCover)
    End Sub

    Private Sub uiBtnOpenPngCN_Click(sender As Object, e As EventArgs) Handles uiBtnOpenPngCN.Click
        quickOpenPng(QuickOpenPNGFont.JobTypes.Carton)
    End Sub

    Private Sub uiBtnOpenPngRT_Click(sender As Object, e As EventArgs) Handles uiBtnOpenPngRT.Click
        quickOpenPng(QuickOpenPNGFont.JobTypes.ReturnEnv)
    End Sub


    Private Sub uiBtnSettings_Click(sender As Object, e As EventArgs) Handles uiBtnSettings.Click
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

    Private Sub uiBtnClose_Click(sender As Object, e As EventArgs) Handles uiBtnClose.Click
        Me.Dispose()
    End Sub

    Private Sub uiBtnMinimize_Click(sender As Object, e As EventArgs) Handles uiBtnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' drag to move window functions ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub dragWindowMouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub dragWindowMouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub dragWindowMouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        drag = False
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
        dragWindowMouseDown(sender, e)
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        dragWindowMouseMove(sender, e)
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp
        dragWindowMouseUp(sender, e)
    End Sub


    Private Sub Label10_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label10.MouseDown
        dragWindowMouseDown(sender, e)
    End Sub

    Private Sub Label10_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label10.MouseMove
        dragWindowMouseMove(sender, e)
    End Sub

    Private Sub Label10_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label10.MouseUp
        dragWindowMouseUp(sender, e)
    End Sub

    Private Sub Label11_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label11.MouseDown
        dragWindowMouseDown(sender, e)
    End Sub

    Private Sub Label11_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label11.MouseMove
        dragWindowMouseMove(sender, e)
    End Sub

    Private Sub Label11_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label11.MouseUp
        dragWindowMouseUp(sender, e)
    End Sub


    Private Sub Label12_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label12.MouseDown
        dragWindowMouseDown(sender, e)
    End Sub

    Private Sub Label12_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label12.MouseMove
        dragWindowMouseMove(sender, e)
    End Sub

    Private Sub Label12_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label12.MouseUp
        dragWindowMouseUp(sender, e)
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' end drag to move window functions ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' shortcut button clicks '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub uiBtnShortcutPngFont_Click(sender As Object, e As EventArgs) Handles uiBtnShortcutPngFont.Click
        '' when using PngFont shortcut, copy folder number to clipboard (if not empty)
        If (uiTxtFolderNumber.Text.Trim <> "") Then Clipboard.SetText(uiTxtFolderNumber.Text.Trim)

        Dim cmd As String = My.Settings.PngFontPath
        Dim psi As New ProcessStartInfo(cmd)
        Process.Start(psi)
    End Sub

    Private Sub uiBtnShortcutFontTool_Click(sender As Object, e As EventArgs) Handles uiBtnShortcutFontTool.Click
        '' when using FontTool shortcut, copy folder number to clipboard, if not empty
        If (uiTxtFolderNumber.Text.Trim <> "") Then Clipboard.SetText(uiTxtFolderNumber.Text.Trim)

        Dim cmd As String = My.Settings.FontToolsPath
        Dim psi As New ProcessStartInfo(cmd)
        Process.Start(psi)
    End Sub

    Private Sub uiBtnShortcutPhotoshop_Click(sender As Object, e As EventArgs) Handles uiBtnShortcutPhotoshop.Click
        Dim cmd As String = My.Settings.PhotoshopPath
        Dim psi As New ProcessStartInfo(cmd)
        Process.Start(psi)
    End Sub

    Private Sub uiBtnShortcutQuark_Click(sender As Object, e As EventArgs) Handles uiBtnShortcutQuark.Click
        Dim cmd As String = My.Settings.QuarkPath
        Dim psi As New ProcessStartInfo(cmd)
        Process.Start(psi)
    End Sub

    Private Sub uiBtnShortcutColorPrint_Click(sender As Object, e As EventArgs) Handles uiBtnShortcutColorPrint.Click
        Dim cmd As String = My.Settings.ColorPrintPath
        'Call Shell(cmd, AppWinStyle.MaximizedFocus)
        '' set the working directory and start the process
        Dim psi As New ProcessStartInfo(cmd)
        psi.WorkingDirectory = IO.Path.GetDirectoryName(cmd)
        Process.Start(psi)
    End Sub

    Private Sub uiBtnShortcutWinshell_Click(sender As Object, e As EventArgs) Handles uiBtnShortcutWinshell.Click
        Dim cmd As String = My.Settings.WinshellPath
        Dim psi As New ProcessStartInfo(cmd)
        Process.Start(psi)
    End Sub

    Private Sub uiBtnShortcutDWC_Click(sender As Object, e As EventArgs) Handles uiBtnShortcutDWC.Click
        Dim cmd As String = My.Settings.DWCPath
        Dim psi As New ProcessStartInfo(cmd)
        Process.Start(psi)
    End Sub

    Private Sub uiBtnShortcutJobBuilder_Click(sender As Object, e As EventArgs) Handles uiBtnShortcutJobBuilder.Click
        Dim cmd As String = My.Settings.JobBuilderPath
        Dim psi As New ProcessStartInfo(cmd)
        Process.Start(psi)
    End Sub

    Private Sub uiBtnViewPNGs_Click(sender As Object, e As EventArgs) Handles uiBtnViewPNGs.Click
        Dim pngFrm As New PNGImages()
        pngFrm.FolderNum = uiTxtFolderNumber.Text.Trim
        '' show as a modal dialog, so this doesn't return until the dialog is closed
        pngFrm.ShowDialog()

    End Sub

    Private Sub uiListViewPNGs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uiListViewPNGs.SelectedIndexChanged
        '' make sure one and only one item is selected (is multiselect=false, so this should be true)
        If (uiListViewPNGs.SelectedItems.Count() = 1) Then
            Dim id As String = uiListViewPNGs.SelectedItems(0).Text

            showPNGImage(id)

        End If

    End Sub



    Private Sub CBCopyHelperForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        '' if they pressed control plus c
        If (e.Control AndAlso e.KeyCode = Keys.C) Then
            '' and there's a selected image
            If (uiListViewPNGs.SelectedItems.Count = 1) Then
                PNGPreview.copyBitmapToClipboardPNG(uiPictureBoxPNGPreview.Image)
            End If
        End If
    End Sub


    Private Sub uiBtnPrintPNGPreview_Click(sender As Object, e As EventArgs) Handles uiBtnPrintPNGPreview.Click
        PrintDocument.OriginAtMargins = True

        '' set portrait / landscape based on uiPictureBox dimensions
        Dim bmp As Bitmap = uiPictureBoxPNGPreview.Image
        If (bmp.Width > bmp.Height) Then PrintDocument.DefaultPageSettings.Landscape = True
        PrintPreviewDialog.ShowDialog()
    End Sub


    Private Sub PrintDocument_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument.PrintPage
        Dim bmp As Bitmap = uiPictureBoxPNGPreview.Image

        Dim scaleX As Double = e.MarginBounds.Width / bmp.Width
        Dim scaleY As Double = e.MarginBounds.Height / bmp.Height

        '' scale by amount needed to fit page
        Dim scale As Double = If(scaleX < scaleY, scaleX, scaleY)

        e.Graphics.DrawImage(bmp, 0, 0, CInt(bmp.Width * scale), CInt(bmp.Height * scale))

    End Sub

    
End Class
