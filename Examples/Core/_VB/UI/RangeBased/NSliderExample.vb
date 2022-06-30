Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NSliderExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NSliderExample.NSliderExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NSliderExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim gbh As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Horizontal")
            stack.Add(gbh)
            Me.m_HSlider = New Nevron.Nov.UI.NSlider()
            gbh.Content = Me.m_HSlider
            Me.m_HSlider.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_HSlider.PreferredWidth = 300
            AddHandler Me.m_HSlider.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSliderValueChanged)
            Dim gbv As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Vertical")
            stack.Add(gbv)
            Me.m_VSlider = New Nevron.Nov.UI.NSlider()
            gbv.Content = Me.m_VSlider
            Me.m_VSlider.Orientation = Nevron.Nov.Layout.ENHVOrientation.Vertical
            Me.m_VSlider.PreferredHeight = 300
            Me.m_VSlider.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            AddHandler Me.m_VSlider.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSliderValueChanged)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' Create a tab
			Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            stack.Add(tab)
            Dim properties As Nevron.Nov.Dom.NProperty() = New Nevron.Nov.Dom.NProperty() {Nevron.Nov.UI.NSlider.EnabledProperty, Nevron.Nov.UI.NSlider.ValueProperty, Nevron.Nov.UI.NSlider.LargeChangeProperty, Nevron.Nov.UI.NSlider.SnappingStepProperty, Nevron.Nov.UI.NSlider.MinimumProperty, Nevron.Nov.UI.NSlider.MaximumProperty, Nevron.Nov.UI.NSlider.TicksPlacementProperty, Nevron.Nov.UI.NSlider.TicksIntervalProperty, Nevron.Nov.UI.NSlider.TicksLengthProperty}

			' Create the Horizontal slider properties
			Dim hsbStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_HSlider), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_HSlider, properties)

            For i As Integer = 0 To editors.Count - 1
                hsbStack.Add(editors(i))
            Next

            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Horizontal", hsbStack))

			' Create the Vertical slider properties
			Dim vsbStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            editors = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_VSlider), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_VSlider, properties)

            For i As Integer = 0 To editors.Count - 1
                vsbStack.Add(editors(i))
            Next

            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Vertical", vsbStack))

			' Add events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use sliders. The slider is a widget that
	lets the user select a distinct value in a given range by dragging a thumb. You can
	specify whether the slider is horizontally or vertically oriented through its
	<b>Orientation</b> property. To control the tick visibility and placement use the
	<b>TicksLength</b>, <b>TicksInterval</b> and <b>TicksPlacement</b> properties.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSliderValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim text As String = If(args.TargetNode Is Me.m_HSlider, "Horizontal: ", "Vertical: ")
            text += args.NewValue.ToString()
            Me.m_EventsLog.LogEvent(text)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_HSlider As Nevron.Nov.UI.NSlider
        Private m_VSlider As Nevron.Nov.UI.NSlider
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSliderExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
