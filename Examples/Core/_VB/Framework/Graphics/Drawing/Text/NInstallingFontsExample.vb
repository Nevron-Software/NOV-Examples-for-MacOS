Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports System
Imports System.Text
Imports Nevron.Nov.TrueType

Namespace Nevron.Nov.Examples.Framework
	''' <summary>
	''' The example demonstrates how to paint text at location
	''' </summary>
	Public Class NInstallingFontsExample
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
            Nevron.Nov.Examples.Framework.NInstallingFontsExample.NInstallingFontsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NInstallingFontsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim font As Nevron.Nov.TrueType.NOTResourceInstalledFont = NResources.Font_LiberationMonoBold_ttf
            Dim descriptor As Nevron.Nov.TrueType.NOTFontDescriptor = font.InstalledFonts(CInt((0))).Descriptor
            Me.m_FontDescriptor = New Nevron.Nov.Graphics.NFontDescriptor(descriptor.m_FamilyName, descriptor.m_FontVariant)
            Call Nevron.Nov.NApplication.FontService.InstalledFontsMap.InstallFont(font)

			' Create a table panel to hold the canvases and the labels
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
            stack.Add(canvas)
            AddHandler canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
            canvas.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
The example demonstrates how to install fonts.
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

			' create the text bounds
			Dim width As Double = contentEge.Width * 0.5
            Dim height As Double = contentEge.Height * 0.5
            Dim center As Nevron.Nov.Graphics.NPoint = contentEge.Center
            Dim textBounds As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(center.X - width / 2.0, center.Y - height / 2.0, width, height)

			' create the settings
			Dim settings As Nevron.Nov.Graphics.NPaintTextRectSettings = New Nevron.Nov.Graphics.NPaintTextRectSettings()
            settings.SingleLine = False
            settings.WrapMode = Nevron.Nov.Graphics.ENTextWrapMode.WordWrap
            settings.HorzAlign = Nevron.Nov.Graphics.ENTextHorzAlign.Center
            settings.VertAlign = Nevron.Nov.Graphics.ENTextVertAlign.Center

			' create the text
			Dim builder As System.Text.StringBuilder = New System.Text.StringBuilder()
            builder.AppendLine("This text is displayed using Liberation Fonts!")
            builder.AppendLine("distributed under the SIL Open Font License (OFL)")

			' paint the bounding box
			paintVisitor.ClearStyles()
            paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.LightBlue)
            paintVisitor.PaintRectangle(textBounds)

			' init font and fill
			paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.Black)
            Dim fontStyle As Nevron.Nov.Graphics.ENFontStyle = Nevron.Nov.Graphics.NFontFaceDescriptor.FontVariantToFontStyle(Me.m_FontDescriptor.FontVariant)
            paintVisitor.SetFont(New Nevron.Nov.Graphics.NFont(Me.m_FontDescriptor.FamilyName, 10, fontStyle))

			' paint the text
			paintVisitor.PaintString(textBounds, builder.ToString(), settings)
        End Sub

		#EndRegion

		#Region"Fonts"

		Private m_FontDescriptor As Nevron.Nov.Graphics.NFontDescriptor

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NInstallingFontsExample.
		''' </summary>
		Public Shared ReadOnly NInstallingFontsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
