Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NFillSplitButtonExample
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
            Nevron.Nov.Examples.UI.NFillSplitButtonExample.NFillSplitButtonExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NFillSplitButtonExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_FillSplitButton = New Nevron.Nov.UI.NFillSplitButton()
            Me.m_FillSplitButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_FillSplitButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_FillSplitButton.SelectedValueChanged, AddressOf Me.OnFillSplitButtonSelectedValueChanged
            Return Me.m_FillSplitButton
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_FillSplitButton), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_FillSplitButton, Nevron.Nov.UI.NFillSplitButton.EnabledProperty, Nevron.Nov.UI.NFillSplitButton.HorizontalPlacementProperty, Nevron.Nov.UI.NFillSplitButton.VerticalPlacementProperty, Nevron.Nov.UI.NFillSplitButton.DropDownButtonPositionProperty, Nevron.Nov.UI.NFillSplitButton.HasAutomaticButtonProperty, Nevron.Nov.UI.NFillSplitButton.HasNoneButtonProperty, Nevron.Nov.UI.NFillSplitButton.HasMoreOptionsButtonProperty)
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
	This example demonstrates how to create and use fill split buttons. Split buttons are drop down edits,
	whose item slot is filled with an action button, which generates a <b>Click</b> event on behalf of the
	split button. The fill split button's drop down content provides the user with a convenient way to quickly
	select a fill.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnFillSplitButtonSelectedValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim selectedValue As Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NFill) = CType(arg.NewValue, Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NFill))
            Dim str As String

            If selectedValue.Automatic Then
                str = "Automatic"
            ElseIf selectedValue.Value Is Nothing Then
                str = "None"
            Else
                str = selectedValue.Value.ToString()
            End If

            Me.m_EventsLog.LogEvent("Selected fill: " & str)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog
        Private m_FillSplitButton As Nevron.Nov.UI.NFillSplitButton

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NFillSplitButtonExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
