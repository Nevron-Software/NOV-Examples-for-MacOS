Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Stacked Line Example
	''' </summary>
	Public Class NStackedLineExample
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
            Nevron.Nov.Examples.Chart.NStackedLineExample.NStackedLineExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStackedLineExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Stacked Line"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add interlaced stripe to the Y axis
			Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Strips.Add(stripStyle)

			' add the first line
			Me.m_Line1 = Me.CreateLineSeries("Line 1", Nevron.Nov.Chart.ENMultiLineMode.Series)
            Me.m_Line1.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.SeriesVisibility
            Me.m_Chart.Series.Add(Me.m_Line1)

			' add the second line
			Me.m_Line2 = Me.CreateLineSeries("Line 2", Nevron.Nov.Chart.ENMultiLineMode.Stacked)
            Me.m_Line2.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.SeriesVisibility
            Me.m_Chart.Series.Add(Me.m_Line2)

			' add the third line
			Me.m_Line3 = Me.CreateLineSeries("Line 3", Nevron.Nov.Chart.ENMultiLineMode.Stacked)
            Me.m_Line3.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.SeriesVisibility
            Me.m_Chart.Series.Add(Me.m_Line3)

			' positive data
			Me.OnPositiveDataButtonClick(Nothing)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim positiveDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Positive Values")
            AddHandler positiveDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnPositiveDataButtonClick)
            stack.Add(positiveDataButton)
            Dim positiveAndNegativeDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Positive and Negative Values")
            AddHandler positiveAndNegativeDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnPositiveAndNegativeDataButtonClick)
            stack.Add(positiveAndNegativeDataButton)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a stacked line chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnPositiveAndNegativeDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Line1.DataPoints.Clear()
            Me.m_Line2.DataPoints.Clear()
            Me.m_Line3.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 12 - 1
                Me.m_Line1.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](100) - 50))
                Me.m_Line2.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](100) - 50))
                Me.m_Line3.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](100) - 50))
            Next
        End Sub

        Private Sub OnPositiveDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Line1.DataPoints.Clear()
            Me.m_Line2.DataPoints.Clear()
            Me.m_Line3.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 12 - 1
                Me.m_Line1.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](90) + 10))
                Me.m_Line2.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](90) + 10))
                Me.m_Line3.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](90) + 10))
            Next
        End Sub

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' Creates a new line series
		''' </summary>
		''' <paramname="name"></param>
		''' <paramname="multiLineMode"></param>
		''' <returns></returns>
		Private Function CreateLineSeries(ByVal name As String, ByVal multiLineMode As Nevron.Nov.Chart.ENMultiLineMode) As Nevron.Nov.Chart.NLineSeries
            Dim line As Nevron.Nov.Chart.NLineSeries = New Nevron.Nov.Chart.NLineSeries()
            line.Name = name
            line.MultiLineMode = multiLineMode
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.ArrowLength = 0
            line.DataLabelStyle = dataLabelStyle
            Return line
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Line1 As Nevron.Nov.Chart.NLineSeries
        Private m_Line2 As Nevron.Nov.Chart.NLineSeries
        Private m_Line3 As Nevron.Nov.Chart.NLineSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStackedLineExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
