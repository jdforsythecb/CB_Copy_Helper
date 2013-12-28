<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CBCopyHelperForm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.uiTxtFolderNumber = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.uiLstDesignFiles = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.uiBtnOpenPNG = New System.Windows.Forms.Button()
        Me.uiBtnOpenFontTools = New System.Windows.Forms.Button()
        Me.uiLstProofFiles = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.uiBtnPrintProof = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.uiTxtFontCode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.uiBtnTemplateDollar = New System.Windows.Forms.Button()
        Me.uiBtnTemplateCarton = New System.Windows.Forms.Button()
        Me.uiBtnTemplatePremier = New System.Windows.Forms.Button()
        Me.uiBtnTemplateBookCover = New System.Windows.Forms.Button()
        Me.uiBtnTemplateMailback = New System.Windows.Forms.Button()
        Me.uiBtnTemplateBookFront = New System.Windows.Forms.Button()
        Me.uiBtnTemplateBizhub = New System.Windows.Forms.Button()
        Me.uiBtnTemplateBookBack = New System.Windows.Forms.Button()
        Me.lblCopyFilesFound = New System.Windows.Forms.Label()
        Me.lblProofFilesFound = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Folder Number:"
        '
        'uiTxtFolderNumber
        '
        Me.uiTxtFolderNumber.Location = New System.Drawing.Point(112, 33)
        Me.uiTxtFolderNumber.Name = "uiTxtFolderNumber"
        Me.uiTxtFolderNumber.Size = New System.Drawing.Size(100, 20)
        Me.uiTxtFolderNumber.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Existing Design Files:"
        '
        'uiLstDesignFiles
        '
        Me.uiLstDesignFiles.FormattingEnabled = True
        Me.uiLstDesignFiles.Location = New System.Drawing.Point(19, 89)
        Me.uiLstDesignFiles.Name = "uiLstDesignFiles"
        Me.uiLstDesignFiles.Size = New System.Drawing.Size(179, 199)
        Me.uiLstDesignFiles.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(201, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 130)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Double-click to open" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(copies folder # to clipboard)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Right-click for explorer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Left-click to copy folder" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "location to clipboard" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click buttons below to op" & _
    "en" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the folder in that program"
        '
        'uiBtnOpenPNG
        '
        Me.uiBtnOpenPNG.Location = New System.Drawing.Point(204, 228)
        Me.uiBtnOpenPNG.Name = "uiBtnOpenPNG"
        Me.uiBtnOpenPNG.Size = New System.Drawing.Size(109, 29)
        Me.uiBtnOpenPNG.TabIndex = 3
        Me.uiBtnOpenPNG.Text = "Open in PNG Font"
        Me.uiBtnOpenPNG.UseVisualStyleBackColor = True
        '
        'uiBtnOpenFontTools
        '
        Me.uiBtnOpenFontTools.Location = New System.Drawing.Point(204, 262)
        Me.uiBtnOpenFontTools.Name = "uiBtnOpenFontTools"
        Me.uiBtnOpenFontTools.Size = New System.Drawing.Size(109, 26)
        Me.uiBtnOpenFontTools.TabIndex = 4
        Me.uiBtnOpenFontTools.Text = "Open in Font Tools"
        Me.uiBtnOpenFontTools.UseVisualStyleBackColor = True
        '
        'uiLstProofFiles
        '
        Me.uiLstProofFiles.FormattingEnabled = True
        Me.uiLstProofFiles.Location = New System.Drawing.Point(19, 350)
        Me.uiLstProofFiles.Name = "uiLstProofFiles"
        Me.uiLstProofFiles.Size = New System.Drawing.Size(179, 199)
        Me.uiLstProofFiles.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 333)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Existing Proofs:"
        '
        'uiBtnPrintProof
        '
        Me.uiBtnPrintProof.Location = New System.Drawing.Point(204, 431)
        Me.uiBtnPrintProof.Name = "uiBtnPrintProof"
        Me.uiBtnPrintProof.Size = New System.Drawing.Size(109, 29)
        Me.uiBtnPrintProof.TabIndex = 6
        Me.uiBtnPrintProof.Text = "Print Proof"
        Me.uiBtnPrintProof.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(201, 350)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 65)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Double-click to open" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Right-click for explorer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click below to print"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(373, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(109, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Create New Copy:"
        '
        'uiTxtFontCode
        '
        Me.uiTxtFontCode.Enabled = False
        Me.uiTxtFontCode.Location = New System.Drawing.Point(449, 89)
        Me.uiTxtFontCode.Name = "uiTxtFontCode"
        Me.uiTxtFontCode.Size = New System.Drawing.Size(100, 20)
        Me.uiTxtFontCode.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(373, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Font Code:"
        '
        'uiBtnTemplateDollar
        '
        Me.uiBtnTemplateDollar.Enabled = False
        Me.uiBtnTemplateDollar.Location = New System.Drawing.Point(376, 121)
        Me.uiBtnTemplateDollar.Name = "uiBtnTemplateDollar"
        Me.uiBtnTemplateDollar.Size = New System.Drawing.Size(83, 23)
        Me.uiBtnTemplateDollar.TabIndex = 8
        Me.uiBtnTemplateDollar.Text = "Dollar"
        Me.uiBtnTemplateDollar.UseVisualStyleBackColor = True
        '
        'uiBtnTemplateCarton
        '
        Me.uiBtnTemplateCarton.Enabled = False
        Me.uiBtnTemplateCarton.Location = New System.Drawing.Point(557, 121)
        Me.uiBtnTemplateCarton.Name = "uiBtnTemplateCarton"
        Me.uiBtnTemplateCarton.Size = New System.Drawing.Size(83, 23)
        Me.uiBtnTemplateCarton.TabIndex = 10
        Me.uiBtnTemplateCarton.Text = "Carton"
        Me.uiBtnTemplateCarton.UseVisualStyleBackColor = True
        '
        'uiBtnTemplatePremier
        '
        Me.uiBtnTemplatePremier.Enabled = False
        Me.uiBtnTemplatePremier.Location = New System.Drawing.Point(465, 121)
        Me.uiBtnTemplatePremier.Name = "uiBtnTemplatePremier"
        Me.uiBtnTemplatePremier.Size = New System.Drawing.Size(83, 23)
        Me.uiBtnTemplatePremier.TabIndex = 9
        Me.uiBtnTemplatePremier.Text = "Premier"
        Me.uiBtnTemplatePremier.UseVisualStyleBackColor = True
        '
        'uiBtnTemplateBookCover
        '
        Me.uiBtnTemplateBookCover.Enabled = False
        Me.uiBtnTemplateBookCover.Location = New System.Drawing.Point(557, 150)
        Me.uiBtnTemplateBookCover.Name = "uiBtnTemplateBookCover"
        Me.uiBtnTemplateBookCover.Size = New System.Drawing.Size(83, 23)
        Me.uiBtnTemplateBookCover.TabIndex = 13
        Me.uiBtnTemplateBookCover.Text = "UV Cover"
        Me.uiBtnTemplateBookCover.UseVisualStyleBackColor = True
        '
        'uiBtnTemplateMailback
        '
        Me.uiBtnTemplateMailback.Enabled = False
        Me.uiBtnTemplateMailback.Location = New System.Drawing.Point(376, 179)
        Me.uiBtnTemplateMailback.Name = "uiBtnTemplateMailback"
        Me.uiBtnTemplateMailback.Size = New System.Drawing.Size(83, 23)
        Me.uiBtnTemplateMailback.TabIndex = 16
        Me.uiBtnTemplateMailback.Text = "Mailback"
        Me.uiBtnTemplateMailback.UseVisualStyleBackColor = True
        '
        'uiBtnTemplateBookFront
        '
        Me.uiBtnTemplateBookFront.Enabled = False
        Me.uiBtnTemplateBookFront.Location = New System.Drawing.Point(376, 150)
        Me.uiBtnTemplateBookFront.Name = "uiBtnTemplateBookFront"
        Me.uiBtnTemplateBookFront.Size = New System.Drawing.Size(83, 23)
        Me.uiBtnTemplateBookFront.TabIndex = 11
        Me.uiBtnTemplateBookFront.Text = "Booklet Front"
        Me.uiBtnTemplateBookFront.UseVisualStyleBackColor = True
        '
        'uiBtnTemplateBizhub
        '
        Me.uiBtnTemplateBizhub.Enabled = False
        Me.uiBtnTemplateBizhub.Location = New System.Drawing.Point(465, 179)
        Me.uiBtnTemplateBizhub.Name = "uiBtnTemplateBizhub"
        Me.uiBtnTemplateBizhub.Size = New System.Drawing.Size(83, 23)
        Me.uiBtnTemplateBizhub.TabIndex = 15
        Me.uiBtnTemplateBizhub.Text = "Bizhub Cover"
        Me.uiBtnTemplateBizhub.UseVisualStyleBackColor = True
        '
        'uiBtnTemplateBookBack
        '
        Me.uiBtnTemplateBookBack.Enabled = False
        Me.uiBtnTemplateBookBack.Location = New System.Drawing.Point(465, 150)
        Me.uiBtnTemplateBookBack.Name = "uiBtnTemplateBookBack"
        Me.uiBtnTemplateBookBack.Size = New System.Drawing.Size(83, 23)
        Me.uiBtnTemplateBookBack.TabIndex = 12
        Me.uiBtnTemplateBookBack.Text = "Booklet Back"
        Me.uiBtnTemplateBookBack.UseVisualStyleBackColor = True
        '
        'lblCopyFilesFound
        '
        Me.lblCopyFilesFound.AutoSize = True
        Me.lblCopyFilesFound.Location = New System.Drawing.Point(22, 291)
        Me.lblCopyFilesFound.Name = "lblCopyFilesFound"
        Me.lblCopyFilesFound.Size = New System.Drawing.Size(118, 13)
        Me.lblCopyFilesFound.TabIndex = 17
        Me.lblCopyFilesFound.Text = "0 files found in 0 folders"
        '
        'lblProofFilesFound
        '
        Me.lblProofFilesFound.AutoSize = True
        Me.lblProofFilesFound.Location = New System.Drawing.Point(22, 552)
        Me.lblProofFilesFound.Name = "lblProofFilesFound"
        Me.lblProofFilesFound.Size = New System.Drawing.Size(64, 13)
        Me.lblProofFilesFound.TabIndex = 18
        Me.lblProofFilesFound.Text = "0 files found"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(781, 24)
        Me.MenuStrip1.TabIndex = 19
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(61, 20)
        Me.ToolStripMenuItem1.Text = "Settings"
        '
        'CBCopyHelperForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 572)
        Me.Controls.Add(Me.lblProofFilesFound)
        Me.Controls.Add(Me.lblCopyFilesFound)
        Me.Controls.Add(Me.uiBtnTemplateBookBack)
        Me.Controls.Add(Me.uiBtnTemplateBizhub)
        Me.Controls.Add(Me.uiBtnTemplateBookFront)
        Me.Controls.Add(Me.uiBtnTemplateMailback)
        Me.Controls.Add(Me.uiBtnTemplateBookCover)
        Me.Controls.Add(Me.uiBtnTemplatePremier)
        Me.Controls.Add(Me.uiBtnTemplateCarton)
        Me.Controls.Add(Me.uiBtnTemplateDollar)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.uiTxtFontCode)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.uiBtnPrintProof)
        Me.Controls.Add(Me.uiLstProofFiles)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.uiBtnOpenFontTools)
        Me.Controls.Add(Me.uiBtnOpenPNG)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.uiLstDesignFiles)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.uiTxtFolderNumber)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "CBCopyHelperForm"
        Me.Text = "CB Copy Helper"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents uiTxtFolderNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents uiLstDesignFiles As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents uiBtnOpenPNG As System.Windows.Forms.Button
    Friend WithEvents uiBtnOpenFontTools As System.Windows.Forms.Button
    Friend WithEvents uiLstProofFiles As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents uiBtnPrintProof As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents uiTxtFontCode As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents uiBtnTemplateDollar As System.Windows.Forms.Button
    Friend WithEvents uiBtnTemplateCarton As System.Windows.Forms.Button
    Friend WithEvents uiBtnTemplatePremier As System.Windows.Forms.Button
    Friend WithEvents uiBtnTemplateBookCover As System.Windows.Forms.Button
    Friend WithEvents uiBtnTemplateMailback As System.Windows.Forms.Button
    Friend WithEvents uiBtnTemplateBookFront As System.Windows.Forms.Button
    Friend WithEvents uiBtnTemplateBizhub As System.Windows.Forms.Button
    Friend WithEvents uiBtnTemplateBookBack As System.Windows.Forms.Button
    Friend WithEvents lblCopyFilesFound As System.Windows.Forms.Label
    Friend WithEvents lblProofFilesFound As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem

End Class
