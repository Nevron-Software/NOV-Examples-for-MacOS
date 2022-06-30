Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTabExample
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
            Nevron.Nov.Examples.UI.NTabExample.NTabExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTabExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' create a tab
			Me.m_Tab = New Nevron.Nov.UI.NTab()
            Me.m_Tab.TabPages.Add(Me.CreatePage("Page 1", "This is the first tab page."))
            Me.m_Tab.TabPages.Add(Me.CreatePage("Page 2", "This is the second tab page."))
            Me.m_Tab.TabPages.Add(Me.CreatePage("Page 3", "This is the third tab page." & Global.Microsoft.VisualBasic.Constants.vbLf & "It is the largest both horizontally and vertically."))
            Me.m_Tab.TabPages.Add(Me.CreatePage("Page 4", "This is the fourth tab page."))
            Me.m_Tab.TabPages.Add(Me.CreatePage("Page 5", "This is the fifth tab page."))
            Me.m_Tab.SelectedIndex = 0
            AddHandler Me.m_Tab.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnTabSelectedIndexChanged)

			' host it
			Return Me.m_Tab
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Tab), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Tab, Nevron.Nov.UI.NTab.EnabledProperty, Nevron.Nov.UI.NTab.SizeToSelectedPageProperty, Nevron.Nov.UI.NTab.CycleTabPagesProperty, Nevron.Nov.UI.NTab.HeadersPositionProperty, Nevron.Nov.UI.NTab.HeadersModeProperty, Nevron.Nov.UI.NTab.HeadersAlignmentProperty, Nevron.Nov.UI.NTab.HeadersSpacingProperty, Nevron.Nov.UI.NTab.HorizontalPlacementProperty, Nevron.Nov.UI.NTab.VerticalPlacementProperty)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

			' create the events list box
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create tab widgets. The tab is a widget that contains tab pages. Their order in the
	<b>TabPages</b> collection of the tab widget reflects the order they appear in the widget. The <b>SelectedIndex</b>
	property stores the index of the currently selected tab page. The <b>HeadersPosition</b> property determines the
	way the tab page headers are position in respect to the tab widget. The possible values are: <b>Left</b>, <b>Top</b>
	(default), <b>Right</b> and <b>Bottom</b>. You can also specify the spacing between the headers. To do so, use the
	<b>HeadersSpacing</b> property. The <b>HeadersAlignment</b> property determines how the tab page headers are aligned
	on the tab widget side they are placed on. The supported values are <b>Near</b> (default), <b>Center</b> and <b>Far</b>. 
	The <b>CycleTablePages</b> instructs the control to cycle pages when the tab has focus and the user presseses the left 
	or right arrow keys.
</p>
"
        End Function
		#EndRegion

		#Region"Implementation"

		Private Function CreatePage(ByVal name As String, ByVal content As String) As Nevron.Nov.UI.NTabPage
            Dim tabPage As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage(name, content)
            tabPage.Header.Content = Nevron.Nov.UI.NPairBox.Create("Text With Image:", New Nevron.Nov.UI.NImageBox(NResources.Image__16x16_folderDeleted_png))
            Return tabPage
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnHeadersPositionComboSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim combo As Nevron.Nov.UI.NComboBox = CType(args.TargetNode, Nevron.Nov.UI.NComboBox)
            Dim headersPosition As Nevron.Nov.UI.ENTabHeadersPosition = CType(args.NewValue, Nevron.Nov.UI.ENTabHeadersPosition)
            Me.m_Tab.HeadersPosition = headersPosition
        End Sub

        Private Sub OnHeadersSpacingComboSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim combo As Nevron.Nov.UI.NComboBox = CType(args.TargetNode, Nevron.Nov.UI.NComboBox)
            Me.m_Tab.HeadersSpacing = combo.SelectedIndex
        End Sub

        Private Sub OnTabSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Selected Index: " & args.NewValue)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Tab As Nevron.Nov.UI.NTab
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTabExample.
		''' </summary>
		Public Shared ReadOnly NTabExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
