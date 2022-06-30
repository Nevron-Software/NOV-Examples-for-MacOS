Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NBorderSplitButtonExample
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
            Nevron.Nov.Examples.UI.NBorderSplitButtonExample.NBorderSplitButtonExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NBorderSplitButtonExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_BorderSplitButton = New Nevron.Nov.UI.NBorderSplitButton()
            Me.m_BorderSplitButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_BorderSplitButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_BorderSplitButton.SelectedValueChanged, AddressOf Me.OnBorderSplitButtonSelectedValueChanged
            Return Me.m_BorderSplitButton
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_BorderSplitButton), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_BorderSplitButton, Nevron.Nov.UI.NBorderSplitButton.EnabledProperty, Nevron.Nov.UI.NBorderSplitButton.HorizontalPlacementProperty, Nevron.Nov.UI.NBorderSplitButton.VerticalPlacementProperty, Nevron.Nov.UI.NBorderSplitButton.DropDownButtonPositionProperty, Nevron.Nov.UI.NBorderSplitButton.HasAutomaticButtonProperty, Nevron.Nov.UI.NBorderSplitButton.HasNoneButtonProperty, Nevron.Nov.UI.NBorderSplitButton.HasMoreOptionsButtonProperty)
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
	This example demonstrates how to create and use border split buttons. Split buttons are drop down edits,
	whose item slot is filled with an action button, which generates a <b>Click</b> event on behalf of the
	split button. The border split button's drop down content provides the user with a convenient way to quickly
	select a border and its thickness.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnBorderSplitButtonSelectedValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim selectedValue As Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.UI.NBorder) = CType(arg.NewValue, Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.UI.NBorder))
            Dim str As String

            If selectedValue.Automatic Then
                str = "Automatic"
            ElseIf selectedValue.Value Is Nothing Then
                str = "None"
            Else
                str = selectedValue.Value.ToString()
            End If

            Me.m_EventsLog.LogEvent("Selected border: " & str)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog
        Private m_BorderSplitButton As Nevron.Nov.UI.NBorderSplitButton

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NBorderSplitButtonExample.
		''' </summary>
		Public Shared ReadOnly NBorderSplitButtonExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
