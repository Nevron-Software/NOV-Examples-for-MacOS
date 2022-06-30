Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Editors

Namespace Nevron.Nov.Examples.UI
    Public Class NRadioButtonExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NRadioButtonExample.NRadioButtonExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NRadioButtonExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()

			' create a tab page with vertically arranged radio buttons
			Dim verticalTabPage As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Vertical Radio Group")
            tab.TabPages.Add(verticalTabPage)
            Dim verticalRadioGroup As Nevron.Nov.UI.NRadioButtonGroup = New Nevron.Nov.UI.NRadioButtonGroup()
            verticalRadioGroup.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            verticalRadioGroup.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            verticalTabPage.Content = verticalRadioGroup
            Dim verticalStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            verticalRadioGroup.Content = verticalStack

            For i As Integer = 0 To 5 - 1
                Dim radioButton As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton("Item " & i.ToString())
                verticalStack.Add(radioButton)
            Next

            Dim disabledRadioButton1 As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton("Disabled")
            disabledRadioButton1.Enabled = False
            verticalStack.Add(disabledRadioButton1)
            AddHandler verticalRadioGroup.SelectedIndexChanged, AddressOf Me.OnVerticalRadioGroupSelectedIndexChanged

			' create a tab page with horizontally arranged radio buttons
			Dim horizontalTabPage As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Horizontal Radio Group")
            tab.TabPages.Add(horizontalTabPage)
            Dim horizontalRadioGroup As Nevron.Nov.UI.NRadioButtonGroup = New Nevron.Nov.UI.NRadioButtonGroup()
            horizontalRadioGroup.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            horizontalRadioGroup.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            horizontalTabPage.Content = horizontalRadioGroup
            Dim horizontalStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            horizontalStack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            horizontalRadioGroup.Content = horizontalStack

            For i As Integer = 0 To 5 - 1
                Dim radioButton As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton("Item " & i.ToString())
                horizontalStack.Add(radioButton)
            Next

            Dim disabledRadioButton2 As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton("Disabled")
            disabledRadioButton2.Enabled = False
            horizontalStack.Add(disabledRadioButton2)
            AddHandler horizontalRadioGroup.SelectedIndexChanged, AddressOf Me.OnHorizontalRadioGroupSelectedIndexChanged
            Return tab
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' create the events list box
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use radio buttons and radio button groups. 
</p>
<p>
    A radio button (NRadioButton) is a kind of a toggle button, which is intended to be placed inside the sub-hierarchy of a radio button group (NRadioButtonGroup).
    When there are multiple radio buttons inside the radio button group sub-hierarchy, only one can be checked at a time. 
    Checking a different radio button will automatically uncheck the previously checked one.
</p>
<p>
    The radio button group (NRadioButtonGroup) is a content container, thus it is up to the developer to choose the style in which the radio buttons are arranged.
    In this example we have created two radio groups, each of which holding a stack panel (horizontal or vertical) that contain radio buttons. 
    It is however possible to arrange the radio buttons in any way that you see fit.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnHorizontalRadioGroupSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Horizontal Radio Selected: " & CInt(args.NewValue))
        End Sub

        Private Sub OnVerticalRadioGroupSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Vertical Radio Selected: " & CInt(args.NewValue))
        End Sub
		
		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NRadioButtonExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
