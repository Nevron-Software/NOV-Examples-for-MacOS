Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NShadowExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' 
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' 
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Framework.NShadowExample.NShadowExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NShadowExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Shadow = New Nevron.Nov.Graphics.NShadow(New Nevron.Nov.Graphics.NColor(160, 160, 160), 20, 20)
            Dim names As String() = New String() {"Line", "Polyline", "Rectangle", "Ellipse", "Triangle", "Quad", "Polygon", "Graphics Path"}
            Dim delegates As Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate() = New Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate() {New Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate(AddressOf Me.PaintLine), New Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate(AddressOf Me.PaintPolyline), New Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate(AddressOf Me.PaintRectangle), New Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate(AddressOf Me.PaintEllipse), New Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate(AddressOf Me.PaintTriangle), New Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate(AddressOf Me.PaintQuadrangle), New Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate(AddressOf Me.PaintPolygon), New Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate(AddressOf Me.PaintPath)}
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
                canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NShadowExample.DefaultCanvasWidth, Nevron.Nov.Examples.Framework.NShadowExample.DefaultCanvasHeight)
                canvas.Tag = delegates(i)
                canvas.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
                AddHandler canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
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
			' get editors for shadow properties
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Shadow), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Shadow, Nevron.Nov.Graphics.NShadow.ColorProperty, Nevron.Nov.Graphics.NShadow.AlignXFactorProperty, Nevron.Nov.Graphics.NShadow.AlignYFactorProperty, Nevron.Nov.Graphics.NShadow.OffsetXProperty, Nevron.Nov.Graphics.NShadow.OffsetYProperty, Nevron.Nov.Graphics.NShadow.ScaleXProperty, Nevron.Nov.Graphics.NShadow.ScaleYProperty, Nevron.Nov.Graphics.NShadow.SkewXProperty, Nevron.Nov.Graphics.NShadow.SkewYProperty, Nevron.Nov.Graphics.NShadow.UseFillAndStrokeAlphaProperty, Nevron.Nov.Graphics.NShadow.ApplyToFillingProperty, Nevron.Nov.Graphics.NShadow.ApplyToOutlineProperty)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            AddHandler Me.m_Shadow.Changed, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnEditShadowChanged)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the built-in shadows.
	Use the controls to the right to modify various properties of the shadow.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return
            Dim paintDelegate As Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate = TryCast(canvas.Tag, Nevron.Nov.Examples.Framework.NShadowExample.PaintPrimitiveDelegate)
            If paintDelegate Is Nothing Then Throw New System.Exception("The canvas has no assigned paint delegate.")

			' Clear all styles and set the shadow
			args.PaintVisitor.ClearStyles()
            args.PaintVisitor.SetShadow(Me.m_Shadow)

			' Paint the scene for the current canvas
			paintDelegate(args.PaintVisitor, canvas.Width, canvas.Height)

			' Paint a bounding rectangle for the canvas
			args.PaintVisitor.ClearStyles()
            args.PaintVisitor.SetStroke(Nevron.Nov.Graphics.NColor.Red, 1)
            args.PaintVisitor.PaintRectangle(0, 0, canvas.Width, canvas.Height)
        End Sub

        Private Sub OnEditShadowChanged(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim localValueChangeArgs As Nevron.Nov.Dom.NValueChangeEventArgs = TryCast(args, Nevron.Nov.Dom.NValueChangeEventArgs)

            If localValueChangeArgs IsNot Nothing Then
                Me.InvalidateCanvases()
            End If
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

        Private Sub PaintLine(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            paintVisitor.SetStroke(Nevron.Nov.Graphics.NColor.Black, 2)
            paintVisitor.PaintLine(0.2 * w, 0.8 * h, 0.7 * w, 0.2 * h)
        End Sub

        Private Sub PaintPolyline(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            paintVisitor.SetStroke(Nevron.Nov.Graphics.NColor.Black, 2)
            Dim polyline As Nevron.Nov.Graphics.NPolyline = New Nevron.Nov.Graphics.NPolyline(5)
            polyline.Add(0.2 * w, 0.2 * h)
            polyline.Add(0.4 * w, 0.3 * h)
            polyline.Add(0.3 * w, 0.5 * h)
            polyline.Add(0.4 * w, 0.7 * h)
            polyline.Add(0.8 * w, 0.8 * h)
            paintVisitor.PaintPolyline(polyline)
        End Sub

        Private Sub PaintRectangle(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            Dim lgf As Nevron.Nov.Graphics.NLinearGradientFill = New Nevron.Nov.Graphics.NLinearGradientFill()
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0, Nevron.Nov.Graphics.NColor.Indigo))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.5F, Nevron.Nov.Graphics.NColor.SlateBlue))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1, New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Crimson, 30)))
            paintVisitor.SetStroke(Nevron.Nov.Graphics.NColor.Black, 1)
            paintVisitor.SetFill(lgf)
            paintVisitor.PaintRectangle(0.2 * w, 0.3 * h, 0.6 * w, 0.4 * h)
        End Sub

        Private Sub PaintEllipse(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            Dim rgf As Nevron.Nov.Graphics.NRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0, Nevron.Nov.Graphics.NColor.Indigo))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.6F, Nevron.Nov.Graphics.NColor.SlateBlue))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1, New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Crimson, 30)))
            paintVisitor.SetStroke(Nevron.Nov.Graphics.NColor.Black, 1)
            paintVisitor.SetFill(rgf)
            paintVisitor.PaintEllipse(0.2 * w, 0.3 * h, 0.6 * w, 0.4 * h)
        End Sub

        Private Sub PaintTriangle(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            paintVisitor.SetStroke(Nevron.Nov.Graphics.NColor.Black, 2)
            paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.Crimson)
            Dim p1 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.5 * w, 0.2 * h)
            Dim p2 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.8 * w, 0.8 * h)
            Dim p3 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.2 * w, 0.7 * h)
            paintVisitor.PaintTriangle(p1, p2, p3)
        End Sub

        Private Sub PaintQuadrangle(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            paintVisitor.SetStroke(Nevron.Nov.Graphics.NColor.Black, 2)
            paintVisitor.SetFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Crimson, 128))
            Dim p1 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.3 * w, 0.2 * h)
            Dim p2 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.6 * w, 0.2 * h)
            Dim p3 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.8 * w, 0.8 * h)
            Dim p4 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(0.2 * w, 0.6 * h)
            paintVisitor.PaintQuadrangle(p1, p2, p3, p4)
        End Sub

        Private Sub PaintPolygon(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            paintVisitor.SetStroke(New Nevron.Nov.Graphics.NColor(0, 0, 0, 160), 6)
            paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.GreenYellow)
            Dim polygon As Nevron.Nov.Graphics.NPolygon = New Nevron.Nov.Graphics.NPolygon(6)
            polygon.Add(0.3 * w, 0.2 * h)
            polygon.Add(0.6 * w, 0.2 * h)
            polygon.Add(0.5 * w, 0.4 * h)
            polygon.Add(0.8 * w, 0.8 * h)
            polygon.Add(0.3 * w, 0.7 * h)
            polygon.Add(0.2 * w, 0.4 * h)
            paintVisitor.PaintPolygon(polygon, Nevron.Nov.Graphics.ENFillRule.EvenOdd)
        End Sub

        Private Sub PaintPath(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)
            paintVisitor.SetStroke(New Nevron.Nov.Graphics.NColor(0, 0, 0, 160), 6)
            paintVisitor.SetFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.GreenYellow, 128))
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.StartFigure(0.2 * w, 0.2 * h)
            path.LineTo(0.7 * w, 0.3 * h)
            path.CubicBezierTo(0.8 * w, 0.8 * h, 1 * w, 0.4 * h, 0.5 * w, 0.7 * h)
            path.LineTo(0.3 * w, 0.7 * h)
            path.CubicBezierTo(0.2 * w, 0.2 * h, 0.3 * w, 0.7 * h, 0.4 * w, 0.6 * h)
            path.CloseFigure()
            paintVisitor.PaintPath(path)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Shadow As Nevron.Nov.Graphics.NShadow
        Private m_Table As Nevron.Nov.UI.NTableFlowPanel

		#EndRegion

		#Region"Constants"

		Private Const DefaultCanvasWidth As Integer = 220
        Private Const DefaultCanvasHeight As Integer = 220

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NShadowExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Nested Types"

		Friend Delegate Sub PaintPrimitiveDelegate(ByVal paintVisitor As Nevron.Nov.Dom.NPaintVisitor, ByVal w As Double, ByVal h As Double)

		#EndRegion
	End Class
End Namespace
