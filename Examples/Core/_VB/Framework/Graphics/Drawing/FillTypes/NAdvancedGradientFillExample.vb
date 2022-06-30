Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors

Namespace Nevron.Nov.Examples.Framework
    Public Class NAdvancedGradientFillExample
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
            Nevron.Nov.Examples.Framework.NAdvancedGradientFillExample.NAdvancedGradientFillExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NAdvancedGradientFillExample), NExampleBase.NExampleBaseSchema)
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
            Me.m_Table.MaxOrdinal = 3
            Dim texts As String() = New String() {"Azure", "Flow", "Bulb", "Eclipse", "The Eye", "Medusa", "Kaleidoscope", "Reactor", "Green"}

			' Create the advanced gradients
			Dim fills As Nevron.Nov.Graphics.NAdvancedGradientFill() = New Nevron.Nov.Graphics.NAdvancedGradientFill() {Me.AzureLight, Me.Flow, Me.Bulb, Me.Eclipse, Me.TheEye, Me.Medusa, Me.Kaleidoscope, Me.Reactor, Me.Green}

			' Add a canvas for each demonstrated gradient
			For i As Integer = 0 To fills.Length - 1
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Me.m_Table.Add(stack)
                stack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
                stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
                stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First

				' Create a widget with the proper filling
				Dim canvas As Nevron.Nov.UI.NCanvas = New Nevron.Nov.UI.NCanvas()
                canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(Nevron.Nov.Examples.Framework.NAdvancedGradientFillExample.defaultCanvasWidth, Nevron.Nov.Examples.Framework.NAdvancedGradientFillExample.defaultCanvasHeight)
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
            Me.m_CanvasWidthUpDown.Value = Nevron.Nov.Examples.Framework.NAdvancedGradientFillExample.defaultCanvasWidth
            Me.m_CanvasWidthUpDown.[Step] = 1
            Me.m_CanvasWidthUpDown.DecimalPlaces = 0
            AddHandler Me.m_CanvasWidthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnNumericUpDownValueChanged)

			' Canvas height editor
			Me.m_CanvasHeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CanvasHeightUpDown.Minimum = 100
            Me.m_CanvasHeightUpDown.Maximum = 350
            Me.m_CanvasHeightUpDown.Value = Nevron.Nov.Examples.Framework.NAdvancedGradientFillExample.defaultCanvasHeight
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
	This example demonstrates how to create and configure advanced gradient fills.
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

		Public ReadOnly Property Eclipse As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Dim ag As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
                ag.BackgroundColor = Nevron.Nov.Graphics.NColor.MidnightBlue
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Crimson, Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.5F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.NAngle.Zero, 0.1F, 0.2F, 0.7F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.DarkOrchid, Nevron.Nov.NAngle.Zero, 0.9F, 0.9F, 1.0F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                Return ag
            End Get
        End Property

        Public ReadOnly Property Green As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Dim ag As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
                ag.BackgroundColor = Nevron.Nov.Graphics.NColor.Black
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.2F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Green, New Nevron.Nov.NAngle(90, Nevron.Nov.NUnit.Degree), 0.5F, 0.5F, 0.2F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.5F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                Return ag
            End Get
        End Property

        Public ReadOnly Property Bulb As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Dim ag As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
                ag.BackgroundColor = Nevron.Nov.Graphics.NColor.Purple
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Khaki, Nevron.Nov.NAngle.Zero, 0.65F, 0.35F, 0.4F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, New Nevron.Nov.NAngle(135, Nevron.Nov.NUnit.Degree), 0.5F, 0.5F, 0.7F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
                Return ag
            End Get
        End Property

        Public ReadOnly Property Kaleidoscope As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Dim ag As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
                ag.BackgroundColor = Nevron.Nov.Graphics.NColor.DarkSlateBlue
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.DarkSlateBlue, Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.3F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Rectangle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Cornsilk, New Nevron.Nov.NAngle(45, Nevron.Nov.NUnit.Degree), 0.5F, 0.5F, 0.4F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Rectangle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Thistle, Nevron.Nov.NAngle.Zero, 0.1F, 0.1F, 0.3F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Thistle, Nevron.Nov.NAngle.Zero, 0.9F, 0.1F, 0.3F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Thistle, Nevron.Nov.NAngle.Zero, 0.9F, 0.9F, 0.3F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Thistle, Nevron.Nov.NAngle.Zero, 0.1F, 0.9F, 0.3F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                Return ag
            End Get
        End Property

        Public ReadOnly Property TheEye As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Dim ag As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
                ag.BackgroundColor = New Nevron.Nov.Graphics.NColor(64, 0, 128)
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(128, 128, 255), Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.51F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.49F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(0, 0, 64), Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.23F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Black, Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.13F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                Return ag
            End Get
        End Property

        Public ReadOnly Property Medusa As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Dim ag As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
                ag.BackgroundColor = New Nevron.Nov.Graphics.NColor(0, 0, 64)
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(0, 128, 255), Nevron.Nov.NAngle.Zero, 0.12F, 0.57F, 0.60F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.NAngle.Zero, 0.29F, 0.29F, 0.35F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(0, 128, 255), Nevron.Nov.NAngle.Zero, 0.57F, 0.12F, 0.60F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(128, 0, 255), Nevron.Nov.NAngle.Zero, 0.60F, 0.60F, 0.37F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.NAngle.Zero, 0.84F, 0.84F, 0.50F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                Return ag
            End Get
        End Property

        Public ReadOnly Property Reactor As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Dim ag As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
                ag.BackgroundColor = Nevron.Nov.Graphics.NColor.Black
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(255, 128, 255), Nevron.Nov.NAngle.Zero, 0.50F, 0.78F, 0.35F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(128, 128, 255), Nevron.Nov.NAngle.Zero, 0.50F, 0.22F, 0.35F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.NAngle.Zero, 0.50F, 0.50F, 0.52F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                Return ag
            End Get
        End Property

        Public ReadOnly Property Flow As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Dim ag As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
                ag.BackgroundColor = New Nevron.Nov.Graphics.NColor(64, 0, 128)
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(255, 255, 128), New Nevron.Nov.NAngle(50, Nevron.Nov.NUnit.Degree), 0.38F, 0.17F, 0.48F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(255, 0, 128), Nevron.Nov.NAngle.Zero, 0.58F, 0.74F, 1, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
                Return ag
            End Get
        End Property

        Public ReadOnly Property AzureLight As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Dim ag As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
                ag.BackgroundColor = Nevron.Nov.Graphics.NColor.White
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(235, 168, 255), Nevron.Nov.NAngle.Zero, 0.87F, 0.29F, 0.92F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(New Nevron.Nov.Graphics.NColor(64, 199, 255), Nevron.Nov.NAngle.Zero, 0.53F, 0.82F, 0.81F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                ag.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.NAngle.Zero, 0.16F, 0.17F, 1, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
                Return ag
            End Get
        End Property

		#EndRegion

		#Region"Fields"

		Private m_Table As Nevron.Nov.UI.NTableFlowPanel
        Private m_CanvasWidthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_CanvasHeightUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Constants"

		Const defaultCanvasWidth As Integer = 180
        Const defaultCanvasHeight As Integer = 180

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NAdvancedGradientFillExample.
		''' </summary>
		Public Shared ReadOnly NAdvancedGradientFillExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
