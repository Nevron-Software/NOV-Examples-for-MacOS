Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NPaintingImagesExample
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
            Nevron.Nov.Examples.Framework.NPaintingImagesExample.NPaintingImagesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NPaintingImagesExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_PaintImageInRectangle = False
            Me.m_PosX = 10
            Me.m_PosY = 10
            Me.m_Width = 200
            Me.m_Height = 200
            Me.m_Canvas = New Nevron.Nov.UI.NCanvas()
            Me.m_Canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(800, 600)
            Me.m_Canvas.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(220, 220, 200))
            Me.m_Canvas.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Me.m_Canvas.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            AddHandler Me.m_Canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
            Dim scroll As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
            scroll.Content = Me.m_Canvas
            scroll.NoScrollHAlign = Nevron.Nov.UI.ENNoScrollHAlign.Center
            scroll.NoScrollVAlign = Nevron.Nov.UI.ENNoScrollVAlign.Center
            Return scroll
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim drawAtPointButton As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton("Draw Image at Point")
            Dim drawInRectButton As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton("Draw Image in Rectangle")
            Dim radioStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            radioStack.Add(drawAtPointButton)
            radioStack.Add(drawInRectButton)
            Me.m_RadioGroup = New Nevron.Nov.UI.NRadioButtonGroup()
            Me.m_RadioGroup.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_RadioGroup.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_RadioGroup.Content = radioStack
            Me.m_RadioGroup.SelectedIndex = If(Me.m_PaintImageInRectangle, 1, 0)
            AddHandler Me.m_RadioGroup.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueChanged)

			' Image X position editor
			Me.m_PositionXUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_PositionXUpDown.Minimum = 0
            Me.m_PositionXUpDown.Maximum = 800
            Me.m_PositionXUpDown.Value = Me.m_PosX
            Me.m_PositionXUpDown.[Step] = 1
            Me.m_PositionXUpDown.DecimalPlaces = 0
            AddHandler Me.m_PositionXUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueChanged)

			' Image Y position editor
			Me.m_PositionYUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_PositionYUpDown.Minimum = 0
            Me.m_PositionYUpDown.Maximum = 600
            Me.m_PositionYUpDown.Value = Me.m_PosY
            Me.m_PositionYUpDown.[Step] = 1
            Me.m_PositionYUpDown.DecimalPlaces = 0
            AddHandler Me.m_PositionYUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueChanged)

			' Image height editor
			Me.m_WidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_WidthUpDown.Enabled = Me.m_PaintImageInRectangle
            Me.m_WidthUpDown.Minimum = 0
            Me.m_WidthUpDown.Maximum = 400
            Me.m_WidthUpDown.Value = Me.m_Width
            Me.m_WidthUpDown.[Step] = 1
            Me.m_WidthUpDown.DecimalPlaces = 0
            AddHandler Me.m_WidthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueChanged)

			' Image height editor
			Me.m_HeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_HeightUpDown.Enabled = Me.m_PaintImageInRectangle
            Me.m_HeightUpDown.Minimum = 0
            Me.m_HeightUpDown.Maximum = 400
            Me.m_HeightUpDown.Value = Me.m_Height
            Me.m_HeightUpDown.[Step] = 1
            Me.m_HeightUpDown.DecimalPlaces = 0
            AddHandler Me.m_HeightUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueChanged)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None
            stack.Add(Me.m_RadioGroup)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("X Position:", Me.m_PositionXUpDown))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Y Position:", Me.m_PositionYUpDown))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Width:", Me.m_WidthUpDown))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Height:", Me.m_HeightUpDown))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the image painting capabilities of the NOV graphics.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return
            Dim pv As Nevron.Nov.Dom.NPaintVisitor = args.PaintVisitor
            pv.ClearStyles()

            If Me.m_PaintImageInRectangle Then
                pv.PaintImage(NResources.Image_JpegSuite_q080_jpg.ImageSource, New Nevron.Nov.Graphics.NRectangle(Me.m_PosX, Me.m_PosY, Me.m_Width, Me.m_Height))
            Else
                pv.PaintImage(NResources.Image_JpegSuite_q080_jpg.ImageSource, New Nevron.Nov.Graphics.NPoint(Me.m_PosX, Me.m_PosY))
            End If

			' paint a border around the canvas
			pv.SetStroke(Nevron.Nov.Graphics.NColor.Black, 1)
            pv.PaintRectangle(0, 0, canvas.Width, canvas.Height)
        End Sub

        Private Sub OnValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PaintImageInRectangle = (Me.m_RadioGroup.SelectedIndex = 1)
            Me.m_PosX = Me.m_PositionXUpDown.Value
            Me.m_PosY = Me.m_PositionYUpDown.Value
            Me.m_Width = Me.m_WidthUpDown.Value
            Me.m_Height = Me.m_HeightUpDown.Value
            Me.m_WidthUpDown.Enabled = Me.m_PaintImageInRectangle
            Me.m_HeightUpDown.Enabled = Me.m_PaintImageInRectangle
            Me.m_Canvas.InvalidateDisplay()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Canvas As Nevron.Nov.UI.NCanvas
        Private m_RadioGroup As Nevron.Nov.UI.NRadioButtonGroup
        Private m_PositionXUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_PositionYUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_WidthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_HeightUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_PaintImageInRectangle As Boolean
        Private m_PosX As Double
        Private m_PosY As Double
        Private m_Width As Double
        Private m_Height As Double

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NPaintingImagesExample.
		''' </summary>
		Public Shared ReadOnly NPaintingImagesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
