Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NRangeSliderExample
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
            Nevron.Nov.Examples.UI.NRangeSliderExample.NRangeSliderExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NRangeSliderExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_HSlider = New Nevron.Nov.UI.NRangeSlider()
            Me.m_HSlider.BeginValue = 20
            Me.m_HSlider.EndValue = 40
            Me.m_HSlider.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_HSlider.PreferredWidth = 300
            AddHandler Me.m_HSlider.BeginValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSliderValueChanged)
            AddHandler Me.m_HSlider.EndValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSliderValueChanged)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Horizontal", Me.m_HSlider))
            Me.m_VSlider = New Nevron.Nov.UI.NRangeSlider()
            Me.m_VSlider.BeginValue = 20
            Me.m_VSlider.EndValue = 40
            Me.m_VSlider.Orientation = Nevron.Nov.Layout.ENHVOrientation.Vertical
            Me.m_VSlider.PreferredHeight = 300
            Me.m_VSlider.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            AddHandler Me.m_VSlider.BeginValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSliderValueChanged)
            AddHandler Me.m_VSlider.EndValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSliderValueChanged)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Vertical", Me.m_VSlider))
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' Create a tab
			Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            stack.Add(tab)
            Dim properties As Nevron.Nov.Dom.NProperty() = New Nevron.Nov.Dom.NProperty() {Nevron.Nov.UI.NRangeSlider.EnabledProperty, Nevron.Nov.UI.NRangeSlider.BeginValueProperty, Nevron.Nov.UI.NRangeSlider.EndValueProperty, Nevron.Nov.UI.NRangeSlider.LargeChangeProperty, Nevron.Nov.UI.NRangeSlider.SnappingStepProperty, Nevron.Nov.UI.NRangeSlider.MinimumProperty, Nevron.Nov.UI.NRangeSlider.MaximumProperty, Nevron.Nov.UI.NRangeSlider.TicksPlacementProperty, Nevron.Nov.UI.NRangeSlider.TicksIntervalProperty, Nevron.Nov.UI.NRangeSlider.TicksLengthProperty}

			' Create the Horizontal slider properties
			Dim hsbStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_HSlider), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_HSlider, properties)

            For i As Integer = 0 To editors.Count - 1
                hsbStack.Add(editors(i))
            Next

            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Horzontal", hsbStack))

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
	This example demonstrates how to create and use range sliders. The range slider is a widget that
	lets the user select a range defined by a begin and end value in a given range by dragging thumbs.
	You can specify whether the slider is horizontally or vertically oriented through its
	<b>Orientation</b> property. To control the tick visibility and placement use the
	<b>TicksLength</b>, <b>TicksInterval</b> and <b>TicksPlacement</b> properties.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSliderValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim slider As Nevron.Nov.UI.NRangeSlider = CType(args.TargetNode, Nevron.Nov.UI.NRangeSlider)
            Dim text As String = If(slider Is Me.m_HSlider, "Horizontal Range: ", "Vertical Range: ")
            text += slider.BeginValue.ToString("0.###") & " - " & slider.EndValue.ToString("0.###")
            Me.m_EventsLog.LogEvent(text)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_HSlider As Nevron.Nov.UI.NRangeSlider
        Private m_VSlider As Nevron.Nov.UI.NRangeSlider
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRangeSliderExample.
		''' </summary>
		Public Shared ReadOnly NRangeSliderExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
