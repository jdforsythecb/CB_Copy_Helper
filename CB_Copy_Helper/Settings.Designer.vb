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
        Me.SuspendLayout()
        '
        'btnCancelSettings
        '
        Me.btnCancelSettings.Location = New System.Drawing.Point(119, 68)
        Me.btnCancelSettings.Name = "btnCancelSettings"
        Me.btnCancelSettings.Size = New System.Drawing.Size(101, 21)
        Me.btnCancelSettings.TabIndex = 13
        Me.btnCancelSettings.Text = "Cancel"
        Me.btnCancelSettings.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(12, 68)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(101, 21)
        Me.btnSaveSettings.TabIndex = 12
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'rdioMM
        '
        Me.rdioMM.AutoSize = True
        Me.rdioMM.Location = New System.Drawing.Point(12, 36)
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
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(232, 103)
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
End Class
