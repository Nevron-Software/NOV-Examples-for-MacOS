Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NLinearGradientFillExample
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
            Nevron.Nov.Examples.Framework.NLinearGradientFillExample.NLinearGradientFillExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NLinearGradientFillExample), NExampleBase.NExampleBaseSchema)
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
            Me.m_Table.MaxOrdinal = 2
            Dim texts As String() = New String() {"Two Gradient Stops, Horizontal Gradient Axis", "Five Gradient Stops, Vertical Gradient Axis", "Gradient Axis Angle = 45deg, Mapping Mode = ZoomToFill", "Gradient Axis Angle = 45deg, Mapping Mode = Stretch"}
            Dim fills As Nevron.Nov.Graphics.NLinearGradientFill() = New Nevron.Nov.Graphics.NLinearGradientFill() {Me.GradientWithTwoStops(), Me.GradientWithFiveStops(), Me.GradientInZoomToFillMode(), Me.GradientInStretchMode()}

			' Add a canvas for each demonstrated gradient
			For i As Integer = 0 To fills.Length - 1
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Me.m_Table.Add(stack)
                stack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
                stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
                stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First

				' Create a widget with the proper filling
				Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
                canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NLinearGradientFillExample.defaultCanvasWidth, Nevron.Nov.Examples.Framework.NLinearGradientFillExample.defaultCanvasHeight)
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
			' Canvas width editor
			Me.m_CanvasWidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasWidthUpDown.Minimum = 100
            Me.m_CanvasWidthUpDown.Maximum = 350
            Me.m_CanvasWidthUpDown.Value = Nevron.Nov.Examples.Framework.NLinearGradientFillExample.defaultCanvasWidth
            Me.m_CanvasWidthUpDown.[Step] = 1
            Me.m_CanvasWidthUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasWidthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' Canvas height editor
			Me.m_CanvasHeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasHeightUpDown.Minimum = 100
            Me.m_CanvasHeightUpDown.Maximum = 350
            Me.m_CanvasHeightUpDown.Value = Nevron.Nov.Examples.Framework.NLinearGradientFillExample.defaultCanvasHeight
            Me.m_CanvasHeightUpDown.[Step] = 1
            Me.m_CanvasHeightUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasHeightUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Width:", Me.m_CanvasWidthUpDown))
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Canvas Height:", Me.m_CanvasHeightUpDown))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates NOV's linear gradient fills. The first row contains a horizontal linear gradient with two gradient stops and a vertical gradient with five gradient stops.
	The second row contains two gradients that have the same gradient stops and axis angles, but are mapped differently.
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

		Private Function GradientWithTwoStops() As Nevron.Nov.Graphics.NLinearGradientFill
            Dim lgf As Nevron.Nov.Graphics.NLinearGradientFill = New Nevron.Nov.Graphics.NLinearGradientFill()
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0, Nevron.Nov.Graphics.NColor.Red))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1, Nevron.Nov.Graphics.NColor.DarkBlue))
            Return lgf
        End Function

        Private Function GradientWithFiveStops() As Nevron.Nov.Graphics.NLinearGradientFill
            Dim lgf As Nevron.Nov.Graphics.NLinearGradientFill = New Nevron.Nov.Graphics.NLinearGradientFill()
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.00F, Nevron.Nov.Graphics.NColor.Red))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.25F, Nevron.Nov.Graphics.NColor.Yellow))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.50F, Nevron.Nov.Graphics.NColor.LimeGreen))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.75F, Nevron.Nov.Graphics.NColor.RoyalBlue))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.00F, Nevron.Nov.Graphics.NColor.BlueViolet))
            lgf.SetAngle(New Nevron.Nov.NAngle(90, Nevron.Nov.NUnit.Degree))
            Return lgf
        End Function

        Private Function GradientInZoomToFillMode() As Nevron.Nov.Graphics.NLinearGradientFill
            Dim lgf As Nevron.Nov.Graphics.NLinearGradientFill = New Nevron.Nov.Graphics.NLinearGradientFill()
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.0F, Nevron.Nov.Graphics.NColor.Red))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.4F, Nevron.Nov.Graphics.NColor.BlueViolet))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.5F, Nevron.Nov.Graphics.NColor.LavenderBlush))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.6F, Nevron.Nov.Graphics.NColor.BlueViolet))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.0F, Nevron.Nov.Graphics.NColor.Red))
            lgf.SetAngle(New Nevron.Nov.NAngle(45, Nevron.Nov.NUnit.Degree))
            ' FIX: Gradient Transform
			' lgf.MappingMode = ENGradientMappingMode.ZoomToFill;
			Return lgf
        End Function

        Private Function GradientInStretchMode() As Nevron.Nov.Graphics.NLinearGradientFill
            Dim lgf As Nevron.Nov.Graphics.NLinearGradientFill = New Nevron.Nov.Graphics.NLinearGradientFill()
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.0F, Nevron.Nov.Graphics.NColor.Red))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.4F, Nevron.Nov.Graphics.NColor.BlueViolet))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.5F, Nevron.Nov.Graphics.NColor.LavenderBlush))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.6F, Nevron.Nov.Graphics.NColor.BlueViolet))
            lgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.0F, Nevron.Nov.Graphics.NColor.Red))
            lgf.SetAngle(New Nevron.Nov.NAngle(45, Nevron.Nov.NUnit.Degree))
            ' FIX: Gradient Transform
            ' lgf.MappingMode = ENGradientMappingMode.Stretch;
			Return lgf
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Table As Nevron.Nov.UI.NTableFlowPanel
        Private m_CanvasWidthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_CanvasHeightUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Constants"

		Const defaultCanvasWidth As Integer = 320
        Const defaultCanvasHeight As Integer = 200

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NLinearGradientFillExample.
		''' </summary>
		Public Shared ReadOnly NLinearGradientFillExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
