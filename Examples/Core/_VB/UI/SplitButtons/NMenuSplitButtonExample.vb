Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NMenuSplitButtonExample
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
            Nevron.Nov.Examples.UI.NMenuSplitButtonExample.NMenuSplitButtonExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NMenuSplitButtonExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create an array of images to use for the headers of the menu items
			Dim BorderSideImages As Nevron.Nov.Graphics.NImage() = New Nevron.Nov.Graphics.NImage() {Nevron.Nov.Text.NResources.Image_TableBorders_AllBorders_png, Nevron.Nov.Text.NResources.Image_TableBorders_NoBorder_png, Nevron.Nov.Text.NResources.Image_TableBorders_OutsideBorders_png, Nevron.Nov.Text.NResources.Image_TableBorders_InsideBorders_png, Nevron.Nov.Text.NResources.Image_TableBorders_TopBorder_png, Nevron.Nov.Text.NResources.Image_TableBorders_BottomBorder_png, Nevron.Nov.Text.NResources.Image_TableBorders_LeftBorder_png, Nevron.Nov.Text.NResources.Image_TableBorders_RightBorder_png, Nevron.Nov.Text.NResources.Image_TableBorders_InsideHorizontalBorder_png, Nevron.Nov.Text.NResources.Image_TableBorders_InsideVerticalBorder_png}

			' Create a menu split button
			Me.m_MenuSplitButton = New Nevron.Nov.UI.NMenuSplitButton()
            Me.m_MenuSplitButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_MenuSplitButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top

			' Fill the menu split button drop down menu from an enum and with
			' the images created above for headers
			Me.m_MenuSplitButton.FillFromEnum(Of Nevron.Nov.Text.ENTableBorders, Nevron.Nov.Graphics.NImage)(BorderSideImages)

			' Subscribe to the SelectedIndexChanged and the Click events
			AddHandler Me.m_MenuSplitButton.SelectedIndexChanged, AddressOf Me.OnMenuSplitButtonSelectedIndexChanged
            AddHandler Me.m_MenuSplitButton.Click, AddressOf Me.OnMenuSplitButtonClick
            Return Me.m_MenuSplitButton
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_MenuSplitButton), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_MenuSplitButton, Nevron.Nov.UI.NMenuSplitButton.EnabledProperty, Nevron.Nov.UI.NMenuSplitButton.HorizontalPlacementProperty, Nevron.Nov.UI.NMenuSplitButton.VerticalPlacementProperty, Nevron.Nov.UI.NMenuSplitButton.DropDownButtonPositionProperty, Nevron.Nov.UI.NMenuSplitButton.SelectedIndexProperty)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use menu split buttons. Split buttons are drop down edits,
	whose item slot is filled with an action button, which generates a <b>Click</b> event on behalf of the
	split button. The menu split button's drop down content is a menu, which allows the user to quickly
	select an option. This option is then assigned to the action button of the split button and can be
	activated by clicking the action button.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnMenuSplitButtonSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
			' Get the selected index
			Dim selectedIndex As Integer = CInt(arg.NewValue)

            If selectedIndex = -1 Then
                Me.m_EventsLog.LogEvent("No item selected")
                Return
            End If

			' Obtain and show the selected enum value
			Dim splitButton As Nevron.Nov.UI.NMenuSplitButton = CType(arg.CurrentTargetNode, Nevron.Nov.UI.NMenuSplitButton)
            Dim selectedSide As Nevron.Nov.Text.ENTableBorders = CType(splitButton.SelectedValue, Nevron.Nov.Text.ENTableBorders)
            Me.m_EventsLog.LogEvent("Selected Index: " & selectedIndex.ToString() & " (" & selectedSide.ToString() & ")")
        End Sub

        Private Sub OnMenuSplitButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' Get the selected index
			Dim splitButton As Nevron.Nov.UI.NMenuSplitButton = CType(arg.CurrentTargetNode, Nevron.Nov.UI.NMenuSplitButton)
            Dim selectedIndex As Integer = splitButton.SelectedIndex

            If selectedIndex = -1 Then
                Me.m_EventsLog.LogEvent("No item selected")
                Return
            End If

			' Obtain and show the selected enum value
			Dim selectedSide As Nevron.Nov.Text.ENTableBorders = CType(splitButton.SelectedValue, Nevron.Nov.Text.ENTableBorders)
            Me.m_EventsLog.LogEvent("Action button clicked, index: " & selectedIndex.ToString() & " (" & selectedSide.ToString() & ")")
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog
        Private m_MenuSplitButton As Nevron.Nov.UI.NMenuSplitButton

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NMenuSplitButtonExample.
		''' </summary>
		Public Shared ReadOnly NMenuSplitButtonExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
