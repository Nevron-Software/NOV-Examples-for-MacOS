Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NSolidColorFillExample
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
            Nevron.Nov.Examples.Framework.NSolidColorFillExample.NSolidColorFillExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NSolidColorFillExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Canvas = New Nevron.Nov.UI.NCanvas()
            Me.m_Canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NSolidColorFillExample.W, Nevron.Nov.Examples.Framework.NSolidColorFillExample.H)
            Me.m_Canvas.BackgroundFill = New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.LargeCheckerBoard, Nevron.Nov.Graphics.NColor.LightGray, Nevron.Nov.Graphics.NColor.White)
            AddHandler Me.m_Canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
            Me.m_Canvas.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Me.m_Canvas.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Dim scroll As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
            scroll.Content = Me.m_Canvas
            scroll.NoScrollHAlign = Nevron.Nov.UI.ENNoScrollHAlign.Center
            scroll.NoScrollVAlign = Nevron.Nov.UI.ENNoScrollVAlign.Center
            Return scroll
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
			' Create background color editor
			Dim colorBox As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            colorBox.SelectedColor = Me.m_ColorFills(CInt((0))).Color
            AddHandler colorBox.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnColorBoxSelectedColorChanged)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Detached Slice's Color:", colorBox))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the most common fill type - the solid color fill. A solid color fill paints the interior of a shape or graphics path with a single solid color (opaque or semi-transparent).
	In this example each pie slice is filled with a different solid color fill. You can change the color of the detached pie slice using the color combo box in the upper-right corner.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return
            Dim pv As Nevron.Nov.Dom.NPaintVisitor = args.PaintVisitor
            Dim count As Integer = Me.m_Values.Length

			' calculate total value
			Dim total As Double = 0

            For i As Integer = 0 To count - 1
                total += Me.m_Values(i)
            Next

			' paint the pie slices
			Dim beginAngle As Double = 0
            pv.ClearStyles()

            For i As Integer = 0 To count - 1
                Dim sweepAngle As Double = Nevron.Nov.NMath.PI2 * (Me.m_Values(i) / total)
                Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
                path.AddPie(0.1 * Nevron.Nov.Examples.Framework.NSolidColorFillExample.W, 0.1 * Nevron.Nov.Examples.Framework.NSolidColorFillExample.H, 0.8 * Nevron.Nov.Examples.Framework.NSolidColorFillExample.W, 0.8 * Nevron.Nov.Examples.Framework.NSolidColorFillExample.H, beginAngle, sweepAngle)

                If i = 0 Then
                    Const detachment As Double = 20
                    Dim midAngle As Double = beginAngle + sweepAngle / 2
                    Dim dx As Double = System.Math.Cos(midAngle) * detachment
                    Dim dy As Double = System.Math.Sin(midAngle) * detachment
                    path.Translate(dx, dy)
                End If

                pv.SetFill(Me.m_ColorFills(i))
                pv.PaintPath(path)
                beginAngle += sweepAngle
            Next

			' paint a border around the canvas
			pv.ClearFill()
            pv.SetStroke(Nevron.Nov.Graphics.NColor.Black, 1)
            pv.PaintRectangle(0, 0, canvas.Width, canvas.Height)
        End Sub

        Private Sub OnColorBoxSelectedColorChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ColorFills(0) = New Nevron.Nov.Graphics.NColorFill(CType(args.NewValue, Nevron.Nov.Graphics.NColor))
            Me.m_Canvas.InvalidateDisplay()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Canvas As Nevron.Nov.UI.NCanvas
        Private m_ColorFills As Nevron.Nov.Graphics.NColorFill() = New Nevron.Nov.Graphics.NColorFill() {New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.IndianRed, 0.5F)), New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Peru), New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkKhaki), New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.OliveDrab), New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkSeaGreen), New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.MediumSeaGreen), New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.SteelBlue), New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.SlateBlue), New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.MediumOrchid), New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.HotPink)} ' semi-transparent
        Private m_Values As Double() = New Double() {40, 20, 15, 19, 27, 29, 21, 32, 19, 14}

		#EndRegion

		#Region"Constants"

		Private Const W As Integer = 400
        Private Const H As Integer = 400

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NSolidColorFillExample.
		''' </summary>
		Public Shared ReadOnly NSolidColorFillExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
