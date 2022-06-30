Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NShadowSplitButtonExample
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
            Nevron.Nov.Examples.UI.NShadowSplitButtonExample.NShadowSplitButtonExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NShadowSplitButtonExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ShadowSplitButton = New Nevron.Nov.UI.NShadowSplitButton()
            Me.m_ShadowSplitButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ShadowSplitButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_ShadowSplitButton.SelectedValueChanged, AddressOf Me.OnShadowSplitButtonSelectedValueChanged
            Return Me.m_ShadowSplitButton
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ShadowSplitButton), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ShadowSplitButton, Nevron.Nov.UI.NShadowSplitButton.EnabledProperty, Nevron.Nov.UI.NShadowSplitButton.HorizontalPlacementProperty, Nevron.Nov.UI.NShadowSplitButton.VerticalPlacementProperty, Nevron.Nov.UI.NShadowSplitButton.DropDownButtonPositionProperty, Nevron.Nov.UI.NShadowSplitButton.HasAutomaticButtonProperty, Nevron.Nov.UI.NShadowSplitButton.HasNoneButtonProperty, Nevron.Nov.UI.NShadowSplitButton.HasMoreOptionsButtonProperty)
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
	This example demonstrates how to create and use shadow split buttons. Split buttons are drop down edits,
	whose item slot is filled with an action button, which generates a <b>Click</b> event on behalf of the
	split button. The shadow split button's drop down content provides the user with a convenient way to quickly
	select a shadow.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnShadowSplitButtonSelectedValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim selectedValue As Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NShadow) = CType(arg.NewValue, Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NShadow))
            Dim str As String

            If selectedValue.Automatic Then
                str = "Automatic"
            ElseIf selectedValue.Value Is Nothing Then
                str = "None"
            Else
                str = selectedValue.Value.ToString()
            End If

            Me.m_EventsLog.LogEvent("Selected shadow: " & str)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog
        Private m_ShadowSplitButton As Nevron.Nov.UI.NShadowSplitButton

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NShadowSplitButtonExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
