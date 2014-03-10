Public Class Settings

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        My.Settings.isMM = rdioMM.Checked
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Settings.Reload()

        '' restore saved settings
        rdioMM.Checked = My.Settings.isMM

    End Sub

    Private Sub btnCancelSettings_Click(sender As Object, e As EventArgs) Handles btnCancelSettings.Click
        Me.Close()
    End Sub

    Private Sub uiBtnBrowsePNG_Click(sender As Object, e As EventArgs) Handles uiBtnBrowsePNG.Click
        If (uiDlgPaths.ShowDialog <> Windows.Forms.DialogResult.Cancel) Then
            My.Settings.PngFontPath = uiDlgPaths.FileName
            My.Settings.Save()
            My.Settings.Reload()
        End If
    End Sub

    Private Sub uiBtnBrowseFontTools_Click(sender As Object, e As EventArgs) Handles uiBtnBrowseFontTools.Click
        If (uiDlgPaths.ShowDialog <> Windows.Forms.DialogResult.Cancel) Then
            My.Settings.FontToolsPath = uiDlgPaths.FileName
            My.Settings.Save()
            My.Settings.Reload()
        End If
    End Sub

    Private Sub uiBtnBrowsePhotoshop_Click(sender As Object, e As EventArgs) Handles uiBtnBrowsePhotoshop.Click
        If (uiDlgPaths.ShowDialog <> Windows.Forms.DialogResult.Cancel) Then
            My.Settings.PhotoshopPath = uiDlgPaths.FileName
            My.Settings.Save()
            My.Settings.Reload()
        End If
    End Sub

    Private Sub uiBtnBrowseQuark_Click(sender As Object, e As EventArgs) Handles uiBtnBrowseQuark.Click
        If (uiDlgPaths.ShowDialog <> Windows.Forms.DialogResult.Cancel) Then
            My.Settings.QuarkPath = uiDlgPaths.FileName
            My.Settings.Save()
            My.Settings.Reload()
        End If
    End Sub

    Private Sub uiBtnBrowseColorPrint_Click(sender As Object, e As EventArgs) Handles uiBtnBrowseColorPrint.Click
        If (uiDlgPaths.ShowDialog <> Windows.Forms.DialogResult.Cancel) Then
            My.Settings.ColorPrintPath = uiDlgPaths.FileName
            My.Settings.Save()
            My.Settings.Reload()
        End If
    End Sub

    Private Sub uiBtnBrowseWinShell_Click(sender As Object, e As EventArgs) Handles uiBtnBrowseWinShell.Click
        If (uiDlgPaths.ShowDialog <> Windows.Forms.DialogResult.Cancel) Then
            My.Settings.WinshellPath = uiDlgPaths.FileName
            My.Settings.Save()
            My.Settings.Reload()
        End If
    End Sub

    Private Sub uiBtnBrowseDWC_Click(sender As Object, e As EventArgs) Handles uiBtnBrowseDWC.Click
        If (uiDlgPaths.ShowDialog <> Windows.Forms.DialogResult.Cancel) Then
            My.Settings.DWCPath = uiDlgPaths.FileName
            My.Settings.Save()
            My.Settings.Reload()
        End If
    End Sub

    Private Sub uiBtnBrowseJobBuilder_Click(sender As Object, e As EventArgs) Handles uiBtnBrowseJobBuilder.Click
        If (uiDlgPaths.ShowDialog <> Windows.Forms.DialogResult.Cancel) Then
            My.Settings.JobBuilderPath = uiDlgPaths.FileName
            My.Settings.Save()
            My.Settings.Reload()
        End If
    End Sub
End Class