Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NHatchFillExample
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
            Nevron.Nov.Examples.Framework.NHatchFillExample.NHatchFillExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NHatchFillExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Get the fill styles
			Dim hatches As Nevron.Nov.Graphics.ENHatchStyle() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Graphics.ENHatchStyle)()
            Dim count As Integer = hatches.Length

			' Create a table layout panel
			Me.m_Table = New Nevron.Nov.UI.NTableFlowPanel()
            Me.m_Table.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_Table.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_Table.Padding = New Nevron.Nov.Graphics.NMargins(30)
            Me.m_Table.HorizontalSpacing = 30
            Me.m_Table.VerticalSpacing = 30
            Me.m_Table.MaxOrdinal = 6

			' Add widgets with the proper filling and names to the panel

			For i As Integer = 0 To count - 1
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Me.m_Table.Add(stack)
                stack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
                stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
                stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First

				' Create a widget with the proper filling
				Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
                canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NHatchFillExample.defaultCanvasWidth, Nevron.Nov.Examples.Framework.NHatchFillExample.defaultCanvasHeight)
                canvas.Tag = New Nevron.Nov.Graphics.NHatchFill(hatches(i), Nevron.Nov.Examples.Framework.NHatchFillExample.defaultForegroundColor, Nevron.Nov.Examples.Framework.NHatchFillExample.defaultBackgroundColor)
                stack.Add(canvas)
                AddHandler canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)

				' Create a label with the corresponding name
				Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(hatches(CInt((i))).ToString())
                stack.Add(label)
                label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Next

			' The table must be scrollable
			Dim scroll As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
            scroll.Content = Me.m_Table
            Return scroll
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
			' Create background color editor
			Dim colorBox1 As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            colorBox1.SelectedColor = Nevron.Nov.Examples.Framework.NHatchFillExample.defaultBackgroundColor
            colorBox1.Tag = Nevron.Nov.Graphics.NHatchFill.BackgroundColorProperty
            AddHandler colorBox1.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnColorBoxSelectedColorChanged)

			' Create foreground color editor
			Dim colorBox2 As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            colorBox2.SelectedColor = Nevron.Nov.Examples.Framework.NHatchFillExample.defaultForegroundColor
            colorBox2.Tag = Nevron.Nov.Graphics.NHatchFill.ForegroundColorProperty
            AddHandler colorBox2.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnColorBoxSelectedColorChanged)

			' Canvas width editor
			Me.m_CanvasWidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasWidthUpDown.Minimum = 100
            Me.m_CanvasWidthUpDown.Maximum = 300
            Me.m_CanvasWidthUpDown.Value = Nevron.Nov.Examples.Framework.NHatchFillExample.defaultCanvasWidth
            Me.m_CanvasWidthUpDown.[Step] = 1
            Me.m_CanvasWidthUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasWidthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' Canvas height editor
			Me.m_CanvasHeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasHeightUpDown.Minimum = 100
            Me.m_CanvasHeightUpDown.Maximum = 300
            Me.m_CanvasHeightUpDown.Value = Nevron.Nov.Examples.Framework.NHatchFillExample.defaultCanvasHeight
            Me.m_CanvasHeightUpDown.[Step] = 1
            Me.m_CanvasHeightUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasHeightUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' create a stack and put the controls in it
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Background Color:", colorBox1))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("ForegroundColor:", colorBox2))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Width:", Me.m_CanvasWidthUpDown))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Height:", Me.m_CanvasHeightUpDown))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example shows the hatch fillings supported by NOV. Use the controls in the right-side panel to specify the background
	and the foreground colors of the hatches.
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
                CType(canvas.Tag, Nevron.Nov.Graphics.NHatchFill).SetValue([property], color)
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

		#Region"Fields"

		Private m_Table As Nevron.Nov.UI.NTableFlowPanel
        Private m_CanvasWidthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_CanvasHeightUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Constants"

		Const defaultCanvasWidth As Integer = 140
        Const defaultCanvasHeight As Integer = 140
        Private Shared ReadOnly defaultForegroundColor As Nevron.Nov.Graphics.NColor = New Nevron.Nov.Graphics.NColor(10, 30, 54)
        Private Shared ReadOnly defaultBackgroundColor As Nevron.Nov.Graphics.NColor = New Nevron.Nov.Graphics.NColor(208, 217, 234)

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NHatchFillExample.
		''' </summary>
		Public Shared ReadOnly NHatchFillExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
