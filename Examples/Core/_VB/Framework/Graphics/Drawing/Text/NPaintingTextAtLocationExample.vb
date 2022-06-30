Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports System
Imports System.Text
Imports Nevron.Nov.Editors

Namespace Nevron.Nov.Examples.Framework
	''' <summary>
	''' The example demonstrates how to paint text at location
	''' </summary>
	Public Class NPaintingTextAtLocationExample
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
            Nevron.Nov.Examples.Framework.NPaintingTextAtLocationExample.NPaintingTextAtLocationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NPaintingTextAtLocationExample), NExampleBase.NExampleBaseSchema)
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
            Me.m_HorizontalAlignmentCombo = New Nevron.Nov.UI.NComboBox()
            Me.m_HorizontalAlignmentCombo.FillFromEnum(Of Nevron.Nov.Graphics.ENTextHorzAlign)()
            AddHandler Me.m_HorizontalAlignmentCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnHorizontalAlignmentComboSelectedIndexChanged)
            Me.m_VerticalAlignmentCombo = New Nevron.Nov.UI.NComboBox()
            Me.m_VerticalAlignmentCombo.FillFromEnum(Of Nevron.Nov.Graphics.ENTextVertAlign)()
            AddHandler Me.m_VerticalAlignmentCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnVerticalAlignmentComboSelectedIndexChanged)
            Me.m_SingleLineCheckBox = New Nevron.Nov.UI.NCheckBox("Single Line")
            AddHandler Me.m_SingleLineCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSingleLineCheckBoxCheckedChanged)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Horizontal Alignment", Me.m_HorizontalAlignmentCombo))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Vertical Alignment", Me.m_VerticalAlignmentCombo))
            stack.Add(Me.m_SingleLineCheckBox)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
The example demonstrates how to paint text at location. Use the controls to the right to modify different parameters of the point paint text settings.
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
            Dim settings As Nevron.Nov.Graphics.NPaintTextPointSettings = New Nevron.Nov.Graphics.NPaintTextPointSettings()
            settings.SingleLine = Me.m_SingleLineCheckBox.Checked
            settings.VertAlign = CType(Me.m_VerticalAlignmentCombo.SelectedItem.Tag, Nevron.Nov.Graphics.ENTextVertAlign)
            settings.HorzAlign = CType(Me.m_HorizontalAlignmentCombo.SelectedItem.Tag, Nevron.Nov.Graphics.ENTextHorzAlign)
            Dim location As Nevron.Nov.Graphics.NPoint = canvas.GetContentEdge().Center

			' set styles
			paintVisitor.ClearStyles()
            paintVisitor.SetFont(New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 10))
            paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.Black)

			' create text to paint
			Dim builder As System.Text.StringBuilder = New System.Text.StringBuilder()
            builder.AppendLine("Paint text at location [" & location.X.ToString("0.") & ", " & location.Y.ToString("0.") & "]")
            builder.AppendLine("Horizontal Alignment [" & settings.HorzAlign.ToString() & "]")
            builder.AppendLine("Vertical Alignment [" & settings.VertAlign.ToString() & "]")

			' paint string
			paintVisitor.PaintString(location, builder.ToString(), settings)

			' paint location
			Dim inflate As Double = 5.0
            paintVisitor.SetFill(Nevron.Nov.Graphics.NColor.Red)
            paintVisitor.PaintRectangle(New Nevron.Nov.Graphics.NRectangle(location.X - inflate, location.Y - inflate, inflate * 2.0, inflate * 2.0))
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

		Private m_HorizontalAlignmentCombo As Nevron.Nov.UI.NComboBox
        Private m_VerticalAlignmentCombo As Nevron.Nov.UI.NComboBox
        Private m_SingleLineCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_Canvas As Nevron.Nov.UI.NCanvas

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NPaintingTextAtLocationExample.
		''' </summary>
		Public Shared ReadOnly NPaintingTextAtLocationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
