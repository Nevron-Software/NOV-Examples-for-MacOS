Imports System
Imports System.IO
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Globalization
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.IO
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NMouseEventsExample
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
            Nevron.Nov.Examples.UI.NMouseEventsExample.NMouseEventsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NMouseEventsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Canvas = New Nevron.Nov.UI.NCanvas()
            AddHandler Me.m_Canvas.PrePaint, AddressOf Me.OnCanvasPrePaint

            ' subscribe for mouse events
            AddHandler Me.m_Canvas.MouseDown, AddressOf Me.OnCanvasMouseDown
            AddHandler Me.m_Canvas.MouseUp, AddressOf Me.OnCanvasMouseUp
            AddHandler Me.m_Canvas.MouseMove, AddressOf Me.OnCanvasMouseMove
            AddHandler Me.m_Canvas.MouseWheel, AddressOf Me.OnCanvasMouseWheel
            AddHandler Me.m_Canvas.MouseIn, AddressOf Me.OnCanvasMouseIn
            AddHandler Me.m_Canvas.MouseOut, AddressOf Me.OnCanvasMouseOut
            AddHandler Me.m_Canvas.GotMouseCapture, AddressOf Me.OnCanvasGotMouseCapture
            AddHandler Me.m_Canvas.LostMouseCapture, AddressOf Me.OnCanvasLostMouseCapture
            Return Me.m_Canvas
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last

            ' track move events
            Me.m_LogMoveEventsCheck = New Nevron.Nov.UI.NCheckBox("Track Move Events")
            stack.Add(Me.m_LogMoveEventsCheck)

            ' capture mouse on left mouse down
            Me.m_CaptureMouseOnLeftMouseDown = New Nevron.Nov.UI.NCheckBox("Capture Mouse on Left MouseDown")
            stack.Add(Me.m_CaptureMouseOnLeftMouseDown)

            ' capture mouse on right mouse down
            Me.m_CaptureMouseOnRightMouseDown = New Nevron.Nov.UI.NCheckBox("Capture Mouse on Right MouseDown")
            stack.Add(Me.m_CaptureMouseOnRightMouseDown)

            ' Create the events log
            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	Demonstrates the mouse support in NOV. Click and move the mouse over the canvas to explore the mouse events that NOV sends to the application.
    Mouse support is available to most desktop enviroments.
