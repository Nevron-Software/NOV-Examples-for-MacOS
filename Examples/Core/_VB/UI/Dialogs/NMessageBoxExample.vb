Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NMessageBoxExample
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
            Nevron.Nov.Examples.UI.NMessageBoxExample.NMessageBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NMessageBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.PreferredWidth = 300
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
			
			' Create a text box for the message box title
			Me.m_TitleTextBox = New Nevron.Nov.UI.NTextBox("Message Box")
            stack.Add(Nevron.Nov.Examples.UI.NMessageBoxExample.CreatePairBox("Title:", Me.m_TitleTextBox))

			' Create a text box for the message box content
			Me.m_ContentTextBox = New Nevron.Nov.UI.NTextBox("Here goes the content." & Global.Microsoft.VisualBasic.Constants.vbLf & "It can be multiline.")
            Me.m_ContentTextBox.Multiline = True
            Me.m_ContentTextBox.AcceptsEnter = True
            Me.m_ContentTextBox.AcceptsTab = True
            Me.m_ContentTextBox.PreferredHeight = 100
            Me.m_ContentTextBox.HScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Me.m_ContentTextBox.VScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Me.m_ContentTextBox.WordWrap = False
            stack.Add(Nevron.Nov.Examples.UI.NMessageBoxExample.CreatePairBox("Content:", Me.m_ContentTextBox))

			' Create the message box buttons combo box
			Me.m_ButtonsComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_ButtonsComboBox.FillFromEnum(Of Nevron.Nov.UI.ENMessageBoxButtons)()
            stack.Add(Nevron.Nov.Examples.UI.NMessageBoxExample.CreatePairBox("Buttons:", Me.m_ButtonsComboBox))

			' Create the message box icon combo box
			Me.m_IconComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_IconComboBox.FillFromEnum(Of Nevron.Nov.UI.ENMessageBoxIcon)()
            Me.m_IconComboBox.SelectedIndex = 1
            stack.Add(Nevron.Nov.Examples.UI.NMessageBoxExample.CreatePairBox("Icon:", Me.m_IconComboBox))

			' Create the show button
			Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Show")
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Dim showButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(label)
            AddHandler showButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnShowButtonClick)
            stack.Add(showButton)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
			' Create the events log
			Me.m_EventsLog = New NExampleEventsLog()
            Return Me.m_EventsLog
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create message boxes in NOV. Use the controls at the top to set
	the title, the content and the buttons of the message box.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnShowButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim button As Nevron.Nov.UI.NButton = CType(args.TargetNode, Nevron.Nov.UI.NButton)
            Dim settings As Nevron.Nov.UI.NMessageBoxSettings = New Nevron.Nov.UI.NMessageBoxSettings(Me.m_ContentTextBox.Text, Me.m_TitleTextBox.Text, CType(Me.m_ButtonsComboBox.SelectedIndex, Nevron.Nov.UI.ENMessageBoxButtons), CType(Me.m_IconComboBox.SelectedIndex, Nevron.Nov.UI.ENMessageBoxIcon), Nevron.Nov.UI.ENMessageBoxDefaultButton.Button1, DisplayWindow)                                  ' the message box content
                                    ' the message box title
   ' the button configuration of the message box
         ' the icon to use
						' the default focused button
                                           ' the parent window of the message box
            Call Nevron.Nov.UI.NMessageBox.Show(CType((settings), Nevron.Nov.UI.NMessageBoxSettings)).[Then](Sub(ByVal result As Nevron.Nov.UI.ENWindowResult) Me.m_EventsLog.LogEvent("Message box result: '" & result.ToString() & "'"))     ' delegate that gets called when the message box is closed
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TitleTextBox As Nevron.Nov.UI.NTextBox
        Private m_ContentTextBox As Nevron.Nov.UI.NTextBox
        Private m_ButtonsComboBox As Nevron.Nov.UI.NComboBox
        Private m_IconComboBox As Nevron.Nov.UI.NComboBox
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NMessageBoxExample.
		''' </summary>
		Public Shared ReadOnly NMessageBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreatePairBox(ByVal text As String, ByVal widget As Nevron.Nov.UI.NWidget) As Nevron.Nov.UI.NPairBox
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Right
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            widget.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Return New Nevron.Nov.UI.NPairBox(label, widget, True)
        End Function

		#EndRegion
	End Class
End Namespace
