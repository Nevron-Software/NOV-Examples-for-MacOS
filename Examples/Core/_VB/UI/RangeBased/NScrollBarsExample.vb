Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NScrollBarsExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NScrollBarsExample.NScrollBarsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NScrollBarsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' create the root tab
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
			
			' create the HScrollBar
			Me.m_HScrollBar = New Nevron.Nov.UI.NHScrollBar()
            Me.m_HScrollBar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_HScrollBar.StartScrolling, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnScrollBarStartScrolling)
            AddHandler Me.m_HScrollBar.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnScrollBarValueChanged)
            AddHandler Me.m_HScrollBar.EndScrolling, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnScrollBarEndScrolling)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Horizontal", Me.m_HScrollBar))

			' create the VScrollBar
			Me.m_VScrollBar = New Nevron.Nov.UI.NVScrollBar()
            Me.m_VScrollBar.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            AddHandler Me.m_VScrollBar.StartScrolling, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnScrollBarStartScrolling)
            AddHandler Me.m_VScrollBar.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnScrollBarValueChanged)
            AddHandler Me.m_VScrollBar.EndScrolling, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnScrollBarEndScrolling)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Vertical", Me.m_VScrollBar))
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' Create a tab
			Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            stack.Add(tab)
            Dim properties As Nevron.Nov.Dom.NProperty() = New Nevron.Nov.Dom.NProperty() {Nevron.Nov.UI.NScrollBar.EnabledProperty, Nevron.Nov.UI.NScrollBar.ValueProperty, Nevron.Nov.UI.NScrollBar.SmallChangeProperty, Nevron.Nov.UI.NScrollBar.LargeChangeProperty, Nevron.Nov.UI.NScrollBar.SnappingStepProperty, Nevron.Nov.UI.NScrollBar.MinimumProperty, Nevron.Nov.UI.NScrollBar.MaximumProperty}

			' Create the Horizontal scrollbar properties
			Dim hsbStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_HScrollBar), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_HScrollBar, properties)

            For i As Integer = 0 To editors.Count - 1
                hsbStack.Add(editors(i))
            Next

            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Horizontal", hsbStack))

			' Create the Vertical scrollbar properties
			Dim vsbStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            editors = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_VScrollBar), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_VScrollBar, properties)

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
	This example demonstrates how to create and use scrollbars. Scrollbars are range based widgets, which come in handy
	when you need to display large content in a limited area on screen. The scrollbars let the user change the currently
	visible part of this large content by dragging a thumb or clicking on an arrow button. The scrollbars can be horizontal
	and vertical and expose a set of properties, which you can use to control their appearance and behavior.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnScrollBarStartScrolling(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Me.m_EventsLog.LogEvent("Start Scrolling")
        End Sub

        Private Sub OnScrollBarEndScrolling(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Me.m_EventsLog.LogEvent("End Scrolling")
        End Sub

        Private Sub OnScrollBarValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Value: " & args.NewValue.ToString())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog
        Private m_HScrollBar As Nevron.Nov.UI.NHScrollBar
        Private m_VScrollBar As Nevron.Nov.UI.NVScrollBar

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NScrollBarsExample.
		''' </summary>
		Public Shared ReadOnly NScrollBarsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
