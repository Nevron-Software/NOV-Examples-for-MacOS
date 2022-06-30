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
    Public Class NTouchSupportExample
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
            Nevron.Nov.Examples.UI.NTouchSupportExample.NTouchSupportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTouchSupportExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Canvas = New Nevron.Nov.UI.NCanvas()
            AddHandler Me.m_Canvas.PrePaint, AddressOf Me.OnCanvasPrePaint

            ' subscribe for touch events
            AddHandler Me.m_Canvas.TouchDown, AddressOf Me.OnCanvasTouchDown
            AddHandler Me.m_Canvas.TouchMove, AddressOf Me.OnCanvasTouchMove
            AddHandler Me.m_Canvas.TouchUp, AddressOf Me.OnCanvasTouchUp

            ' subscribe for mouse events
            AddHandler Me.m_Canvas.MouseDown, AddressOf Me.OnCanvasMouseDown
            AddHandler Me.m_Canvas.MouseUp, AddressOf Me.OnCanvasMouseUp
            AddHandler Me.m_Canvas.MouseMove, AddressOf Me.OnCanvasMouseMove
            Return Me.m_Canvas
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last

            ' capture touch check
            Me.m_CaptureTouchCheck = New Nevron.Nov.UI.NCheckBox("Capture Touch")
            stack.Add(Me.m_CaptureTouchCheck)

            ' handle touch events check
            Me.m_HandleTouchEventsCheck = New Nevron.Nov.UI.NCheckBox("Handle Touch Events")
            stack.Add(Me.m_HandleTouchEventsCheck)

            ' track move events
            Me.m_LogMoveEventsCheck = New Nevron.Nov.UI.NCheckBox("Track Move Events")
            stack.Add(Me.m_LogMoveEventsCheck)

            ' Create clear canvas button
            Dim clearCanvasButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Clear Canvas")
            AddHandler clearCanvasButton.Click, AddressOf Me.clearCanvas_Click
            stack.Add(clearCanvasButton)

            ' Create the events log
            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Private Sub clearCanvas_Click(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_TouchPoints.Clear()
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	Demonstrates the core touch support in NOV. Use your fingers to draw on the canvas and explore the touch events that NOV sends to the application.
    Note that touch input is only available to touch enabled enviroments.
</p>
"
        End Function

        #EndRegion

        #Region"Canvas Event Handlers"

        Private Sub OnCanvasPrePaint(ByVal arg As Nevron.Nov.UI.NCanvasPaintEventArgs)
            For i As Integer = 0 To Me.m_TouchPoints.Count - 1
                Me.m_TouchPoints(CInt((i))).Paint(arg.PaintVisitor)
            Next
        End Sub

        Private Sub OnCanvasTouchUp(ByVal arg As Nevron.Nov.UI.NTouchActionEventArgs)
            Me.AddTouchPoint(arg)
            Me.m_EventsLog.LogEvent("Touch Up")

            If Me.m_HandleTouchEventsCheck.Checked Then
                arg.Cancel = True
            End If
        End Sub

        Private Sub OnCanvasTouchMove(ByVal arg As Nevron.Nov.UI.NTouchActionEventArgs)
            Me.AddTouchPoint(arg)

            If Me.m_LogMoveEventsCheck.Checked Then
                Me.m_EventsLog.LogEvent("Touch Move")
            End If

            If Me.m_HandleTouchEventsCheck.Checked Then
                arg.Cancel = True
            End If
        End Sub

        Private Sub OnCanvasTouchDown(ByVal arg As Nevron.Nov.UI.NTouchActionEventArgs)
            Me.AddTouchPoint(arg)
            Me.m_EventsLog.LogEvent("Touch Down")

            If Me.m_CaptureTouchCheck.Checked Then
                Me.m_Canvas.CaptureTouch(arg.Device)
                Me.m_EventsLog.LogEvent("Captured")
            End If

            If Me.m_HandleTouchEventsCheck.Checked Then
                arg.Cancel = True
            End If
        End Sub

        Private Sub OnCanvasMouseMove(ByVal arg As Nevron.Nov.UI.NMouseEventArgs)
            If Me.m_LogMoveEventsCheck.Checked Then
                Me.m_EventsLog.LogEvent("Mouse Move")
            End If
        End Sub

        Private Sub OnCanvasMouseUp(ByVal arg As Nevron.Nov.UI.NMouseButtonEventArgs)
            Me.m_EventsLog.LogEvent("Mouse Up")
        End Sub

        Private Sub OnCanvasMouseDown(ByVal arg As Nevron.Nov.UI.NMouseButtonEventArgs)
            Me.m_EventsLog.LogEvent("Mouse Down")
        End Sub

        Private Sub AddTouchPoint(ByVal arg As Nevron.Nov.UI.NTouchActionEventArgs)
            Me.m_TouchPoints.Add(New Nevron.Nov.Examples.UI.NTouchSupportExample.NTouchPoint(arg.TargetPosition, arg.ScreenSize, arg.DeviceState))
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_CaptureTouchCheck As Nevron.Nov.UI.NCheckBox
        Private m_HandleTouchEventsCheck As Nevron.Nov.UI.NCheckBox
        Private m_LogMoveEventsCheck As Nevron.Nov.UI.NCheckBox
        Private m_Canvas As Nevron.Nov.UI.NCanvas
        Private m_EventsLog As NExampleEventsLog
        Private m_TouchPoints As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NTouchSupportExample.NTouchPoint) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NTouchSupportExample.NTouchPoint)()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NTouchSupportExample.
        ''' </summary>
        Public Shared ReadOnly NTouchSupportExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Constants"

        Private Const EnglishLanguageName As String = "English (US)"
        Private Const BulgarianLanguageName As String = "Bulgarian"
        Private Const GermanLanguageName As String = "German"

        #EndRegion

        #Region"Nested Types - TouchPoint"

        Public Class NTouchPoint
            Public Sub New(ByVal point As Nevron.Nov.Graphics.NPoint, ByVal size As Nevron.Nov.Graphics.NSize, ByVal state As Nevron.Nov.UI.ENTouchDeviceState)
                Me.State = state
                Me.Location = point
                Me.Size = size
            End Sub

            Public Sub Paint(ByVal visitor As Nevron.Nov.Dom.NPaintVisitor)
                Dim color As Nevron.Nov.Graphics.NColor

                Select Case Me.State
                    Case Nevron.Nov.UI.ENTouchDeviceState.Down
                        color = Nevron.Nov.Graphics.NColor.Blue
                    Case Nevron.Nov.UI.ENTouchDeviceState.Unknown
                        color = Nevron.Nov.Graphics.NColor.Green
                    Case Nevron.Nov.UI.ENTouchDeviceState.Up
                        color = Nevron.Nov.Graphics.NColor.Red
                    Case Else
                        Throw New System.Exception("New ENTouchDeviceState?")
                End Select

                Dim size As Nevron.Nov.Graphics.NSize = Me.Size

                If size.Width = 0 OrElse size.Height = 0 Then
                    size = New Nevron.Nov.Graphics.NSize(5, 5)
                End If

                visitor.SetStroke(New Nevron.Nov.Graphics.NStroke(color))
                visitor.PaintEllipse(Nevron.Nov.Graphics.NRectangle.FromCenterAndSize(Me.Location, size))
            End Sub

            Private Location As Nevron.Nov.Graphics.NPoint
            Private Size As Nevron.Nov.Graphics.NSize
            Private State As Nevron.Nov.UI.ENTouchDeviceState
        End Class

        #EndRegion
    End Class
End Namespace
