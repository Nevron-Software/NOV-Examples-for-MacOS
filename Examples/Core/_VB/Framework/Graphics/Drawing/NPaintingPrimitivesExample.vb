Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.Editors
Imports Nevron.Nov.DataStructures

Namespace Nevron.Nov.Examples.Framework
    Public Class NPaintingPrimitivesExample
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
            Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.NPaintingPrimitivesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim names As String() = New String() {"Line", "Polyline", "Rectangle", "Ellipse", "Triangle", "Quad", "Polygon", "Graphics Path"}
            Dim delegates As Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate() = New Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate() {New Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate(AddressOf Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintLine), New Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate(AddressOf Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPolyline), New Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate(AddressOf Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintRectangle), New Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate(AddressOf Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintEllipse), New Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate(AddressOf Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintTriangle), New Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate(AddressOf Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintQuadrangle), New Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate(AddressOf Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPolygon), New Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate(AddressOf Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPath)}
            Dim count As Integer = delegates.Length

			' Create a table panel to hold the canvases and the labels
			Me.m_Table = New Nevron.Nov.UI.NTableFlowPanel()
            Me.m_Table.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_Table.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_Table.Padding = New Nevron.Nov.Graphics.NMargins(30)
            Me.m_Table.HorizontalSpacing = 30
            Me.m_Table.VerticalSpacing = 30
            Me.m_Table.MaxOrdinal = 4

            For i As Integer = 0 To count - 1
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Me.m_Table.Add(stack)
                stack.VerticalSpacing = 5

				' Create a canvas to draw in
				Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
                canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.DefaultCanvasWidth, Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.DefaultCanvasHeight)
                canvas.Tag = delegates(i)
                AddHandler canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
                canvas.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)
                stack.Add(canvas)

				' Create a label for the geometry primitive's name
				Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(names(i))
                label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                stack.Add(label)
            Next

			' The table must be scrollable
			Dim scroll As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
            scroll.Content = Me.m_Table
            Return scroll
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
			' Fill
			Me.m_FillSplitButton = New Nevron.Nov.UI.NFillSplitButton()
            AddHandler Me.m_FillSplitButton.SelectedValueChanged, AddressOf Me.OnFillSplitButtonSelectedValueChanged

			' Stroke color
			Me.m_StrokeColorBox = New Nevron.Nov.UI.NColorBox()
            Me.m_StrokeColorBox.SelectedColor = Me.m_Stroke.Color
            AddHandler Me.m_StrokeColorBox.SelectedColorChanged, AddressOf Me.OnStrokeColorBoxSelectedColorChanged

			' Stroke width
			Me.m_StrokeWidthCombo = New Nevron.Nov.UI.NComboBox()

            For i As Integer = 0 To 6 - 1
                Me.m_StrokeWidthCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem(i.ToString()))
            Next

            Me.m_StrokeWidthCombo.SelectedIndex = 1
            AddHandler Me.m_StrokeWidthCombo.SelectedIndexChanged, AddressOf Me.OnStrokeWidthComboSelectedIndexChanged

			' Canvas width editor
			Me.m_CanvasWidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasWidthUpDown.Minimum = 100
            Me.m_CanvasWidthUpDown.Maximum = 350
            Me.m_CanvasWidthUpDown.Value = Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.DefaultCanvasWidth
            Me.m_CanvasWidthUpDown.[Step] = 1
            Me.m_CanvasWidthUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasWidthUpDown.ValueChanged, AddressOf Me.OnNumericUpDownValueChanged

			' Canvas height editor
			Me.m_CanvasHeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasHeightUpDown.Minimum = 100
            Me.m_CanvasHeightUpDown.Maximum = 350
            Me.m_CanvasHeightUpDown.Value = Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.DefaultCanvasHeight
            Me.m_CanvasHeightUpDown.[Step] = 1
            Me.m_CanvasHeightUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasHeightUpDown.ValueChanged, AddressOf Me.OnNumericUpDownValueChanged
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Fill:", Me.m_FillSplitButton))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Stroke Color:", Me.m_StrokeColorBox))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Stroke Width:", Me.m_StrokeWidthCombo))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Width:", Me.m_CanvasWidthUpDown))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Height:", Me.m_CanvasHeightUpDown))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the primitive painting capabilities of the NOV graphics.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return
            Dim paintDelegate As Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate = TryCast(canvas.Tag, Nevron.Nov.Examples.Framework.NPaintingPrimitivesExample.PaintPrimitiveDelegate)
            If paintDelegate Is Nothing Then Throw New System.Exception("The canvas has no assigned paint delegate.")
            args.PaintVisitor.ClearStyles()
            args.PaintVisitor.SetStroke(Me.m_Stroke)
            args.PaintVisitor.SetFill(Me.m_Fill)
            paintDelegate(args.PaintVisitor, canvas.Width, canvas.Height)
        End Sub

        Private Sub OnNumericUpDownValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_Table Is Nothing Then Return
            Dim width As Double = Me.m_CanvasWidthUpDown.Value
            Dim height As Double = Me.m_CanvasHeightUpDown.Value

			' Resize the canvases
			Dim iterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Dom.NNode) = Me.m_Table.GetSubtreeIterator(Nevron.Nov.Dom.ENTreeTraversalOrder.DepthFirstPreOrder, New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NCanvas.NCanvasSchema))

            While iterator.MoveNext()
                Dim canvas As Nevron.Nov.UI.NCanvas = CType(iterator.Current, Nevron.Nov.UI.NCanvas)
                CType(canvas.ParentNode, Nevron.Nov.UI.NWidget).PreferredWidth = width
                canvas.PreferredHeight = height
            End While
        End Sub

        Private Sub OnFillSplitButtonSelectedValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim selectedFill As Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NFill) = CType(arg.NewValue, Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NFill))
            Me.m_Fill = If(selectedFill.Automatic, Nothing, selectedFill.Value)
            Me.InvalidateCanvases()
        End Sub

        Private Sub OnStrokeColorBoxSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Stroke.Color = Me.m_StrokeColorBox.SelectedColor
            Me.InvalidateCanvases()
        End Sub

        Private Sub OnStrokeWidthComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Stroke.Width = Me.m_StrokeWidthCombo.SelectedIndex
            Me.InvalidateCanvases()
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub InvalidateCanvases()
            If Me.m_Table Is Nothing Then Return
            Dim iterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Dom.NNode) = Me.m_Table.GetSubtreeIterator(Nevron.Nov.Dom.ENTreeTraversalOrder.DepthFirstPreOrder, New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NCanvas.NCanvasSchema))

            While iterator.MoveNext()
                Dim canvas As Nevron.Nov.UI.NCanvas = CType(iterator.Current, Nevron.Nov.UI.NCanvas)
                canvas.InvalidateDisplay()
            End While
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Table As Nevron.Nov.UI.NTableFlowPanel
        Private m_CanvasWidthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_CanvasHeightUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_FillSplitButton As Nevron.Nov.UI.NFillSplitButton
        Private m_StrokeColorBox As Nevron.Nov.UI.NColorBox
        Private m_StrokeWidthCombo As Nevron.Nov.UI.NComboBox
        Private m_Stroke As Nevron.Nov.Graphics.NStroke = New Nevron.Nov.Graphics.NStroke()
        Private m_Fill As Nevron.Nov.Graphics.NFill

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NPaintingPrimitivesExample.
		''' </summary>
		Public Shared ReadOnly NPaintingPrimitivesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Sub PaintLine(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            paintVisitor.PaintLine(0.2 * w, 0.1 * h, 0.9 * w, 0.8 * h)
            paintVisitor.PaintLine(0.1 * w, 0.4 * h, 0.8 * w, 0.9 * h)
        End Sub

        Private Shared Sub PaintPolyline(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            Dim polyline As Nevron.Nov.Graphics.NPolyline = New Nevron.Nov.Graphics.NPolyline(5)
            polyline.Add(0.1 * w, 0.1 * h)
            polyline.Add(0.4 * w, 0.2 * h)
            polyline.Add(0.2 * w, 0.5 * h)
            polyline.Add(0.3 * w, 0.8 * h)
            polyline.Add(0.9 * w, 0.9 * h)
            paintVisitor.PaintPolyline(polyline)
        End Sub

        Private Shared Sub PaintRectangle(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            paintVisitor.PaintRectangle(0.05 * w, 0.25 * h, 0.9 * w, 0.5 * h)
        End Sub

        Private Shared Sub PaintEllipse(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            paintVisitor.PaintEllipse(0.05 * w, 0.25 * h, 0.9 * w, 0.5 * h)
        End Sub

        Private Shared Sub PaintTriangle(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            Dim p1 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.5 * w, 0.1 * h)
            Dim p2 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.9 * w, 0.9 * h)
            Dim p3 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.1 * w, 0.8 * h)
            paintVisitor.PaintTriangle(p1, p2, p3)
        End Sub

        Private Shared Sub PaintQuadrangle(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            Dim p1 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.2 * w, 0.1 * h)
            Dim p2 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.6 * w, 0.1 * h)
            Dim p3 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.9 * w, 0.9 * h)
            Dim p4 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.1 * w, 0.6 * h)
            paintVisitor.PaintQuadrangle(p1, p2, p3, p4)
        End Sub

        Private Shared Sub PaintPolygon(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            Dim polygon As Nevron.Nov.Graphics.NPolygon = New Nevron.Nov.Graphics.NPolygon(6)
            polygon.Add(0.3 * w, 0.1 * h)
            polygon.Add(0.7 * w, 0.1 * h)
            polygon.Add(0.5 * w, 0.4 * h)
            polygon.Add(0.9 * w, 0.9 * h)
            polygon.Add(0.2 * w, 0.8 * h)
            polygon.Add(0.1 * w, 0.4 * h)
            paintVisitor.PaintPolygon(polygon, Nevron.Nov.Graphics.ENFillRule.EvenOdd)
        End Sub

        Private Shared Sub PaintPath(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.StartFigure(0.1 * w, 0.1 * h)
            path.LineTo(0.7 * w, 0.2 * h)
            path.CubicBezierTo(0.9 * w, 0.9 * h, 1 * w, 0.4 * h, 0.5 * w, 0.7 * h)
            path.LineTo(0.2 * w, 0.8 * h)
            path.CubicBezierTo(0.1 * w, 0.1 * h, 0.3 * w, 0.7 * h, 0.4 * w, 0.6 * h)
            path.CloseFigure()
            paintVisitor.PaintPath(path)
        End Sub

		#EndRegion

		#Region"Constants"

		Private Const DefaultCanvasWidth As Integer = 220
        Private Const DefaultCanvasHeight As Integer = 220

		#EndRegion

		#Region"Nested Types"

		Friend Delegate Sub PaintPrimitiveDelegate(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)

		#EndRegion
	End Class
End Namespace
