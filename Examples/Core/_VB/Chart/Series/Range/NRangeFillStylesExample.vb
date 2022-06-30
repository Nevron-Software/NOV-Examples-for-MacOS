Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Range Fill Styles Example
	''' </summary>
	Public Class NRangeFillStylesExample
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
            Nevron.Nov.Examples.Chart.NRangeFillStylesExample.NRangeFillStylesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NRangeFillStylesExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Tallest Buildings in the World"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' setup X axis
			Dim xScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            xScale.Labels.Visible = False
            xScale.InnerMajorTicks.Visible = False
            xScale.OuterMajorTicks.Visible = False
            xScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.Logical
            xScale.LogicalInflate = New Nevron.Nov.Graphics.NRange(10, 10)

			' setup Y axis
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.MajorGridLines.Visible = True
			
			' add interlaced stripe
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            yScale.Strips.Add(strip)

			' setup shape series
			Dim rangeSeries As Nevron.Nov.Chart.NRangeSeries = New Nevron.Nov.Chart.NRangeSeries()
            chart.Series.Add(rangeSeries)
            chart.FitMode = Nevron.Nov.Chart.ENCartesianChartFitMode.Aspect
            chart.Aspect = 1
            rangeSeries.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            rangeSeries.UseXValues = True
            rangeSeries.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.None

			' fill data
			Dim buildingNames As String() = New String() {"Jeddah Tower", "Burj Khalifa", "Abraj Al Bait", "Taipei 101", "Zifeng Tower"}
            Dim countryNames As String() = New String() {"Saudi Arabia", "UAE", "Saudi Arabia", "Taiwan", "China"}
            Dim legend As Nevron.Nov.Chart.NLegend = CType(chartView.Surface.Legends(0), Nevron.Nov.Chart.NLegend)
            legend.Mode = Nevron.Nov.Chart.ENLegendMode.Custom
            legend.Header = New Nevron.Nov.UI.NLabel("Some header")
            Dim xOffset As Double = 0

            For i As Integer = 0 To buildingNames.Length - 1
                Dim buildingImageResourceName As String = "RIMG_Buildings_" & buildingNames(CInt((i))).Replace(" ", "") & "_emf"
                Dim buildingImage As Nevron.Nov.Graphics.NImage = Nevron.Nov.Graphics.NImage.FromResource(NResources.Instance.GetResource(buildingImageResourceName))

				' add data point
				Dim rangeDataPoint As Nevron.Nov.Chart.NRangeDataPoint = New Nevron.Nov.Chart.NRangeDataPoint()
                Dim buildingWidth As Double = buildingImage.Width / 2
                Dim buildingHeight As Double = buildingImage.Height / 2
                rangeDataPoint.X = xOffset
                rangeDataPoint.X2 = xOffset + buildingWidth
                rangeDataPoint.Y = 0
                rangeDataPoint.Y2 = buildingHeight
                rangeDataPoint.Fill = New Nevron.Nov.Graphics.NImageFill(buildingImage)
                rangeSeries.DataPoints.Add(rangeDataPoint)
                Dim customRangeLabel As Nevron.Nov.Chart.NCustomRangeLabel = New Nevron.Nov.Chart.NCustomRangeLabel(New Nevron.Nov.Graphics.NRange(rangeDataPoint.X, rangeDataPoint.X2), buildingNames(i))
                customRangeLabel.LabelStyle.TickMode = Nevron.Nov.Chart.ENRangeLabelTickMode.Separators
                customRangeLabel.LabelStyle.FitMode = Nevron.Nov.Chart.ENRangeLabelFitMode.Wrap Or Nevron.Nov.Chart.ENRangeLabelFitMode.AutoFlip
                xScale.CustomLabels.Add(customRangeLabel)
                xOffset += buildingWidth + 10

				' add legend item
				Dim buildingCountryResourceName As String = "RIMG_Buildings_" & countryNames(CInt((i))).Replace(" ", "") & "_emf"
                Dim buildingCountryImage As Nevron.Nov.Graphics.NImage = Nevron.Nov.Graphics.NImage.FromResource(NResources.Instance.GetResource(buildingCountryResourceName))
                Dim buildingCountryImageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(buildingCountryImage)
                buildingCountryImageBox.PreferredSize = New Nevron.Nov.Graphics.NSize(40, 30)
                legend.Items.Add(New Nevron.Nov.UI.NPairBox(buildingCountryImageBox, New Nevron.Nov.UI.NLabel(buildingNames(i) & ", " & countryNames(i))))
            Next

            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demostrates how to apply different fill styles to range elements, as well as the ability of the control to use vector images in WMF, EMF and EMF+ formats.</p>"
        End Function

		#EndRegion

		#Region"Fields"

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NRangeFillStylesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
