Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors

Namespace Nevron.Nov.Examples.Framework
    Public Class NPredefinedAdvancedGradientsExample
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
            Nevron.Nov.Examples.Framework.NPredefinedAdvancedGradientsExample.NPredefinedAdvancedGradientsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NPredefinedAdvancedGradientsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a table panel to hold the canvases and the labels
			Me.m_Table = New Nevron.Nov.UI.NTableFlowPanel()
            Me.m_Table.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_Table.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_Table.Padding = New Nevron.Nov.Graphics.NMargins(30)
            Me.m_Table.HorizontalSpacing = 30
            Me.m_Table.VerticalSpacing = 30
            Me.m_Table.MaxOrdinal = 4
            Dim predefinedGradientSchemes As Nevron.Nov.Graphics.ENAdvancedGradientColorScheme() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Graphics.ENAdvancedGradientColorScheme)()

            For i As Integer = 0 To predefinedGradientSchemes.Length - 1
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Me.m_Table.Add(stack)
                stack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
                stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
                stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First

				' Create a widget with the proper filling
				Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
                canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NPredefinedAdvancedGradientsExample.defaultCanvasWidth, Nevron.Nov.Examples.Framework.NPredefinedAdvancedGradientsExample.defaultCanvasHeight)
                canvas.Tag = Nevron.Nov.Graphics.NAdvancedGradientFill.Create(predefinedGradientSchemes(i), 0)
                stack.Add(canvas)
                AddHandler canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)

				' Create a label with the corresponding name
				Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(predefinedGradientSchemes(CInt((i))).ToString())
                stack.Add(label)
                label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Next

			' The table must be scrollable
			Dim scroll As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
            scroll.Content = Me.m_Table
            Return scroll
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Gradients Variant:")
            Dim radioGroup As Nevron.Nov.UI.NRadioButtonGroup = New Nevron.Nov.UI.NRadioButtonGroup()
            groupBox.Content = radioGroup
            Dim radioButtonsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            radioGroup.Content = radioButtonsStack

            For i As Integer = 0 To 16 - 1
                Dim radioButton As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton("Variant " & i.ToString())
                radioButtonsStack.Add(radioButton)
            Next

            radioGroup.SelectedIndex = 0
            AddHandler radioGroup.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRadioGroupSelectedIndexChanged)

			' Canvas width editor
			Me.m_CanvasWidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasWidthUpDown.Minimum = 100
            Me.m_CanvasWidthUpDown.Maximum = 350
            Me.m_CanvasWidthUpDown.Value = Nevron.Nov.Examples.Framework.NPredefinedAdvancedGradientsExample.defaultCanvasWidth
            Me.m_CanvasWidthUpDown.[Step] = 1
            Me.m_CanvasWidthUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasWidthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' Canvas height editor
			Me.m_CanvasHeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasHeightUpDown.Minimum = 100
            Me.m_CanvasHeightUpDown.Maximum = 350
            Me.m_CanvasHeightUpDown.Value = Nevron.Nov.Examples.Framework.NPredefinedAdvancedGradientsExample.defaultCanvasHeight
            Me.m_CanvasHeightUpDown.[Step] = 1
            Me.m_CanvasHeightUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasHeightUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None
            stack.Add(groupBox)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Width:", Me.m_CanvasWidthUpDown))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Height:", Me.m_CanvasHeightUpDown))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the predefined advanced gradient fills provided by NOV. Use the radio buttons on the right to select
	the gradient variant.
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

        Private Sub OnRadioGroupSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_Table Is Nothing Then Return
            Dim predefinedGradientSchemes As Nevron.Nov.Graphics.ENAdvancedGradientColorScheme() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Graphics.ENAdvancedGradientColorScheme)()
            Dim iterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Dom.NNode) = Me.m_Table.GetSubtreeIterator(Nevron.Nov.Dom.ENTreeTraversalOrder.DepthFirstPreOrder, New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NCanvas.NCanvasSchema))
            Dim gradientVariant As Integer = CInt(args.NewValue)
            Dim schemeIndex As Integer = 0

            While iterator.MoveNext()
                Dim canvas As Nevron.Nov.UI.NCanvas = CType(iterator.Current, Nevron.Nov.UI.NCanvas)
                canvas.Tag = Nevron.Nov.Graphics.NAdvancedGradientFill.Create(predefinedGradientSchemes(System.Math.Min(System.Threading.Interlocked.Increment(schemeIndex), schemeIndex - 1)), gradientVariant)
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

		Const defaultCanvasWidth As Integer = 220
        Const defaultCanvasHeight As Integer = 136

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NPredefinedAdvancedGradientsExample.
		''' </summary>
		Public Shared ReadOnly NPredefinedAdvancedGradientsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
