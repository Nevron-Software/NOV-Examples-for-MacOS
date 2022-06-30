Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis model crossing example
	''' </summary>
	Public Class NAxisModelCrossingExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' Static constructor
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Chart.NAxisModelCrossingExample.NAxisModelCrossingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisModelCrossingExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Model Crossing"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)
            Dim primaryX As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryX)
            Dim primaryY As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryY)

			' configure axes
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(primaryY.Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.MajorGridLines = Me.CreateDottedGrid()
            Dim yStrip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.LightGray, 40)), Nothing, True, 0, 0, 1, 1)
            yStrip.Interlaced = True
            yScale.Strips.Add(yStrip)
            Dim xScale As Nevron.Nov.Chart.NLinearScale = CType(primaryX.Scale, Nevron.Nov.Chart.NLinearScale)
            xScale.MajorGridLines = Me.CreateDottedGrid()
            Dim xStrip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.LightGray, 40)), Nothing, True, 0, 0, 1, 1)
            xStrip.Interlaced = True
            xScale.Strips.Add(xStrip)

			' cross X and Y axes
			primaryX.Anchor = New Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor(0, Nevron.Nov.Chart.ENAxisCrossAlignment.Center, primaryY, Nevron.Nov.Chart.ENCartesianAxisOrientation.Horizontal, Nevron.Nov.Chart.ENScaleOrientation.Right, 0.0F, 100.0F)
            primaryY.Anchor = New Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor(0, Nevron.Nov.Chart.ENAxisCrossAlignment.Center, primaryX, Nevron.Nov.Chart.ENCartesianAxisOrientation.Vertical, Nevron.Nov.Chart.ENScaleOrientation.Left, 0.0F, 100.0F)

			' setup bubble series
			Dim bubble As Nevron.Nov.Chart.NBubbleSeries = New Nevron.Nov.Chart.NBubbleSeries()
            bubble.Name = "Bubble Series"
            bubble.InflateMargins = True
            bubble.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bubble.UseXValues = True

			' fill with random data
			Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 10 - 1
                bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(random.[Next](-20, 20), random.[Next](-20, 20), random.[Next](1, 6)))
            Next

            Me.m_Chart.Series.Add(bubble)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, True))
            Return chartView
        End Function

		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("Vertical Axis"))
            Dim verticalAxisAlignmentComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            verticalAxisAlignmentComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENAxisCrossAlignment)()
            AddHandler verticalAxisAlignmentComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnVerticalAxisAlignmentComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Alignment:", verticalAxisAlignmentComboBox))
            verticalAxisAlignmentComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENAxisCrossAlignment.Center)
            Dim verticalAxisOffsetUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler verticalAxisOffsetUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnVerticalAxisOffsetUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Offset:", verticalAxisOffsetUpDown))
            stack.Add(New Nevron.Nov.UI.NLabel("Horizontal Axis"))
            Dim horizontalAxisAlignmentComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            horizontalAxisAlignmentComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENAxisCrossAlignment)()
            AddHandler horizontalAxisAlignmentComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnHorizontalAxisAlignmentComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Alignment:", horizontalAxisAlignmentComboBox))
            horizontalAxisAlignmentComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENAxisCrossAlignment.Center)
            Dim horizontalAxisOffsetUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler horizontalAxisOffsetUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnHorizontalAxisOffsetUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Offset:", horizontalAxisOffsetUpDown))
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to cross two axes at a specified model offset.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnHorizontalAxisOffsetUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim anchor As Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Anchor, Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor)
            anchor.Offset = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnVerticalAxisOffsetUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim anchor As Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Anchor, Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor)
            anchor.Offset = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnHorizontalAxisAlignmentComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim anchor As Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Anchor, Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor)
            anchor.Alignment = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENAxisCrossAlignment)
        End Sub

        Private Sub OnVerticalAxisAlignmentComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim anchor As Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Anchor, Nevron.Nov.Chart.NModelCrossCartesianAxisAnchor)
            anchor.Alignment = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENAxisCrossAlignment)
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function CreateDottedGrid() As Nevron.Nov.Chart.NScaleGridLines
            Dim scaleGrid As Nevron.Nov.Chart.NScaleGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            scaleGrid.Visible = True
            scaleGrid.Stroke.Width = 1
            scaleGrid.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            scaleGrid.Stroke.Color = Nevron.Nov.Graphics.NColor.Gray
            Return scaleGrid
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisModelCrossingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
