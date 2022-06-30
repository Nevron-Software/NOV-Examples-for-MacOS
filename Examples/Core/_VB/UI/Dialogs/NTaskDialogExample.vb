Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTaskDialogExample
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
            Nevron.Nov.Examples.UI.NTaskDialogExample.NTaskDialogExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTaskDialogExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Dim messageBoxLikeButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Message Box Like")
            AddHandler messageBoxLikeButton.Click, AddressOf Me.OnMessageBoxLikeButtonClick
            stack.Add(messageBoxLikeButton)
            Dim customButtonsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Custom Buttons")
            AddHandler customButtonsButton.Click, AddressOf Me.OnCustomButtonsButtonClick
            stack.Add(customButtonsButton)
            Dim advancedCustomButtonsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Advanced Custom Buttons")
            AddHandler advancedCustomButtonsButton.Click, AddressOf Me.OnAdvancedCustomButtonsButtonClick
            stack.Add(advancedCustomButtonsButton)
            Dim radioButtonsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Radio Buttons")
            AddHandler radioButtonsButton.Click, AddressOf Me.OnRadioButtonsButtonClick
            stack.Add(radioButtonsButton)
            Dim verificationButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Verification Check Box")
            AddHandler verificationButton.Click, AddressOf Me.OnVerificationButtonClick
            stack.Add(verificationButton)
            Dim allFeaturesButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("All Features")
            AddHandler allFeaturesButton.Click, AddressOf Me.OnAllFeaturesButtonClick
            stack.Add(allFeaturesButton)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Me.m_EventsLog = New NExampleEventsLog()
            Return Me.m_EventsLog
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create task dialogs in NOV. Click any of the buttons above to show
	a preconfigured task dialog, which demonstrates different task dialog features.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnMessageBoxLikeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim taskDialog As Nevron.Nov.UI.NTaskDialog = Nevron.Nov.UI.NTaskDialog.Create(DisplayWindow)
            taskDialog.Title = "Task Dialog"
            taskDialog.Header = New Nevron.Nov.UI.NTaskDialogHeader(Nevron.Nov.UI.ENMessageBoxIcon.Question, "Do you want to save the changes?")
            taskDialog.Buttons = Nevron.Nov.UI.ENTaskDialogButton.Yes Or Nevron.Nov.UI.ENTaskDialogButton.No Or Nevron.Nov.UI.ENTaskDialogButton.Cancel

			' Change the texts of the Yes and No buttons
			Dim stack As Nevron.Nov.UI.NStackPanel = taskDialog.ButtonStrip.GetPredefinedButtonsStack()
            Dim label As Nevron.Nov.UI.NLabel = CType(stack(CInt((0))).GetFirstDescendant(Nevron.Nov.UI.NLabel.NLabelSchema), Nevron.Nov.UI.NLabel)
            label.Text = "Save"
            label = CType(stack(CInt((1))).GetFirstDescendant(Nevron.Nov.UI.NLabel.NLabelSchema), Nevron.Nov.UI.NLabel)
            label.Text = "Don't Save"

			' Subscribe to the Closed event and open the task dialog
			AddHandler taskDialog.Closed, AddressOf Me.OnTaskDialogClosed
            taskDialog.Open()
        End Sub

        Private Sub OnCustomButtonsButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim taskDialog As Nevron.Nov.UI.NTaskDialog = Nevron.Nov.UI.NTaskDialog.Create(DisplayWindow)
            taskDialog.Title = "Task Dialog"
            taskDialog.Header = New Nevron.Nov.UI.NTaskDialogHeader(Nevron.Nov.UI.ENMessageBoxIcon.Information, "This is a task dialog with custom buttons.")
            taskDialog.CustomButtons = New Nevron.Nov.UI.NTaskDialogCustomButtonCollection("Custom Button 1", "Custom Button 2", "Custom Button 3")

			' Subscribe to the Closed event and open the task dialog
			AddHandler taskDialog.Closed, AddressOf Me.OnTaskDialogClosed
            taskDialog.Open()
        End Sub

        Private Sub OnAdvancedCustomButtonsButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim taskDialog As Nevron.Nov.UI.NTaskDialog = Nevron.Nov.UI.NTaskDialog.Create(DisplayWindow)
            taskDialog.Title = "Task Dialog"
            taskDialog.Header = New Nevron.Nov.UI.NTaskDialogHeader(Nevron.Nov.UI.ENMessageBoxIcon.Information, "This is a task dialog with custom buttons.")
            taskDialog.Content = New Nevron.Nov.UI.NLabel("These custom buttons contain a symbol/image, a title and a description.")

			' Create some custom buttons
			taskDialog.CustomButtons = New Nevron.Nov.UI.NTaskDialogCustomButtonCollection()
            taskDialog.CustomButtons.Add(New Nevron.Nov.UI.NTaskDialogCustomButton("Title Only"))
            taskDialog.CustomButtons.Add(New Nevron.Nov.UI.NTaskDialogCustomButton("Title and Description", "This button has a title and a description."))
            taskDialog.CustomButtons.Add(New Nevron.Nov.UI.NTaskDialogCustomButton(Nevron.Nov.UI.NTaskDialogCustomButton.CreateDefaultSymbol(), "Symbol, Title, and Description", "This button has a symbol, a title and a description."))
            taskDialog.CustomButtons.Add(New Nevron.Nov.UI.NTaskDialogCustomButton(NResources.Image__16x16_Mail_png, "Image, Title and Description", "This button has an icon, a title and a description."))

			' Subscribe to the Closed event and open the task dialog
			AddHandler taskDialog.Closed, AddressOf Me.OnTaskDialogClosed
            taskDialog.Open()
        End Sub

        Private Sub OnRadioButtonsButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim taskDialog As Nevron.Nov.UI.NTaskDialog = Nevron.Nov.UI.NTaskDialog.Create(DisplayWindow)
            taskDialog.Title = "Task Dialog"
            taskDialog.Header = New Nevron.Nov.UI.NTaskDialogHeader(Nevron.Nov.UI.ENMessageBoxIcon.Information, "This is a task dialog with radio buttons.")
            taskDialog.RadioButtonGroup = New Nevron.Nov.UI.NTaskDialogRadioButtonGroup("Radio Button 1", "Radio Button 2")
            taskDialog.Buttons = Nevron.Nov.UI.ENTaskDialogButton.OK

			' Subscribe to the Closed event and open the task dialog
			AddHandler taskDialog.Closed, AddressOf Me.OnTaskDialogClosed
            taskDialog.Open()
        End Sub

        Private Sub OnVerificationButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim taskDialog As Nevron.Nov.UI.NTaskDialog = Nevron.Nov.UI.NTaskDialog.Create(DisplayWindow)
            taskDialog.Title = "Task Dialog"
            taskDialog.Header = New Nevron.Nov.UI.NTaskDialogHeader(Nevron.Nov.UI.ENMessageBoxIcon.Information, "This is the header.")
            taskDialog.Content = New Nevron.Nov.UI.NLabel("This is the content.")
            taskDialog.VerificationCheckBox = New Nevron.Nov.UI.NCheckBox("This is the verification check box.")
            taskDialog.Buttons = Nevron.Nov.UI.ENTaskDialogButton.OK Or Nevron.Nov.UI.ENTaskDialogButton.Cancel

			' Subscribe to the Closed event and open the task dialog
			AddHandler taskDialog.Closed, AddressOf Me.OnTaskDialogClosed
            taskDialog.Open()
        End Sub

        Private Sub OnAllFeaturesButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim taskDialog As Nevron.Nov.UI.NTaskDialog = Nevron.Nov.UI.NTaskDialog.Create(DisplayWindow)
            taskDialog.Title = "Task Dialog"

			' Add header and content
			taskDialog.Header = New Nevron.Nov.UI.NTaskDialogHeader(Nevron.Nov.UI.ENMessageBoxIcon.Information, "This is the header.")
            taskDialog.Content = New Nevron.Nov.UI.NLabel("This is the content.")

			' Add some radio buttons and custom buttons
			taskDialog.RadioButtonGroup = New Nevron.Nov.UI.NTaskDialogRadioButtonGroup("Radio Button 1", "Radio Button 2", "Radio Button 3")
            taskDialog.CustomButtons = New Nevron.Nov.UI.NTaskDialogCustomButtonCollection("Custom Button 1", "Custom Button 2", "Custom Button 3")

			' Set the common buttons
			taskDialog.Buttons = Nevron.Nov.UI.ENTaskDialogButton.OK Or Nevron.Nov.UI.ENTaskDialogButton.Cancel

			' Add a verification check box and a footer
			taskDialog.VerificationCheckBox = New Nevron.Nov.UI.NCheckBox("This is the verification check box.")
            taskDialog.Footer = New Nevron.Nov.UI.NLabel("This is the footer.")

			' Subscribe to the Closed event and open the task dialog
			AddHandler taskDialog.Closed, AddressOf Me.OnTaskDialogClosed
            taskDialog.Open()
        End Sub

        Private Sub OnTaskDialogClosed(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim taskDialog As Nevron.Nov.UI.NTaskDialog = CType(arg.TargetNode, Nevron.Nov.UI.NTaskDialog)
            Dim radioButtonIndex As Integer = If(taskDialog.RadioButtonGroup IsNot Nothing, taskDialog.RadioButtonGroup.SelectedIndex, -1)
            Dim verificationChecked As Boolean = If(taskDialog.VerificationCheckBox IsNot Nothing, taskDialog.VerificationCheckBox.Checked, False)
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            sb.AppendLine("Task result: " & taskDialog.TaskResult.ToString())
            sb.AppendLine("Radio button: " & radioButtonIndex.ToString())
            sb.AppendLine("Custom button: " & taskDialog.CustomButtonIndex.ToString())
            sb.AppendLine("Verification: " & verificationChecked.ToString())
            Me.m_EventsLog.LogEvent(sb.ToString())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTaskDialogExample.
		''' </summary>
		Public Shared ReadOnly NTaskDialogExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
