Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NMultiSplitterExample
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
            Nevron.Nov.Examples.UI.NMultiSplitterExample.NMultiSplitterExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NMultiSplitterExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a multi splitter
			Me.m_MultiSplitter = New Nevron.Nov.UI.NMultiSplitter()
            Dim pane1 As Nevron.Nov.UI.NSplitterPane = New Nevron.Nov.UI.NSplitterPane()
            pane1.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            pane1.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGreen)
            pane1.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            pane1.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            pane1.Content = New Nevron.Nov.UI.NLabel("Pane 1")
            Me.m_MultiSplitter.Widgets.Add(pane1)
            Dim thumb1 As Nevron.Nov.UI.NSplitterThumb = New Nevron.Nov.UI.NSplitterThumb()
            thumb1.CollapseMode = Nevron.Nov.UI.ENSplitterCollapseMode.BothPanes
            Me.m_MultiSplitter.Widgets.Add(thumb1)
            Dim pane2 As Nevron.Nov.UI.NSplitterPane = New Nevron.Nov.UI.NSplitterPane()
            pane2.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            pane2.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)
            pane2.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            pane2.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            pane2.Content = New Nevron.Nov.UI.NLabel("Pane 2")
            Me.m_MultiSplitter.Widgets.Add(pane2)
            Dim thumb2 As Nevron.Nov.UI.NSplitterThumb = New Nevron.Nov.UI.NSplitterThumb()
            thumb2.CollapseMode = Nevron.Nov.UI.ENSplitterCollapseMode.BothPanes
            Me.m_MultiSplitter.Widgets.Add(thumb2)
            Dim pane3 As Nevron.Nov.UI.NSplitterPane = New Nevron.Nov.UI.NSplitterPane()
            pane3.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            pane3.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightYellow)
            pane3.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            pane3.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            pane3.Content = New Nevron.Nov.UI.NLabel("Pane 3")
            Me.m_MultiSplitter.Widgets.Add(pane3)
            Return Me.m_MultiSplitter
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Create multi splitter property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_MultiSplitter), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_MultiSplitter, Nevron.Nov.UI.NSplitterBase.OrientationProperty, Nevron.Nov.UI.NSplitterBase.ResizeWhileDraggingProperty, Nevron.Nov.UI.NSplitterBase.ResizeStepProperty)
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                propertyStack.Add(editors(i))
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Splitter Properties", propertyStack))

			' Create splitter thumb property editors
			Dim thumbs As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.UI.NSplitterThumb) = Me.m_MultiSplitter.Widgets.GetChildren(Of Nevron.Nov.UI.NSplitterThumb)()

            For i As Integer = 0 To thumbs.Count - 1
                editors = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((thumbs(CInt((i)))), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(thumbs(i), Nevron.Nov.UI.NSplitterThumb.SplitModeProperty, Nevron.Nov.UI.NSplitterThumb.SplitOffsetProperty, Nevron.Nov.UI.NSplitterThumb.SplitFactorProperty, Nevron.Nov.UI.NSplitterThumb.CollapseModeProperty)
                propertyStack = New Nevron.Nov.UI.NStackPanel()

                For j As Integer = 0 To editors.Count - 1
                    propertyStack.Add(editors(j))
                Next

                stack.Add(New Nevron.Nov.UI.NGroupBox("Splitter Thumb " & (i + 1).ToString() & " Properties", propertyStack))
            Next

            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and configure a multi splitter. The multi splitter is a widget that
	splits its content area into multiple resizable panes, which can be interactively resized with help of thumbs.
	Using the <b>Orientation</b> property you can specify whether the splitter is horizontal or vertical.
	To control how the splitter splits its content area you can use the <b>SplitMode</b> property.
</p>
"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_MultiSplitter As Nevron.Nov.UI.NMultiSplitter

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NMultiSplitterExample.
		''' </summary>
		Public Shared ReadOnly NMultiSplitterExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
