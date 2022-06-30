Imports System
Imports System.IO
Imports Nevron.Nov.Dom
Imports Nevron.Nov.IO
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Formats
Imports Nevron.Nov.UI
Imports System.Runtime.InteropServices

Namespace Nevron.Nov.Examples.Text
    Public Class NHtmlImportExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor.
		''' </summary>
		Public Sub New()
        End Sub

		''' <summary>
		''' Static constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Text.NHtmlImportExample.NHtmlImportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NHtmlImportExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim splitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter()
            splitter.SplitMode = Nevron.Nov.UI.ENSplitterSplitMode.Proportional
            splitter.SplitFactor = 0.5

			' Create the "HTML Code" group box
			Me.m_HtmlTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_HtmlTextBox.AcceptsEnter = True
            Me.m_HtmlTextBox.AcceptsTab = True
            Me.m_HtmlTextBox.Multiline = True
            Me.m_HtmlTextBox.WordWrap = False
            Me.m_HtmlTextBox.VScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Me.m_HtmlTextBox.HScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Dim importButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Import")
            importButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler importButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnImportButtonClick)
            Dim pairBox As Nevron.Nov.UI.NPairBox = Nevron.Nov.Examples.Text.NHtmlImportExample.CreatePairBox(Me.m_HtmlTextBox, importButton)
            splitter.Pane1.Content = New Nevron.Nov.UI.NGroupBox("HTML Code", pairBox)

			' Create the "Preview" group box
			Me.m_PreviewRichText = New Nevron.Nov.Text.NRichTextView()
            Me.m_PreviewRichText.[ReadOnly] = True
            AddHandler Me.m_PreviewRichText.DocumentLoaded, AddressOf Me.OnRichTextDocumentLoaded
            splitter.Pane2.Content = New Nevron.Nov.UI.NGroupBox("Preview", Me.m_PreviewRichText)
            Return splitter
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.VerticalSpacing = 10
            Me.m_ElapsedTimeLabel = New Nevron.Nov.UI.NLabel()
            stack.Add(Me.CreatePredefinedPageGroupBox())
#IfNot SILVERLIGHT
			stack.Add(Me.CreateNavigationGroupBox())
#EndIf
			stack.Add(Me.m_ElapsedTimeLabel)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the HTML import capabilities of the Nevron Rich Text widget. Simply select one of the preloaded examples
	from the combo box to the right and see it imported. You can also edit the source of the HTML code and press the <b>Import</b>
	button when ready.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreatePredefinedPageGroupBox() As Nevron.Nov.UI.NGroupBox
            Const HtmlSuitePrefix As String = "RSTR_HtmlSuite_"
            Dim testListBox As Nevron.Nov.UI.NListBox = New Nevron.Nov.UI.NListBox()
            Dim resourceName As String() = Nevron.Nov.Text.NResources.Instance.GetResourceNames()
            Dim i As Integer = 0, count As Integer = resourceName.Length

            While i < count
                Dim resName As String = resourceName(i)

                If resName.StartsWith(HtmlSuitePrefix, System.StringComparison.Ordinal) Then
					' The current resource is an HTML page, so add it to the list box
					Dim testName As String = resName.Substring(HtmlSuitePrefix.Length, resName.Length - HtmlSuitePrefix.Length - 5)
                    Dim item As Nevron.Nov.UI.NListBoxItem = New Nevron.Nov.UI.NListBoxItem(Nevron.Nov.NStringHelpers.InsertSpacesBeforeUppersAndDigits(testName))
                    item.Tag = resName
                    testListBox.Items.Add(item)
                End If

                i += 1
            End While

            AddHandler testListBox.Selection.Selected, AddressOf Me.OnListBoxItemSelected
            testListBox.Selection.SingleSelect(testListBox.Items(0))
            Return New Nevron.Nov.UI.NGroupBox("Predefined HTML pages", testListBox)
        End Function

        Private Function CreateNavigationGroupBox() As Nevron.Nov.UI.NGroupBox
			' Create the navigation pair box
			Me.m_NavigationTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_NavigationTextBox.Text = Nevron.Nov.Examples.Text.NHtmlImportExample.DefaultAddress
            Me.m_NavigationTextBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Dim goButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Go")
            goButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            AddHandler goButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGoButtonClick)
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(Me.m_NavigationTextBox, goButton)
            pairBox.FitMode = Nevron.Nov.Layout.ENStackFitMode.First
            pairBox.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
            pairBox.Spacing = 3
            Return New Nevron.Nov.UI.NGroupBox("Import from URL", pairBox)
        End Function

        Private Function TryParseUrl(ByVal url As String, <Out> ByRef uri As Nevron.Nov.NUriBase) As Boolean
            uri = Nothing
            url = Nevron.Nov.NStringHelpers.SafeTrim(url)
            If System.[String].IsNullOrEmpty(url) Then Return False

            If Not url.Contains("://") AndAlso url.IndexOf(":\") <> 1 AndAlso url(0) <> "/"c Then ' URI scheme check
 ' Windows file path check
 ' Unix file path check
				' This URL doesn't have a scheme and is not a file path, so add an HTTP URI scheme
				url = "http://" & url
            End If

            Return Nevron.Nov.NUri.TryCreate(url, Nevron.Nov.ENUriKind.Absolute, uri)
        End Function

        Private Sub LoadSource(ByVal stream As System.IO.Stream)
            Dim bytes As Byte() = Nevron.Nov.IO.NStreamHelpers.ReadToEnd(stream)
            Me.m_HtmlTextBox.Text = Nevron.Nov.Text.NEncoding.UTF8.GetString(bytes)
        End Sub

        Private Sub LoadHtml(ByVal stream As System.IO.Stream, ByVal baseUri As String)
            Me.m_ElapsedTimeLabel.Text = System.[String].Empty
            stream.Position = 0
            Me.m_Stopwatch = Nevron.Nov.NStopwatch.StartNew()
            Dim settings As Nevron.Nov.Text.Formats.NTextLoadSettings = Nothing

            If Not Equals(baseUri, Nothing) Then
                settings = New Nevron.Nov.Text.Formats.NTextLoadSettings()
                settings.BaseUri = New Nevron.Nov.NUri(baseUri)
            End If

            Me.m_PreviewRichText.LoadFromStream(stream, Nevron.Nov.Text.Formats.NTextFormat.Html, settings)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnImportButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Dim bytes As Byte() = Nevron.Nov.Text.NEncoding.UTF8.GetBytes(Me.m_HtmlTextBox.Text)

            Using stream As System.IO.MemoryStream = New System.IO.MemoryStream(bytes)
                Me.LoadHtml(stream, Nothing)
            End Using
        End Sub

        Private Sub OnListBoxItemSelected(ByVal arg1 As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            If arg1.TargetNode Is Nothing Then Return

			' Determine the full name of the selected resource
			Dim resName As String = CStr(arg1.Item.Tag)

			' Read the stream and set it as text of the HTML code text box
			Using stream As System.IO.Stream = Nevron.Nov.Text.NResources.Instance.GetResourceStream(resName)
                Me.LoadSource(stream)
                Me.LoadHtml(stream, Nothing)
            End Using
        End Sub

        Private Sub OnGoButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Dim url As String = Me.m_NavigationTextBox.Text
            If System.[String].IsNullOrEmpty(url) Then Return

			' Normalize the URL
			Dim uri As Nevron.Nov.NUriBase

            If Not Me.TryParseUrl(url, uri) Then
                Call Nevron.Nov.UI.NMessageBox.ShowError("Invalid URL or file path", "Error")
                Return
            End If

            If uri.IsFile Then
				' Load from file
				Dim file As Nevron.Nov.IO.NFile = Nevron.Nov.IO.NFileSystem.Current.GetFile(url)
                file.OpenRead().[Then](Sub(ByVal stream As System.IO.Stream)
                                           Using stream
                                               Me.LoadSource(stream)
                                               Me.LoadHtml(stream, url)
                                           End Using
                                       End Sub)
            ElseIf uri.IsHTTP Then
				' Load from URI
				Try
                    Me.m_Stopwatch = Nevron.Nov.NStopwatch.StartNew()
                    Me.m_PreviewRichText.LoadFromUri(CType(uri, Nevron.Nov.NUri))
                Catch ex As System.Exception
                    Me.m_Stopwatch.[Stop]()
                    Call Nevron.Nov.UI.NMessageBox.ShowError(ex.Message, "Error")
                End Try
            End If
        End Sub

        Private Sub OnRichTextDocumentLoaded(ByVal args As Nevron.Nov.Dom.NEventArgs)
            ' Show the elapsed time
            If Me.m_Stopwatch IsNot Nothing Then
                Me.m_Stopwatch.[Stop]()
                Me.m_ElapsedTimeLabel.Text = "Elapsed time: " & Me.m_Stopwatch.ElapsedMilliseconds & " ms"
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_HtmlTextBox As Nevron.Nov.UI.NTextBox
        Private m_PreviewRichText As Nevron.Nov.Text.NRichTextView
        Private m_NavigationTextBox As Nevron.Nov.UI.NTextBox
        Private m_ElapsedTimeLabel As Nevron.Nov.UI.NLabel
        Private m_Stopwatch As Nevron.Nov.NStopwatch

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NHtmlImportExample.
		''' </summary>
		Public Shared ReadOnly NHtmlImportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreatePairBox(ByVal widget1 As Nevron.Nov.UI.NWidget, ByVal widget2 As Nevron.Nov.UI.NWidget) As Nevron.Nov.UI.NPairBox
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(widget1, widget2, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2)
            pairBox.FitMode = Nevron.Nov.Layout.ENStackFitMode.First
            pairBox.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
            pairBox.Spacing = Nevron.Nov.NDesign.VerticalSpacing
            Return pairBox
        End Function

		#EndRegion

		#Region"Constants"

		Private Const DefaultAddress As String = "http://en.wikipedia.org/wiki/Web_browser"

		#EndRegion
	End Class
End Namespace