</p>
"
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnCanvasPrePaint(ByVal arg As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim visitor As Nevron.Nov.Dom.NPaintVisitor = arg.PaintVisitor

            ' paint background
            visitor.SetFill(Nevron.Nov.Graphics.NColor.Ivory)
            visitor.PaintRectangle(Me.m_Canvas.GetContentEdge())

            ' paint mouse
            If Nevron.Nov.UI.NMouse.IsOver(Me.m_Canvas) Then
                ' define some metrics first
                Dim buttonWidth As Double = 10
                Dim buttonHeight As Double = 15
                Dim buttonsOffset As Double = 5
                Dim pointerSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(5, 5)
                Dim buttonsCenter As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(Me.m_MouseLocation.X, Me.m_MouseLocation.Y + buttonsOffset + buttonHeight / 2)
                Dim buttonsFrame As Nevron.Nov.Graphics.NRectangle = Nevron.Nov.Graphics.NRectangle.FromCenterAndSize(buttonsCenter, buttonWidth * 3, buttonHeight)

                ' paint left button, if down
                If Me.m_LeftMouseDown Then
                    Dim buttonRect As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(buttonsFrame.Left, buttonsFrame.Top, buttonWidth, buttonHeight)
                    visitor.SetFill(Nevron.Nov.Graphics.NColor.Red)
                    visitor.PaintRectangle(buttonRect)
                End If

                ' paint middle button, if down
                If Me.m_MiddleMouseDown Then
                    Dim buttonRect As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(buttonsFrame.Left + buttonWidth, buttonsFrame.Top, buttonWidth, buttonHeight)
                    visitor.SetFill(Nevron.Nov.Graphics.NColor.Green)
                    visitor.PaintRectangle(buttonRect)
                End If

                ' paint right button, if down
                If Me.m_RightMouseDown Then
                    Dim buttonRect As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(buttonsFrame.Right - buttonWidth, buttonsFrame.Top, buttonWidth, buttonHeight)
                    visitor.SetFill(Nevron.Nov.Graphics.NColor.Blue)
                    visitor.PaintRectangle(buttonRect)
                End If

                ' paint mouse pointer and buttons frame
                visitor.ClearStyles()
                visitor.SetStroke(New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Black))
                visitor.PaintEllipse(Nevron.Nov.Graphics.NRectangle.FromCenterAndSize(Me.m_MouseLocation, pointerSize))
                visitor.PaintRectangle(buttonsFrame)
                Dim leftSeparator As Double = buttonsCenter.X - buttonWidth / 2
                visitor.PaintLine(leftSeparator, buttonsFrame.Top, leftSeparator, buttonsFrame.Bottom)
                Dim rightSeparator As Double = buttonsCenter.X + buttonWidth / 2
                visitor.PaintLine(rightSeparator, buttonsFrame.Top, rightSeparator, buttonsFrame.Bottom)
            End If

            ' paint capture frame
            If Nevron.Nov.UI.NMouse.IsCaptured(Me.m_Canvas) Then
                Dim outerRect As Nevron.Nov.Graphics.NRectangle = Me.m_Canvas.GetContentEdge()
                Dim innerRect As Nevron.Nov.Graphics.NRectangle = outerRect
                innerRect.Deflate(3)
                Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
                path.AddRectangle(outerRect)
                path.AddRectangle(innerRect)
                visitor.ClearStyles()
                visitor.SetFill(Nevron.Nov.Graphics.NColor.Red)
                visitor.PaintPath(path)
            End If
        End Sub

        Private Sub OnCanvasMouseMove(ByVal arg As Nevron.Nov.UI.NMouseEventArgs)
            ' log event
            If Me.m_LogMoveEventsCheck.Checked Then
                Me.m_EventsLog.LogEvent(System.[String].Format("Mouse Move. Position X: {0}, Y; {1}", Me.m_MouseLocation.X, Me.m_MouseLocation.Y))
            End If

            ' update mouse location
            Me.m_MouseLocation = arg.CurrentTargetPosition
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnCanvasMouseUp(ByVal arg As Nevron.Nov.UI.NMouseButtonEventArgs)
            Me.m_EventsLog.LogEvent("Mouse Up. Button: " & arg.Button)

            Select Case arg.Button
                Case Nevron.Nov.UI.ENMouseButtons.Left
                    Me.m_LeftMouseDown = False
                Case Nevron.Nov.UI.ENMouseButtons.Right
                    Me.m_RightMouseDown = False
                Case Nevron.Nov.UI.ENMouseButtons.Middle
                    Me.m_MiddleMouseDown = False
                Case Else
            End Select

            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnCanvasMouseDown(ByVal arg As Nevron.Nov.UI.NMouseButtonEventArgs)
            Me.m_EventsLog.LogEvent("Mouse Down. Button: " & arg.Button)

            Select Case arg.Button
                Case Nevron.Nov.UI.ENMouseButtons.Left
                    Me.m_LeftMouseDown = True

                    If Me.m_CaptureMouseOnLeftMouseDown.Checked Then
                        Me.m_Canvas.CaptureMouse()
                    End If

                Case Nevron.Nov.UI.ENMouseButtons.Right
                    Me.m_RightMouseDown = True

                    If Me.m_CaptureMouseOnRightMouseDown.Checked Then
                        Me.m_Canvas.CaptureMouse()
                    End If

                Case Nevron.Nov.UI.ENMouseButtons.Middle
                    Me.m_MiddleMouseDown = True
                Case Else
            End Select

            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnCanvasMouseWheel(ByVal arg As Nevron.Nov.UI.NMouseWheelEventArgs)
            Me.m_EventsLog.LogEvent("Mouse Wheel. Delta: " & arg.Delta)
        End Sub

        Private Sub OnCanvasMouseOut(ByVal arg As Nevron.Nov.UI.NMouseOverChangeEventArgs)
            Me.m_EventsLog.LogEvent("Mouse Out")
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnCanvasMouseIn(ByVal arg As Nevron.Nov.UI.NMouseOverChangeEventArgs)
            Me.m_EventsLog.LogEvent("Mouse In")
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnCanvasGotMouseCapture(ByVal arg As Nevron.Nov.UI.NMouseCaptureChangeEventArgs)
            Me.m_EventsLog.LogEvent("Got Mouse Capture")
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnCanvasLostMouseCapture(ByVal arg As Nevron.Nov.UI.NMouseCaptureChangeEventArgs)
            Me.m_EventsLog.LogEvent("Lost Mouse Capture")
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_LogMoveEventsCheck As Nevron.Nov.UI.NCheckBox
        Private m_CaptureMouseOnLeftMouseDown As Nevron.Nov.UI.NCheckBox
        Private m_CaptureMouseOnRightMouseDown As Nevron.Nov.UI.NCheckBox
        Private m_Canvas As Nevron.Nov.UI.NCanvas
        Private m_EventsLog As NExampleEventsLog
        Private m_MouseLocation As Nevron.Nov.Graphics.NPoint
        Private m_LeftMouseDown As Boolean
        Private m_MiddleMouseDown As Boolean
        Private m_RightMouseDown As Boolean

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NMouseEventsExample.
        ''' </summary>
        Public Shared ReadOnly NMouseEventsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Constants"

        Private Const EnglishLanguageName As String = "English (US)"
        Private Const BulgarianLanguageName As String = "Bulgarian"
        Private Const GermanLanguageName As String = "German"

        #EndRegion
    End Class
End Namespace
