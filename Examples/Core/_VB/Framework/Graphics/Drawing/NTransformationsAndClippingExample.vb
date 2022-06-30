Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NTransformationsAndClippingExample
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
            Nevron.Nov.Examples.Framework.NTransformationsAndClippingExample.NTransformationsAndClippingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NTransformationsAndClippingExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_PositionX = 100
            Me.m_PositionY = 220
            Me.m_Angle1 = -50
            Me.m_Angle2 = 40
            Me.m_Angle3 = 90
            Me.m_ClipRect = New Nevron.Nov.Graphics.NRectangle(20, 20, 500, 360)
            Me.m_Canvas = New Nevron.Nov.UI.NCanvas()
            Me.m_Canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(600, 400)
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
            Me.m_PositionXUpDown = Me.CreateNumericUpDown(40, 400, Me.m_PositionX)
            Me.m_PositionYUpDown = Me.CreateNumericUpDown(100, 300, Me.m_PositionY)
            Me.m_Angle1UpDown = Me.CreateNumericUpDown(-90, -10, Me.m_Angle1)
            Me.m_Angle2UpDown = Me.CreateNumericUpDown(-100, 100, Me.m_Angle2)
            Me.m_Angle3UpDown = Me.CreateNumericUpDown(-100, 100, Me.m_Angle3)
            Dim roboArmControlsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            roboArmControlsStack.Add(Nevron.Nov.UI.NPairBox.Create("X:", Me.m_PositionXUpDown))
            roboArmControlsStack.Add(Nevron.Nov.UI.NPairBox.Create("Y:", Me.m_PositionYUpDown))
            roboArmControlsStack.Add(Nevron.Nov.UI.NPairBox.Create("Angle 1:", Me.m_Angle1UpDown))
            roboArmControlsStack.Add(Nevron.Nov.UI.NPairBox.Create("Angle 2:", Me.m_Angle2UpDown))
            roboArmControlsStack.Add(Nevron.Nov.UI.NPairBox.Create("Angle 3:", Me.m_Angle3UpDown))
            Dim roboArmGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Robo Arm")
            roboArmGroupBox.Content = roboArmControlsStack
            Me.m_ClipRectXUpDown = Me.CreateNumericUpDown(0, 600, Me.m_ClipRect.X)
            Me.m_ClipRectYUpDown = Me.CreateNumericUpDown(0, 400, Me.m_ClipRect.Y)
            Me.m_ClipRectWUpDown = Me.CreateNumericUpDown(0, 600, Me.m_ClipRect.Width)
            Me.m_ClipRectHUpDown = Me.CreateNumericUpDown(0, 400, Me.m_ClipRect.Height)
            Dim clipRectControlsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            clipRectControlsStack.Add(Nevron.Nov.UI.NPairBox.Create("X:", Me.m_ClipRectXUpDown))
            clipRectControlsStack.Add(Nevron.Nov.UI.NPairBox.Create("Y:", Me.m_ClipRectYUpDown))
            clipRectControlsStack.Add(Nevron.Nov.UI.NPairBox.Create("Width:", Me.m_ClipRectWUpDown))
            clipRectControlsStack.Add(Nevron.Nov.UI.NPairBox.Create("Height:", Me.m_ClipRectHUpDown))
            Dim clipRectGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Clip Rect")
            clipRectGroupBox.Content = clipRectControlsStack

			' create a stack and put the controls in it
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(roboArmGroupBox)
            stack.Add(clipRectGroupBox)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates geometric transforms and clipping with the NOV graphics.
