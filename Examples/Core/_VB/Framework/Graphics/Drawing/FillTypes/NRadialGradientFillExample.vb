Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NRadialGradientFillExample
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
            Nevron.Nov.Examples.Framework.NRadialGradientFillExample.NRadialGradientFillExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NRadialGradientFillExample), NExampleBase.NExampleBaseSchema)
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
            Dim texts As String() = New String() {"Two Gradient Stops (Stretch Mapping)", "Two Gradient Stops (ZoomToFill Mapping)", "Five Gradient Stops (Stretch Mapping)", "Five Gradient Stops (ZoomToFill Mapping)", "Shifted Gradient Center (Stretch Mapping)", "Shifted Gradient Center (ZoomToFill Mapping)", "Shifted Gradient Focus (Stretch Mapping)", "Shifted Gradient Focus (ZoomToFill Mapping)"}
            Dim fills As Nevron.Nov.Graphics.NRadialGradientFill() = New Nevron.Nov.Graphics.NRadialGradientFill() {Me.TwoGradientStops_Stretch(), Me.TwoGradientStops_Zoom(), Me.FiveGradientStops_Stretch(), Me.FiveGradientStops_Zoom(), Me.ShiftedCenter_Stretch(), Me.ShiftedCenter_Zoom(), Me.ShiftedFocus_Stretch(), Me.ShiftedFocus_Zoom()}

			' Add a canvas for each demonstrated gradient
			For i As Integer = 0 To fills.Length - 1
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Me.m_Table.Add(stack)
                stack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
                stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
                stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First

				' Create a widget with the proper filling
				Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
                canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NRadialGradientFillExample.defaultCanvasWidth, Nevron.Nov.Examples.Framework.NRadialGradientFillExample.defaultCanvasHeight)
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
            Me.m_CanvasWidthUpDown.Value = Nevron.Nov.Examples.Framework.NRadialGradientFillExample.defaultCanvasWidth
            Me.m_CanvasWidthUpDown.[Step] = 1
            Me.m_CanvasWidthUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasWidthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' Canvas height editor
			Me.m_CanvasHeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasHeightUpDown.Minimum = 100
            Me.m_CanvasHeightUpDown.Maximum = 350
            Me.m_CanvasHeightUpDown.Value = Nevron.Nov.Examples.Framework.NRadialGradientFillExample.defaultCanvasHeight
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
	This example demonstrates NOV's radial gradient fillings.
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

		Private Function TwoGradientStops_Stretch() As Nevron.Nov.Graphics.NRadialGradientFill
            Dim rgf As Nevron.Nov.Graphics.NRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0, Nevron.Nov.Graphics.NColor.AliceBlue))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1, Nevron.Nov.Graphics.NColor.DarkSlateBlue))
            Return rgf
        End Function

        Private Function TwoGradientStops_Zoom() As Nevron.Nov.Graphics.NRadialGradientFill
            Dim rgf As Nevron.Nov.Graphics.NRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0, Nevron.Nov.Graphics.NColor.AliceBlue))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1, Nevron.Nov.Graphics.NColor.DarkSlateBlue))
			' rgf.TextureMapping = new NFitAndAlignTextureMapping();
			Return rgf
        End Function

        Private Function FiveGradientStops_Stretch() As Nevron.Nov.Graphics.NRadialGradientFill
            Dim rgf As Nevron.Nov.Graphics.NRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.00F, Nevron.Nov.Graphics.NColor.Red))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.25F, Nevron.Nov.Graphics.NColor.Yellow))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.50F, Nevron.Nov.Graphics.NColor.LimeGreen))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.75F, Nevron.Nov.Graphics.NColor.MediumBlue))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.00F, Nevron.Nov.Graphics.NColor.DarkViolet))
            Return rgf
        End Function

        Private Function FiveGradientStops_Zoom() As Nevron.Nov.Graphics.NRadialGradientFill
            Dim rgf As Nevron.Nov.Graphics.NRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.00F, Nevron.Nov.Graphics.NColor.Red))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.25F, Nevron.Nov.Graphics.NColor.Yellow))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.50F, Nevron.Nov.Graphics.NColor.LimeGreen))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.75F, Nevron.Nov.Graphics.NColor.MediumBlue))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.00F, Nevron.Nov.Graphics.NColor.DarkViolet))
            ' FIX: Gradient Transform
            ' rgf.MappingMode = ENGradientMappingMode.ZoomToFill;
			Return rgf
        End Function

        Private Function ShiftedCenter_Stretch() As Nevron.Nov.Graphics.NRadialGradientFill
            Dim rgf As Nevron.Nov.Graphics.NRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.0F, Nevron.Nov.Graphics.NColor.Crimson))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.5F, Nevron.Nov.Graphics.NColor.Goldenrod))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.6F, Nevron.Nov.Graphics.NColor.Indigo))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.0F, Nevron.Nov.Graphics.NColor.Thistle))

			' The center coordinates are specified with values between 0 and 1
            ' FIX: Radial Gradient
			' rgf.CenterFactorX = 0.0f;
            ' rgf.CenterFactorY = 1.0f;

			Return rgf
        End Function

        Private Function ShiftedCenter_Zoom() As Nevron.Nov.Graphics.NRadialGradientFill
            Dim rgf As Nevron.Nov.Graphics.NRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.0F, Nevron.Nov.Graphics.NColor.Crimson))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.5F, Nevron.Nov.Graphics.NColor.Goldenrod))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.6F, Nevron.Nov.Graphics.NColor.Indigo))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.0F, Nevron.Nov.Graphics.NColor.Thistle))
            
			' FIX: Gradient Transform
            ' rgf.MappingMode = ENGradientMappingMode.ZoomToFill;

			' The center coordinates are specified with values between 0 and 1
            ' FIX: Radial Gradient
            ' rgf.CenterFactorX = 0.0f;
            ' rgf.CenterFactorY = 1.0f;

			Return rgf
        End Function

        Private Function ShiftedFocus_Stretch() As Nevron.Nov.Graphics.NRadialGradientFill
            Dim rgf As Nevron.Nov.Graphics.NRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.0F, Nevron.Nov.Graphics.NColor.White))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.4F, Nevron.Nov.Graphics.NColor.Red))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.0F, Nevron.Nov.Graphics.NColor.Black))
            rgf.FocusFactorX = -0.6F
            rgf.FocusFactorY = -0.6F
            Return rgf
        End Function

        Private Function ShiftedFocus_Zoom() As Nevron.Nov.Graphics.NRadialGradientFill
            Dim rgf As Nevron.Nov.Graphics.NRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.0F, Nevron.Nov.Graphics.NColor.White))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.4F, Nevron.Nov.Graphics.NColor.Red))
            rgf.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.0F, Nevron.Nov.Graphics.NColor.Black))
            rgf.FocusFactorX = -0.6F
            rgf.FocusFactorY = -0.6F
            ' FIX: Radial Gradient
            ' rgf.MappingMode = ENGradientMappingMode.ZoomToFill;
			Return rgf
        End Function

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
		''' Schema associated with NRadialGradientFillExample.
		''' </summary>
		Public Shared ReadOnly NRadialGradientFillExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
