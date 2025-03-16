'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'                                                        HIK CAMERA PICTURE DOWNLOAD UTILITY
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Option Strict On
Option Explicit On

Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text

Public Class Form1

	Dim PicName(1000000) As String
	Dim PicAddress(1000000) As String
	Dim PicDTime(1000000) As DateTime
	Dim PicSize(1000000) As String
	Dim PicPlaybackURI(1000000) As String
	Dim PicJpgFilename(1000000) As String
	Dim pictureFiles() As String

	Dim picIndex, newStartNum, newEndNum, pauseStartNum, numPictures As Integer
	Dim filePath As String
	Dim searchFrom As DateTime
	Dim requestCancel, connectActive, downloadActive, playActive As Boolean

	Dim iniFile As FileStream
	Dim cameraCredentials As New NetworkCredential
	Dim cameraImage As Drawing.Image
	Dim timePictureWidth, timePictureHeight As Integer

	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'                                                               MAIN FORM LOAD
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		If Not File.Exists("HikCameraPictureDownloadV2.ini") Then
			UpdateIniFile()
		End If
		iniFile = File.Open("HikCameraPictureDownloadV2.ini", CType(FileAccess.ReadWrite, FileMode))
		Dim iniReader As New StreamReader(iniFile)
		Username.Text = iniReader.ReadLine()
		DownloadFolder.Text = iniReader.ReadLine()
		Dim fileTypesTemp = iniReader.ReadLine()
		IpInfo.Text = iniReader.ReadLine()
		Do While Not iniReader.EndOfStream
			IpInfo.Items.Add(iniReader.ReadLine())
		Loop
		iniFile.Close()
		FileTypes.Text = fileTypesTemp
		UtcOffset.Value = Convert.ToInt32((DateTime.Now - DateTime.UtcNow).TotalHours)
		cameraCredentials.UserName = Username.Text
		cameraCredentials.Password = Password.Text
		timePictureWidth = 1200
		timePictureHeight = 70

	End Sub

	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'                                                               MAIN FORM UNLOAD
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

	Private Sub Form1_Unload(sender As Object, e As EventArgs) Handles MyBase.Closed
		End
	End Sub

	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'                                                             CONNECT BUTTON CLICK
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

	Private Async Sub ConnectButton_Click(sender As Object, e As EventArgs) Handles ConnectButton.Click

		If Not OkToConnect() Then
			Return
		End If
		EnableConnectInputs(False)
		connectActive = True
		requestCancel = False
		CancelButton.Enabled = True
		Me.Cursor = Cursors.AppStarting

		Try                                                                 ' REQUEST CAMERA NAME
			Dim cameraHandler As New HttpClientHandler With {.Credentials = cameraCredentials}
			Dim cameraClient As New HttpClient(cameraHandler) With {.Timeout = TimeSpan.FromSeconds(30)}
			Dim cameraRequest As New HttpRequestMessage With {
				.Method = HttpMethod.Get,
				.RequestUri = New Uri("http://" + IpInfo.Text + "/ISAPI/Streaming/channels/101/capabilities"),
				.Content = New StringContent("", Encoding.UTF8)
			}
			Dim cameraResponse = cameraClient.Send(cameraRequest)
			cameraResponse.EnsureSuccessStatusCode()
			Dim cameraStream = Await cameraResponse.Content.ReadAsStringAsync().ConfigureAwait(True)
			Dim Xml = cameraStream.ToString
			Xml = Replace(Xml, " version=""2.0"" xmlns=""http://www.hikvision.com/ver20/XMLSchema""", "", 1, 1)
			Dim xElement As XElement = XElement.Parse(Xml, options:=LoadOptions.None)
			CameraName.Text = VerifyCameraName(xElement.Descendants("channelName").Value).ToString
			If CameraName.Text = "" Then
				Throw New Exception("Camera has no name.")
			Else
				CameraName.BackColor = Color.LawnGreen
				If Not IpInfo.Items.Contains(IpInfo.Text) Then
					IpInfo.Items.Add(IpInfo.Text)
					UpdateIniFile()
				End If
			End If
		Catch exceptionError As Exception
			MsgBox(String.Format("Error: {0}", exceptionError.Message), vbDefaultButton2, "Error connecting...")
			Disconnect()
			EnableConnectInputs(True)
			Return
		End Try

		filePath = DownloadFolder.Text + "\" + CameraName.Text +            ' CHECK FILES ON DISK PREVIOUSLY
				   "\" + FileTypes.Text
		If Directory.Exists(filePath) Then
			OldCount.Value = Directory.GetFiles(filePath).Length
			If OldCount.Value > 0 Then
				Dim oldestFile = Directory.GetFiles(filePath).OrderByDescending(Function(f) New FileInfo(f).LastWriteTime).Last()
				oldestFile = Replace(oldestFile, filePath + "\" + CameraName.Text + "_", "")
				oldestFile = Replace(oldestFile, ".jpg", "")
				OldStart.Text = StrDateToDT(oldestFile).ToString("yyyy/MM/dd HH:mm:ss")

				Dim newestFile = Directory.GetFiles(filePath).
										   OrderByDescending(Function(f) New FileInfo(f).LastWriteTime).First()

				CameraMainPicture.Load(newestFile) 'Load the picture of just the newest existing file
				LoadPicture(CameraMainPicture.Image, False)

				newestFile = Replace(newestFile, filePath + "\" + CameraName.Text + "_", "")
				newestFile = Replace(newestFile, ".jpg", "")
				OldEnd.Text = StrDateToDT(newestFile).ToString("yyyy/MM/dd HH:mm:ss")

				PicDTime(1) = StrDateToDT(newestFile) 'This is needed to keep the progress bar accurate
				picIndex = CInt(OldCount.Value)
			End If
		End If

		Try                                                                 ' REQUEST NEW PICTURE BATCHES
			If OldEnd.Text <> "" Then
				searchFrom = Convert.ToDateTime(OldEnd.Text)
			Else
				searchFrom = New DateTime(2022, 1, 1, 0, 0, 0, 0)
			End If
			Dim searchTo = Now
			Dim picBatchAmt = 50
			While picBatchAmt = 50
				Dim cameraHandler As New HttpClientHandler With {.Credentials = cameraCredentials}
				Dim cameraClient As New HttpClient(cameraHandler) With {.Timeout = TimeSpan.FromSeconds(30)}
				Dim cameraRequest As New HttpRequestMessage With {
					.Method = HttpMethod.Post,
					.RequestUri = New Uri("http://" + IpInfo.Text + "/ISAPI/contentMgmt/search"),
					.Content = New StringContent("<CMSearchDescription><searchID>ID</searchID><trackIDList>" +
										"<trackID>103</trackID></trackIDList><timeSpanList><timeSpan>" +
										"<startTime>" + HikDate(searchFrom.AddHours(-UtcOffset.Value)) + "</startTime>" +
										"<endTime>" + HikDate(searchTo.AddHours(-UtcOffset.Value)) + "</endTime>" +
										"</timeSpan></timeSpanList><contentTypeList>" +
										"<contentType>metadata</contentType></contentTypeList>" +
										"<maxResults>50</maxResults><searchResultPostion>0</searchResultPostion>" +
										"<metadataList><metadataDescriptor>" +
										"//recordType.meta.std-cgi.com/" + IsapiFileTypeString(FileTypes.Text) +
										"</metadataDescriptor></metadataList></CMSearchDescription>", Encoding.UTF8)
				}
				Dim cameraResponse = cameraClient.Send(cameraRequest)
				Dim cameraStream = Await cameraResponse.Content.ReadAsStringAsync().ConfigureAwait(True)
				Dim Xml = cameraStream.ToString
				Xml = Replace(Xml, " version=""2.0"" xmlns=""http://www.hikvision.com/ver20/XMLSchema""", "", 1, 1)
				Dim xElement As XElement = XElement.Parse(Xml, options:=LoadOptions.None)
				picBatchAmt = CInt(xElement.Descendants("numOfMatches").Value)
				If picBatchAmt = 0 And picIndex = 0 Then
					Throw New Exception("No """ + FileTypes.Text + """ event pictures on " +
										CameraName.Text + " camera memory card.")
				End If

				Dim CMSearchResult = xElement.Descendants("playbackURI")    ' CREATE ARRAY OF PICTURES
				For Each playbackURI As XElement In CMSearchResult
					picIndex += 1
					PicPlaybackURI(picIndex) = playbackURI.Value

					Dim SearchWithinThis = playbackURI.Value
					Dim SearchForThis = "http://"
					Dim FirstChar = SearchWithinThis.IndexOf(SearchForThis)
					SearchForThis = "/ISAPI"
					Dim LastChar = SearchWithinThis.IndexOf(SearchForThis)
					If LastChar = -1 Then
						SearchForThis = "/Streaming"
						LastChar = SearchWithinThis.IndexOf(SearchForThis)
					End If
					PicAddress(picIndex) = Mid(playbackURI.Value, FirstChar + 8, LastChar - FirstChar - 7)

					SearchWithinThis = playbackURI.Value
					SearchForThis = "starttime="
					FirstChar = SearchWithinThis.IndexOf(SearchForThis)
					PicDTime(picIndex) = StrDateToDT(Mid(playbackURI.Value, FirstChar + 11, 8) +
															   Mid(playbackURI.Value, FirstChar + 20, 6))
					PicDTime(picIndex) = PicDTime(picIndex).AddHours(UtcOffset.Value)

					SearchWithinThis = playbackURI.Value
					SearchForThis = "size="
					FirstChar = SearchWithinThis.IndexOf(SearchForThis)
					PicSize(picIndex) = Mid(playbackURI.Value, FirstChar + 6, playbackURI.Value.Length - FirstChar - 4)

					SearchWithinThis = playbackURI.Value
					SearchForThis = "name="
					FirstChar = SearchWithinThis.IndexOf(SearchForThis)
					SearchForThis = "size="
					LastChar = SearchWithinThis.IndexOf(SearchForThis)
					PicName(picIndex) = Mid(playbackURI.Value, FirstChar + 6, LastChar - FirstChar - 6)

					SearchWithinThis = playbackURI.Value
					SearchForThis = "@"
					FirstChar = SearchWithinThis.IndexOf(SearchForThis)
					SearchForThis = "size="
					LastChar = SearchWithinThis.IndexOf(SearchForThis)
					PicJpgFilename(picIndex) = filePath + "\" + CameraName.Text + "_" +
											   PicDTime(picIndex).ToString("yyyyMMddHHmmss") + "_" +
											   PicName(picIndex) + ".jpg"

					If My.Computer.FileSystem.FileExists(PicJpgFilename(picIndex)) Then
						picIndex -= 1
					Else
						NewCount.Value += 1
						If newStartNum = 0 Then
							newStartNum = picIndex
							NewStart.Text = PicDTime(picIndex).ToString("yyyy/MM/dd HH:mm:ss")
							RequestPicture(picIndex) 'Load just first new pic while connecting
							LoadPicture(cameraImage, True)
						End If
						newEndNum = picIndex
						NewEnd.Text = PicDTime(picIndex).ToString("yyyy/MM/dd HH:mm:ss")
						ProgressBar.Value = CInt(100 * (1 - (DateDiff("n", PicDTime(picIndex), searchTo) /
															(DateDiff("n", PicDTime(1), searchTo) + 1))))
						My.Application.DoEvents()
						System.Threading.Thread.Sleep((100 - CpuDelay.Value) * 10)
						If requestCancel = True Then
							Disconnect()
							EnableConnectInputs(True)
							Return
						End If
					End If

					My.Application.DoEvents()
					System.Threading.Thread.Sleep((100 - CpuDelay.Value) * 10)
					If requestCancel = True Then
						Disconnect()
						EnableConnectInputs(True)
						Return
					End If
				Next
				searchFrom = PicDTime(picIndex)
			End While
			If picIndex = OldCount.Value Then
				NewStart.Text = "none"
			End If
			ProgressBar.Value = 0
		Catch exceptionError As Exception
			MsgBox(String.Format("Error: {0}", exceptionError.Message), vbDefaultButton2, "Error Connecting")
			Disconnect()
			EnableConnectInputs(True)
			Return
		End Try

		If NewCount.Value > 0 Then
			DownloadButton.Enabled = True
		ElseIf OldCount.Value > 0 Then
			PlayButton.Enabled = True
		End If
		If NewCount.Value = 0 And OldCount.Value = 0 Then
			Disconnect()
			EnableConnectInputs(True)
		Else
			Me.Cursor = Cursors.Arrow
		End If
		connectActive = False

	End Sub

	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'                                                          DOWNLOAD BUTTON CLICK
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

	Private Sub DownloadButton_Click(sender As Object, e As EventArgs) Handles DownloadButton.Click

		downloadActive = True
		DownloadButton.Enabled = False
		PlayButton.Enabled = False
		requestCancel = False
		CancelButton.Text = "Pause"
		Try
			If Not Directory.Exists(filePath) Then
				My.Computer.FileSystem.CreateDirectory(filePath)
			End If
			If pauseStartNum = 0 Then
				pauseStartNum = newStartNum
			End If
			For picIndex = pauseStartNum To newEndNum

				RequestPicture(picIndex)
				cameraImage.Save(PicJpgFilename(picIndex))
				LoadPicture(cameraImage, True)

				If picIndex = newStartNum And OldStart.Text = "" Then
					OldStart.Text = PicDTime(picIndex).ToString("yyyy/MM/dd HH:mm:ss")
				End If
				OldEnd.Text = PicDTime(picIndex).ToString("yyyy/MM/dd HH:mm:ss")
				OldCount.Value = OldCount.Value + 1
				If picIndex <> newEndNum Then
					NewStart.Text = PicDTime(picIndex + 1).ToString("yyyy/MM/dd HH:mm:ss")
					NewCount.Value = NewCount.Value - 1
					ProgressBar.Value = CInt(100 * (1 - ((newEndNum - picIndex) / (newEndNum - newStartNum))))
				Else
					NewStart.Text = ""
					NewEnd.Text = ""
					NewCount.Value = 0
					newStartNum = 0
					ProgressBar.Value = 0
				End If

				My.Application.DoEvents()
				Threading.Thread.Sleep((100 - CpuDelay.Value) * 10)
				If requestCancel Then
					requestCancel = False
					CancelButton.Text = "Cancel"
					downloadActive = False
					pauseStartNum = picIndex + 1
					DownloadButton.Enabled = True
					Return
				End If
			Next
		Catch exceptionError As Exception
			MsgBox(String.Format("Error: {0}", exceptionError.Message), vbDefaultButton2, "Error downloadActive")
			Disconnect()
			EnableConnectInputs(True)
			Return
		End Try

		NewStart.Text = "none"
		downloadActive = False
		CancelButton.Text = "Cancel"
		If OldCount.Value > 0 Then
			PlayButton.Enabled = True
		End If

	End Sub

	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'                                                         CANCEL BUTTON CLICK
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

	Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
		If playActive Then
			playActive = False
		ElseIf connectActive Then
			requestCancel = True
		ElseIf Not connectActive And Not downloadActive Then
			Disconnect()
			EnableConnectInputs(True)
		Else
			requestCancel = True
		End If
	End Sub

	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'                                                         REQUEST PICTURE FROM CAMERA
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

	Private Async Sub RequestPicture(picIndex As Integer)

		Dim requestRetry As Integer = 0
		Dim noError As Boolean
		While requestRetry <= 4
			requestRetry += 1
			noError = True
			Try
				Dim cameraHandler As New HttpClientHandler With {.Credentials = cameraCredentials}
				Dim cameraClient As New HttpClient(cameraHandler) With {.Timeout = TimeSpan.FromSeconds(30)}
				Dim cameraRequest As New HttpRequestMessage With {
				.Method = HttpMethod.Get,
				.RequestUri = New Uri("http://" + IpInfo.Text + "/ISAPI/contentMgmt/download"),
				.Content = New StringContent("<?xml version='1.0'?><downloadRequest><playbackURI>rtsp://" +
									   PicAddress(picIndex) + "/Streaming/tracks/120?starttime=" +
									   HikDate(PicDTime(picIndex).AddDays(-UtcOffset.Value)) + "&amp;endtime=" +
									   HikDate(PicDTime(picIndex).AddDays(-UtcOffset.Value)) + "&amp;name=" +
									   PicName(picIndex) + "&amp;size=" +
									   PicSize(picIndex) + "</playbackURI></downloadRequest>", Encoding.UTF8)
				}
				Dim cameraResponse = cameraClient.Send(cameraRequest)
				cameraResponse.EnsureSuccessStatusCode()
				Dim cameraByteStream = Await cameraResponse.Content.ReadAsByteArrayAsync().ConfigureAwait(True)
				Dim cameraMemoryStream = New MemoryStream(cameraByteStream)

				cameraImage = Drawing.Image.FromStream(cameraMemoryStream)

			Catch exceptionError As Exception
				If requestRetry = 4 Then
					MsgBox(String.Format("Error: {0}", exceptionError.Message), vbDefaultButton2, "Error Requesting Picture")
					Return
				Else
					noError = False
					My.Application.DoEvents()
					Threading.Thread.Sleep(1000)
				End If
			End Try
			If noError Then Return
		End While

	End Sub

	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'                                                         PLAY BUTTON CLICK
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

	Private Sub PlayButton_Click(sender As Object, e As EventArgs) Handles PlayButton.Click

		If Not playActive Then
			playActive = True
			PlayButton.Text = "Pause"
			PlayScrollBar.Enabled = True
			pictureFiles = IO.Directory.GetFiles(filePath)
			Array.Sort(pictureFiles)
			numPictures = pictureFiles.Length - 1

			For picIndex = 0 To numPictures
				CameraMainPicture.Image.Dispose()
				CameraMainPicture.Load(pictureFiles(picIndex))
				LoadPicture(CameraMainPicture.Image, False)
				PlayScrollBar.Value = CInt(100 * (1 - ((numPictures - picIndex) / numPictures)))
				My.Application.DoEvents()
				System.Threading.Thread.Sleep((100 - CpuDelay.Value) * 10)
				While playActive And PlayButton.Text = "Play"
					If PlayButton.Text = "Pause" Then
						Exit While
					End If
					My.Application.DoEvents()
					System.Threading.Thread.Sleep(50) ' Sleep for 1/20 second waiting for play or cancel (CPU=100% w/o this)
					System.Threading.Thread.Sleep((100 - CpuDelay.Value) * 10)
				End While
				If Not playActive Then
					Disconnect()
					EnableConnectInputs(True)
					Return
				End If
			Next

			PlayScrollBar.Value = 100
			playActive = False
			PlayButton.Text = "Play"
			PlayScrollBar.Enabled = False
		Else
			Select Case PlayButton.Text
				Case "Pause"
					PlayButton.Text = "Play"
				Case "Play"
					PlayButton.Text = "Pause"
			End Select
		End If

	End Sub

	Private Sub PlayScrollBar_Scroll(sender As Object, e As ScrollEventArgs) Handles PlayScrollBar.Scroll
		picIndex = CInt(numPictures * (PlayScrollBar.Value / 100))
		CameraMainPicture.Image.Dispose()
		CameraMainPicture.Load(pictureFiles(picIndex))
		LoadPicture(CameraMainPicture.Image, False)
		PlayScrollBar.Value = CInt(100 * (1 - ((numPictures - picIndex) / numPictures)))
	End Sub

	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'                                                              FUNCTIONS 
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

	Private Sub EnableConnectInputs(TurnOn As Boolean)
		ConnectButton.Enabled = TurnOn
		CancelButton.Enabled = Not TurnOn
		Username.Enabled = TurnOn
		Password.Enabled = TurnOn
		IpInfo.Enabled = TurnOn
		DownloadFolder.Enabled = TurnOn
		FileTypes.Enabled = TurnOn
		UtcOffset.Enabled = TurnOn
		downloadActive = False
		playActive = False
		pauseStartNum = 0
		CancelButton.Text = "Cancel"
		Me.Cursor = Cursors.Arrow
		My.Application.DoEvents()
	End Sub

	Private Sub Disconnect()
		connectActive = False
		DownloadButton.Enabled = False
		PlayButton.Enabled = False
		playActive = False
		PlayButton.Text = "Play"
		PlayScrollBar.Enabled = False
		CameraName.Text = "DISCONNECTED"
		CameraName.BackColor = Color.Firebrick
		CameraMainPicture.Image = Nothing
		CameraTimePicture.Image = Nothing
		ProgressBar.Value = 0
		picIndex = 0
		OldStart.Text = ""
		OldEnd.Text = ""
		OldCount.Value = 0
		NewStart.Text = ""
		NewEnd.Text = ""
		NewCount.Value = 0
		newStartNum = 0
		newEndNum = 0

	End Sub

	Private Function OkToConnect() As Boolean
		If Username.Text = "" Then
			MsgBox("Enter Username to connect", vbDefaultButton2, "Username missing")
			Return False
		End If
		If Password.Text = "" Then
			MsgBox("Enter Password to connect", vbDefaultButton2, "Password missing")
			Return False
		End If
		If IpInfo.Text = "" Then
			MsgBox("Enter IP Address:Port to connect", vbDefaultButton2, "IP Address:Port missing")
			Return False
		End If
		If DownloadFolder.Text = "" Then
			MsgBox("Select Download Folder to connect", vbDefaultButton2, "Download Folder missing")
			Return False
		End If
		Return True
	End Function

	Private Sub UpdateIniFile()
		Dim iniFileText As String = Username.Text + Environment.NewLine +
									DownloadFolder.Text + Environment.NewLine +
									FileTypes.Text + Environment.NewLine +
									IpInfo.Text
		For x = 0 To IpInfo.Items.Count - 1
			iniFileText += Environment.NewLine + IpInfo.Items.Item(x).ToString
		Next
		File.WriteAllText("HikCameraPictureDownloadV2.ini", iniFileText)
	End Sub

	Private Sub DownloadFolder_Click(sender As Object, e As EventArgs) Handles DownloadFolder.Click
		FolderBrowser.InitialDirectory = DownloadFolder.Text
		FolderBrowser.ShowDialog()
		DownloadFolder.Text = FolderBrowser.SelectedPath
		Disconnect()
		UpdateIniFile()
	End Sub

	Private Sub Username_Leave(sender As Object, e As EventArgs) Handles Username.Leave
		cameraCredentials.UserName = Username.Text
		Disconnect()
		UpdateIniFile()
	End Sub

	Private Sub Password_Leave(sender As Object, e As EventArgs) Handles Password.Leave
		cameraCredentials.Password = Password.Text
		Disconnect()
	End Sub

	Private Sub IpInfo_SelectedValueChanged(sender As Object, e As EventArgs) Handles IpInfo.SelectedValueChanged
		Disconnect()
		UpdateIniFile()
	End Sub

	Private Sub FileTypes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FileTypes.SelectedIndexChanged
		Disconnect()
		UpdateIniFile()
	End Sub

	Private Sub GmtOffset_ValueChanged(sender As Object, e As EventArgs) Handles UtcOffset.ValueChanged
		Disconnect()
	End Sub

	Private Function VerifyCameraName(cameraName As String) As String
		'Hikvision warning when setting cameraname: Invalid characters / \ : * ? ' " < > | % 
		'So it's not really needed to catch all these,but just in case some other firmware allows them...
		Return Replace(cameraName, "\", "_").Replace("/", "_").Replace(":", "_").Replace("*", "_").Replace("(", "_").
			   Replace("'", "_").Replace("?", "_").Replace("<", "_").Replace(">", "_").Replace("!", "_").Replace(")", "_").
			   Replace("|", "_").Replace("%", "_").Replace("""", "_").Replace(" ", "_") 'last one here is space !!!
	End Function

	Private Function IsapiFileTypeString(fileTypeString As String) As String
		Select Case fileTypeString
			Case "All Types"
				fileTypeString = "allPic"
			Case "Continuous"
				fileTypeString = "CMR"
			Case "Motion"
				fileTypeString = "MOTION"
			Case "Line Crossing"
				fileTypeString = "LineDetection"
			Case "Intrusion"
				fileTypeString = "FieldDetection"
			Case "Scene Change"
				fileTypeString = "scenechangedetection"
			Case "Region Entrance"
				fileTypeString = "regionEntrance"
			Case "Region Exiting"
				fileTypeString = "regionExiting"
			Case "Alarm"
				fileTypeString = "ALARM"
			Case "Face Detection"
				fileTypeString = "facedetection"
			Case "Unattended Baggage"
				fileTypeString = "unattendedBaggage"
			Case "Object Removal"
				fileTypeString = "attendedBaggage"
		End Select
		Return fileTypeString
	End Function

	Private Function HikDate(normalDate As DateTime) As String  ' takes datetime returns "yyyy-mm-ddThh:mm:ssZ"
		HikDate = normalDate.ToString("yyyy-MM-dd") + "T" + normalDate.ToString("HH:mm:ss") + "Z"
	End Function

	Private Function StrDateToDT(dateStr As String) As DateTime ' takes "yyyymmddhhmmss" returns datetime
		dateStr = Mid(dateStr, 1, 4) + "/" + Mid(dateStr, 5, 2) + "/" + Mid(dateStr, 7, 2) + " " +
				  Mid(dateStr, 9, 2) + ":" + Mid(dateStr, 11, 2) + ":" + Mid(dateStr, 13, 2)
		StrDateToDT = Convert.ToDateTime(dateStr)
	End Function

	Private Function StrDate(aDate As DateTime) As String       ' takes datetime returns "yyyyMMddHHmmss"
		StrDate = aDate.ToString("yyyyMMddHHmmss")
	End Function

	Private Sub LoadPicture(cameraImage As Image, mainAndTime As Boolean) ' This should be changed, only load here use more parameters.
		Dim cameraBitmap As System.Drawing.Bitmap
		Dim cameraGraphics As Graphics
		If mainAndTime Then
			'CameraMainPicture.Image.Dispose()      *********************  NOT NEEDED?
			CameraMainPicture.Image = cameraImage
		End If
		'CameraTimePicture.Image.Dispose() '                                                 check order of stuff here???
		cameraBitmap = CType(cameraImage, Bitmap)
		CameraTimePicture.Image = New Bitmap(timePictureWidth, timePictureHeight)
		cameraGraphics = Graphics.FromImage(CameraTimePicture.Image)
		cameraGraphics.DrawImage(image:=cameraBitmap,
								 destRect:=New Rectangle(x:=0, y:=0, width:=timePictureWidth, height:=timePictureHeight),
								 srcRect:=New Rectangle(x:=0, y:=0, width:=timePictureWidth, height:=timePictureHeight),
								 srcUnit:=GraphicsUnit.Pixel)
	End Sub

End Class