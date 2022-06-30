Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NProgressWindowExample
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
            Nevron.Nov.Examples.UI.NProgressWindowExample.NProgressWindowExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NProgressWindowExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Padding = New Nevron.Nov.Graphics.NMargins(10)
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ShowButton = New Nevron.Nov.UI.NButton("Show Window")
            AddHandler Me.m_ShowButton.Click, AddressOf Me.OnShowButtonClick
            stack.Add(Me.m_ShowButton)
            Me.m_CloseButton = New Nevron.Nov.UI.NButton("Close Window")
            Me.m_CloseButton.Enabled = False
            AddHandler Me.m_CloseButton.Click, AddressOf Me.OnCloseButtonClick
            stack.Add(Me.m_CloseButton)
            Me.m_IncreaseProgressButton = New Nevron.Nov.UI.NButton("Increase Progress")
            Me.m_IncreaseProgressButton.Visibility = Nevron.Nov.UI.ENVisibility.Hidden
            AddHandler Me.m_IncreaseProgressButton.Click, AddressOf Me.OnIncreaseProgressButtonClick
            stack.Add(Me.m_IncreaseProgressButton)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_ProgressHeaderTextBox = New Nevron.Nov.UI.NTextBox("Header Text")
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Header:", Me.m_ProgressHeaderTextBox))
            Me.m_ProgressContentTextBox = New Nevron.Nov.UI.NTextBox("This is the content of the progress window.")
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Content:", Me.m_ProgressContentTextBox))
            Me.m_ProgressFooterTextBox = New Nevron.Nov.UI.NTextBox("Footer Text")
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Footer:", Me.m_ProgressFooterTextBox))
            Me.m_ShowCancelButtonCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_ShowCancelButtonCheckBox.Checked = True
            Me.m_ShowCancelButtonCheckBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Cancel button:", Me.m_ShowCancelButtonCheckBox))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to create, show and close progress windows. They are typically used to indicate progress
during long-running operations. Click the <b>Show Window</b> button to show a progress window and the <b>Close Window</b>
button to close it. Use the controls on the right to configure the window.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnShowButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_ProgressWindow = Nevron.Nov.UI.NProgressWindow.Create(DisplayWindow, Me.m_ProgressHeaderTextBox.Text)
            Me.m_ProgressWindow.Content = If(System.[String].IsNullOrEmpty(Me.m_ProgressContentTextBox.Text), Nothing, New Nevron.Nov.UI.NLabel(Me.m_ProgressContentTextBox.Text))
            Me.m_ProgressWindow.Footer = If(System.[String].IsNullOrEmpty(Me.m_ProgressFooterTextBox.Text), Nothing, New Nevron.Nov.UI.NLabel(Me.m_ProgressFooterTextBox.Text))

            If Me.m_ShowCancelButtonCheckBox.Checked Then
                Me.m_ProgressWindow.ButtonStrip = New Nevron.Nov.UI.NButtonStrip()
                Me.m_ProgressWindow.ButtonStrip.AddCancelButton()
            End If

            Me.m_ProgressWindow.Open()
            Me.m_CloseButton.Enabled = True
            Me.m_IncreaseProgressButton.Enabled = True
            Me.m_IncreaseProgressButton.Visibility = Nevron.Nov.UI.ENVisibility.Visible
        End Sub

        Private Sub OnCloseButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_ProgressWindow.Close()
            Me.m_CloseButton.Enabled = False
        End Sub

        Private Sub OnIncreaseProgressButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_ProgressWindow.ProgressBar.Value += 10

            If Me.m_ProgressWindow.ProgressBar.Value >= Me.m_ProgressWindow.ProgressBar.Maximum Then
                Me.m_IncreaseProgressButton.Enabled = False
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ProgressWindow As Nevron.Nov.UI.NProgressWindow
        Private m_ShowButton As Nevron.Nov.UI.NButton
        Private m_CloseButton As Nevron.Nov.UI.NButton
        Private m_IncreaseProgressButton As Nevron.Nov.UI.NButton

		' Controls
		Private m_ProgressHeaderTextBox As Nevron.Nov.UI.NTextBox
        Private m_ProgressContentTextBox As Nevron.Nov.UI.NTextBox
        Private m_ProgressFooterTextBox As Nevron.Nov.UI.NTextBox
        Private m_ShowCancelButtonCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NProgressWindowExample.
		''' </summary>
		Public Shared ReadOnly NProgressWindowExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
