<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PNGImages
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
        Me.uiTxtPNGFolder = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.uiBtnPNGSearch = New System.Windows.Forms.Button()
        Me.uiListBoxPNGFonts = New System.Windows.Forms.ListBox()
        Me.uiPicBoxEnvelope = New System.Windows.Forms.PictureBox()
        CType(Me.uiPicBoxEnvelope, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'uiTxtPNGFolder
        '
        Me.uiTxtPNGFolder.Location = New System.Drawing.Point(12, 27)
        Me.uiTxtPNGFolder.Name = "uiTxtPNGFolder"
        Me.uiTxtPNGFolder.Size = New System.Drawing.Size(82, 20)
        Me.uiTxtPNGFolder.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Folder #"
        '
        'uiBtnPNGSearch
        '
        Me.uiBtnPNGSearch.Location = New System.Drawing.Point(100, 24)
        Me.uiBtnPNGSearch.Name = "uiBtnPNGSearch"
        Me.uiBtnPNGSearch.Size = New System.Drawing.Size(75, 23)
        Me.uiBtnPNGSearch.TabIndex = 2
        Me.uiBtnPNGSearch.Text = "Find Fonts"
        Me.uiBtnPNGSearch.UseVisualStyleBackColor = True
        '
        'uiListBoxPNGFonts
        '
        Me.uiListBoxPNGFonts.FormattingEnabled = True
        Me.uiListBoxPNGFonts.Location = New System.Drawing.Point(12, 53)
        Me.uiListBoxPNGFonts.Name = "uiListBoxPNGFonts"
        Me.uiListBoxPNGFonts.Size = New System.Drawing.Size(120, 485)
        Me.uiListBoxPNGFonts.TabIndex = 3
        '
        'uiPicBoxEnvelope
        '
        Me.uiPicBoxEnvelope.Location = New System.Drawing.Point(192, 9)
        Me.uiPicBoxEnvelope.Name = "uiPicBoxEnvelope"
        Me.uiPicBoxEnvelope.Size = New System.Drawing.Size(535, 529)
        Me.uiPicBoxEnvelope.TabIndex = 4
        Me.uiPicBoxEnvelope.TabStop = False
        '
        'PNGImages
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 549)
        Me.Controls.Add(Me.uiPicBoxEnvelope)
        Me.Controls.Add(Me.uiListBoxPNGFonts)
        Me.Controls.Add(Me.uiBtnPNGSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.uiTxtPNGFolder)
        Me.Name = "PNGImages"
        Me.Text = "PNGImages"
        CType(Me.uiPicBoxEnvelope, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents uiTxtPNGFolder As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents uiBtnPNGSearch As System.Windows.Forms.Button
    Friend WithEvents uiListBoxPNGFonts As System.Windows.Forms.ListBox
    Friend WithEvents uiPicBoxEnvelope As System.Windows.Forms.PictureBox
End Class
