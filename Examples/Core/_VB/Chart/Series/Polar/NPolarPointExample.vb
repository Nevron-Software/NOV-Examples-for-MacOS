Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Polar point example
	''' </summary>
	Public Class NPolarPointExample
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
            Nevron.Nov.Examples.Chart.NPolarPointExample.NPolarPointExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NPolarPointExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NPolarPointExample.CreatePolarChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Polar Point"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NPolarChart)
            Me.m_Chart.SetPredefinedPolarAxes(Nevron.Nov.Chart.ENPredefinedPolarAxes.AngleValue)

			' setup polar axis
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
            linearScale.InflateViewRangeBegin = True
            linearScale.InflateViewRangeEnd = True
            linearScale.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.Cyan, 0.4F))
            strip.Interlaced = True
            linearScale.Strips.Add(strip)
            linearScale.MajorGridLines.Visible = True

			' setup polar angle axis
			Dim angularScale As Nevron.Nov.Chart.NAngularScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NAngularScale)
            strip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromRGBA(192, 192, 192, 125))
            strip.Interlaced = True
            angularScale.Strips.Add(strip)
            angularScale.MajorGridLines.Visible = True

			' create three polar point series
            Dim random As System.Random = New System.Random()
            Dim s1 As Nevron.Nov.Chart.NSeries = Me.CreatePolarPointSeries("Sample 1", Nevron.Nov.Chart.ENPointShape.Ellipse, random)
            Dim s2 As Nevron.Nov.Chart.NSeries = Me.CreatePolarPointSeries("Sample 2", Nevron.Nov.Chart.ENPointShape.Rectangle, random)
            Dim s3 As Nevron.Nov.Chart.NSeries = Me.CreatePolarPointSeries("Sample 3", Nevron.Nov.Chart.ENPointShape.Triangle, random)

			' add the series to the chart
			Me.m_Chart.Series.Add(s1)
            Me.m_Chart.Series.Add(s2)
            Me.m_Chart.Series.Add(s3)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a polar point chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

	

		#EndRegion

		#Region"Implementation"

        Private Function CreatePolarPointSeries(ByVal name As String, ByVal shape As Nevron.Nov.Chart.ENPointShape, ByVal random As System.Random) As Nevron.Nov.Chart.NSeries
            Dim series As Nevron.Nov.Chart.NPolarPointSeries = New Nevron.Nov.Chart.NPolarPointSeries()
            series.Name = name
            series.InflateMargins = False
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = False
            dataLabelStyle.Format = "<value> - <angle_in_degrees>"
            series.DataLabelStyle = dataLabelStyle
            series.Shape = shape
            series.Size = 3.0

			' add data
			For i As Integer = 0 To 1000 - 1
                series.DataPoints.Add(New Nevron.Nov.Chart.NPolarPointDataPoint(random.[Next](360), random.[Next](100)))
            Next

            Return series
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NPolarChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPolarPointExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreatePolarChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Polar)
            Return chartView
        End Function

		#EndRegion
	End Class
End Namespace
