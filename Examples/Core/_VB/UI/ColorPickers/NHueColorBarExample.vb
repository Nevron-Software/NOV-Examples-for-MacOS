Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.Editors
Imports Nevron.Nov.DataStructures

Namespace Nevron.Nov.Examples.UI
    Public Class NHueColorBarExample
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
            Nevron.Nov.Examples.UI.NHueColorBarExample.NHueColorBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NHueColorBarExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_HueColorBar = New Nevron.Nov.UI.NHueColorBar()
            Me.m_HueColorBar.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_HueColorBar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_HueColorBar.SelectedValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnHueColorBarSelectedValueChanged)
            Return Me.m_HueColorBar
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_HueColorBar), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_HueColorBar, Nevron.Nov.UI.NHueColorBar.UpdateWhileDraggingProperty, Nevron.Nov.UI.NHueColorBar.SelectedValueProperty, Nevron.Nov.UI.NHueColorBar.OrientationProperty, Nevron.Nov.UI.NHueColorBar.ValueSelectorExtendPercentProperty)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim i As Integer = 0, editorsCount As Integer = editors.Count

            While i < editorsCount
                stack.Add(editors(i))
                i += 1
            End While

			' Create an events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use a Hue Color Bar. The Hue Color Bar is a color bar that lets the user select
	the hue component of a color. You can control its appearance and behavior using the controls to the right.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnHueColorBarSelectedValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Selected Hue: " & args.NewValue.ToString())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_HueColorBar As Nevron.Nov.UI.NHueColorBar
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NHueColorBarExample.
		''' </summary>
		Public Shared ReadOnly NHueColorBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
