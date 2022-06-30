Imports System.IO
Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.IO
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NFileDialogsExample
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
            Nevron.Nov.Examples.UI.NFileDialogsExample.NFileDialogsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NFileDialogsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_TextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_TextBox.Multiline = True
            Me.m_TextBox.AcceptsEnter = True
            Me.m_TextBox.AcceptsTab = True
            Me.m_TextBox.VScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Me.m_TextBox.Text = "This is a sample text." & Global.Microsoft.VisualBasic.Constants.vbLf & Global.Microsoft.VisualBasic.Constants.vbLf & "You can edit and save it." & Global.Microsoft.VisualBasic.Constants.vbLf & Global.Microsoft.VisualBasic.Constants.vbLf & "You can also load text from a text file."
            Return Me.m_TextBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' create the buttons group
			Dim buttonsGroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Open Dialogs from Buttons")
            stack.Add(buttonsGroup)
            Dim buttonsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            buttonsStack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            buttonsGroup.Content = buttonsStack
            Dim openButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open File...")
            openButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler openButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnOpenButtonClick)
            buttonsStack.Add(openButton)
            Dim openMultiselectButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Choose Multiple Files...")
            openMultiselectButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler openMultiselectButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnMultiselectOpenButtonClick)
            buttonsStack.Add(openMultiselectButton)
            Dim saveButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save to File...")
            saveButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler saveButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSaveButtonClick)
            buttonsStack.Add(saveButton)

			' create the menu group
			Dim menuGroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Open Dialogs from Menu Items")
            stack.Add(menuGroup)
            Dim menuBar As Nevron.Nov.UI.NMenuBar = New Nevron.Nov.UI.NMenuBar()
            menuGroup.Content = menuBar
            Dim fileMenu As Nevron.Nov.UI.NMenuDropDown = New Nevron.Nov.UI.NMenuDropDown("File")
            menuBar.Items.Add(fileMenu)
            Dim openFileMenuItem As Nevron.Nov.UI.NMenuItem = New Nevron.Nov.UI.NMenuItem("Open File...")
            AddHandler openFileMenuItem.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnOpenFileMenuItemClick)
            fileMenu.Items.Add(openFileMenuItem)
            Dim saveFileMenuItem As Nevron.Nov.UI.NMenuItem = New Nevron.Nov.UI.NMenuItem("Save File...")
            AddHandler saveFileMenuItem.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSaveFileMenuItemClick)
            fileMenu.Items.Add(saveFileMenuItem)

			' create the dialog group
			Dim dialogGroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Open Dialogs from Dialog")
            stack.Add(dialogGroup)
            Dim showDialogButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Show Dialog...")
            AddHandler showDialogButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnShowDialogButtonClick)
            dialogGroup.Content = showDialogButton

			' add the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use the open and save file dialogs provided by NOV.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnOpenButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.ShowOpenFileDialog()
        End Sub

        Private Sub OnMultiselectOpenButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.ShowMultiselectOpenFileDialog()
        End Sub

        Private Sub OnSaveButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.ShowSaveFileDialog()
        End Sub

        Private Sub OnOpenFileMenuItemClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Me.ShowOpenFileDialog()
        End Sub

        Private Sub OnSaveFileMenuItemClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Me.ShowSaveFileDialog()
        End Sub

        Private Sub OnShowDialogButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Margins = New Nevron.Nov.Graphics.NMargins(10)
            stack.VerticalSpacing = 10
            Dim openButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open File...")
            openButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler openButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnOpenButtonClick)
            stack.Add(openButton)
            Dim saveButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save to File...")
            saveButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler saveButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSaveButtonClick)
            stack.Add(saveButton)
            Dim closeButtonStrip As Nevron.Nov.UI.NButtonStrip = New Nevron.Nov.UI.NButtonStrip()
            closeButtonStrip.AddCloseButton()
            stack.Add(closeButtonStrip)

			' create a dialog that is owned by this widget window
			Dim dialog As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow()
            dialog.SetupDialogWindow("Show File Dialogs", False)
            dialog.Content = stack
            dialog.Open()
        End Sub

		#EndRegion

		#Region"Implementation - Show Open And Save Dialog"

		Private Sub ShowOpenFileDialog()
            Dim openFileDialog As Nevron.Nov.UI.NOpenFileDialog = New Nevron.Nov.UI.NOpenFileDialog()
            openFileDialog.FileTypes = New Nevron.Nov.UI.NFileDialogFileType() {New Nevron.Nov.UI.NFileDialogFileType("Text Files", "txt"), New Nevron.Nov.UI.NFileDialogFileType("XML Files", "xml"), New Nevron.Nov.UI.NFileDialogFileType("All Files", "*")}
            openFileDialog.SelectedFilterIndex = 0
            openFileDialog.MultiSelect = False
            openFileDialog.InitialDirectory = ""
            openFileDialog.Title = "Open Text File"
            AddHandler openFileDialog.Closed, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NOpenFileDialogResult)(AddressOf Me.OnOpenFileDialogClosed)
            openFileDialog.RequestShow()
        End Sub

        Private Sub OnOpenFileDialogClosed(ByVal result As Nevron.Nov.UI.NOpenFileDialogResult)
            Select Case result.Result
                Case Nevron.Nov.UI.ENCommonDialogResult.OK
                    Dim file As Nevron.Nov.IO.NFile = result.Files(0)
                    file.OpenRead().[Then](Sub(ByVal stream As System.IO.Stream)
                                               Using stream
                                                   Me.m_TextBox.Text = Nevron.Nov.IO.NStreamHelpers.ReadToEndAsString(stream)
                                               End Using

                                               Me.m_EventsLog.LogEvent("File opened: " & file.Name)
                                           End Sub)
                Case Nevron.Nov.UI.ENCommonDialogResult.Cancel
                    Me.m_EventsLog.LogEvent("File not selected")
                Case Nevron.Nov.UI.ENCommonDialogResult.[Error]
                    Me.m_EventsLog.LogEvent("Error message: " & result.ErrorException.Message)
            End Select
        End Sub

        Private Sub ShowMultiselectOpenFileDialog()
            Dim openFileDialog As Nevron.Nov.UI.NOpenFileDialog = New Nevron.Nov.UI.NOpenFileDialog()
            openFileDialog.MultiSelect = True
            openFileDialog.Title = "Select Multiple Files"
            AddHandler openFileDialog.Closed, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NOpenFileDialogResult)(AddressOf Me.OnMultiselectOpenFileDialogClosed)
            openFileDialog.RequestShow()
        End Sub

        Private Sub OnMultiselectOpenFileDialogClosed(ByVal result As Nevron.Nov.UI.NOpenFileDialogResult)
            Select Case result.Result
                Case Nevron.Nov.UI.ENCommonDialogResult.OK
                    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

                    For i As Integer = 0 To result.Files.Length - 1
                        sb.AppendLine(result.Files(CInt((i))).Name)
                    Next

                    Me.m_TextBox.Text = sb.ToString()
                    Me.m_EventsLog.LogEvent("Multiple files selected")
                Case Nevron.Nov.UI.ENCommonDialogResult.Cancel
                    Me.m_EventsLog.LogEvent("File not selected")
                Case Nevron.Nov.UI.ENCommonDialogResult.[Error]
                    Me.m_EventsLog.LogEvent("Error message: " & result.ErrorException.Message)
            End Select
        End Sub

        Private Sub ShowSaveFileDialog()
            Dim saveFileDialog As Nevron.Nov.UI.NSaveFileDialog = New Nevron.Nov.UI.NSaveFileDialog()
            saveFileDialog.FileTypes = New Nevron.Nov.UI.NFileDialogFileType() {New Nevron.Nov.UI.NFileDialogFileType("Text Files", "txt"), New Nevron.Nov.UI.NFileDialogFileType("All Files", "*")}
            saveFileDialog.SelectedFilterIndex = 0
            saveFileDialog.DefaultFileName = "NevronTest"
            saveFileDialog.DefaultExtension = "txt"
            saveFileDialog.Title = "Save File As"
            AddHandler saveFileDialog.Closed, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NSaveFileDialogResult)(AddressOf Me.OnSaveFileDialogClosed)
            saveFileDialog.RequestShow()
        End Sub

        Private Sub OnSaveFileDialogClosed(ByVal result As Nevron.Nov.UI.NSaveFileDialogResult)
            Select Case result.Result
                Case Nevron.Nov.UI.ENCommonDialogResult.OK
                    result.File.Create().[Then](Sub(ByVal stream As System.IO.Stream)
                                                    Using writer As System.IO.StreamWriter = New System.IO.StreamWriter(stream)
                                                        writer.Write(Me.m_TextBox.Text)
                                                    End Using

                                                    Me.m_EventsLog.LogEvent("File saved: " & result.SafeFileName)
                                                End Sub)
                Case Nevron.Nov.UI.ENCommonDialogResult.Cancel
                    Me.m_EventsLog.LogEvent("File not selected")
                Case Nevron.Nov.UI.ENCommonDialogResult.[Error]
                    Me.m_EventsLog.LogEvent("Error Message: " & result.ErrorException.Message)
            End Select
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TextBox As Nevron.Nov.UI.NTextBox
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NFileDialogsExample.
		''' </summary>
		Public Shared ReadOnly NFileDialogsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