</p>
"
        End Function


		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return
            canvas.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            canvas.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Dim pv As Nevron.Nov.Dom.NPaintVisitor = args.PaintVisitor
            pv.ClearStyles()
            pv.SetStroke(Nevron.Nov.Graphics.NColor.MidnightBlue, 1)
            pv.SetFill(Nevron.Nov.Graphics.NColor.LightSteelBlue)
            Dim m1 As Nevron.Nov.Graphics.NMatrix = Nevron.Nov.Graphics.NMatrix.Identity
            m1.Rotate(Nevron.Nov.NAngle.Degree2Rad * Me.m_Angle1)
            Dim m2 As Nevron.Nov.Graphics.NMatrix = Nevron.Nov.Graphics.NMatrix.Identity
            m2.Rotate(Nevron.Nov.NAngle.Degree2Rad * Me.m_Angle2)
            m2.Translate(100, 0)
            Dim m3 As Nevron.Nov.Graphics.NMatrix = Nevron.Nov.Graphics.NMatrix.Identity
            m3.Rotate(Nevron.Nov.NAngle.Degree2Rad * Me.m_Angle3)
            m3.Translate(100, 0)
            Dim clipRegion As Nevron.Nov.Graphics.NRegion = Nevron.Nov.Graphics.NRegion.FromRectangle(Me.m_ClipRect)
            pv.PushClip(clipRegion)
            pv.PushTransform(New Nevron.Nov.Graphics.NMatrix(Me.m_PositionX, 0))
            Me.PaintVerticalBar(pv)
            pv.PushTransform(New Nevron.Nov.Graphics.NMatrix(0, Me.m_PositionY))
            Me.PaintBase(pv)
            pv.PushTransform(m1)
            Me.PaintLink(pv, 20)
            Me.PaintJoint(pv, 20)
            pv.PushSnapToPixels(False)
            pv.PushTransform(m2)
            Me.PaintLink(pv, 16)
            Me.PaintJoint(pv, 16)
            pv.PushTransform(m3)
            Me.PaintGripper(pv)
            Me.PaintJoint(pv, 12)
            pv.PopTransform()' m3
            pv.PopTransform()' m2
            pv.PopTransform()' m1
            pv.PopTransform()' mTY
            pv.PopTransform()' mTX
            pv.PopSnapToPixels()
            pv.PopClip()

			' paint a border around the clip rectangle
			pv.ClearFill()
            pv.SetStroke(Nevron.Nov.Graphics.NColor.Red, 1)
            pv.PaintRectangle(Me.m_ClipRect)

			' paint a border around the canvas
			pv.SetStroke(Nevron.Nov.Graphics.NColor.Black, 1)
            pv.PaintRectangle(0, 0, canvas.Width, canvas.Height)
        End Sub

        Private Sub OnNumericUpDownValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_Canvas Is Nothing Then Return
            Me.m_PositionX = Me.m_PositionXUpDown.Value
            Me.m_PositionY = Me.m_PositionYUpDown.Value
            Me.m_Angle1 = Me.m_Angle1UpDown.Value
            Me.m_Angle2 = Me.m_Angle2UpDown.Value
            Me.m_Angle3 = Me.m_Angle3UpDown.Value
            Me.m_ClipRect.X = Me.m_ClipRectXUpDown.Value
            Me.m_ClipRect.Y = Me.m_ClipRectYUpDown.Value
            Me.m_ClipRect.Width = Me.m_ClipRectWUpDown.Value
            Me.m_ClipRect.Height = Me.m_ClipRectHUpDown.Value
            Me.m_Canvas.InvalidateDisplay()
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub PaintJoint(ByVal pv As Nevron.Nov.Dom.NPaintVisitor, ByVal radius As Double)
            Dim innerR As Double = radius - 3
            pv.PaintEllipse(-radius, -radius, 2 * radius, 2 * radius)
            pv.PaintEllipse(-innerR, -innerR, 2 * innerR, 2 * innerR)
        End Sub

        Private Sub PaintLink(ByVal pv As Nevron.Nov.Dom.NPaintVisitor, ByVal radius As Double)
            Dim r As Double = radius - 8
            pv.PaintRectangle(0, -r, 100, 2 * r)
        End Sub

        Private Sub PaintGripper(ByVal pv As Nevron.Nov.Dom.NPaintVisitor)
            If Me.m_ArmGripperPath Is Nothing Then
                Me.m_ArmGripperPath = New Nevron.Nov.Graphics.NGraphicsPath()
                Me.m_ArmGripperPath.StartFigure(0, -6)
                Me.m_ArmGripperPath.LineTo(20, -6)
                Me.m_ArmGripperPath.LineTo(20, -14)
                Me.m_ArmGripperPath.LineTo(30, -14)
                Me.m_ArmGripperPath.LineTo(30, 14)
                Me.m_ArmGripperPath.LineTo(20, 14)
                Me.m_ArmGripperPath.LineTo(20, 6)
                Me.m_ArmGripperPath.LineTo(0, 6)
                Me.m_ArmGripperPath.CloseFigure()
                Me.m_ArmGripperPath.StartFigure(30, -14)
                Me.m_ArmGripperPath.LineTo(40, -14)
                Me.m_ArmGripperPath.LineTo(50, -10)
                Me.m_ArmGripperPath.LineTo(50, -7)
                Me.m_ArmGripperPath.LineTo(30, -7)
                Me.m_ArmGripperPath.CloseFigure()
                Me.m_ArmGripperPath.StartFigure(30, 14)
                Me.m_ArmGripperPath.LineTo(40, 14)
                Me.m_ArmGripperPath.LineTo(50, 10)
                Me.m_ArmGripperPath.LineTo(50, 7)
                Me.m_ArmGripperPath.LineTo(30, 7)
                Me.m_ArmGripperPath.CloseFigure()
            End If

            pv.PaintPath(Me.m_ArmGripperPath)
        End Sub

        Private Sub PaintBase(ByVal pv As Nevron.Nov.Dom.NPaintVisitor)
            If Me.m_ArmBasePath Is Nothing Then
                Me.m_ArmBasePath = New Nevron.Nov.Graphics.NGraphicsPath()
                Me.m_ArmBasePath.StartFigure(0, 0)
                Me.m_ArmBasePath.LineTo(-40, 0)
                Me.m_ArmBasePath.LineTo(-40, 50)
                Me.m_ArmBasePath.LineTo(25, 50)
                Me.m_ArmBasePath.LineTo(25, 20)
                Me.m_ArmBasePath.CloseFigure()
            End If

            pv.PaintPath(Me.m_ArmBasePath)
        End Sub

        Private Sub PaintVerticalBar(ByVal pv As Nevron.Nov.Dom.NPaintVisitor)
            pv.PaintRectangle(-35, 0, 8, 400)
        End Sub

        Private Function CreateNumericUpDown(ByVal min As Double, ByVal max As Double, ByVal value As Double) As Nevron.Nov.UI.NNumericUpDown
            Dim control As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            control.Minimum = min
            control.Maximum = max
            control.Value = value
            control.[Step] = 1
            control.DecimalPlaces = 0
            AddHandler control.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)
            Return control
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Canvas As Nevron.Nov.UI.NCanvas
        Private m_PositionXUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_PositionYUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_Angle1UpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_Angle2UpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_Angle3UpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ClipRectXUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ClipRectYUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ClipRectWUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ClipRectHUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ArmBasePath As Nevron.Nov.Graphics.NGraphicsPath
        Private m_ArmGripperPath As Nevron.Nov.Graphics.NGraphicsPath
        Private m_PositionX As Double
        Private m_PositionY As Double
        Private m_Angle1 As Double
        Private m_Angle2 As Double
        Private m_Angle3 As Double
        Private m_ClipRect As Nevron.Nov.Graphics.NRectangle

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTransformationsAndClippingExample.
		''' </summary>
		Public Shared ReadOnly NTransformationsAndClippingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
