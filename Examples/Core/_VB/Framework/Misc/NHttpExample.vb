Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.IO
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Networking
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NHttpExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.Framework.NHttpExample.NHttpExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NHttpExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.Add(Me.CreatePredefinedRequestsWidget())
            stack.Add(Me.CreateCustomRequestWidget())
            Me.m_ResponseContentHolder = New Nevron.Nov.UI.NContentHolder()
            stack.Add(Me.m_ResponseContentHolder)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' clear button
			Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Clear Requests")
            button.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler button.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnClearRequestsListBoxButtonClick)
            stack.Add(button)

			' create the requests list box in which we add the submitted requests.
			Me.m_RequestsListBox = New Nevron.Nov.UI.NListBox()
            stack.Add(Me.m_RequestsListBox)
            Return New Nevron.Nov.UI.NGroupBox("Requests", stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
Demonstrates the HTTP protocol wrapper that comes along with. It allows you to make HTTP requests from a single code base.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		#Region"Implementation - User Interface"

		Private Function CreatePredefinedRequestsWidget() As Nevron.Nov.UI.NWidget
            Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Predefined Requests")
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            groupBox.Content = stack
            stack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight

			' get Google logo
			Dim getGoogleLogoButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get Google LOGO")
            AddHandler getGoogleLogoButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.GetGoogleLogoClick)
            stack.Add(getGoogleLogoButton)

			' get Google thml
			Dim getGoogleHtmlButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get Google HTML")
            AddHandler getGoogleHtmlButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.GetGoogleHtmlClick)
            stack.Add(getGoogleHtmlButton)

			' get wikipedia logo
			Dim getWikipediaLogoButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get Wikipedia LOGO")
            AddHandler getWikipediaLogoButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGetWikipediaLogoClick)
            stack.Add(getWikipediaLogoButton)

			' get wikipedia home page HTML
			Dim getWikipediaHtmlButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get Wikipedia HTML")
            AddHandler getWikipediaHtmlButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGetWikipediaHtmlClick)
            stack.Add(getWikipediaHtmlButton)

			' get wikipedia home page HTML
			Dim getNevronPieChartImage As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get Nevron Pie Chart Image")
            AddHandler getNevronPieChartImage.Click, AddressOf Me.OnGetNevronPieChartImageClick
            stack.Add(getNevronPieChartImage)
            Return groupBox
        End Function

        Private Function CreateCustomRequestWidget() As Nevron.Nov.UI.NWidget
            Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Request URI")
            Dim dock As Nevron.Nov.UI.NDockPanel = New Nevron.Nov.UI.NDockPanel()
            groupBox.Content = dock
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("URI:")
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(label, Nevron.Nov.Layout.ENDockArea.Left)
            dock.Add(label)
            Me.m_URLTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_URLTextBox.Padding = New Nevron.Nov.Graphics.NMargins(0, 3, 0, 3)
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(Me.m_URLTextBox, Nevron.Nov.Layout.ENDockArea.Center)
            dock.Add(Me.m_URLTextBox)
            Dim submitButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Submit")
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(submitButton, Nevron.Nov.Layout.ENDockArea.Right)
            AddHandler submitButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSumbitCustomRequestClick)
            dock.Add(submitButton)
            Return groupBox
        End Function

		#EndRegion

		#Region"Implementation - Event Handlers"

		Private Sub GetGoogleLogoClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
			' create a HTTP request for the Google logo and subscribe for Completed event
			Dim googleLogoURI As String = "https://www.google.com//images//srpr//logo3w.png"
            Dim request As Nevron.Nov.Networking.NHttpWebRequest = New Nevron.Nov.Networking.NHttpWebRequest(googleLogoURI)
            request.Headers(Nevron.Nov.Networking.NHttpHeaderFieldName.Accept) = "image/png"
            Me.m_URLTextBox.Text = googleLogoURI

			' create a list box item for the request, prior to submittion and submit the request
			Me.CreateRequestUIItem(request)
            request.Submit()
        End Sub

        Private Sub GetGoogleHtmlClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
			' create a HTTP request for the Google home page
			Dim googleHtmlURI As String = "https://www.google.com"
            Dim request As Nevron.Nov.Networking.NHttpWebRequest = New Nevron.Nov.Networking.NHttpWebRequest(googleHtmlURI)
            Me.m_URLTextBox.Text = googleHtmlURI

			' create a list box item for the request, prior to submition and submit the request
			Me.CreateRequestUIItem(request)
            request.Submit()
        End Sub

        Private Sub OnGetWikipediaLogoClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
			' create a HTTP request for the Wikipedia logo and subscribe for Completed event
			Dim wikipediaLogoURI As String = "https://upload.wikimedia.org//wikipedia//commons//6//63//Wikipedia-logo.png"
            Dim request As Nevron.Nov.Networking.NHttpWebRequest = New Nevron.Nov.Networking.NHttpWebRequest(wikipediaLogoURI)
            Me.m_URLTextBox.Text = wikipediaLogoURI

			' create a list box item for the request, prior to submittion and submit the request
			Me.CreateRequestUIItem(request)
            request.Submit()
        End Sub

        Private Sub OnGetWikipediaHtmlClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
			' create a HTTP request for the Wikipedia home page and subscribe for Completed event
			Dim wikipediaHtmlURI As String = "https://en.wikipedia.org/wiki/Main_Page"
            Dim request As Nevron.Nov.Networking.NHttpWebRequest = New Nevron.Nov.Networking.NHttpWebRequest(wikipediaHtmlURI)
            Me.m_URLTextBox.Text = wikipediaHtmlURI

			' create a list box item for the request, prior to submittion and submit the request
			Me.CreateRequestUIItem(request)
            request.Submit()
        End Sub

        Private Sub OnGetNevronPieChartImageClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' create a HTTP request for the Wikipedia home page and subscribe for Completed event
			Dim nevronPieChartImage As String = "https://www.nevron.com//NIMG.axd?i=Chart//ChartTypes//Pie//3D_pie_cut_edge_ring.png"
            Dim request As Nevron.Nov.Networking.NHttpWebRequest = New Nevron.Nov.Networking.NHttpWebRequest(nevronPieChartImage)
            Me.m_URLTextBox.Text = nevronPieChartImage

			' create a list box item for the request, prior to submittion and submit the request
			Me.CreateRequestUIItem(request)
            request.Submit()
        End Sub

        Private Sub OnSumbitCustomRequestClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Try
				' create a HTTP request for the custom URI and subscribe for Completed event
				Dim uri As String = Me.m_URLTextBox.Text
                Dim request As Nevron.Nov.Networking.NWebRequest

                If Not Nevron.Nov.Networking.NWebRequest.TryCreate(New Nevron.Nov.NUri(uri), request) Then
                    Call Nevron.Nov.UI.NMessageBox.Show("The specified URI string is not valid for a URI request. Expected was an HTTP or File uri.", "Invalid URI", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.[Error])
                    Return
                End If
				
				' create a list box item for the request, prior to submittion and submit the request
				Me.CreateRequestUIItem(request)
                request.Submit()
            Catch ex As System.Exception
                Call Nevron.Nov.UI.NMessageBox.Show("Failed to submit custom request." & Global.Microsoft.VisualBasic.Constants.vbLf & Global.Microsoft.VisualBasic.Constants.vbLf & "Exception was: " & ex.Message, "Failed to submit custom request", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.[Error])
            End Try
        End Sub

        Private Sub OnClearRequestsListBoxButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_RequestsListBox.Items.Clear()
            Me.m_Request2UIItem.Clear()
        End Sub

		#EndRegion

		#Region"Implementation - Requests List"

		''' <summary>
		''' Called when a request is about to be submitted. Adds a new entry in the requests list box.
		''' </summary>
		''' <paramname="request"></param>
		Private Sub CreateRequestUIItem(ByVal request As Nevron.Nov.Networking.NWebRequest)
            Dim item As Nevron.Nov.Examples.Framework.NHttpExample.NUriRequestItem = New Nevron.Nov.Examples.Framework.NHttpExample.NUriRequestItem(request, Me.m_ResponseContentHolder)
            Me.m_RequestsListBox.Items.Add(item.ListBoxItem)
            Me.m_Request2UIItem.Add(request, item)
        End Sub


		#EndRegion

		#EndRegion

		#Region"Fields"

		''' <summary>
		''' A content holder for the content of the last completed request.
		''' </summary>
		Private m_ResponseContentHolder As Nevron.Nov.UI.NContentHolder
		''' <summary>
		''' A text box in which the user enters the URI for a custom request.
		''' </summary>
		Private m_URLTextBox As Nevron.Nov.UI.NTextBox
		''' <summary>
		''' The list in which we add information about the sumbitted requests.
		''' </summary>
		Private m_RequestsListBox As Nevron.Nov.UI.NListBox
		''' <summary>
		''' A map for the requests 2 list box items.
		''' </summary>
		Private m_Request2UIItem As Nevron.Nov.DataStructures.NMap(Of Nevron.Nov.Networking.NWebRequest, Nevron.Nov.Examples.Framework.NHttpExample.NUriRequestItem) = New Nevron.Nov.DataStructures.NMap(Of Nevron.Nov.Networking.NWebRequest, Nevron.Nov.Examples.Framework.NHttpExample.NUriRequestItem)()

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NHttpExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Nested Types"

		Public Class NUriRequestItem
            #Region"Constructors"

            Public Sub New(ByVal request As Nevron.Nov.Networking.NWebRequest, ByVal responseContentHolder As Nevron.Nov.UI.NContentHolder)
                Me.Request = request
                Me.ResponseContentHolder = responseContentHolder
                Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox(New Nevron.Nov.UI.NLabel("URI: " & request.Uri.ToString()))
                groupBox.Header.MaxWidth = 350
                groupBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Fit
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Fit
                groupBox.Content = stack
                Dim hstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                hstack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
                hstack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Fit
                hstack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
                hstack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Equal
                stack.Add(hstack)

				' create progress bar
				Me.ProgressBar = New Nevron.Nov.UI.NProgressBar()
                Me.ProgressBar.PreferredHeight = 20
                Me.ProgressBar.Minimum = 0
                Me.ProgressBar.Maximum = 100
                stack.Add(Me.ProgressBar)

				' create status lable
				Me.StatusLabel = New Nevron.Nov.UI.NLabel()
                Me.StatusLabel.Text = " Status: Submitted"
                stack.Add(Me.StatusLabel)

				' create the abort button.
				Me.AbortButton = New Nevron.Nov.UI.NButton("Abort")
                AddHandler Me.AbortButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAbortRequestButtonClick)
                hstack.Add(Me.AbortButton)

				' create view response headers button
				If TypeOf Me.Request Is Nevron.Nov.Networking.NHttpWebRequest Then
                    Me.ViewHeadersButton = New Nevron.Nov.UI.NButton("View Response Headers")
                    AddHandler Me.ViewHeadersButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnViewResponseHeadersButtonClick)
                    hstack.Add(Me.ViewHeadersButton)
                End If

				' add item
				Dim item As Nevron.Nov.UI.NListBoxItem = New Nevron.Nov.UI.NListBoxItem(groupBox)
                item.BorderThickness = New Nevron.Nov.Graphics.NMargins(2)
                item.Border = Nothing
                Me.ListBoxItem = item

				' hook request events
				AddHandler request.Completed, New Nevron.Nov.[Function](Of Nevron.Nov.Networking.NWebRequestCompletedEventArgs)(AddressOf Me.OnRequestCompleted)
                AddHandler request.StartDownload, AddressOf Me.OnRequestStartDownload
                AddHandler request.DownloadProgress, AddressOf Me.OnRequestDownloadProgress
                AddHandler request.EndDownload, AddressOf Me.OnRequestEndDownload
            End Sub

            #EndRegion

            #Region"Event Handlers - Request"
			 
            Private Sub OnRequestEndDownload(ByVal arg As Nevron.Nov.Networking.NWebRequestDataEventArgs)
                Me.ProgressBar.Value = 100
            End Sub

            Private Sub OnRequestDownloadProgress(ByVal arg As Nevron.Nov.Networking.NWebRequestDataProgressEventArgs)
                Dim factor As Double = CDbl(arg.ProgressLength) / CDbl(arg.DataLength)
                Me.ProgressBar.Value = factor * 100.0R
            End Sub

            Private Sub OnRequestStartDownload(ByVal arg As Nevron.Nov.Networking.NWebRequestDataEventArgs)
                Me.ProgressBar.Value = 0
                Me.StatusLabel.Text = " Status: Downloading Data"
            End Sub

            #EndRegion

            #Region"Event Handlers - Buttons"

            ''' <summary>
            ''' 
            ''' </summary>
            ''' <paramname="args"></param>
            Private Sub OnAbortRequestButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
                Me.Request.Abort()
            End Sub
			''' <summary>
			''' 
			''' </summary>
			''' <paramname="args"></param>
			Private Sub OnViewResponseHeadersButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
                Dim httpResponse As Nevron.Nov.Networking.NHttpResponse = TryCast(Me.Response, Nevron.Nov.Networking.NHttpResponse)
                If httpResponse Is Nothing Then Return

				' create a top level window, setup as a dialog
				Dim window As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow()
                window.SetupDialogWindow(Me.Request.Uri.ToString(), True)

				' create a list box for the headers
				Dim listBox As Nevron.Nov.UI.NListBox = New Nevron.Nov.UI.NListBox()
                window.Content = listBox

				' fill with header fields
				Dim it As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Networking.NHttpHeaderField) = httpResponse.HeaderFields.GetIterator()

                While it.MoveNext()
                    listBox.Items.Add(New Nevron.Nov.UI.NListBoxItem(it.Current.ToString()))
                End While

				' open the window
				window.Open()
            End Sub
			''' <summary>
			''' Called by a NHttpRequest when it has been completed.
			''' </summary>
			''' <paramname="args"></param>
			Private Sub OnRequestCompleted(ByVal args As Nevron.Nov.Networking.NWebRequestCompletedEventArgs)
                Me.Response = CType(args.Response, Nevron.Nov.Networking.NHttpResponse)

				' highlight the completed item in red
				Me.ListBoxItem.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.LightCoral)

				' update the status
				Me.StatusLabel.Text += " Status: " & Me.Response.Status.ToString() & ", Received In: " & (Me.Response.ReceivedAt - Me.Request.SentAt).TotalSeconds.ToString() & " seconds"

				' Disable the Abort button
				Me.AbortButton.Enabled = False

				' Enable the Headers Button
				Me.ViewHeadersButton.Enabled = True

				' update the response content holder
				Select Case args.Response.Status
						' request has been aborted by the user -> do nothing.
						Case Nevron.Nov.Networking.ENAsyncResponseStatus.Aborted
                    Case Nevron.Nov.Networking.ENAsyncResponseStatus.Failed
						' request has failed -> fill content with an error message
						Me.ResponseContentHolder.Content = New Nevron.Nov.UI.NLabel("Request for URI: " & args.Request.Uri.ToString() & " failed. Error was: " & args.Response.ErrorException.ToString())
                    Case Nevron.Nov.Networking.ENAsyncResponseStatus.Succeeded
						' request succeded -> fill content with the response content
						Dim httpResponse As Nevron.Nov.Networking.NHttpResponse = TryCast(Me.Response, Nevron.Nov.Networking.NHttpResponse)

                        If httpResponse IsNot Nothing Then
                            Me.HandleHttpResponse(httpResponse)
                        Else
                            Dim fileResponse As Nevron.Nov.Networking.NFileWebResponse = TryCast(Me.Response, Nevron.Nov.Networking.NFileWebResponse)

                            If fileResponse IsNot Nothing Then
                                Me.HandleFileResponse(fileResponse)
                            End If
                        End If
                End Select
            End Sub

			#EndRegion

			#Region"Implementation - Responses"

			''' <summary>
			''' Handles an HTTP response
			''' </summary>
			''' <paramname="response"></param>
			Private Sub HandleHttpResponse(ByVal httpResponse As Nevron.Nov.Networking.NHttpResponse)
                Try
					' get the Content-Type Http Header field, and split it to portions
					' NOTE: the Content-Type is a multi value field. Values are seperated with the ';' char
					Dim contentType As String = httpResponse.HeaderFields(Nevron.Nov.Networking.NHttpHeaderFieldName.ContentType)
                    Dim contentTypes As String() = contentType.Split(New Char() {";"c})

					' normalize content type values (trim and make lower case)
					For i As Integer = 0 To contentTypes.Length - 1
                        contentTypes(i) = contentTypes(CInt((i))).Trim()
                        contentTypes(i) = contentTypes(CInt((i))).ToLower()
                    Next

					' the first part of the content type is the mime type of the content
					Select Case contentTypes(0)
                        Case "image/png", "image/jpeg", "image/bmp"
                            Dim image As Nevron.Nov.Graphics.NImage = New Nevron.Nov.Graphics.NImage(New Nevron.Nov.Graphics.NBytesImageSource(httpResponse.DataArray))
                            Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(image)
                            Me.ResponseContentHolder.Content = New Nevron.Nov.UI.NScrollContent(imageBox)
                        Case "text/html", "application/json"
                            Dim charSet As String = (If(contentTypes.Length >= 1, contentTypes(1), "charset=utf-8"))
                            Dim html As String = ""

                            Select Case charSet
                                Case "charset=utf-8"
                                    html = Nevron.Nov.Text.NEncoding.UTF8.GetString(httpResponse.DataArray)
                                Case Else
                                    html = Nevron.Nov.Text.NEncoding.UTF8.GetString(httpResponse.DataArray)
                            End Select

                            Dim textBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
                            textBox.Text = html
                            Me.ResponseContentHolder.Content = textBox
                        Case Else
                    End Select

                Catch ex As System.Exception
                    Me.ResponseContentHolder.Content = New Nevron.Nov.UI.NLabel("Request for URI: " & Me.Request.Uri.ToString() & " decoding failed. Error was: " & ex.Message.ToString())
                End Try
            End Sub
			''' <summary>
			''' Handles a File response
			''' </summary>
			''' <paramname="fileResponse"></param>
			Private Sub HandleFileResponse(ByVal fileResponse As Nevron.Nov.Networking.NFileWebResponse)
                Dim extension As String = Nevron.Nov.IO.NPath.Current.GetExtension(Me.Request.Uri.GetLocalPath())

                Select Case extension
                    Case "png", "jpeg", "bmp"
                        Dim image As Nevron.Nov.Graphics.NImage = New Nevron.Nov.Graphics.NImage(New Nevron.Nov.Graphics.NBytesImageSource(fileResponse.DataArray))
                        Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(image)
                        Me.ResponseContentHolder.Content = New Nevron.Nov.UI.NScrollContent(imageBox)
                    Case "html", "json", "txt"
                        Dim html As String = Nevron.Nov.Text.NEncoding.UTF8.GetString(fileResponse.DataArray)
                        Dim textBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
                        textBox.Text = html
                        Me.ResponseContentHolder.Content = textBox
                    Case Else
                End Select
            End Sub

			#EndRegion

			#Region"Fields"

			Public ReadOnly ResponseContentHolder As Nevron.Nov.UI.NContentHolder
            Public ReadOnly Request As Nevron.Nov.Networking.NWebRequest
            Public ReadOnly ListBoxItem As Nevron.Nov.UI.NListBoxItem
            Public ReadOnly ProgressBar As Nevron.Nov.UI.NProgressBar
            Public ReadOnly StatusLabel As Nevron.Nov.UI.NLabel
            Public ReadOnly AbortButton As Nevron.Nov.UI.NButton
            Public ReadOnly ViewHeadersButton As Nevron.Nov.UI.NButton
            Public Response As Nevron.Nov.Networking.NWebResponse

			#EndRegion
		End Class

		#EndRegion
	End Class
End Namespace
