Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NStrokeExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default Constructor.
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' Static Constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Framework.NStrokeExample.NStrokeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NStrokeExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim width As Double = 1
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Me.m_arrStrokes = New Nevron.Nov.Graphics.NStroke() {New Nevron.Nov.Graphics.NStroke(width, color, Nevron.Nov.Graphics.ENDashStyle.Solid), New Nevron.Nov.Graphics.NStroke(width, color, Nevron.Nov.Graphics.ENDashStyle.Dot), New Nevron.Nov.Graphics.NStroke(width, color, Nevron.Nov.Graphics.ENDashStyle.Dash), New Nevron.Nov.Graphics.NStroke(width, color, Nevron.Nov.Graphics.ENDashStyle.DashDot), New Nevron.Nov.Graphics.NStroke(width, color, Nevron.Nov.Graphics.ENDashStyle.DashDotDot), New Nevron.Nov.Graphics.NStroke(width, color, New Nevron.Nov.Graphics.NDashPattern(2, 2, 2, 2, 0, 2))}
            Me.m_EditStroke = New Nevron.Nov.Graphics.NStroke()
            Me.m_EditStroke.Width = width
            Me.m_EditStroke.Color = color
            Me.m_EditStroke.DashCap = Nevron.Nov.Graphics.ENLineCap.Square
            Me.m_EditStroke.StartCap = Nevron.Nov.Graphics.ENLineCap.Square
            Me.m_EditStroke.EndCap = Nevron.Nov.Graphics.ENLineCap.Square

            For i As Integer = 0 To Me.m_arrStrokes.Length - 1
                Dim stroke As Nevron.Nov.Graphics.NStroke = Me.m_arrStrokes(i)
                stroke.DashCap = Me.m_EditStroke.DashCap
                stroke.StartCap = Me.m_EditStroke.StartCap
                stroke.EndCap = Me.m_EditStroke.EndCap
            Next

            Me.m_LabelFont = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 12, Nevron.Nov.Graphics.ENFontStyle.Bold)
            Me.m_LabelFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Black)
            Me.m_CanvasStack = New Nevron.Nov.UI.NStackPanel()
            Me.m_CanvasStack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            Me.m_CanvasStack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None
            Dim preferredSize As Nevron.Nov.Graphics.NSize = Me.GetCanvasPreferredSize(Me.m_EditStroke.Width)

            For i As Integer = 0 To Me.m_arrStrokes.Length - 1
                Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
                AddHandler canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
                canvas.PreferredSize = preferredSize
                canvas.Tag = Me.m_arrStrokes(i)
                canvas.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
                Me.m_CanvasStack.Add(canvas)
            Next

			' The stack must be scrollable
			Dim scroll As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
            scroll.Content = Me.m_CanvasStack
            Return scroll
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None

			' get editors for stroke properties
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_EditStroke), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_EditStroke, Nevron.Nov.Graphics.NStroke.WidthProperty, Nevron.Nov.Graphics.NStroke.ColorProperty, Nevron.Nov.Graphics.NStroke.DashCapProperty, Nevron.Nov.Graphics.NStroke.StartCapProperty, Nevron.Nov.Graphics.NStroke.EndCapProperty, Nevron.Nov.Graphics.NStroke.LineJoinProperty)

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            AddHandler Me.m_EditStroke.Changed, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnEditStrokeChanged)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to use strokes. You can specify the width and the color of a stroke,
	as well as its dash properties. NOV supports the following dash styles:
	<ul>
		<li>Solid - specifies a solid line.</li>
		<li>Dash - specifies a line consisting of dashes.</li>
		<li>Dot - specifies a line consisting of dots.</li>
		<li>DashDot - specifies a line consisting of a repeating pattern of dash-dot.</li>
		<li>DashDotDot - specifies a line consisting of a repeating pattern of dash-dot-dot.</li>
		<li>Custom - specifies a user-defined custom dash style.</li>
	</ul>

	Use the controls to the right to modify various properties of the stroke.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return
            Dim stroke As Nevron.Nov.Graphics.NStroke = CType(canvas.Tag, Nevron.Nov.Graphics.NStroke)
            Dim visitor As Nevron.Nov.Dom.NPaintVisitor = args.PaintVisitor
            visitor.SetStroke(stroke)
            visitor.SetFill(Nothing)
            Dim strokeWidth As Double = stroke.Width
            Dim rectWidth As Double = 300
            Dim ellipseWidth As Double = 150
            Dim polylineWidth As Double = 180
            Dim dist As Double = 20
            Dim x1 As Double = 10 + strokeWidth / 2
            Dim x2 As Double = x1 + rectWidth + dist + strokeWidth
            Dim x3 As Double = x2 + ellipseWidth
            Dim x4 As Double = x3 + dist + strokeWidth
            Dim x5 As Double = x4 + polylineWidth + dist + strokeWidth / 2
            Dim y1 As Double = 10 + strokeWidth / 2
            Dim y2 As Double = y1 + strokeWidth + 10
            Dim y3 As Double = y1 + 50

			' draw a horizontal line
			visitor.PaintLine(x1, y1, x3, y1)

			' draw a rectangle
			visitor.PaintRectangle(x1, y2, rectWidth, 100)

			' draw an ellipse
			visitor.PaintEllipse(x2, y2, ellipseWidth, 100)

			' draw a polyline
			Dim polyLine As Nevron.Nov.Graphics.NPolyline = New Nevron.Nov.Graphics.NPolyline(4)
            polyLine.Add(New Nevron.Nov.Graphics.NPoint(x4, y2 + 90))
            polyLine.Add(New Nevron.Nov.Graphics.NPoint(x4 + 60, y2))
            polyLine.Add(New Nevron.Nov.Graphics.NPoint(x4 + 120, y2 + 90))
            polyLine.Add(New Nevron.Nov.Graphics.NPoint(x4 + 180, y2))
            visitor.PaintPolyline(polyLine)

			' draw text
			Dim dashStyleName As String = stroke.DashStyle.ToString()
            visitor.ClearStroke()
            visitor.SetFont(Me.m_LabelFont)
            visitor.SetFill(Me.m_LabelFill)
            Dim settings As Nevron.Nov.Graphics.NPaintTextRectSettings = New Nevron.Nov.Graphics.NPaintTextRectSettings()
            visitor.PaintString(New Nevron.Nov.Graphics.NRectangle(x5, y3, 200, 50), dashStyleName, settings)
        End Sub

        Private Sub OnEditStrokeChanged(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim localValueChangeArgs As Nevron.Nov.Dom.NValueChangeEventArgs = TryCast(args, Nevron.Nov.Dom.NValueChangeEventArgs)

            If localValueChangeArgs IsNot Nothing Then
                For i As Integer = 0 To Me.m_arrStrokes.Length - 1
                    Dim stroke As Nevron.Nov.Graphics.NStroke = Me.m_arrStrokes(i)
                    stroke.SetValue(localValueChangeArgs.[Property], localValueChangeArgs.NewValue)
                    Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(Me.m_CanvasStack(i), Nevron.Nov.UI.NCanvas)
                    Dim strokeWidth As Double = stroke.Width
                    If strokeWidth < 0 Then strokeWidth = 0
                    canvas.PreferredSize = Me.GetCanvasPreferredSize(strokeWidth)

                    If canvas IsNot Nothing Then
                        canvas.InvalidateDisplay()
                    End If
                Next
            End If
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function GetCanvasPreferredSize(ByVal strokeWidth As Double) As Nevron.Nov.Graphics.NSize
            Return New Nevron.Nov.Graphics.NSize(850 + 3 * strokeWidth, 150 + 2 * strokeWidth)
        End Function

		#EndRegion

		#Region"Fields"

		Private m_LabelFont As Nevron.Nov.Graphics.NFont
        Private m_LabelFill As Nevron.Nov.Graphics.NFill
        Private m_EditStroke As Nevron.Nov.Graphics.NStroke
        Private m_arrStrokes As Nevron.Nov.Graphics.NStroke()
        Private m_CanvasStack As Nevron.Nov.UI.NStackPanel

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStrokeExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
