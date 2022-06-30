Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports System
Imports System.Text
Imports Nevron.Nov.Editors

Namespace Nevron.Nov.Examples.Framework
	''' <summary>
	''' The example demonstrates how to paint text at given bounds
	''' </summary>
	Public Class NPaintingTextAtBoundsExample
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
            Nevron.Nov.Examples.Framework.NPaintingTextAtBoundsExample.NPaintingTextAtBoundsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NPaintingTextAtBoundsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Canvas = New Nevron.Nov.UI.NCanvas()
            AddHandler Me.m_Canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
            Me.m_Canvas.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            Return Me.m_Canvas
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Me.m_WrapModeCombo = New Nevron.Nov.UI.NComboBox()
            Me.m_WrapModeCombo.FillFromEnum(Of Nevron.Nov.Graphics.ENTextWrapMode)()
            AddHandler Me.m_WrapModeCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnWrapModeComboSelectedIndexChanged)
            Me.m_HorizontalAlignmentCombo = New Nevron.Nov.UI.NComboBox()
            Me.m_HorizontalAlignmentCombo.FillFromEnum(Of Nevron.Nov.Graphics.ENTextHorzAlign)()
            AddHandler Me.m_HorizontalAlignmentCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnHorizontalAlignmentComboSelectedIndexChanged)
            Me.m_VerticalAlignmentCombo = New Nevron.Nov.UI.NComboBox()
            Me.m_VerticalAlignmentCombo.FillFromEnum(Of Nevron.Nov.Graphics.ENTextVertAlign)()
            AddHandler Me.m_VerticalAlignmentCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnVerticalAlignmentComboSelectedIndexChanged)
            Me.m_SingleLineCheckBox = New Nevron.Nov.UI.NCheckBox("Single Line")
            AddHandler Me.m_SingleLineCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSingleLineCheckBoxCheckedChanged)
            Me.m_WidthPercentUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_WidthPercentUpDown.Value = 50
            Me.m_WidthPercentUpDown.Minimum = 0
            Me.m_WidthPercentUpDown.Maximum = 100.0
            AddHandler Me.m_WidthPercentUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnWidthPercentValueChanged)
            Me.m_HeightPercentUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_HeightPercentUpDown.Value = 50
            Me.m_HeightPercentUpDown.Minimum = 0
            Me.m_HeightPercentUpDown.Maximum = 100.0
            AddHandler Me.m_HeightPercentUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnHeightPercentValueChanged)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Wrap Mode", Me.m_WrapModeCombo))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Horizontal Alignment", Me.m_HorizontalAlignmentCombo))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Vertical Alignment", Me.m_VerticalAlignmentCombo))
            stack.Add(Me.m_SingleLineCheckBox)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Width Percent:", Me.m_WidthPercentUpDown))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Height Percent:", Me.m_HeightPercentUpDown))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
The example demonstrates how to paint text path. Use the controls to the right to modify different parameters of the rectangular paint text settings.
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
			Dim width As Double = contentEge.Width * Me.m_WidthPercentUpDown.Value / 100.0
            Dim height As Double = contentEge.Height * Me.m_HeightPercentUpDown.Value / 100.0
            Dim center As Nevron.Nov.Graphics.NPoint = contentEge.Center
            Dim textBounds As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(center.X - width / 2.0, center.Y - height / 2.0, width, height)

			' create the settings
			Dim settings As Nevron.Nov.Graphics.NPaintTextRectSettings = New Nevron.Nov.Graphics.NPaintTextRectSettings()
            settings.SingleLine = Me.m_SingleLineCheckBox.Checked
            settings.WrapMode = CType(Me.m_WrapModeCombo.SelectedIndex, Nevron.Nov.Graphics.ENTextWrapMode)
            settings.HorzAlign = CType(Me.m_HorizontalAlignmentCombo.SelectedIndex, Nevron.Nov.Graphics.ENTextHorzAlign)
            settings.VertAlign = CType(Me.m_VerticalAlignmentCombo.SelectedIndex, Nevron.Nov.Graphics.ENTextVertAlign)

			' create the text
			Dim builder As System.Text.StringBuilder = New System.Text.StringBuilder()
            builder.AppendLine("Paint text at bounds [" & textBounds.X.ToString("0.") & ", " & textBounds.Y.ToString("0.") & "]")
            builder.AppendLine("Horizontal Alignment [" & settings.HorzAlign.ToString() & "]")
            builder.AppendLine("Vertical Alignment [" & settings.VertAlign.ToString() & "]")

			' paint the bounding box
			paintVisitor.ClearStyles()
            paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.LightBlue)
            paintVisitor.PaintRectangle(textBounds)

			' init font and fill
			paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.Black)
            paintVisitor.SetFont(New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 10))

			' paint the text
			paintVisitor.PaintString(textBounds, builder.ToString(), settings)
        End Sub

        Private Sub OnWidthPercentValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnHeightPercentValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnWrapModeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnSingleLineCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnVerticalAlignmentComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnHorizontalAlignmentComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_WidthPercentUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_HeightPercentUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_WrapModeCombo As Nevron.Nov.UI.NComboBox
        Private m_HorizontalAlignmentCombo As Nevron.Nov.UI.NComboBox
        Private m_VerticalAlignmentCombo As Nevron.Nov.UI.NComboBox
        Private m_SingleLineCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_Canvas As Nevron.Nov.UI.NCanvas

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NPaintingTextAtBoundsExample.
		''' </summary>
		Public Shared ReadOnly NPaintingTextAtBoundsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
