Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NStrokeSplitButtonExample
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
            Nevron.Nov.Examples.UI.NStrokeSplitButtonExample.NStrokeSplitButtonExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NStrokeSplitButtonExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_StrokeSplitButton = New Nevron.Nov.UI.NStrokeSplitButton()
            Me.m_StrokeSplitButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_StrokeSplitButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_StrokeSplitButton.SelectedValueChanged, AddressOf Me.OnStrokeSplitButtonSelectedValueChanged
            Return Me.m_StrokeSplitButton
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_StrokeSplitButton), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_StrokeSplitButton, Nevron.Nov.UI.NStrokeSplitButton.EnabledProperty, Nevron.Nov.UI.NStrokeSplitButton.HorizontalPlacementProperty, Nevron.Nov.UI.NStrokeSplitButton.VerticalPlacementProperty, Nevron.Nov.UI.NStrokeSplitButton.DropDownButtonPositionProperty, Nevron.Nov.UI.NStrokeSplitButton.HasAutomaticButtonProperty, Nevron.Nov.UI.NStrokeSplitButton.HasNoneButtonProperty, Nevron.Nov.UI.NStrokeSplitButton.HasMoreOptionsButtonProperty)
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
	This example demonstrates how to create and use stroke split buttons. Split buttons are drop down edits,
	whose item slot is filled with an action button, which generates a <b>Click</b> event on behalf of the
	split button. The stroke split button drop down content provides the user with a convenient way to quickly
	select a stroke.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnStrokeSplitButtonSelectedValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim selectedValue As Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NStroke) = CType(arg.NewValue, Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NStroke))
            Dim str As String

            If selectedValue.Automatic Then
                str = "Automatic"
            ElseIf selectedValue.Value Is Nothing Then
                str = "None"
            Else
                str = selectedValue.Value.ToString()
            End If

            Me.m_EventsLog.LogEvent("Selected stroke: " & str)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog
        Private m_StrokeSplitButton As Nevron.Nov.UI.NStrokeSplitButton

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStrokeSplitButtonExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
