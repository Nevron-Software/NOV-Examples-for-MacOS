Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NRangeScrollBarsExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NRangeScrollBarsExample.NRangeScrollBarsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NRangeScrollBarsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a stack for the scroll bars
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
			
			' Create the HScrollBar
			Me.m_HScrollBar = New Nevron.Nov.UI.NHRangeScrollBar()
            Me.m_HScrollBar.BeginValue = 20
            Me.m_HScrollBar.EndValue = 40
            Me.m_HScrollBar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_HScrollBar.BeginValueChanged, AddressOf Me.OnScrollBarValueChanged
            AddHandler Me.m_HScrollBar.EndValueChanged, AddressOf Me.OnScrollBarValueChanged
            stack.Add(New Nevron.Nov.UI.NGroupBox("Horizontal", Me.m_HScrollBar))

			' Create the VScrollBar
			Me.m_VScrollBar = New Nevron.Nov.UI.NVRangeScrollBar()
            Me.m_VScrollBar.BeginValue = 20
            Me.m_VScrollBar.EndValue = 40
            Me.m_VScrollBar.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            AddHandler Me.m_VScrollBar.BeginValueChanged, AddressOf Me.OnScrollBarValueChanged
            AddHandler Me.m_VScrollBar.EndValueChanged, AddressOf Me.OnScrollBarValueChanged
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

			' Create the Horizontal scrollbar properties
			Dim hsbStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_HScrollBar), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_HScrollBar, Nevron.Nov.UI.NHRangeScrollBar.EnabledProperty, Nevron.Nov.UI.NHRangeScrollBar.BeginValueProperty, Nevron.Nov.UI.NHRangeScrollBar.EndValueProperty, Nevron.Nov.UI.NHRangeScrollBar.SmallChangeProperty, Nevron.Nov.UI.NHRangeScrollBar.SnappingStepProperty, Nevron.Nov.UI.NHRangeScrollBar.MinimumProperty, Nevron.Nov.UI.NHRangeScrollBar.MaximumProperty)

            For i As Integer = 0 To editors.Count - 1
                hsbStack.Add(editors(i))
            Next

            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Horizontal", hsbStack))

			' Create the Vertical scrollbar properties
			Dim vsbStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            editors = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_VScrollBar), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_VScrollBar, Nevron.Nov.UI.NVRangeScrollBar.EnabledProperty, Nevron.Nov.UI.NVRangeScrollBar.BeginValueProperty, Nevron.Nov.UI.NVRangeScrollBar.EndValueProperty, Nevron.Nov.UI.NVRangeScrollBar.SmallChangeProperty, Nevron.Nov.UI.NVRangeScrollBar.SnappingStepProperty, Nevron.Nov.UI.NVRangeScrollBar.MinimumProperty, Nevron.Nov.UI.NVRangeScrollBar.MaximumProperty)

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
	This example demonstrates how to create and use range scroll bars. Range scroll bars are range based widgets, which
	are used for selecting a range defined by a begin and an end value. They can be horizontal and vertical and expose
	a set of properties, which you can use to control their appearance and behavior.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnScrollBarValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim text As String

            If args.TargetNode Is Me.m_HScrollBar Then
                text = "Horizontal Range: " & Me.m_HScrollBar.BeginValue.ToString("0.###") & " - " & Me.m_HScrollBar.EndValue.ToString("0.###")
            Else
                text = "Vertical Range: " & Me.m_VScrollBar.BeginValue.ToString("0.###") & " - " & Me.m_VScrollBar.EndValue.ToString("0.###")
            End If

            Me.m_EventsLog.LogEvent(text)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog
        Private m_HScrollBar As Nevron.Nov.UI.NHRangeScrollBar
        Private m_VScrollBar As Nevron.Nov.UI.NVRangeScrollBar

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRangeScrollBarsExample.
		''' </summary>
		Public Shared ReadOnly NRangeScrollBarsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
