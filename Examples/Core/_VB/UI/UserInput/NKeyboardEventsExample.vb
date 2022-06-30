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
    Public Class NKeyboardEventsExample
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
            Nevron.Nov.Examples.UI.NKeyboardEventsExample.NKeyboardEventsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NKeyboardEventsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Canvas = New Nevron.Nov.UI.NCanvas()
            AddHandler Me.m_Canvas.PrePaint, AddressOf Me.OnCanvasPrePaint

            ' subscribe for keyboard events
            AddHandler Me.m_Canvas.KeyDown, AddressOf Me.OnCanvasKeyDown
            AddHandler Me.m_Canvas.KeyUp, AddressOf Me.OnCanvasKeyUp
            AddHandler Me.m_Canvas.InputChar, AddressOf Me.OnCanvasInputChar
            AddHandler Me.m_Canvas.GotFocus, AddressOf Me.OnCanvasGotFocus
            AddHandler Me.m_Canvas.LostFocus, AddressOf Me.OnCanvasLostFocus

            ' subscribe for mouse events
            AddHandler Me.m_Canvas.MouseDown, AddressOf Me.OnCanvasMouseDown
            Return Me.m_Canvas
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last

            ' handle key up
            Me.m_HandleKeyDown = New Nevron.Nov.UI.NCheckBox("Handle Key Down")
            stack.Add(Me.m_HandleKeyDown)

            ' handle key up
            Me.m_HandleKeyUp = New Nevron.Nov.UI.NCheckBox("Handle Key Up")
            stack.Add(Me.m_HandleKeyUp)

            ' handle key up
            Me.m_HandleInputChar = New Nevron.Nov.UI.NCheckBox("Handle Input Char")
            stack.Add(Me.m_HandleInputChar)

            ' Create the events log
            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	Demonstrates the keyboard support in NOV. Click on the canvas and start pressing the keyboard keys to see the keyboard events that NOV sends to the application.
    Keyboard support is available to most desktop enviroments.
</p>
"
        End Function

        #EndRegion

        #Region"Canvas Event Handlers"

        Private Sub OnCanvasPrePaint(ByVal arg As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim visitor As Nevron.Nov.Dom.NPaintVisitor = arg.PaintVisitor
            Dim outerRect As Nevron.Nov.Graphics.NRectangle = Me.m_Canvas.GetContentEdge()
            Dim borderRect As Nevron.Nov.Graphics.NRectangle = outerRect
            borderRect.Deflate(3)
            Dim textRect As Nevron.Nov.Graphics.NRectangle = borderRect
            textRect.Deflate(3)

            ' paint background
            visitor.SetFill(Nevron.Nov.Graphics.NColor.Ivory)
            visitor.PaintRectangle(outerRect)

            ' paint string
            Dim font As Nevron.Nov.Graphics.NFont = CType(Nevron.Nov.NSystem.SafeDeepClone(Font), Nevron.Nov.Graphics.NFont)

            If font IsNot Nothing Then
                font.Size = 16
                visitor.SetFont(font)
                Dim settings As Nevron.Nov.Graphics.NPaintTextRectSettings = New Nevron.Nov.Graphics.NPaintTextRectSettings()
                visitor.SetFill(Nevron.Nov.Graphics.NColor.Black)
                visitor.PaintString(textRect, Me.m_sInputString, settings)
            End If

            ' paint focus frame
            If Nevron.Nov.UI.NKeyboard.IsFocused(Me.m_Canvas) Then
                Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
                path.AddRectangle(outerRect)
                path.AddRectangle(borderRect)
                visitor.ClearStyles()
                visitor.SetFill(Nevron.Nov.Graphics.NColor.Red)
                visitor.PaintPath(path)
            End If
        End Sub

        Private Sub OnCanvasKeyUp(ByVal arg As Nevron.Nov.UI.NKeyEventArgs)
            Me.m_EventsLog.LogEvent("Key Up: " & arg.Key.ToString())

            If Me.m_HandleKeyUp.Checked Then
                arg.Cancel = True
            End If
        End Sub

        Private Sub OnCanvasKeyDown(ByVal arg As Nevron.Nov.UI.NKeyEventArgs)
            Me.m_EventsLog.LogEvent("Key Down: " & arg.Key.ToString())

            If Me.m_HandleKeyDown.Checked Then
                arg.Cancel = True
            End If
        End Sub

        Private Sub OnCanvasInputChar(ByVal arg As Nevron.Nov.UI.NInputCharEventArgs)
            Me.m_EventsLog.LogEvent("Input Char: " & arg.[Char])
            Me.m_sInputString += arg.[Char]
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnCanvasGotFocus(ByVal arg As Nevron.Nov.UI.NFocusChangeEventArgs)
            Me.m_EventsLog.LogEvent("Got Focus")
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnCanvasLostFocus(ByVal arg As Nevron.Nov.UI.NFocusChangeEventArgs)
            Me.m_EventsLog.LogEvent("Lost Focus")
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnCanvasMouseDown(ByVal arg As Nevron.Nov.UI.NMouseButtonEventArgs)
            Me.m_Canvas.Focus()
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_HandleKeyDown As Nevron.Nov.UI.NCheckBox
        Private m_HandleKeyUp As Nevron.Nov.UI.NCheckBox
        Private m_HandleInputChar As Nevron.Nov.UI.NCheckBox
        Private m_Canvas As Nevron.Nov.UI.NCanvas
        Private m_EventsLog As NExampleEventsLog
        Private m_sInputString As String = ""

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NKeyboardEventsExample.
        ''' </summary>
        Public Shared ReadOnly NKeyboardEventsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Constants"

        Private Const EnglishLanguageName As String = "English (US)"
        Private Const BulgarianLanguageName As String = "Bulgarian"
        Private Const GermanLanguageName As String = "German"

        #EndRegion
    End Class
End Namespace
