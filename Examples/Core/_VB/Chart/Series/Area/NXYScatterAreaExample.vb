Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' XY Scatter Area Example
	''' </summary>
	Public Class NXYScatterAreaExample
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
            Nevron.Nov.Examples.Chart.NXYScatterAreaExample.NXYScatterAreaExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NXYScatterAreaExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Me.m_ChartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			Me.m_ChartView.Surface.Titles(CInt((0))).Text = "XY Scatter Area"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

            ' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            linearScale.Strips.Add(stripStyle)

            ' show the x axis grid lines
			linearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim majorGrid As Nevron.Nov.Chart.NScaleGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            majorGrid.Visible = True
            linearScale.MajorGridLines = majorGrid

			' add the area series
			Me.m_Area = New Nevron.Nov.Chart.NAreaSeries()
            Me.m_Area.Name = "Area Series"
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = True
            dataLabelStyle.ArrowStroke.Width = 0
            dataLabelStyle.Format = "<value>"
            Me.m_Area.DataLabelStyle = dataLabelStyle
            Me.m_Area.UseXValues = True

			' add xy values
			Me.m_Area.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(12, 10))
            Me.m_Area.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(25, 23))
            Me.m_Area.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(45, 12))
            Me.m_Area.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(55, 24))
            Me.m_Area.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(61, 16))
            Me.m_Area.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(69, 19))
            Me.m_Area.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(78, 17))
            chart.Series.Add(Me.m_Area)
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim changeXValuesButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Change X Values")
            AddHandler changeXValuesButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnChangeXValuesButtonClick)
            stack.Add(changeXValuesButton)
            Dim xAxisRoundToTickCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("X Axis Round To Tick")
            AddHandler xAxisRoundToTickCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnXAxisRoundToTickCheckBoxCheckedChanged)
            stack.Add(xAxisRoundToTickCheckBox)
            Dim invertXAxisCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Invert X Axis")
            AddHandler invertXAxisCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnInvertXAxisCheckBoxCheckedChanged)
            stack.Add(invertXAxisCheckBox)
            Dim invertYAxisCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Invert Y Axis")
            AddHandler invertYAxisCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnInvertYAxisCheckBoxCheckedChanged)
            stack.Add(invertYAxisCheckBox)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create an xy scatter area chart.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnChangeXValuesButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim random As System.Random = New System.Random()
            Dim dataPointCount As Integer = Me.m_Area.DataPoints.Count
            Me.m_Area.DataPoints(CInt((0))).X = CDbl(random.[Next](10))

            For i As Integer = 1 To dataPointCount - 1
                Me.m_Area.DataPoints(CInt((i))).X = Me.m_Area.DataPoints(CInt((i - 1))).X + random.[Next](1, 10)
            Next
        End Sub

        Private Sub OnInvertXAxisCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Invert = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnInvertYAxisCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Invert = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnXAxisRoundToTickCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)

            If TryCast(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
                linearScale.InflateViewRangeBegin = True
                linearScale.InflateViewRangeEnd = True
            Else
                linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.Logical
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ChartView As Nevron.Nov.Chart.NChartView
        Private m_Area As Nevron.Nov.Chart.NAreaSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NXYScatterAreaExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
