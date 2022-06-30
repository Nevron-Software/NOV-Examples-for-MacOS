Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NPaintingGraphicsPathsExample
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
            Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.NPaintingGraphicsPathsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a table panel to hold the canvases and the labels
			Me.m_Table = New Nevron.Nov.UI.NTableFlowPanel()
            Me.m_Table.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_Table.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_Table.Padding = New Nevron.Nov.Graphics.NMargins(30)
            Me.m_Table.HorizontalSpacing = 30
            Me.m_Table.VerticalSpacing = 30
            Me.m_Table.MaxOrdinal = 4
            Dim names As String() = New String() {"Rectangle", "Rounded Rectangle", "Ellipse", "Ellipse Segment", "Elliptical Arc", "Pie", "Circle", "Circle Segment", "Triangle", "Quad", "Polygon", "Line Segment", "Polyline", "Cubic Bezier", "Nurbs Curve", "Path with Multiple Figures"}
            Dim paths As Nevron.Nov.Graphics.NGraphicsPath() = Me.CreatePaths(Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.DefaultCanvasWidth, Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.DefaultCanvasHeight)
            Dim count As Integer = paths.Length

            For i As Integer = 0 To count - 1
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Me.m_Table.Add(stack)
                stack.VerticalSpacing = 5

				' Create a canvas to draw the graphics path in
				Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
                canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.DefaultCanvasWidth, Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.DefaultCanvasHeight)
                canvas.Tag = paths(i)
                AddHandler canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
                canvas.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGreen)
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
            AddHandler Me.m_StrokeColorBox.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnStrokeColorBoxSelectedColorChanged)

			' Stroke width
			Me.m_StrokeWidthCombo = New Nevron.Nov.UI.NComboBox()

            For i As Integer = 0 To 6 - 1
                Me.m_StrokeWidthCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem(i.ToString()))
            Next

            Me.m_StrokeWidthCombo.SelectedIndex = 1
            AddHandler Me.m_StrokeWidthCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnStrokeWidthComboSelectedIndexChanged)

			' Canvas width editor
			Me.m_CanvasWidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasWidthUpDown.Minimum = 100
            Me.m_CanvasWidthUpDown.Maximum = 350
            Me.m_CanvasWidthUpDown.Value = Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.DefaultCanvasWidth
            Me.m_CanvasWidthUpDown.[Step] = 1
            Me.m_CanvasWidthUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasWidthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' Canvas height editor
			Me.m_CanvasHeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasHeightUpDown.Minimum = 100
            Me.m_CanvasHeightUpDown.Maximum = 350
            Me.m_CanvasHeightUpDown.Value = Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.DefaultCanvasHeight
            Me.m_CanvasHeightUpDown.[Step] = 1
            Me.m_CanvasHeightUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasHeightUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)
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
	This example demonstrates the graphics path's capabilities for painting of geometric primitives.
	You can use the controls in the right-side panel to change the fill and stroke of the graphics paths.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = CType(canvas.Tag, Nevron.Nov.Graphics.NGraphicsPath)
            args.PaintVisitor.ClearStyles()
            args.PaintVisitor.SetStroke(Me.m_Stroke)
            args.PaintVisitor.SetFill(Me.m_Fill)
            args.PaintVisitor.PaintPath(path, Nevron.Nov.Graphics.ENFillRule.EvenOdd)
        End Sub

        Private Sub OnNumericUpDownValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_Table Is Nothing Then Return
            Dim width As Double = Me.m_CanvasWidthUpDown.Value
            Dim height As Double = Me.m_CanvasHeightUpDown.Value

			' Recreate all graphics paths
			Dim paths As Nevron.Nov.Graphics.NGraphicsPath() = Me.CreatePaths(width, height)
            Dim index As Integer = 0

			' Resize the canvases
			Dim iterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Dom.NNode) = Me.m_Table.GetSubtreeIterator(Nevron.Nov.Dom.ENTreeTraversalOrder.DepthFirstPreOrder, New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NCanvas.NCanvasSchema))

            While iterator.MoveNext()
                Dim canvas As Nevron.Nov.UI.NCanvas = CType(iterator.Current, Nevron.Nov.UI.NCanvas)
                CType(canvas.ParentNode, Nevron.Nov.UI.NWidget).PreferredWidth = width
                canvas.PreferredHeight = height
                canvas.Tag = paths(System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1))
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

        Private Function CreatePaths(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath()
            Dim paths As Nevron.Nov.Graphics.NGraphicsPath() = New Nevron.Nov.Graphics.NGraphicsPath() {Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateRectangle(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateRoundedRectangle(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateEllipse(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateEllipseSegment(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateEllipticalArc(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreatePie(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateCircle(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateCircleSegment(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateTriangle(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateQuad(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreatePolygon(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateLineSegment(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreatePolyline(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateCubicBezier(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreateNurbsCurve(w, h), Nevron.Nov.Examples.Framework.NPaintingGraphicsPathsExample.CreatePathWithMultipleFigures(w, h)}
            Return paths
        End Function

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

		#Region"Static Methods"

		Private Shared Function CreateRectangle(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddRectangle(0.05 * w, 0.25 * h, 0.9 * w, 0.5 * h)
            Return path
        End Function

        Private Shared Function CreateRoundedRectangle(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim rect As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(0.05 * w, 0.25 * h, 0.9 * w, 0.5 * h)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddRoundedRectangle(rect, h * 0.05)
            Return path
        End Function

        Private Shared Function CreateEllipse(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddEllipse(0.05 * w, 0.25 * h, 0.9 * w, 0.5 * h)
            Return path
        End Function

        Private Shared Function CreateEllipseSegment(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim rect As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(0.05 * w, 0.25 * h, 0.9 * w, 0.5 * h)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddEllipseSegment(rect, Nevron.Nov.NMath.PI * 0.1, Nevron.Nov.NMath.PI * 1.2)
            Return path
        End Function

        Private Shared Function CreateEllipticalArc(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim start As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(w * 0.3, h * 0.85)
            Dim control As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(w * 0.5, h * 0.15)
            Dim [end] As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(w * 0.8, h * 0.85)
            Dim angle As Double = 1
            Dim ratio As Double = 1.4
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddEllipticalArc(start, control, [end], angle, ratio)
            Return path
        End Function

        Private Shared Function CreatePie(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddPie(0.1 * w, 0.1 * h, 0.8 * w, 0.8 * h, 0.25 * Nevron.Nov.NMath.PI, 1.5 * Nevron.Nov.NMath.PI)
            Return path
        End Function

        Private Shared Function CreateCircle(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim radius As Double = 0.4 * Nevron.Nov.NMath.Min(w, h)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddCircle(0.5 * w, 0.5 * h, radius)
            Return path
        End Function

        Private Shared Function CreateCircleSegment(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim radius As Double = 0.4 * Nevron.Nov.NMath.Min(w, h)
            Dim center As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.5 * w, 0.5 * h)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddCircleSegment(center, radius, 0.25 * Nevron.Nov.NMath.PI, 1.5 * Nevron.Nov.NMath.PI)
            Return path
        End Function

        Private Shared Function CreateTriangle(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim triangle As Nevron.Nov.Graphics.NTriangle = New Nevron.Nov.Graphics.NTriangle()
            triangle.A = New Nevron.Nov.Graphics.NPoint(0.5 * w, 0.1 * h)
            triangle.B = New Nevron.Nov.Graphics.NPoint(0.9 * w, 0.9 * h)
            triangle.C = New Nevron.Nov.Graphics.NPoint(0.1 * w, 0.8 * h)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddTriangle(triangle)
            Return path
        End Function

        Private Shared Function CreateQuad(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim quad As Nevron.Nov.Graphics.NQuadrangle = New Nevron.Nov.Graphics.NQuadrangle()
            quad.A = New Nevron.Nov.Graphics.NPoint(0.2 * w, 0.1 * h)
            quad.B = New Nevron.Nov.Graphics.NPoint(0.6 * w, 0.1 * h)
            quad.C = New Nevron.Nov.Graphics.NPoint(0.9 * w, 0.9 * h)
            quad.D = New Nevron.Nov.Graphics.NPoint(0.1 * w, 0.6 * h)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddQuad(quad)
            Return path
        End Function

        Private Shared Function CreatePolygon(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim polygon As Nevron.Nov.Graphics.NPolygon = New Nevron.Nov.Graphics.NPolygon(6)
            polygon.Add(0.3 * w, 0.1 * h)
            polygon.Add(0.7 * w, 0.1 * h)
            polygon.Add(0.5 * w, 0.4 * h)
            polygon.Add(0.9 * w, 0.9 * h)
            polygon.Add(0.2 * w, 0.8 * h)
            polygon.Add(0.1 * w, 0.4 * h)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddPolygon(polygon)
            Return path
        End Function

        Private Shared Function CreateLineSegment(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddLineSegment(0.2 * w, 0.1 * h, 0.9 * w, 0.9 * h)
            Return path
        End Function

        Private Shared Function CreatePolyline(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim points As Nevron.Nov.Graphics.NPoint() = New Nevron.Nov.Graphics.NPoint() {New Nevron.Nov.Graphics.NPoint(0.1 * w, 0.1 * h), New Nevron.Nov.Graphics.NPoint(0.9 * w, 0.3 * h), New Nevron.Nov.Graphics.NPoint(0.2 * w, 0.6 * h), New Nevron.Nov.Graphics.NPoint(0.8 * w, 0.7 * h), New Nevron.Nov.Graphics.NPoint(0.6 * w, 0.9 * h)}
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddPolyline(points)
            Return path
        End Function

        Private Shared Function CreateCubicBezier(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim start As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.1 * w, 0.1 * h)
            Dim c1 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.8 * w, 0.0 * h)
            Dim c2 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.5 * w, 1.0 * h)
            Dim [end] As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.9 * w, 0.9 * h)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddCubicBezier(start, c1, c2, [end])
            Return path
        End Function

        Private Shared Function CreateNurbsCurve(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim nurbs As Nevron.Nov.Graphics.NNurbsCurve = New Nevron.Nov.Graphics.NNurbsCurve(3)
            nurbs.ControlPoints.Add(New Nevron.Nov.Graphics.NNurbsControlPoint(0.05 * w, 0.50 * h))
            nurbs.ControlPoints.Add(New Nevron.Nov.Graphics.NNurbsControlPoint(0.20 * w, 0.20 * h))
            nurbs.ControlPoints.Add(New Nevron.Nov.Graphics.NNurbsControlPoint(0.40 * w, 0.00 * h))
            nurbs.ControlPoints.Add(New Nevron.Nov.Graphics.NNurbsControlPoint(0.60 * w, 0.50 * h))
            nurbs.ControlPoints.Add(New Nevron.Nov.Graphics.NNurbsControlPoint(0.60 * w, 0.70 * h))
            nurbs.ControlPoints.Add(New Nevron.Nov.Graphics.NNurbsControlPoint(0.30 * w, 0.95 * h))
            nurbs.ControlPoints.Add(New Nevron.Nov.Graphics.NNurbsControlPoint(0.40 * w, 0.50 * h))
            nurbs.ControlPoints.Add(New Nevron.Nov.Graphics.NNurbsControlPoint(0.90 * w, 0.00 * h))
            nurbs.ControlPoints.Add(New Nevron.Nov.Graphics.NNurbsControlPoint(0.95 * w, 0.50 * h))
            nurbs.Knots.AddRange(New Double() {0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 6, 6, 6})
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddNurbsCurve(nurbs)
            Return path
        End Function

        Private Shared Function CreatePathWithMultipleFigures(ByVal w As Double, ByVal h As Double) As Nevron.Nov.Graphics.NGraphicsPath
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.StartFigure(0.05 * w, 0.05 * h)
            path.LineTo(0.4 * w, 0.2 * h)
            path.LineTo(0.54 * w, 0.5 * h)
            path.CircularArcTo(New Nevron.Nov.Graphics.NPoint(0.05 * w, 0.5 * h), 12)
            path.CloseFigure()
            path.StartFigure(0.05 * w, 0.95 * h)
            path.LineTo(0.05 * w, 0.6 * h)
            path.CubicBezierTo(0.6 * w, 0.7 * h, 0.2 * w, 0.1 * h, 0.5 * w, 0.7 * h)
            path.LineTo(0.6 * w, 0.95 * h)
            path.CloseFigure()
            Dim points As Nevron.Nov.Graphics.NPoint() = New Nevron.Nov.Graphics.NPoint() {New Nevron.Nov.Graphics.NPoint(0.95 * w, 0.05 * h), New Nevron.Nov.Graphics.NPoint(0.95 * w, 0.95 * h), New Nevron.Nov.Graphics.NPoint(0.65 * w, 0.95 * h), New Nevron.Nov.Graphics.NPoint(0.65 * w, 0.60 * h)}
            path.StartFigure(0.4 * w, 0.05 * h)
            path.LineTos(points, 0, points.Length)
            path.CloseFigure()
            path.StartFigure(0.7 * w, 0.1 * h)
            path.EllipticalArcTo(New Nevron.Nov.Graphics.NPoint(0.7 * w, 0.3 * h), New Nevron.Nov.Graphics.NPoint(0.9 * w, 0.2 * h), 0, 2)
            path.CloseFigure()
            path.StartFigure(0.7 * w, 0.6 * h)
            path.CircularArcTo(New Nevron.Nov.Graphics.NPoint(0.7 * w, 0.8 * h), 0.2 * w)
            path.CloseFigure()
            Return path
        End Function

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NPaintingGraphicsPathsExample.
		''' </summary>
		Public Shared ReadOnly NPaintingGraphicsPathsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const DefaultCanvasWidth As Integer = 220
        Private Const DefaultCanvasHeight As Integer = 220

		#EndRegion
	End Class
End Namespace
