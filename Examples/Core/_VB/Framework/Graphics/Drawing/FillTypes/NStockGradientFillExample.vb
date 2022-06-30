Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports System.Runtime.InteropServices

Namespace Nevron.Nov.Examples.Framework
    Public Class NStockGradientFillExample
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
            Nevron.Nov.Examples.Framework.NStockGradientFillExample.NStockGradientFillExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NStockGradientFillExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the gradient fills
			Dim fills As Nevron.Nov.Graphics.NFill()
            Dim texts As String()
            Dim columnCount As Integer = Me.CreateFillsAndDescriptions(fills, texts)

			' Create a table panel to hold the canvases and the labels
			Me.m_Table = New Nevron.Nov.UI.NTableFlowPanel()
            Me.m_Table.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_Table.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_Table.Padding = New Nevron.Nov.Graphics.NMargins(30)
            Me.m_Table.HorizontalSpacing = 30
            Me.m_Table.VerticalSpacing = 30
            Me.m_Table.MaxOrdinal = columnCount

            For i As Integer = 0 To fills.Length - 1
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Me.m_Table.Add(stack)
                stack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
                stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
                stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First

				' Create a widget with the proper filling
				Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
                canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NStockGradientFillExample.defaultCanvasWidth, Nevron.Nov.Examples.Framework.NStockGradientFillExample.defaultCanvasHeight)
                canvas.Tag = fills(i)
                stack.Add(canvas)
                AddHandler canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)

				' Create a label with the corresponding name
				Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(texts(i))
                stack.Add(label)
                label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Next

			' The table must be scrollable
			Dim scroll As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
            scroll.Content = Me.m_Table
            Return scroll
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim colorBox1 As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            colorBox1.SelectedColor = Nevron.Nov.Examples.Framework.NStockGradientFillExample.defaultBeginColor
            AddHandler colorBox1.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnColorBoxSelectedColorChanged)
            colorBox1.Tag = Nevron.Nov.Graphics.NStockGradientFill.BeginColorProperty
            Dim colorBox2 As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            colorBox2.SelectedColor = Nevron.Nov.Examples.Framework.NStockGradientFillExample.defaultEndColor
            AddHandler colorBox2.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnColorBoxSelectedColorChanged)
            colorBox2.Tag = Nevron.Nov.Graphics.NStockGradientFill.EndColorProperty

			' Canvas width editor
			Me.m_CanvasWidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasWidthUpDown.Minimum = 100
            Me.m_CanvasWidthUpDown.Maximum = 300
            Me.m_CanvasWidthUpDown.Value = Nevron.Nov.Examples.Framework.NStockGradientFillExample.defaultCanvasWidth
            Me.m_CanvasWidthUpDown.[Step] = 1
            Me.m_CanvasWidthUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasWidthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' Canvas height editor
			Me.m_CanvasHeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasHeightUpDown.Minimum = 100
            Me.m_CanvasHeightUpDown.Maximum = 300
            Me.m_CanvasHeightUpDown.Value = Nevron.Nov.Examples.Framework.NStockGradientFillExample.defaultCanvasHeight
            Me.m_CanvasHeightUpDown.[Step] = 1
            Me.m_CanvasHeightUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasHeightUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' create a stack and put the controls in it
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin Color:", colorBox1))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("End Color:", colorBox2))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Width:", Me.m_CanvasWidthUpDown))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Height:", Me.m_CanvasHeightUpDown))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example shows the gradient fillings supported by NOV. Use the controls to the right to specify the begin and end colors of the gradients.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            Dim canvas As Nevron.Nov.UI.NCanvas = TryCast(args.TargetNode, Nevron.Nov.UI.NCanvas)
            If canvas Is Nothing Then Return
            Dim fill As Nevron.Nov.Graphics.NFill = CType(canvas.Tag, Nevron.Nov.Graphics.NFill)
            args.PaintVisitor.ClearStyles()
            args.PaintVisitor.SetFill(fill)
            args.PaintVisitor.PaintRectangle(0, 0, canvas.Width, canvas.Height)
        End Sub

        Private Sub OnColorBoxSelectedColorChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_Table Is Nothing Then Return
            Dim color As Nevron.Nov.Graphics.NColor = CType(args.NewValue, Nevron.Nov.Graphics.NColor)
            Dim colorBox As Nevron.Nov.UI.NColorBox = CType(args.TargetNode, Nevron.Nov.UI.NColorBox)
            Dim [property] As Nevron.Nov.Dom.NProperty = CType(colorBox.Tag, Nevron.Nov.Dom.NProperty)
            Dim iterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Dom.NNode) = Me.m_Table.GetSubtreeIterator(Nevron.Nov.Dom.ENTreeTraversalOrder.DepthFirstPreOrder, New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NCanvas.NCanvasSchema))

            While iterator.MoveNext()
                Dim canvas As Nevron.Nov.UI.NCanvas = CType(iterator.Current, Nevron.Nov.UI.NCanvas)

				' update the gradient color that corresponds to the changed color box
				CType(canvas.Tag, Nevron.Nov.Graphics.NStockGradientFill).SetValue([property], color)

				' Invalidate the canvas
				canvas.InvalidateDisplay()
            End While
        End Sub

        Private Sub OnNumericUpDownValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_Table Is Nothing Then Return
            Dim width As Double = Me.m_CanvasWidthUpDown.Value
            Dim height As Double = Me.m_CanvasHeightUpDown.Value

			' Resize the canvases
			Dim iterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Dom.NNode) = Me.m_Table.GetSubtreeIterator(Nevron.Nov.Dom.ENTreeTraversalOrder.DepthFirstPreOrder, New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NCanvas.NCanvasSchema))

            While iterator.MoveNext()
                Dim canvas As Nevron.Nov.UI.NCanvas = CType(iterator.Current, Nevron.Nov.UI.NCanvas)
                CType(canvas.ParentNode, Nevron.Nov.UI.NWidget).PreferredWidth = width
                canvas.PreferredHeight = height
            End While
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function CreateFillsAndDescriptions(<Out> ByRef fills As Nevron.Nov.Graphics.NFill(), <Out> ByRef texts As String()) As Integer
            Dim gradientStyles As System.Array = Nevron.Nov.NEnum.GetValues(GetType(Nevron.Nov.Graphics.ENGradientStyle))
            Dim gradientVariants As System.Array = Nevron.Nov.NEnum.GetValues(GetType(Nevron.Nov.Graphics.ENGradientVariant))
            Dim styleCount As Integer = gradientStyles.Length
            Dim variantCount As Integer = gradientVariants.Length
            Dim count As Integer = styleCount * variantCount
            Dim index As Integer = 0
            fills = New Nevron.Nov.Graphics.NFill(count - 1) {}
            texts = New String(count - 1) {}

			' Create the gradient fills
			For i As Integer = 0 To variantCount - 1
                Dim [variant] As Nevron.Nov.Graphics.ENGradientVariant = CType(gradientVariants.GetValue(i), Nevron.Nov.Graphics.ENGradientVariant)

                For j As Integer = 0 To styleCount - 1
                    Dim style As Nevron.Nov.Graphics.ENGradientStyle = CType(gradientStyles.GetValue(j), Nevron.Nov.Graphics.ENGradientStyle)
                    fills(index) = New Nevron.Nov.Graphics.NStockGradientFill(style, [variant], Nevron.Nov.Examples.Framework.NStockGradientFillExample.defaultBeginColor, Nevron.Nov.Examples.Framework.NStockGradientFillExample.defaultEndColor)
                    texts(index) = style.ToString() & " " & [variant].ToString()
                    index += 1
                Next
            Next

            Return styleCount
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Table As Nevron.Nov.UI.NTableFlowPanel
        Private m_CanvasWidthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_CanvasHeightUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Constants"

		Const defaultCanvasWidth As Integer = 160
        Const defaultCanvasHeight As Integer = 100
        Private Shared ReadOnly defaultBeginColor As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Lavender
        Private Shared ReadOnly defaultEndColor As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Indigo

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NStockGradientFillExample.
		''' </summary>
		Public Shared ReadOnly NStockGradientFillExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
