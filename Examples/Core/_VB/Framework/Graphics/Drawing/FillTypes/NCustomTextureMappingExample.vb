Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports System.Runtime.InteropServices

Namespace Nevron.Nov.Examples.Framework
    Public Class NCustomTextureMappingExample
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
            Nevron.Nov.Examples.Framework.NCustomTextureMappingExample.NCustomTextureMappingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NCustomTextureMappingExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_PathAngle = 0
            Me.m_PathPositionX = 200
            Me.m_PathPositionY = 100
            Me.m_Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Red)

			' create an image fill using an embedded image
			Me.m_ImageFill = New Nevron.Nov.Graphics.NImageFill(NResources.Image_Artistic_Plane_png)

			' create a custom texture mapping and assign it to the image fill
			Me.m_MyTextureMapping = New Nevron.Nov.Examples.Framework.MyTextureMapping()
            Me.m_MyTextureMapping.TextureAngle = 45
            Me.m_MyTextureMapping.PinPoint = New Nevron.Nov.Graphics.NPoint(Me.m_PathPositionX, Me.m_PathPositionY)
            Me.m_ImageFill.TextureMapping = Me.m_MyTextureMapping
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
			' path rotation angle editor
			Me.m_PathAngleSpin = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_PathAngleSpin.Minimum = 0
            Me.m_PathAngleSpin.Maximum = 360
            Me.m_PathAngleSpin.Value = Me.m_PathAngle
            Me.m_PathAngleSpin.[Step] = 1
            Me.m_PathAngleSpin.DecimalPlaces = 1
            AddHandler Me.m_PathAngleSpin.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' path rotation angle editor
			Me.m_TextureAngleSpin = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_TextureAngleSpin.Minimum = 0
            Me.m_TextureAngleSpin.Maximum = 360
            Me.m_TextureAngleSpin.Value = Me.m_MyTextureMapping.TextureAngle
            Me.m_TextureAngleSpin.[Step] = 1
            Me.m_TextureAngleSpin.DecimalPlaces = 1
            AddHandler Me.m_TextureAngleSpin.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' X position editor
			Me.m_XPositionSpin = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_XPositionSpin.Minimum = 0
            Me.m_XPositionSpin.Maximum = 800
            Me.m_XPositionSpin.Value = Me.m_PathPositionX
            Me.m_XPositionSpin.[Step] = 1
            Me.m_XPositionSpin.DecimalPlaces = 1
            AddHandler Me.m_XPositionSpin.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' Y position editor
			Me.m_YPositionSpin = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_YPositionSpin.Minimum = 0
            Me.m_YPositionSpin.Maximum = 600
            Me.m_YPositionSpin.Value = Me.m_PathPositionY
            Me.m_YPositionSpin.[Step] = 1
            Me.m_YPositionSpin.DecimalPlaces = 1
            AddHandler Me.m_YPositionSpin.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Path Angle (degrees):", Me.m_PathAngleSpin))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Texture Angle (degrees):", Me.m_TextureAngleSpin))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("X Position:", Me.m_XPositionSpin))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Y Position:", Me.m_YPositionSpin))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	In cases when the built-in texture mappings do not fit your requirements you can implement and use your own texture mapping types.
	The custom texture mapping presented in this example demonstrates how the texture can be rotated independently of the textured shape.
	Use the controls to the right to set the rotation angles of the shape and the texture.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return

			' Create a transform matrix for the graphics path
			Dim matrix As Nevron.Nov.Graphics.NMatrix = Nevron.Nov.Graphics.NMatrix.CreateRotationMatrix(Me.m_PathAngle * Nevron.Nov.NAngle.Degree2Rad, Nevron.Nov.Graphics.NPoint.Zero)
            matrix.Translate(Me.m_PathPositionX, Me.m_PathPositionY)

			' Create a graphics path containing a rectangle and transform it
			Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddRectangle(0, 0, Nevron.Nov.Examples.Framework.NCustomTextureMappingExample.RectWidth, Nevron.Nov.Examples.Framework.NCustomTextureMappingExample.RectHeight)
            path.Transform(matrix)

			' Paint the graphics path
			Dim pv As Nevron.Nov.Dom.NPaintVisitor = args.PaintVisitor
            pv.SetStroke(Me.m_Stroke)
            pv.SetFill(Me.m_ImageFill)
            pv.PaintPath(path)

			' Paint a border around the canvas
			pv.ClearFill()
            pv.SetStroke(Nevron.Nov.Graphics.NColor.Black, 1)
            pv.PaintRectangle(0, 0, canvas.Width, canvas.Height)
        End Sub

        Private Sub OnNumericUpDownValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PathAngle = Me.m_PathAngleSpin.Value
            Me.m_PathPositionX = Me.m_XPositionSpin.Value
            Me.m_PathPositionY = Me.m_YPositionSpin.Value
            Me.m_MyTextureMapping.PinPoint = New Nevron.Nov.Graphics.NPoint(Me.m_PathPositionX, Me.m_PathPositionY)
            Me.m_MyTextureMapping.TextureAngle = Me.m_TextureAngleSpin.Value
            Me.m_Canvas.InvalidateDisplay()
        End Sub

		#EndRegion

		#Region"Constants"

		Friend Const RectWidth As Double = 300
        Friend Const RectHeight As Double = 240

		#EndRegion

		#Region"Fields"

		Private m_Canvas As Nevron.Nov.UI.NCanvas
        Private m_Stroke As Nevron.Nov.Graphics.NStroke
        Private m_ImageFill As Nevron.Nov.Graphics.NImageFill
        Private m_MyTextureMapping As Nevron.Nov.Examples.Framework.MyTextureMapping
        Private m_TextureAngleSpin As Nevron.Nov.UI.NNumericUpDown
        Private m_PathAngleSpin As Nevron.Nov.UI.NNumericUpDown
        Private m_XPositionSpin As Nevron.Nov.UI.NNumericUpDown
        Private m_YPositionSpin As Nevron.Nov.UI.NNumericUpDown
        Private m_PathAngle As Double
        Private m_PathPositionX As Double
        Private m_PathPositionY As Double

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NCustomTextureMappingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class

    Public Class MyTextureMapping
        Inherits Nevron.Nov.Graphics.NCustomTextureMapping
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' Static constructor
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Framework.MyTextureMapping.MyTextureMappingSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.MyTextureMapping), Nevron.Nov.Graphics.NCustomTextureMapping.NCustomTextureMappingSchema)
            Nevron.Nov.Examples.Framework.MyTextureMapping.TextureAngleProperty = Nevron.Nov.Examples.Framework.MyTextureMapping.MyTextureMappingSchema.AddSlot("TextureAngle", Nevron.Nov.Dom.NDomType.[Double], 0.0R)
            Nevron.Nov.Examples.Framework.MyTextureMapping.PinPointProperty = Nevron.Nov.Examples.Framework.MyTextureMapping.MyTextureMappingSchema.AddSlot("PinPoint", Nevron.Nov.Dom.NDomType.NPoint, Nevron.Nov.Graphics.NPoint.Zero)
        End Sub

		#EndRegion

		#Region"Properties"

		''' <summary>
		''' The texture angle (in degrees)
		''' </summary>
		Public Property TextureAngle As Double
            Get
                Return CDbl(MyBase.GetValue(Nevron.Nov.Examples.Framework.MyTextureMapping.TextureAngleProperty))
            End Get
            Set(ByVal value As Double)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.MyTextureMapping.TextureAngleProperty, value)
            End Set
        End Property
		''' <summary>
		''' The point around which the texture is rotated
		''' </summary>
		Public Property PinPoint As Nevron.Nov.Graphics.NPoint
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.MyTextureMapping.PinPointProperty), Nevron.Nov.Graphics.NPoint)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NPoint)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.MyTextureMapping.PinPointProperty, value)
            End Set
        End Property

		#EndRegion

		#Region"Protected Overrides from NCustomTextureMapping"

        Public Overrides Sub GetTextureMappingInfo(<Out> ByRef tileMode As Nevron.Nov.Graphics.ENTileMode, <Out> ByRef textureCalibrator As Nevron.Nov.Graphics.NTextureCalibrator)
            tileMode = Nevron.Nov.Graphics.ENTileMode.Tile
            textureCalibrator = New Nevron.Nov.Examples.Framework.MyTextureMapping.MyTextureCalibrator(Me)
        End Sub

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with MyTextureMapping
		''' </summary>
		Public Shared ReadOnly MyTextureMappingSchema As Nevron.Nov.Dom.NSchema
		''' <summary>
		''' Reference to the TextureAngle property
		''' </summary>
		Public Shared ReadOnly TextureAngleProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the PinPoint property
		''' </summary>
		Public Shared ReadOnly PinPointProperty As Nevron.Nov.Dom.NProperty

		#EndRegion

        Friend Class MyTextureCalibrator
            Inherits Nevron.Nov.Graphics.NTextureCalibrator

            Public Sub New(ByVal textureMapping As Nevron.Nov.Examples.Framework.MyTextureMapping)
                Me.m_TextureMapping = textureMapping
            End Sub

            Public Overrides Function Calibrate(ByVal visitor As Nevron.Nov.Dom.NPaintVisitor, ByVal imgWidth As Double, ByVal imgHeight As Double, ByVal targetRect As Nevron.Nov.Graphics.NRectangle) As Nevron.Nov.Graphics.NMatrix
                ' Initialize the image transform
                Dim matrix As Nevron.Nov.Graphics.NMatrix = Nevron.Nov.Graphics.NMatrix.Identity

                ' Scale the image so that it fits 2 times in width and 3 times in height
                matrix.Scale(Nevron.Nov.Examples.Framework.NCustomTextureMappingExample.RectWidth / (2.0 * imgWidth), Nevron.Nov.Examples.Framework.NCustomTextureMappingExample.RectHeight / (3.0 * imgHeight))

                ' Rotate the image to the specified angle
                matrix.Rotate(Me.m_TextureMapping.TextureAngle * Nevron.Nov.NAngle.Degree2Rad)

                ' Translate the image to the specfied pin point
                matrix.Translate(Me.m_TextureMapping.PinPoint.X, Me.m_TextureMapping.PinPoint.Y)
                Return matrix
            End Function

            Public Overrides Function DeepClone() As Object
                Return New Nevron.Nov.Examples.Framework.MyTextureMapping.MyTextureCalibrator(Me.m_TextureMapping)
            End Function

            Private m_TextureMapping As Nevron.Nov.Examples.Framework.MyTextureMapping
        End Class
    End Class
End Namespace
