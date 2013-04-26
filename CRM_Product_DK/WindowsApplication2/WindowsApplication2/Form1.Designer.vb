<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.GroupBox1 = New Elegant.Ui.GroupBox
        Me.rbOutBound = New Elegant.Ui.RadioButton
        Me.rbInBound = New Elegant.Ui.RadioButton
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.tbCompany1 = New System.Windows.Forms.TextBox
        Me.tbUserId1 = New System.Windows.Forms.TextBox
        Me.tbUserId2 = New System.Windows.Forms.TextBox
        Me.tbCompany2 = New System.Windows.Forms.TextBox
        Me.tbPwd2 = New System.Windows.Forms.TextBox
        Me.tbPwd1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(57, 38)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "유저정보세팅1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(13, 72)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(57, 38)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "유저정보세팅2"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbOutBound)
        Me.GroupBox1.Controls.Add(Me.rbInBound)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Id = "00592a09-da20-470a-8f62-450bb09e752c"
        Me.GroupBox1.Location = New System.Drawing.Point(4, 130)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(445, 57)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.Text = "GroupBox1"
        '
        'rbOutBound
        '
        Me.rbOutBound.Id = "1b50e8ab-4ee3-4c0a-89e4-742d3003edd9"
        Me.rbOutBound.Location = New System.Drawing.Point(260, 17)
        Me.rbOutBound.Name = "rbOutBound"
        Me.rbOutBound.RadioGroupName = "rgInOut"
        Me.rbOutBound.Size = New System.Drawing.Size(104, 24)
        Me.rbOutBound.TabIndex = 9
        Me.rbOutBound.Text = "OutBound"
        '
        'rbInBound
        '
        Me.rbInBound.Id = "38403df4-1b2b-4598-b447-3f3031d7c4fd"
        Me.rbInBound.Location = New System.Drawing.Point(176, 17)
        Me.rbInBound.Name = "rbInBound"
        Me.rbInBound.RadioGroupName = "rgInOut"
        Me.rbInBound.Size = New System.Drawing.Size(104, 24)
        Me.rbInBound.TabIndex = 8
        Me.rbInBound.Text = "Inbound"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(7, 17)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(146, 21)
        Me.TextBox1.TabIndex = 7
        Me.TextBox1.Text = "01088336128"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(367, 14)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(71, 25)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "고객팝업"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'tbCompany1
        '
        Me.tbCompany1.Location = New System.Drawing.Point(149, 25)
        Me.tbCompany1.Name = "tbCompany1"
        Me.tbCompany1.Size = New System.Drawing.Size(42, 21)
        Me.tbCompany1.TabIndex = 7
        Me.tbCompany1.Text = "8888"
        '
        'tbUserId1
        '
        Me.tbUserId1.Location = New System.Drawing.Point(245, 25)
        Me.tbUserId1.Name = "tbUserId1"
        Me.tbUserId1.Size = New System.Drawing.Size(63, 21)
        Me.tbUserId1.TabIndex = 8
        Me.tbUserId1.Text = "0001"
        '
        'tbUserId2
        '
        Me.tbUserId2.Location = New System.Drawing.Point(245, 75)
        Me.tbUserId2.Name = "tbUserId2"
        Me.tbUserId2.Size = New System.Drawing.Size(63, 21)
        Me.tbUserId2.TabIndex = 10
        Me.tbUserId2.Text = "0003"
        '
        'tbCompany2
        '
        Me.tbCompany2.Location = New System.Drawing.Point(149, 75)
        Me.tbCompany2.Name = "tbCompany2"
        Me.tbCompany2.Size = New System.Drawing.Size(42, 21)
        Me.tbCompany2.TabIndex = 9
        Me.tbCompany2.Text = "8888"
        '
        'tbPwd2
        '
        Me.tbPwd2.Location = New System.Drawing.Point(343, 75)
        Me.tbPwd2.Name = "tbPwd2"
        Me.tbPwd2.Size = New System.Drawing.Size(63, 21)
        Me.tbPwd2.TabIndex = 12
        Me.tbPwd2.Text = "0003"
        '
        'tbPwd1
        '
        Me.tbPwd1.Location = New System.Drawing.Point(343, 25)
        Me.tbPwd1.Name = "tbPwd1"
        Me.tbPwd1.Size = New System.Drawing.Size(63, 21)
        Me.tbPwd1.TabIndex = 11
        Me.tbPwd1.Text = "0001"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(91, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "회사코드"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(91, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "회사코드"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(200, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 12)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "userid"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(200, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 12)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "userid"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(311, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "pwd"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(311, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "pwd"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 257)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbPwd2)
        Me.Controls.Add(Me.tbPwd1)
        Me.Controls.Add(Me.tbUserId2)
        Me.Controls.Add(Me.tbCompany2)
        Me.Controls.Add(Me.tbUserId1)
        Me.Controls.Add(Me.tbCompany1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As Elegant.Ui.GroupBox
    Friend WithEvents rbOutBound As Elegant.Ui.RadioButton
    Friend WithEvents rbInBound As Elegant.Ui.RadioButton
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents tbCompany1 As System.Windows.Forms.TextBox
    Friend WithEvents tbUserId1 As System.Windows.Forms.TextBox
    Friend WithEvents tbUserId2 As System.Windows.Forms.TextBox
    Friend WithEvents tbCompany2 As System.Windows.Forms.TextBox
    Friend WithEvents tbPwd2 As System.Windows.Forms.TextBox
    Friend WithEvents tbPwd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
