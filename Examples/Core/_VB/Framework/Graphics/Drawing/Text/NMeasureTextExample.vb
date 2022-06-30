Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports System
Imports System.Text

Namespace Nevron.Nov.Examples.Framework
	''' <summary>
	''' The example demonstrates how to paint text at location
	''' </summary>
	Public Class NMeasureTextExample
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
            Nevron.Nov.Examples.Framework.NMeasureTextExample.NMeasureTextExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NMeasureTextExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a table panel to hold the canvases and the labels
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Me.m_Canvas = New Nevron.Nov.UI.NCanvas()
            stack.Add(Me.m_Canvas)
            AddHandler Me.m_Canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
            Me.m_Canvas.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Me.m_TextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_TextBox.Multiline = True
            Me.m_TextBox.AcceptsEnter = True
            Me.m_TextBox.MinHeight = 200
            Me.m_TextBox.Text = "Type some text to measure"
            AddHandler Me.m_TextBox.TextChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnTextBoxTextChanged)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NLabel("Text:"))
            stack.Add(Me.m_TextBox)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
The example demonstrates how to measure text. Type some text in the text box on the right. The blue rectangle shows the measured bounds. 
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' 
		''' </summary>
		''' <paramname="args"></param>
		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return
            Dim paintVisitor As Nevron.Nov.Dom.NPaintVisitor = args.PaintVisitor
            Dim contentEge As Nevron.Nov.Graphics.NRectangle = canvas.GetContentEdge()

			' create the settings
			Dim settings As Nevron.Nov.Graphics.NPaintTextRectSettings = New Nevron.Nov.Graphics.NPaintTextRectSettings()
            settings.SingleLine = False
            settings.WrapMode = Nevron.Nov.Graphics.ENTextWrapMode.WordWrap
            settings.HorzAlign = Nevron.Nov.Graphics.ENTextHorzAlign.Left
            settings.VertAlign = Nevron.Nov.Graphics.ENTextVertAlign.Top

			' create the text
			Dim text As String = Me.m_TextBox.Text

			' calculate the text bounds the text bounds
			Dim resolution As Double = canvas.GetResolution()
            Dim font As Nevron.Nov.Graphics.NFont = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 10, Nevron.Nov.Graphics.ENFontStyle.Regular)
            Dim textSize As Nevron.Nov.Graphics.NSize = font.MeasureString(text.ToCharArray(), resolution, contentEge.Width, settings)
            Dim center As Nevron.Nov.Graphics.NPoint = contentEge.Center
            Dim textBounds As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(center.X - textSize.Width / 2.0, center.Y - textSize.Height / 2.0, textSize.Width, textSize.Height)

			' paint the bounding box
			paintVisitor.ClearStyles()
            paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.LightBlue)
            paintVisitor.PaintRectangle(textBounds)

			' init font and fill
			paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.Black)
            paintVisitor.SetFont(font)

			' paint the text
			paintVisitor.PaintString(textBounds, text.ToCharArray(), settings)
        End Sub

        Private Sub OnTextBoxTextChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Canvas As Nevron.Nov.UI.NCanvas
        Private m_TextBox As Nevron.Nov.UI.NTextBox

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NMeasureTextExample.
		''' </summary>
		Public Shared ReadOnly NMeasureTextExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
