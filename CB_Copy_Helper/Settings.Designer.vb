<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnCancelSettings = New System.Windows.Forms.Button()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.rdioMM = New System.Windows.Forms.RadioButton()
        Me.rdioCB = New System.Windows.Forms.RadioButton()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.uiBtnBrowsePNG = New System.Windows.Forms.Button()
        Me.uiBtnBrowseFontTools = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.uiDlgPaths = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'btnCancelSettings
        '
        Me.btnCancelSettings.Location = New System.Drawing.Point(119, 199)
        Me.btnCancelSettings.Name = "btnCancelSettings"
        Me.btnCancelSettings.Size = New System.Drawing.Size(101, 28)
        Me.btnCancelSettings.TabIndex = 13
        Me.btnCancelSettings.Text = "Cancel"
        Me.btnCancelSettings.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(12, 199)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(101, 28)
        Me.btnSaveSettings.TabIndex = 12
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'rdioMM
        '
        Me.rdioMM.AutoSize = True
        Me.rdioMM.Location = New System.Drawing.Point(136, 12)
        Me.rdioMM.Name = "rdioMM"
        Me.rdioMM.Size = New System.Drawing.Size(84, 17)
        Me.rdioMM.TabIndex = 11
        Me.rdioMM.TabStop = True
        Me.rdioMM.Text = "Monthly Mail"
        Me.rdioMM.UseVisualStyleBackColor = True
        '
        'rdioCB
        '
        Me.rdioCB.AutoSize = True
        Me.rdioCB.Location = New System.Drawing.Point(12, 12)
        Me.rdioCB.Name = "rdioCB"
        Me.rdioCB.Size = New System.Drawing.Size(96, 17)
        Me.rdioCB.TabIndex = 10
        Me.rdioCB.TabStop = True
        Me.rdioCB.Text = "Church Budget"
        Me.rdioCB.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.CB_Copy_Helper.My.MySettings.Default, "PngFontPath", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.TextBox1.Location = New System.Drawing.Point(12, 72)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(359, 20)
        Me.TextBox1.TabIndex = 14
        Me.TextBox1.Text = Global.CB_Copy_Helper.My.MySettings.Default.PngFontPath
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "PNG Font Location:"
        '
        'uiBtnBrowsePNG
        '
        Me.uiBtnBrowsePNG.Location = New System.Drawing.Point(377, 72)
        Me.uiBtnBrowsePNG.Name = "uiBtnBrowsePNG"
        Me.uiBtnBrowsePNG.Size = New System.Drawing.Size(75, 23)
        Me.uiBtnBrowsePNG.TabIndex = 16
        Me.uiBtnBrowsePNG.Text = "Browse"
        Me.uiBtnBrowsePNG.UseVisualStyleBackColor = True
        '
        'uiBtnBrowseFontTools
        '
        Me.uiBtnBrowseFontTools.Location = New System.Drawing.Point(377, 130)
        Me.uiBtnBrowseFontTools.Name = "uiBtnBrowseFontTools"
        Me.uiBtnBrowseFontTools.Size = New System.Drawing.Size(75, 23)
        Me.uiBtnBrowseFontTools.TabIndex = 19
        Me.uiBtnBrowseFontTools.Text = "Browse"
        Me.uiBtnBrowseFontTools.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 111)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Font Tools Location:"
        '
        'TextBox2
        '
        Me.TextBox2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.CB_Copy_Helper.My.MySettings.Default, "FontToolsPath", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.TextBox2.Location = New System.Drawing.Point(12, 130)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(359, 20)
        Me.TextBox2.TabIndex = 17
        Me.TextBox2.Text = Global.CB_Copy_Helper.My.MySettings.Default.FontToolsPath
        '
        'uiDlgPaths
        '
        Me.uiDlgPaths.FileName = "OpenFileDialog1"
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 295)
        Me.Controls.Add(Me.uiBtnBrowseFontTools)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.uiBtnBrowsePNG)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnCancelSettings)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.rdioMM)
        Me.Controls.Add(Me.rdioCB)
        Me.Name = "Settings"
        Me.Text = "Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancelSettings As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents rdioMM As System.Windows.Forms.RadioButton
    Friend WithEvents rdioCB As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents uiBtnBrowsePNG As System.Windows.Forms.Button
    Friend WithEvents uiBtnBrowseFontTools As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents uiDlgPaths As System.Windows.Forms.OpenFileDialog
End Class
