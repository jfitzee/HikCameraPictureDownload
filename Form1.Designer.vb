<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
		CameraMainPicture = New PictureBox()
		ConnectButton = New Button()
		CancelButton = New Button()
		DownloadButton = New Button()
		UtcOffset = New NumericUpDown()
		ProgressBar = New ProgressBar()
		DownloadFolder = New TextBox()
		Password = New TextBox()
		Username = New TextBox()
		CpuDelay = New TrackBar()
		OldStart = New TextBox()
		OldEnd = New TextBox()
		CameraTimePicture = New PictureBox()
		NewStart = New TextBox()
		NewEnd = New TextBox()
		Label1 = New Label()
		Label2 = New Label()
		Label3 = New Label()
		Label4 = New Label()
		Label5 = New Label()
		Label6 = New Label()
		Label7 = New Label()
		Label8 = New Label()
		CameraName = New TextBox()
		Label10 = New Label()
		OldCount = New NumericUpDown()
		NewCount = New NumericUpDown()
		IpInfo = New ComboBox()
		FileTypes = New ComboBox()
		Label9 = New Label()
		PlayButton = New Button()
		PlayScrollBar = New HScrollBar()
		FolderBrowser = New FolderBrowserDialog()
		Panel1 = New Panel()
		Panel3 = New Panel()
		Panel2 = New Panel()
		CType(CameraMainPicture, ComponentModel.ISupportInitialize).BeginInit()
		CType(UtcOffset, ComponentModel.ISupportInitialize).BeginInit()
		CType(CpuDelay, ComponentModel.ISupportInitialize).BeginInit()
		CType(CameraTimePicture, ComponentModel.ISupportInitialize).BeginInit()
		CType(OldCount, ComponentModel.ISupportInitialize).BeginInit()
		CType(NewCount, ComponentModel.ISupportInitialize).BeginInit()
		Panel1.SuspendLayout()
		Panel3.SuspendLayout()
		Panel2.SuspendLayout()
		SuspendLayout()
		' 
		' CameraMainPicture
		' 
		CameraMainPicture.BackColor = Color.Gray
		CameraMainPicture.BorderStyle = BorderStyle.Fixed3D
		CameraMainPicture.Dock = DockStyle.Fill
		CameraMainPicture.Location = New Point(0, 42)
		CameraMainPicture.Name = "CameraMainPicture"
		CameraMainPicture.Size = New Size(402, 265)
		CameraMainPicture.SizeMode = PictureBoxSizeMode.StretchImage
		CameraMainPicture.TabIndex = 0
		CameraMainPicture.TabStop = False
		' 
		' ConnectButton
		' 
		ConnectButton.BackColor = SystemColors.Control
		ConnectButton.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		ConnectButton.Location = New Point(231, 490)
		ConnectButton.Name = "ConnectButton"
		ConnectButton.Size = New Size(79, 36)
		ConnectButton.TabIndex = 8
		ConnectButton.Text = "Connect"
		ConnectButton.UseVisualStyleBackColor = False
		' 
		' CancelButton
		' 
		CancelButton.BackColor = SystemColors.Control
		CancelButton.Enabled = False
		CancelButton.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		CancelButton.Location = New Point(325, 490)
		CancelButton.Name = "CancelButton"
		CancelButton.Size = New Size(79, 36)
		CancelButton.TabIndex = 9
		CancelButton.Text = "Cancel"
		CancelButton.UseVisualStyleBackColor = False
		' 
		' DownloadButton
		' 
		DownloadButton.BackColor = SystemColors.Control
		DownloadButton.Enabled = False
		DownloadButton.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		DownloadButton.Location = New Point(421, 490)
		DownloadButton.Name = "DownloadButton"
		DownloadButton.Size = New Size(79, 36)
		DownloadButton.TabIndex = 10
		DownloadButton.Text = "Download"
		DownloadButton.UseVisualStyleBackColor = False
		' 
		' UtcOffset
		' 
		UtcOffset.BorderStyle = BorderStyle.FixedSingle
		UtcOffset.DecimalPlaces = 2
		UtcOffset.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
		UtcOffset.Increment = New Decimal(New Integer() {25, 0, 0, 131072})
		UtcOffset.Location = New Point(82, 6)
		UtcOffset.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
		UtcOffset.Minimum = New Decimal(New Integer() {12, 0, 0, Integer.MinValue})
		UtcOffset.Name = "UtcOffset"
		UtcOffset.Size = New Size(78, 23)
		UtcOffset.TabIndex = 1
		UtcOffset.TextAlign = HorizontalAlignment.Center
		' 
		' ProgressBar
		' 
		ProgressBar.Dock = DockStyle.Top
		ProgressBar.ForeColor = Color.Blue
		ProgressBar.Location = New Point(0, 34)
		ProgressBar.Name = "ProgressBar"
		ProgressBar.Size = New Size(402, 8)
		ProgressBar.TabIndex = 5
		' 
		' DownloadFolder
		' 
		DownloadFolder.BorderStyle = BorderStyle.FixedSingle
		DownloadFolder.Location = New Point(14, 83)
		DownloadFolder.Name = "DownloadFolder"
		DownloadFolder.Size = New Size(405, 23)
		DownloadFolder.TabIndex = 2
		' 
		' Password
		' 
		Password.BorderStyle = BorderStyle.FixedSingle
		Password.Location = New Point(138, 32)
		Password.Name = "Password"
		Password.Size = New Size(117, 23)
		Password.TabIndex = 5
		Password.TextAlign = HorizontalAlignment.Center
		Password.UseSystemPasswordChar = True
		' 
		' Username
		' 
		Username.BorderStyle = BorderStyle.FixedSingle
		Username.Location = New Point(17, 32)
		Username.Name = "Username"
		Username.Size = New Size(104, 23)
		Username.TabIndex = 4
		Username.TextAlign = HorizontalAlignment.Center
		' 
		' CpuDelay
		' 
		CpuDelay.Location = New Point(56, 280)
		CpuDelay.Maximum = 100
		CpuDelay.Name = "CpuDelay"
		CpuDelay.Size = New Size(106, 45)
		CpuDelay.TabIndex = 10
		CpuDelay.TabStop = False
		CpuDelay.TickStyle = TickStyle.None
		CpuDelay.Value = 100
		' 
		' OldStart
		' 
		OldStart.BorderStyle = BorderStyle.FixedSingle
		OldStart.Enabled = False
		OldStart.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		OldStart.Location = New Point(13, 69)
		OldStart.Name = "OldStart"
		OldStart.ReadOnly = True
		OldStart.Size = New Size(149, 23)
		OldStart.TabIndex = 12
		OldStart.TabStop = False
		OldStart.TextAlign = HorizontalAlignment.Center
		' 
		' OldEnd
		' 
		OldEnd.BorderStyle = BorderStyle.FixedSingle
		OldEnd.Enabled = False
		OldEnd.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		OldEnd.Location = New Point(13, 96)
		OldEnd.Name = "OldEnd"
		OldEnd.ReadOnly = True
		OldEnd.Size = New Size(149, 23)
		OldEnd.TabIndex = 13
		OldEnd.TabStop = False
		OldEnd.TextAlign = HorizontalAlignment.Center
		' 
		' CameraTimePicture
		' 
		CameraTimePicture.BackColor = Color.Gray
		CameraTimePicture.BorderStyle = BorderStyle.Fixed3D
		CameraTimePicture.Dock = DockStyle.Top
		CameraTimePicture.ImageLocation = ""
		CameraTimePicture.InitialImage = Nothing
		CameraTimePicture.Location = New Point(0, 0)
		CameraTimePicture.Name = "CameraTimePicture"
		CameraTimePicture.Size = New Size(402, 34)
		CameraTimePicture.SizeMode = PictureBoxSizeMode.StretchImage
		CameraTimePicture.TabIndex = 14
		CameraTimePicture.TabStop = False
		' 
		' NewStart
		' 
		NewStart.BorderStyle = BorderStyle.FixedSingle
		NewStart.Enabled = False
		NewStart.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		NewStart.Location = New Point(13, 179)
		NewStart.Name = "NewStart"
		NewStart.ReadOnly = True
		NewStart.Size = New Size(149, 23)
		NewStart.TabIndex = 15
		NewStart.TabStop = False
		NewStart.TextAlign = HorizontalAlignment.Center
		' 
		' NewEnd
		' 
		NewEnd.BorderStyle = BorderStyle.FixedSingle
		NewEnd.Enabled = False
		NewEnd.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		NewEnd.Location = New Point(13, 206)
		NewEnd.Name = "NewEnd"
		NewEnd.ReadOnly = True
		NewEnd.Size = New Size(149, 23)
		NewEnd.TabIndex = 16
		NewEnd.TabStop = False
		NewEnd.TextAlign = HorizontalAlignment.Center
		' 
		' Label1
		' 
		Label1.AutoSize = True
		Label1.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label1.Location = New Point(50, 52)
		Label1.Name = "Label1"
		Label1.Size = New Size(75, 15)
		Label1.TabIndex = 17
		Label1.Text = "Downloaded"
		' 
		' Label2
		' 
		Label2.AutoSize = True
		Label2.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label2.Location = New Point(24, 162)
		Label2.Name = "Label2"
		Label2.Size = New Size(131, 15)
		Label2.TabIndex = 18
		Label2.Text = "Available for Download"
		' 
		' Label3
		' 
		Label3.AutoSize = True
		Label3.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label3.Location = New Point(17, 10)
		Label3.Name = "Label3"
		Label3.Size = New Size(64, 15)
		Label3.TabIndex = 19
		Label3.Text = "UTC Offset"
		' 
		' Label4
		' 
		Label4.AutoSize = True
		Label4.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label4.Location = New Point(20, 283)
		Label4.Name = "Label4"
		Label4.Size = New Size(40, 15)
		Label4.TabIndex = 20
		Label4.Text = "Speed"
		' 
		' Label5
		' 
		Label5.AutoSize = True
		Label5.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label5.Location = New Point(20, 65)
		Label5.Name = "Label5"
		Label5.Size = New Size(98, 15)
		Label5.TabIndex = 23
		Label5.Text = "Download Folder"
		' 
		' Label6
		' 
		Label6.AutoSize = True
		Label6.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label6.Location = New Point(36, 15)
		Label6.Name = "Label6"
		Label6.Size = New Size(60, 15)
		Label6.TabIndex = 24
		Label6.Text = "Username"
		' 
		' Label7
		' 
		Label7.AutoSize = True
		Label7.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label7.Location = New Point(169, 15)
		Label7.Name = "Label7"
		Label7.Size = New Size(57, 15)
		Label7.TabIndex = 25
		Label7.Text = "Password"
		' 
		' Label8
		' 
		Label8.AutoSize = True
		Label8.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label8.Location = New Point(300, 15)
		Label8.Name = "Label8"
		Label8.Size = New Size(94, 15)
		Label8.TabIndex = 26
		Label8.Text = "IP Address : Port"
		' 
		' CameraName
		' 
		CameraName.BackColor = Color.LawnGreen
		CameraName.BorderStyle = BorderStyle.FixedSingle
		CameraName.Enabled = False
		CameraName.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		CameraName.ForeColor = SystemColors.WindowText
		CameraName.Location = New Point(440, 31)
		CameraName.Name = "CameraName"
		CameraName.ReadOnly = True
		CameraName.Size = New Size(140, 23)
		CameraName.TabIndex = 28
		CameraName.TabStop = False
		CameraName.Text = "NOT CONNECTED"
		CameraName.TextAlign = HorizontalAlignment.Center
		' 
		' Label10
		' 
		Label10.AutoSize = True
		Label10.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label10.Location = New Point(485, 14)
		Label10.Name = "Label10"
		Label10.Size = New Size(47, 15)
		Label10.TabIndex = 29
		Label10.Text = "Camera"
		' 
		' OldCount
		' 
		OldCount.BorderStyle = BorderStyle.FixedSingle
		OldCount.Enabled = False
		OldCount.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		OldCount.Location = New Point(84, 123)
		OldCount.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
		OldCount.Name = "OldCount"
		OldCount.ReadOnly = True
		OldCount.Size = New Size(78, 23)
		OldCount.TabIndex = 30
		OldCount.TextAlign = HorizontalAlignment.Center
		OldCount.ThousandsSeparator = True
		' 
		' NewCount
		' 
		NewCount.BorderStyle = BorderStyle.FixedSingle
		NewCount.Enabled = False
		NewCount.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		NewCount.Location = New Point(84, 233)
		NewCount.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
		NewCount.Name = "NewCount"
		NewCount.ReadOnly = True
		NewCount.Size = New Size(78, 23)
		NewCount.TabIndex = 31
		NewCount.TabStop = False
		NewCount.TextAlign = HorizontalAlignment.Center
		NewCount.ThousandsSeparator = True
		' 
		' IpInfo
		' 
		IpInfo.FormattingEnabled = True
		IpInfo.Location = New Point(272, 32)
		IpInfo.MaxDropDownItems = 100
		IpInfo.Name = "IpInfo"
		IpInfo.Size = New Size(147, 23)
		IpInfo.Sorted = True
		IpInfo.TabIndex = 6
		' 
		' FileTypes
		' 
		FileTypes.FormattingEnabled = True
		FileTypes.ImeMode = ImeMode.NoControl
		FileTypes.Items.AddRange(New Object() {"All Types", "Continuous", "Motion", "Line Crossing", "Intrusion", "Scene Change", "Region Entrance", "Region Exiting", "Alarm", "Face Detection", "Unattended Baggage", "Object Removal"})
		FileTypes.Location = New Point(439, 83)
		FileTypes.Name = "FileTypes"
		FileTypes.Size = New Size(143, 23)
		FileTypes.TabIndex = 3
		' 
		' Label9
		' 
		Label9.AutoSize = True
		Label9.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Label9.Location = New Point(478, 65)
		Label9.Name = "Label9"
		Label9.Size = New Size(64, 15)
		Label9.TabIndex = 39
		Label9.Text = "Event Type"
		' 
		' PlayButton
		' 
		PlayButton.BackColor = SystemColors.Control
		PlayButton.Enabled = False
		PlayButton.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		PlayButton.Location = New Point(133, 490)
		PlayButton.Name = "PlayButton"
		PlayButton.Size = New Size(79, 36)
		PlayButton.TabIndex = 7
		PlayButton.Text = "Play"
		PlayButton.UseVisualStyleBackColor = False
		' 
		' PlayScrollBar
		' 
		PlayScrollBar.Dock = DockStyle.Bottom
		PlayScrollBar.Enabled = False
		PlayScrollBar.Location = New Point(0, 279)
		PlayScrollBar.Name = "PlayScrollBar"
		PlayScrollBar.Size = New Size(402, 28)
		PlayScrollBar.TabIndex = 41
		' 
		' Panel1
		' 
		Panel1.BackColor = Color.Black
		Panel1.BorderStyle = BorderStyle.FixedSingle
		Panel1.Controls.Add(PlayScrollBar)
		Panel1.Controls.Add(CameraMainPicture)
		Panel1.Controls.Add(ProgressBar)
		Panel1.Controls.Add(CameraTimePicture)
		Panel1.Location = New Point(18, 18)
		Panel1.Name = "Panel1"
		Panel1.Size = New Size(404, 309)
		Panel1.TabIndex = 42
		' 
		' Panel3
		' 
		Panel3.BackColor = SystemColors.ControlLight
		Panel3.BorderStyle = BorderStyle.FixedSingle
		Panel3.Controls.Add(OldEnd)
		Panel3.Controls.Add(UtcOffset)
		Panel3.Controls.Add(CpuDelay)
		Panel3.Controls.Add(OldStart)
		Panel3.Controls.Add(NewStart)
		Panel3.Controls.Add(NewEnd)
		Panel3.Controls.Add(NewCount)
		Panel3.Controls.Add(Label1)
		Panel3.Controls.Add(OldCount)
		Panel3.Controls.Add(Label2)
		Panel3.Controls.Add(Label3)
		Panel3.Controls.Add(Label4)
		Panel3.Location = New Point(438, 19)
		Panel3.Name = "Panel3"
		Panel3.Size = New Size(177, 307)
		Panel3.TabIndex = 44
		' 
		' Panel2
		' 
		Panel2.BackColor = SystemColors.ControlLight
		Panel2.BorderStyle = BorderStyle.FixedSingle
		Panel2.Controls.Add(IpInfo)
		Panel2.Controls.Add(DownloadFolder)
		Panel2.Controls.Add(Password)
		Panel2.Controls.Add(Username)
		Panel2.Controls.Add(Label9)
		Panel2.Controls.Add(Label5)
		Panel2.Controls.Add(FileTypes)
		Panel2.Controls.Add(Label6)
		Panel2.Controls.Add(Label7)
		Panel2.Controls.Add(Label10)
		Panel2.Controls.Add(Label8)
		Panel2.Controls.Add(CameraName)
		Panel2.Location = New Point(19, 342)
		Panel2.Name = "Panel2"
		Panel2.Size = New Size(596, 126)
		Panel2.TabIndex = 45
		' 
		' Form1
		' 
		AcceptButton = ConnectButton
		AutoScaleMode = AutoScaleMode.None
		AutoValidate = AutoValidate.EnablePreventFocusChange
		BackColor = SystemColors.ControlLight
		ClientSize = New Size(632, 543)
		Controls.Add(Panel2)
		Controls.Add(Panel3)
		Controls.Add(Panel1)
		Controls.Add(PlayButton)
		Controls.Add(DownloadButton)
		Controls.Add(CancelButton)
		Controls.Add(ConnectButton)
		Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
		ForeColor = Color.Black
		FormBorderStyle = FormBorderStyle.FixedSingle
		Icon = CType(resources.GetObject("$this.Icon"), Icon)
		MaximizeBox = False
		MaximumSize = New Size(1024, 768)
		Name = "Form1"
		StartPosition = FormStartPosition.CenterScreen
		Text = "Hik Camera Picture Download Utility V2.2"
		CType(CameraMainPicture, ComponentModel.ISupportInitialize).EndInit()
		CType(UtcOffset, ComponentModel.ISupportInitialize).EndInit()
		CType(CpuDelay, ComponentModel.ISupportInitialize).EndInit()
		CType(CameraTimePicture, ComponentModel.ISupportInitialize).EndInit()
		CType(OldCount, ComponentModel.ISupportInitialize).EndInit()
		CType(NewCount, ComponentModel.ISupportInitialize).EndInit()
		Panel1.ResumeLayout(False)
		Panel3.ResumeLayout(False)
		Panel3.PerformLayout()
		Panel2.ResumeLayout(False)
		Panel2.PerformLayout()
		ResumeLayout(False)
	End Sub

	Friend WithEvents CameraMainPicture As PictureBox
    Friend WithEvents ConnectButton As Button
    Friend WithEvents CancelButton As Button
    Friend WithEvents DownloadButton As Button
    Friend WithEvents UtcOffset As NumericUpDown
    Friend WithEvents ProgressBar As ProgressBar
    Friend WithEvents DownloadFolder As TextBox
    Friend WithEvents Password As TextBox
    Friend WithEvents Username As TextBox
    Friend WithEvents CpuDelay As TrackBar
    Friend WithEvents OldStart As TextBox
    Friend WithEvents OldEnd As TextBox
    Friend WithEvents CameraTimePicture As PictureBox
    Friend WithEvents NewStart As TextBox
    Friend WithEvents NewEnd As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents CameraName As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents OldCount As NumericUpDown
    Friend WithEvents NewCount As NumericUpDown
    Friend WithEvents IpInfo As ComboBox
	Friend WithEvents FileTypes As ComboBox
	Friend WithEvents Label9 As Label
	Friend WithEvents PlayButton As Button
	Friend WithEvents PlayScrollBar As HScrollBar
	Friend WithEvents FolderBrowser As FolderBrowserDialog
	Friend WithEvents Panel1 As Panel
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel2 As Panel

End Class
